using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProgPOE.Middleware
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();

            // Allow access to login page, home page, and static files for everyone
            if (path.Contains("/account/login") ||
                path.Contains("/home") ||
                path.StartsWith("/css") ||
                path.StartsWith("/js") ||
                path.StartsWith("/lib") ||
                path.StartsWith("/images"))
            {
                await _next(context);
                return;
            }

            // Check if user is authenticated
            bool isAuthenticated = context.Session.GetInt32("UserId").HasValue;
            if (!isAuthenticated)
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            // Get the user's role
            string userRole = context.Session.GetString("Role");

            // Enforce role-based authorization
            if (path.Contains("/farmer/") && userRole != "Farmer")
            {
                context.Response.Redirect("/Account/Login"); // Redirect to login if not a Farmer
                return;
            }

            if (path.Contains("/employee/") && userRole != "Employee")
            {
                context.Response.Redirect("/Account/Login"); // Redirect to login if not an Employee
                return;
            }

            await _next(context);
        }
    }

    // Extension method to add the middleware to the request pipeline
    public static class RoleAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseRoleAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoleAuthorizationMiddleware>();
        }
    }
}