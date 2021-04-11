using System;
using UnityEngine;

//as the lazyAsset class is a generic, you may want to simplify the way it looks inside the code...
//sadly the only answer to that is this:
using LazySprite = AssetFetcher.LazyAsset<UnityEngine.Sprite>;
using LazyMaterial = AssetFetcher.LazyAsset<UnityEngine.Material>;
//i come from the c++ world, i know typedefs. no typedefs in c# :,(

namespace AssetFetcher.__Exemples
{
    class AssetLoadingExempleProcess
    {
        private void Main()
        {
            //assetbundle's assets requires 2 things : 
            //a file name that describe the asset inside the bundle.
            //a path that describe the assetbundle's path.

            //embedded only simplifies the fact that it comes from inside your assembly
            LazySprite button = new LazySprite(AssetSource.EmbeddedAssetBundle, "Button.sprite", "Ressources.Assets.ModAssets");

            SpriteRenderer sr = GameObject.FindObjectOfType<SpriteRenderer>();
            sr.sprite = button; //used just like the traditionnal UnityEngine.Sprite class

            //same goes for the materials and any other assets really
            LazyMaterial buttonMat = new LazyMaterial(AssetSource.ExternalAssetBundle, "Button.mat", "Ressources.Assets.ModAssets");
            sr.material = buttonMat;

        }
    }
}
