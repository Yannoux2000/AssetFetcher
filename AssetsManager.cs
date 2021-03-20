using AssetFetcher.Utils;
using System.Collections.Generic;

namespace AssetFetcher
{
    class AssetsManager : Singleton<AssetsManager>
    {
        private Dictionary<string, object> Ress = new Dictionary<string, object>();

        public static void Register<T>(string key, T obj)
        {
            AssetsManager.Instance.Register(key, obj);
        }
        public static T AssignHard<T>(string key)
        {
            T asset;
            if (!AssetsManager.Instance.Get(key, out asset))
            {
                throw new System.Exception($"Couldn't Retrieve Material:{key} from {typeof(AssetsManager).Name}, Report this error to the mod developper.");
            }
            return asset;
        }
        public static T AssignSoft<T>(string key)
        {
            T asset;
            if (!AssetsManager.Instance.Get(key, out asset))
            {
                throw new System.Exception($"Couldn't Retrieve Material:{key} from {typeof(AssetsManager).Name}, Report this error to the mod developper.");
            }
            return asset;
        }

        public void Register(string key, object obj)
        {
            Ress[key] = obj;
        }
        public bool Get<T>(string key, out T ext)
        {
            object obj = null;
            bool flag = Ress.TryGetValue(key, out obj);
            if (!flag)
            {
                ext = default(T);
                return false;
            }
            ext = (T)obj;
            return flag;
        }

        public bool IsRegistered(string key)
        {
            return Ress.ContainsKey(key);
        }
        public bool IsRegistered<T>(string key)
        {
            object obj = null;
            bool flag = Ress.TryGetValue(key, out obj);
            return flag;
        }
    }
}
