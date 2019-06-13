using System;
using System.Threading;
using System.Threading.Tasks;
using Flashcards;
using Flashcards.Storages;
using MongoDB.Driver;

namespace FlashcardsApi.Mongo
{
    public class MongoUserStorage : IUserStorage
    {
        private readonly IMongoContext context;

        public MongoUserStorage(IMongoContext context)
        {
            this.context = context;
        }
        public async Task AddUser(User user, CancellationToken token)
        {
            await context.Users.InsertOneAsync(user, cancellationToken: token);
        }

        public async Task<User> FindUserById(Guid id, CancellationToken token)
        {
            return await context.Users.Find(u => u.Id == id).FirstOrDefaultAsync(token);
        }

        public async Task<User> FindUserByLogin(string login, CancellationToken token)
        {
            return await context.Users.Find(u => u.Login == login).FirstOrDefaultAsync(token);
        }
    }
}