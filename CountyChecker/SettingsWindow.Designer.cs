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
    partial class SettingsWindow
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            label1 = new Label();
            tbReportFolder = new TextBox();
            btReportFolder = new Button();
            cbAutoName = new CheckBox();
            toolTip1 = new ToolTip(components);
            tbName = new TextBox();
            cbPromptOverwrite = new CheckBox();
            cbDeleteOnCancel = new CheckBox();
            cbViewOutput = new CheckBox();
            cbOmitMatch = new CheckBox();
            gbNaming = new GroupBox();
            rbFixed = new RadioButton();
            rbAppend = new RadioButton();
            rbPrepend = new RadioButton();
            gbReportOptions = new GroupBox();
            label2 = new Label();
            nudSugs = new NumericUpDown();
            rbSugsNone = new RadioButton();
            rbSugsNum = new RadioButton();
            rbSugsAll = new RadioButton();
            btSave = new Button();
            btCancel = new Button();
            label3 = new Label();
            tbOutputPath = new TextBox();
            btOutputFolder = new Button();
            cbOutUseRepFolder = new CheckBox();
            gbNaming.SuspendLayout();
            gbReportOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSugs).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 0;
            label1.Text = "RM Reports Folder";
            // 
            // tbReportFolder
            // 
            tbReportFolder.Location = new Point(12, 43);
            tbReportFolder.Name = "tbReportFolder";
            tbReportFolder.Size = new Size(408, 23);
            tbReportFolder.TabIndex = 1;
            toolTip1.SetToolTip(tbReportFolder, "The path to your Reports folder");
            // 
            // btReportFolder
            // 
            btReportFolder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btReportFolder.Location = new Point(426, 41);
            btReportFolder.Name = "btReportFolder";
            btReportFolder.Size = new Size(32, 28);
            btReportFolder.TabIndex = 2;
            btReportFolder.Text = "...";
            toolTip1.SetToolTip(btReportFolder, "Click to open the Folder Browser");
            btReportFolder.UseVisualStyleBackColor = true;
            btReportFolder.Click += btReportFolder_Click;
            // 
            // cbAutoName
            // 
            cbAutoName.AutoSize = true;
            cbAutoName.Location = new Point(10, 76);
            cbAutoName.Name = "cbAutoName";
            cbAutoName.Size = new Size(143, 19);
            cbAutoName.TabIndex = 4;
            cbAutoName.Text = "Auto name output file";
            toolTip1.SetToolTip(cbAutoName, "If checked, the ouput file wil be auto named using the entries in Naming Style");
            cbAutoName.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            tbName.Location = new Point(10, 47);
            tbName.Name = "tbName";
            tbName.Size = new Size(425, 23);
            tbName.TabIndex = 3;
            toolTip1.SetToolTip(tbName, "String to apply to output filename");
            tbName.TextChanged += tbName_TextChanged;
            // 
            // cbPromptOverwrite
            // 
            cbPromptOverwrite.AutoSize = true;
            cbPromptOverwrite.Location = new Point(198, 76);
            cbPromptOverwrite.Name = "cbPromptOverwrite";
            cbPromptOverwrite.Size = new Size(233, 19);
            cbPromptOverwrite.TabIndex = 5;
            cbPromptOverwrite.Text = "Do not prompt to overwrite existing file";
            toolTip1.SetToolTip(cbPromptOverwrite, "If checked, will overwrite an existing file with the same name without warning");
            cbPromptOverwrite.UseVisualStyleBackColor = true;
            // 
            // cbDeleteOnCancel
            // 
            cbDeleteOnCancel.AutoSize = true;
            cbDeleteOnCancel.Location = new Point(230, 22);
            cbDeleteOnCancel.Name = "cbDeleteOnCancel";
            cbDeleteOnCancel.Size = new Size(173, 19);
            cbDeleteOnCancel.TabIndex = 1;
            cbDeleteOnCancel.Text = "Delete output file on Cancel";
            toolTip1.SetToolTip(cbDeleteOnCancel, "If checked, the output file will be deleted if you click the Cancel button");
            cbDeleteOnCancel.UseVisualStyleBackColor = true;
            // 
            // cbViewOutput
            // 
            cbViewOutput.AutoSize = true;
            cbViewOutput.Location = new Point(230, 52);
            cbViewOutput.Name = "cbViewOutput";
            cbViewOutput.Size = new Size(173, 19);
            cbViewOutput.TabIndex = 3;
            cbViewOutput.Text = "View output file after saving";
            toolTip1.SetToolTip(cbViewOutput, "Open output file in default CSV file editor");
            cbViewOutput.UseVisualStyleBackColor = true;
            // 
            // cbOmitMatch
            // 
            cbOmitMatch.AutoSize = true;
            cbOmitMatch.Location = new Point(230, 82);
            cbOmitMatch.Name = "cbOmitMatch";
            cbOmitMatch.Size = new Size(201, 19);
            cbOmitMatch.TabIndex = 2;
            cbOmitMatch.Text = "Omit Events with matching Place";
            toolTip1.SetToolTip(cbOmitMatch, "If checked, output will not contain lines which match a known place");
            cbOmitMatch.UseVisualStyleBackColor = true;
            // 
            // gbNaming
            // 
            gbNaming.Controls.Add(cbPromptOverwrite);
            gbNaming.Controls.Add(rbFixed);
            gbNaming.Controls.Add(tbName);
            gbNaming.Controls.Add(rbAppend);
            gbNaming.Controls.Add(rbPrepend);
            gbNaming.Controls.Add(cbAutoName);
            gbNaming.Location = new Point(12, 138);
            gbNaming.Name = "gbNaming";
            gbNaming.Size = new Size(446, 106);
            gbNaming.TabIndex = 3;
            gbNaming.TabStop = false;
            gbNaming.Text = "Output File Naming Style";
            // 
            // rbFixed
            // 
            rbFixed.AutoSize = true;
            rbFixed.Location = new Point(326, 22);
            rbFixed.Name = "rbFixed";
            rbFixed.Size = new Size(105, 19);
            rbFixed.TabIndex = 2;
            rbFixed.TabStop = true;
            rbFixed.Text = "Use fixed name";
            rbFixed.UseVisualStyleBackColor = true;
            // 
            // rbAppend
            // 
            rbAppend.AutoSize = true;
            rbAppend.Location = new Point(169, 22);
            rbAppend.Name = "rbAppend";
            rbAppend.Size = new Size(149, 19);
            rbAppend.TabIndex = 1;
            rbAppend.TabStop = true;
            rbAppend.Text = "Append to intput name";
            rbAppend.UseVisualStyleBackColor = true;
            // 
            // rbPrepend
            // 
            rbPrepend.AutoSize = true;
            rbPrepend.Location = new Point(10, 22);
            rbPrepend.Name = "rbPrepend";
            rbPrepend.Size = new Size(151, 19);
            rbPrepend.TabIndex = 0;
            rbPrepend.TabStop = true;
            rbPrepend.Text = "Prepend to intput name";
            rbPrepend.UseVisualStyleBackColor = true;
            // 
            // gbReportOptions
            // 
            gbReportOptions.Controls.Add(cbViewOutput);
            gbReportOptions.Controls.Add(label2);
            gbReportOptions.Controls.Add(nudSugs);
            gbReportOptions.Controls.Add(cbOmitMatch);
            gbReportOptions.Controls.Add(rbSugsNone);
            gbReportOptions.Controls.Add(rbSugsNum);
            gbReportOptions.Controls.Add(rbSugsAll);
            gbReportOptions.Controls.Add(cbDeleteOnCancel);
            gbReportOptions.Location = new Point(12, 256);
            gbReportOptions.Name = "gbReportOptions";
            gbReportOptions.Size = new Size(446, 113);
            gbReportOptions.TabIndex = 4;
            gbReportOptions.TabStop = false;
            gbReportOptions.Text = "Report Options";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(72, 52);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 6;
            label2.Text = "Suggestions per Event";
            // 
            // nudSugs
            // 
            nudSugs.Location = new Point(30, 50);
            nudSugs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSugs.Name = "nudSugs";
            nudSugs.Size = new Size(40, 23);
            nudSugs.TabIndex = 5;
            nudSugs.TextAlign = HorizontalAlignment.Right;
            nudSugs.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // rbSugsNone
            // 
            rbSugsNone.AutoSize = true;
            rbSugsNone.Location = new Point(10, 82);
            rbSugsNone.Name = "rbSugsNone";
            rbSugsNone.Size = new Size(154, 19);
            rbSugsNone.TabIndex = 4;
            rbSugsNone.TabStop = true;
            rbSugsNone.Text = "Do not save Suggestions";
            rbSugsNone.UseVisualStyleBackColor = true;
            // 
            // rbSugsNum
            // 
            rbSugsNum.AutoSize = true;
            rbSugsNum.Location = new Point(10, 52);
            rbSugsNum.Name = "rbSugsNum";
            rbSugsNum.Size = new Size(28, 19);
            rbSugsNum.TabIndex = 4;
            rbSugsNum.TabStop = true;
            rbSugsNum.Text = " ";
            rbSugsNum.UseVisualStyleBackColor = true;
            rbSugsNum.CheckedChanged += rbSugsNum_CheckedChanged;
            // 
            // rbSugsAll
            // 
            rbSugsAll.AutoSize = true;
            rbSugsAll.Location = new Point(10, 22);
            rbSugsAll.Name = "rbSugsAll";
            rbSugsAll.Size = new Size(131, 19);
            rbSugsAll.TabIndex = 4;
            rbSugsAll.TabStop = true;
            rbSugsAll.Text = "Save all Suggestions";
            rbSugsAll.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            btSave.Location = new Point(131, 386);
            btSave.Name = "btSave";
            btSave.Size = new Size(80, 40);
            btSave.TabIndex = 5;
            btSave.Text = "Save";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // btCancel
            // 
            btCancel.Location = new Point(262, 386);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(80, 40);
            btCancel.TabIndex = 6;
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 79);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 7;
            label3.Text = "Output Folder";
            // 
            // tbOutputPath
            // 
            tbOutputPath.Location = new Point(12, 100);
            tbOutputPath.Name = "tbOutputPath";
            tbOutputPath.Size = new Size(408, 23);
            tbOutputPath.TabIndex = 8;
            toolTip1.SetToolTip(tbOutputPath, "The path for your Output reports");
            // 
            // btOutputFolder
            // 
            btOutputFolder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btOutputFolder.Location = new Point(426, 98);
            btOutputFolder.Name = "btOutputFolder";
            btOutputFolder.Size = new Size(32, 28);
            btOutputFolder.TabIndex = 2;
            btOutputFolder.Text = "...";
            btOutputFolder.UseVisualStyleBackColor = true;
            btOutputFolder.Click += btOutputFolder_Click;
            // 
            // cbOutUseRepFolder
            // 
            cbOutUseRepFolder.AutoSize = true;
            cbOutUseRepFolder.Location = new Point(125, 79);
            cbOutUseRepFolder.Name = "cbOutUseRepFolder";
            cbOutUseRepFolder.Size = new Size(124, 19);
            cbOutUseRepFolder.TabIndex = 9;
            cbOutUseRepFolder.Text = "Use Reports Folder";
            cbOutUseRepFolder.UseVisualStyleBackColor = true;
            cbOutUseRepFolder.CheckedChanged += cbOutUseRepFolder_CheckedChanged;
            // 
            // SettingsWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 443);
            Controls.Add(cbOutUseRepFolder);
            Controls.Add(tbOutputPath);
            Controls.Add(label3);
            Controls.Add(btCancel);
            Controls.Add(btSave);
            Controls.Add(gbNaming);
            Controls.Add(btOutputFolder);
            Controls.Add(btReportFolder);
            Controls.Add(tbReportFolder);
            Controls.Add(label1);
            Controls.Add(gbReportOptions);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            KeyDown += SettingsWindow_KeyDown;
            gbNaming.ResumeLayout(false);
            gbNaming.PerformLayout();
            gbReportOptions.ResumeLayout(false);
            gbReportOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSugs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btReportFolder;
        internal TextBox tbReportFolder;
        private ToolTip toolTip1;
        private GroupBox gbNaming;
        internal RadioButton rbPrepend;
        internal RadioButton rbAppend;
        internal TextBox tbName;
        internal RadioButton rbFixed;
        internal CheckBox cbPromptOverwrite;
        private GroupBox gbReportOptions;
        internal CheckBox cbAutoName;
        internal CheckBox cbDeleteOnCancel;
        internal CheckBox cbViewOutput;
        private Button btSave;
        private Button btCancel;
        internal CheckBox cbOmitMatch;
        internal NumericUpDown nudSugs;
        private Label label2;
        internal RadioButton rbSugsNone;
        internal RadioButton rbSugsNum;
        internal RadioButton rbSugsAll;
        private Label label3;
        internal TextBox tbOutputPath;
        private Button btOutputFolder;
        internal CheckBox cbOutUseRepFolder;
    }
}