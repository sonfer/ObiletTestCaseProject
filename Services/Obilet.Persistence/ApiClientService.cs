using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Obilet.Application.Interfaces;
using Obilet.Shared.Responses;

namespace Obilet.Persistence;

public class ApiClientService<T>: IApiClientService<T>
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiClientService(string baseUrl)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _baseUrl = baseUrl;
    }

    public async Task<DataResult<T>> GetAsync(string requestUrl)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{requestUrl}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Api request failed!");
        }

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(content);
        return new DataResult<T>(true, result);
    }

    public async Task<DataResult<T>> PostAsync<TRequest>(string requestUrl, TRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/{requestUrl}", httpContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Api request failed");
        }

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(content);

        return new DataResult<T>(true, result);

    }
}