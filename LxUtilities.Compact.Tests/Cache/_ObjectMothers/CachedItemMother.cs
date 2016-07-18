using System;

namespace LxUtilities.Compact.Tests.Cache._ObjectMothers
{
    internal class CachedItem
    {
        public string SomeProperty { get; set; }
    }

    internal static class CachedItemMother
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
