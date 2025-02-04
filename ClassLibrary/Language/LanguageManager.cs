using ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Language
{
    public class LanguageManager : ILanguage
    {
        private static readonly ResourceManager languageManager =
            new ResourceManager($"ClassLibrary.Language.Strings", typeof(LanguageManager).Assembly);

        public void SetLanguage(string languageCode)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(languageCode))
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
                }
                else
                {
                    throw new ArgumentException("Language code cannot be null or whitespace.", nameof(languageCode));
                }
            }
            catch (CultureNotFoundException ex)
            {
                Console.Error.WriteLine($"Invalid language code '{languageCode}': {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"Error setting language: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error setting language: {ex.Message}");
            }
        }

        public string GetLanguageString(string key)
        {
            try
            {
                string? value = languageManager.GetString(key);
                if (value == null)
                {
                    throw new MissingManifestResourceException($"The key '{key}' was not found in the resource files.");
                }
                return value;
            }
            catch (MissingManifestResourceException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return $"[Missing: {key}]";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error retrieving language string for key '{key}': {ex.Message}");
                return $"[Error: {key}]";
            }
        }
    }
}
