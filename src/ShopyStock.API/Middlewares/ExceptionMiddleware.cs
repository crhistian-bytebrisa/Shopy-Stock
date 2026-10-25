using System.Net;
using System.Text.Json;
using ShopyStock.Application.DTOs.Common;
using ShopyStock.Domain.Exceptions; // Asegúrate de incluir tu namespace de excepciones

namespace ShopyStock.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (StockException e)
            {
                _logger.LogError(e, "Ocurrió un error de stock en los pedidos: {Message}", e.Message);

                var errors = new List<string>();
                foreach (var product in e.RejectedProducts)
                {
                    errors.Add($"No contamos con suficiente stock para el producto: {product.Name}.");
                }

                await HandleExceptionAsync(context, e, errors);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogWarning(e, "Recurso no encontrado: {Message}", e.Message);
                await HandleExceptionAsync(context, e, new List<string> { e.Message });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocurrió un error no controlado: {Message}", e.Message);

                var errors = new List<string>();
                //Para mostrar solo en pruebas
#if DEBUG
                errors.Add(e.Message);
#endif

                await HandleExceptionAsync(context, e, errors);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, List<string> errors)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Path = context.Request.Path,
                Errors = errors ?? new List<string>()
            };

            switch (exception)
            {
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = exception.Message;
                    break;

                case StockException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Ocurrió un error interno en el servidor.";
                    break;
            }

            context.Response.StatusCode = response.StatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonResponse = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}