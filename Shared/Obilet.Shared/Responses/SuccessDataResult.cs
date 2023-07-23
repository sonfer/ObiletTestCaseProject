namespace Obilet.Shared.Responses;

public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data) : base(true, data)
    {

    }

    public SuccessDataResult(T data, long totalCount) : base(true, data)
    {

    }

    public SuccessDataResult(string message, string status, T data) : base(true, message,status, data)
    {

    }

    public SuccessDataResult(string message,string status, T data, long totalCount) : base(true, message, status, data)
    {

    }

    public SuccessDataResult(T data, long totalCount, int page, int pageSize, int totalPage) : base(true, data)
    {

    }

    public SuccessDataResult(string message, string status,T data, long totalCount, int page, int pageSize, int totalPage) : base(true, message, status, data)
    {

    }
}