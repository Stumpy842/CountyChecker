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
using DukeBlazeLauncher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountyChecker
{
    public partial class IgnoreListWindow : Form
    {
        public IgnoreListWindow()
        {
            InitializeComponent();
            IgnoreList.Init(this);
            IgnoreList.Load();
            Text = $"{Form1.AppName} Ignore List";
            UpdateRemoveButton(0);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            using InputDialog inputDialog = new();
            DialogResult ans = inputDialog.ShowDialog(out string Val);
            if (ans != DialogResult.OK) { return; }
            lbxIgnore.Items.Add(Val);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            using InputDialog inputDialog = new();
            DialogResult ans = inputDialog.ShowDialog(out string Val, lbxIgnore.SelectedItems[0]!.ToString()!,
                "Edit line from Ignore List", "Edit Line");
            if (ans != DialogResult.OK) { return; }
            int i = lbxIgnore.SelectedIndices[0];
            if (i >= 0) { lbxIgnore.Items[i] = Val; }
        }

        private void UpdateRemoveButton(int count)
        {
            btRemove.Enabled = count > 0;
            btEdit.Enabled = count == 1;
        }

        private void IgnoreList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SaveIt();
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void SaveIt()
        {
            IgnoreList.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveIt();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btClearAll_Click(object sender, EventArgs e)
        {
            lbxIgnore.Items.Clear();
            UpdateRemoveButton(0);
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            for (int i = lbxIgnore.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int index = lbxIgnore.SelectedIndices[i];
                lbxIgnore.Items.RemoveAt(index);
            }
            UpdateRemoveButton(0);
        }

        private void lbxIgnore_SelectionCountChanged(object sender, SelectionCountChangedEventArgs e)
        {
            UpdateRemoveButton(e.SelectedCount);
        }
    }
}
