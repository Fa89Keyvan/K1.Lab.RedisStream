using StackExchange.Redis;

namespace K1.Lab.RedisStream.Consumer.App_Start
{
    public static class RedisConfig
    {
        public static IDatabase InitDatabase()
        {
            var muxer = ConnectionMultiplexer.Connect("localhost");
            return muxer.GetDatabase();
        }
    }
}