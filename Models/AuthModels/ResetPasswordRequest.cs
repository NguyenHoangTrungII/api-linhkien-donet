namespace linhkien_donet.Models.AuthModels
{
    public class ResetPasswordRequest
    {
        public string userId { get; set; }
        public string token { get; set; }

        public string newPassword { get; set; }

    }
}
