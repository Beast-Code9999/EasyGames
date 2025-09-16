using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGames.Models
{
    public class ApplicationUser : IdentityUser // ensure that it extends the identityUser
    {
        // ensure that name is required
        [Required]
        public int Name {  get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        // we then add a mapper for this in applicationDbContext
    }
}
