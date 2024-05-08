using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationsContext.books;
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id)
        {
            var book = ApplicationsContext.books.Where(b => b.Id.Equals(id)).SingleOrDefault();

            if (book is null)
            {
                return NotFound(); //404
            }
            return Ok(book);
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); //400 
                }
                ApplicationsContext.books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            //check book
            var entity = ApplicationsContext.books.Find(b => b.Id.Equals(id));
            if (entity is null)
            {
                return NotFound(); //404
            }
            //check id
            if (id != book.Id)
            {
                return BadRequest(); //400
            }
            ApplicationsContext.books.Remove(entity);
            book.Id = entity.Id;
            ApplicationsContext.books.Add(book);
            return Ok(book);
        }
        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationsContext.books.Clear();
            return NoContent();// 204
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")] int id) {
            var entity = ApplicationsContext
                .books
                .Find(b=>b.Id.Equals(id));
            if (entity is null)
            {  
                return NotFound(
                    new
                    {
                        statusCode = 404,
                        message=$"Book with id:{id} could not found."
                    }); //404
            }
            ApplicationsContext.books.Remove(entity);
            return NoContent(); 
        
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name ="id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity = ApplicationsContext.books.Find(b => b.Id.Equals(id));
            if (entity is null)
            {
                return NotFound(); //404
            }
            bookPatch.ApplyTo(entity);
            return NoContent();//204

        }

    }
}
