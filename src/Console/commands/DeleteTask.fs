namespace Application.Commands

module DeleteTask =
    open System.Text.RegularExpressions
    open Application
    open Application.Utils.Regex
    open Application.Types.Commands

    type DeleteTask() =
        inherit Command("delete", "Удалить задачу -> delete {todoId::integer}")
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

            MemDatabase.TryGetTodo(id - 1)
            |> MemDatabase.DeleteTodo

    let Impl = DeleteTask()
