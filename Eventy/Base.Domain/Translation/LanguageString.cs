

namespace Base.Domain.Translation;


/// <summary>
/// Class Represents JSON Format Type To Be Stored in Database.
///  - Primary Use For Language Translations.
/// </summary>
public class LanguageString : Dictionary<string, string>
{
    
    /// <summary>
    /// Constant Value Defines Default Culture.
    /// </summary>
    private const string DefaultCulture = "en-GB";
    
    
    /// <summary>
    /// Basic Parameterless LanguageString Object Constructor.
    /// </summary>
    public LanguageString() {}

    
    /// <summary>
    /// Basic LanguageString Object Constructor Which Takes String Value as Parameter. (Uses Current Culture)
    /// </summary>
    /// <param name="value">String Value As Parameter To Be Added.</param>
    public LanguageString(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name) {}

    
    /// <summary>
    /// Basic LanguageString Object Constructor Which Takes String Value and Current Culture as Parameters.
    /// </summary>
    /// <param name="value">String Value To Be Added In Dictionary as Value.</param>
    /// <param name="culture">Culture String Value To Be Added In Dictionary as Key.</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public LanguageString(string value, string culture) => this[culture] = value;
    
    
    /// <summary>
    /// Translates Value In Dictionary.
    ///  - Adds New or Rewrites Existed.
    /// </summary>
    /// <param name="value">String Value To Be Added To The Dictionary as Value.</param>
    public void SetTranslation(string value) => this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    
    
    /// <summary>
    /// Method Finds Needed Translation In LanguageString Object.
    /// </summary>
    /// <param name="culture">Culture String Value To Be Searched In Dictionary as Key.</param>
    /// <returns>Value of The Dictionary With Requested Culture.</returns>
    private string? Translate(string? culture = null)
    {
        // Check If Dictionary is Empty.
        if (Count == 0) return null;

        // Define Culture for Translation.
        var currentCulture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;

        // Return If Culture is Found.
        if (ContainsKey(currentCulture)) return this[currentCulture];
        
        // Try To Find Neutral Culture for This Region.
        var neutralCulture = currentCulture.Split("-")[0];

        // Return If Culture is Found.
        if (ContainsKey(neutralCulture)) return this[neutralCulture];
        
        // Return Default Culture if Exist or Nothing.
        return ContainsKey(DefaultCulture) ? this[DefaultCulture] : null;
    }
    
    
    /// <summary>
    /// String Representation Of The LanguageString Object.
    /// </summary>
    /// <returns>String Representation Of The LanguageString Object.</returns>
    public override string ToString() => Translate() ?? "null";
    
    
    /// <summary>
    /// Method Represents Implicit Conversion of LanguageString Object To String.
    /// </summary>
    /// <param name="languageString">LanguageString Object To Be Converted To String.</param>
    /// <returns>Language Object in String Format.</returns>
    public static implicit operator string(LanguageString? languageString) => languageString?.ToString() ?? "null";
    
    
    /// <summary>
    /// Method Represents Implicit Conversion of String To LanguageString Object.
    /// </summary>
    /// <param name="value">String Value To Be Converted To The LanguageString Object.</param>
    /// <returns></returns>
    public static implicit operator LanguageString(string value) => new(value);

}