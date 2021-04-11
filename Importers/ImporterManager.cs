using System;
using System.Collections.Generic;
using System.Reflection;
using AssetFetcher.Utils;

namespace AssetFetcher.Importers
{
    public class ImporterManager : Singleton<ImporterManager>
    {
        private Dictionary<Type, ImporterHandler> importers = new Dictionary<Type, ImporterHandler>();

        internal void AddImportor(ImporterHandler importer)
        {
            if (importer.Type.IsAssignableFrom(typeof(UnityEngine.Object)))
            {
                importers.Add(importer.Type, importer);
            }
        }
        public void AddImportor<T, I>() where I : IImporter<T> where T : UnityEngine.Object
        {
            I imp = (I)Activator.CreateInstance(typeof(I));
             importers.Add(typeof(T), imp);
        }

        public IImporter<T> GetImporter<T>()
        {
            ImporterHandler aux = null;

            if (importers.TryGetValue(typeof(T), out aux))
            {
                if (aux is IImporter<T>)
                {
                    return (IImporter<T>)aux;
                }
            }

            return null;
        }
    }
}
