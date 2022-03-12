using IMS.CoreBusiness.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        //public int Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        [Product_EnsurePriceIsGreaterThenInventories]
        public double Price { get; set; }
        public bool IsActive { get; set; } = true;
        public List<ProductInventory> ProductInventories { get; set; }
        public int Quantity { get; set; }

        public bool ValidatePricing()
        {
            if (ProductInventories == null || ProductInventories.Count <= 0) return true;

            double priceAllInventories = ProductInventories.Sum(pi => pi.InventoryQuantity * (pi.Inventory?.Price ?? 0));
            if (priceAllInventories > Price) return false;

            return true;
        }
    }
}
