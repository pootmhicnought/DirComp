# Directory Comparison Tool (C#)

A C# Windows Forms conversion of the original Delphi Directory Comparison utility.

## Overview

This application compares the contents of two directories and reports:
- Files that exist in one directory but not the other
- Files that exist in both directories but have different content
- Optional detailed byte-level differences

## Features

- **Recursive directory comparison** - Compares all files in subdirectories
- **Missing file detection** - Shows files that exist in one directory but not the other  
- **Content comparison** - Byte-by-byte comparison of file contents
- **Difference details** - Optional detailed output showing byte-level differences
- **Progress tracking** - Progress bars for both overall progress and individual file comparison
- **Settings persistence** - Remembers the last used directory paths
- **Cancellation support** - Cancel long-running operations

## Requirements

- .NET 8.0 or later
- Windows (Windows Forms dependency)

## Usage

1. **Select directories**: Use the "..." buttons to browse and select the two directories to compare
2. **Choose options**: Check "Output Diff Details" if you want detailed byte-level difference information
3. **Start comparison**: Click "GO" to begin the comparison
4. **View results**: Results are displayed in the report area, showing missing files and files with differences

## Original Delphi Features Converted

- ✅ Directory browsing and path selection
- ✅ Recursive file discovery (replaced `AbFindFiles` with `Directory.GetFiles`)
- ✅ Byte-by-byte file comparison
- ✅ Missing file alignment between directory lists  
- ✅ Progress reporting with cancellation support
- ✅ Settings persistence (simplified from INI to text file)
- ✅ Detailed difference reporting with block analysis

## Architecture

- **MainForm**: Windows Forms UI with async/await for non-blocking operations
- **DirectoryComparer**: Core comparison logic with progress reporting
- **Supporting classes**: Progress tracking, results modeling, and difference analysis

## Building

If you have the .NET SDK installed:
```bash
dotnet build DirCompCS.csproj
dotnet run
```

## Differences from Original

- **Async operations**: Uses async/await instead of blocking UI with `Application.ProcessMessages`
- **Modern UI**: Uses StatusStrip instead of StatusBar, RichTextBox for better text display
- **Simplified settings**: Uses simple text file instead of INI file
- **Error handling**: Improved exception handling and user feedback
- **Path normalization**: Better cross-platform path handling (though still Windows-only due to WinForms)

## Files

- `DirCompCS.csproj` - Project file
- `Program.cs` - Application entry point
- `MainForm.cs` - Main form logic and event handlers  
- `MainForm.Designer.cs` - Form design and control layout
- `DirectoryComparer.cs` - Core comparison logic and supporting classes
- `README.md` - This file
