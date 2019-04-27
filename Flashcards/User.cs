using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    public class User
    {
        public string Id { get; }
        public string Login { get; }
        
        public User(string login, string id = null)
        {
            Login = login;
            Id = id ?? Guid.NewGuid().ToString();
        }
    }
}
