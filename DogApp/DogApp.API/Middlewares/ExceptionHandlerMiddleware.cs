using DogApp.API.Models;
using DogApp.BLL.Exceptions;
using System.Net;

namespace DogApp.API.Middlewares;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlerMiddleware(RequestDelegate next)
    {
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (ArgumentException ex)
		{
			await ProcessExpectedException(context, ex);
		}
		catch (DogNameAlreadyExistsException ex)
		{
			await ProcessExpectedException(context, ex);
		}
		catch (Exception)
		{
			await ProcessUnexpectedException(context);
		}
	}

	private static async Task ProcessUnexpectedException(HttpContext context)
	{
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		context.Response.ContentType = "application/json";

		var error = new ErrorResponce
		{
			StatusCode = (int)HttpStatusCode.InternalServerError,
			ErrorMessage = "Something went wrong! We are looking into resolving this."
		};

		await context.Response.WriteAsJsonAsync(error);
	}

	private static async Task ProcessExpectedException(HttpContext context, Exception ex)
	{
		context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
		context.Response.ContentType = "application/json";

		var error = new ErrorResponce
		{
			StatusCode = (int)HttpStatusCode.BadRequest,
			ErrorMessage = ex.Message,
		};

		await context.Response.WriteAsJsonAsync(error);
	}
}
