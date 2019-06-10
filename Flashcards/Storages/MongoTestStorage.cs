using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Flashcards
{
    public class MongoTestStorage : ITestStorage
    {
        private readonly IMongoCollection<Test> tests;

        public MongoTestStorage()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flashcards");
            tests = database.GetCollection<Test>("tests");
            client.StartSession();
        }
        
        public async Task AddTest(Test test, CancellationToken token = default(CancellationToken))
        {
            await tests.InsertOneAsync(test, cancellationToken:token);
        }

        public async Task<Test> FindTest(string testId, CancellationToken token = default(CancellationToken))
        {
            return await tests.Find(test => test.Id == testId).FirstOrDefaultAsync(cancellationToken: token);
        }

        public async Task DeleteTest(string testId, CancellationToken token = default(CancellationToken))
        {
            await tests.FindOneAndDeleteAsync(a => a.Id == testId, cancellationToken: token);
        }
    }
}