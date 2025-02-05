using ClassLibrary.Interfaces;
using ClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    public static class RepoFactory
    {
        public static ISettingsRepo GetSettingsRepo() => new AppSettingsRepo();
        public static IFavouriteSettingsRepo GetFavouriteSettingsRepo() => new FavouriteSettingsRepo();

        public static async Task<IDataRepo> GetDataRepoAsync()
        {
            var settingsRepo = GetSettingsRepo();
            var settings = await settingsRepo.GetSettingsAsync();
            var repoType = settings.LoadingDataBy;

            if (repoType == "Json")
            {
                return new JsonDataRepo(settings.GenderCategory);
            }
            else if (repoType == "API")
            {
                return new ApiDataRepo();
            }
            else { throw new InvalidOperationException("Invalid repo type."); }
        }
        private static readonly AppSettings appSettings;
        public static IDataRepo GetJsonDataRepo() => new JsonDataRepo(appSettings.GenderCategory);
        public static IDataRepo GetApiRepo() => new ApiDataRepo();
        public static IPlayerIconRepo GetPlayerIconRepo() => new PlayerIconRepo();
    }
}
