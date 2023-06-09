using BookWise.Catalog.API.Data;
using BookWise.Catalog.API.Entities;
using MongoDB.Driver;

namespace BookWise.Catalog.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ICatalogContext _context;

        public BookRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.Find(p => true).ToListAsync();
        }
        public async Task<Book> GetBook(string id)
        {
            return await _context.Books.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByTitle(string title)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Title, title);

            return await _context.Books.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByAuthorName(string authorName)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Author, authorName);

            return await _context.Books.Find(filter).ToListAsync();
        }

        public async Task CreateBook(Book Book)
        {
            await _context.Books.InsertOneAsync(Book);
        }

        public async Task<bool> UpdateBook(Book Book)
        {
            var updateResult = await _context.Books.ReplaceOneAsync(filter: g => g.Id == Book.Id, replacement: Book);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteBook(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Books.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}
