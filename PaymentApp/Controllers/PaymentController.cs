using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Payment.Interfaces.Interfaces;
using Payment.Interfaces.Models.Request;
using Payment.Interfaces.Models.Response;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("status")]
        public async Task<PaymentStatusResponse> GetPaymentStatus(string id)
        {
            _logger.LogInformation($"{nameof(PaymentController)} - {nameof(GetPaymentStatus)} - {nameof(id)} is {id}");

            try
            {
                var status = await _paymentService.GetPaymentStatusAsync(id);

                return status;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{nameof(PaymentController)} - {nameof(GetPaymentStatus)} - Exception is {e.Message}");
                return null;
            }
        }

        [HttpPost("create")]
        public async Task<CreatePaymentResponse> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            _logger.LogInformation(
                $"{nameof(PaymentController)} - {nameof(CreatePayment)} - {nameof(CreatePaymentRequest)} is {request}");

            try
            {
                var status = await _paymentService.CreatePaymentAsync(request);

                return status;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{nameof(PaymentController)} - {nameof(CreatePayment)} - Exception is {e.Message}");
                return null;
            }
        }

        [HttpPost("confirm")]
        public async Task<PaymentStatusResponse> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
        {
            _logger.LogInformation(
                $"{nameof(PaymentController)} - {nameof(ConfirmPayment)} - {nameof(ConfirmPaymentRequest)} is {request}");

            try
            {
                var status = await _paymentService.ConfirmPaymentAsync(request);

                return status;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{nameof(PaymentController)} - {nameof(CreatePayment)} - Exception is {e.Message}");
                return null;
            }
        }
    }
}
