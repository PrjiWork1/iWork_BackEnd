using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
using IWork.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class AdvertisementService : BaseService<Advertisement>, IAdvertisementService
    {
        private readonly DataContext _context;
        public AdvertisementService(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Advertisement> GetAdvertisementById(Guid AdvertisemenId)
        {
            var Advertisement = _context.Advertisement
                 .AsNoTracking()
                 .Include(a => a.Category)
                 .Include(a => a.User)
                 .Include(a => a.Items)
                 .FirstOrDefault(a => a.Id == AdvertisemenId);

           

            return Advertisement;
        }

        public async Task<List<Advertisement>> GetAllAdvertisements(bool isAdmin)
        {
            var advertisements = await _context.Advertisement
               .AsNoTracking()
               .Include(a => a.Category)
               .Include(a => a.User)
               .Include(a => a.Items)
               .ToListAsync();

            // Filtragem e ordenação com base no contexto
            if (isAdmin)
            {
                advertisements = advertisements
                    .Where(ad => ad.Status == AdvertisementStatus.UnderReview && ad.IsActive)
                    .OrderByDescending(ad => ad.CreatedAt)
                    .ToList();
            }
            else
            {
                advertisements = advertisements
                    .Where(ad => ad.Status == AdvertisementStatus.Approved && ad.IsActive)
                    .OrderBy(ad => ad.CreatedAt)
                    .ToList();
            }

            return advertisements;
        }
    }
}
