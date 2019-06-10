using System.Threading;
using System.Threading.Tasks;
using Flashcards;
using MongoDB.Driver;

namespace FlashcardsApi.Mongo
{
    public class MongoTestStorage : ITestStorage
    {
        private readonly IMongoContext context;

        public MongoTestStorage(IMongoContext context)
        {
            this.context = context;
        }
        
        public async Task AddTest(Test test, CancellationToken token = default(CancellationToken))
        {
            await context.Tests.InsertOneAsync(test, cancellationToken:token);
        }

        public async Task<Test> FindTest(string testId, CancellationToken token = default(CancellationToken))
        {
            return await context.Tests.Find(test => test.Id == testId).FirstOrDefaultAsync(token);
        }

        public async Task DeleteTest(string testId, CancellationToken token = default(CancellationToken))
        {
            await context.Tests.FindOneAndDeleteAsync(a => a.Id == testId, cancellationToken: token);
        }
    }
}