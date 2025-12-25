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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Runtime.InteropServices.Marshalling;

namespace CountyChecker
{
    public partial class Form1 : Form
    {
        internal static string myPath = Application.StartupPath;
        internal static string AppName = Assembly.GetEntryAssembly()!.GetName().Name!;
        internal static string AppVersion = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
        private string inFile = string.Empty;
        private string outFile = string.Empty;
        private long count;
        private long linesout;
        private long progress;
        private long records;
        private long recsout;
        private long errors;
        private long suggestions;
        private long sugsout;
        private long ignored;
        private long matches;
        private int males;
        private int females;
        private int unknown;
        private int malesout;
        private int femalesout;
        private int unknownout;
        private int skipped;
        private readonly string rmale = @"^.*\(M\)""$";
        private readonly string rfemale = @"^.*\(F\)""$";
        private readonly List<string> ar = [];
        private readonly List<string> rec = [];
        private readonly char dq = '"';
        private readonly string sug = "Suggestion:";
        private readonly string error = "Error:";
        private readonly string match = "Match:";
        private bool badFile;
        // Match a line starting and ending with double quotes, and optionally ending commas
        private readonly string rddq = "^\".*\",*$";
        private readonly string com2 = "^,,";
        private readonly string nl = Environment.NewLine;
        private readonly Stopwatch stopwatch = new();

        //Settings variables
        private string optReportsPath = string.Empty;
        private string optOutputPath = string.Empty;
        private bool optOutUseRep;
        private Settings.PAF optNameStyle;
        private Settings.SugsLevel optSugsLevel;
        private int optSugsNUD;
        private string optOutName = string.Empty;
        private bool optAutoName;
        private bool optViewOutput;
        private bool optDeleteOnCancel;
        private bool optNoPromptOverwrite;
        private bool optOmitMatch;

        // Ignore List
        private static List<string> IgnoreListItems = [];
        internal static bool optIgnoreEnabled;

        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = AppName;
            btGO.Enabled = false;
            btCancel.Enabled = false;
            btOpenFile.Enabled = true;
            aboutCountyCheckerToolStripMenuItem.Text = $"&About {AppName}";
            LoadSettings();
            LoadIgnoreList();
            ColorLabels();

        }

        private void LoadSettings()
        {
            Settings.Load();
            optReportsPath = Settings.CurrentSettings.ReportsPath;
            optOutputPath = Settings.CurrentSettings.OutputPath;
            optOutUseRep = Settings.CurrentSettings.OutUseRep;
            optNameStyle = Settings.CurrentSettings.NameStyle;
            optOutName = Settings.CurrentSettings.OutName;
            optAutoName = Settings.CurrentSettings.AutoName;
            optViewOutput = Settings.CurrentSettings.ViewOutput;
            optDeleteOnCancel = Settings.CurrentSettings.DeleteOnCancel;
            optSugsLevel = Settings.CurrentSettings.SugsLvl;
            optSugsNUD = Settings.CurrentSettings.SugsNUD;
            optNoPromptOverwrite = Settings.CurrentSettings.NoPromptOverwrite;
            optOmitMatch = Settings.CurrentSettings.OmitMatch;
        }

        private void LoadIgnoreList()
        {
            IgnoreList.Load();
            IgnoreListItems = IgnoreList.CurrentIgnores.Ignore;
            optIgnoreEnabled = IgnoreList.CurrentIgnores.IgnoreEnabled;
            disableIgnoreListToolStripMenuItem.Text = optIgnoreEnabled ? "&Disable Ignore List" : "&Enable Ignore List";
        }

        private void ColorLabels()
        {
            switch (optSugsLevel)
            {
                case Settings.SugsLevel.None:
                    lbSuggsestions.ForeColor = Color.Red;
                    lbSugsDesc.ForeColor = Color.Red;
                    break;
                case Settings.SugsLevel.Num:
                    lbSuggsestions.ForeColor = Color.Purple;
                    lbSugsDesc.ForeColor = Color.Purple;
                    break;
                case Settings.SugsLevel.All:
                    lbSuggsestions.ForeColor = SystemColors.ControlText;
                    lbSugsDesc.ForeColor = SystemColors.ControlText;
                    break;
            }
            if (optOmitMatch)
            {
                lbMatches.ForeColor = Color.Red;
                lbMatchesDesc.ForeColor = Color.Red;
            }
            else
            {
                lbMatches.ForeColor = SystemColors.ControlText;
                lbMatchesDesc.ForeColor = SystemColors.ControlText;
            }
            if (optIgnoreEnabled)
            {
                lbIgnored.ForeColor = SystemColors.ControlText;
                lbIgnoredDesc.ForeColor = SystemColors.ControlText;
            }
            else
            {
                lbIgnored.ForeColor = Color.Red;
                lbIgnoredDesc.ForeColor = Color.Red; ;
            }

        }

