using Newtonsoft.Json.Linq;

namespace pos_api_app.Utilities.Handlers;

public class OnlyNumberHandler<TKey>
{
    public static bool ValidNumber(TKey property)
    {
        string propertyString = property!.ToString()!; // Convert decimal to string for validation
        return System.Text.RegularExpressions.Regex.IsMatch(propertyString, @"^[0-9.,]+$");
    }
}
