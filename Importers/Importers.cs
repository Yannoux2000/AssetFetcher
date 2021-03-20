using System;
using System.Collections.Generic;
using System.Reflection;
using AssetFetcher.Utils;

namespace AssetFetcher.Importers
{
    public class Importers : Singleton<Importers>
    {
        private Dictionary<Type, ImporterHandler> importers = new Dictionary<Type, ImporterHandler>();

        internal void AddImportor(ImporterHandler importer)
        {
            if (importer.Type.IsAssignableFrom(typeof(UnityEngine.Object)))
            {
                importers.Add(importer.Type, importer);
            }
        }

        public void LoadImporters(Assembly assembly)
        {
            ImporterAttribute.Register(assembly);
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

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ImporterAttribute : Attribute
    {
        public ImporterAttribute() { }

        internal static void Register(Assembly assembly)
        {
            Assembly asm = Assembly.GetCallingAssembly();
            foreach (var type in asm.GetTypes())
            {
                var attrib = type.GetCustomAttribute<ImporterAttribute>();

                if (typeof(ImporterHandler).IsAssignableFrom(type))
                {
                    ImporterHandler instance = (ImporterHandler)Activator.CreateInstance(type);
                    Importers.Instance.AddImportor(instance);
                }
            }
        }
    }
}
