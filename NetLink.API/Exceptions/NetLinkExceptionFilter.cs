using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetLink.API.Exceptions;

// ReSharper disable once ClassNeverInstantiated.Global
public class NetLinkExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not NetLinkCustomException netLinkCustomException) return;
        context.Result = context.Exception switch
        {
            DeveloperException or EndUserException or SensorException or SensorGroupException or RecordedValueException => new BadRequestObjectResult(new
            {
                netLinkCustomException.Message
            }),
            NotFoundException => new NotFoundObjectResult(new { netLinkCustomException.Message }),
            _ => context.Result
        };

        context.ExceptionHandled = true;
    }
}