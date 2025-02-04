namespace Forms
{
    partial class RankLists
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
            cbRankBy = new ComboBox();
            lblRankBy = new Label();
            btnSettings = new Button();
            btnPrint = new Button();
            lbRankings = new ListBox();
            SuspendLayout();
            // 
            // cbRankBy
            // 
            cbRankBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRankBy.FormattingEnabled = true;
            cbRankBy.Location = new Point(54, 33);
            cbRankBy.Name = "cbRankBy";
            cbRankBy.Size = new Size(201, 23);
            cbRankBy.TabIndex = 1;
            cbRankBy.SelectedIndexChanged += cbRankBy_SelectedIndexChanged;
            // 
            // lblRankBy
            // 
            lblRankBy.AutoSize = true;
            lblRankBy.Font = new Font("Segoe UI", 9F);
            lblRankBy.Location = new Point(12, 36);
            lblRankBy.Name = "lblRankBy";
            lblRankBy.Size = new Size(36, 15);
            lblRankBy.TabIndex = 2;
            lblRankBy.Text = "Rank:";
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(466, 33);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(106, 23);
            btnSettings.TabIndex = 3;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(354, 33);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(106, 23);
            btnPrint.TabIndex = 4;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // lbRankings
            // 
            lbRankings.FormattingEnabled = true;
            lbRankings.ItemHeight = 15;
            lbRankings.Location = new Point(12, 62);
            lbRankings.Name = "lbRankings";
            lbRankings.Size = new Size(560, 289);
            lbRankings.TabIndex = 5;
            // 
            // RankLists
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            Controls.Add(lbRankings);
            Controls.Add(btnPrint);
            Controls.Add(btnSettings);
            Controls.Add(lblRankBy);
            Controls.Add(cbRankBy);
            Name = "RankLists";
            Text = "RankLists";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbRankBy;
        private Label lblRankBy;
        private Button btnSettings;
        private Button btnPrint;
        private ListBox lbRankings;
    }
}