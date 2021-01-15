namespace Application.Events

module TestEvent =
    open Application.Types.Events

    let TestEventType = "test"

    let Listener =
        Listener(fun e -> printfn "%s" "Invoked test event")
