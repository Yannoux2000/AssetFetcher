using System;
using System.Reflection;
using UnityEngine;
using AssetFetcher.AssetBundles;

namespace AssetFetcher.Importers
{
    [Importer]
    public class SpriteImporter : IImporter<Sprite>
    {
        public override Sprite GetEmbeddedAssetBundle(string Path, string Name, Assembly assembly)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path, assembly);
            return bundle.LoadAsset<Sprite>(Name);
        }
        public override Sprite GetExternalAssetBundle(string Path, string Name)
        {
            AssetBundle bundle = BundleManager.Instance.GetBundle(Path);
            return bundle.LoadAsset<Sprite>(Name);
        }

        public override Sprite GetEmbeddedNative(string Path, string Name, Assembly asm)
        {
            throw new NotImplementedException();
            //Texture2D tex = new Texture2D(0, 0, TextureFormat.RGBA32, Texture.GenerateAllMips, false, IntPtr.Zero);
            //return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
        }
        public override Sprite GetExternalNative(string Path, string Name)
        {
            throw new NotImplementedException();
        }
    }
}
