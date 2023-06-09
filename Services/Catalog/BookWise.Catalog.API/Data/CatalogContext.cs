using BookWise.Catalog.API.Entities;
using MongoDB.Driver;

namespace BookWise.Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Books = database.GetCollection<Book>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //CatalogContextSeed.SeedData(Books);
        }

        public IMongoCollection<Book> Books { get; }
    }
}
