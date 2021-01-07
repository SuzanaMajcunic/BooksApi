using BooksApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(x => true).ToList();

        public Book Get(string id) =>
            _books.Find(x => x.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(x => x.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(x => x.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(x => x.Id == id);
    }
}
