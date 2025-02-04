namespace Forms
{
    partial class FavouritePlayers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flpAllPlayers = new FlowLayoutPanel();
            flpFavouritePlayers = new FlowLayoutPanel();
            lblAllPlayers = new Label();
            lblFavouritePlayers = new Label();
            btnRankLists = new Button();
            btnSettings = new Button();
            SuspendLayout();
            // 
            // flpAllPlayers
            // 
            flpAllPlayers.AutoScroll = true;
            flpAllPlayers.Location = new Point(12, 29);
            flpAllPlayers.Name = "flpAllPlayers";
            flpAllPlayers.Size = new Size(484, 466);
            flpAllPlayers.TabIndex = 0;
            // 
            // flpFavouritePlayers
            // 
            flpFavouritePlayers.AutoScroll = true;
            flpFavouritePlayers.Location = new Point(516, 29);
            flpFavouritePlayers.Name = "flpFavouritePlayers";
            flpFavouritePlayers.Size = new Size(484, 466);
            flpFavouritePlayers.TabIndex = 1;
            // 
            // lblAllPlayers
            // 
            lblAllPlayers.AutoSize = true;
            lblAllPlayers.Font = new Font("Segoe UI", 12F);
            lblAllPlayers.Location = new Point(12, 5);
            lblAllPlayers.Name = "lblAllPlayers";
            lblAllPlayers.Size = new Size(82, 21);
            lblAllPlayers.TabIndex = 2;
            lblAllPlayers.Text = "All players";
            // 
            // lblFavouritePlayers
            // 
            lblFavouritePlayers.AutoSize = true;
            lblFavouritePlayers.Font = new Font("Segoe UI", 12F);
            lblFavouritePlayers.Location = new Point(516, 5);
            lblFavouritePlayers.Name = "lblFavouritePlayers";
            lblFavouritePlayers.Size = new Size(128, 21);
            lblFavouritePlayers.TabIndex = 3;
            lblFavouritePlayers.Text = "Favourite players";
            // 
            // btnRankLists
            // 
            btnRankLists.Location = new Point(368, 3);
            btnRankLists.Name = "btnRankLists";
            btnRankLists.Size = new Size(128, 23);
            btnRankLists.TabIndex = 4;
            btnRankLists.Text = "Rank lists";
            btnRankLists.UseVisualStyleBackColor = true;
            btnRankLists.Click += btnRankLists_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(234, 3);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(128, 23);
            btnSettings.TabIndex = 5;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // FavouritePlayers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1012, 505);
            Controls.Add(btnSettings);
            Controls.Add(btnRankLists);
            Controls.Add(lblFavouritePlayers);
            Controls.Add(lblAllPlayers);
            Controls.Add(flpFavouritePlayers);
            Controls.Add(flpAllPlayers);
            Name = "FavouritePlayers";
            Text = "FavouritePlayers";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpAllPlayers;
        private FlowLayoutPanel flpFavouritePlayers;
        private Label lblAllPlayers;
        private Label lblFavouritePlayers;
        private Button btnRankLists;
        private Button btnSettings;
    }
}