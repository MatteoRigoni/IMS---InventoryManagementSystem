using System.ComponentModel.DataAnnotations;

namespace IMS.CoreBusiness
{
    public class Inventory
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public double Price { get; set; }
        public List<ProductInventory> ProductInventories { get; set; }

    }
}