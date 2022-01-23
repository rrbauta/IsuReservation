using System.Globalization;
using IsuReservation.Resources;

namespace IsuReservation.Helpers;

/// <summary>
/// </summary>
public static class CultureHelper
{
    /// <summary>
    ///     Set Current Culture using language sent in http request headers
    /// </summary>
    /// <param name="lang"></param>
    public static void SetCultureByUserLanguage(string? lang)
    {
        MessageResource.Culture = CultureInfo.GetCultureInfo(!string.IsNullOrEmpty(lang) ? lang : "es");
    }
}