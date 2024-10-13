using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models.IdentityEntities
{
    public class User : IdentityUser
    {
        public string CompleteName { get; set; }
        public string? CPF { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public ICollection<DynamicAdvertisement> DynamicAdvertisements { get; set; } 
        public ICollection<NormalAdvertisement> NormalAdvertisements { get; set; } 
        public bool IsActive { get; set; }
    }
}
