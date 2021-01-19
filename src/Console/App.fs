namespace Application

module App =
    open System.Collections.Generic
    open Application.Events
    open Application.Commands
    open Application.Types
    open Application.Types.Commands


    let Commands: Command list =
        [ CreateTask.Impl
          DeleteTask.Impl
          UpdateTasks.Impl
          GetTask.Impl
          GetTasks.Impl
          HelpTask.Impl ]

    type ApplicationState =
        { mutable EventStorage: Events.EventStorage
          Commands: Command list }

    let WriteUsage () =
        printfn "%s" "Todo приложение. Для использования введите номер команды"


    let Usage () =
        Commands
        |> List.map (fun el -> el.Usage)
        |> List.mapi (fun i e -> $"{i + 1}. {e}")
        |> List.iter (fun e -> printfn "%s" e)

    let InvokeEvents (storage: Events.EventStorage) =
        let listeners = storage.Listeners
        let events = Events.getEvents (storage)

        events
        |> List.iter
            (fun v ->
                if listeners.ContainsKey v.Type then
                    listeners.[v.Type]
                    |> List.iter (fun l -> l.Callback v))
        |> fun _ -> storage.Events.Clear()

    let Parse (line: string) (state: ApplicationState) =

        let cmd =
            Commands |> List.tryFind (fun el -> el.Match line)

        match cmd with
        | Some v ->
            match v.Name with
            | "help" -> Usage()
            | _ -> v.Execute line
        | _ -> printfn "%s" "Command not found"

        async { InvokeEvents state.EventStorage }
        |> Async.Start

        state

    let Init (): ApplicationState =

        let eventStorage: Events.EventStorage =
            { Events = Dictionary()
              Listeners = Dictionary() }


        let app =
            { EventStorage =
                  eventStorage
                  |> Events.addListener TestEvent.TestEventType TestEvent.Listener

              Commands = Commands }

        app
