using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using ClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories
{
    internal class FavouriteSettingsRepo : IFavouriteSettingsRepo
    {
        private const string SETTINGS_TEAM_FILE_PATH = @"..\..\..\..\ClassLibrary\Settings\fav_teams";
        private const string SETTINGS_PLAYERS_FILE_PATH = @"..\..\..\..\ClassLibrary\Settings\fav_players.txt";
        private const string DEFAULT_TEAM = "null";
        private const string DEFAULT_PLAYER = "null";

        public async Task<bool> CreateSettingsAsync()
        {
            bool created = false;

            if (!File.Exists(SETTINGS_TEAM_FILE_PATH))
            {
                await File.WriteAllTextAsync(SETTINGS_TEAM_FILE_PATH, DEFAULT_TEAM);
                created = true;
            }

            if (!File.Exists(SETTINGS_PLAYERS_FILE_PATH))
            {
                await File.WriteAllTextAsync(SETTINGS_PLAYERS_FILE_PATH, DEFAULT_PLAYER);
                created = true;
            }

            return created;
        }

        public async Task<FavouriteSettings> GetSettingsAsync()
        {
            await CreateSettingsAsync(); 

            var team = await File.ReadAllTextAsync(SETTINGS_TEAM_FILE_PATH);
            var playersLine = await File.ReadAllTextAsync(SETTINGS_PLAYERS_FILE_PATH);

            var settings = new FavouriteSettings
            {
                FavouriteTeam = await ParseFavTeamAsync(team),
                FavouritePlayers = await ParseFavPlayers(playersLine)
            };

            return settings;
        }

        private async Task<List<Player>> ParseFavPlayers(string line)
        {
            var favPlayers = new List<Player>();
            var playerNames = Utilities.GetValuesFromLine(line);
            var players = await DataFactory.GetPlayersAsync();

            foreach (var playerName in playerNames)
            {
                var player = players.FirstOrDefault(p => p.Name == playerName);
                if (player != null)
                {
                    favPlayers.Add(player);
                }
            }
            return favPlayers;
        }

        private async Task<Result> ParseFavTeamAsync(string value)
        {
            if (value == DEFAULT_TEAM)
            {
                return null;
            }

            var settings = await DataFactory.GetAppSettingsAsync();
            var gender = settings.GenderCategory;
            var nationalTeams = await DataFactory.GetNationalTeamsAsync(gender);
            return nationalTeams.FirstOrDefault(t => t.Country == value);
        }

        public async Task UpdateSettingsAsync(FavouriteSettings appSettings)
        {
            await CreateSettingsAsync(); 

            var playersString = string.Join(Environment.NewLine, appSettings.FavouritePlayers.Select(p => p.Name));
            var teamString = appSettings.FavouriteTeam?.Country ?? DEFAULT_TEAM;

            await File.WriteAllTextAsync(SETTINGS_TEAM_FILE_PATH, teamString);
            await File.WriteAllTextAsync(SETTINGS_PLAYERS_FILE_PATH, playersString);
        }
    }
}
