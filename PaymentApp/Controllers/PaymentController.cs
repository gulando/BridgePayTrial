using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Payment.Api.Models;
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

        [HttpGet("status/{id}")]
        public async Task<ActionResult<PaymentStatusResponse>> GetPaymentStatus(string id)
        {
            _logger.LogInformation($"{nameof(PaymentController)} - {nameof(GetPaymentStatus)} - {nameof(id)} is {id}");

            try
            {
                var status = await _paymentService.GetPaymentStatusAsync(id);

                return status;
            }
            catch (PaymentException e)
            {
                _logger.LogInformation($"{nameof(PaymentController)} - {nameof(GetPaymentStatus)} - Status Code is {e.StatusCode}");
                return StatusCode((int)e.StatusCode);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            _logger.LogInformation(
                $"{nameof(PaymentController)} - {nameof(CreatePayment)} - {nameof(CreatePaymentRequest)} is {request.OrderId}");

            try
            {
                var payment = await _paymentService.CreatePaymentAsync(request);

                return payment;
            }
            catch (PaymentException e)
            {
                _logger.LogInformation($"{nameof(PaymentController)} - {nameof(CreatePayment)} - Status Code is {e.StatusCode}");
                return StatusCode((int)e.StatusCode);
            }
        }

        [HttpPost("confirm")]
        public async Task<ActionResult<PaymentStatusResponse>> ConfirmPayment([FromBody] ConfirmPaymentRequest request)
        {
            _logger.LogInformation(
                $"{nameof(PaymentController)} - {nameof(ConfirmPayment)} - {nameof(ConfirmPaymentRequest)} is {request.TransactionId}");

            try
            {
                var confirmPayment = await _paymentService.ConfirmPaymentAsync(request);

                return confirmPayment;
            }
            catch (PaymentException e)
            {
                _logger.LogInformation($"{nameof(PaymentController)} - {nameof(CreatePayment)} - Status Code is {e.StatusCode}");
                return StatusCode((int)e.StatusCode);
            }
        }
    }
}
