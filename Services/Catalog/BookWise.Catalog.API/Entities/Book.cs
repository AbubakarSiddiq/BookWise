using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.CompilerServices;

namespace BookWise.Catalog.API.Entities
{
    public class Book
    {
        public Book() 
        {
            this.Title = string.Empty;
            this.Author = string.Empty;
            this.Description = string.Empty;
            this.Genre= string.Empty;
        }

        public Book(string title, string author, string genre, string description) {
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}
