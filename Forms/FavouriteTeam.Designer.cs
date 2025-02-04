namespace Forms
{
    partial class FavouriteTeam
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
            lblFavouriteTeam = new Label();
            cbFavouriteTeam = new ComboBox();
            cbSubmitTeam = new Button();
            SuspendLayout();
            // 
            // lblFavouriteTeam
            // 
            lblFavouriteTeam.AutoSize = true;
            lblFavouriteTeam.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFavouriteTeam.Location = new Point(121, 121);
            lblFavouriteTeam.Name = "lblFavouriteTeam";
            lblFavouriteTeam.Size = new Size(116, 21);
            lblFavouriteTeam.TabIndex = 0;
            lblFavouriteTeam.Text = "Favourite team:";
            // 
            // cbFavouriteTeam
            // 
            cbFavouriteTeam.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFavouriteTeam.FormattingEnabled = true;
            cbFavouriteTeam.Location = new Point(243, 123);
            cbFavouriteTeam.Name = "cbFavouriteTeam";
            cbFavouriteTeam.Size = new Size(220, 23);
            cbFavouriteTeam.TabIndex = 1;
            cbFavouriteTeam.SelectedIndexChanged += cbFavouriteTeam_SelectedIndexChanged;
            // 
            // cbSubmitTeam
            // 
            cbSubmitTeam.Location = new Point(242, 188);
            cbSubmitTeam.Name = "cbSubmitTeam";
            cbSubmitTeam.Size = new Size(100, 35);
            cbSubmitTeam.TabIndex = 2;
            cbSubmitTeam.Text = "Submit";
            cbSubmitTeam.UseVisualStyleBackColor = true;
            cbSubmitTeam.Click += cbSubmit_Click;
            // 
            // FavouriteTeam
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(cbSubmitTeam);
            Controls.Add(cbFavouriteTeam);
            Controls.Add(lblFavouriteTeam);
            Name = "FavouriteTeam";
            Text = "FavouriteTeam";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFavouriteTeam;
        private ComboBox cbFavouriteTeam;
        private Button cbSubmitTeam;
    }
}