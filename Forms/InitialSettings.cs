using ClassLibrary.Interfaces;
using ClassLibrary.Language;
using ClassLibrary.Repositories;
using ClassLibrary.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class InitialSettings : Form
    {
        private readonly ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();
        private AppSettings currentSettings;
        private readonly ILanguage languageManager = new LanguageManager();

        private const string SETTINGS_FILE_PATH = @"..\..\..\..\ClassLibrary\Settings\settings.txt";

        public InitialSettings()
        {
            InitializeComponent();
            LoadSettingsAsync();
            this.FormClosing += new FormClosingEventHandler(InitialSettings_FormClosing);
            cbLanguage.SelectedIndexChanged += new EventHandler(cbLanguage_SelectedIndexChanged);
        }

        private async Task LoadSettingsAsync()
        {
            currentSettings = await settingsRepo.GetSettingsAsync();
            ChangeSettings();
        }

        private void ChangeSettings()
        {
            cbTournament.SelectedIndex = currentSettings.GenderCategory == "Men" ? 0 : 1;
            cbLanguage.SelectedIndex = currentSettings.Language == "English" ? 0 : 1;
        }

        private async Task<bool> SaveSettings()
        {
            if (cbTournament.SelectedIndex < 0)
            {
                MessageBox.Show("Please select tournament.");
                return false;
            }

            var genderCategory = cbTournament.SelectedIndex == 0 ? "Men" : "Women";
            var language = cbLanguage.SelectedIndex == 0 ? "English" : "Hrvatski";

            currentSettings.GenderCategory = genderCategory;
            currentSettings.Language = language;

            await settingsRepo.UpdateSettingsAsync(currentSettings);

            string[] lines = File.ReadAllLines(SETTINGS_FILE_PATH);
            foreach (string line in lines)
            {
                Debug.WriteLine(line);
            }

            return true;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to save these settings?", "Confirm Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (await SaveSettings())
                {
                    this.FormClosing -= new FormClosingEventHandler(InitialSettings_FormClosing);
                    this.Hide();
                    FavouriteTeam favouriteTeamForm = new FavouriteTeam();
                    favouriteTeamForm.FormClosed += (s, args) => this.Close();
                    favouriteTeamForm.Show();
                }
            }
        }

        private void InitialSettings_FormClosing(object sender, FormClosingEventArgs e)
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

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLanguage = cbLanguage.SelectedIndex == 0 ? "en" : "hr";
            languageManager.SetLanguage(selectedLanguage);
            UpdateUIStrings();
        }

        private void UpdateUIStrings()
        {
            btnSubmitSettings.Text = languageManager.GetLanguageString("btnSubmitSettings");
            lblTournament.Text = languageManager.GetLanguageString("lblTournament");
            lblLanguage.Text = languageManager.GetLanguageString("lblLanguage");
        }
    }
}
