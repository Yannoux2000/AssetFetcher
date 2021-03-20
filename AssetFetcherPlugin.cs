using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;

namespace AssetFetcher
{
    [BepInPlugin(Id)]
    public class AssetFetcherPlugin : BasePlugin
    {
        public const string Id = "com.inxs212.AssetFetcherPlugin";
        //public Harmony Harmony { get; } = new Harmony(Id);
        public static ManualLogSource log { get; private set; }
        public override void Load()
        {
            log = base.Log;
        }
    }
}
