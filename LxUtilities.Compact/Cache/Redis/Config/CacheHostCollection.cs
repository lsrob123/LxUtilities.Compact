using System.Configuration;

namespace LxUtilities.Compact.Cache.Redis.Config
{
    public class CacheHostCollection : ConfigurationElementCollection
    {
        public CacheHost this[int index]
        {
            get
            {
                return BaseGet(index) as CacheHost;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheHost();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheHost)element).Host;
        }
    }
}
