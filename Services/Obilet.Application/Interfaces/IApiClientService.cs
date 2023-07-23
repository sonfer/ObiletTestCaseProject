using Obilet.Shared.Responses;

namespace Obilet.Application.Interfaces;

public interface IApiClientService<T>
{
    Task<DataResult<T>> GetAsync(string requestUrl);
    Task<DataResult<T>> PostAsync<TRequest>(string requestUrl, TRequest request);
}