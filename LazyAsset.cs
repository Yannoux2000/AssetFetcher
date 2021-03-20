using AssetFetcher.Importers;
using System;
using System.Reflection;

namespace AssetFetcher
{
    /// <summary>
    /// Lazy, because it will not only init when needed, but also
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class LazyAsset<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// Describe how the asset is supposed to be imported
        /// </summary>
        public enum AssetSource
        {
            /// <summary>
            /// Copy game's own asset to be reused, throws error if unavailable
            /// </summary>
            VanillaHard,
            /// <summary>
            /// Copy game's own asset to be reused, returns null if unavailable
            /// </summary>
            VanillaSoft,
            /// <summary>
            /// Asset is in an assetbundle, embedded inside the assembly
            /// </summary>
            EmbeddedAssetBundle,
            /// <summary>
            /// Asset is in an assetbundle, accessible from a path
            /// </summary>
            ExternalAssetBundle,
            /// <summary>
            /// Asset is in native format, embedded inside the assembly
            /// </summary>
            EmbeddedNative,
            /// <summary>
            /// Asset is in native format, accessible from a path
            /// </summary>
            ExternalNative
        }

        /// <summary>
        /// path to assetbundle if <see cref="AssetSource"/> is AssetBundle
        /// path to file if <see cref="AssetSource"/> is Embedded
        /// otherwise discared
        /// </summary>
        public readonly string Path = "";

        /// <summary>
        /// Name of the Asset/AssetFile
        /// </summary>
        public readonly string Name = "";

        /// <summary>
        /// The Asset's importation type
        /// </summary>
        public readonly AssetSource Source;

        /// <summary>
        /// The Asset's Type
        /// </summary>
        public readonly Type Type = typeof(T);

        public T Asset { get; private set; }

        public LazyAsset(AssetSource source, string name, string path = "")
        {
            Source = source;
            Name = name;
            Path = path;
        }

        public T Generate(Assembly assembly)
        {
            Asset = Import(assembly);
            return Asset;
        }

        private T Import(Assembly assembly)
        {
            IImporter<T> importer = Importers.Importers.Instance.GetImporter<T>();
            switch (Source)
            {
                case AssetSource.VanillaHard:
                    return importer.GetAsset(Name);
                case AssetSource.VanillaSoft:
                    return importer.GetAssetSoft(Name);
                case AssetSource.EmbeddedAssetBundle:
                    return importer.GetEmbeddedAssetBundle(Path, Name, assembly);
                case AssetSource.ExternalAssetBundle:
                    return importer.GetExternalAssetBundle(Path, Name);
                case AssetSource.EmbeddedNative:
                    return importer.GetEmbeddedNative(Path, Name, assembly);
                case AssetSource.ExternalNative:
                    return importer.GetExternalNative(Path, Name);
            }
            return null;
        }
    }
}
