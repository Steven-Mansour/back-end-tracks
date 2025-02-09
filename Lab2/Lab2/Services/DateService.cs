namespace lab2.Services;
using System.Globalization;

public class DateService : IDateService
{
    private readonly List<String> _validCultures;

    public DateService()
    {
        var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
        _validCultures = allCultures.Select(c => c.Name).ToList();
    }

    public string GetFormattedDate(string acceptedLanguage)
    {
        try
        {
            if (!_validCultures.Contains(acceptedLanguage))
                throw new Exception("Accepted Language is not supported");

        }
        catch (Exception ex)
        {
            acceptedLanguage = "en-US";
            
        }
        
        if (acceptedLanguage != "en-ES" && acceptedLanguage != "fr-FR")
        {
            acceptedLanguage = "en-US";
        }
    
        
        var culture = new CultureInfo(acceptedLanguage);
        return DateTime.Now.ToString(culture);
    
}

    public bool IsValidCulture(string culture)
    {
        return _validCultures.Contains(culture);
    }
}
