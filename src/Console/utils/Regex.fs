namespace Application.Utils

module Regex =
    open System.Text.RegularExpressions

    let (|ParseRegex|_|) regex (str: string) =
        let m = Regex(regex).Match(str.TrimEnd())

        match m.Success with
        | true -> Some m.Value
        | false -> None
