namespace PostSharpDemo
{
    public static class CacheFactory
    {
        public static ICache GetCurrentCache(CacheSupport type)
        {
            switch (type)
            {
                case CacheSupport.Test:
                    return new TestCacheImp();

                case CacheSupport.Redis:
                    break;

                default:
                    break;
            }

            return null;
        }
    }

    public enum CacheSupport
    {
        Test = 0,
        Redis
    }
}