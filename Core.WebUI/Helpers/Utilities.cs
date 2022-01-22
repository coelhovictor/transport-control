using Core.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace System
{
    public static class Utilities
    {
        public static ApplicationUser UserProfile(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
        {
            if (!user.Identity.IsAuthenticated) return null;
            return userManager.FindByEmailAsync(user.Identity.Name).Result;
        }

        public static bool IsDev(this IHtmlHelper html)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return env.Equals("Development") ? true : false;
        }

        public static string FormatMoney(this IHtmlHelper html, decimal? value)
        {
            if (!value.HasValue) return "0.00";
            return value.Value.ToString("c", new CultureInfo("en-US", false));
        }

        public static string IsActive(this IHtmlHelper html, string action, string control)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            var returnActive = control == routeControl &&
                               action == routeAction;

            return returnActive ? "active" : "";
        }

        public static string InvalidStateMessages(ModelStateDictionary modelState)
        {
            return string.Join("; ", modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        }

        public static DateTime FirstDateWeek(this IHtmlHelper html)
        {
            int dayOfWeek = DateTime.Now.DayOfWeek.GetHashCode();
            return DateTime.Now.AddDays(-dayOfWeek);
        }

        public static DateTime LastDateWeek(this IHtmlHelper html)
        {
            int dayOfWeek = DateTime.Now.DayOfWeek.GetHashCode();
            return DateTime.Now.AddDays(6 - dayOfWeek);
        }

        public static DateTime FirstDateMonth(this IHtmlHelper html)
        {
            DateTime min = DateTime.Now;
            return new DateTime(min.Year, min.Month, 1, 00, 00, 00);
        }

        public static DateTime LastDateMonth(this IHtmlHelper html)
        {
            DateTime max = DateTime.Now.AddMonths(1);
            return new DateTime(max.Year, max.Month, 1, 23, 59, 59).AddDays(-1);
        }
    }
}
