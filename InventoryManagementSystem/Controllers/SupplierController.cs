using InventoryManagementSystem.DBConfigure;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SupplierController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Supplier
        public ActionResult Index()
        {
            var result = db.Suppliers.ToList();
            return View(result);
        }

        // Create new Supplier
        public ActionResult Create()
        {
            var sup = new Supplier();
            sup.Action = EnumAction.Create.ToString();
            return View(sup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.Suppliers.FirstOrDefault(x => x.Name == supplier.Name);
                if (duplicate != null)
                {
                    ModelState.AddModelError("Name", "Duplicate found");
                    return View(supplier);
                }

                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
                return HttpNotFound();

            return View(supplier);
        }

        public ActionResult Edit(int? id)
        {
            var sup =db.Suppliers.Find(id);
            sup.Action = EnumAction.Update.ToString();
            return View(sup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var existingSupplier = db.Suppliers.Find(supplier.Id);

                var duplicate = db.Suppliers.FirstOrDefault(x => x.Name == supplier.Name);
                if (duplicate!=null)
                {
                    ModelState.AddModelError("Name", "Duplicate found");
                    return View(supplier);
                }

                if (existingSupplier != null)
                {
                    existingSupplier.Name = supplier.Name;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Supplier not found");
            }

            return View(supplier);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var supplier = db.Suppliers.Find(id);

            if (supplier == null)
                return HttpNotFound();

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var supplier = db.Suppliers.Find(id);

            if (supplier != null)
            {
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}