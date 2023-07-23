using System.Net.Http.Headers;
using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Shared.Helpers;
using Obilet.Shared.Responses;
using Tradeport.Shared.Common;

namespace Obilet.Service.SessionServices;

public class SessionService : ISessionService
{
    public async Task<DataResult<GetSessionResponse>> GetSession(GetSessionRequest request)
    {
        var getSessionResponse = ApiHelper.PostApiResponse<DataResult<GetSessionResponse>, object>(new
        {
            request.Type,
            request.Connection,
            request.Browser
        }, new ApiModel()
        {
            Url = $"https://v2-api.obilet.com/api/client/getsession",
            AuthenticationHeaderValue = new AuthenticationHeaderValue("Basic", "JEcYcEMyantZV095WVc3G2JtVjNZbWx1")
        });

        return getSessionResponse.Data;
    }
}