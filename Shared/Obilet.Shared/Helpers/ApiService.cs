using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Obilet.Shared.Common;
using Tradeport.Shared.Common;

namespace Obilet.Shared.Helpers
{
    public class ApiService : IApiService
    {
        public Response<TResponse> GetServiceUrl<TResponse>(ApiModel apiModel)
        {
            return ApiHelper.GetApiResponse<TResponse>(apiModel);
        }

        public Response<TResponse> PostServiceUrl<TResponse, TRequest>(TRequest request, ApiModel apiModel)
        {
            return ApiHelper.PostApiResponse<TResponse, TRequest>(request, apiModel);
        }
        
    }
}
