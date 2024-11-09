using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class HiringAdvertisementService : BaseService<HiringAdvertisement>, IHiringAdvertisementService
    {
        public HiringAdvertisementService(DataContext context) : base(context)
        {
        }
    }
}
