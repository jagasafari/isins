module Database

open System.Linq
open MongoDB.Driver
open MongoDB.Bson

let getDatabase address name =
    let connStr = sprintf "mongodb://%s" address
    let client = 
        (new MongoClient(connStr)) :> IMongoClient
    client.GetDatabase name
    
let getCollection (db: IMongoDatabase) name =
    (db.GetCollection<BsonDocument> name).AsQueryable()
