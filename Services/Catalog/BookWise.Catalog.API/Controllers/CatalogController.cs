using BookWise.Catalog.API.Entities;
using BookWise.Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookWise.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IBookRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var Books = await _repository.GetBooks();
            return Ok(Books);
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var Book = await _repository.GetBook(id);
            if (Book == null)
            {
                _logger.LogError($"Book with id: {id}, not found.");
                return NotFound();
            }
            return Ok(Book);
        }

        [Route("[action]/{title}", Name = "GetBookByTitle")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByTitle(string title)
        {
            var Books = await _repository.GetBookByTitle(title);
            return Ok(Books);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book Book)
        {
            await _repository.CreateBook(Book);

            return CreatedAtRoute("GetBook", new { id = Book.Id }, Book);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBook([FromBody] Book Book)
        {
            return Ok(await _repository.UpdateBook(Book));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteBook")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookById(string id)
        {
            return Ok(await _repository.DeleteBook(id));
        }
    }
}
