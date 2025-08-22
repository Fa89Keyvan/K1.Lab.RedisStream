using K1.Lab.RedisStream.Shared.Models;

namespace K1.Lab.RedisStream.Publisher.Models;

public sealed class UpdateStateRequestDto
{
    public RequestState NewState { get; set; }
}
