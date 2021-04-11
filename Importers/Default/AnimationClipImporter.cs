using System;
using System.Reflection;
using UnityEngine;
using AssetFetcher.AssetBundles;

namespace AssetFetcher.Importers
{
    public class AnimationClipImporter : IImporter<AnimationClip>
    {
        public override AnimationClip GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path, assembly);
            return bundle.LoadAsset<AnimationClip>(Name);
        }
        public override AnimationClip GetExternalAssetBundle(string Path, string Name)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path);
            return bundle.LoadAsset<AnimationClip>(Name);
        }

        public override AnimationClip GetEmbeddedNative(string Path, string Name, Assembly asm)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
        public override AnimationClip GetExternalNative(string Path, string Name)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
    }
}
