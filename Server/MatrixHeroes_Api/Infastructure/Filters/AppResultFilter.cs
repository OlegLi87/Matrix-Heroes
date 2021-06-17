using System;
using MatrixHeroes_Api.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatrixHeroes_Api.Infastructure.Filters
{
    public class AppResultFilterAttribute : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // in case of validation error.
            if (context.Result is BadRequestObjectResult badReqRes)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new ObjectResult(new { (badReqRes.Value as ValidationProblemDetails)?.Errors });
                return;
            }

            if (context.Result is ObjectResult objResult)
            {
                if (objResult.Value is AppResponse appResponse)
                {
                    context.HttpContext.Response.StatusCode = appResponse.StatusCode;
                    if (appResponse.StatusCode == StatusCodes.Status204NoContent)
                        return;

                    if (appResponse.IsSuccess)
                        context.Result = new ObjectResult(appResponse.ResponsePayload.ResponseObj);
                    else context.Result = new ObjectResult(new { appResponse.ResponsePayload.Errors });
                }
                // in case there is another error response.
                else
                {
                    context.HttpContext.Response.StatusCode = (int)objResult.StatusCode;
                    if (objResult.Value is ProblemDetails problems)
                        context.Result = new ObjectResult(new { Errors = problems.Title });
                    else context.Result = new ObjectResult(new { Errors = "Something went wrong." });
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }
    }
}