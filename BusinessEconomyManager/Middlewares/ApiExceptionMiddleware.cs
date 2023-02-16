using AutoMapper;
using BusinessEconomyManager.Models.Exceptions;
using Newtonsoft.Json;

namespace BusinessEconomyManager.Middlewares
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly IMapper _mapper;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger, IMapper mapper)
        {
            _next = next;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";

                ApiExceptionDto apiExceptionDto = ex switch
                {
                    ApiException apiEx => _mapper.Map<ApiExceptionDto>(apiEx),
                    _ => new()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        ErrorMessage = ex.Message,
                    },
                };

                httpContext.Response.StatusCode = apiExceptionDto.StatusCode;
                string json = JsonConvert.SerializeObject(apiExceptionDto);
                await httpContext.Response.WriteAsync(json);
            }
        }

    }
}
