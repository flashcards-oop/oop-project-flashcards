using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class User
    {
        [BsonElement]
        public string Id { get; set; }
        [BsonElement]
        public string Login { get; set; }
        
        [BsonConstructor]
        public User(string login, string id = null)
        {
            Login = login;
            Id = id ?? Guid.NewGuid().ToString();
        }
    }
}
