using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Games.Api
{
    public class HandleExceptionsMiddleware : IMiddleware
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly ILogger _logger;

        public HandleExceptionsMiddleware(JsonSerializerSettings serializerSettings, ILogger<HandleExceptionsMiddleware> logger)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serializerSettings.Converters.Add(new ProblemDetailsConverter());
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);

                var statusCode = GetStatusCode(ex);
                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Type = $"https://httpstatuses.com/{statusCode}",
                    Title = ReasonPhrases.GetReasonPhrase(statusCode),
                };

                if (ex is Core.Exceptions.ValidationException validationException)
                {
                    problemDetails.Extensions.Add("ErrorDetail", validationException.Error);
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, _serializerSettings));
            }
        }

        private int GetStatusCode(Exception ex) =>
            ex switch
            {
                Core.Exceptions.ValidationException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
