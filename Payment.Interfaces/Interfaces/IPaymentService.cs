using System.Threading.Tasks;
using Payment.Interfaces.Models.Request;
using Payment.Interfaces.Models.Response;

namespace Payment.Interfaces.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentStatusResponse> GetPaymentStatusAsync(string id);

        Task<CreatePaymentResponse> CreatePaymentAsync(CreatePaymentRequest request);

        Task<PaymentStatusResponse> ConfirmPaymentAsync(ConfirmPaymentRequest request);
    }
}
