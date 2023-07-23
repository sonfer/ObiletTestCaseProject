namespace Obilet.Shared.Responses;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data) : base(false, data)
    {
    }

    public ErrorDataResult(T data, int count) : base(false, data)
    {
    }

    public ErrorDataResult(string message, string status, T data) : base(false, message, status, data)
    {
    }
}