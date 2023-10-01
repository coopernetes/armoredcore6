#r "nuget: FSharp.Data,6.3.0"

open System
open FSharp.Data

[<Literal>]
let partsUrl: string = "http://armoredcore6.wikidot.com/wiki:armored-core-vi:fires-of-rubicon-parts"

let pageContent = 
    HtmlDocument.Load(partsUrl).Descendants "div"
    |> Seq.filter (fun node -> node.HasId("page-content"))
if not (pageContent |> Seq.length = 1) then
    printfn "No page content found"
else
    printfn $"""pageContent: {String.Join(" ", pageContent)}"""