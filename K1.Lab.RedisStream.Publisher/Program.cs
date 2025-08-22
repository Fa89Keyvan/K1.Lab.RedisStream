using K1.Lab.RedisStream.Publisher.Models;
using K1.Lab.RedisStream.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace K1.Lab.RedisStream.Publisher
{
    public class Program
    {
        private static readonly IList<UserRequest> Requests = new List<UserRequest>();
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.MapPost("/api/v1/request/register", (RequestDto requestDto) =>
            {
                var userRequest = UserRequest.CreateNewRequest(requestDto.UserName, requestDto.RequestText);
                Requests.Add(userRequest);

                var responseDto = ApiResponse.Created(userRequest.Identifier.ToString());
                return Results.Json(responseDto);
            });

            app.MapPut("/api/v1/request/update-state/{traceId:guid}", (Guid traceId, UpdateStateRequestDto requestDto) =>
            {
                var request = Requests.FirstOrDefault(request => request.Identifier == traceId);
                if (request is null)
                {
                    return Results.NotFound();
                }

                request.RequestState = requestDto.NewState;
                return Results.Ok();
            });

            app.UseRouting();
            app.Run();
        }
    }
}
