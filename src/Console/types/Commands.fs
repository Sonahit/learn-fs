namespace Application.Types

module Commands =

    [<AbstractClass>]
    type Command(name: string, usage: string) =
        member this.Name = name
        member this.Usage = usage
        abstract member Regex: string
        abstract member Match: string -> bool
        abstract member Execute: string -> unit
