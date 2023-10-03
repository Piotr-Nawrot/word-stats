namespace Fsharp.WordStats

module Types =
    type CountingConfiguration = {
        IgnoreCase: bool
        SortDescending: bool
    }
    type ExecutionConfiguration = {
        FileName: string option
        Options: CountingConfiguration
    }


