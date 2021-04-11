using AssetFetcher.Importers;
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;

namespace AssetFetcher
{
    [BepInPlugin(Id, "AssetFetcher", "1.1.0")]
    public class AssetFetcherPlugin : BasePlugin
    {
        public const string Id = "com.inxs212.AssetFetcherPlugin";
        public static ManualLogSource log { get; private set; }
        public override void Load()
        {
            log = base.Log;
            ImporterManager.Instance.AddImportor<UnityEngine.Material, MaterialImporter>();
            ImporterManager.Instance.AddImportor<UnityEngine.Sprite, SpriteImporter>();
            ImporterManager.Instance.AddImportor<UnityEngine.TextAsset, TextAssetImporter>();
            ImporterManager.Instance.AddImportor<UnityEngine.GameObject, GameObjectImporter>();
            ImporterManager.Instance.AddImportor<UnityEngine.AudioClip, AudioClipImporter>();
            ImporterManager.Instance.AddImportor<UnityEngine.AnimationClip, AnimationClipImporter>();
        }
    }
}
