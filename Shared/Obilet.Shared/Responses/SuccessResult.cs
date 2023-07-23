namespace Obilet.Shared.Responses;

public class SuccessResult : Result
{
    public SuccessResult() : base(true)
    {

    }

    public SuccessResult(string message, string status) : base(true, message, status)
    {

    }
}