namespace Application.Commands

module GetTasks =
    open System
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

        override this.Execute line =

            MemDatabase.Todos
            |> Array.iteri (fun i el -> printfn "%d. Todo -> Name = %s" (i + 1) el.Name)

    let Impl = GetTasks()
