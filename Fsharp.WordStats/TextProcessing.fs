namespace Fsharp.WordStats

module TextProcessing = 
    open System.IO
    open Fsharp.WordStats.Types
    open TextStatistics

    let safeReadFile (filename: string option) =
        match filename with
        | Some f when File.Exists(f) -> File.ReadAllText(f) |> Some
        | Some _ -> None
        | None -> 
            printfn "No file provided."
            None

    let countWordsInText (config: ExecutionConfiguration) (text: string) =
        let stats =
            text
            |> countWords config.Options

        match stats with
        | [| |]  -> None
        | array  -> Some array

    let countWordsInFile config =
        match config.FileName |> safeReadFile with
        | Some text -> 
            let wordStats = countWords config.Options text
            Some wordStats
        | None ->
            config.FileName |> Option.iter (printfn "File does not exist: %s")
            None

    let printWordCounts (wordCounts: (string * int) array option) =
        let printCounts counts =
            counts
            |> Array.iter (fun (word, count) -> printfn $"%d{count}: %s{word}")

        wordCounts
        |> Option.iter printCounts