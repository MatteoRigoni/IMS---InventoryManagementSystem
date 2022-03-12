using IMS.CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public string PurchaseOrder { get; set; }
        public int InventoryId { get; set; }
        [Required]
        public string InventoryName { get; set; }
        [Range(minimum:1, maximum:Int32.MaxValue)]
        public int QuantityToPurchase { get; set; }
        public double InventoryPrice { get; set; }
    }
}
