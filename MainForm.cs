using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirCompCS
{
    public partial class MainForm : Form
    {
        private bool cancelled;
        private readonly DirectoryComparer comparer;

        public MainForm()
        {
            InitializeComponent();
            comparer = new DirectoryComparer();
            LoadSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Dir Compare";
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(edPath1.Text) || string.IsNullOrWhiteSpace(edPath2.Text))
            {
                MessageBox.Show("Please select both directories to compare.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(edPath1.Text) || !Directory.Exists(edPath2.Text))
            {
                MessageBox.Show("One or both directories do not exist.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetUIState(false);
            cancelled = false;

            try
            {
                var progress = new Progress<ComparisonProgress>(UpdateProgress);
                var results = await comparer.CompareDirectoriesAsync(
                    edPath1.Text, 
                    edPath2.Text, 
                    cbDiffDetails.Checked,
                    progress,
                    () => cancelled);

                DisplayResults(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during comparison: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetUIState(true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelled = true;
            btnCancel.Enabled = false;
        }

        private void sbPath1_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = edPath1.Text;
                dialog.Description = "Select first directory to compare";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    edPath1.Text = dialog.SelectedPath;
                }
            }
        }

        private void sbPath2_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = edPath2.Text;
                dialog.Description = "Select second directory to compare";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    edPath2.Text = dialog.SelectedPath;
                }
            }
        }

        private void UpdateProgress(ComparisonProgress progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ComparisonProgress>(UpdateProgress), progress);
                return;
            }

            statusBar.Items[0].Text = progress.Status;
            
            if (progress.MaxFiles > 0)
            {
                pbarProcess.Maximum = progress.MaxFiles;
                pbarProcess.Value = Math.Min(progress.CurrentFile, progress.MaxFiles);
            }

            if (progress.MaxBytes > 0)
            {
                pbarFile.Maximum = (int)Math.Min(progress.MaxBytes, int.MaxValue);
                pbarFile.Value = (int)Math.Min(progress.CurrentBytes, pbarFile.Maximum);
            }

            if (progress.FailedFiles > 0)
            {
                statusBar.Items[1].Text = progress.FailedFiles.ToString();
            }

            Application.DoEvents();
        }

        private void DisplayResults(ComparisonResults results)
        {
            lboxSource.Items.Clear();
            lboxDest.Items.Clear();
            memoReport.Clear();

            // Show different/missing files in listboxes
            for (int i = 0; i < results.SourceFiles.Count; i++)
            {
                lboxSource.Items.Add(results.SourceFiles[i]);
                lboxDest.Items.Add(i < results.DestFiles.Count ? results.DestFiles[i] : "<n/a>");
            }

            // Generate and show report
            var report = new StringBuilder();
            report.AppendLine($"Comparison Results from: {edPath1.Text} and {edPath2.Text}");
            report.AppendLine("=================================================================");
            report.AppendLine();

            foreach (var difference in results.Differences)
            {
                switch (difference.Type)
                {
                    case DifferenceType.Missing:
                        if (difference.SourceFile == "<n/a>")
                            report.AppendLine($"{edPath1.Text} is missing file: {difference.DestFile}");
                        else
                            report.AppendLine($"{edPath2.Text} is missing file: {difference.SourceFile}");
                        break;
                    
                    case DifferenceType.Different:
                        report.AppendLine($"{difference.SourceFile} : {difference.DifferenceCount} Differences");
                        if (!string.IsNullOrEmpty(difference.Details))
                        {
                            report.AppendLine(difference.Details);
                        }
                        break;
                }
            }

            report.AppendLine();
            report.AppendLine("end of report");

            memoReport.Text = report.ToString();
            memoReport.Visible = true;
            memoReport.BringToFront();
        }

        private void SetUIState(bool enabled)
        {
            btnGo.Enabled = enabled;
            btnGo.Visible = enabled;
            btnCancel.Visible = !enabled;
            btnCancel.Enabled = !enabled;

            if (!enabled)
            {
                memoReport.Visible = false;
                pbarProcess.Value = 0;
                pbarFile.Value = 0;
                statusBar.Items[0].Text = "Ready";
                statusBar.Items[1].Text = "0";
            }

            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private void LoadSettings()
        {
            try
            {
                var settingsPath = Path.Combine(Application.StartupPath, "settings.txt");
                if (File.Exists(settingsPath))
                {
                    var lines = File.ReadAllLines(settingsPath);
                    if (lines.Length >= 2)
                    {
                        edPath1.Text = lines[0];
                        edPath2.Text = lines[1];
                    }
                }
            }
            catch
            {
                // Ignore settings load errors
            }
        }

        private void SaveSettings()
        {
            try
            {
                var settingsPath = Path.Combine(Application.StartupPath, "settings.txt");
                File.WriteAllLines(settingsPath, new[] { edPath1.Text, edPath2.Text });
            }
            catch
            {
                // Ignore settings save errors
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }
}
