namespace Application.Commands

module CreateTask =
    open System.Text.RegularExpressions
    open Application
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
                match groups.Head with
                | "" -> defaultCount
                | _ -> int groups.Head

            let rows =
                ConsoleRequire count (List.rev [ "name" ])

            rows
            |> List.map (fun (el: (string) list) -> (el.Item 0))
            |> List.iter (fun name -> MemDatabase.AddTodo { Name = name })
            |> ignore


            MemDatabase.Todos
            |> Array.iteri (fun id el -> printfn "%d. Todo -> Name = %s" (id + 1) el.Name)

    let Impl = CreateTask()
