using System;
using System.Reflection;
using UnityEngine;
using AssetFetcher.AssetBundles;

namespace AssetFetcher.Importers
{
    public class AudioClipImporter : IImporter<AudioClip>
    {
        public override AudioClip GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path, assembly);
            return bundle.LoadAsset<AudioClip>(Name);
        }
        public override AudioClip GetExternalAssetBundle(string Path, string Name)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path);
            return bundle.LoadAsset<AudioClip>(Name);
        }

        public override AudioClip GetEmbeddedNative(string Path, string Name, Assembly asm)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
        public override AudioClip GetExternalNative(string Path, string Name)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
    }
}
