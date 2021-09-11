using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Payment.Api.Interfaces;
using Payment.Api.Models;

namespace Payment.Api.HttpClient
{
    public class PaymentApiHttpClient : IPaymentApiHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly ILogger<PaymentApiHttpClient> _logger;

        public PaymentApiHttpClient(System.Net.Http.HttpClient httpClient, ILogger<PaymentApiHttpClient> logger)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Mechant-Id", "");
            _httpClient.DefaultRequestHeaders.Add("Secret-Key", "");

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
                    result = JsonConvert.DeserializeObject<T>(stringResult);
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
            catch (Exception exception)
            {
                _logger.LogError($"Stack Trace is {exception.StackTrace}");
                _logger.LogError($"Uri is {uri}");
                _logger.LogError(
                    $"ExceptionMessage: {exception.Message} | Inner Exception: {exception.InnerException?.Message}");

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
                    result = JsonConvert.DeserializeObject<TOut>(stringResult);
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
            catch (Exception exception)
            {
                _logger.LogError($"Stack Trace is {exception.StackTrace}");
                _logger.LogError($"Uri is {uri}");
                _logger.LogError(
                    $"ExceptionMessage: {exception.Message} | Inner Exception: {exception.InnerException?.Message}");

                throw;
            }
        }
    }
}
