using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment.Api.Interfaces;
using Payment.Api.Models;

namespace Payment.Api.HttpClient
{
    public class PaymentApiHttpClient : IPaymentApiHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly ILogger<PaymentApiHttpClient> _logger;
        private readonly IOptions<PaymentApiSettings> _paymentOptions;

        public PaymentApiHttpClient(System.Net.Http.HttpClient httpClient, ILogger<PaymentApiHttpClient> logger,
            IOptions<PaymentApiSettings> paymentOptions)
        {
            _httpClient = httpClient;
            _paymentOptions = paymentOptions;

            var paymentSettings = _paymentOptions.Value;

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Mechant-Id", paymentSettings.MerchantId);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Secret-Key", paymentSettings.SecretKey);

            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string uri, string id)
        {
            try
            {
                var result = default(T);
                var finalUri = uri.Replace("{id}", id);
                var response = await _httpClient.GetAsync(finalUri);
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(JObject.Parse(stringResult)["result"].ToString());
                }
                else
                {
                    if (response.Content != null)
                    {
                        var responseMessage = await response.Content.ReadAsStringAsync();
                        throw new PaymentException(response.StatusCode, responseMessage);
                    }
                }

                return result;
            }
            catch (PaymentException exception)
            {
                _logger.LogError($"Stack Trace is {exception.StackTrace}");
                _logger.LogError($"Uri is {uri}");
                _logger.LogError($"Status code is: {exception.StatusCode}");

                throw;
            }
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string uri, TIn body)
        {
            try
            {
                var result = default(TOut);
                var response = await _httpClient.PostAsync(uri,
                    new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TOut>(JObject.Parse(stringResult)["result"].ToString());
                }
                else
                {
                    if (response.Content != null)
                    {
                        var responseMessage = await response.Content.ReadAsStringAsync();
                        throw new PaymentException(response.StatusCode, responseMessage);
                    }
                }

                return result;
            }
            catch (PaymentException exception)
            {
                _logger.LogError($"Stack Trace is {exception.StackTrace}");
                _logger.LogError($"Uri is {uri}");
                _logger.LogError($"Status code is: {exception.StatusCode}");

                throw;
            }
        }
    }
}
