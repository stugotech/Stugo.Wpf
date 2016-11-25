namespace Stugo.Wpf.Localisation
{
    public interface ILocalisationManager
    {
        string GetString(string key);
        string GetPlural(string baseKey, int n);
    }
}
