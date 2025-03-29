using InventoryManagementSystem.DBConfigure;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Product
        public ActionResult Index(string searchQuery)
        {
            var products = db.Products.Include("Warehouse").AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery));
            }

            return View(products.ToList());
        }

        // Create new Product
        public ActionResult Create()
        {
            ViewBag.Warehousees = new SelectList(db.Warehouses, "Id", "Name");
            var sup = new Product();
            sup.Action = EnumAction.Create.ToString();
 
            return View(sup);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product Product)
        {
            ViewBag.Warehousees = new SelectList(db.Warehouses, "Id", "Name",Product.WarehouseId);
             

            if (ModelState.IsValid)
            {
                var duplicate = db.Products.FirstOrDefault(x => x.Name == Product.Name);
                if (duplicate != null)
                {
                    ModelState.AddModelError("Name", "Duplicate found");
                    return View(Product);
                }

                db.Products.Add(Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var Product = db.Products.Find(id);
            if (Product == null)
                return HttpNotFound();

            return View(Product);
        }

        public ActionResult Edit(int? id)
        {

            var sup = db.Products.Find(id);
            ViewBag.Warehousees = new SelectList(db.Warehouses, "Id", "Name");

            sup.Action = EnumAction.Update.ToString();
            return View(sup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product Product)
        {
            ViewBag.Warehousees = new SelectList(db.Warehouses, "Id", "Name", Product.WarehouseId);

            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Find(Product.Id);

                var duplicate = db.Products.FirstOrDefault(x => x.Name == Product.Name && x.Id!=Product.Id);
                if (duplicate != null)
                {
                    ModelState.AddModelError("Name", "Duplicate found");
                    return View(Product);
                }

                if (existingProduct != null)
                {
                    existingProduct.Name = Product.Name;
                    existingProduct.SKU = Product.SKU;
                    existingProduct.WarehouseId = Product.WarehouseId;
                    existingProduct.QuantityInStock = Product.QuantityInStock;
                    
                    db.SaveChanges();

        
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Product not found");
            }

            return View(Product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var Product = db.Products.Find(id);

            if (Product == null)
                return HttpNotFound();

            return View(Product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var Product = db.Products.Find(id);

            if (Product != null)
            {
                db.Products.Remove(Product);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}