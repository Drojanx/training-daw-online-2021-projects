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
            IEnumerable<Order> orders = _context.Orders
                .Include(order => order.Items)
                .ToList();  
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

    }
}