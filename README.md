# UnityAssetFetcher !V1.1 is out!

UnityAssetFetcher is a library that helps mod creators resolve ingame dependancy at runtime (for Assets mostly)

# How to use
Just add the dll(in release 1.1.0) to the BepInEx plugins, then add the dll to your Assembly Dependencies, just like a BepInEx plugin dependency !

# Can i fork/customize/extend this library ?
Absolutely, feel free to use it inside your mods, the license may only allow for extentions, and to stick with this, it is extentiable ! you can customize importers, and even add your CustomImporter
Each unity asset's type only have one importer slot, but you can add CustomImporters on an asset basis.
