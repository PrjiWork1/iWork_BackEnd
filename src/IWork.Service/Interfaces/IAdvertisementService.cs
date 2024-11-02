using IWork.Domain.DTO;
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
    public interface IAdvertisementService
    {
        Task<List<AdvertisementDTO>> GetAllAdvertisements(bool isAdmin);
        Task<AdvertisementDTO> GetAdvertisementById(Guid AdvertisemenId);
        Task<bool> UpdateAdvertisementStatus(Guid advertisementId, AdvertisementStatus status);
        Task<bool> UpdateAdvertisementNumberOfSales(Guid advertisementId, int numberOfSales);
    }
}
