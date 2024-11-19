using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using IWork.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Service.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const long MaxFileSizeInBytes = 5 * 1024 * 1024;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            // Verifica se o arquivo é uma imagem e não excede o limite de tamanho
            ValidateImageFile(contentType, content.Length);

            var containerClient = GetContainerClient(blobContainerName);

            // Gera um GUID e cria um novo nome de arquivo
            var guid = Guid.NewGuid().ToString();
            var fileNameWithGuid = $"{Path.GetFileNameWithoutExtension(fileName)}_{guid}{Path.GetExtension(fileName)}";

            var blobClient = containerClient.GetBlobClient(fileNameWithGuid);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }

        private void ValidateImageFile(string contentType, long fileSize)
        {
            // Verifica se o tipo de conteúdo é uma imagem
            var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };

            if (!validImageTypes.Contains(contentType.ToLower()))
            {
                throw new InvalidOperationException("O arquivo não é uma imagem válida.");
            }

            if (fileSize > MaxFileSizeInBytes)
            {
                throw new InvalidOperationException("O arquivo excede o tamanho máximo de 5 MB.");
            }
        }

        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
    }
}
