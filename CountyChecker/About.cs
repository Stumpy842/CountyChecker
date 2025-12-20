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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace CountyChecker
{
    public partial class About : Form
    {
        private const string GitText = @"https://github.com/Stumpy842/CountyChecker";
        private readonly string nl = Environment.NewLine;
        public About()
        {
            InitializeComponent();
            lbTitle.Text = Form1.AppName + " v" + Form1.AppVersion;
            lbInfo.Text = $"© Steven J. Stover, 2025{nl}" +
                $"Licensed under GNU Affero{nl}General Public License v3.0{nl}{nl}" +
                $"    (Not affiliated with{nl}    RootsMagic, Inc.)";
            Text = $"About {Form1.AppName}";
            lbGitHubPageLink.Text = GitText;
        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbGitHubPageLink_Click(object sender, EventArgs e)
        {
            try
            {
                using Process p = Process.Start(new ProcessStartInfo(GitText) { UseShellExecute = true })!;
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show($@"Cannot open page {GitText}{nl}{ex.Message}", Form1.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
