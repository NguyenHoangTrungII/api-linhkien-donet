using _01_WEBAPI.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using linhkien_donet.Interfaces;
using linhkien_donet.Models.CloudinaryModels;

namespace linhkien_donet.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private string CLOUD_NAME = string.Empty;
        private string API_KEY = string.Empty;
        private string API_SECRET = string.Empty;

        public CloudinaryService()
        {
            //this.CLOUD_NAME = ConfigurationManager.AppSettings["CLOUDNAME"];
            //this.API_KEY = ConfigurationManager.AppSettings["APIKEY"];
            //this.API_SECRET = ConfigurationManager.AppSettings["APISECREAT"];
            this.CLOUD_NAME = "dtdpz7tk5";
            this.API_KEY = "634851463263782";
            this.API_SECRET="16v14uvQ1D4eMfJnQRYK_z2YYRg";
        }

        public string UploadPhoto(string filePath)
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(
             CLOUD_NAME,
              API_KEY,
             API_SECRET);



            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(filePath)
            };

            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            return cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
        }

        public string UploadPhoto(IFormFile file)
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(
                CLOUD_NAME,
                API_KEY,
                API_SECRET);

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                return cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
            }
        }


        //public string UploadPhoto(Stream stream)
        //{
        //    Account account = new Account(
        //     CLOUD_NAME,
        //      API_KEY,
        //     API_SECRET);

        //    Cloudinary cloudinary = new Cloudinary(account);
        //    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
        //    {
        //        File = new CloudinaryDotNet.Actions.FileDescription(Guid.NewGuid().ToString(), stream),
        //    };

        //    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
        //    return cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
        //}

        //public string UploadPhoto(Stream stream, int height, int width)
        //{
        //    Account account = new Account(
        //      CLOUD_NAME,
        //       API_KEY,
        //      API_SECRET);

        //    Cloudinary cloudinary = new Cloudinary(account);
        //    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
        //    {
        //        File = new CloudinaryDotNet.Actions.FileDescription(Guid.NewGuid().ToString(), stream),
        //    };

        //    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
        //    return
        //        cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(width).Height(height).Crop("fill"))
        //            .BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));

        //}
    }
}

