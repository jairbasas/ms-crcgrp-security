using Newtonsoft.Json;
using System.Text;

namespace Security.Services.Helper
{
    public class BaseService
    {
        readonly HttpClient _httpClient;

        public BaseService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<T> CallServiceList<T>(string url)
        {
            var uri = $"{_httpClient.BaseAddress.AbsoluteUri}{url}";
            var result = await _httpClient.GetAsync(uri);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
                return default(T);
        }

        public async Task<T> CallServiceEntity<T>(string url)
        {
            var uri = $"{_httpClient.BaseAddress.AbsoluteUri}{url}";
            var result = await _httpClient.GetAsync(uri);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
                return default(T);
        }
        public async Task<T> PostServiceEntity<T>(string url, string postJson)
        {
            var uri = $"{_httpClient.BaseAddress.AbsoluteUri}{url}";
            var result = await _httpClient.PostAsync(uri, new StringContent(postJson, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
                return default(T);
        }
    }
}
