using BookStoreApiWithMongoDb.Models;

namespace BookStoreApiWithMongoDb.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAsync();
        Task<Student> GetByIdAsync(string studentId);
        Task<Student> CreateAsync(Student newStudent);
        Task UpdateAsync(string studentId, Student updatedStudent);
        Task RemoveAsync(string studentId);
    }
}
