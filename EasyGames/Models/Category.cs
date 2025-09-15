using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EasyGames.Models
{
    public class Category
    {
        
        [Key] // Explicitly define Id as the primary key
        public int Id { get; set; }
        [Required] // Ensure that the script in the SQL have a not null setting
        [DisplayName("Category Name")]
        [MaxLength(40)] // add max length of 40 char to name
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 200)] // Min and Max amount 
        public int DisplayOrder { get; set; }
    }
}
