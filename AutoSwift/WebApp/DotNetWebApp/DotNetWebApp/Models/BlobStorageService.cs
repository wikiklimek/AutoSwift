using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DotNetWebApp.Models
{
    public class BlobStorageService
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=storagedotnet;AccountKey=xx;EndpointSuffix=core.windows.net";
        private readonly string _containerName = "photos";

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.ToString();  
        }

		public async Task<string> UploadFileUserDataAsync(IFormFile file, string userID)
		{
			var blobServiceClient = new BlobServiceClient(_connectionString);
			var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

			var fileName = $"{Guid.NewGuid()}_{file.FileName}";
			var blobClient = blobContainerClient.GetBlobClient(fileName);

			using (var stream = file.OpenReadStream())
			{
				await blobClient.UploadAsync(stream, overwrite: true);
			}

			return blobClient.Uri.ToString();  
		}
	}
}
