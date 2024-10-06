using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel(string name, bool isActive)
        {
           Name = name;
           IsActive = isActive;
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
