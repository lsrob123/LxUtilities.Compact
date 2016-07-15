using LxUtilities.Compact.Serialization;

namespace LxUtilities.Compact.Tests._ObjectFactories
{
    public static class CacheFactory
    {
        public static Compact.Cache.Redis.Cache Default()
        {
            return new Compact.Cache.Redis.Cache(LoggerFactory.DoNothingLogger(), new JsonSerializer());
        }
    }
}
