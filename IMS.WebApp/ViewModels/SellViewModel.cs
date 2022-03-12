using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required]
        public string SalesOrderNumber { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int QuantityToSell { get; set; }
        [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double ProductPrice { get; set; }
    }
}
