using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    internal class ApiDataRepo : IDataRepo
    {
        private const string URLBASE = "https://worldcup-vua.nullbit.hr";
        private readonly HttpClient _httpClient;

        public ApiDataRepo() => _httpClient = new HttpClient();

        public async Task<ISet<Match>> GetAllMatchData(string gender)
        {
            string url = $"{URLBASE}/{GetGender(gender)}/matches";
            return await Utilities.GetJsonObjFromUrlAsync<HashSet<Match>>(url);
        }

        public async Task<ISet<Result>> GetAllNationalTeamData(string gender)
        {
            string url = $"{URLBASE}/{GetGender(gender)}/teams/results";
            return await Utilities.GetJsonObjFromUrlAsync<HashSet<Result>>(url);
        }

        public async Task<ISet<Match>> GetMatchDataByCountry(string gender, string country)
        {
            string url = $"{URLBASE}/{GetGender(gender)}/matches/country?fifa_code={country}";
            return await Utilities.GetJsonObjFromUrlAsync<HashSet<Match>>(url);
        }

        private string GetGender(string gender) => gender.ToLower() == "men" ? "men" : "women";
    }
}
