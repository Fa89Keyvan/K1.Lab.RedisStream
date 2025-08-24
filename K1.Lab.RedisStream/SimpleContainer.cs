using StackExchange.Redis;

namespace K1.Lab.RedisStream.Consumer.Controllers
{
    public static class SimpleContainer
    {
        public static IDatabase RedisDb { get; set; }
    }
}
