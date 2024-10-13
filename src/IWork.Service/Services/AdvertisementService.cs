using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Domain.Models.IdentityEntities;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class AdvertisementService : BaseService<NormalAdvertisement>
    {
        private readonly DataContext _context;
        public AdvertisementService(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<AdvertisementViewModel> GetAlldvertisements()
        {
            var advertisement = _context.NormalAdvertisement
               .AsNoTracking()
               .Include(a => a.Category)
               .Include(a => a.User)
                .Select(n => new AdvertisementViewModel(
                n.Id,
                n.Title,
                n.Description,
                n.UrlBanner,
                n.Type,
                n.IWorkPro,
                n.UserId,
                n.User.UserName,
                n.User.CompleteName,
                n.CategoryId,
                n.Category.Description,
                n.AdvertisementRate,
                n.CreatedAt,
                n.Price,
                n.IsActive
            ));

            return advertisement.ToList();
        }

        public List<AdvertisementViewModel> GetAdvertisementById(Guid AdvertisementId)
        {
            var advertisement = _context.NormalAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .Where(a => a.Id == AdvertisementId)
                 .Select(n => new AdvertisementViewModel(
                 n.Id,
                 n.Title,
                 n.Description,
                 n.UrlBanner,
                 n.Type,
                 n.IWorkPro,
                 n.UserId,
                 n.User.UserName,
                 n.User.CompleteName,
                 n.CategoryId,
                 n.Category.Description,
                 n.AdvertisementRate,
                 n.CreatedAt,
                 n.Price,
                 n.IsActive
             ));

            return advertisement.ToList();
        }
    }
}
