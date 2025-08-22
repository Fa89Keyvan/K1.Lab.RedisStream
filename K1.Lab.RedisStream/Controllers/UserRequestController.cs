using K1.Lab.RedisStream.Shared.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace K1.Lab.RedisStream.Consumer.Controllers
{
    public sealed class UserRequestController : Controller
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RegisterRequest(RequestDto requestDto)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:9000"),
            };

            var httpResponse = await httpClient.PostAsync("/api/v1/request/register",
                new StringContent(JsonSerializer.Serialize(requestDto, JsonSerializerOptions),
                    Encoding.UTF8, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var responseDtoInJson = await httpResponse.Content.ReadAsStringAsync();
            var responseDto = JsonSerializer.Deserialize<ApiResponse>(responseDtoInJson, JsonSerializerOptions);

            return Json(responseDto, JsonRequestBehavior.AllowGet);
        }
    }
}