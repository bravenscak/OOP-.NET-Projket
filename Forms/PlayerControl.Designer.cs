namespace Forms
{
    partial class PlayerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbPicture = new PictureBox();
            lblName = new Label();
            lblShirtNumber = new Label();
            lblPosition = new Label();
            lblCaptain = new Label();
            lblNameValue = new Label();
            lblShirtNumberValue = new Label();
            lblPositionValue = new Label();
            lblCaptainValue = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pbPicture
            // 
            pbPicture.Location = new Point(3, 3);
            pbPicture.Name = "pbPicture";
            pbPicture.Size = new Size(176, 225);
            pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pbPicture.TabIndex = 0;
            pbPicture.TabStop = false;
            pbPicture.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                            // 
                                                            // lblName
                                                            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F);
            lblName.Location = new Point(231, 37);
            lblName.Name = "lblName";
            lblName.Size = new Size(42, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Name:";
            lblName.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                          // 
                                                          // lblShirtNumber
                                                          // 
            lblShirtNumber.AutoSize = true;
            lblShirtNumber.Font = new Font("Segoe UI", 9F);
            lblShirtNumber.Location = new Point(194, 64);
            lblShirtNumber.Name = "lblShirtNumber";
            lblShirtNumber.Size = new Size(79, 15);
            lblShirtNumber.TabIndex = 2;
            lblShirtNumber.Text = "Shirt number:";
            lblShirtNumber.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                                 // 
                                                                 // lblPosition
                                                                 // 
            lblPosition.AutoSize = true;
            lblPosition.Font = new Font("Segoe UI", 9F);
            lblPosition.Location = new Point(220, 91);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(53, 15);
            lblPosition.TabIndex = 3;
            lblPosition.Text = "Position:";
            lblPosition.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                              // 
                                                              // lblCaptain
                                                              // 
            lblCaptain.AutoSize = true;
            lblCaptain.Font = new Font("Segoe UI", 9F);
            lblCaptain.Location = new Point(222, 118);
            lblCaptain.Name = "lblCaptain";
            lblCaptain.Size = new Size(51, 15);
            lblCaptain.TabIndex = 4;
            lblCaptain.Text = "Captain:";
            lblCaptain.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                             // 
                                                             // lblNameValue
                                                             // 
            lblNameValue.AutoSize = true;
            lblNameValue.Location = new Point(279, 37);
            lblNameValue.Name = "lblNameValue";
            lblNameValue.Size = new Size(38, 15);
            lblNameValue.TabIndex = 5;
            lblNameValue.Text = "label1";
            lblNameValue.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                               // 
                                                               // lblShirtNumberValue
                                                               // 
            lblShirtNumberValue.AutoSize = true;
            lblShirtNumberValue.Location = new Point(279, 64);
            lblShirtNumberValue.Name = "lblShirtNumberValue";
            lblShirtNumberValue.Size = new Size(38, 15);
            lblShirtNumberValue.TabIndex = 6;
            lblShirtNumberValue.Text = "label1";
            lblShirtNumberValue.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                                      // 
                                                                      // lblPositionValue
                                                                      // 
            lblPositionValue.AutoSize = true;
            lblPositionValue.Location = new Point(279, 91);
            lblPositionValue.Name = "lblPositionValue";
            lblPositionValue.Size = new Size(38, 15);
            lblPositionValue.TabIndex = 7;
            lblPositionValue.Text = "label1";
            lblPositionValue.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                                   // 
                                                                   // lblCaptainValue
                                                                   // 
            lblCaptainValue.AutoSize = true;
            lblCaptainValue.Location = new Point(279, 118);
            lblCaptainValue.Name = "lblCaptainValue";
            lblCaptainValue.Size = new Size(38, 15);
            lblCaptainValue.TabIndex = 8;
            lblCaptainValue.Text = "label1";
            lblCaptainValue.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                                  // 
                                                                  // pictureBox1
                                                                  // 
            pictureBox1.Image = Properties.Resources.star_png_image_pngfre_2;
            pictureBox1.Location = new Point(446, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 35);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
                                                              // 
                                                              // PlayerControl
                                                              // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox1);
            Controls.Add(lblCaptainValue);
            Controls.Add(lblPositionValue);
            Controls.Add(lblShirtNumberValue);
            Controls.Add(lblNameValue);
            Controls.Add(lblCaptain);
            Controls.Add(lblPosition);
            Controls.Add(lblShirtNumber);
            Controls.Add(lblName);
            Controls.Add(pbPicture);
            Name = "PlayerControl";
            Size = new Size(484, 231);
            MouseDown += PlayerControl_MouseDown; // Attach MouseDown event
            ((System.ComponentModel.ISupportInitialize)pbPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private PictureBox pbPicture;
        private Label lblName;
        private Label lblShirtNumber;
        private Label lblPosition;
        private Label lblCaptain;
        private Label lblNameValue;
        private Label lblShirtNumberValue;
        private Label lblPositionValue;
        private Label lblCaptainValue;
        private PictureBox pictureBox1;
    }
}
