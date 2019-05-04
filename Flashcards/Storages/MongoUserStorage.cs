using MongoDB.Driver;

namespace Flashcards
{
    public class MongoUserStorage : IUserStorage
    {
        private readonly IMongoCollection<User> users;

        public MongoUserStorage()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flashcards");
            users = database.GetCollection<User>("users");
        }
        public void AddUser(User user)
        {
            users.InsertOne(user);
        }

        public User FindUserById(string id)
        {
            return users.Find(u => u.Id == id).FirstOrDefault();
        }

        public User FindUserByLogin(string login)
        {
            return users.Find(u => u.Login == login).FirstOrDefault();
        }
    }
}