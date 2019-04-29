using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class DumbUserStorage : IUserStorage
    {
        private List<User> users;

        public DumbUserStorage()
        {
            users = new List<User>();
            users.Add(new User("Admin"));
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User FindUserById(string id)
        {
            return users.Where(user => user.Id == id).FirstOrDefault();
        }

        public User FindUserByLogin(string login)
        {
            return users.Where(user => user.Login == login).FirstOrDefault();
        }
    }
}
