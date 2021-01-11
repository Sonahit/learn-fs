namespace Application.Utils


module Stdin =
    open System

    let rec ConsoleStdin (cbk: string -> 't) =
        Seq.initInfinite (fun _ -> Console.ReadLine())
        |> Seq.map
            (fun line ->
                match line with
                | "exit" -> 0
                | l ->
                    (cbk l |> ignore
                     1))
        |> Seq.item 0
        |> fun result ->
            match result with
            | 1 -> ConsoleStdin cbk
            | _ -> 0

    let rec ConsoleRequire (count: int) (rows: string list): string list list =
        printfn
            "Required: %s"
            (rows
             |> List.fold (fun acc el -> if acc = "" then el else el + "; " + acc) "")

        Seq.init count (fun _ -> Console.ReadLine())
        |> Seq.map
            (fun line ->
                let words: string array = line.Split [| ' ' |]

                if words.Length < rows.Length then
                    ConsoleRequire 1 rows |> List.head
                elif (words |> Array.exists (fun el -> el = "")) then
                    ConsoleRequire 1 rows |> List.head
                else
                    Array.toList words)
        |> Seq.toList
