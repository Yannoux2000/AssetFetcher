using System;
using UnityEngine;

namespace AssetFetcher.__Exemples
{
    /// <summary>
    /// Declaration of FetchTimings
    /// Thoses enums are translated into bytes, but blunt bytes can do the trick if you have a better way to deal with them
    /// Recommanded way is like here
    /// </summary>
    enum FetchTimings : byte
    {
        onMainMenu = 1,
        onHUDStart = 2,
    }

    public static class Ressources
    {
        //this is the way you declare LazyAssets that are extracted from the original game at runtime
        public static readonly LazyAsset<Sprite> Button = new LazyAsset<Sprite>(AssetSource.VanillaHard, "Button.sprite");

        //VanillaHard = will throw error if cannot access the asset, VanillaSoft will only return null
        public static readonly LazyAsset<Sprite> SoftButton = new LazyAsset<Sprite>(AssetSource.VanillaSoft, "Button.sprite");
    }
    public static class MainMenuButtonFetcher
    {
        /// <summary>
        /// Searches for ExitGameButton inside the same scene as the expected <see cref="AssetFetcherAttribute.Fetch"/> will be called from.
        /// </summary>
        public static GameObject ExitButton
        {
            get => GameObject.Find("ExitGameButton");
        }

        /// <summary>
        /// Fetches the button's sprite (which in this case is a 9 sliced sprite).
        /// </summary>
        [AssetFetcher((byte)FetchTimings.onMainMenu)]
        class ButtonSpriteFetcher
        {
            private static bool done = false;
            public static void Process()
            {
                if (!done)
                {
                    var sr = ExitButton.GetComponent<SpriteRenderer>();
                    AssetsManager.Register(Ressources.Button.Name, sr.sprite);
                    done = true;
                }
            }
        }

        ///<summary>
        /// Fetches the button's sprite (which in this case is a 9 sliced sprite).
        /// This variante doesn't fetch the same asset multiple times
        ///</summary>
            /*[AssetFetcher((byte)FetchTimings.onMainMenu)]
            class ButtonSpriteFetcher
            {
                private static bool done = false;
                public static void Process()
                {
                    if (!done)
                    {
                        var sr = ExitButton.GetComponent<SpriteRenderer>();
                        AssetsManager.Register(Ressources.Button.Name, sr.sprite);
                        done = true;
                    }
                }
            }*/

        /// <summary>
        /// Patcher class needs to call <see cref="AssetFetcherAttribute.Fetch"/> before using it's fetched asset inside
        /// </summary>
        class ExempleFetchingProcess
        {
            //[HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
            class VersionShower_Start_Patch
            {
                public static void Prefix(/*VersionShower __instance*/)
                {
                    //Calls all fetcher for the fetchgroup 1 ((byte)FetchTimings.onMainMenu => 1)
                    AssetFetcherAttribute.Fetch((byte)FetchTimings.onMainMenu, typeof(VersionShower_Start_Patch).Assembly);
                }
            }
        }
    }
}
