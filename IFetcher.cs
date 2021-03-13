using System;
using System.Reflection;

namespace AssetFetcher
{
    interface IAssetFetcher
    {
        public void Process();
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    class AssetFetcherAttribute : Attribute
    {

        private readonly byte when;

        public AssetFetcherAttribute(byte when = 0)
        {
            this.when = when;
        }

        public static void FetchLoad(Assembly asm)
        {
            Fetch(0, asm);
        }

        public static void Fetch(byte when, Assembly asm)
        {
            //inXSAPIPlugin.log.LogDebug("Fetching...");
            foreach (Type type in asm.GetTypes())
            {
                var attrib = type.GetCustomAttribute<AssetFetcherAttribute>();
                if (attrib == null) continue;
                if (attrib.when == when)
                {
                    if (!typeof(IAssetFetcher).IsAssignableFrom(type))
                    {
                        AssetFetcherPlugin.log.LogError($"Type {type.FullName} has {nameof(AssetFetcherAttribute)} but doesn't implement {nameof(IAssetFetcher)}.");
                        continue;
                    }

                    MethodInfo method = type.GetMethod("Process");
                    object instance = Activator.CreateInstance(type);
                    if (instance == null)
                    {
                        AssetFetcherPlugin.log.LogError($"Invalid Fetcher Class {type.FullName}: No valid Constructor was available for {type.Name}! Skipping...");
                        continue;
                    }

                    try
                    {
                        method.Invoke(instance, null);
                    }
                    catch (System.Exception e)
                    {
                        AssetFetcherPlugin.log.LogError($"Failed when Processing: {type.FullName}: {e.Message}\n{e.StackTrace}\n\tFetching Has Failed! Skipping...");
                        continue;
                    }
                }
            }
        }
    }
}
