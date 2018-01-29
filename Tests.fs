module Tests

open Xunit
open System
open System.Collections.Generic
open Swensen.Unquote
open SignatureUtil
open MongoDB.Driver
open MongoDB.Bson

let getDatabase (connectionstring: string) name =
    let client = 
        (new MongoClient(connectionstring))
        :> IMongoClient
    client.GetDatabase name
    
[<Fact>]
let ``get database: does not throw`` () =
    let db = 
        getDatabase "mongodb://localhost" "isins"
    let fxName = "foreign_exchange"
    let c = db.GetCollection<BsonDocument> fxName
    c.GetType() |> writeTypeMembers
    c.FindSync() |> ignore
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
