using ClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface ISettingsRepo
    {
        Task<AppSettings> GetSettingsAsync();
        Task UpdateSettingsAsync(AppSettings appSettings);
        Task<bool> CreateSettingsAsync();
    }
}
