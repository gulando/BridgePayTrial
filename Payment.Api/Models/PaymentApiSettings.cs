namespace Payment.Api.Models
{
    public class PaymentApiSettings
    {
        public const string PaymentApiSettingsSection = "PaymentApiSettings";

        public string BaseUrl { get; set; }

        public string MerchantId { get; set; }

        public string SecretKey { get; set; }

        public RestEndpoints RestEndpoints { get; set; }
    }
}
