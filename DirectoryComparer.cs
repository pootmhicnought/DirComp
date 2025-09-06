using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirCompCS
{
    public enum DifferenceType
    {
        Missing,
        Different
    }

    public class FileDifference
    {
        public string SourceFile { get; set; } = "";
        public string DestFile { get; set; } = "";
        public DifferenceType Type { get; set; }
        public int DifferenceCount { get; set; }
        public string Details { get; set; } = "";
    }

    public class ComparisonProgress
    {
        public string Status { get; set; } = "";
        public int CurrentFile { get; set; }
        public int MaxFiles { get; set; }
        public long CurrentBytes { get; set; }
        public long MaxBytes { get; set; }
        public int FailedFiles { get; set; }
    }

    public class ComparisonResults
    {
        public List<string> SourceFiles { get; set; } = new List<string>();
        public List<string> DestFiles { get; set; } = new List<string>();
        public List<FileDifference> Differences { get; set; } = new List<FileDifference>();
    }

    public class DirectoryComparer
    {
        private const int BufferSize = 4096;
        private const string Missing = "<n/a>";

        public async Task<ComparisonResults> CompareDirectoriesAsync(
            string path1, 
            string path2, 
            bool includeDiffDetails,
            IProgress<ComparisonProgress> progress,
            Func<bool> cancellationCheck)
        {
            var results = new ComparisonResults();
            var progressInfo = new ComparisonProgress();

            try
            {
                // Get file lists
                progressInfo.Status = "Getting First Path Files";
                progress?.Report(progressInfo);
                
                var sourceFiles = await Task.Run(() => GetAllFiles(path1, path1));
                
                progressInfo.Status = "Getting Second Path Files";
                progress?.Report(progressInfo);
                
                var destFiles = await Task.Run(() => GetAllFiles(path2, path2));

                progressInfo.Status = "Making paths relative";
                progress?.Report(progressInfo);

                // Sort the lists for comparison
                sourceFiles.Sort();
                destFiles.Sort();

                progressInfo.Status = "Checking for missing files";
                progress?.Report(progressInfo);

                // Align the file lists (add missing entries)
                AlignFileLists(sourceFiles, destFiles);

                results.SourceFiles = sourceFiles.ToList();
                results.DestFiles = destFiles.ToList();

                // Compare files
                progressInfo.Status = "Comparing Files";
                progressInfo.MaxFiles = sourceFiles.Count;
                progress?.Report(progressInfo);

                var failedFiles = 0;

                for (int i = 0; i < sourceFiles.Count; i++)
                {
                    if (cancellationCheck?.Invoke() == true)
                        break;

                    progressInfo.CurrentFile = i;
                    progressInfo.FailedFiles = failedFiles;

                    if (sourceFiles[i] == destFiles[i] && sourceFiles[i] != Missing)
                    {
                        progressInfo.Status = $"Comparing: {sourceFiles[i]}";
                        progress?.Report(progressInfo);

                        var sourceFullPath = Path.Combine(path1, sourceFiles[i]);
                        var destFullPath = Path.Combine(path2, destFiles[i]);

                        var comparisonResult = await CompareFilesAsync(
                            sourceFullPath, 
                            destFullPath, 
                            includeDiffDetails,
                            progress,
                            progressInfo);

                        if (comparisonResult.IsSame)
                        {
                            // Remove identical files from display lists
                            results.SourceFiles.RemoveAt(i);
                            results.DestFiles.RemoveAt(i);
                            sourceFiles.RemoveAt(i);
                            destFiles.RemoveAt(i);
                            i--; // Adjust index after removal
                        }
                        else
                        {
                            failedFiles++;
                            results.Differences.Add(new FileDifference
                            {
                                SourceFile = sourceFiles[i],
                                DestFile = destFiles[i],
                                Type = DifferenceType.Different,
                                DifferenceCount = comparisonResult.DifferenceCount,
                                Details = comparisonResult.Details
                            });
                        }
                    }
                    else
                    {
                        // Missing file
                        results.Differences.Add(new FileDifference
                        {
                            SourceFile = sourceFiles[i],
                            DestFile = destFiles[i],
                            Type = DifferenceType.Missing
                        });
                    }

                    progress?.Report(progressInfo);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error during directory comparison: {ex.Message}", ex);
            }
        }

        private List<string> GetAllFiles(string rootPath, string basePath)
        {
            var files = new List<string>();

            try
            {
                var allFiles = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
                
                foreach (var file in allFiles)
                {
                    var relativePath = Path.GetRelativePath(basePath, file);
                    // Normalize path separators to match original Delphi behavior
                    relativePath = relativePath.Replace('/', '\\');
                    files.Add(relativePath);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip directories we can't access
            }
            catch (DirectoryNotFoundException)
            {
                // Skip missing directories
            }

            return files;
        }

        private void AlignFileLists(List<string> sourceFiles, List<string> destFiles)
        {
            int sourceIndex = 0;
            
            while (sourceIndex < sourceFiles.Count)
            {
                if (sourceIndex < destFiles.Count && 
                    sourceFiles[sourceIndex] == destFiles[sourceIndex])
                {
                    sourceIndex++;
                }
                else
                {
                    var destIndex = destFiles.IndexOf(sourceFiles[sourceIndex]);
                    
                    if (destIndex == -1)
                    {
                        // File missing in destination
                        destFiles.Insert(sourceIndex, Missing);
                    }
                    else
                    {
                        // Files missing in source
                        var missingCount = destIndex - sourceIndex;
                        for (int i = 0; i < missingCount; i++)
                        {
                            sourceFiles.Insert(sourceIndex + i, Missing);
                        }
                        sourceIndex += missingCount;
                    }
                    sourceIndex++;
                }
            }

            // Handle remaining files in destination
            while (destFiles.Count > sourceFiles.Count)
            {
                sourceFiles.Add(Missing);
            }
        }

        private async Task<FileComparisonResult> CompareFilesAsync(
            string file1, 
            string file2, 
            bool includeDiffDetails,
            IProgress<ComparisonProgress> progress,
            ComparisonProgress progressInfo)
        {
            var result = new FileComparisonResult();

            try
            {
                using var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read);

                if (fs1.Length != fs2.Length)
                {
                    result.IsSame = false;
                    return result;
                }

                progressInfo.MaxBytes = fs1.Length;
                progressInfo.CurrentBytes = 0;

                var buffer1 = new byte[BufferSize];
                var buffer2 = new byte[BufferSize];
                var diffDetails = includeDiffDetails ? new List<string>() : null;
                long absolutePosition = 0;

                result.IsSame = true;

                while (fs1.Position < fs1.Length)
                {
                    var bytesToRead = (int)Math.Min(BufferSize, fs1.Length - fs1.Position);
                    
                    var read1 = await fs1.ReadAsync(buffer1, 0, bytesToRead);
                    var read2 = await fs2.ReadAsync(buffer2, 0, bytesToRead);

                    for (int i = 0; i < read1; i++)
                    {
                        absolutePosition++;
                        
                        if (buffer1[i] != buffer2[i])
                        {
                            result.IsSame = false;
                            result.DifferenceCount++;
                            
                            if (includeDiffDetails && diffDetails != null)
                            {
                                diffDetails.Add($"{absolutePosition:X10}: {buffer1[i]:X2} {buffer2[i]:X2}");
                            }
                        }
                    }

                    progressInfo.CurrentBytes = fs1.Position;
                    progress?.Report(progressInfo);

                    // Early exit if not collecting details and we found differences
                    if (!result.IsSame && !includeDiffDetails)
                        break;
                }

                if (includeDiffDetails && diffDetails?.Count > 0)
                {
                    result.Details = FormatDiffDetails(diffDetails);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error comparing files '{file1}' and '{file2}': {ex.Message}", ex);
            }
        }

        private string FormatDiffDetails(List<string> details)
        {
            if (details.Count == 0)
                return "";

            var result = new StringBuilder();
            var blocks = new List<(int start, int count)>();
            int blockStart = 0;
            int blockCount = 0;
            int lastPosition = 0;

            foreach (var detail in details)
            {
                // Parse position from hex string like "0000000123: AB CD"
                var colonIndex = detail.IndexOf(':');
                if (colonIndex > 0)
                {
                    if (int.TryParse(detail.Substring(0, colonIndex), 
                        System.Globalization.NumberStyles.HexNumber, 
                        null, out int currentPos))
                    {
                        if (currentPos > lastPosition + 1)
                        {
                            if (blockCount > 1)
                            {
                                blocks.Add((blockStart, blockCount));
                            }
                            blockStart = currentPos;
                            blockCount = 1;
                        }
                        else
                        {
                            blockCount++;
                        }
                        lastPosition = currentPos;
                    }
                }
            }

            foreach (var (start, count) in blocks)
            {
                result.AppendLine($"BlockStart: {start}, BlockCount: {count}");
            }

            return result.ToString();
        }
    }

    internal class FileComparisonResult
    {
        public bool IsSame { get; set; } = true;
        public int DifferenceCount { get; set; } = 0;
        public string Details { get; set; } = "";
    }
}
