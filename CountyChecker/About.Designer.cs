/*
CountyChecker report generator
© Steven J. Stover, 2025

This program is free software: you can redistribute it and/or modify it under the terms of the
GNU Affero General Public License as published by the Free Software Foundation, either version 3 
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even
the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General
Public License for more details.

You should have received a copy of the GNU Affero General Public License along with this program.
If not, see <https://www.gnu.org/licenses/>.
*/
namespace CountyChecker
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            lbTitle = new Label();
            lbInfo = new Label();
            pictureBox1 = new PictureBox();
            btOK = new Button();
            lbGitHubPageLink = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(12, 63);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(57, 21);
            lbTitle.TabIndex = 0;
            lbTitle.Text = "label1";
            // 
            // lbInfo
            // 
            lbInfo.AutoSize = true;
            lbInfo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbInfo.Location = new Point(32, 103);
            lbInfo.Name = "lbInfo";
            lbInfo.Size = new Size(41, 17);
            lbInfo.TabIndex = 1;
            lbInfo.Text = "label1";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.CC;
            pictureBox1.Location = new Point(241, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 256);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btOK
            // 
            btOK.Location = new Point(85, 257);
            btOK.Name = "btOK";
            btOK.Size = new Size(75, 23);
            btOK.TabIndex = 3;
            btOK.Text = "OK";
            btOK.UseVisualStyleBackColor = true;
            btOK.Click += btOK_Click;
            // 
            // lbGitHubPageLink
            // 
            lbGitHubPageLink.AutoSize = true;
            lbGitHubPageLink.Cursor = Cursors.Hand;
            lbGitHubPageLink.ForeColor = SystemColors.HotTrack;
            lbGitHubPageLink.Location = new Point(32, 295);
            lbGitHubPageLink.Name = "lbGitHubPageLink";
            lbGitHubPageLink.Size = new Size(38, 15);
            lbGitHubPageLink.TabIndex = 4;
            lbGitHubPageLink.Text = "label1";
            lbGitHubPageLink.Click += lbGitHubPageLink_Click;
            // 
            // About
            // 
            AcceptButton = btOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 325);
            Controls.Add(lbGitHubPageLink);
            Controls.Add(btOK);
            Controls.Add(pictureBox1);
            Controls.Add(lbInfo);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            KeyDown += About_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitle;
        private Label lbInfo;
        private PictureBox pictureBox1;
        private Button btOK;
        private Label lbGitHubPageLink;
    }
}