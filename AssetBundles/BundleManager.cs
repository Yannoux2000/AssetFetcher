using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AssetFetcher.Utils;

namespace AssetFetcher.AssetBundles
{
    /// <summary>
    /// Keeps loaded Bundles references for other uses
    /// </summary>
    public class BundleManager : Singleton<BundleManager>
    {
        /// <summary>
        /// References to already loaded Bundles, organized via paths
        /// </summary>
        private Dictionary<string, AssetBundle> Bundles = new Dictionary<string, AssetBundle>();

        /// <summary>
        /// AssetBundle Encaplusate access and loading of assetbundles.
        /// </summary>
        /// <param name="shortPath">Path to the AssetBundle from the game's root</param>
        /// <returns></returns>
        public AssetBundle GetBundle(string shortPath)
        {
            if (!Bundles.ContainsKey(shortPath))
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), shortPath);
                AssetBundle assetBundle = AssetBundle.LoadFromFile(fullPath);
                if (assetBundle != null)
                {
                    //Make sure you are using the same Unity version as the GameAssembly.dll !
                    throw new System.ArgumentException($"External AssetBundle at {shortPath}, was not found/couldn't be loaded properly.");
                }
                Bundles.Add(shortPath, assetBundle);
            }
            return Bundles[shortPath];
        }

        /// <summary>
        /// AssetBundle Encaplusate access and loading of assetbundles.
        /// </summary>
        /// <param name="path">Path to the AssetBundle Inside an embedded assembly</param>
        /// <param name="assembly">Assembly containing the AssetBundle</param>
        /// <returns></returns>
        public AssetBundle GetBundle(string path, Assembly assembly)
        {
            if (!Bundles.ContainsKey(path))
            {
                Assembly a = Assembly.GetExecutingAssembly();
                Stream stream = a.GetManifestResourceStream(path);
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, (int)stream.Length);


                AssetBundle assetBundle = AssetBundle.LoadFromMemory(array);
                if (assetBundle != null)
                {
                    //Make sure you are using the same Unity version as the GameAssembly.dll !
                    throw new System.ArgumentException($"Embedded AssetBundle at {path}, was not found/couldn't be loaded properly.");
                }
                Bundles.Add(path, assetBundle);
            }
            return Bundles[path];
        }
    }
}
