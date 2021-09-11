using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Payment.Api.HttpClient;
using Payment.Api.Interfaces;
using Payment.Api.Models;

namespace Payment.Api
{
    public static class DISetup
    {
        public static IServiceCollection RegisterPaymentApiClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            var section = configuration.GetSection(PaymentApiSettings.PaymentApiSettingsSection);
            var paymentApiSettings = section.Get<PaymentApiSettings>();

            services.Configure<PaymentApiHttpClient>(opt => section.Bind(opt));

            services.AddHttpClient<IPaymentApiHttpClient, PaymentApiHttpClient>(config =>
            {
                config.BaseAddress = new Uri(paymentApiSettings.BaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true,
            });

            return services;
        }
    }
}
