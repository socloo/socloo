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
    public class CalendarsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public CalendarsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<Calendar>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Calendar>("Calendars").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Calendar> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Calendar>("Calendars");
                var filter = Builders<Calendar>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Calendar calendar)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(calendar.UserId)},
                 { "OccurrencesId", bsonarray},

            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Calendars");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Calendar calendar)
        {

            try
            {
                var document = new BsonDocument
            {
                   { "UserId", ObjectId.Parse(calendar.UserId)},
                 { "OccurrencesId", new BsonArray(calendar.OccurrencesId)},
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Calendars");
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
                var collection = mongoDB.database.GetCollection<Calendar>("Calendars");
                var filter = Builders<Calendar>.Filter.Eq("_id", ObjectId.Parse(id));
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