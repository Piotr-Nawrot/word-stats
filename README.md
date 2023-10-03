# Word Stats

## Overview

The WordStats project is designed to perform text analysis on files, providing statistics on the frequency of each word. The project is implemented in F#, and it offers a command-line interface for easy operation.

## Features

- Count word occurrences in a text file
- Option to ignore case sensitivity
- Sort results in ascending or descending order

## Prerequisites

- .NET SDK
- F# Compiler

## How to Build

Navigate to the project directory and run the following command to build the project:

```powershell
dotnet build
```

## How to Run

When in project folder, you can run it using the following command:

```powershell
dotnet run -- [options]
```

### Demo Files

Two demo text files, `./demo1.txt` and `./demo2.txt`, are included in the project for testing purposes.

### Options

- `--file [path]`: Specify a path to a file you want to process. For example, you can use one of the demo files: `./demo1.txt` or `./demo2.txt`.
- `--ignore-case`: Use this flag if you want the text analysis to ignore case.
- `--sort-descending`: Use this flag if you want the results to be sorted in descending order.

#### Example

```powershell
dotnet run --file ./demo1.txt --ignore-case --sort-descending
```

This will process the text in the file located at `./demo1.txt`, ignoring case sensitivity, and sort the results in descending order.

### Example Output in PowerShell

```powershell
6: kiu
6: en
10: estas
15: de
...
```

## Troubleshooting

If you encounter any issues or invalid command options, the program will display the appropriate usage message.

## Contributing

Feel free to contribute to this project by submitting pull requests or issues.
