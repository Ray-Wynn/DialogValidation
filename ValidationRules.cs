using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;

namespace DialogValidation
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateNotEmpty : ValidationRule
    {
        /// <summary>
        /// Validate entry string is not null or empty
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <param name="cultureInfo">default localal.</param>
        /// <returns>ValidationResult</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Required");
            }

            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Validate there are no ProductName duplicates
    /// </summary>
    public class ValidationNoProductNameDuplicate : ValidationRule
    {        
        public ProductItems? Products { get; set; } // Collection of ProductItem.
        public string? ProductName { get; set; } // Ignore the ProductName being validated in duplicate test.

        /// <summary>
        /// Validate entry string is not null or empty
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <param name="cultureInfo">default localal.</param>
        /// <returns>ValidationResult</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.Assert(Products != null, "ValidationNoProductNameDuplicate Products null!");            

            if(Products != null)
            {
                foreach(ProductItem product in Products)
                {
                    if (product.ProductName == (string)value && product.ProductName != ProductName)
                    {
                        return new ValidationResult(false, "Duplicate product name!");
                    }
                }
            }

            return ValidationResult.ValidResult;
        }
    }

    public class ValidatePrice : ValidationRule
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double price = 0;

            try
            {                
                if (((string)value).Length > 0) 
                {
                    string priceStr = (string)value; // Cast object to string
                    priceStr = priceStr.Replace("$", ""); // Strip any $ from value.
                    price = double.Parse(priceStr); // Convert string to double. This is where the exception would occur.
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if (price < MinPrice || price > MaxPrice)
            {
                return new ValidationResult(false, string.Format("Price is outside product range {0:C} to {1:C}", MinPrice, MaxPrice));
            }
            
            return ValidationResult.ValidResult;
        }
    }
}
