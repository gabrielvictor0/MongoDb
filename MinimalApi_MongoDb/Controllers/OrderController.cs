using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_MongoDb.Domains;
using MinimalApi_MongoDb.Services;
using MinimalApi_MongoDb.ViewModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MinimalApi_MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Product> _product;
        private readonly IMongoCollection<Client> _client;

        public OrderController(MongoDbService mongoDbService)
        {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
        }

        [HttpPost("ViewModel")]
        public IActionResult Create(OrderViewModel orderViewModel)
        {
            try
            {
                Order order = new Order();

                order.Id = orderViewModel.Id;
                order.Date = orderViewModel.Date;
                order.ProductId = orderViewModel.ProductId;
                order.ClientId = orderViewModel.ClientId;

                _order.InsertOne(order);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("ById")]
        public IActionResult Get(string id)
        {
            try
            {
                var order = _order.Find(x => x.Id == id).FirstOrDefault();
                List<Product> listProducts = new List<Product>();

                var client = _client.Find(x => x.Id == order.ClientId).FirstOrDefault();

                foreach (var productId in order.ProductId!)
                {
                    var product = _product.Find(x => x.Id == productId).FirstOrDefault();

                    listProducts.Add(product);
                }

                order.Products = listProducts;
                order.Client = client;

                return Ok(order);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                

                List<Order> listOrder = _order.Find(FilterDefinition<Order>.Empty).ToList();

                foreach (var order in listOrder)
                {
                    List<Product> listProduct = new List<Product>();

                    foreach (var productId in order.ProductId!)
                    {
                        var product = _product.Find(x => x.Id == productId).FirstOrDefault();

                        listProduct.Add(product);

                    }

                    order.Products = listProduct;

                    var client = _client.Find(x => x.Id == order.ClientId).FirstOrDefault();

                    order.Client = client;
                }

                return Ok(listOrder);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id) 
        {
            try
            {
                _order.FindOneAndDelete(x => x.Id == id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(OrderViewModel orderViewModel, string id) 
        {
            try
            {
                Order order = new Order();

                order.Id = id;
                order.Date = orderViewModel.Date;
                order.ProductId = orderViewModel.ProductId;
                order.ClientId = orderViewModel.ClientId;

                _order.FindOneAndReplace(x => x.Id == id, order);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
