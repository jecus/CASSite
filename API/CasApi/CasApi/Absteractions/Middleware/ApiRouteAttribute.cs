using Microsoft.AspNetCore.Mvc;

namespace CasApi.Absteractions.Middleware
{
    public class ApiRouteAttribute : RouteAttribute
    {
        public const string CONTROLLER_PLACEHOLDER = "[controller]";
        public const string API_VERSION_PLACEHOLDER = "{version:apiVersion}";

        public ApiRouteAttribute()
            : this(CONTROLLER_PLACEHOLDER)
        {
        }

        public ApiRouteAttribute(string endpoint)
            : base($"v{API_VERSION_PLACEHOLDER}/{endpoint}")
        {
        }
    }
}