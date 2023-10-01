open FSharp.Data

[<Literal>]
let connectionString =
    "Data Source=" +
    __SOURCE_DIRECTORY__ + @"/../data/ArmoredCore6.db;" +
    "Version=3;"

[<Literal>]
let baseUrl = "http://armoredcore6.wikidot.com"
let mainPage = $"{baseUrl}/wiki:armored-core-vi:fires-of-rubicon-parts"


[<EntryPoint>]
let main args =
    let content: HtmlNode = 
        HtmlDocument.Load(mainPage).Descendants "div"
        |> Seq.filter (fun (node: HtmlNode)  -> node.HasId("page-content"))
        |> Seq.item 0

    let links =
        content.Descendants "a"
        |> Seq.choose (fun x ->
            if x.AttributeValue("href") = "/os-tuning"
            then None 
            else Some $"""{baseUrl}{x.AttributeValue("href")}""")
    printfn "links: %A" links
    printfn "links length: %A" (links |> Seq.length)
    0
