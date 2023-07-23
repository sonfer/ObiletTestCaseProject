namespace Obilet.Shared.Responses;

public interface IResult
{
    bool Success { get; set; }
    string Status { get; set; }
    string Message { get; set; }
}