
namespace AssetFetcher.Utils
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
            set
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = value;
                    }
                }
            }
        }
    }
}
