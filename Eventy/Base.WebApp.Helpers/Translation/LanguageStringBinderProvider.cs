using Base.Domain.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Base.WebApp.Helpers.Translation;


/// <summary>
/// Language String Class Model Binding Implementation.
/// </summary>
public class LanguageStringBinderProvider : IModelBinder
{
    
    /// <summary>
    /// Method Creates Support For JSON Objects Model Binding.
    /// </summary>
    /// <param name="bindingContext">A Context That Contains Operating Information For Model Binding and Validation.</param>
    /// <returns>Asynchronous Operation as An Indicator of Process Completion.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        
        // Check if There is No Value. (Nothing To Do)
        if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

        // Handle Multiple Values.
        var value = valueProviderResult.FirstValue;

        // No Value is Presented.
        if (value == null) return Task.CompletedTask;
        
        bindingContext.Result = ModelBindingResult.Success(new LanguageString(value));
        
        return Task.CompletedTask;
    }
}