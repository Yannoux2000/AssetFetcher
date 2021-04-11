using System;
using System.Reflection;
using UnityEngine;
using AssetFetcher.AssetBundles;

namespace AssetFetcher.Importers
{
    public class TextAssetImporter : IImporter<TextAsset>
    {
        public override TextAsset GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path, assembly);
            return bundle.LoadAsset<TextAsset>(Name);
        }
        public override TextAsset GetExternalAssetBundle(string Path, string Name)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path);
            return bundle.LoadAsset<TextAsset>(Name);
        }

        public override TextAsset GetEmbeddedNative(string Path, string Name, Assembly asm)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
        public override TextAsset GetExternalNative(string Path, string Name)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
    }
}
