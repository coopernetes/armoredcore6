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
    printfn "cells: %A" cells
    printfn "cells length: %A" (cells |> Seq.length)
    
    cells
    |> Seq.map (fun tds -> {
            id = 0;
            name = tds[0].InnerText();
            manufacturer = tds[1].InnerText();
            ap = tds[2].InnerText() |> int64;
            anti_kinetic_defense = Some(tds[3].InnerText() |> int64);
            anti_energy_defense = Some(tds[4].InnerText() |> int64);
            anti_explosive_defense = Some(tds[5].InnerText() |> int64);
            average_defense = Some(tds[6].InnerText() |> double);
            attitude_stability = Some(tds[7].InnerText() |> int64);
            system_recovery = Some(tds[8].InnerText() |> int64);
            scan_distance = Some(tds[9].InnerText() |> int64);
            scan_effect_duration = Some(tds[10].InnerText() |> double);
            weight = tds[11].InnerText() |> int64;
            en_load = tds[12].InnerText() |> int64;
            description = tds[13].InnerText();
            image = None;
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
