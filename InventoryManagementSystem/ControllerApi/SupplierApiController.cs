using InventoryManagementSystem.DBConfigure;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagementSystem.Controllers
{
    [RoutePrefix("api/supplier")]
    public class SupplierApiController : ApiController
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: api/supplier
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetSuppliers()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var suppliers = db.Suppliers.ToList();
            return Ok(suppliers);
        }

        // GET: api/supplier/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetSupplier(int id)
        {
            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        // POST: api/supplier
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duplicate = db.Suppliers.FirstOrDefault(x => x.Name == supplier.Name);
            if (duplicate != null)
                return Content(HttpStatusCode.Conflict, "Duplicate supplier found.");

            db.Suppliers.Add(supplier);
            db.SaveChanges();
            return Created("api/supplier/" + supplier.Id, supplier);
        }

        // PUT: api/supplier/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSupplier = db.Suppliers.Find(id);
            if (existingSupplier == null)
                return NotFound();

            var duplicate = db.Suppliers.FirstOrDefault(x => x.Name == supplier.Name && x.Id != id);
            if (duplicate != null)
                return Content(HttpStatusCode.Conflict, "Duplicate supplier found.");

            existingSupplier.Name = supplier.Name;
            db.SaveChanges();

            return Ok(existingSupplier);
        }

        // DELETE: api/supplier/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteSupplier(int id)
        {
            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
                return NotFound();

            db.Suppliers.Remove(supplier);
            db.SaveChanges();

            return Ok();
        }
    }
}
