module Tests

open Xunit
open System
open System.Collections.Generic
open System.Linq
open Swensen.Unquote
open SignatureUtil
open MongoDB.Driver
open MongoDB.Driver.Builders
open MongoDB.Bson
open Database

[<Fact>]
[<Trait("Category", "Integration")>]
let ``count all fx collection items`` () =
    let db = getDatabase "localhost" "isins"
    let allFx = 
        "foreign_exchange" |> getCollection db |> Seq.toList
    allFx |> Seq.length =! 1000
    
[<Literal>]
let defaultFxIndex = 
    """{ "v" : 1, "key" : { "_id" : 1 }, "name" : "_id_", "ns" : "isins.foreign_exchange" }"""

[<Fact>]
[<Trait("Category", "Integration")>]
let ``get indexes`` () =
    let db = getDatabase "localhost" "isins"
    let fx = db.GetCollection<BsonDocument> "foreign_exchange"
    use indexes = fx.Indexes.List() 
    let before = indexes.ToList()
    before.Count =! 1
    before.First().ToString() =! defaultFxIndex
    let isinKey = "properties.ISIN.ISIN"
    let updatedDateKey = 
        "priperties.ISIN.LastUpdateDateTime"
    let isinIndex = 
        sprintf "{ \"%s\": 1, \"%s\": 1 }"
            isinKey updatedDateKey
    fx.CreateIndex(isinIndex) |> ignore
    ()

[<Fact>]
let ``getTypeFileName:`` () =
    getTypeFileName (typeof<System.IO.File>) =! "File"
    getTypeFileName (typeof<IEnumerable<_>>) 
    =! "IEnumerable"

[<Fact>]
[<Trait("Category", "Integration")>]
let ``print to file type members`` () =
    writeTypeMembers (typeof<MongoDB.Driver.IMongoClient>)
    writeTypeMembers (typeof<MongoDB.Driver.IMongoDatabase>)
    writeTypeMembers (typeof<MongoDB.Driver.IMongoCollection<_>>)
