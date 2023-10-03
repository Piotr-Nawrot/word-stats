namespace Fsharp.Tests

open System
open FsCheck.Xunit
open Fsharp.WordStats.Types
open Fsharp.Tests.Generators
open Fsharp.Tests.Types
open Fsharp.WordStats.TextProcessing
open FsCheck

[<assembly: Properties(Arbitrary = [|
    typeof<GoodFileNameGenerator>
    typeof<WrongFileNameGenerator>
    typeof<CountingConfigurationGenerator>
    typeof<MultiWordTextGenerator>
|] )>] do()
module Tests =
    //SafeReadFile
    [<Property(MaxTest = 2)>]
    let ``safeReadFile should return Some when called with a known good filename`` (GoodFileName filename) =
        match safeReadFile (Some filename) with
        | Some _ -> true
        | None -> false
        
    [<Property>]
    let ``safeReadFile should return None when called with a known wrong filename`` (WrongFileName filename) =
        match safeReadFile (Some filename) with
        | Some _ -> false
        | None -> true
    
    //countWordsInText
    [<Property>]
    let ``countWordsInText should return None when called with an empty string`` (config: ExecutionConfiguration) =
        match countWordsInText config "" with
        | Some _ -> false
        | None -> true

    [<Property>]
    let ``countWordsInText should return a non-empty array option when called with a text containing words`` (NonEmptyString text) (config: ExecutionConfiguration) =
        match countWordsInText config text with
        | Some array when array |> Array.isEmpty |> not -> true
        | _ when text.ToCharArray() |> Array.exists Char.IsLetterOrDigit |> not -> true
        | _ -> false
        
    [<Property>]
    let ``countWordsInText should return the same word count regardless of the case of the words when IgnoreCase is true`` (MultiWordText text) (config: ExecutionConfiguration) =
        let lowerCaseText = text.ToLowerInvariant()
        let upperCaseText = text.ToUpperInvariant()
        let ignoreCase = { config with Options = { config.Options with IgnoreCase = true }}
        
        let textCount = countWordsInText ignoreCase text
        let lowerCaseCount = countWordsInText ignoreCase lowerCaseText
        let upperCaseCount = countWordsInText ignoreCase upperCaseText
        
        textCount = lowerCaseCount && textCount = upperCaseCount 
