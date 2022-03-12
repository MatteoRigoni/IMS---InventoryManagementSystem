using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Range(minimum: 1, maximum: Int32.MaxValue)]
        public int QuantityToProduce { get; set; }
        public double ProductPrice { get; set; }
    }
}
