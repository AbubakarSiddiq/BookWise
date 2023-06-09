using BookWise.Catalog.API.Entities;

namespace BookWise.Catalog.API.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(string id);
        Task<IEnumerable<Book>> GetBookByTitle(string title);
        Task<IEnumerable<Book>> GetBookByAuthorName(string authorName);
        Task CreateBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(string id);
    }
}
