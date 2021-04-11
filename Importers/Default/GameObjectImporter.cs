using System;
using System.Reflection;
using UnityEngine;
using AssetFetcher.AssetBundles;

namespace AssetFetcher.Importers
{
    public class GameObjectImporter : IImporter<GameObject>
    {
        public override GameObject GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path, assembly);
            return bundle.LoadAsset<GameObject>(Name);
        }
        public override GameObject GetExternalAssetBundle(string Path, string Name)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path);
            return bundle.LoadAsset<GameObject>(Name);
        }

        public override GameObject GetEmbeddedNative(string Path, string Name, Assembly asm)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
        public override GameObject GetExternalNative(string Path, string Name)
        {
            //this may never be possible in fact
            //you would be better off reading stream from a text file
            throw new NotImplementedException();
        }
    }
}
