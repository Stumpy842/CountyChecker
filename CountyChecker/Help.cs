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

namespace CountyChecker
{
    public partial class Help : Form
    {
        private const string ImportInstructionsFilename = "HelpFile.txt";
        private static readonly string title = Form1.AppName;

        public Help()
        {
            InitializeComponent();
            Text = title + " Help";
        }

        private void Help_Load(object sender, EventArgs e)
        {
            lbHelp.Text = "";
            if (Tools.GetTextFromResourceFile(ImportInstructionsFilename, out string text))
                lbHelp.Text = text.Replace("|title|", title);
            else
                Tools.TimedMessage(text, title, this);
        }

        private void Help_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { e.Handled = true; Close(); }
        }
    }
}
