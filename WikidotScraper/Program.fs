module WikidotScraper.Program

open System
open FSharp.Data
open SqlHydra.Query
open System.Data.SQLite
open WikidotScraper.DatabaseTypes
open SqlHydra.Query.SqliteExtensions

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
            part_id = 0;
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
        part_id = 0;
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
            part_id = 0;
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
        part_id = 0;
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
        part_id = 0;
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
        part_id = 0;
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
        part_id = 0;
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
        part_id = 0;
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
    // Incomplete data 
    //  http://armoredcore6.wikidot.com/weapon:hml-g2
    //  http://armoredcore6.wikidot.com/weapon:44-091-jvln-beta
    let exclude = [
        "45-091 JVLN BETA"
        "HML-G2/P19 MLT-04"
    ]

    rows
    |> Seq.filter (fun tds -> exclude |> Seq.contains tds.[1] |> not)
    |> Seq.map (fun tds -> 
        let tryInt str : option<int> =
            if (str = "" || str = "-") then None else Some(str |> int)
        let tryDecimal str : option<decimal> =
            if (str = "" || str = "-") then None else Some(str |> decimal)

        // ascii x
        // random x-like: ×
        let splitters = [
            "x"
            "×"
            "s" // thanks to http://armoredcore6.wikidot.com/weapon:bml-g2-p08duo-03
        ]
        let atkPowCell = tds.[4]
        let atkPowParts: string array = 
            if atkPowCell.Contains("x") then atkPowCell.Split("x")
            elif atkPowCell.Contains("×") then atkPowCell.Split("×")
            elif atkPowCell.Contains("s") then atkPowCell.Split("s")
            else [|atkPowCell|]
        let attackPower = if atkPowParts.[0] = "" then None else Some(atkPowParts.[0] |> int)
        let attackPowerMultiplier = 
            match atkPowParts.Length with
                | 1 -> None
                | 2 -> Some(atkPowParts.[1] |> int)
                | _ -> failwith "Invalid attack power"
        
        let impactCell = tds.[5]
        let impactParts = 
            if impactCell.Contains("x") then impactCell.Split("x")
            elif impactCell.Contains("×") then impactCell.Split("×")
            elif impactCell.Contains("s") then impactCell.Split("s")
            else [|impactCell|]
        let impact = tryInt(impactParts.[0])
        let impactMultiplier = 
            match impactParts.Length with
                | 1 -> None
                | 2 -> Some(impactParts.[1] |> int)
                | _ -> failwith "Invalid impact"
        
        let accumImpactCell = tds.[6]
        let accumImpactParts = 
            if accumImpactCell.Contains("x") then accumImpactCell.Split("x")
            elif accumImpactCell.Contains("×") then accumImpactCell.Split("×")
            elif accumImpactCell.Contains("s") then accumImpactCell.Split("s")
            else [|accumImpactCell|]
        let accumImpact = if accumImpactParts.[0] = "" then None else Some(accumImpactParts.[0] |> int)
        let accumImpactMultiplier = 
            match accumImpactParts.Length with
                | 1 -> None
                | 2 -> Some(accumImpactParts.[1] |> int)
                | _ -> failwith "Invalid accumulative impact"
        {
            part_id = 0;
            slot = tds.[0];
            name = tds.[1];
            part_type = tds.[2];
            manufacturer = tds.[3];
            attack_power = attackPower;
            attack_power_multiplier = attackPowerMultiplier;
            impact = impact;
            impact_multiplier = impactMultiplier;
            accumulative_impact = accumImpact;
            accumulative_impact_multiplier = accumImpactMultiplier;
            blast_radius = tryInt(tds.[7]);
            atk_heat_build_up = tryInt(tds.[8]);
            direct_hit_adjustment = tryInt(tds.[9]);
            recoil = tryInt(tds.[10]);
            effective_range = tryInt(tds.[11]);
            range_limt = tryInt(tds.[12]);
            rapid_fire = tryDecimal(tds.[13])
            total_rounds = tryInt(tds.[14]);
            reload = tryDecimal(tds.[15])
            cooling = tryInt(tds.[16]);
            ammunition_cost = tryInt(tds.[17]);
            consecutive_hits = tryInt(tds.[18]);
            weight = tds.[19] |> int;
            en_load = tds.[20] |> int;
            description = tds.[21];
            image = None
        }
    )

let ToCoreExpansions (rows : list<list<string>>) : seq<main.parts_weapon> =
    []


/// Opens a connection and creates a QueryContext that will generate SQL Server dialect queries
let openContext() = 
    let compiler = SqlKata.Compilers.SqliteCompiler()
    let conn = new SQLiteConnection("Data Source=" + __SOURCE_DIRECTORY__ + "@/../data/ArmoredCore6.db")
    conn.Open()
    new QueryContext(conn, compiler)

