using StackExchange.Redis;

namespace K1.Lab.RedisStream.Publisher.Redis.DependencyInjection;

public static class RedisDependencyInjection
{
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        var muxer = ConnectionMultiplexer.Connect("localhost");
        var redisDataBase = muxer.GetDatabase();

        services.AddSingleton(redisDataBase);

        return services;
    }
}
