using InventoryManagementSystem.DBConfigure;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class WarehouseController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Warehouse
        public ActionResult Index()
        {
            var result = db.Warehouses.ToList();
            return View(result);
        }

        // Create new Warehouse
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            var sup = new Warehouse();
            sup.Action = EnumAction.Create.ToString();
 
            return View(sup);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Warehouse Warehouse)
        {
            ViewBag.Suppliers = new SelectList(db.Suppliers, "Id", "Name", Warehouse.SupplierId);

            if (ModelState.IsValid)
            {
                var duplicate = db.Warehouses.FirstOrDefault(x => x.Name == Warehouse.Name);
                if (duplicate != null)
                {
                    ModelState.AddModelError("Name", "Duplicate found");
                    return View(Warehouse);
                }

                db.Warehouses.Add(Warehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Warehouse);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var Warehouse = db.Warehouses.Find(id);
            if (Warehouse == null)
                return HttpNotFound();

            return View(Warehouse);
        }

        public ActionResult Edit(int? id)
        {

            var sup = db.Warehouses.Find(id);
            ViewBag.Suppliers = new SelectList(db.Suppliers, "Id", "Name", sup.SupplierId);
             
            sup.Action = EnumAction.Update.ToString();
            return View(sup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Warehouse Warehouse)
        {
            ViewBag.Suppliers = new SelectList(db.Suppliers, "Id", "Name", Warehouse.SupplierId);

            if (ModelState.IsValid)
            {
                var existingWarehouse = db.Warehouses.Find(Warehouse.Id);

                var duplicate = db.Warehouses.FirstOrDefault(x => x.Name == Warehouse.Name && x.Id!=Warehouse.Id);
                if (duplicate != null)
                {
                    ModelState.AddModelError("Name", "Duplicate found");
                    return View(Warehouse);
                }

                if (existingWarehouse != null)
                {
                    existingWarehouse.Name = Warehouse.Name;
                    existingWarehouse.SupplierId = Warehouse.SupplierId;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Warehouse not found");
            }

            return View(Warehouse);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var Warehouse = db.Warehouses.Find(id);

            if (Warehouse == null)
                return HttpNotFound();

            return View(Warehouse);
        }

        // POST: Warehouse/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var Warehouse = db.Warehouses.Find(id);

            if (Warehouse != null)
            {
                db.Warehouses.Remove(Warehouse);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}