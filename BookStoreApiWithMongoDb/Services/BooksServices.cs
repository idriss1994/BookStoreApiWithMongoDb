using BookStoreApiWithMongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApiWithMongoDb.Services
{
    public class BooksServices
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BooksServices(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseOptions)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseOptions.Value.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseOptions.Value.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<Book>(
                bookStoreDatabaseOptions.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(string id) =>
            await _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) => 
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Book updatedBook) =>
                await _booksCollection.ReplaceOneAsync(b => b.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(b => b.Id == id);
    }
}
