using System.Net;

namespace DogApp.API.Middlewares;

public class RateLimitingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly int _limit;
	private static readonly Dictionary<string, (DateTime Start, int Requests)> _clients = new();

	public RateLimitingMiddleware(RequestDelegate next, IConfiguration configuration)
	{
		_next = next;
		_limit = Convert.ToInt32(configuration["RequestLimit"]);
	}

	public async Task Invoke(HttpContext context)
	{
		var clientId = context.Connection.RemoteIpAddress.ToString();

		if (_clients.ContainsKey(clientId))
		{
			var (Start, Requests) = _clients[clientId];

			var dif = (DateTime.Now - Start).TotalSeconds;

			if (dif < 60 && Requests > _limit)
			{
				context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
				return;
			}

			if (dif >= 60)
			{
				_clients[clientId] = (DateTime.Now, 0);
			}
			else
			{
				_clients[clientId] = (Start, Requests + 1);
			}
		}
		else
		{
			_clients.Add(clientId, (DateTime.Now, 1));
		}

		await _next.Invoke(context);
	}
}
