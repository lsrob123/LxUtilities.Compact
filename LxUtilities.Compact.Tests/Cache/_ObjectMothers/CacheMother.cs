using LxUtilities.Compact.Serialization;

namespace LxUtilities.Compact.Tests.Cache._ObjectMothers
{
    public static class CacheMother
    {
        public static Compact.Cache.Redis.Cache Default()
        {
            return new Compact.Cache.Redis.Cache(LoggerMother.DoNothingLogger(), new JsonSerializer());
        }
    }
}
