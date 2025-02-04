using ClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IFavouriteSettingsRepo
    {
        Task<bool> CreateSettingsAsync();
        Task<FavouriteSettings> GetSettingsAsync();
        Task UpdateSettingsAsync(FavouriteSettings appSettings);
    }
}
