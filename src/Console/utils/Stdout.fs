namespace Application.Utils

module Stdout =
    open System

    let error (str: string): unit =
        Console.WriteLine $"[ERROR] {str}" |> ignore

    let log (str: string): unit =
        Console.WriteLine $"[LOG] {str}" |> ignore

    let warn (str: string): unit =
        Console.WriteLine $"[WARN] {str}" |> ignore
