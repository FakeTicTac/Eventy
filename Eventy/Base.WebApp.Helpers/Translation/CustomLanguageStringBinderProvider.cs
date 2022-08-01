using Base.Domain.Translation;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Base.WebApp.Helpers.Translation;


/// <summary>
/// Language String Class Model Binding Provider Implementation.
/// </summary>
public class CustomLanguageStringBinderProvider : IModelBinderProvider
{
    
    /// <summary>
    /// Method Creates Support For JSON Objects Model Binding.
    /// </summary>
    /// <param name="context">A Context For GetBinder(ModelBinderProviderContext)</param>
    /// <returns>Defined Interface for Model Binders.</returns>
    /// <exception cref="ArgumentNullException">Thrown If Passed Context is Not Defined.</exception>
    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        // Check if Context is Defined.
        if (context == null) throw new ArgumentNullException($"Context {nameof(context)} is not defined.");
        
        return context.Metadata.ModelType == typeof(LanguageString) ? new LanguageStringBinderProvider() : null;
    }
}