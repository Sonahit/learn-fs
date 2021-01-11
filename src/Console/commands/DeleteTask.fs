namespace Application.Commands

module DeleteTask =
    open Application.Utils.Regex
    open Application.Types.Commands

    type DeleteTask() =
        inherit Command("delete", "Удалить задачу -> delete {todoId::integer}")
        override this.Regex = $"{this.Name} (\\d+)$"

        override this.Match line =
            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line = printfn "%s" this.Usage |> ignore

    let Impl = DeleteTask()
