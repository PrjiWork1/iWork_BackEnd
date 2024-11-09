using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Interfaces
{
    public interface IAdvertisementService : IBaseService<Advertisement>
    {
        Task<List<Advertisement>> GetAllAdvertisements(bool isAdmin);
        Task<Advertisement> GetAdvertisementById(Guid AdvertisemenId);
    }
}
