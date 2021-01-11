module Csv

open System.IO

let columns =
    [ "Продукт"
      "Оплачено"
      "Цена"
      "Пользователь"
      "Продавец" ]

let strColumns =
    columns
    |> List.rev
    |> List.fold (fun acc el -> el + ";" + acc) ""


let randomWord (max: int): string =
    let r = System.Random()

    let chars =
        [ 1 .. max ]
        |> List.map (fun _ -> char ((r.Next 27) + 65))
        |> Array.ofList

    new string (chars)


let randomDate (): string =
    let seed = int System.DateTime.Now.Ticks

    (System.DateTime.Now.AddDays(float (seed % 50)))
        .ToString("MM/dd/yyyy")


let genData (rows: int) =
    let r = System.Random()

    seq {
        for _ in 0 .. rows - 1 do

            let product = randomWord 5
            let boughtDate = randomDate ()
            let price = r.Next System.Int32.MaxValue
            let user = randomWord 5
            let seller = randomWord 5

            yield (product, price, boughtDate, user, seller)
    }



let genCsv (path: string) (max: int) =
    let rows =
        genData max
        |> Seq.map
            (fun (product, price, boughtDate, user, seller) -> $"{product};{price};{boughtDate};{user};{seller};")
        |> Seq.toList

    let lines = [ strColumns ] @ rows

    File.WriteAllLines(path, lines)
