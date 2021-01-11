namespace Application.Commands

module CreateTask =
    open System.Text.RegularExpressions
    open Application.Utils.Regex
    open Application.Utils.Stdin
    open Application.Types.Commands

    type CreateTask() =
        inherit Command("create", "Создать задачу -> create [count::integer]")

        override this.Regex = $"^{this.Name}\\s?(\\d+)?$"

        override this.Match line =

            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line =
            let m = Regex(this.Regex).Match(line.TrimEnd())

            let groups =
                List.tail [ for x in m.Groups -> x.Value ]

            let defaultCount = 1

            let count =
                if groups.Head = "" then
                    defaultCount
                else
                    int groups.Head

            let rows =
                ConsoleRequire count (List.rev [ "name"; "description" ])

            printfn "%A" rows

    let Impl = CreateTask()