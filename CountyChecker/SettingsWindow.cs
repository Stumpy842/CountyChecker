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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountyChecker
{
    public partial class SettingsWindow : Form
    {
        private static readonly string title = Form1.AppName + " Settings";
        public SettingsWindow(bool Show = true)
        {
            InitializeComponent();
            if (Show)
            {
                Text = title;
                Settings.Init(this);
                Settings.Load();
            }
        }

        private bool GetFolder(string desc, ref string path, bool rep = true)
        {
            path = rep ? Settings.CurrentSettings.ReportsPath : Settings.CurrentSettings.OutputPath;
            if (!Directory.Exists(path)) { path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }
            using FolderBrowserDialog fbd = new()
            {
                Description = desc,
                InitialDirectory = path
            };
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            { path = fbd.SelectedPath; return true; }
            return false;
        }

        private void btReportFolder_Click(object sender, EventArgs e)
        {
            string retpath = "";
            if (GetFolder("Select RM Reports folder", ref retpath))
            { tbReportFolder.Text = retpath; }
        }

        private void btOutputFolder_Click(object sender, EventArgs e)
        {
            string retpath = "";
            if (GetFolder("Select Output folder", ref retpath, false))
            { tbOutputPath.Text = retpath;}
        }

        private void SaveIt()
        {
            Settings.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveIt();
        }

        private void SettingsWindow_KeyDown(object sender, KeyEventArgs e)
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

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void rbSugsNum_CheckedChanged(object sender, EventArgs e)
        {
            nudSugs.Enabled = rbSugsNum.Checked;
        }

        string i = "InputFilename";
        string e = ".csv";
        private void tbName_TextChanged(object sender, EventArgs ea)
        {
            string n = tbName.Text;
            toolTip1.SetToolTip(rbPrepend, $"{n}_{i}{e}");
            toolTip1.SetToolTip(rbAppend, $"{i}_{n}{e}");
            toolTip1.SetToolTip(rbFixed, $"{n}{e}");
        }

        private void cbOutUseRepFolder_CheckedChanged(object sender, EventArgs e)
        {
            tbOutputPath.Enabled = !cbOutUseRepFolder.Checked;
        }
    }
}
