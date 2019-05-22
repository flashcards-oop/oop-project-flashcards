using System.Collections.Generic;
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
        
        public string AddAnswers(IEnumerable<Exercise> exercises)
        {
            var answersList = new MongoAnswers(exercises);
            answers.InsertOne(answersList);
            return answersList.Id;
        }

        public IEnumerable<Answer> FindAnswers(string testId)
        {
            return answers.Find(a => a.Id == testId).FirstOrDefault().Answers;
        }

        public void DeleteTest(string testId)
        {
            answers.FindOneAndDelete(a => a.Id == testId);
        }
    }
}