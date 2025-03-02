namespace UserService.API.Middleware
{
    public class GatewayMiddleware
    {
        private readonly RequestDelegate _next;

        public GatewayMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var gatewayIp = "::1";//localhost

            var remoteIp = context.Connection.RemoteIpAddress?.ToString();

            if (remoteIp != gatewayIp)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied. Only API Gateway requests are allowed.");
                return;
            }

            await _next(context);
        }
    }
}
