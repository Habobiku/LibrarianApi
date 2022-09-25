using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarianApi.Client;
using LibrarianApi.Models;
using LibrarianApi.Responce;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static LibrarianApi.Extension.Extension;

namespace LibrarianApi.Controllers
{
    [ApiController]
    [Route(@"api")]
    public class DBController : Controller
    {
        private readonly IDynamoDB _dynamoDbClient;
        private ILogger<DBController> _logger;

        public DBController(ILogger<DBController> logger, IDynamoDB dynamoDBClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDBClient;
        }


        [HttpGet("GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetBook([FromQuery] GetResponce get)
        {
            var result = await _dynamoDbClient.GetBook(get);
            if (result == null)
                return NotFound("Not found book");

            return Ok(result);

        }

        [HttpGet("GetBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetBooks()
        {
            var result = await _dynamoDbClient.GetBooks();
            if (result == null)
                return NotFound("Not found books");

            return Ok(result);
        }

        [HttpPost("PostBook")]
        public async Task<IActionResult> PostAn([FromQuery] Book post)
        {
            var data = new PostResponce
            {
                Id = GenerateRandom(),
                Title = post.Title,
                Status = post.Status,
                Isbn = post.Isbn,
                Author = post.Author,
                Publisher = post.Publisher,
                Genre = post.Genre,
                Date = post.Date,
            };
            var result = await _dynamoDbClient.PostBook(data);

            if (result == false)
                return BadRequest("Cannot insert to database");

            return Ok("Successful have been added.Your id=" + data.Id);
         }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> ReserveBook([FromQuery] string action, string id)
        {

            var result = await _dynamoDbClient.UpdateStatus(action,id);

            if (result == false)
                return BadRequest("Check the console");

            return Ok("Successful have been updated status to " + action);


        }

        [HttpGet("GetStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetStatus(string title)
        {
            var result = await _dynamoDbClient.GetBook(new GetResponce { Key="Title",Id = title });
            if (result == null)
                return NotFound("Not found books");

            return Ok(new StatusBook { Status=result.Status,Title=result.Title});
        }
    }
}

