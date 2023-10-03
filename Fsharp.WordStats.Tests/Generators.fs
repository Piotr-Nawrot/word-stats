namespace Fsharp.Tests

open System
open FsCheck
open Fsharp.WordStats.Types
open Fsharp.Tests.Types

module Generators =
    type GoodFileNameGenerator() = 
        static member GoodFileName() =
            ["demo1.txt"; "demo2.txt"]
            |> Gen.elements
            |> Arb.fromGen

    type WrongFileNameGenerator() = 
        static member WrongFileName() = 
            Gen.sized (fun _ ->
                Gen.nonEmptyListOf Arb.generate<char>
                |> Gen.map (fun chars -> chars |> List.toArray |> string )
                |> Gen.map (fun filename -> filename + ".txt")        
                |> Gen.filter (fun filename -> filename <> "demo1.txt" && filename <> "demo2.txt" )
                |> Gen.map WrongFileName
            )
            |> Arb.fromGen
            
    type MultiWordTextGenerator() = 
        static member MultiWordText() =
            let alphabet = [ 'S'; 'I'; 'M'; 's'; 'i'; 'm'; ]
            let letterGen : Gen<char> = Gen.elements alphabet
            
            let randomWordsGen =
                Gen.choose (1, 4) >>= (fun n -> Gen.listOfLength n letterGen)
                |> Gen.map (List.toArray >> String.Concat)
                
            let randomSentenceGen = 
                Gen.listOfLength 100 randomWordsGen
                |> Gen.map (fun words -> String.Join(" ", words))

            randomSentenceGen
            |> Gen.map MultiWordText
            |> Arb.fromGen
            
    type CountingConfigurationGenerator() =
        static member CountingConfiguration() =
            gen {
                let! ignoreCase = Arb.generate<bool>   
                let! sortDescending = Arb.generate<bool>
                return {
                    IgnoreCase = ignoreCase
                    SortDescending = sortDescending
                }
            } |> Arb.fromGen
