using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    internal class JsonDataRepo : IDataRepo
    {
        private const string FOLDER_PATH = @"..\..\..\..\ClassLibrary\JsonData";
        private readonly string _matchesPath;
        private readonly string _teamsPath;

        public JsonDataRepo(string gender)
        {
            string genderPath = gender == "men" ? "men" : "women";
            _matchesPath = Path.Combine(FOLDER_PATH, genderPath, "matches.json");
            _teamsPath = Path.Combine(FOLDER_PATH, genderPath, "results.json");
        }

        public async Task<ISet<Match>> GetAllMatchData(string gender)
        {
            return await Utilities.ReadJsonFileAsync<HashSet<Match>>(_matchesPath);
        }

        public async Task<ISet<Result>> GetAllNationalTeamData(string gender)
        {
            return await Utilities.ReadJsonFileAsync<HashSet<Result>>(_teamsPath);
        }

        public async Task<ISet<Match>> GetMatchDataByCountry(string gender, string country)
        {
            var allMatches = await GetAllMatchData(gender);
            var countryMatches = new HashSet<Match>();
            foreach (Match match in allMatches)
            {
                if (match.HomeTeam.Code == country || match.AwayTeam.Code == country)
                {
                    countryMatches.Add(match);
                }
            }
            return countryMatches;
        }
    }
}

