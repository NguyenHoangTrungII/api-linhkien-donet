namespace linhkien_donet.Interfaces
{
    public interface ICloudinaryService
    {
        public string UploadPhoto(string file);

        public string UploadPhoto(IFormFile file);

    }
}
