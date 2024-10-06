using IWork.Domain.Commands.UploadCommands;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;

namespace IWork.API.Handlers.UploadHandler
{
    public class CreateUploadHandler : IRequestHandler<UploadAddCommand, string>
    {
        private readonly IBlobService _blobService;

        public CreateUploadHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }
        public async Task<string> Handle(UploadAddCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null) throw new ArgumentNullException(nameof(request.File), "File not found.");

            var result = await _blobService.UploadFileBlobAsync(
                "iwork-banner-images",
                request.File.OpenReadStream(),
                request.File.ContentType,
                request.File.FileName

            );

            return result.AbsoluteUri;
        }
    }
}
