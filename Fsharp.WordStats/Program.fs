open Fsharp.WordStats.Types
open Fsharp.WordStats.TextProcessing
open Argu

type CliArguments =
    | File of path:string option
    | Ignore_Case
    | Sort_Descending

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | File _ -> "Specify a path to a file you want to process."
            | Ignore_Case _ -> "Specify whether the text analysis should ignore case"
            | Sort_Descending _ -> "If set, the results will be sorted in descending order."
            
let getConfig (args: ParseResults<CliArguments>) =
    try
        let file = args.GetResult CliArguments.File 
        let ignoreCase = args.Contains CliArguments.Ignore_Case 
        let sortDescending = args.Contains CliArguments.Sort_Descending
        Some {
            FileName = file
            Options = { IgnoreCase = ignoreCase; SortDescending = sortDescending }
        }
    with
        | :? ArguParseException -> None

[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<CliArguments>(programName = "Fsharp.WordStats.exe")

    match parser.Parse argv |> getConfig with
    | Some cfg -> 
        cfg |> countWordsInFile |> printWordCounts
    | None ->
        parser.PrintUsage() |> printfn "%s"

    0