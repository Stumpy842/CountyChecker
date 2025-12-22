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
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            btOpenFile = new Button();
            toolTip1 = new ToolTip(components);
            btGO = new Button();
            btCancel = new Button();
            lbInFile = new Label();
            lbOutFile = new Label();
            label3 = new Label();
            tbRec = new TextBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            lbProgress = new Label();
            label5 = new Label();
            lbCount = new Label();
            lbSugsDesc = new Label();
            label7 = new Label();
            lbSuggsestions = new Label();
            lbErrors = new Label();
            label4 = new Label();
            lbRecords = new Label();
            label8 = new Label();
            lbRecsOut = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            ignoreListToolStripMenuItem = new ToolStripMenuItem();
            editIgnoreListToolStripMenuItem = new ToolStripMenuItem();
            disableIgnoreListToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            countyCheckerHelpToolStripMenuItem = new ToolStripMenuItem();
            aboutCountyCheckerToolStripMenuItem = new ToolStripMenuItem();
            label9 = new Label();
            lbLinesOut = new Label();
            lbMatchesDesc = new Label();
            label10 = new Label();
            lbMatches = new Label();
            lbTime = new Label();
            pictureBox1 = new PictureBox();
            lbIgnoredDesc = new Label();
            lbIgnored = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 56);
            label1.Name = "label1";
            label1.Size = new Size(265, 15);
            label1.TabIndex = 2;
            label1.Text = "Input file (CSV file from RM CountyCheck report)";
            // 
            // btOpenFile
            // 
            btOpenFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btOpenFile.Location = new Point(12, 54);
            btOpenFile.Name = "btOpenFile";
            btOpenFile.Size = new Size(30, 45);
            btOpenFile.TabIndex = 1;
            btOpenFile.Text = "...";
            toolTip1.SetToolTip(btOpenFile, "Select input file");
            btOpenFile.UseVisualStyleBackColor = true;
            btOpenFile.Click += btOpenFile_Click;
            // 
            // btGO
            // 
            btGO.Location = new Point(509, 46);
            btGO.Name = "btGO";
            btGO.Size = new Size(75, 53);
            btGO.TabIndex = 4;
            btGO.Text = "&Start";
            toolTip1.SetToolTip(btGO, "Process file");
            btGO.UseVisualStyleBackColor = true;
            btGO.Click += ProcessFile;
            // 
            // btCancel
            // 
            btCancel.Location = new Point(608, 46);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(75, 53);
            btCancel.TabIndex = 5;
            btCancel.Text = "&Cancel";
            toolTip1.SetToolTip(btCancel, "Cancel process");
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += CancelProcess;
            // 
            // lbInFile
            // 
            lbInFile.AutoEllipsis = true;
            lbInFile.BackColor = SystemColors.ControlLightLight;
            lbInFile.Location = new Point(48, 76);
            lbInFile.Name = "lbInFile";
            lbInFile.Size = new Size(420, 23);
            lbInFile.TabIndex = 3;
            lbInFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbOutFile
            // 
            lbOutFile.AutoEllipsis = true;
            lbOutFile.BackColor = SystemColors.ControlLightLight;
            lbOutFile.Location = new Point(48, 142);
            lbOutFile.Name = "lbOutFile";
            lbOutFile.Size = new Size(420, 23);
            lbOutFile.TabIndex = 7;
            lbOutFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(48, 122);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 6;
            label3.Text = "Output file";
            // 
            // tbRec
            // 
            tbRec.AcceptsReturn = true;
            tbRec.Location = new Point(12, 208);
            tbRec.Multiline = true;
            tbRec.Name = "tbRec";
            tbRec.Size = new Size(776, 329);
            tbRec.TabIndex = 20;
            tbRec.TabStop = false;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // lbProgress
            // 
            lbProgress.AutoEllipsis = true;
            lbProgress.BackColor = SystemColors.Window;
            lbProgress.Dock = DockStyle.Bottom;
            lbProgress.Location = new Point(0, 550);
            lbProgress.Name = "lbProgress";
            lbProgress.Padding = new Padding(10, 3, 3, 3);
            lbProgress.Size = new Size(800, 21);
            lbProgress.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(480, 120);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 8;
            label5.Text = "Lines In";
            // 
            // lbCount
            // 
            lbCount.AutoEllipsis = true;
            lbCount.BackColor = SystemColors.Control;
            lbCount.Location = new Point(540, 120);
            lbCount.Name = "lbCount";
            lbCount.Size = new Size(64, 15);
            lbCount.TabIndex = 9;
            // 
            // lbSugsDesc
            // 
            lbSugsDesc.AutoSize = true;
            lbSugsDesc.Location = new Point(612, 160);
            lbSugsDesc.Name = "lbSugsDesc";
            lbSugsDesc.Size = new Size(71, 15);
            lbSugsDesc.TabIndex = 14;
            lbSugsDesc.Text = "Suggestions";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(612, 140);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 10;
            label7.Text = "Errors";
            // 
            // lbSuggsestions
            // 
            lbSuggsestions.AutoEllipsis = true;
            lbSuggsestions.BackColor = SystemColors.Control;
            lbSuggsestions.Location = new Point(686, 160);
            lbSuggsestions.Name = "lbSuggsestions";
            lbSuggsestions.Size = new Size(102, 15);
            lbSuggsestions.TabIndex = 15;
            // 
            // lbErrors
            // 
            lbErrors.AutoEllipsis = true;
            lbErrors.BackColor = SystemColors.Control;
            lbErrors.Location = new Point(686, 140);
            lbErrors.Name = "lbErrors";
            lbErrors.Size = new Size(102, 15);
            lbErrors.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(480, 160);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 16;
            label4.Text = "Recs In";
            // 
            // lbRecords
            // 
            lbRecords.AutoEllipsis = true;
            lbRecords.BackColor = SystemColors.Control;
            lbRecords.Location = new Point(540, 160);
            lbRecords.Name = "lbRecords";
            lbRecords.Size = new Size(64, 15);
            lbRecords.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(480, 180);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 18;
            label8.Text = "Recs Out";
            // 
            // lbRecsOut
            // 
            lbRecsOut.AutoEllipsis = true;
            lbRecsOut.BackColor = SystemColors.Control;
            lbRecsOut.Location = new Point(540, 180);
            lbRecsOut.Name = "lbRecsOut";
            lbRecsOut.Size = new Size(64, 15);
            lbRecsOut.TabIndex = 19;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, ignoreListToolStripMenuItem, settingsToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(129, 22);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += btOpenFile_Click;
            // 
            // ignoreListToolStripMenuItem
            // 
            ignoreListToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { editIgnoreListToolStripMenuItem, disableIgnoreListToolStripMenuItem });
            ignoreListToolStripMenuItem.Name = "ignoreListToolStripMenuItem";
            ignoreListToolStripMenuItem.Size = new Size(129, 22);
            ignoreListToolStripMenuItem.Text = "&Ignore List";
            // 
            // editIgnoreListToolStripMenuItem
            // 
            editIgnoreListToolStripMenuItem.Name = "editIgnoreListToolStripMenuItem";
            editIgnoreListToolStripMenuItem.Size = new Size(170, 22);
            editIgnoreListToolStripMenuItem.Text = "&Edit Ignore List";
            editIgnoreListToolStripMenuItem.Click += editIgnoreListToolStripMenuItem_Click;
            // 
            // disableIgnoreListToolStripMenuItem
            // 
            disableIgnoreListToolStripMenuItem.Name = "disableIgnoreListToolStripMenuItem";
            disableIgnoreListToolStripMenuItem.Size = new Size(170, 22);
            disableIgnoreListToolStripMenuItem.Text = "&Disable Ignore List";
            disableIgnoreListToolStripMenuItem.Click += disableIgnoreListToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(129, 22);
            settingsToolStripMenuItem.Text = "&Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(129, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { countyCheckerHelpToolStripMenuItem, aboutCountyCheckerToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // countyCheckerHelpToolStripMenuItem
            // 
            countyCheckerHelpToolStripMenuItem.Name = "countyCheckerHelpToolStripMenuItem";
            countyCheckerHelpToolStripMenuItem.Size = new Size(127, 22);
            countyCheckerHelpToolStripMenuItem.Text = "&View Help";
            countyCheckerHelpToolStripMenuItem.Click += countyCheckerHelpToolStripMenuItem_Click;
            // 
            // aboutCountyCheckerToolStripMenuItem
            // 
            aboutCountyCheckerToolStripMenuItem.Name = "aboutCountyCheckerToolStripMenuItem";
            aboutCountyCheckerToolStripMenuItem.Size = new Size(127, 22);
            aboutCountyCheckerToolStripMenuItem.Text = "&About";
            aboutCountyCheckerToolStripMenuItem.Click += aboutCountyCheckerToolStripMenuItem_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(480, 140);
            label9.Name = "label9";
            label9.Size = new Size(57, 15);
            label9.TabIndex = 8;
            label9.Text = "Lines Out";
            // 
            // lbLinesOut
            // 
            lbLinesOut.AutoEllipsis = true;
            lbLinesOut.BackColor = SystemColors.Control;
            lbLinesOut.Location = new Point(540, 140);
            lbLinesOut.Name = "lbLinesOut";
            lbLinesOut.Size = new Size(64, 15);
            lbLinesOut.TabIndex = 19;
            // 
            // lbMatchesDesc
            // 
            lbMatchesDesc.AutoSize = true;
            lbMatchesDesc.Location = new Point(612, 120);
            lbMatchesDesc.Name = "lbMatchesDesc";
            lbMatchesDesc.Size = new Size(52, 15);
            lbMatchesDesc.TabIndex = 21;
            lbMatchesDesc.Text = "Matches";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(332, 180);
            label10.Name = "label10";
            label10.Size = new Size(34, 15);
            label10.TabIndex = 22;
            label10.Text = "Time";
            // 
            // lbMatches
            // 
            lbMatches.Location = new Point(686, 120);
            lbMatches.Name = "lbMatches";
            lbMatches.Size = new Size(102, 15);
            lbMatches.TabIndex = 23;
            // 
            // lbTime
            // 
            lbTime.AutoEllipsis = true;
            lbTime.Location = new Point(372, 180);
            lbTime.Name = "lbTime";
            lbTime.Size = new Size(102, 15);
            lbTime.TabIndex = 24;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.CC;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(714, 46);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(64, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            // 
            // lbIgnoredDesc
            // 
            lbIgnoredDesc.AutoSize = true;
            lbIgnoredDesc.Location = new Point(612, 180);
            lbIgnoredDesc.Name = "lbIgnoredDesc";
            lbIgnoredDesc.Size = new Size(48, 15);
            lbIgnoredDesc.TabIndex = 14;
            lbIgnoredDesc.Text = "Ignored";
            // 
            // lbIgnored
            // 
            lbIgnored.AutoEllipsis = true;
            lbIgnored.BackColor = SystemColors.Control;
            lbIgnored.Location = new Point(686, 180);
            lbIgnored.Name = "lbIgnored";
            lbIgnored.Size = new Size(102, 15);
            lbIgnored.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 571);
            Controls.Add(pictureBox1);
            Controls.Add(lbTime);
            Controls.Add(lbMatches);
            Controls.Add(label10);
            Controls.Add(lbMatchesDesc);
            Controls.Add(label4);
            Controls.Add(lbErrors);
            Controls.Add(lbLinesOut);
            Controls.Add(lbRecsOut);
            Controls.Add(lbIgnored);
            Controls.Add(lbSuggsestions);
            Controls.Add(lbCount);
            Controls.Add(lbRecords);
            Controls.Add(lbProgress);
            Controls.Add(label7);
            Controls.Add(label9);
            Controls.Add(label5);
            Controls.Add(label8);
            Controls.Add(lbIgnoredDesc);
            Controls.Add(lbSugsDesc);
            Controls.Add(tbRec);
            Controls.Add(lbOutFile);
            Controls.Add(lbInFile);
            Controls.Add(btCancel);
            Controls.Add(btGO);
            Controls.Add(btOpenFile);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button btOpenFile;
        private ToolTip toolTip1;
        private Button btGO;
        private Label lbInFile;
        private Label lbOutFile;
        private Label label3;
        private TextBox tbRec;
        private Button btCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label5;
        private Label lbSugsDesc;
        private Label label7;
        private Label label4;
        private Label label8;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem countyCheckerHelpToolStripMenuItem;
        private ToolStripMenuItem aboutCountyCheckerToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        internal Label lbProgress;
        internal Label lbCount;
        internal Label lbSuggsestions;
        internal Label lbRecords;
        internal Label lbRecsOut;
        internal Label lbErrors;
        private Label label9;
        internal Label lbLinesOut;
        private Label lbMatchesDesc;
        private Label label10;
        internal Label lbMatches;
        internal Label lbTime;
        private PictureBox pictureBox1;
        private ToolStripMenuItem ignoreListToolStripMenuItem;
        private Label lbIgnoredDesc;
        internal Label lbIgnored;
        private ToolStripMenuItem editIgnoreListToolStripMenuItem;
        private ToolStripMenuItem disableIgnoreListToolStripMenuItem;
    }
}
