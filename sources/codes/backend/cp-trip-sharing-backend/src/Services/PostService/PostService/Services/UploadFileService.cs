using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using PostService.Utils;
using System.Net.Http;

namespace PostService.Services
{
    public class UploadFileService : IUploadFileService
    {
        public string UploadImage(ImageParam imageParam)
        {
            var storageSettings = ReadAppSettings.ReadCloudStorageSettings().Value;
            var bucketName = storageSettings.BucketName;
            var baseUrl = storageSettings.BaseUrl;

            GoogleCredential credential = null;
            using (var jsonStream = new FileStream("storage-credentials.json", FileMode.Open,
                FileAccess.Read, FileShare.Read))
            {
                credential = GoogleCredential.FromStream(jsonStream);
            }

            var storageClient = StorageClient.Create(credential);

            var bytes = Convert.FromBase64String(imageParam.Image);

            var extension = FileUtils.GetImageExtension(imageParam.Type);
            var filename = FileUtils.RandomImageName() + extension;

            var uploaded = storageClient.UploadObject(bucketName, filename, imageParam.Type, new MemoryStream(bytes));

            var imageUrl = "";
            if (uploaded != null)
            {
                imageUrl = string.Join("/", new string[] { baseUrl, bucketName, filename }); 
            }
            return imageUrl;
        }
    }
}
