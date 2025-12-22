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
namespace DukeBlazeLauncher
{
    partial class InputDialog
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
            lbPrompt = new Label();
            tbInput = new TextBox();
            btSave = new Button();
            btCancel = new Button();
            SuspendLayout();
            // 
            // lbPrompt
            // 
            lbPrompt.AutoSize = true;
            lbPrompt.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbPrompt.Location = new Point(14, 13);
            lbPrompt.Margin = new Padding(4, 0, 4, 0);
            lbPrompt.Name = "lbPrompt";
            lbPrompt.Size = new Size(194, 15);
            lbPrompt.TabIndex = 0;
            lbPrompt.Text = "Enter a line to add to the Ignore List";
            // 
            // tbInput
            // 
            tbInput.Location = new Point(14, 35);
            tbInput.Margin = new Padding(4, 3, 4, 3);
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(408, 23);
            tbInput.TabIndex = 1;
            tbInput.Enter += tbInput_Enter;
            // 
            // btSave
            // 
            btSave.AutoSize = true;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btSave.Location = new Point(238, 69);
            btSave.Margin = new Padding(4, 3, 4, 3);
            btSave.Name = "btSave";
            btSave.Size = new Size(88, 27);
            btSave.TabIndex = 2;
            btSave.Text = "&Save";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // btCancel
            // 
            btCancel.Location = new Point(334, 69);
            btCancel.Margin = new Padding(4, 3, 4, 3);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(88, 27);
            btCancel.TabIndex = 3;
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // InputDialog
            // 
            AcceptButton = btSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(438, 108);
            Controls.Add(btCancel);
            Controls.Add(btSave);
            Controls.Add(tbInput);
            Controls.Add(lbPrompt);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Line";
            TopMost = true;
            KeyDown += InputDialog_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbPrompt;
        private TextBox tbInput;
        private Button btSave;
        private Button btCancel;
    }
}
