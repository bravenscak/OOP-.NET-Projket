using ClassLibrary.Models;
using ClassLibrary.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassLibrary.Interfaces;
using ClassLibrary.Repositories;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TeamDetailsWindow.xaml
    /// </summary>
    public partial class TeamDetailsWindow : Window
    {
        private readonly ILanguage languageManager = new LanguageManager();

        public TeamDetailsWindow()
        {
            InitializeComponent();
            LoadSettingsAsync();
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
        }

        private void SetLanguage(string languageCode)
        {
            languageManager.SetLanguage(languageCode);
            UpdateUIStrings();
        }

        private void UpdateUIStrings()
        {
            lblCountry.Content = languageManager.GetLanguageString("lblCountry");
            lblCode.Content = languageManager.GetLanguageString("lblCode");
            lblPlayedMatches.Content = languageManager.GetLanguageString("lblPlayedMatches");
            lblWins.Content = languageManager.GetLanguageString("lblWins");
            lblLoses.Content = languageManager.GetLanguageString("lblLoses");
            lblDraws.Content = languageManager.GetLanguageString("lblDraws");
            lblGoals.Content = languageManager.GetLanguageString("lblGoals");
            lblConsededGoals.Content = languageManager.GetLanguageString("lblConsededGoals");
            lblGoalDifference.Content = languageManager.GetLanguageString("lblGoalDifference");
        }

        public void LoadData(Result nationalTeam, long wins, long loses, long draws, long goalsScored, long goalsReceived, long goalDifference)
        {
            lblCountryValue.Content = $"{nationalTeam.Country}";
            lblCodeValue.Content = $"{nationalTeam.FifaCode}";
            lblPlayedMatchesValue.Content = $"{nationalTeam.GamesPlayed}";
            lblWinsValue.Content = $"{wins}";
            lblLosesValue.Content = $"{loses}";
            lblDrawsValue.Content = $"{draws}";
            lblGoalsValue.Content = $"{goalsScored}";
            lblConsededGoalsValue.Content = $"{goalsReceived}";
            lblGoalDifferenceValue.Content = $"{goalDifference}";
        }
    }
}
