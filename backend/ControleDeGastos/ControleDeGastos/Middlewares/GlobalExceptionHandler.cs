using ControleDeGastos.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Middlewares;

// Centraliza o tratamento de exceções da API
public sealed class GlobalExceptionHandler : IExceptionHandler {
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        var response = exception switch {
            NotFoundException => (StatusCode: StatusCodes.Status404NotFound, Title: "Não encontrado", Detail: exception.Message),
            BusinessRuleException => (StatusCode: StatusCodes.Status400BadRequest, Title: "Regra de negócio violada", Detail: exception.Message),
            _ => (StatusCode: StatusCodes.Status500InternalServerError, Title: "Erro interno", Detail: "Ocorreu um erro inesperado.")
        };

        // Log de erros inesperados
        if (response.StatusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Erro não tratado na requisição {Path}", httpContext.Request.Path);

        var problemDetails = new ProblemDetails {
            Status = response.StatusCode,
            Title = response.Title,
            Detail = response.Detail,
            Type = $"https://httpstatuses.io/{response.StatusCode}",
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}