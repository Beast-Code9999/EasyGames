using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGames.Models
{
    public class Product
    {
        [Key] // Explicitly define Id as the primary key
        public int Id { get; set; }
        [Required] // Ensure that the script in the SQL have a not null setting
        [DisplayName("Product Name")]
        [MaxLength(40)] // add max length of 40 char to name
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Product name must contain only letters.")]
        public string Name { get; set; }
        public string Description { get; set; }
        // add price between 0.01 to 10,000
        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
