namespace Application.Types

module Events =
    open System.Collections.Generic

    type EventType = string

    type Event(_type: EventType, value: obj, ?name: string) =
        member this.Name = (defaultArg name _type)
        member this.Value = value
        member this.Type = _type

    type Listener(cbk: Event -> unit) =
        member this.Callback = cbk

    type EventStorage =
        { Events: Dictionary<EventType, Event list>
          Listeners: Dictionary<EventType, Listener list> }

    let addEvent (ev: Event) (storage: EventStorage): EventStorage =
        let oldEvents = storage.Events
        let evType = ev.Type
        let contains = oldEvents.ContainsKey evType

        let events =
            if contains then
                oldEvents.[evType]
            else
                []

        let newEvents = List.append events [ ev ]

        if contains then
            oldEvents.[evType] <- newEvents
        else
            oldEvents.Add(evType, newEvents)

        { storage with Events = oldEvents }

    let getEventsByType (eventType: EventType) (storage: EventStorage): Event list =
        if storage.Events.Values.Count <= 0 then
            []
        else
            storage.Events
            |> Seq.filter (fun e -> e.Key = eventType)
            |> Seq.map (fun e -> e.Value)
            |> Seq.reduce (List.append)
            |> Seq.toList

    let getEvents (storage: EventStorage): Event list =
        if storage.Events.Values.Count <= 0 then
            []
        else
            storage.Events
            |> Seq.map (fun e -> e.Value)
            |> Seq.reduce (List.append)
            |> Seq.toList

    let addListener (evType: EventType) (listener: Listener) (storage: EventStorage): EventStorage =
        let oldListeners = storage.Listeners
        let contains = oldListeners.ContainsKey evType

        let listeners =
            if contains then
                oldListeners.[evType]
            else
                []

        let newListeners = List.append listeners [ listener ]

        if contains then
            oldListeners.[evType] <- newListeners
        else
            oldListeners.Add(evType, newListeners)

        { storage with
              Listeners = oldListeners }
