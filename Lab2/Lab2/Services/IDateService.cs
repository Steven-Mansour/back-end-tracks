namespace lab2.Services;
using System.Globalization;

public interface IDateService
{
    string GetFormattedDate(string acceptedLanguage);
    bool IsValidCulture(string culture);
}