using Obilet.Shared.Common;
using Tradeport.Shared.Common;

namespace Obilet.Shared.Helpers
{
    public interface IApiService
    {
        Response<TResponse> GetServiceUrl<TResponse>(ApiModel apiModel);
        Response<TResponse> PostServiceUrl<TResponse, TRequest>(TRequest request, ApiModel apiModel);
    }
}
