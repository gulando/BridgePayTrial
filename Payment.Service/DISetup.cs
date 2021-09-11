using Microsoft.Extensions.DependencyInjection;
using Payment.Interfaces.Interfaces;
using Payment.Service.Services;

namespace Payment.Service
{
    public static class DISetup
    {
        public static IServiceCollection RegisterPaymentService(this IServiceCollection services)
        {
            return services.AddSingleton<IPaymentService, PaymentService>();
        }
    }
}
