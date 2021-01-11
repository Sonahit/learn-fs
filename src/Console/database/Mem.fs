namespace Application

module MemDatabase =
    open Application.Types.Todo

    let mutable Todos: Todo array = [||]

    let AddTodo (todo: Todo) =
        Todos <- (Array.append Todos [| todo |])

    let UpdateTodo (name: string) (todo: Todo): Todo option =
        let oldTodo =
            Todos
            |> Array.indexed
            |> Array.tryFind (fun (_, el: Todo) -> el.Name = todo.Name)

        if oldTodo.IsSome then
            let (i, _oldTodo) = oldTodo.Value
            let newTodo = { _oldTodo with Name = name }

            Todos.[i] <- newTodo

            Some(newTodo)
        else
            None

    let DeleteTodo (todo: Todo) =
        Todos <- Array.filter (fun (el: Todo) -> el.Name = todo.Name) Todos
