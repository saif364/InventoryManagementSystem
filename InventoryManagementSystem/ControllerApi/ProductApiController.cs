using InventoryManagementSystem.DBConfigure;
using InventoryManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace InventoryManagementSystem.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductApiController : ApiController
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: api/products
        [HttpGet, Route("")]
        public IHttpActionResult GetProducts(string searchQuery = null)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var products = db.Products.Include("Warehouse").AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery));
            }
            var result =products.ToList();
            return Ok( result);
        }   

        // GET: api/products/{id}
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.Include("Warehouse").FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost, Route("")]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var duplicate = db.Products.FirstOrDefault(p => p.Name == product.Name);
            if (duplicate != null)
                return Content(HttpStatusCode.Conflict, "Duplicate product found.");

            db.Products.Add(product);
            db.SaveChanges();
            return Created($"api/products/{product.Id}", product);
        }

        // PUT: api/products/{id}
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProduct = db.Products.Find(id);
            if (existingProduct == null) return NotFound();

            var duplicate = db.Products.FirstOrDefault(p => p.Name == product.Name && p.Id != id);
            if (duplicate != null)
                return Content(HttpStatusCode.Conflict, "Duplicate found.");

            existingProduct.Name = product.Name;

            existingProduct.SKU = product.SKU;

            existingProduct.WarehouseId = product.WarehouseId;

            existingProduct.QuantityInStock = product.QuantityInStock;

            db.SaveChanges();
            return Ok(existingProduct);
        }

        // DELETE: api/products/{id}
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null) return NotFound();

            db.Products.Remove(product);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
