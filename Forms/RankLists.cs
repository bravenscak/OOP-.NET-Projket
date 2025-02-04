using ClassLibrary.Interfaces;
using ClassLibrary.Language;
using ClassLibrary.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class RankLists : Form
    {
        private readonly string selectedTeam;
        private static readonly ISettingsRepo settingsRepository = RepoFactory.GetSettingsRepo();
        private readonly ILanguage languageManager = new LanguageManager();

        public RankLists(string team)
        {
            InitializeComponent();
            selectedTeam = team;
            LoadSettingsAsync();
            InitializeComboBox();
            this.FormClosing += new FormClosingEventHandler(RankLists_FormClosing);
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

        private void InitializeComboBox()
        {
            cbRankBy.Items.Add("Players: most goals");
            cbRankBy.Items.Add("Players: most yellow cards");
            cbRankBy.Items.Add("Matches: most attendance");
            cbRankBy.SelectedIndex = -1;
        }

        private async Task DisplayRankingsAsync(string selectedCriteria)
        {
            lbRankings.Items.Clear();
            if (selectedCriteria.Contains("Players"))
            {
                var playerRankings = await DataFactory.GetPlayerDataForSelectedCountryAsync(selectedTeam);

                List<(string PlayerName, int Goals, int YellowCards)> rankedPlayers;

                if (selectedCriteria == languageManager.GetLanguageString("Players: most goals"))
                {
                    rankedPlayers = playerRankings.OrderByDescending(x => x.Value.Goals)
                        .Select(x => (x.Key, x.Value.Goals, x.Value.YellowCards)).ToList();
                }
                else
                {
                    rankedPlayers = playerRankings.OrderByDescending(x => x.Value.YellowCards)
                        .Select(x => (x.Key, x.Value.Goals, x.Value.YellowCards)).ToList();
                }

                foreach (var player in rankedPlayers)
                {
                    lbRankings.Items.Add($"{player.PlayerName} - {"Goals"}: {player.Goals}, {"Yellow Cards"}: {player.YellowCards}");
                }
            }
            else if (selectedCriteria == languageManager.GetLanguageString("Matches: most attendance"))
            {
                var settings = await settingsRepository.GetSettingsAsync();
                var matches = await DataFactory.GetMatchesAsync(settings.GenderCategory);

                var rankedMatches = matches.OrderByDescending(m => m.Attendance)
                    .Where(m => m.HomeTeamCountry == selectedTeam || m.AwayTeamCountry == selectedTeam)
                    .Select(m => new { Location = m.Location, Attendance = m.Attendance, HomeTeam = m.HomeTeamCountry, AwayTeam = m.AwayTeamCountry });

                foreach (var match in rankedMatches)
                {
                    lbRankings.Items.Add($"{match.HomeTeam} - {match.AwayTeam}, {languageManager.GetLanguageString("Location")}: {match.Location}, {languageManager.GetLanguageString("Attendance")}: {match.Attendance}");
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(RankLists_FormClosing);
            this.Hide();
            InitialSettings settingsForm = new InitialSettings();
            settingsForm.FormClosed += (s, args) => this.Close();
            settingsForm.Show();
        }

        private async void cbRankBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCriteria = cbRankBy.SelectedItem.ToString();
            await DisplayRankingsAsync(selectedCriteria);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ExportListBoxToPDF(lbRankings, "Ranking.pdf");
        }

        private void ExportListBoxToPDF(ListBox listBox, string fileName)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(docPath, fileName);

            if (listBox.Items.Count > 0)
            {
                using (iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4))
                {
                    PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
                    pdfDoc.Open();

                    foreach (var item in listBox.Items)
                    {
                        pdfDoc.Add(new Paragraph(item.ToString()));
                    }

                    pdfDoc.Close();
                }

                MessageBox.Show($"{languageManager.GetLanguageString("PDF has been successfully created at")}: {filePath}", languageManager.GetLanguageString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RankLists_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                languageManager.GetLanguageString("Are you sure you want to close the form?"),
                languageManager.GetLanguageString("Confirm Close"),
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
            btnSettings.Text = languageManager.GetLanguageString("btnSettings");
            btnPrint.Text = languageManager.GetLanguageString("btnPrint");
        }
    }
}