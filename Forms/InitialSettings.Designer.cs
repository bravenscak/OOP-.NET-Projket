namespace Forms
{
    partial class InitialSettings
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
            lblLanguage = new Label();
            lblTournament = new Label();
            cbTournament = new ComboBox();
            btnSubmitSettings = new Button();
            cbLanguage = new ComboBox();
            SuspendLayout();
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLanguage.Location = new Point(158, 106);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(81, 21);
            lblLanguage.TabIndex = 0;
            lblLanguage.Text = "Language:";
            // 
            // lblTournament
            // 
            lblTournament.AutoSize = true;
            lblTournament.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTournament.Location = new Point(143, 152);
            lblTournament.Name = "lblTournament";
            lblTournament.Size = new Size(96, 21);
            lblTournament.TabIndex = 1;
            lblTournament.Text = "Tournament:";
            // 
            // cbTournament
            // 
            cbTournament.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTournament.FormattingEnabled = true;
            cbTournament.Items.AddRange(new object[] { "2018 FIFA Men's World Cup", "2019 FIFA Women's World Cup" });
            cbTournament.Location = new Point(245, 154);
            cbTournament.Name = "cbTournament";
            cbTournament.Size = new Size(196, 23);
            cbTournament.TabIndex = 2;
            // 
            // btnSubmitSettings
            // 
            btnSubmitSettings.Location = new Point(242, 203);
            btnSubmitSettings.Name = "btnSubmitSettings";
            btnSubmitSettings.Size = new Size(100, 35);
            btnSubmitSettings.TabIndex = 3;
            btnSubmitSettings.Text = "Submit";
            btnSubmitSettings.UseVisualStyleBackColor = true;
            btnSubmitSettings.Click += btnSubmit_Click;
            // 
            // cbLanguage
            // 
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { "English", "Hrvatski" });
            cbLanguage.Location = new Point(245, 108);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(196, 23);
            cbLanguage.TabIndex = 4;
            // 
            // InitialSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(cbLanguage);
            Controls.Add(btnSubmitSettings);
            Controls.Add(cbTournament);
            Controls.Add(lblTournament);
            Controls.Add(lblLanguage);
            Name = "InitialSettings";
            Text = "InitialSettings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLanguage;
        private Label lblTournament;
        private ComboBox cbTournament;
        private Button btnSubmitSettings;
        private ComboBox cbLanguage;
    }
}