using HttpClientConsumeApi.Interface;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;

namespace HttpClientConsumeApi.Service
{
    public class HttpCallService : IHttpCallService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpCallService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> GetData<T>()
        {
            T data = default(T);

            var httpRequestMesage = new HttpRequestMessage(HttpMethod.Get, "https://dogapi.dog/api/v2/breeds")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json"
                    },
                    { HeaderNames.UserAgent, "HttpRequestSample"
                    }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.SendAsync(httpRequestMesage);
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
            }
            else
            {
                Debug.WriteLine("Failure");
            }
            return data;
        }
    }
}
