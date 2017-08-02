using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;

namespace Stugo.Wpf.Localisation
{
    public sealed class ResourcesLocalisationManager : ILocalisationManager
    {
        private static readonly Regex invalidCharRegex = new Regex("(^[^A-Za-z_]|[^A-Za-z0-9_])+", RegexOptions.Compiled);

        public ICollection<ResourceManager> Resources { get; private set; }


        public ResourcesLocalisationManager(params Type[] resourceTypes)
        {
            this.Resources = new List<ResourceManager>(
                resourceTypes.Select(
                    x => new ResourceManager(x.FullName, x.Assembly)
                )
            );
        }


        public string GetString(string key)
        {
            key = Sanitise(key);
            string value = null;

            foreach (var res in Resources)
            {
                value = res.GetString(key);

                if (value != null)
                    break;
            }

            return value;
        }


        public string GetPlural(string baseKey, int n)
        {
            string value = null;
            var start = Math.Min(4, Math.Max(0, n));

            for (var i = start; i >= 0 && value == null; --i)
            {
                value = GetString(string.Format("{0}_{1}", baseKey, i));
            }

            return string.Format(value ?? GetString(baseKey), n);
        }


        private static string Sanitise(string key)
        {
            return invalidCharRegex.Replace(key, "_");
        }
    }
}
