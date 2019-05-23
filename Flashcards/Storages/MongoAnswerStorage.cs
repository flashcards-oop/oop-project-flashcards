using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Flashcards
{
    public class MongoAnswersStorage : IAnswersStorage
    {
        private readonly IMongoCollection<MongoAnswers> answers;

        public MongoAnswersStorage()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flashcards");
            answers = database.GetCollection<MongoAnswers>("answers");
            client.StartSession();
        }
        
        public async Task<string> AddAnswers(IEnumerable<Exercise> exercises)
        {
            var answersList = new MongoAnswers(exercises);
            await answers.InsertOneAsync(answersList);
            return answersList.Id;
        }

        public async Task<IEnumerable<Answer>> FindAnswers(string testId)
        {
            return (await answers.Find(a => a.Id == testId).FirstOrDefaultAsync()).Answers;
        }

        public async Task DeleteTest(string testId)
        {
            await answers.FindOneAndDeleteAsync(a => a.Id == testId);
        }
    }
}