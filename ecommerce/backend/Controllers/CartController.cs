using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CartProduct>> Get()
        {
            return Ok(_context.Cart);
        }

        [HttpGet]
        [Route("{productId}")]
        public ActionResult<IEnumerable<CartProduct>> Get(int productId)
        {
            CartProduct cartProduct = _context.Cart.Find(productId);
            return cartProduct == null ? NotFound("Product not found") : Ok(cartProduct);
        }
        
        [HttpPost]
        public ActionResult Post(CartProduct cartProduct)
        {
            Product productExists = _context.Products.Find(cartProduct.ProductId);
            if (productExists==null) {
                return NotFound("Product doesn't exist");
            }
            var existingProduct = _context.Cart.SingleOrDefault(cp => cp.ProductId == cartProduct.ProductId);
            if (existingProduct!=null) {
                return Conflict("Product already in cart. To modify data, use PUT");
            } else {
            _context.Cart.Add(cartProduct);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + cartProduct.Id;
            return Created(resourceUrl, cartProduct);
            }
        }
        
        [HttpPut]
        [Route("{cartId}")]
        public ActionResult Put(CartProduct cartProduct, int cartId) {
            Product productExists = _context.Products.Find(cartProduct.ProductId);
            if (productExists==null) {
                return NotFound("Product doesn't exist");
            }
            CartProduct existingCartProductLIne = _context.Cart.Find(cartId);
            if (existingCartProductLIne==null) {
                return Conflict("There is no product line with this Id in the cart");
            }
            var existingProduct = _context.Cart.SingleOrDefault(cp => cp.ProductId == cartProduct.ProductId);
            if (existingProduct!=null && existingProduct.Id != cartId) {
                return Conflict("Product already in cart in a different line. Try MODIFYING that one and DELETING this one");
            }            
            existingCartProductLIne.ProductId = cartProduct.ProductId;
            existingCartProductLIne.Quantity = cartProduct.Quantity;
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + cartId;
            return Ok(existingCartProductLIne);
        }
        
        [HttpDelete]
        [Route("{cartId}")]
        public ActionResult Delete(int cartId) {
            var existingCartProductLIne = _context.Cart.Find(cartId);
            if (existingCartProductLIne == null) {
                return NotFound("Product line not found");
            } else {
                _context.Cart.Remove(existingCartProductLIne);
                _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}