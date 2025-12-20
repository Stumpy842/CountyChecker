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
        private long matches;
        private readonly List<string> ar = [];
        private readonly char dq = '"';
        private readonly string sug = "Suggestion:";
        private readonly string error = "Error:";
        private readonly string match = "Match:";
        private bool badFile;
        // Match a line starting and ending with double quotes, and optionally ending commas
        private readonly string pat = "^\".*\",*$";
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

                lbOutFile.Text = outFile;
                lbCount.Text = count.ToString();
                tbRec.Text = string.Empty;   
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
        /// <summary>
        ///  Gets the output filename in outf
        /// </summary>
        /// <param name="inf"></param>
        /// <param name="outf"></param>
        /// <returns></returns>
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
                lbSuggsestions.ForeColor = optSugsLevel == Settings.SugsLevel.None ? Color.Red :
                    optSugsLevel == Settings.SugsLevel.Num ? Color.Purple : SystemColors.ControlText;
                lbMatches.ForeColor = optOmitMatch ? Color.Red : SystemColors.ControlText;
                badFile = false;
                progress = 0;
                int chunk = 0;
                records = 0;
                recsout = 0;
                linesout = 0;
                matches = 0;
                errors = 0;
                suggestions = 0;
                sugsout = 0;
                int el = 0;
                bool soe = false; // Suggestion or Error
                stopwatch.Start();
                while (progress <= count)
                {
                    if (worker!.CancellationPending)
                    {
                        reader.Close();
                        writer.Close();
                        e.Cancel = true; break;
                    }

                    ar.Insert(el, reader.ReadLine()!);

                    // Check for first line
                    if (progress == 0)
                    {
                        if (!Regex.IsMatch(ar[el], pat))
                        {
                            reader.Close();
                            writer.Close();
                            badFile = true; e.Cancel = true; break;
                        }
                        el++;
                    }
                    else
                    {
                        if ((ar[el] is null) || (ar[el].StartsWith(dq)))
                        {
                            if (soe)
                            {
                                writer.WriteLine(ar[0]);
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
                                        if (k > 0) { writer.WriteLine(ar[c - 1]); k = 0; linesout++; }
                                        switch (optSugsLevel)
                                        {
                                            case Settings.SugsLevel.All: { writer.WriteLine(ar[c]); sugsout++; linesout++; break; }
                                            case Settings.SugsLevel.Num:
                                                {
                                                    if (spr < optSugsNUD) { writer.WriteLine(ar[c]); sugsout++; linesout++; spr++; }
                                                    break;
                                                }
                                        }
                                        c++;
                                    }
                                    else
                                    {
                                        spr = 0;
                                        if (!Regex.IsMatch(ar[c], com2))
                                        { k++; c++; }
                                        else
                                        {
                                            mf = ar[c].Contains(match);
                                            if (mf) { matches++; }
                                            for (i = c - k; i <= c; i++)
                                            {
                                                if (mf)
                                                {
                                                    if (!optOmitMatch) { writer.WriteLine(ar[i]); linesout++; }
                                                }
                                                else { writer.WriteLine(ar[i]); linesout++; }
                                            }
                                            c += k > 0 ? k : 1;
                                            k = 0;
                                            mf = false;
                                        }
                                    }
                                }

                                recsout++;
                                soe = false;
                            }
                            records++;
                            ar[0] = ar[el];
                            el = 1;
                        }
                        else
                        {
                            if (ar[el].Contains(sug)) { suggestions++; soe = true; }
                            if (ar[el].Contains(error)) { errors++; soe = true; }
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
                MessageBox.Show($"The file {inFile} was not found{nl}{ex.Message}");
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

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            lbProgress.Text = $"Progress: {e.ProgressPercentage}%";
            lbSuggsestions.Text = $"{sugsout}/{suggestions}";
            toolTip1.SetToolTip(lbSuggsestions, $"{sugsout} of {suggestions}");
            lbErrors.Text = errors.ToString();
            lbRecords.Text = records.ToString();
            lbRecsOut.Text = recsout.ToString();
            lbLinesOut.Text = linesout.ToString();
            lbMatches.Text = matches.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            stopwatch.Stop(); DisplayTime();
            string ofd = optDeleteOnCancel ? ": Output file deleted" : "";
            lbProgress.Text = e.Cancelled ? $"Cancelled{ofd}" : e.Error != null ? "Error: " + e.Error.Message : "Done";
            lbSuggsestions.Text = $"{sugsout}/{suggestions}";
            toolTip1.SetToolTip(lbSuggsestions, $"{sugsout} of {suggestions}");
            lbErrors.Text = errors.ToString();
            lbRecords.Text = records.ToString();
            lbRecsOut.Text = recsout.ToString();
            lbLinesOut.Text = linesout.ToString();
            lbMatches.Text = matches.ToString();

            if (badFile)
            {
                ShowError($"Invalid input file. First line should start{nl}and end with double quotes.");
                try
                {
                    lbOutFile.Text = string.Empty;
                    File.Delete(outFile);
                }
                catch { }
            }
            else
            {
                try
                {
                    if (File.Exists(outFile))
                    {
                        int n = 0;
                        int v = GetVisibileLineCount(tbRec);
                        string line;
                        using StreamReader rdr = new(File.OpenRead(outFile));
                        while ((line = rdr.ReadLine()!) != null)
                        {
                            n++;
                            if (n > v) { break; }
                            tbRec.AppendText(line + Environment.NewLine);
                        }
                        rdr.Close();
                    }
                    else { tbRec.Text = $"Cannot find {outFile}"; }
                }
                catch (IOException) { tbRec.Text = $"Error reading {outFile}"; }
                catch (OutOfMemoryException) { tbRec.Text = $"Error reading {outFile}"; }
            }

            Reset();

            // Code to view output file if optViewOutput is true
            if (optViewOutput && File.Exists(outFile))
            {
                try
                {
                    using Process pr = Process.Start(new ProcessStartInfo(outFile) { UseShellExecute = true })!;
                }
                catch (Exception ex)
                {
                    ShowError($@"Cannot start {outFile}{nl}{ex}");
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
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsWindow();
        }
    }
}
