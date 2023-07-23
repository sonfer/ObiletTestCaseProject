using Obilet.Data.Requestes;
using Obilet.Data.Responses;

namespace Obilet.Service.JourneyServices;

public interface IJourneyService
{
    Task<BaseResponse<List<GetJourneysResponse>>> GetLocations(GetJourneyRequest request);
}