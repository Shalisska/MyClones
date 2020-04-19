using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace MVC.MyClones.Extensions
{
    public static class ModelStateDictionaryExtension
    {
        public static string ToFullErrorString(this ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState.Values)
            {
                foreach (var error in entry.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}
