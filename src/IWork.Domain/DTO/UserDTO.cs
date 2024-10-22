using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.DTO
{
    public class UserDTO
    {
        public UserDTO(Guid id, string completeName, string userName, string email, string role, bool isActive)
        {
            Id = id;
            CompleteName = completeName;
            UserName = userName;
            Email = email;
            Role = role;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string CompleteName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
