using Core.Entities;
using MongoDB.Driver;

namespace core.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        public MongoDBContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Batch> Batches => _database.GetCollection<Batch>("Batches");
        public IMongoCollection<Course> Courses => _database.GetCollection<Course>("Courses");
        public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students");
        public IMongoCollection<StudentCourse> StudentCourses => _database.GetCollection<StudentCourse>("StudentCourses");
    }
}
