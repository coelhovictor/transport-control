using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace System
{
    public static class Utilities
    {
        public static string InvalidStateMessages(ModelStateDictionary modelState)
        {
            return string.Join("; ", modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        }
    }
}
