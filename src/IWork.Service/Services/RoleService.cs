using IWork.Data.Context;
using IWork.Domain.Models.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class RoleService : BaseService<Role>
    {
        public RoleService(DataContext context) : base(context) {}
    }
}
