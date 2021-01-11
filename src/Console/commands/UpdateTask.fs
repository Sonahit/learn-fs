namespace Application.Commands

module UpdateTasks =
    open Application.Utils.Regex
    open Application.Types.Commands

    type UpdateTasks() =
        inherit Command("update", "Обновить задачу -> update {todoId::integer}")

        override this.Regex = $"{this.Name} (\\d+)$"

        override this.Match line =

            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line = printfn "%s" this.Usage |> ignore

    let Impl = UpdateTasks()
