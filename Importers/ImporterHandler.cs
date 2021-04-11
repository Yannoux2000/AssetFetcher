using System;

namespace AssetFetcher.Importers
{
    internal interface ImporterHandler { 
        public Type Type { get; }
    };
}
