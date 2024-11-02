using IWork.Data.Context;
using IWork.Domain.DTO;
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
    public class AdvertisementService : IAdvertisementService
    {
        private readonly DataContext _context;
        public AdvertisementService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AdvertisementDTO>> GetAllAdvertisements(bool isAdmin)
        {
            // Consultar anúncios normais
            var normalAdvertisements = await _context.NormalAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .ToListAsync(); 

            // Consultar anúncios dinâmicos
            var dynamicAdvertisements = await _context.DynamicAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .Include(a => a.Items)
                .ToListAsync(); 

            
            var allAdvertisements = normalAdvertisements
                .Select(n => new AdvertisementDTO(
                    n.Id,
                    n.Title,
                    n.Description,
                    n.UrlBanner,
                    n.Type,
                    n.UserId,
                    n.User.UserName,
                    n.User.CompleteName,
                    n.CategoryId,
                    n.Category.Description,
                    n.AdvertisementRate,
                    n.CreatedAt,
                    n.Price,
                    n.Status,
                    n.NumberOfSales,
                    null, // Para itens, se não houver
                    n.IsActive
                ))
                .Concat(dynamicAdvertisements.Select(d => new AdvertisementDTO(
                    d.Id,
                    d.Title,
                    d.Description,
                    d.UrlBanner,
                    d.Type,
                    d.UserId,
                    d.User.UserName,
                    d.User.CompleteName,
                    d.CategoryId,
                    d.Category.Description,
                    d.AdvertisementRate,
                    d.CreatedAt,
                    0, // Preço como 0 para anúncios dinâmicos
                    d.Status,
                    d.NumberOfSales,
                    d.Items.Select(item => new ItemAdvertisementDTO(
                        item.Name,
                        item.Price
                    )).ToList(),
                    d.IsActive
                )))
                .ToList(); 

            // Filtragem e ordenação com base no contexto
            if (isAdmin)
            {
                allAdvertisements = allAdvertisements
                    .Where(ad => ad.Status == AdvertisementStatus.UnderReview)
                    .OrderByDescending(ad => ad.CreatedAt)
                    .ToList();
            }
            else
            {
                allAdvertisements = allAdvertisements
                    .Where(ad => ad.Status == AdvertisementStatus.Approved)
                    .OrderBy(ad => ad.CreatedAt)
                    .ToList();
            }

            return allAdvertisements;
        }


        public Task<AdvertisementDTO> GetAdvertisementById(Guid AdvertisemenId)
        {
            // Tenta encontrar o anúncio normal pelo ID
            var normalAdvertisement = _context.NormalAdvertisement
                .AsNoTracking()
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefault(n => n.Id == AdvertisemenId);

            if (normalAdvertisement != null)
            {
                return Task.FromResult(new AdvertisementDTO(
                    normalAdvertisement.Id,
                    normalAdvertisement.Title,
                    normalAdvertisement.Description,
                    normalAdvertisement.UrlBanner,
                    normalAdvertisement.Type,
                    normalAdvertisement.UserId,
                    normalAdvertisement.User.UserName,
                    normalAdvertisement.User.CompleteName,
                    normalAdvertisement.CategoryId,
                    normalAdvertisement.Category.Description,
                    normalAdvertisement.AdvertisementRate,
                    normalAdvertisement.CreatedAt,
                    normalAdvertisement.Price,
                    normalAdvertisement.Status,
                    normalAdvertisement.NumberOfSales,
                    null, // Para itens, se não houver
                    normalAdvertisement.IsActive
                ));
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
                return Task.FromResult( new AdvertisementDTO(
                    dynamicAdvertisement.Id,
                    dynamicAdvertisement.Title,
                    dynamicAdvertisement.Description,
                    dynamicAdvertisement.UrlBanner,
                    dynamicAdvertisement.Type,
                    dynamicAdvertisement.UserId,
                    dynamicAdvertisement.User.UserName,
                    dynamicAdvertisement.User.CompleteName,
                    dynamicAdvertisement.CategoryId,
                    dynamicAdvertisement.Category.Description,
                    dynamicAdvertisement.AdvertisementRate,
                    dynamicAdvertisement.CreatedAt,
                    0, // Preço como 0 para anúncios dinâmicos
                    dynamicAdvertisement.Status,
                    dynamicAdvertisement.NumberOfSales,
                    dynamicAdvertisement.Items.Select(item => new ItemAdvertisementDTO(
                        item.Name,
                        item.Price
                    )).ToList(),
                    dynamicAdvertisement.IsActive
                ));
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

        public async Task<bool> UpdateAdvertisementNumberOfSales(Guid advertisementId, int numberOfSales)
        {
            var normalAdvertisement = await _context.NormalAdvertisement
                .FirstOrDefaultAsync(n => n.Id == advertisementId);

            if (normalAdvertisement != null)
            {
                normalAdvertisement.NumberOfSales = numberOfSales;
                _context.NormalAdvertisement.Update(normalAdvertisement);
                await _context.SaveChangesAsync();
                return true;
            }

            var dynamicAdvertisement = await _context.DynamicAdvertisement
                .FirstOrDefaultAsync(d => d.Id == advertisementId);

            if (dynamicAdvertisement != null)
            {

                dynamicAdvertisement.NumberOfSales = numberOfSales;
                _context.DynamicAdvertisement.Update(dynamicAdvertisement);
                await _context.SaveChangesAsync();
                return true;
            }


            return false;
        }
    }
}
