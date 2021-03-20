using System;
using System.Reflection;
using UnityEngine;
using AssetFetcher.AssetBundles;

namespace AssetFetcher.Importers
{
    [Importer]
    public class MaterialImporter : IImporter<Material>
    {
        public override Material GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path, assembly);
            return bundle.LoadAsset<Material>(Name);
        }
        public override Material GetExternalAssetBundle(string Path, string Name)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path);
            return bundle.LoadAsset<Material>(Name);
        }

        public override Material GetEmbeddedNative(string Path, string Name, Assembly asm)
        {
            throw new NotImplementedException();
        }
        public override Material GetExternalNative(string Path, string Name)
        {
            throw new NotImplementedException();
        }
    }
}
