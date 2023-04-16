using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AddressBook.Api.Extensions.ValidationExtensions
{
    public static class ValidationResultExtension
    {
            public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
    }
}
