open System
open System.Threading
open Suave
open XPlot.Plotly

let layout = Layout(title = "Basic Bar Chart")

[<EntryPoint>]
let main argv =

    let csvData = Csv.genData 100

    let data =
        csvData
        |> Seq.sortBy (fun (_, price, __, ___, ____) -> price)
        |> Seq.toList

    let trace =
        Scatter(
            x = List.toArray (List.map (fun (product, _, __, ___, ____) -> product) data),
            y = List.toArray (List.map (fun (_, price, __, ___, ____) -> price) data)
        )

    let plot =
        trace
        |> Chart.Plot
        |> Chart.WithLayout layout
        |> Chart.WithHeight 500
        |> Chart.WithWidth 700
        |> Chart.WithWidth 700

    let html = plot.GetHtml()

    let cts = new CancellationTokenSource()

    let conf =
        { defaultConfig with
              cancellationToken = cts.Token }

    let listening, server =
        startWebServerAsync conf (Successful.OK html)

    Async.Start(server, cts.Token)
    printfn "Make requests now"
    Console.ReadKey true |> ignore

    cts.Cancel()
    0
