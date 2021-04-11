using System.Reflection;

namespace AssetFetcher.Importers
{
    public interface ICustomImporter
    {
        public object GetEmbedded(string Path, string Name, Assembly assembly);
        public object GetExternal(string Path, string Name);
    }
}
