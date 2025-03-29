using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        [Required]
        public int QuantityInStock { get; set; }


        //

        [NotMapped]
        public string Action { get; set; }
    }
}