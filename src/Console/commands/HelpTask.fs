namespace Application.Commands

module HelpTask =
    open System.Text.RegularExpressions
    open Application.Utils.Regex
    open Application.Types.Commands
    open Application.Commands

    type HelpTask() =
        inherit Command("help", "Получить помощь по программе -> help")
        override this.Regex = $"^{this.Name}"

        override this.Match line =
            match line with
            | ParseRegex this.Regex _ -> true
            | _ -> false

        override this.Execute line = ignore line

    let Impl = HelpTask()
