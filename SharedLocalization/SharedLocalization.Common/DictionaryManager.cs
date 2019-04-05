using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SharedLocalization.Common
{
    public class DictionaryManager
    {
        public string Translate(string key)
        {
            ResourceManager rm = new ResourceManager("SharedLocalization.Common.Resources.Resource", typeof(DictionaryManager).GetTypeInfo().Assembly);

            var currentCulture = CultureInfo.DefaultThreadCurrentCulture;
            return rm.GetString(key, currentCulture);
        }
    }
}
