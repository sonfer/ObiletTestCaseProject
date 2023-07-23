using System.Net.Http.Headers;
using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Shared.Helpers;
using Tradeport.Shared.Common;

namespace Obilet.Service.JourneyServices;

public class JourneyService: IJourneyService
{
    public async Task<BaseResponse<List<GetJourneysResponse>>> GetLocations(GetJourneyRequest request)
    {
        var getLocationResponse = ApiHelper.PostApiResponse<BaseResponse<List<GetJourneysResponse>>, object>(request
            , new ApiModel()
            {
                Url = $"https://v2-api.obilet.com/api/journey/getbusjourneys",
                AuthenticationHeaderValue = new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1")
            });

        return getLocationResponse.Data;
    }
}