using BookStoreApiWithMongoDb.Models;
using MongoDB.Driver;

namespace BookStoreApiWithMongoDb.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);
        }
        public async Task<Student> CreateAsync(Student newStudent)
        {
            await _students.InsertOneAsync(newStudent);
            return newStudent;
        }

        public async Task<List<Student>> GetAsync()
        {
            return await _students.Find(student => true).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string studentId)
        {
            return await _students.Find(student => student.Id == studentId).FirstOrDefaultAsync(); 
        }

        public async Task RemoveAsync(string studentId)
        {
            await _students.DeleteOneAsync(student => student.Id == studentId);
        }

        public async Task UpdateAsync(string studentId, Student updatedStudent)
        {
            await _students.ReplaceOneAsync(student => student.Id == studentId, updatedStudent);
        }
    }
}
