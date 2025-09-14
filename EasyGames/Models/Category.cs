using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EasyGames.Models
{
    public class Category
    {
        
        [Key] // Explicitly define Id as the primary key
        public int Id { get; set; }
        [Required] // Ensure that the script in the SQL have a not null setting
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
