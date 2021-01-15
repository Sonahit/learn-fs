namespace Application.Commands

module GetTasks =
    open Application
    open Application.Utils.Regex
    open Application.Types.Commands

    type GetTasks() =
        inherit Command("getall", "Получить список задач -> getall")


        override this.Regex = $"^{this.Name}$"

        override this.Match line =

            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line = printfn "%A" MemDatabase.Todos

    let Impl = GetTasks()
