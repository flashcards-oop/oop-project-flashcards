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
        
        public async Task AddTest(Test test)
        {
            await tests.InsertOneAsync(test);
        }

        public async Task<Test> FindTest(string testId)
        {
            return await tests.Find(test => test.Id == testId).FirstOrDefaultAsync();
        }

        public async Task DeleteTest(string testId)
        {
            await tests.FindOneAndDeleteAsync(a => a.Id == testId);
        }
    }
}