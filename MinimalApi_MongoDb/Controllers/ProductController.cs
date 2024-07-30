using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_MongoDb.Domains;
using MinimalApi_MongoDb.Services;
using MongoDB.Driver;

namespace MinimalApi_MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMongoCollection<Product> _product;

        public ProductController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            try
            {
                _product.InsertOne(product);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();

                return Ok(products);
            }
            catch (Exception e )
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var productSearched = _product.Find(x => x.Id == id).FirstOrDefault();

                return productSearched is not null ? Ok(productSearched) : NotFound("Produto não encontrado");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [ HttpDelete]
        public IActionResult Delete(string id) 
        {
            try
            {
                _product.FindOneAndDelete<Product>(x => x.Id == id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(string id, Product newProduct) 
        {
            try
            {
                // productSearched = _product.Find<Product>();
                newProduct.Id = id;
                _product.ReplaceOne(x => x.Id == id, newProduct);
                return Ok();
            }           
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }        
        }

       
    }
}
