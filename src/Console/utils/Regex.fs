namespace Application.Utils

module Regex =
    open System.Text.RegularExpressions

    let (|ParseRegex|_|) regex (str: string) =
        let m = Regex(regex).Match(str.TrimEnd())

        if m.Success then
            Some(m.Value)
        else
            None
