using Microsoft.AspNetCore.Http;
using Serilog;
using System.IO;
using System.Threading.Tasks;

namespace Fletchling.Api.Logging
{
    public static class SeriLogEnrichment
    {
        public static async Task EnrichWithRequestDetails(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
            diagnosticContext.Set("RequestBody", await ReadyBodyFromRequest(request));
        }

        private static async Task<string> ReadyBodyFromRequest(HttpRequest request)
        {
            request.Body.Position = 0;

            using var streamReader = new StreamReader(request.Body);
            var requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;
            return requestBody;
        }
    }
}