        private void btOpenFile_Click(object sender, EventArgs e)
        {
            string path = optReportsPath;
            if (!Directory.Exists(path))
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Tools.TimedMessage("Invalid RM Reports Folder name in Settings", "Error", this);
            }
            using OpenFileDialog ofd = new()
            {
                DefaultExt = "csv",
                InitialDirectory = path,
                Filter = "csv files (*.csv)|*.csv",
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                inFile = ofd.FileName;
                lbInFile.Text = inFile;
                btGO.Enabled = true;
                ActiveControl = btGO;
            }
            else
            {
                lbInFile.Text = string.Empty;
                lbOutFile.Text = string.Empty;
                btGO.Enabled = false;
            }
        }

        private void Reset(string m = "")
        {
            try
            {
                btGO.Enabled = File.Exists(inFile);
            }
            catch (Exception)
            {
                btGO.Enabled = false;
            }
            btCancel.Enabled = false;
            btOpenFile.Enabled = true;
            if (m != "") { lbProgress.Text = m; }
        }

        private void CancelProcess(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                Reset();
            }
        }

        private void ShowError(string msg)
        {
            Reset("Error");
            using (new CenterWinDialog(this))
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ProcessFile(object sender, EventArgs ea)
        {

            // Main method start
            btGO.Enabled = false;
            btOpenFile.Enabled = false;
            btCancel.Enabled = true;
            string err = string.Empty;

            count = CountLinesReader(inFile, ref err);
            if (err != "")
            {
                ShowError(err);
            }
            else
            {
                if (!getOutFile(inFile, ref outFile))
                {
                    ShowError($"Cannot get output filename from {inFile}");
                    return;
                }

                try
                {
                    if (!optNoPromptOverwrite && File.Exists(outFile))
                    {
                        DialogResult res = MessageBox.Show($"File {outFile} exists, overwrite?",
                            "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (res == DialogResult.Cancel)
                        {
                            Tools.TimedMessage("Cancelled", "", this, MessageBoxIcon.Asterisk);
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    ShowError($"Error: {e.Message}");
                    return;
                }

                if (!backgroundWorker1.IsBusy)
                {
                    tbRec.Text = string.Empty;
                    lbOutFile.Text = outFile;
                    lbCount.Text = count.ToString();
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
        /// <summary>
        ///  Gets the output filename in outf
        /// </summary>
        /// <param name="inf"></param>
        /// <param name="outf"></param>
        /// <returns>true if successful</returns>
        private bool getOutFile(string inf, ref string outf)
        {

            string Combine(string p, string n, string e)
            {
                string s;
                string o;
                string u = "_";
                switch (optNameStyle)
                {
                    case Settings.PAF.Prepend:
                        o = optOutName.EndsWith(u) ? optOutName : optOutName + u;
                        s = Path.Combine(p, $"{o}{n}{e}");
                        break;
                    case Settings.PAF.Append:
                        o = optOutName.StartsWith(u) ? optOutName : u + optOutName;
                        s = Path.Combine(p, $"{n}{o}{e}");
                        break;
                    default:
                        s = Path.Combine(p, optOutName + e);
                        break;
                }
                return s;
            }

            // Method starts here
            if (string.IsNullOrEmpty(inf))
            {
                ShowError($"Empty filename: {inf}");
                return false;
            }

            string outTmp;
            try
            {
                outTmp = Path.GetFileNameWithoutExtension(inf);
            }
            catch (Exception)
            {
                ShowError($"Illegal filename: {inf}");
                return false;
            }
            if (string.IsNullOrEmpty(outTmp))
            {
                ShowError($"Empty filename: {inf}");
                return false;
            }

            string outPath;
            try
            {
                if (optOutUseRep) { outPath = Path.GetDirectoryName(inf)!; }
                else { outPath = Path.GetDirectoryName(optOutputPath + @"\*")!; }
            }
            catch
            {
                ShowError($"Invalid pathname: {inf}");
                return false;
            }
            if (string.IsNullOrEmpty(outPath))
            {
                outPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (!optAutoName)
            {
                outf = Combine(outPath, outTmp, ".csv");
                return true;
            }

            int n = 0;
            string np = string.Empty;
            outf = Combine(outPath, outTmp, $"{np}.csv");
            try
            {
                while (File.Exists(outf))
                {
                    np = $" ({++n})";
                    outf = Combine(outPath, outTmp, $"{np}.csv");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Big Bada-Boom!{nl}{ex}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Background Worker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            static bool IgnoreFound(string line)
            {
                if (!optIgnoreEnabled) return false;
                foreach (string ign in IgnoreListItems)
                {
                    if (line.Contains(ign))
                    {
                        return true;
                    }
                }
                return false;
            }

            BackgroundWorker? worker = sender as BackgroundWorker;
            // Now process inFile
            try
            {
                try
                {
                    // Delete output file to prevent spurious
                    // behavior when trying to overwrite existing file
                    if (File.Exists(outFile)) { File.Delete(outFile); }
                }
                catch { }
                // Open the stream for reading. File.OpenRead opens the file with read-only access
                // and a default FileShare.Read mode, allowing other processes to read simultaneously.
                using StreamReader reader = new(File.OpenRead(inFile));
                using StreamWriter writer = new(File.OpenWrite(outFile));

                badFile = false;
                progress = 0;
                int chunk = 0;
                records = 0;
                recsout = 0;
                linesout = 0;
                matches = 0;
                errors = 0;
                males = 0;
                females = 0;
                unknown = 0;
                skipped = 0;
                malesout = 0;
                femalesout = 0;
                unknownout = 0;
                suggestions = 0; // Suggestions found
                sugsout = 0; // Suggestions output
                ignored = 0; // Ignored lines
                int el = 0; // Index into ar[]
                int soe = 0; // Suggestion or Error
                rec.Clear();
                stopwatch.Start();

                while (progress <= count)
                {
                    if (worker!.CancellationPending)
                    {
                        reader.Close();
                        writer.Close();
                        e.Cancel = true; break;
                    }

                    // Read a line from the input file
                    ar.Insert(el, reader.ReadLine()!);

                    // Check for first line
                    if (progress == 0)
                    {
                        // Does line start and end with double quotes?
                        if (!Regex.IsMatch(ar[el], rddq))
                        {
                            // No, quit with error
                            reader.Close();
                            writer.Close();
                            badFile = true; e.Cancel = true; break;
                        }
                        el++;
                    }
                    else
                    {
                        // Is line starting a new record?
                        if ((ar[el] is null) || (ar[el].StartsWith(dq)))
                        {
                            // Yes, process previous record
                            if (soe > 0)
                            {
                                // Suggestion or Error found in record
                                // Save first line (name line)
                                rec.Add(ar[0]);
                                linesout++;
                                int i;
                                int c = 1;
                                int k = 0;
                                bool mf = false; //Match found
                                int spr = 0; // Suggestions per record
                                int loop = 0;
                                while (c < el)
                                {
                                    loop++;
                                    if (loop > 9999) break; // Sanity check, prevent runaway looping

                                    if (ar[c].Contains(sug))
                                    {
                                        // Line is a Suggestion
                                        if (k > 0) { rec.Add(ar[c - 1]); k = 0; linesout++; }
                                        switch (optSugsLevel)
                                        {
                                            case Settings.SugsLevel.All: { rec.Add(ar[c]); sugsout++; linesout++; break; }
                                            case Settings.SugsLevel.Num:
                                                {
                                                    if (spr < optSugsNUD) { rec.Add(ar[c]); sugsout++; linesout++; spr++; }
                                                    break;
                                                }
                                                // case Settings.SugsLevel.None: do nothing
                                        }
                                        c++;
                                    }
                                    else
                                    {
                                        // Not a Suggestion line
                                        spr = 0;
                                        if (!Regex.IsMatch(ar[c], com2))
                                        {
                                            // Does not start with ',,'
                                            if (ar[c] == "") // Marked to Ignore if true
                                            {
                                                // Ignore up until next line not starting with ',,'
                                                c++; ignored++; soe--; // Skip this line
                                                // Skip ahead until line not starting with ',,'
                                                while (Regex.IsMatch(ar[c], com2)) { c++; ignored++; soe--; }
                                            }
                                            else
                                            {
                                                k++; c++;
                                            }
                                        }
                                        else
                                        {
                                            // Line starts with ',,'
                                            mf = ar[c].Contains(match);
                                            if (mf) { matches++; }
                                            for (i = c - k; i <= c; i++)
                                            {
                                                if (mf)
                                                {
                                                    if (!optOmitMatch) { rec.Add(ar[i]); linesout++; }
                                                }
                                                else { rec.Add(ar[i]); linesout++; }
                                            }
                                            c += k > 0 ? k : 1;
                                            k = 0;
                                            mf = false;
                                        }
                                    }
                                }

                                TallySexes(rec[0], rec.Count > 1);
                                // Write out accumulated record lines
                                if (rec.Count > 1)
                                {
                                    foreach (string s in rec) { writer.WriteLine(s); }
                                    recsout++;
                                }
                                else
                                {
                                    skipped++;
                                }
                                rec.Clear();
                                soe = 0;
                            }
                            records++;
                            ar[0] = ar[el];
                            el = 1;
                        }
                        else
                        {
                            // Continue accumulating lines for current record
                            if (IgnoreFound(ar[el]))
                            {
                                ar[el] = string.Empty;
                            }
                            else
                            {
                                if (ar[el].Contains(sug)) { suggestions++; soe++; }
                                if (ar[el].Contains(error)) { errors++; soe++; }
                            }
                            el++;
                        }
                    }

                    progress++; chunk++;

                    if (chunk > 99)
                    {
                        worker.ReportProgress(Convert.ToInt32(progress * 100L / count));
                        chunk = 0;
                        // For testing
                        //System.Threading.Thread.Sleep(1);
                    }
                } // end while progress < count

                reader.Close();
                writer.Close();
            } // end try

            catch (FileNotFoundException ex)
            {
                ShowError($"The file {inFile} was not found{nl}{ex.Message}");
            }
            catch (IOException ex)
            {
                ShowError($"An I/O error occurred{nl}{ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                ShowError($"Insufficient permissions to access {inFile}{nl}{ex.Message}");
            }
            catch (IndexOutOfRangeException ex)
            {
                ShowError($"Too many events per person, please contact the developer.{nl}{ex.Message}");
            }
        }


        private void TallySexes(string r, bool writing)
        {
            if (Regex.IsMatch(r, rmale)) { males++; if (writing) malesout++; }
            else if (Regex.IsMatch(r, rfemale)) { females++; if (writing) femalesout++; }
            else { unknown++; if (writing) unknownout++; }

        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            lbProgress.Text = $"Progress: {e.ProgressPercentage}%";
            lbSuggsestions.Text = $"{sugsout}/{suggestions}";
            toolTip1.SetToolTip(lbSuggsestions, $"{sugsout} of {suggestions}");
            lbErrors.Text = errors.ToString();
            lbRecords.Text = records.ToString();
            int ro = Decimal.ToInt32(recsout);
            lbRecsOut.Text = $"{ro}/{ro + skipped}";
            lbLinesOut.Text = linesout.ToString();
            lbMatches.Text = matches.ToString();
            lbIgnored.Text = ignored.ToString();
        }

        private void ClearLabels() {
            lbCount.Text = string.Empty;
            lbSuggsestions.Text = string.Empty;
            lbErrors.Text = string.Empty;
            lbRecords.Text = string.Empty;
            lbRecsOut.Text = string.Empty;
            lbLinesOut.Text = string.Empty;
            lbMatches.Text = string.Empty;
            lbIgnored.Text = string.Empty;
            lbTime.Text = string.Empty;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            stopwatch.Stop(); DisplayTime();
            string ofd = optDeleteOnCancel ? ": Output file deleted" : "";
            lbProgress.Text = e.Cancelled ? $"Cancelled{ofd}" : e.Error != null ? "Error: " + e.Error.Message : "Done";
            Reset();

            if (badFile)
            {
                ClearLabels();
                ShowError($"Invalid input file. First line should start{nl}and end with double quotes.");
                lbOutFile.Text = string.Empty;
                try { File.Delete(outFile); }
                catch { }
                return;
            }

            if (optDeleteOnCancel)
            {
                if (e.Cancelled && File.Exists(outFile))
                {
                    ClearLabels();
                    lbOutFile.Text = string.Empty;
                    try { File.Delete(outFile); }
                    catch (Exception) { }
                    return;
                }
            }

            lbSuggsestions.Text = $"{sugsout}/{suggestions}";
            toolTip1.SetToolTip(lbSuggsestions, $"{sugsout} of {suggestions}");
            lbErrors.Text = errors.ToString();
            lbRecords.Text = records.ToString();
            int ro = Decimal.ToInt32(recsout);
            lbRecsOut.Text = $"{ro}/{ro + skipped}";
            lbLinesOut.Text = linesout.ToString();
            lbMatches.Text = matches.ToString();
            lbIgnored.Text = ignored.ToString();

            if (File.Exists(outFile))
            {
                try
                {
                    int n = 0;
                    int v = GetVisibileLineCount(tbRec) - 1;
                    string line;
                    using StreamReader rdr = new(File.OpenRead(outFile));
                    while ((line = rdr.ReadLine()!) != null)
                    {
                        n++;
                        if (n > v) { break; }
                        tbRec.AppendText($"{line}{ nl}");
                    }
                    rdr.Close();
                }
                catch (IOException) { tbRec.Text = $"Error reading {outFile}"; }
                catch (OutOfMemoryException) { tbRec.Text = $"Error reading {outFile}"; }
            }
            else
            {
                tbRec.Text = $"Cannot find {outFile}";
                return;
            }

            // Show count of males & females
            tbRec.AppendText($"Males: {malesout}/{males}, Females: {femalesout}/{females}, Unknown: {unknownout}/{unknown}");

            // MessageBox.Show($"Skipped: {skipped}");

            // Code to view output file if optViewOutput is true
            if (optViewOutput && File.Exists(outFile))
            {
                try
                {
                    using Process pr = Process.Start(new ProcessStartInfo(outFile) { UseShellExecute = true })!;
                }
                catch (Exception ex)
                {
                    ShowError($@"Cannot start {outFile}{nl}{ex.Message}");
                }
            }

            if (closePending)
            {
                // If the application is closed before the
                // background task completes, delete the output file
                try { if (optDeleteOnCancel && File.Exists(outFile)) File.Delete(outFile); }
                catch (Exception) { }
                Close();
            }
            closePending = false;
        }

        private void DisplayTime()
        {
            // Get the elapsed time as a TimeSpan value 
            TimeSpan ts = stopwatch.Elapsed;

            // Format and display the time 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            lbTime.Text = elapsedTime;
            stopwatch.Reset();
        }

        static long CountLinesReader(string file, ref string er)
        {
            long lineCounter = 0L;
            try
            {
                using StreamReader reader = new(File.OpenRead(file));
                while (reader.ReadLine() != null) { lineCounter++; }
                reader.Close();
                if (lineCounter == 0L) { er = "Zero length input file"; }
            }
            catch (OutOfMemoryException ex) { er = ex.Message; }
            catch (FileNotFoundException ex) { er = ex.Message; }
            catch (IOException ex) { er = ex.Message; }
            return lineCounter;
        }

        private static int GetVisibileLineCount(TextBox textBox)
        {
            if (!textBox.Multiline) { return 1; }
            int clientHeight = textBox.ClientRectangle.Height;
            int fontHeight = textBox.Font.Height;
            return (fontHeight > 0) ? clientHeight / fontHeight : 0;
        }

        private bool closePending;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                closePending = true;
                backgroundWorker1.CancelAsync();
                e.Cancel = true;
                this.Enabled = false;   // or this.Hide()
                return;
            }
            base.OnFormClosing(e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); // Safe since OnFormClosing is overridden to stop backgroundworker
        }

        /// <summary>
        /// About Dialog code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutCountyCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using About aboutWindow = new() { KeyPreview = true };
            aboutWindow.ShowDialog(this);
        }

        /// <summary>
        /// Help window code
        /// </summary>
        public Form Frm = null!;
        bool isFrmOpen = false;
        private void countyCheckerHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFrmOpen)
            {
                if (Frm.WindowState == FormWindowState.Minimized)
                    Frm.WindowState = FormWindowState.Normal;
                else
                    Frm.Focus();
            }
            else
            {
                Frm = new Help();
                isFrmOpen = true;
                Frm.FormClosed += new FormClosedEventHandler(Frm_FormClosed!);
                Frm.Show();
            }

            void Frm_FormClosed(object sender, FormClosedEventArgs e)
            {
                isFrmOpen = false;
            }
        }

        /// <summary>
        /// Settings window code
        /// </summary>
        /// <param name="Show"></param>
        private void ShowSettingsWindow(bool Show = true)
        {
            using SettingsWindow settingsWindow = new(Show) { KeyPreview = true };
            if (settingsWindow.ShowDialog(this) == DialogResult.OK)
            {
                LoadSettings();
                lbOutFile.Text = "";
                ColorLabels();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsWindow();
        }

        private void editIgnoreListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using IgnoreListWindow ignoreList = new() { KeyPreview = true };
            if (ignoreList.ShowDialog(this) == DialogResult.OK)
            {
                LoadIgnoreList();
            }
        }

        private void disableIgnoreListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optIgnoreEnabled = !optIgnoreEnabled;
            disableIgnoreListToolStripMenuItem.Text = optIgnoreEnabled ? "&Disable Ignore List" : "&Enable Ignore List";
            IgnoreList.CurrentIgnores.IgnoreEnabled = optIgnoreEnabled;
            IgnoreList.Save(false);
            ColorLabels();
        }
    }
}
