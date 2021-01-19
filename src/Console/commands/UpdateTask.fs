namespace Application.Commands

module UpdateTasks =
    open System.Text.RegularExpressions
    open Application
    open Application.Utils.Regex
    open Application.Utils.Stdin
    open Application.Types.Commands

    type UpdateTasks() =
        inherit Command("update", "Обновить задачу -> update {todoId::integer}")

        override this.Regex = $"{this.Name} (\\d+)$"

        override this.Match line =

            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line =
            let m = Regex(this.Regex).Match(line.TrimEnd())

            let groups =
                List.tail [ for x in m.Groups -> x.Value ]

            let defaultId = 0

            let id =
                match groups.Head with
                | "" -> defaultId
                | _ -> int groups.Head

            match id with
            | i when i <= defaultId -> printfn "%s %d" "Id should not be les than" defaultId
            | id ->
                let rows = ConsoleRequire 1 (List.rev [ "name" ])

                (List.map
                    ((fun (el: (string) list) -> el.Item 0)
                     >> (fun name -> MemDatabase.UpdateTodoById id { Name = name }))
                    rows)
                |> ignore

    let Impl = UpdateTasks()
