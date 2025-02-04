using ClassLibrary.Interfaces;
using ClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    internal class AppSettingsRepo : ISettingsRepo
    {
        private const string SETTINGS_FILE_PATH = @"..\..\..\..\ClassLibrary\Settings\settings.txt";

        public async Task<bool> CreateSettingsAsync()
        {
            if (!File.Exists(SETTINGS_FILE_PATH))
            {
                var appSettings = new AppSettings();

                var defaultSettings = new List<string>
                 {
                     $"Repository:API",
                     $"Language:English",
                     $"Championship:Men",
                     $"Resolution:Fullscren"
                 };

                await File.WriteAllLinesAsync(SETTINGS_FILE_PATH, defaultSettings);

                return true;
            }
            return false;
        }

        public async Task<AppSettings> GetSettingsAsync()
        {
            await CreateSettingsAsync();

            var data = await File.ReadAllLinesAsync(SETTINGS_FILE_PATH);

            var settings = new AppSettings
            {
                LoadingDataBy = Utilities.ParseStringValue(data[0]),
                Language = Utilities.ParseStringValue(data[1]),
                GenderCategory = Utilities.ParseStringValue(data[2]),
                Resolution = Utilities.ParseStringValue(data[3])
            };

            return settings;
        }

        public async Task UpdateSettingsAsync(AppSettings appSettings)
        {
            await CreateSettingsAsync();

            var updatedSettings = new List<string>
            {
                $"Repository:{appSettings.LoadingDataBy}",
                $"Language:{appSettings.Language}",
                $"Championship:{appSettings.GenderCategory}",
                $"Resolution:{appSettings.Resolution}"
            };

            File.WriteAllLines(SETTINGS_FILE_PATH, updatedSettings);
        }
    }
}
