using System;
using System.Reflection;

namespace AssetFetcher
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AssetFetcherAttribute : Attribute
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
                    MethodInfo method = type.GetMethod("Process");
                    if (method == null)
                    {
                        AssetFetcherPlugin.log.LogError($"Invalid Fetcher Class {type.FullName}: No valid Constructor was available for {type.Name}! Skipping...");
                        continue;
                    }
                    if (!method.IsStatic)
                    {
                        AssetFetcherPlugin.log.LogError($"Invalid Fetcher Class {type.FullName}: No valid Constructor was available for {type.Name}! Skipping...");
                        continue;
                    }

                    try
                    {
                        method.Invoke(null, null);
                    }
                    catch (TargetInvocationException e)
                    {
                        AssetFetcherPlugin.log.LogError($"Failed when Processing: {type.FullName}: {e.InnerException.Message}\n{e.InnerException.StackTrace}\n\tFetching Has Failed! Skipping...");
                        continue;
                    }
                }
            }
        }
    }
}