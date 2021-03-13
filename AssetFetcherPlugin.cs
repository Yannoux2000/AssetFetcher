using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;

namespace AssetFetcher
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    public class AssetFetcherPlugin : BasePlugin
    {
        public const string Id = "com.inxs212.AssetFetcherPlugin";
        public Harmony Harmony { get; } = new Harmony(Id);
        public override void Load()
        {

            Harmony.PatchAll();
        }
    }
}
