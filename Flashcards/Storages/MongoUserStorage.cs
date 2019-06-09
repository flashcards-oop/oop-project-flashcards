using System.Threading;
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
        public async Task AddUser(User user, CancellationToken token)
        {
            await users.InsertOneAsync(user, cancellationToken: token);
        }

        public async Task<User> FindUserById(string id, CancellationToken token)
        {
            return await users.Find(u => u.Id == id).FirstOrDefaultAsync(cancellationToken:token);
        }

        public async Task<User> FindUserByLogin(string login, CancellationToken token)
        {
            return await users.Find(u => u.Login == login).FirstOrDefaultAsync(cancellationToken: token);
        }
    }
}