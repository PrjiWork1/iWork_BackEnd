using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.ViewModels
{
    public class UpdateUserRoleViewModel
    {
        public UpdateUserRoleViewModel(string email, string role, bool delete)
        {
            Email = email;
            Role = role;
            Delete = delete;
        }

        public string Email { get; set; }
       public string Role { get; set; }
       public bool Delete { get; set; }
    }
}
