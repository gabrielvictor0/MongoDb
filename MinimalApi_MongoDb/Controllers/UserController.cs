using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_MongoDb.Domains;
using MinimalApi_MongoDb.Services;
using MongoDB.Driver;

namespace MinimalApi_MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _user;

        public UserController(MongoDbService mongoDbService)
        {
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpPost]
        public IActionResult Post(User user) 
        { 
            try
            {
                _user.InsertOneAsync(user);
                return Ok();
            }
            catch (Exception e)
            {
            
                return BadRequest(e.Message);
            }
        }

        [HttpGet] 
        public async Task<ActionResult<User>> Get() 
        {
            try
            {
                var users = await  _user.Find(FilterDefinition<User>.Empty).ToListAsync();
                if(users != null) 
                {
                    return Ok(users);
                }
                else { return NotFound("Nenhum user encontrado"); }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }        
        }
    }
}
