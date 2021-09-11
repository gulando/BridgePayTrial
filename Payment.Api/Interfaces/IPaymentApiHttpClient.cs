using System.Threading.Tasks;

namespace Payment.Api.Interfaces
{
    public interface IPaymentApiHttpClient
    {
        Task<T> GetAsync<T>(string uri, string id);

        Task<TOut> PostAsync<TIn, TOut>(string uri, TIn body);
    }
}
