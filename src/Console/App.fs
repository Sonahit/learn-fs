namespace Application
// TODO: Todo cli


module App =
    open Application.Commands
    open Application.Types
    open Application.Types.Commands
    open Application.Utils

    let EventStorage = Events.EventStorage()

    let Commands: Command list =
        [ DeleteTask.Impl
          CreateTask.Impl
          UpdateTasks.Impl
          GetTask.Impl
          GetTasks.Impl ]


    let WriteUsage () =
        printfn "%s" "Todo приложение. Для использования введите номер команды"


    let Usage () =
        Commands
        |> List.map (fun el -> el.Usage)
        |> List.mapi (fun i e -> $"{i + 1}. {e}")
        |> List.iter (fun e -> printfn "%s" e)

    let Parse (line: string) =
        let cmd =
            Commands |> List.tryFind (fun el -> el.Match line)

        if cmd.IsSome then
            cmd.Value.Execute line
        else
            Stdout.log "Command not found"
