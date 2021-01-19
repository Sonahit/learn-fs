namespace Application.Commands

module GetTask =
    open System.Text.RegularExpressions
    open Application
    open Application.Utils.Regex
    open Application.Types.Commands

    type GetTask() =
        inherit Command("get", "Получить задачу -> get {todoId::integer}")
        override this.Regex = $"{this.Name} (\\d+)$"

        override this.Match line =

            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line =
            let m = Regex(this.Regex).Match(line.TrimEnd())

            let groups =
                List.tail [ for x in m.Groups -> x.Value ]

            let id = int groups.Head
            let todo = MemDatabase.TryGetTodo(id - 1)

            match todo with
            | Some v -> printfn "%d. Todo -> Name = %s" id v.Name
            | _ -> printfn "Todo %d is not found" id

    let Impl = GetTask()
