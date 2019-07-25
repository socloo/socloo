﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class SuperAdminViewModel
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CoursesId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> GroupsId { get; set; }
        public bool Deleted { get; set; } = false;
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
