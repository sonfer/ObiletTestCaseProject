using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Shared.Responses;

namespace Obilet.Service.SessionServices;

public interface ISessionService
{
    Task<DataResult<GetSessionResponse>> GetSession(GetSessionRequest request);
}