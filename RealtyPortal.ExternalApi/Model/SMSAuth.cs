using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyMED.Model
{
    public class TFAAuth
    {
        [BsonElement("Phone")]
        public string PhoneNumber { get; set; } = null!;
        [BsonElement("Otp")]
        public string OtpCode { get; set; }
    }
}
