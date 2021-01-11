open Application

[<EntryPoint>]
let main argv =
    App.WriteUsage()
    App.Usage()
    Utils.Stdin.ConsoleStdin App.Parse |> ignore
    0 // return an integer exit code
