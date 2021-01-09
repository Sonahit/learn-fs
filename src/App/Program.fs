open System
open System.Threading
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open XPlot.Plotly

let layout = Layout(title = "Product - Price")

[<EntryPoint>]
let main argv =
    let cts = new CancellationTokenSource()

    let conf =
        { defaultConfig with
              cancellationToken = cts.Token }

    let getPlot (rows: int) =
        let csvData = Csv.genData rows

        let data =
            csvData
            |> Seq.sortByDescending (fun (_, price, __, ___, ____) -> price)
            |> Seq.toList

        let trace =
            Scatter(
                x = List.toArray (List.map (fun (product, _, __, ___, ____) -> product) data),
                y = List.toArray (List.map (fun (_, price, __, ___, ____) -> price) data)
            )

        trace
        |> Chart.Plot
        |> Chart.WithLayout layout
        |> Chart.WithHeight 500
        |> Chart.WithWidth 1000

    let app =
        choose [ GET
                 >=> choose [ path "/" >=> OK(getPlot(10).GetHtml()) ] ]

    let listening, server = startWebServerAsync conf app

    Async.Start(server, cts.Token)
    Console.ReadKey true |> ignore

    cts.Cancel()
    0
