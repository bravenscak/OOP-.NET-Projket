using ClassLibrary.Interfaces;
using ClassLibrary.Language;
using ClassLibrary.Models;
using ClassLibrary.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class FavouriteTeam : Form
    {
        private const string FAV_TEAM = @"..\..\..\..\ClassLibrary\Settings\fav_team";
        public static ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();
        private readonly ILanguage languageManager = new LanguageManager();

        public FavouriteTeam()
        {
            InitializeComponent();
            LoadSettingsAsync();
            LoadTeams();
            this.FormClosing += new FormClosingEventHandler(FavouriteTeam_FormClosing);
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

        private async void LoadTeams()
        {
            try
            {
                var settings = await DataFactory.GetAppSettingsAsync();
                var teams = await DataFactory.GetNationalTeamsAsync(settings.GenderCategory);

                cbFavouriteTeam.SelectedIndexChanged -= cbFavouriteTeam_SelectedIndexChanged;

                cbFavouriteTeam.DataSource = teams.ToList();
                cbFavouriteTeam.DisplayMember = "Country";
                cbFavouriteTeam.ValueMember = "Country";

                cbFavouriteTeam.SelectedIndexChanged += cbFavouriteTeam_SelectedIndexChanged;

                if (File.Exists(FAV_TEAM))
                {
                    string favTeam = File.ReadAllText(FAV_TEAM).Trim();
                    if (!string.IsNullOrEmpty(favTeam))
                    {
                        foreach (var item in cbFavouriteTeam.Items)
                        {
                            if (cbFavouriteTeam.GetItemText(item) == favTeam)
                            {
                                cbFavouriteTeam.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}");
            }
        }

        private async void cbFavouriteTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTeam = cbFavouriteTeam.SelectedItem as Result;

            if (selectedTeam != null)
            {
                File.WriteAllText(FAV_TEAM, selectedTeam.Country);
            }
        }

        private void cbSubmit_Click(object sender, EventArgs e)
        {
            var selectedTeam = cbFavouriteTeam.SelectedItem as Result;

            this.FormClosing -= new FormClosingEventHandler(FavouriteTeam_FormClosing);
            this.Hide();
            FavouritePlayers favouritePlayersForm = new FavouritePlayers(selectedTeam);
            favouritePlayersForm.FormClosed += (s, args) => this.Close();
            favouritePlayersForm.Show();
        }

        private void FavouriteTeam_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to close the form?",
                "Confirm Close",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void SetLanguage(string languageCode)
        {
            languageManager.SetLanguage(languageCode);
            UpdateUIStrings();
        }

        private void UpdateUIStrings()
        {
            lblFavouriteTeam.Text = languageManager.GetLanguageString("lblFavouriteTeam");
            cbSubmitTeam.Text = languageManager.GetLanguageString("cbSubmitTeam");
        }
    }
}
