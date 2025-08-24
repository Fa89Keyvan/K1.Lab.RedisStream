using K1.Lab.RedisStream.Shared.Models;

namespace K1.Lab.RedisStream.Publisher.Redis.Repository;

public interface IRequestStateChangedRepository
{
    Task AddAsync(UserRequest userRequest);
}
