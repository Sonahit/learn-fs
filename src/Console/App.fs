namespace Application
// TODO: Todo cli


module App =
    open System.Collections.Generic
    open Application.Events
    open Application.Commands
    open Application.Types
    open Application.Types.Commands
    open Application.Utils


    let Commands: Command list =
        [ DeleteTask.Impl
          CreateTask.Impl
          UpdateTasks.Impl
          GetTask.Impl
          GetTasks.Impl ]

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

        let testEvent =
            Events.Event(TestEvent.TestEventType, "hello_world")

        state.EventStorage <- Events.addEvent testEvent state.EventStorage

        if cmd.IsSome then
            cmd.Value.Execute line
        else
            Stdout.log "Command not found"

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
