module WikidotScraper.Program

open System
open FSharp.Data
open SqlHydra.Query
open System.Data.SQLite
open WikidotScraper.DatabaseTypes

[<Literal>]
let baseUrl = "http://armoredcore6.wikidot.com"

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

let TableContentToRowList (content: HtmlNode) =
    let table = 
        content.Descendants "table"
        |> Seq.find (fun x -> x.HasClass("wiki-content-table"))

    table.Descendants "tr"
    |> Seq.skip 1 // Skip the header row
    |> Seq.map (fun x -> x.Descendants "td"
                                    |> Seq.map (fun x -> x.InnerText())
                                    |> Seq.toList)
    |> Seq.toList

let ToHeadParts (rows : list<list<string>>) : seq<main.parts_frame_head> =
    rows
    |> Seq.map (fun tds -> {
            id = 0;
            name = tds[0];
            manufacturer = tds[1];
            ap = tds[2] |> int;
            anti_kinetic_defense = tds[3] |> int;
            anti_energy_defense = tds[4] |> int;
            anti_explosive_defense = tds[5] |> int;
            average_defense = tds[6] |> decimal;
            attitude_stability = tds[7] |> int;
            system_recovery = tds[8] |> int;
            scan_distance = tds[9] |> int;
            scan_effect_duration = tds[10] |> decimal;
            weight = tds[11] |> int;
            en_load = tds[12] |> int;
            description = tds[13];
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

let ToCoreParts (rows : list<list<string>>) : seq<main.parts_frame_core> =
    rows
    |> Seq.map (fun tds -> 
    {
        id = 0;
        name = tds.[0];
        manufacturer = tds.[1];
        ap = tds.[2] |> int;
        anti_kinetic_defense = tds.[3] |> int;
        anti_energy_defense = tds.[4] |> int;
        anti_explosive_defense = tds.[5] |> int;
        average_defense = tds.[6] |> decimal;
        attitude_stability = tds.[7] |> int;
        booster_efficiency_adjustment = tds.[8] |> int;
        generator_output_adjustment = tds.[9] |> int;
        generator_supply_adjustment = tds.[10] |> int;
        weight = tds.[11] |> int;
        en_load = tds.[12] |> int;
        description = tds.[13];
        image = None
    })

let ToArmParts (rows : list<list<string>>) : seq<main.parts_frame_arms> =
    rows
    |> Seq.map (fun tds ->
    // This row is incomplete
    // Source: https://armoredcore.fandom.com/wiki/AC-3000_WRECKER
    if tds.[0] = "AC-3000 WRECKER" then
        {
            id = 0;
            name = tds.[0];
            manufacturer = tds.[1];
            ap = tds.[2] |> int;
            anti_kinetic_defense = 232;
            anti_energy_defense = 170;
            anti_explosive_defense = 237;
            average_defense = 213.0m; // This value is missing from both so this is a guess
            arms_load_limit = tds.[7] |> int;
            recoil_control = 232
            firearms_specialization = tds.[9] |> int;
            melee_specialization = 13;
            weight = tds.[11] |> int;
            en_load = tds.[12] |> int;
            description = tds.[13];
            image = None
        }
    else
    {
        id = 0;
        name = tds.[0];
        manufacturer = tds.[1];
        ap = tds.[2] |> int;
        anti_kinetic_defense = tds.[3] |> int;
        anti_energy_defense = tds.[4] |> int;
        anti_explosive_defense = tds.[5] |> int;
        average_defense = tds.[6] |> decimal;
        arms_load_limit = tds.[7] |> int;
        recoil_control = tds.[8] |> int;
        firearms_specialization = tds.[9] |> int;
        melee_specialization = tds.[10] |> int;
        weight = tds.[11] |> int;
        en_load = tds.[12] |> int;
        description = tds.[13];
        image = None
    })

let ToLegParts (rows : list<list<string>>) : seq<main.parts_frame_legs> =
    rows
    |> Seq.map (fun tds ->
    {
        id = 0;
        name = tds.[0];
        part_type = tds.[1];
        manufacturer = tds.[2];
        ap = tds.[3] |> int;
        anti_kinetic_defense = tds.[4] |> int;
        anti_energy_defense = tds.[5] |> int;
        anti_explosive_defense = tds.[6] |> int;
        average_defense = tds.[7] |> decimal;
        attitude_stability = tds.[8] |> int;
        load_limit = tds.[9] |> int;
        jump_distance = match tds.[10] with
                        | "" -> None
                        | _ -> Some (tds.[10] |> int);
        jump_height = if tds.[11] = "" then None else Some (tds.[11] |> int);
        weight = tds.[12] |> int;
        en_load = tds.[13] |> int;
        description = tds.[14];
        image = None
    })

let ToBoosterParts (rows : list<list<string>>) : seq<main.parts_internal_boosters> =
    rows
    |> Seq.map (fun tds -> 
    {
        id = 0;
        name = tds.[0];
        manufacturer = tds.[1];
        thrust = tds.[2] |> int;
        upward_thrust = tds.[3] |> int;
        upward_en_consumption = tds.[4] |> int;
        qb_thrust = tds.[5] |> int;
        qb_jet_duration = tds.[6] |> decimal;
        qb_en_consumption = tds.[7] |> int;
        qb_reload_time = tds.[8] |> decimal;
        qb_reload_ideal_weight = tds.[9] |> int;
        ab_thrust = tds.[10] |> int;
        ab_en_consumption = tds.[11] |> int;
        melee_attack_thrust = tds.[12] |> int;
        melee_atk_en_consumption = tds.[13] |> int;
        weight = tds.[14] |> int;
        en_load = tds.[15] |> int;
        description = tds.[16];
        image = None
    })

let ToFcsParts (rows : list<list<string>>) : seq<main.parts_internal_fcs> =
    rows
    |> Seq.map (fun tds ->
    {
        id = 0;
        name = tds.[0];
        manufacturer = tds.[1];
        close_range_assist = tds.[2] |> int;
        medium_range_assist = tds.[3] |> int;
        long_range_assist = tds.[4] |> int;
        average_assist = tds.[5] |> int;
        missile_lock_correction = tds.[6] |> int;
        multi_lock_correction = tds.[7] |> int;
        weight = tds.[8] |> int;
        en_load = tds.[9] |> int;
        description = tds.[10];
        image = None
    })

let ToGeneratorParts (rows : list<list<string>>) : seq<main.parts_internal_generator> =
    rows
    |> Seq.map (fun tds ->
    {
        id = 0;
        name = tds.[0];
        manufacturer = tds.[1];
        en_capacity = tds.[2] |> int;
        en_recharge = tds.[3] |> int;
        supply_recovery = tds.[4] |> int;
        post_recovery_en_supply = tds.[5] |> int;
        energy_firearm_spec = tds.[6] |> int;
        weight = tds.[7] |> int;
        en_load = tds.[8] |> int;
        description = tds.[9];
        image = None
    })

let ToWeaponParts (rows : list<list<string>>) : seq<main.parts_weapon> =
    []

let ToCoreExpansions (rows : list<list<string>>) : seq<main.parts_weapon> =
    []


/// Opens a connection and creates a QueryContext that will generate SQL Server dialect queries
let openContext() = 
    let compiler = SqlKata.Compilers.SqliteCompiler()
    let conn = new SQLiteConnection("Data Source=" + __SOURCE_DIRECTORY__ + "@/../data/ArmoredCore6.db")
    conn.Open()
    new QueryContext(conn, compiler)

type HeadParts2 =
    HtmlProvider<"http://armoredcore6.wikidot.com/head/p/2">


[<EntryPoint>]
let main args =

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
    
    printfn "Starting scrape"
    printfn "(FRAME) Head parts"
    printfn "===================="
    let headParts1 =
        buildUrl "/head"
        |> pageContent
        |> TableContentToRowList
        |> ToHeadParts
        |> Seq.toList

    let headParts2 =
        buildUrl "/head/p/2"
        |> pageContent
        |> TableContentToRowList
        |> ToHeadParts
        |> Seq.toList

    let headParts = 
        List.append headParts1 headParts2

    for part in headParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_frame_head do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"

    printfn "(FRAME) Core parts"
    printfn "===================="
    let coreParts =
        buildUrl "/core"
        |> pageContent
        |> TableContentToRowList
        |> ToCoreParts
        |> Seq.toList

    for part in coreParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_frame_core do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"
    
    printfn "(FRAME) Arm parts"
    printfn "===================="
    let armParts =
        buildUrl "/arms"
        |> pageContent
        |> TableContentToRowList
        |> ToArmParts
        |> Seq.toList

    for part in armParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_frame_arms do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"

    printfn "(FRAME) Leg parts"
    printfn "===================="
    let legParts1 =
        buildUrl "/legs"
        |> pageContent
        |> TableContentToRowList
        |> ToLegParts
        |> Seq.toList

    let legParts2 =
        buildUrl "/legs/p/2"
        |> pageContent
        |> TableContentToRowList
        |> ToLegParts
        |> Seq.toList
    let legParts = 
        List.append legParts1 legParts2

    for part in legParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_frame_legs do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"

    printfn "(INTERNAL) Booster parts"
    printfn "===================="
    let boosterParts =
        buildUrl "/boosters"
        |> pageContent
        |> TableContentToRowList
        |> ToBoosterParts
        |> Seq.toList

    for part in boosterParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_internal_boosters do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"

    printfn "(INTERNAL) FCS parts"
    printfn "===================="
    let fcsParts =
        buildUrl "/fcs"
        |> pageContent
        |> TableContentToRowList
        |> ToFcsParts
        |> Seq.toList

    for part in fcsParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_internal_fcs do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"

    printfn "(INTERNAL) Generator parts"
    printfn "===================="
    let generatorParts =
        buildUrl "/generator"
        |> pageContent
        |> TableContentToRowList
        |> ToGeneratorParts
        |> Seq.toList

    for part in generatorParts do
        let result = 
            insertTask (Create openContext) {
                for p in main.parts_internal_generator do
                entity part
                getId p.id
            }
        printfn $"- inserted {result.Result}, part {part.name}"

    0
