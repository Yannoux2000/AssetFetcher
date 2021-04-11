using AssetFetcher.Importers;
using System;
using System.Reflection;
using UnityEngine;

namespace AssetFetcher
{
    /// <summary>
    /// Describe how the asset is supposed to be imported
    /// </summary>
    public enum AssetSource
    {
        /// <summary>
        /// Just an AssetHandle, for methods that require LazyAssetTypes
        /// </summary>
        None,
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
        ExternalNative,
        /// <summary>
        /// Asset is in custom format, embedded inside the assembly
        /// </summary>
        EmbeddedCustom,
        /// <summary>
        /// Asset is in custom format, accessible from a path
        /// </summary>
        ExternalCustom
    }

    /// <summary>
    /// Lazy, because it will not only init when needed, but also
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LazyAsset<T> where T : UnityEngine.Object
    {
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
        /// In case you want to import new types of data a way that is not available otherwise.
        /// </summary>
        public readonly ICustomImporter CustomImporter;

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

        public LazyAsset(T asset = null)
        {
            Source = AssetSource.None;
            Asset = asset;
        }

        public LazyAsset(ICustomImporter importer, string name, string path = "", bool embedded = true)
        {
            Source = (embedded ? AssetSource.EmbeddedCustom : AssetSource.ExternalCustom);
            CustomImporter = importer;
            Name = name;
            Path = path;
        }

        public T Load(Assembly assembly = null)
        {
            IImporter<T> importer = Importers.ImporterManager.Instance.GetImporter<T>();
            if (importer == null) throw new Exception($"WTF Attend ta pas d'importer pour le type {typeof(T).Name}");
            object o;
            switch (Source)
            {
                case AssetSource.None:
                    return Asset;

                case AssetSource.VanillaHard:
                    return importer.GetAssetHard(Name);
                case AssetSource.VanillaSoft:
                    return importer.GetAssetSoft(Name);

                case AssetSource.EmbeddedAssetBundle:
                    if (assembly == null) throw new ArgumentNullException($"Assembly is null when LazyAsset is set as EmbeddedAssetBundle.");
                    return importer.GetEmbeddedAssetBundle(Path, Name, assembly);
                case AssetSource.ExternalAssetBundle:
                    return importer.GetExternalAssetBundle(Path, Name);

                case AssetSource.EmbeddedNative:
                    if (assembly == null) throw new ArgumentNullException($"Assembly is null when LazyAsset is set as EmbeddedNative.");
                    return importer.GetEmbeddedNative(Path, Name, assembly);
                case AssetSource.ExternalNative:
                    return importer.GetExternalNative(Path, Name);

                case AssetSource.EmbeddedCustom:
                    if (assembly == null) throw new ArgumentNullException($"Assembly is null when LazyAsset is set as EmbeddedCustom.");
                    if (CustomImporter == null) throw new ArgumentNullException($"CustomImporter is null when LazyAsset is set as EmbeddedCustom.");
                    o = CustomImporter.GetEmbedded(Path, Name, assembly);
                    if (o is T) return (T)o;
                    break;
                case AssetSource.ExternalCustom:
                    if (CustomImporter == null) throw new ArgumentNullException($"CustomImporter is null when LazyAsset is set as EmbeddedCustom.");
                    o = CustomImporter.GetExternal(Path, Name);
                    if (o is T) return (T)o;
                    AssetFetcherPlugin.log.LogError($"Asset returned from CustomImporter isn't of type {typeof(T).Name}. LazyAsset: ");
                    break;
            }
            return null;
        }

        public static implicit operator T(LazyAsset<T> lazyAsset)
        {
            if (lazyAsset.Asset) lazyAsset.Load();
            return lazyAsset.Asset;
        }
        public static implicit operator LazyAsset<T>(T asset)
        {
            return new LazyAsset<T>(asset);
        }
    }
}
