﻿using IWork.Domain.Commands.NormalAdvertisementCommands;
using IWork.Domain.Models;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.NormalAdvertisementHandler
{
    public class CreateNormalAdvertisementHandler : IRequestHandler<NormalAdvertisementAddCommand, bool>
    {
        private readonly NormalAdvertisementService _normalAdvertisementService;

        public CreateNormalAdvertisementHandler(NormalAdvertisementService normalAdvertisementService)
        {
            _normalAdvertisementService = normalAdvertisementService;
        }

        public async Task<bool> Handle(NormalAdvertisementAddCommand request, CancellationToken cancellationToken)
        {
            var normalAdvertisement = new NormalAdvertisement(request.Title, request.Description, request.UrlBanner,
            request.Type, request.IWorkPro, request.UserId, request.CategoryId, request.IsActive, DateTime.UtcNow,
            request.Price, request.Stock);

            var result = await _normalAdvertisementService.Add(normalAdvertisement);

            if (result) return true;

            return false;
        }
    }
}
