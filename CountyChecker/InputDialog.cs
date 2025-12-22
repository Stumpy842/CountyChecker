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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DukeBlazeLauncher
{
    internal partial class InputDialog : Form
    {
        internal InputDialog()
        {
            InitializeComponent();
        }

        internal DialogResult ShowDialog(out string Value, string iVal = "", string lbPrompt = "Enter a line to add to the Ignore List",
            string frmTitle = "Add Line", string btText = "&Save")
        {
            InputDialog x = new();
            x.tbInput.Text = iVal;
            x.tbInput.HideSelection = true;
            x.lbPrompt.Text = lbPrompt;
            x.btSave.Text = btText;
            x.Text = frmTitle;
            DialogResult result = x.ShowDialog();
            Value = x.tbInput.Text;
            return result;
        }
        private void InputDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbInput_Enter(object sender, EventArgs e)
        {
            tbInput.SelectionStart = 0;
            tbInput.SelectionLength = 0;
        }
    }
}
