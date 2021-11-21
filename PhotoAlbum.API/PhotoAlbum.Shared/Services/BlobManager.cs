using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Shared.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Shared.Services
{
    public interface IBlobManager
    {
        void UploadPhoto(Guid id, byte[] file);
        byte[] DownloadPhoto(Guid id);
    }

    public class BlobManager : IBlobManager
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly string containerName = "cmpg-32333219923-photos";

        public BlobManager(IConfiguration config, ILogger<BlobManager> logger)
        {
            _config = config;
            _logger = logger;

            _connectionString = config[AppSettings.AZURE_STORAGE_CONNECTION_STRING];

            if (string.IsNullOrEmpty(_connectionString))
                throw new Exception("Azure storage blob connection string is invalid!");

            //Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            //Ensure container is created
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (!containerClient.Exists())
            {
                blobServiceClient.CreateBlobContainer(containerName);
            }
        }

        public void UploadPhoto(Guid id, byte[] file)
        {
            BlobServiceClient blobClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobClient.GetBlobContainerClient(containerName);

            //Ensure file does not already exist
            containerClient.DeleteBlobIfExists(id.ToString());

            using (var stream = new MemoryStream(file))
            {
                var uploadResult = containerClient.UploadBlob(id.ToString(), stream);
            }
        }

        public byte[] DownloadPhoto(Guid id)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(id.ToString());

            byte[] file = null;

            if (blobClient.Exists())
            {
                using (var stream = new MemoryStream())
                {
                    blobClient.DownloadTo(stream);
                    file = stream.ToArray();
                }
            }

            return file;
        }
    }
}