type Parts = 
    | Head of main.parts_frame_head
    | Core of main.parts_frame_core
    | Arms of main.parts_frame_arms
    | Legs of main.parts_frame_legs
    | Booster of main.parts_internal_boosters
    | Fcs of main.parts_internal_fcs
    | Generator of main.parts_internal_generator
    | Weapon of main.parts_weapon

let getPartName (parts: Parts) : string =
    match parts with
    | Head p -> p.name
    | Core p -> p.name
    | Arms p -> p.name
    | Legs p -> p.name
    | Booster p -> p.name
    | Fcs p -> p.name
    | Generator p -> p.name
    | Weapon p -> p.name


let PartExists (parts: Parts, getNameFn: (Parts) -> string) : bool =
    let name = getNameFn parts
    match parts with
    | Head p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_frame_head do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Core p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_frame_core do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Arms p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_frame_arms do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Legs p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_frame_legs do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Booster p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p: main.parts_internal_boosters in main.parts_internal_boosters do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Fcs p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_internal_fcs do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Generator p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_internal_generator do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0
    | Weapon p ->
        let task = 
            selectTask HydraReader.Read (Create openContext) {
                for p in main.parts_weapon do
                where (p.name = name)
                select p.part_id
            }
        task.Result |> Seq.length > 0



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
    printfn "Weapon parts"
    printfn "===================="
    let weaponParts1 =
        buildUrl "/weapon"
        |> pageContent
        |> TableContentToRowList
        |> ToWeaponParts
        |> Seq.toList
    let weaponParts2 =
        buildUrl "/weapon/p/2"
        |> pageContent
        |> TableContentToRowList
        |> ToWeaponParts
        |> Seq.toList
    let weaponParts3 =
        buildUrl "/weapon/p/3"
        |> pageContent
        |> TableContentToRowList
        |> ToWeaponParts
        |> Seq.toList
    let weaponParts4 =
        buildUrl "/weapon/p/4"
        |> pageContent
        |> TableContentToRowList
        |> ToWeaponParts
        |> Seq.toList

    let weaponParts = 
        weaponParts1
        |> List.append weaponParts2 
        |> List.append weaponParts3 
        |> List.append weaponParts4

    for part in weaponParts do
        if PartExists (Weapon part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            printfn $"- inserting {part.name}"
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_weapon do
                    entity part
                    getId p.part_id
                }
            printfn $"    id: {result.Result}"

    printfn ""
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
        if PartExists (Head part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_frame_head do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"

    printfn ""
    printfn "(FRAME) Core parts"
    printfn "===================="
    let coreParts =
        buildUrl "/core"
        |> pageContent
        |> TableContentToRowList
        |> ToCoreParts
        |> Seq.toList

    for part in coreParts do
        if PartExists (Core part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_frame_core do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"
    
    printfn ""
    printfn "(FRAME) Arm parts"
    printfn "===================="
    let armParts =
        buildUrl "/arms"
        |> pageContent
        |> TableContentToRowList
        |> ToArmParts
        |> Seq.toList

    for part in armParts do
        if PartExists (Arms part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_frame_arms do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"

    printfn ""
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
        if PartExists (Legs part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_frame_legs do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"

    printfn ""
    printfn "(INTERNAL) Booster parts"
    printfn "===================="
    let boosterParts =
        buildUrl "/boosters"
        |> pageContent
        |> TableContentToRowList
        |> ToBoosterParts
        |> Seq.toList

    for part in boosterParts do
        if PartExists (Booster part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_internal_boosters do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"

    printfn ""
    printfn "(INTERNAL) FCS parts"
    printfn "===================="
    let fcsParts =
        buildUrl "/fcs"
        |> pageContent
        |> TableContentToRowList
        |> ToFcsParts
        |> Seq.toList

    for part in fcsParts do
        if PartExists (Fcs part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_internal_fcs do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"

    printfn ""
    printfn "(INTERNAL) Generator parts"
    printfn "===================="
    let generatorParts =
        buildUrl "/generator"
        |> pageContent
        |> TableContentToRowList
        |> ToGeneratorParts
        |> Seq.toList

    for part in generatorParts do
        if PartExists (Generator part, getPartName) then
            printfn $"    skipping {part.name}"
        else
            let result = 
                insertTask (Create openContext) {
                    for p in main.parts_internal_generator do
                    entity part
                    getId p.part_id
                }
            printfn $"- inserted {result.Result}, part {part.name}"

    0
