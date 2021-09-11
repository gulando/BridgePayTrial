using System.Threading.Tasks;

namespace Payment.Api.Interfaces
{
    public interface IPaymentApiHttpClient
    {
        Task<T> GetAsync<T>(string uri);

        Task PostAsync<T>(string uri, T body);
    }
}
