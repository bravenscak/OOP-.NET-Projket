using ClassLibrary.Interfaces;
using ClassLibrary.Models;
using ClassLibrary.Repositories;
using ClassLibrary.Settings;
using ClassLibrary.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    public partial class MatchDetailsView : Window
    {
        private const string FAV_TEAM_FILE = @"..\..\..\..\ClassLibrary\Settings\fav_team";
        private List<ClassLibrary.Models.Match> matches;
        private List<Result> teams;
        private readonly ILanguage languageManager = new LanguageManager();

        public MatchDetailsView()
        {
            InitializeComponent();
            LoadSettingsAsync();
            LoadFavoriteTeam();
        }

        private async Task LoadSettingsAsync()
        {
            var settings = await DataFactory.GetAppSettingsAsync();
            if (settings.Language == "Hrvatski")
            {
                SetLanguage("hr");
            }
            else
            {
                SetLanguage("en");
            }

            ApplyResolution(settings.Resolution);
        }

        private void SetLanguage(string languageCode)
        {
            languageManager.SetLanguage(languageCode);
            UpdateUIStrings();
        }

        private void UpdateUIStrings()
        {
            btnDetailsHomeTeam.Content = languageManager.GetLanguageString("btnDetailsHomeTeam");
            btnDetailsAwayTeam.Content = languageManager.GetLanguageString("btnDetailsAwayTeam");
            btnSettingsWpf.Content = languageManager.GetLanguageString("btnSettings");
        }

        private void ApplyResolution(string resolution)
        {
            switch (resolution)
            {
                case "Fullscreen":
                    SetFullScreen(true);
                    break;
                case "r1280x720":
                    SetWindowSize(1280, 720);
                    break;
                case "r1024x768":
                    SetWindowSize(1024, 768);
                    break;
                default:
                    SetFullScreen(false);
                    break;
            }
        }

        private void SetWindowSize(int width, int height)
        {
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.CanResize;
            this.Width = width;
            this.Height = height;
        }

        private void SetFullScreen(bool isFullScreen)
        {
            if (isFullScreen)
            {
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }

        private async void LoadFavoriteTeam()
        {
            var settings = await DataFactory.GetAppSettingsAsync();
            string favTeamName = File.Exists(FAV_TEAM_FILE) ? File.ReadAllText(FAV_TEAM_FILE).Trim() : string.Empty;

            try
            {
                teams = (await DataFactory.GetNationalTeamsAsync(settings.GenderCategory)).ToList();
                var favTeam = teams.FirstOrDefault(t => t.Country == favTeamName);

                cbFavouriteCountry.ItemsSource = teams;
                cbFavouriteCountry.DisplayMemberPath = "Country";
                cbFavouriteCountry.SelectedItem = favTeam;

                if (favTeam != null)
                {
                    await LoadOpponents(favTeam.Country);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading teams: {ex.Message}");
            }
        }

        private async Task LoadOpponents(string favoriteTeam)
        {
            var settings = await DataFactory.GetAppSettingsAsync();

            matches = (await DataFactory.GetMatchesAsync(settings.GenderCategory)).ToList();
            var opponentCountries = matches.Where(m => m.HomeTeam.Country == favoriteTeam || m.AwayTeam.Country == favoriteTeam)
                                           .Select(m => m.HomeTeam.Country == favoriteTeam ? m.AwayTeam.Country : m.HomeTeam.Country)
                                           .Distinct()
                                           .ToList();

            var opponents = teams.Where(t => opponentCountries.Contains(t.Country)).ToList();

            cbFavouriteCountryOpponents.ItemsSource = opponents;
            cbFavouriteCountryOpponents.DisplayMemberPath = "Country";
            cbFavouriteCountryOpponents.SelectedValuePath = "Country";

            if (!opponents.Any())
            {
                cbFavouriteCountryOpponents.SelectedIndex = -1;
            }
        }

        private async void cbFavouriteCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFavouriteCountry.SelectedItem is Result selectedTeam)
            {
                HomeGoalies.Children.Clear();
                HomeDefenders.Children.Clear();
                HomeMidfielders.Children.Clear();
                HomeForwards.Children.Clear();
                AwayGoalies.Children.Clear();
                AwayDefenders.Children.Clear();
                AwayMidfielders.Children.Clear();
                AwayForwards.Children.Clear();
                lblMatchResult.Content = "";

                var favouriteTeam = cbFavouriteCountry.SelectedItem as Result;

                if (favouriteTeam != null)
                {
                    File.WriteAllText(FAV_TEAM_FILE, favouriteTeam.Country);
                }

                await LoadOpponents(selectedTeam.Country);
            }
        }

        private void cbFavouriteCountryOpponents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var startingTeam = cbFavouriteCountry.SelectedItem as Result;
            var opponentTeam = cbFavouriteCountryOpponents.SelectedItem as Result;

            if (startingTeam != null && opponentTeam != null && matches != null)
            {
                var match = matches.FirstOrDefault(m =>
                    (m.HomeTeam.Country == startingTeam.Country && m.AwayTeam.Country == opponentTeam.Country) ||
                    (m.HomeTeam.Country == opponentTeam.Country && m.AwayTeam.Country == startingTeam.Country));

                if (match != null)
                {
                    string result = match.HomeTeam.Country == startingTeam.Country
                        ? $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}"
                        : $"{match.AwayTeam.Goals} : {match.HomeTeam.Goals}";

                    DoubleAnimation fadeInAnimation = new DoubleAnimation
                    {
                        From = 0.0,
                        To = 1.0,
                        Duration = TimeSpan.FromSeconds(0.5)
                    };

                    lblMatchResult.BeginAnimation(TextBlock.OpacityProperty, fadeInAnimation);
                    lblMatchResult.Content = result;

                    LoadStartingEleven(match);
                }
                else
                {
                    lblMatchResult.SetValue(UidProperty, "NoMatch");
                }
            }
        }

        private void LoadStartingEleven(ClassLibrary.Models.Match match)
        {
            HomeGoalies.Children.Clear();
            HomeDefenders.Children.Clear();
            HomeMidfielders.Children.Clear();
            HomeForwards.Children.Clear();
            AwayGoalies.Children.Clear();
            AwayDefenders.Children.Clear();
            AwayMidfielders.Children.Clear();
            AwayForwards.Children.Clear();

            var favoriteTeam = cbFavouriteCountry.SelectedItem as Result;

            if (favoriteTeam != null)
            {
                if (match.HomeTeam.Country == favoriteTeam.Country)
                {
                    AddPlayersToTeam(match.HomeTeamStatistics?.StartingEleven, match.HomeTeam.Country, true);
                    AddPlayersToTeam(match.AwayTeamStatistics?.StartingEleven, match.AwayTeam.Country, false);
                }
                else if (match.AwayTeam.Country == favoriteTeam.Country)
                {
                    AddPlayersToTeam(match.AwayTeamStatistics?.StartingEleven, match.AwayTeam.Country, true);
                    AddPlayersToTeam(match.HomeTeamStatistics?.StartingEleven, match.HomeTeam.Country, false);
                }
            }
        }

        private void AddPlayersToTeam(List<Player> players, string teamCountry, bool isHomeTeam)
        {
            if (players == null) return;

            foreach (var player in players)
            {
                var playerControl = new PlayerControlView
                {
                    PlayerName = player.Name,
                    ShirtNumber = player.ShirtNumber,
                    Country = new Result { Country = teamCountry }
                };

                switch (player.Position)
                {
                    case "Goalie":
                        if (isHomeTeam)
                            HomeGoalies.Children.Add(playerControl);
                        else
                            AwayGoalies.Children.Add(playerControl);
                        break;
                    case "Defender":
                        if (isHomeTeam)
                            HomeDefenders.Children.Add(playerControl);
                        else
                            AwayDefenders.Children.Add(playerControl);
                        break;
                    case "Midfield":
                        if (isHomeTeam)
                            HomeMidfielders.Children.Add(playerControl);
                        else
                            AwayMidfielders.Children.Add(playerControl);
                        break;
                    case "Forward":
                        if (isHomeTeam)
                            HomeForwards.Children.Add(playerControl);
                        else
                            AwayForwards.Children.Add(playerControl);
                        break;
                }
            }
        }

        private async void btnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            if (button.Name == btnDetailsHomeTeam.Name)
            {
                if (cbFavouriteCountry.SelectedIndex != -1 && cbFavouriteCountry.SelectedItem is Result favoriteTeam)
                {
                    try
                    {
                        ShowTeamDetailsWindow(favoriteTeam);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while loading matches: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else if (button.Name == btnDetailsAwayTeam.Name)
            {
                if (cbFavouriteCountryOpponents.SelectedIndex != -1 && cbFavouriteCountryOpponents.SelectedItem is Result favoriteTeamOpponent)
                {
                    try
                    {
                        ShowTeamDetailsWindow(favoriteTeamOpponent);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while loading matches: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ShowTeamDetailsWindow(Result team)
        {
            if (team == null)
            {
                MessageBox.Show("Team data is not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var teamDetailsWindow = new TeamDetailsWindow();
            teamDetailsWindow.LoadData(team, team.Wins, team.Losses, team.Draws, team.GoalsFor, team.GoalsAgainst, team.GoalDifferential);
            teamDetailsWindow.Show();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(this);
            settingsWindow.SettingsApplied += SettingsWindow_SettingsApplied;
            settingsWindow.ShowDialog();
            LoadSettingsAsync();
        }

        private void SettingsWindow_SettingsApplied(object sender, EventArgs e)
        {
            LoadFavoriteTeam();
        }
    }
}
