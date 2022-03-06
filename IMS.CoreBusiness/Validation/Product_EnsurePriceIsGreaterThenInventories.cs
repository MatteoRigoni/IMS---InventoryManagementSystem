using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness.Validation
{
    internal class Product_EnsurePriceIsGreaterThenInventories : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var product = validationContext.ObjectInstance as Product;
            if (product != null)
            {
                if (!product.ValidatePricing())
                    return new ValidationResult("The product's price is less than the summary of its inventories's price!");
            }
            return ValidationResult.Success;
        }
    }
}
