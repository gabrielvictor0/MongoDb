using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinimalApi_MongoDb.Domains;
using MinimalApi_MongoDb.Services;
using MongoDB.Driver;

namespace MinimalApi_MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<User> _user;

        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            
        }

        [HttpPost]
        public IActionResult Post(Client client)
        {
            try
            {
                _client.InsertOne(client);
                return Ok();
            }
            catch(Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Client>> Get() 
        {
            try
            {
                var client = _client.Find(FilterDefinition<Client>.Empty).ToList();

                return Ok(client);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }        
        }
    }
}
