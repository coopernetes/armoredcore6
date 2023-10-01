module WikidotScraper.Program

open System
open FSharp.Data
open SqlHydra.Query
open System.Data.SQLite
open WikidotScraper.DatabaseTypes

[<Literal>]
let baseUrl = "http://armoredcore6.wikidot.com"
let partsLinkPage = $"{baseUrl}/wiki:armored-core-vi:fires-of-rubicon-parts"

let hrefToTable = [
    ("/head", "parts_frame_head")
    ("/core", "parts_frame_core")
    ("/arms", "parts_frame_arms")
    ("/legs", "parts_frame_legs")
    ("/boosters", "parts_internal_boosters")
    ("/fcs", "parts_internal_fcs")
    ("/generator", "parts_internal_generator")
    ("/weapon", "parts_weapon")
]

let pageContent (url: string) =
    HtmlDocument.Load(url).Descendants "div"
        |> Seq.filter (fun (node: HtmlNode)  -> node.HasId("page-content"))
        |> Seq.item 0

let buildUrl (href: string) =
    $"{baseUrl}{href}"

/// Opens a connection and creates a QueryContext that will generate SQL Server dialect queries
let openContext() = 
    let compiler = SqlKata.Compilers.SqlServerCompiler()
    let conn = new SQLiteConnection("Data Source=" + __SOURCE_DIRECTORY__ + "@/../data/ArmoredCore6.db")
    conn.Open()
    new QueryContext(conn, compiler)

[<EntryPoint>]
let main args =
    let mainPage = pageContent partsLinkPage

    let task = 
        selectTask HydraReader.Read (Create openContext) {
            for p in main.test do
            select (p.id, p.text_field ,p.number, p.number2, p.data) into selected
            mapList (
                let id, text, num, num2, data = selected
                $"id: {id} " +
                $"text_field: {text} " +
                $"""number: {if num.IsSome then num.Value else 0} """ +
                $"""number2: {if num2.IsSome then num2.Value else 0} """ +
                $"""data: {if data.IsSome then System.Text.Encoding.UTF8.GetString data.Value else "NULL"}"""
            )
        }
    printfn "result: %A" (String.Join("\n", task.Result))

    let links =
        mainPage.Descendants "a"
        |> Seq.choose (fun x ->
            x.TryGetAttribute("href")
            |> Option.filter (fun href -> not (href.Value() = "/os-tuning")))
            |> Seq.map (fun x -> x.Value())
    
    let scrapeHead =
        links
        |> Seq.item 0
        |> buildUrl
        |> pageContent

    printfn "scraped /head page: %A" scrapeHead
    0
