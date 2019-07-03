﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace SoclooAPI.Models
{
    public class AssignmentViewModel
    {

        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public string ExpirationDate { get; set; }
        public string Info { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public string FileId { get; set; }
    }
}