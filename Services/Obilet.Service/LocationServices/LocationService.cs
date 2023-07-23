using System.Net.Http.Headers;
using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Shared.Helpers;
using Obilet.Shared.Responses;
using Tradeport.Shared.Common;

namespace Obilet.Service.LocationServices;

public class LocationService : ILocationService
{
    public async Task<BaseResponse<List<GetLocationResponse>>> GetLocations(GetLocationRequest request)
    {
        var getLocationResponse = ApiHelper.PostApiResponse<BaseResponse<List<GetLocationResponse>>, object>(request
            , new ApiModel()
            {
                Url = $"https://v2-api.obilet.com/api/location/getbuslocations",
                AuthenticationHeaderValue = new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1")
            });

        return getLocationResponse.Data;
    }
}