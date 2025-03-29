using InventoryManagementSystem.DBConfigure;
using InventoryManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace InventoryManagementSystem.Controllers
{
    [RoutePrefix("api/warehouse")]
    public class WarehouseApiController : ApiController
    {
        private InventoryDbContext db = new InventoryDbContext();


        // GET: api/warehouse
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllWarehouses()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var warehouses = db.Warehouses.ToList();
            return Ok(warehouses);
        }

        // GET: api/warehouse/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetWarehouse(int id)
        {
            var warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
                return NotFound();
            return Ok(warehouse);
        }

        // POST: api/warehouse
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateWarehouse(Warehouse warehouse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var duplicate = db.Warehouses.FirstOrDefault(x => x.Name == warehouse.Name);
            if (duplicate != null)
                return BadRequest("Duplicate warehouse name.");

            db.Warehouses.Add(warehouse);
            db.SaveChanges();
            return Ok(warehouse);
        }

        // PUT: api/warehouse/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateWarehouse(int id, Warehouse warehouse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingWarehouse = db.Warehouses.Find(id);
            if (existingWarehouse == null)
                return NotFound();

            var duplicate = db.Warehouses.FirstOrDefault(x => x.Name == warehouse.Name && x.Id != id);
            if (duplicate != null)
                return BadRequest("Duplicate warehouse name.");

            existingWarehouse.Name = warehouse.Name;
            existingWarehouse.SupplierId = warehouse.SupplierId;
            db.SaveChanges();
            return Ok(existingWarehouse);
        }

        // DELETE: api/warehouse/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteWarehouse(int id)
        {
            var warehouse = db.Warehouses.Find(id);
            if (warehouse == null)
                return NotFound();

            db.Warehouses.Remove(warehouse);
            db.SaveChanges();
            return Ok();
        }
    }
}
