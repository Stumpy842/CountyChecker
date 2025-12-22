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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CountyChecker
{
    public static class Settings
    {
        private static readonly string SettingsFilePath = Form1.myPath + "Settings.cc";
        private static readonly string DefaultReportsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private static readonly string title = Form1.AppName;
        private static SettingsWindow? _settingsWindow = null;
        public static SettingsData CurrentSettings { get; private set; } = new();
        public enum PAF { Prepend, Append, Fixed}
        public enum SugsLevel { All, Num, None }

        public static void Init(SettingsWindow settingsWindow)
        {
            _settingsWindow = settingsWindow;
        }

        private static  PAF GetPAF()
        {
            if (_settingsWindow!.rbPrepend.Checked) return PAF.Prepend;
            if (_settingsWindow!.rbAppend.Checked) return PAF.Append;
            return PAF.Fixed;
        }

        private static SugsLevel GetSugsLevel()
        {
            if (_settingsWindow!.rbSugsAll.Checked) return SugsLevel.All;
            if (_settingsWindow!.rbSugsNum.Checked) return SugsLevel.Num;
            return SugsLevel.None;
        }
        public static void Save()
        {
            string text = _settingsWindow!.tbReportFolder.Text.Trim();
            CurrentSettings.ReportsPath = text.Length > 0 ? text : DefaultReportsPath;
            text = _settingsWindow!.tbOutputPath.Text.Trim();
            CurrentSettings.OutputPath = text.Length > 0 ? text : DefaultReportsPath;
            CurrentSettings.OutUseRep = _settingsWindow.cbOutUseRepFolder.Checked;
            CurrentSettings.NameStyle = GetPAF();
            CurrentSettings.SugsLvl = GetSugsLevel();
            CurrentSettings.SugsNUD = Decimal.ToInt32(_settingsWindow.nudSugs.Value);
            CurrentSettings.OutName = _settingsWindow.tbName.Text.Trim(); ;
            CurrentSettings.AutoName = _settingsWindow.cbAutoName.Checked;
            CurrentSettings.NoPromptOverwrite = _settingsWindow.cbPromptOverwrite.Checked;
            CurrentSettings.DeleteOnCancel = _settingsWindow.cbDeleteOnCancel.Checked;
            CurrentSettings.ViewOutput = _settingsWindow.cbViewOutput.Checked;
            CurrentSettings.OmitMatch = _settingsWindow.cbOmitMatch.Checked;

            try
            {
                if (!File.Exists(SettingsFilePath)) File.WriteAllText(Tools.GetRelativePath(SettingsFilePath), String.Empty);
                string savedText = JsonConvert.SerializeObject(CurrentSettings);
                File.WriteAllText(SettingsFilePath, savedText);
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Error saving settings", $"{title} - File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Debug.WriteLine($"***{ex}");
            }
        }

        public static void Load()
        {
            SettingsData? newItems = null;
            try
            {
                if (!File.Exists(SettingsFilePath)) File.WriteAllText(SettingsFilePath, String.Empty);
                var fileText = File.ReadAllText(SettingsFilePath);
                newItems = JsonConvert.DeserializeObject<SettingsData>(fileText);
            }
            catch (Exception ex)
            {
                if (_settingsWindow is not null)
                {
                    Tools.ShowTaskDlg(null!, title, "Settings File Error", "There was an error loading or creating the Settings",
                        ex.Message);
                }
                //Debug.WriteLine($"***{ex}");
            }
            finally
            {
                if (newItems is not null)
                {
                    CurrentSettings = newItems;
                }
                else
                {
                    if (_settingsWindow is not null)
                    {
                        Tools.TimedMessage("Error loading settings, using defaults.", $"{title} - File Load Error");
                    }
                }

                if (_settingsWindow is not null)
                {
                    _settingsWindow.tbReportFolder.Text = CurrentSettings.ReportsPath;
                    _settingsWindow.tbOutputPath.Text = CurrentSettings.OutputPath;
                    _settingsWindow.cbOutUseRepFolder.Checked = CurrentSettings.OutUseRep;
                    _settingsWindow.rbPrepend.Checked = CurrentSettings.NameStyle == PAF.Prepend;
                    _settingsWindow.rbAppend.Checked = CurrentSettings.NameStyle == PAF.Append;
                    _settingsWindow.rbFixed.Checked = CurrentSettings.NameStyle == PAF.Fixed;
                    _settingsWindow.rbSugsAll.Checked = CurrentSettings.SugsLvl == SugsLevel.All;
                    _settingsWindow.rbSugsNum.Checked = CurrentSettings.SugsLvl == SugsLevel.Num;
                    _settingsWindow.rbSugsNone.Checked = CurrentSettings.SugsLvl == SugsLevel.None;
                    _settingsWindow.nudSugs.Value = CurrentSettings.SugsNUD;
                    _settingsWindow.tbName.Text = CurrentSettings.OutName;
                    _settingsWindow.cbAutoName.Checked = CurrentSettings.AutoName;
                    _settingsWindow.cbPromptOverwrite.Checked = CurrentSettings.NoPromptOverwrite;
                    _settingsWindow.cbDeleteOnCancel.Checked = CurrentSettings.DeleteOnCancel;
                    _settingsWindow.cbViewOutput.Checked = CurrentSettings.ViewOutput;
                    _settingsWindow.cbOmitMatch.Checked = CurrentSettings.OmitMatch;
                }
            }
        }

        [Serializable]
        public class SettingsData
        {
            public string ReportsPath { get; set; } = Settings.DefaultReportsPath;
            public string OutputPath { get; set; } = Settings.DefaultReportsPath;
            public bool OutUseRep { get; set; } = true;
            public PAF NameStyle {  get; set; } = PAF.Prepend;
            public SugsLevel SugsLvl { get; set; } = SugsLevel.All;
            public int SugsNUD { get; set; } = 1;
            public string OutName { get; set; } = "Report";
            public bool AutoName { get; set; } = true;
            public bool NoPromptOverwrite { get; set; } = false;
            public bool DeleteOnCancel { get; set; } = false;
            public bool ViewOutput { get; set; } = true;
            public bool OmitMatch {  get; set; } = false;
        }
    }
}
