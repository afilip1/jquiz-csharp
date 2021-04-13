# JQuiz

This is a simple Japanese vocabulary trainer built in C#. It supports both a GUI mode (via `WpfFrontend`)
and a terminal mode (via `CliFrontend`).

The questions are formed from a pool of words in `data.db`, which is a SQLite database. It is formed from TSV files containing word data 
(n5.txt, n4.txt, ...) by using the `DBTool` script.

## Requirements
- Visual Studio 2019
- .NET 5
- PostSharp Community

## How to run
In Visual Studio:
1. Launch the solution
2. Set either `WpfFrontend` or `CliFrontend` as your startup project
3. Run

## I'm getting weird squares when I try to run `CliFrontend`???
To display Japanese characters in Command Line on Windows, set your non-Unicode display language to Japanese:\
**Control Panel > Clock and Region > Region > Administrative tab > Change system locale**

## Screenshots

### GUI mode
![Welcome screen](/screenshots/screenshot_welcome.png)
![Question screen](/screenshots/screenshot_question.png)
![Result screen](/screenshots/screenshot_result.png)

### Terminal mode
![Terminal mode](/screenshots/screenshot_terminal.png)