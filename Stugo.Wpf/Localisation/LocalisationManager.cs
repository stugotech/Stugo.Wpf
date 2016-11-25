using System;

namespace Stugo.Wpf.Localisation
{
    public class LocalisationManager
    {
        private static ILocalisationManager current = null;


        /// <summary>
        /// The current instance.
        /// </summary>
        public static ILocalisationManager Current
        {
            get
            {
                if (current == null)
                    throw new InvalidOperationException("LocalisationManager has not been initialised.");
                return current;
            }
            set
            {
                current = value;
            }
        }
    }
}
