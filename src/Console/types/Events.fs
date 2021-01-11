namespace Application.Types

module Events =

    [<AbstractClass>]
    type Event(_type: string, value: obj, ?name: string) =
        member this.Name = (defaultArg name _type)
        member this.Value = value
        member this.Type = _type

    type Listener(cbk: Event -> obj) =
        member this.Callback = cbk

    type EventStorage() =
        member this.Events: Event list = []
        member this.Listeners: Listener list = []
