using System;
using System.Reflection;

namespace AssetFetcher.Importers
{
    internal interface ImporterHandler { 
        public Type Type { get; }
    };
    public abstract class IImporter<T> : ImporterHandler
    {
        public Type Type { get => typeof(T); }
        public virtual T GetAsset(string Name) => AssetsManager.AssignHard<T>(Name);
        public virtual T GetAssetSoft(string Name) => AssetsManager.AssignSoft<T>(Name);

        public abstract T GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly);
        public abstract T GetExternalAssetBundle(string Path, string Name);

        public abstract T GetEmbeddedNative(string Path, string Name, Assembly asm);
        public abstract T GetExternalNative(string Path, string Name);
    }
}
