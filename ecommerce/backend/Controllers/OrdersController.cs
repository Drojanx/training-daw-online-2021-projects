using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {   
            IEnumerable<Order> orders = _context.Orders
                .Include(order => order.Items)
                .ToList();
            return Ok(_context.Orders);
        }
        
        [HttpGet]
        [Route("{orderId}")]
        public ActionResult<IEnumerable<Order>> Get(int orderId)
        {
            Order order = _context.Orders.Find(orderId);
            return order == null ? NotFound("Order not found") : Ok(order);
        }

        [HttpPost]
        public ActionResult Post(Order order)
        {
            Order existingOrder = _context.Orders.Find(order.Id);
            if (existingOrder!=null) {
                return Conflict("This Id is already being used.");
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + order.Id;
            return Created(resourceUrl, order);
            
        }

        [HttpPut]
        [Route("{orderId}")]
        public ActionResult Put(Product product, int productId) {
            Product existingProduct = _context.Products.Find(productId);
            if (existingProduct==null) {
                return Conflict("There is no product with this Id");
            }
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Short = product.Short;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.MainImage = product.MainImage;
            existingProduct.Disccount = product.Disccount;
            existingProduct.Images = product.Images;
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + productId;
            return Ok();
        }

        /*
        [HttpDelete]
        [Route("{productId}")]
        public ActionResult Delete(int productId) {
            var existingProduct = _context.Products.Find(productId);
            if (existingProduct == null) {
                return NotFound("Product not found");
            } else {
                _context.Products.Remove(existingProduct);
                _context.SaveChangesAsync();
                return NoContent();
            }
        } */
    }
}