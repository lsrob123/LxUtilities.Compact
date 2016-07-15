using System;

namespace LxUtilities.Compact.Tests._ObjectFactories
{
    public class CachedItem
    {
        public string SomeProperty { get; set; }
    }

    public static class CachedItemFactory
    {
        public static CachedItem Random()
        {
            return new CachedItem
            {
                SomeProperty = Guid.NewGuid().ToString()
            };
        }
    }
}
