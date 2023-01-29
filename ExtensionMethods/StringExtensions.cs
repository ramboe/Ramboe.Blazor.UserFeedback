using System.Text.RegularExpressions;
using System.Web;

namespace Ramboe.Blazor.UserFeedback.ExtensionMethods;

public static class StringExtensions
{
    public static string TransformLineBreaksIntoHtmlBr(this string text)
    {
        return Regex.Replace(HttpUtility.HtmlEncode(text), "\r?\n|\r", "<br />");
    }
}