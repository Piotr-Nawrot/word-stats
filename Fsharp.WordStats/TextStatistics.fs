namespace Fsharp.WordStats

open Fsharp.WordStats.Types
open System

module TextStatistics =
    let private countWordsUnsorted ignoreCase (text: string) =
        text.Split([|' '; '\n'; '\t'; '\r'; ','; '.'; ';'; '!'; '?';|], StringSplitOptions.RemoveEmptyEntries)
        |> Array.groupBy (fun word -> if ignoreCase then word.ToUpperInvariant() else word)
        |> Array.Parallel.map (fun (key, group) -> key, group.Length)
    
    let private sortWordsDescending: (string * int) array -> (string * int) array =
        Array.sortByDescending snd
    let private sortWordsAscending: (string * int) array -> (string * int) array =
        Array.sortBy snd

    let countWords (config: CountingConfiguration) (text: string) =
        let wordsWithCounts = countWordsUnsorted config.IgnoreCase text
        let sort = if config.SortDescending then sortWordsDescending else sortWordsAscending
        
        sort wordsWithCounts