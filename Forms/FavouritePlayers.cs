using ClassLibrary.Interfaces;
using ClassLibrary.Language;
using ClassLibrary.Models;
using ClassLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class FavouritePlayers : Form
    {
        private const string FAV_PLAYERS = @"..\..\..\..\ClassLibrary\Settings\fav_players.txt";
        private const string FAV_TEAM = @"..\..\..\..\ClassLibrary\Settings\fav_team";
        public static IFavouriteSettingsRepo favSettingsRepo = RepoFactory.GetFavouriteSettingsRepo();
        public static IPlayerIconRepo playerIconRepo = RepoFactory.GetPlayerIconRepo();
        private readonly ILanguage languageManager = new LanguageManager();

        private Result selectedTeam;

        public FavouritePlayers(Result selectedTeam)
        {
            InitializeComponent();
            this.selectedTeam = selectedTeam;
            LoadSettingsAsync();
            LoadPlayers();
            InitializeDragDrop();
            this.FormClosing += new FormClosingEventHandler(FavouritePlayers_FormClosing);
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

        private void InitializeDragDrop()
        {
            flpAllPlayers.AllowDrop = true;
            flpFavouritePlayers.AllowDrop = true;

            flpFavouritePlayers.DragEnter += Pnl_DragEnter;
            flpFavouritePlayers.DragDrop += Pnl_DragDrop;

            flpAllPlayers.DragEnter += Pnl_DragEnter;
            flpAllPlayers.DragDrop += Pnl_DragDrop;
        }

        private void Pnl_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerControl)))
            {
                var playerControl = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
                var flowLayoutPanel = (FlowLayoutPanel)sender;

                flowLayoutPanel.Controls.Add(playerControl);
                ReorderPlayerControls(flowLayoutPanel);

                if (flowLayoutPanel == flpFavouritePlayers)
                {
                    playerControl.SetFavourite(true);
                }
                else if (flowLayoutPanel == flpAllPlayers)
                {
                    playerControl.SetFavourite(false);
                }

                SaveFavouritePlayers();
            }
        }

        private void Pnl_DragEnter(object? sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(PlayerControl)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private async void LoadPlayers()
        {
            try
            {
                if (selectedTeam == null)
                {
                    MessageBox.Show("No team selected.");
                    return;
                }

                var countryName = selectedTeam.Country;
                var players = await DataFactory.GetPlayersForSelectedCountryAsync(countryName);

                if (players == null || !players.Any())
                {
                    MessageBox.Show("No players found for selected country");
                    return;
                }

                var favouritePlayers = LoadFavouritePlayers();
                var iconPaths = await playerIconRepo.GetAllIconPathsAsync();

                flpFavouritePlayers.Controls.Clear();
                flpAllPlayers.Controls.Clear();

                foreach (var player in players)
                {
                    var isFavourite = favouritePlayers.Contains(player.Name);
                    iconPaths.TryGetValue(player.Name, out string imagePath);
                    player.ImagePath = imagePath;

                    var playerControl = new PlayerControl(player, flpFavouritePlayers, flpAllPlayers);
                    playerControl.FavouriteStatusChanged += PlayerControl_FavouriteStatusChanged;

                    if (isFavourite)
                    {
                        flpFavouritePlayers.Controls.Add(playerControl);
                        playerControl.SetFavourite(true);
                    }
                    else
                    {
                        flpAllPlayers.Controls.Add(playerControl);
                        playerControl.SetFavourite(false);
                    }
                }

                ReorderPlayerControls(flpFavouritePlayers);
                ReorderPlayerControls(flpAllPlayers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}");
            }
        }

        private void PlayerControl_FavouriteStatusChanged(object sender, EventArgs e)
        {
            SaveFavouritePlayers();
        }

        private HashSet<string> LoadFavouritePlayers()
        {
            if (!File.Exists(FAV_PLAYERS))
                return new HashSet<string>();

            var lines = File.ReadAllLines(FAV_PLAYERS);
            return new HashSet<string>(lines);
        }

        private void SaveFavouritePlayers()
        {
            var favouritePlayers = flpFavouritePlayers.Controls
                .OfType<PlayerControl>()
                .Select(pc => pc.Player.Name)
                .ToArray();

            File.WriteAllLines(FAV_PLAYERS, favouritePlayers);
        }

        private void ReorderPlayerControls(FlowLayoutPanel flowLayoutPanel)
        {
            int topPosition = 0;
            foreach (Control control in flowLayoutPanel.Controls)
            {
                control.Top = topPosition;
                control.Left = 0;
                topPosition += control.Height + 10;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(FavouritePlayers_FormClosing);

            this.Hide();
            InitialSettings settingsForm = new InitialSettings();
            settingsForm.FormClosed += (s, args) => this.Close();
            settingsForm.Show();
        }

        private void btnRankLists_Click(object sender, EventArgs e)
        {
            var countryName = selectedTeam.Country;

            this.FormClosing -= new FormClosingEventHandler(FavouritePlayers_FormClosing);
            this.Hide();
            RankLists rankForm = new RankLists(countryName);
            rankForm.FormClosed += (s, args) => this.Close();
            rankForm.Show();
        }

        private void FavouritePlayers_FormClosing(object sender, FormClosingEventArgs e)
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
            btnSettings.Text = languageManager.GetLanguageString("btnSettings");
            btnRankLists.Text = languageManager.GetLanguageString("btnRankLists");
            lblAllPlayers.Text = languageManager.GetLanguageString("lblAllPlayers");
            lblFavouritePlayers.Text = languageManager.GetLanguageString("lblFavouritePlayers");
        }
    }
}
