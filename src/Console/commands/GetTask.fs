namespace Application.Commands

module GetTask =
    open Application.Utils.Regex
    open Application.Types.Commands

    type GetTask() =
        inherit Command("get", "Получить задачу -> get {todoId::integer}")
        override this.Regex = $"{this.Name} (\\d+)$"

        override this.Match line =

            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line = printfn "%s" this.Usage |> ignore

    let Impl = GetTask()
