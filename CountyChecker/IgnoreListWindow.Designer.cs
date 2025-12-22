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
    partial class IgnoreListWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IgnoreListWindow));
            label1 = new Label();
            btAdd = new Button();
            btRemove = new Button();
            btClearAll = new Button();
            btSave = new Button();
            btCancel = new Button();
            lbxIgnore = new ListBoxEx();
            btEdit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 20);
            label1.Name = "label1";
            label1.Size = new Size(249, 15);
            label1.TabIndex = 5;
            label1.Text = "Ignore Places which contain any of these lines";
            // 
            // btAdd
            // 
            btAdd.Location = new Point(477, 40);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(113, 31);
            btAdd.TabIndex = 0;
            btAdd.Text = "Add Line";
            btAdd.UseVisualStyleBackColor = true;
            btAdd.Click += btAdd_Click;
            // 
            // btRemove
            // 
            btRemove.Location = new Point(477, 124);
            btRemove.Name = "btRemove";
            btRemove.Size = new Size(113, 31);
            btRemove.TabIndex = 1;
            btRemove.Text = "Remove Line(s)";
            btRemove.UseVisualStyleBackColor = true;
            btRemove.Click += btRemove_Click;
            // 
            // btClearAll
            // 
            btClearAll.Location = new Point(477, 166);
            btClearAll.Name = "btClearAll";
            btClearAll.Size = new Size(113, 31);
            btClearAll.TabIndex = 2;
            btClearAll.Text = "Clear All";
            btClearAll.UseVisualStyleBackColor = true;
            btClearAll.Click += btClearAll_Click;
            // 
            // btSave
            // 
            btSave.Location = new Point(477, 208);
            btSave.Name = "btSave";
            btSave.Size = new Size(113, 31);
            btSave.TabIndex = 3;
            btSave.Text = "Save";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // btCancel
            // 
            btCancel.Location = new Point(477, 250);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(113, 31);
            btCancel.TabIndex = 4;
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // lbxIgnore
            // 
            lbxIgnore.FormattingEnabled = true;
            lbxIgnore.HorizontalScrollbar = true;
            lbxIgnore.Location = new Point(30, 40);
            lbxIgnore.Name = "lbxIgnore";
            lbxIgnore.SelectionMode = SelectionMode.MultiSimple;
            lbxIgnore.Size = new Size(430, 244);
            lbxIgnore.TabIndex = 6;
            lbxIgnore.SelectionCountChanged += lbxIgnore_SelectionCountChanged;
            // 
            // btEdit
            // 
            btEdit.Location = new Point(477, 82);
            btEdit.Name = "btEdit";
            btEdit.Size = new Size(113, 31);
            btEdit.TabIndex = 7;
            btEdit.Text = "Edit Line";
            btEdit.UseVisualStyleBackColor = true;
            btEdit.Click += btEdit_Click;
            // 
            // IgnoreListWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 311);
            Controls.Add(btEdit);
            Controls.Add(lbxIgnore);
            Controls.Add(btCancel);
            Controls.Add(btSave);
            Controls.Add(btClearAll);
            Controls.Add(btRemove);
            Controls.Add(btAdd);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IgnoreListWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IgnoreList";
            KeyDown += IgnoreList_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btAdd;
        private Button btRemove;
        private Button btClearAll;
        private Button btSave;
        private Button btCancel;
        internal ListBoxEx lbxIgnore;
        private Button btEdit;
    }
}