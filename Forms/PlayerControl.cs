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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class PlayerControl : UserControl
    {
        public static IPlayerIconRepo playerIconRepo = RepoFactory.GetPlayerIconRepo();
        public string PlayerName { get; private set; }
        public bool IsFavourite { get; private set; }
        public string ImagePath { get; private set; }

        public event EventHandler FavouriteStatusChanged;

        private ContextMenuStrip contextMenu;
        private readonly ILanguage languageManager = new LanguageManager();

        private FlowLayoutPanel favouritePanel;
        private FlowLayoutPanel nonFavouritePanel;

        public Player Player { get; private set; }
        public PlayerControl(Player player, FlowLayoutPanel favouritePanel, FlowLayoutPanel nonFavouritePanel)
        {
            InitializeComponent();
            InitializeContextMenu();
            Player = player;
            lblNameValue.Text = player.Name;
            PlayerName = player.Name;
            lblPositionValue.Text = player.Position;
            lblShirtNumberValue.Text = player.ShirtNumber.ToString();
            lblCaptainValue.Text = player.Captain ? "Yes" : "No";
            IsFavourite = false;
            pictureBox1.Visible = IsFavourite;

            pbPicture.ImageLocation = player.ImagePath ?? @"..\..\..\..\ClassLibrary\Images\playerDefault.png";

            this.favouritePanel = favouritePanel;
            this.nonFavouritePanel = nonFavouritePanel;

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

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var markFavouriteMenuItem = new ToolStripMenuItem("Mark as Favourite");
            markFavouriteMenuItem.Click += MarkFavouriteMenuItem_Click;
            contextMenu.Items.Add(markFavouriteMenuItem);

            var setImageMenuItem = new ToolStripMenuItem("Set Player Image");
            setImageMenuItem.Click += SetImageMenuItem_Click;
            contextMenu.Items.Add(setImageMenuItem);

            ContextMenuStrip = contextMenu;
        }

        private void MarkFavouriteMenuItem_Click(object sender, EventArgs e)
        {
            SetFavourite(!IsFavourite);
            UpdateContextMenuText();

            FlowLayoutPanel currentPanel = this.Parent as FlowLayoutPanel;
            currentPanel.Controls.Remove(this);

            if (IsFavourite)
            {
                favouritePanel.Controls.Add(this);
            }
            else
            {
                nonFavouritePanel.Controls.Add(this);
            }

            ReorderPlayerControls(favouritePanel);
            ReorderPlayerControls(nonFavouritePanel);

            FavouriteStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateContextMenuText()
        {
            var menuItem = contextMenu.Items[0] as ToolStripItem;
            menuItem.Text = IsFavourite ? "Remove from favourites" : "Mark as favourite";
        }

        private void SetImageMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImagePath = openFileDialog.FileName;
                    pbPicture.ImageLocation = ImagePath;
                    SavePlayerIconPath();
                }
            }
        }

        private void SavePlayerIconPath()
        {
            playerIconRepo.SavePlayerIconPath(PlayerName, ImagePath);
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

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowContextMenu(e.Location);
                UpdateContextMenuText();
            }
            else if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(this, DragDropEffects.Move);
            }
        }

        private void ShowContextMenu(Point location)
        {
            if (contextMenu == null)
            {
                contextMenu = new ContextMenuStrip();
            }
            contextMenu.Show(this, location);
        }

        internal void SetFavourite(bool isFavourite)
        {
            IsFavourite = isFavourite;
            pictureBox1.Visible = isFavourite;
        }

        private void SetLanguage(string languageCode)
        {
            languageManager.SetLanguage(languageCode);
            UpdateUIStrings();
        }

        private void UpdateUIStrings()
        {
            lblName.Text = languageManager.GetLanguageString("lblName");
            lblPosition.Text = languageManager.GetLanguageString("lblPosition");
            lblShirtNumber.Text = languageManager.GetLanguageString("lblShirtNumber");
            lblCaptain.Text = languageManager.GetLanguageString("lblCaptain");
        }
    }
}
