﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public DocumentsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<Document>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Document>("Documents").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Document> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Document>("Documents");
                var filter = Builders<Document>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Document doc)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                {"FileId",ObjectId.Parse(doc.FileId) },
                 { "UsersId", bsonarray},
                 { "TeacherId", ObjectId.Parse(doc.TeacherId)},
                  { "DateTime",Convert.ToDateTime(doc.DateTime)},

            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Documents");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Document doc)
        {

            try
            {
                var document = new BsonDocument
            {
                 {"FileId",ObjectId.Parse(doc.FileId)  },
                 { "UsersId", new BsonArray(doc.UsersId)},
                 { "TeacherId", ObjectId.Parse(doc.TeacherId)},
                { "DateTime",Convert.ToDateTime(doc.DateTime)},
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Documents");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                await collection.FindOneAndReplaceAsync(filter, document);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Document>("Documents");
                var filter = Builders<Document>.Filter.Eq("_id", ObjectId.Parse(id));
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}