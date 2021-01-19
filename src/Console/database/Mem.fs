namespace Application

module MemDatabase =
    open Application.Types.Todo

    let mutable Todos: Todo array = [||]

    let AddTodo (todo: Todo) =
        Todos <- (Array.append Todos [| todo |])

    let UpdateTodoByName (name: string) (newTodo: Todo): Todo option =
        let oldTodo =
            Todos
            |> Array.indexed
            |> Array.tryFind (fun (_, el: Todo) -> el.Name = name)

        match oldTodo with
        | Some (i, _) ->
            Todos.[i] <- newTodo
            Some(newTodo)
        | None ->
            printfn ("Todo is not found with name '%s'") name
            None

    let UpdateTodoById (id: int) (newTodo: Todo): Todo option =
        let oldTodo =
            Todos
            |> Array.indexed
            |> Array.tryFind (fun (i, _) -> i = id)

        match oldTodo with
        | Some (i, _) ->
            Todos.[i] <- newTodo
            Some(newTodo)
        | None ->
            printfn ("Todo is not found with id '%d'") id
            None

    let TryGetTodo (id: int): Todo option =
        try
            Some Todos.[id]
        with _ -> None

    let DeleteTodo (todo: Todo option) =
        match todo with
        | Some todo -> Todos <- Array.filter (fun (el: Todo) -> el.Name = todo.Name) Todos
        | None -> printfn "Provide correct todo"
