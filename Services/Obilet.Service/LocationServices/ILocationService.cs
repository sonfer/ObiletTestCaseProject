using Obilet.Data.Requestes;
using Obilet.Data.Responses;
using Obilet.Shared.Responses;

namespace Obilet.Service.LocationServices;

public interface ILocationService
{
    Task<BaseResponse<List<GetLocationResponse>>> GetLocations(GetLocationRequest request);
}