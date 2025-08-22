using System.Net;

namespace K1.Lab.RedisStream.Shared.Models
{
    public sealed class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string TraceId { get; set; }

        public static ApiResponse Created(string traceId) => new ApiResponse
        {
            StatusCode = HttpStatusCode.Created,
            TraceId = traceId
        };
    }
}