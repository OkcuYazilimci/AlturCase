using AlturCase.Core.Interfaces;

namespace AlturCase.Application.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;

        public JwtMiddleware(RequestDelegate next, IJwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Validate the token and extract the userId
                var userId = _jwtService.ValidateTokenAndGetUserId(token);
                if (userId != null)
                {
                    // Add userId to HttpContext.Items for use in the controller
                    context.Items["UserId"] = userId;
                }
                else
                {
                    // Handle invalid token case if needed
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid token");
                    return;
                }
            }

            await _next(context);
        }
    }

}
