using System.Threading.Tasks;
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
        public async Task AddUser(User user)
        {
            await users.InsertOneAsync(user);
        }

        public async Task<User> FindUserById(string id)
        {
            return await users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> FindUserByLogin(string login)
        {
            return await users.Find(u => u.Login == login).FirstOrDefaultAsync();
        }
    }
}