using Obilet.Shared.Responses;

namespace Obilet.Shared.Interfaces;

public interface IDataResult<T>: IResult
{
    T Data { get; set; }
    
}