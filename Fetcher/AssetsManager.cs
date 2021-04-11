using AssetFetcher.Utils;
using System.Collections.Generic;

namespace AssetFetcher
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        
        private Dictionary<string, object> Ress = new Dictionary<string, object>();
        public AssetsManager() { }
        public static void Register<T>(string key, T obj)
        {
            Instance.Register(key, obj);
            AssetFetcherPlugin.log.LogDebug($"Asset {key} got register with {obj}:{typeof(T).Name}");
        }
        public static T AssignHard<T>(string key)
        {
            T asset;
            if (!Instance.Get(key, out asset))
            {
                throw new System.Exception($"Couldn't Retrieve Asset: {key}({typeof(T).Name}) from {typeof(AssetsManager).Name}, Report this error to the mod developper.");
            }
            AssetFetcherPlugin.log.LogDebug($"retrieving [{key}({typeof(T).Name}) : {asset}]");
            if(asset == null)
            {
                AssetFetcherPlugin.log.LogWarning($"Asset {key}({typeof(T).Name} is null");
            }
            return asset;
        }
        public static T AssignSoft<T>(string key)
        {
            T asset;
            if (!Instance.Get(key, out asset))
            {
                AssetFetcherPlugin.log.LogWarning($"Asset {key}({typeof(T).Name}) has not being fetched.");
            }
            return asset;
        }

        public void Register(string key, object obj)
        {
            if (obj == null)
            {
                AssetFetcherPlugin.log.LogWarning($"Asset {key} being registered is null");
            }
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
