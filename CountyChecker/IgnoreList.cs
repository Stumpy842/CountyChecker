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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CountyChecker.Settings;

namespace CountyChecker
{
    public static class IgnoreList
    {
        private static readonly string IgnoreListFilePath = Form1.myPath + "IgnoreList.cc";
        private static readonly string title = Form1.AppName;
        private static IgnoreListWindow? _ignoreListWindow = null;
        public static IgnoreData CurrentIgnores { get; private set; } = new();
        private static readonly string nl = Environment.NewLine;


        public static void Init(IgnoreListWindow ignoreListWindow)
        {
            _ignoreListWindow = ignoreListWindow;
        }

        public static void Save(bool editing = true)
        {
            static void MSG(string ms)
            {
                MessageBox.Show($"Error saving Ignore List{nl}{ms}",
                    $"{title} - Ignore List Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (editing)
            {
                CurrentIgnores.Ignore = [.. _ignoreListWindow!.lbxIgnore.Items.Cast<string>()];
                CurrentIgnores.MatchCase = _ignoreListWindow.cbMatchCase.Checked;
            }
            CurrentIgnores.IgnoreEnabled = Form1.optIgnoreEnabled;

            try
            {
                if (!File.Exists(IgnoreListFilePath)) { File.WriteAllText(Tools.GetRelativePath(IgnoreListFilePath), String.Empty); }
                string savedText = JsonConvert.SerializeObject(CurrentIgnores);
                File.WriteAllText(IgnoreListFilePath, savedText);
            }
            catch (Exception ex)
            {
                if (editing)
                {
                    using (new CenterWinDialog(_ignoreListWindow!)) { MSG(ex.Message); }
                }
                else MSG(ex.Message);
            }
        }

        public static void Load()
        {
            IgnoreData? newIgnores = null;
            try
            {
                if (!File.Exists(IgnoreListFilePath)) File.WriteAllText(IgnoreListFilePath, String.Empty);
                var fileText = File.ReadAllText(IgnoreListFilePath);
                newIgnores = JsonConvert.DeserializeObject<IgnoreData>(fileText);
            }
            catch (Exception ex)
            {
                if (_ignoreListWindow is not null)
                {
                    Tools.ShowTaskDlg(null!, title, "Ignore List File Error", "There was an error loading or creating the Ignore List",
                        ex.Message);
                }
            }
            finally
            {
                if (newIgnores is not null)
                {
                    CurrentIgnores = newIgnores;
                }
                else
                {
                    if (_ignoreListWindow is not null)
                    {
                        Tools.TimedMessage("Error loading Ignore List, using default empty list.", $"{title} - File Load Error");
                    }
                }

                if (_ignoreListWindow is not null)
                {
                    _ignoreListWindow.lbxIgnore.Items.AddRange([.. CurrentIgnores.Ignore.ToArray()]);
                    _ignoreListWindow.cbMatchCase.Checked = CurrentIgnores.MatchCase;
                }           
            }
        }



        [Serializable]
        public class IgnoreData
        {
            public List<string> Ignore { get; set; } = [];
            public bool IgnoreEnabled { get; set; } = true;
            public bool MatchCase { get; set; } = true;
        }
    }
}
