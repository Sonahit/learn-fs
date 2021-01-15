open Application

[<EntryPoint>]
let main argv =
    App.WriteUsage()
    App.Usage()

    let appState = App.Init()

    let cmd state =
        Utils.Stdin.ConsoleStdin App.Parse state

    cmd appState |> ignore

    0 // return an integer exit code
