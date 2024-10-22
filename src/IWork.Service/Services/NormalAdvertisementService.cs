using IWork.Data.Context;
using IWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class NormalAdvertisementService : BaseService<NormalAdvertisement>
    {
        public NormalAdvertisementService(DataContext context) : base(context)
        {
        }
    }
}
