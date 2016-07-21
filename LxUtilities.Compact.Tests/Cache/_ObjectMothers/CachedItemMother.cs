using System;

namespace LxUtilities.Compact.Tests.Cache._ObjectMothers
{
    public class CachedItem
    {
        public string SomeProperty { get; set; }
    }

    public static class CachedItemMother
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
