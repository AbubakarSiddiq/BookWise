using BookWise.Catalog.API.Entities;
using MongoDB.Driver;

namespace BookWise.Catalog.API.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Book> Books { get; }
    }
}
