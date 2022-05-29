using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_context.Products);
        }

        [HttpGet]
        [Route("{productId}")]
        public ActionResult<IEnumerable<Product>> Get(int productId)
        {
            Product product = _context.Products.Find(productId);
            return product == null ? NotFound("Product not found") : Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct!=null) {
                return Conflict("This Id is already being used.");
            } else {
            _context.Products.Add(product);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + product.Id;
            return Created(resourceUrl, product);
            }
        }

        [HttpPut]
        [Route("{productId}")]
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
    }
}