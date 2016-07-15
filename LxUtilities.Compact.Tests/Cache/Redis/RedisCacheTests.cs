using System;
using System.Collections.Generic;
using System.Linq;
using LxUtilities.Compact.Tests._ObjectFactories;
using NUnit.Framework;

namespace LxUtilities.Compact.Tests.Cache.Redis
{
    [TestFixture]
    public class RedisCacheTests
    {
        [Test]
        public void SingleValueSetGet()
        {
            var cacheKey = Guid.NewGuid().ToString();
            var cachedItem = CachedItemFactory.Random();
            using (var cache = CacheFactory.Default())
            {
                cache.SetCachedItem(cacheKey, cachedItem, TimeSpan.FromSeconds(10));
            }

            using (var cache2 = CacheFactory.Default())
            {
                var cachedImage= cache2.GetCachedItem<CachedItem>(cacheKey);
                Assert.IsNotNull(cachedImage);
                Assert.AreEqual(cachedItem.SomeProperty, cachedImage.SomeProperty);
            }


        }

        [Test]
        public void HashSetGet()
        {
            var hashKey = Guid.NewGuid().ToString();
            var fieldName = Guid.NewGuid().ToString();
            var fieldValue = Guid.NewGuid().ToString();

            using (var cache = CacheFactory.Default())
            {
                cache.HashSet(hashKey,
                    new Dictionary<string, string>
                    {
                        {fieldName, fieldValue}
                    });
            }

            using (var cache2 = CacheFactory.Default())
            {
                var cachedFields = cache2.HashGet(hashKey, fieldName);
                Assert.IsNotNull(cachedFields);
                Assert.IsTrue(cachedFields.Any());
                Assert.AreEqual(fieldValue, cachedFields.First());
            }

        }
    }
}