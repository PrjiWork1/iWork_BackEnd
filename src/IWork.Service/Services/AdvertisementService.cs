using IWork.Data.Context;
using IWork.Domain.Models;
using IWork.Domain.Models.Enums;
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
    public class AdvertisementService 
    {
        private readonly DataContext _context;
        public AdvertisementService(DataContext context)
        {
            _context = context;
        }

        public List<AdvertisementViewModel> GetAllAdvertisements()
        {
            var normalAdvertisements = _context.NormalAdvertisement
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
                    n.Status,
                    null, // Para itens, se não houver
                    n.IsActive
                ))
                .ToList(); // Materializa a consulta em uma lista

            var dynamicAdvertisements = _context.DynamicAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .Include(a => a.Items) // Incluindo os itens
                .Select(d => new AdvertisementViewModel(
                    d.Id,
                    d.Title,
                    d.Description,
                    d.UrlBanner,
                    d.Type,
                    d.IWorkPro,
                    d.UserId,
                    d.User.UserName,
                    d.User.CompleteName,
                    d.CategoryId,
                    d.Category.Description,
                    d.AdvertisementRate,
                    d.CreatedAt,
                    0, // Preço como 0 para anúncios dinâmicos
                    d.Status,
                    d.Items.Select(item => new ItemAdvertisementViewModel(
                        item.Name,
                        item.Price
                    )).ToList(),
                    d.IsActive
                ))
                .ToList(); // Materializa a consulta em uma lista

            var allAdvertisements = normalAdvertisements
                .Concat(dynamicAdvertisements) // Concatena as duas listas
                .ToList(); // Embora não seja necessário aqui, é uma boa prática

            return allAdvertisements;
        }


        public AdvertisementViewModel GetAdvertisementById(Guid AdvertisemenId)
        {
            // Tenta encontrar o anúncio normal pelo ID
            var normalAdvertisement = _context.NormalAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefault(n => n.Id == AdvertisemenId);

            if (normalAdvertisement != null)
            {
                return new AdvertisementViewModel(
                    normalAdvertisement.Id,
                    normalAdvertisement.Title,
                    normalAdvertisement.Description,
                    normalAdvertisement.UrlBanner,
                    normalAdvertisement.Type,
                    normalAdvertisement.IWorkPro,
                    normalAdvertisement.UserId,
                    normalAdvertisement.User.UserName,
                    normalAdvertisement.User.CompleteName,
                    normalAdvertisement.CategoryId,
                    normalAdvertisement.Category.Description,
                    normalAdvertisement.AdvertisementRate,
                    normalAdvertisement.CreatedAt,
                    normalAdvertisement.Price,
                    normalAdvertisement.Status,
                    null, // Para itens, se não houver
                    normalAdvertisement.IsActive
                );
            }

            // Tenta encontrar o anúncio dinâmico pelo ID
            var dynamicAdvertisement = _context.DynamicAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .Include(a => a.Items) // Incluindo os itens
                .FirstOrDefault(d => d.Id == AdvertisemenId);

            if (dynamicAdvertisement != null)
            {
                return new AdvertisementViewModel(
                    dynamicAdvertisement.Id,
                    dynamicAdvertisement.Title,
                    dynamicAdvertisement.Description,
                    dynamicAdvertisement.UrlBanner,
                    dynamicAdvertisement.Type,
                    dynamicAdvertisement.IWorkPro,
                    dynamicAdvertisement.UserId,
                    dynamicAdvertisement.User.UserName,
                    dynamicAdvertisement.User.CompleteName,
                    dynamicAdvertisement.CategoryId,
                    dynamicAdvertisement.Category.Description,
                    dynamicAdvertisement.AdvertisementRate,
                    dynamicAdvertisement.CreatedAt,
                    0, // Preço como 0 para anúncios dinâmicos
                    dynamicAdvertisement.Status,
                    dynamicAdvertisement.Items.Select(item => new ItemAdvertisementViewModel(
                        item.Name,
                        item.Price
                    )).ToList(),
                    dynamicAdvertisement.IsActive
                );
            }

            return null;
        }

        public async Task<bool> UpdateAdvertisementStatus(Guid advertisementId, AdvertisementStatus status)
        {

            var normalAdvertisement = await _context.NormalAdvertisement
                .FirstOrDefaultAsync(n => n.Id == advertisementId);

            if (normalAdvertisement != null)
            {
                normalAdvertisement.Status = status;
                _context.NormalAdvertisement.Update(normalAdvertisement);
                await _context.SaveChangesAsync();
                return true;
            }

            var dynamicAdvertisement = await _context.DynamicAdvertisement
                .FirstOrDefaultAsync(d => d.Id == advertisementId);

            if (dynamicAdvertisement != null)
            {

                dynamicAdvertisement.Status = status;
                _context.DynamicAdvertisement.Update(dynamicAdvertisement);
                await _context.SaveChangesAsync();
                return true;
            }


            return false;
        }
    }
}
