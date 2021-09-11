using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payment.Api.Interfaces;
using Payment.Api.Models;
using Payment.Interfaces.Interfaces;
using Payment.Interfaces.Models.Request;
using Payment.Interfaces.Models.Response;

namespace Payment.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IPaymentApiHttpClient _paymentApiHttpClient;
        private readonly IOptions<PaymentApiSettings> _paymentOptions;

        public PaymentService(IPaymentApiHttpClient paymentApiHttpClient, ILogger<PaymentService> logger, IOptions<PaymentApiSettings> paymentOptions)
        {
            _paymentApiHttpClient = paymentApiHttpClient;
            _paymentOptions = paymentOptions;
            _logger = logger;
        }

        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string id)
        {
            _logger.LogInformation($"{nameof(PaymentService)} - {nameof(GetPaymentStatusAsync)} - {nameof(id)} is {id}");
            
            var url = _paymentOptions.Value.RestEndpoints.GetPaymentStatus;
            var paymentStatus = await _paymentApiHttpClient.GetAsync<PaymentStatusResponse>(url, id);

            return paymentStatus;
        }

        public async Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request)
        {
            _logger.LogInformation(
                $"{nameof(PaymentService)} - {nameof(CreatePaymentAsync)} - {nameof(CreatePaymentRequest)} is {request}");

            var url = _paymentOptions.Value.RestEndpoints.CreatePayment;

            var payment =
                await _paymentApiHttpClient.PostAsync<CreatePaymentRequest, CreatePaymentResponse>(url, request);

            return payment;
        }

        public async Task<PaymentStatusResponse> ConfirmPaymentAsync(ConfirmPaymentRequest request)
        {
            _logger.LogInformation(
                $"{nameof(PaymentService)} - {nameof(ConfirmPaymentAsync)} - {nameof(ConfirmPaymentRequest)} is {request}");


            var url = _paymentOptions.Value.RestEndpoints.ConfirmPayment;

            var payment =
                await _paymentApiHttpClient.PostAsync<ConfirmPaymentRequest, PaymentStatusResponse>(url, request);

            return payment;
        }
    }
}
