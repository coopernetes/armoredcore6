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

let convertHeaderToColumn (header: string) =
    header.ToLower().Replace(" ", "_")
        .Replace("-", "_")
        .Replace(".", "")


let pageContent (url: string) =
    HtmlDocument.Load(url).Descendants "div"
        |> Seq.filter (fun (node: HtmlNode)  -> node.HasId("page-content"))
        |> Seq.item 0

let buildUrl (href: string) =
    $"{baseUrl}{href}"

let convertRowsToHeadParts (nodes : seq<HtmlNode>) : seq<main.parts_frame_head> =
    let cells =
        nodes
        |> Seq.map (fun x -> x.Descendants "td" |> Seq.toList)
        |> Seq.toList
    printfn "cells length: %A" (cells |> Seq.length)
    
    cells
    |> Seq.map (fun tds -> {
            id = 0;
            name = tds[0].InnerText();
            manufacturer = tds[1].InnerText();
            ap = tds[2].InnerText() |> int;
            anti_kinetic_defense = tds[3].InnerText() |> int;
            anti_energy_defense = tds[4].InnerText() |> int;
            anti_explosive_defense = tds[5].InnerText() |> int;
            average_defense = tds[6].InnerText() |> decimal;
            attitude_stability = tds[7].InnerText() |> int;
            system_recovery = tds[8].InnerText() |> int;
            scan_distance = tds[9].InnerText() |> int;
            scan_effect_duration = tds[10].InnerText() |> decimal;
            weight = tds[11].InnerText() |> int;
            en_load = tds[12].InnerText() |> int;
            description = tds[13].InnerText();
            image = None
                // This was generated and is close enough to what I want if images ever do get added
                // If images exist, I want to download them as base64 encoded bytes and store in the database
                // (tds[14].Descendants "img"
                //         |> Seq.map (fun x -> x.TryGetAttribute("src"))
                //         |> Seq.filter (fun x -> x.IsSome)
                //         |> Option.map (fun (src: HtmlAttribute) -> src.Value())
                //         |> Option.map (fun url -> Http.Request(url, httpMethod = "GET"))
                //         |> Option.filter (fun (response: HttpResponse) -> response.StatusCode = 200)
                //         |> Option.map (fun (response: HttpResponse) -> response.Body)
                //         |> Option.map (fun (body: HttpResponseBody) -> 
                //             match body with
                //             | HttpResponseBody.Binary bytes -> bytes
                //             | _ -> [||]));
        })



/// Opens a connection and creates a QueryContext that will generate SQL Server dialect queries
let openContext() = 
    let compiler = SqlKata.Compilers.SqliteCompiler()
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
    
    let headParts =
        links
        |> Seq.item 0
        |> buildUrl
        |> pageContent
        |> fun x -> x.Descendants "table"
        |> Seq.find (fun x -> x.HasClass("wiki-content-table"))
        |> fun x -> x.Descendants "tr"
        |> Seq.skip 1 // Skip the header row
        |> convertRowsToHeadParts
    printfn "scraped /head page: %A" headParts

    for part in headParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_frame_head do
                entity part
                getId p.id
            }
        printfn "inserted %A" result.Result
    0
