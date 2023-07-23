namespace Obilet.Shared.Extensions;

public class CommonExtension
{
    public static Exception GetInnerException(Exception ex)
    {
        var innerException = ex;
        while (innerException?.InnerException != null)
        {
            innerException = innerException.InnerException;
        }

        return innerException;

    }
}