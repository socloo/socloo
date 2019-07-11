﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        public MessagesController(IConfiguration config, ILogger<MessagesController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Message>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Message> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Message>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Message message)
        {

            await UnitOfWork.Repository<Message>().InsertAsync(message);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Message message)
        {

            try
            {
                var document = new BsonDocument
            {
                  { "UserId", ObjectId.Parse(message.UserId)},
                 { "DataTime", Convert.ToDateTime(message.DataTime)},
                 { "MessageText",message.MessageText},
                { "ChatId",  ObjectId.Parse(message.ChatId)},
            };

                UnitOfWork.Repository<Group>().Update(document, ObjectId.Parse(id), "messages");
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
                Message message = this.GetById(id).Result;
                var document = new BsonDocument
            {
                  { "UserId", ObjectId.Parse(message.UserId)},
                 { "DataTime", Convert.ToDateTime(message.DataTime)},
                 { "MessageText",message.MessageText},
                { "ChatId",  ObjectId.Parse(message.ChatId)},
                 {"Deleted",true }
            };
                UnitOfWork.Repository<Message>().DeleteAsync(document, ObjectId.Parse(id), "messages", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}