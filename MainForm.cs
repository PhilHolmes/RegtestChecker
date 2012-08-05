using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace RegtestChecker
{
	// Todos: Add a check for the number of pixels changed and display if relevant - circle changes if small - done
	// Multi-page output? - think this is done too - now with no file errors caught
	// CPU time -  done.
	// Crop final image? - done
	// Add a coloured text key to the version number - done
	public struct ThreadData
	{
		public string FileName;
		public int ThreadNumber;
	}

	public partial class MainForm : Form
	{
		int NumThreads;
		int Resolution;
		string[] Status;
		string[] CompareStatus;
		bool bComplete = true;
		AutoResetEvent[] EventFlag;
		bool TextBoxChanged = false;
		string ProcessTimes;
		string OldVersion = "";
		string NewVersion = "";
		bool Abort = false;
		StreamWriter LogFileLogger;
		string Output = "";
		string IncludeFile = "";
		string NewRegValue = "";
		
		public MainForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			UpdateRegistry();
			UpdateTimer.Enabled = true;
			btnGo.Enabled = false;
			btnStop.Enabled = true;

			IncludeFile = txtInclude.Text;
			IncludeFile = IncludeFile.Replace(@"\", @"/");

			Thread MainThread = new Thread(ProcessAllFiles);
			MainThread.Start();
			bComplete = false;
			Abort = false;
			ProcessTimes = "";

			Status = new string[NumThreads];
			CompareStatus = new string[NumThreads];
			EventFlag = new AutoResetEvent[NumThreads];

		}

		private void ProcessAllFiles()
		{
			try
			{
				if (chkUseSame.Checked)
				{
					NewRegValue = txtOldReg.Text + @"\Other";
				}
				else
				{
					NewRegValue = txtNewReg.Text;
				}
				bool[] ThreadActive = new bool[NumThreads];
				Thread[] WorkThread = new Thread[NumThreads];
				string FileSpec = "*.ly";
				if (txtFileSpec.Text.Length>0)
				{
					FileSpec = txtFileSpec.Text;
				}
				string OldRegDir = txtOldReg.Text;
				SearchOption Option = SearchOption.TopDirectoryOnly;
				if (chkSubdirs.Checked)
				{
					Option = SearchOption.AllDirectories;
				}

				string[] FilesList = Directory.GetFiles(OldRegDir, FileSpec, Option);

				Directory.CreateDirectory(txtOldReg.Text + @"\Output");
				Directory.CreateDirectory(txtOldReg.Text + @"\Output\LogFiles");
				Directory.CreateDirectory(NewRegValue + @"\Output");
				Directory.CreateDirectory(NewRegValue + @"\Output\Differences");
				Directory.CreateDirectory(NewRegValue + @"\Output\LogFiles");

				LogFileLogger = new StreamWriter(NewRegValue + @"\Output\LogFiles\LoggedDiffs.log", false);

				for (int j = 0; j < NumThreads; j++)
				{
					EventFlag[j] = new AutoResetEvent(false);
					ThreadActive[j] = false;
				}
				int i = 0;
				while (i < FilesList.Length)
				{
					FileInfo FileDetails;
					string FileName;

					for (int j = 0; j < NumThreads; j++)
					{
						if (!ThreadActive[j])
						{
							FileDetails = new FileInfo(FilesList[i]);
							if (!chkSubdirs.Checked)
							{
								FileName = FileDetails.Name;  // Leave as it was before adding the subdir bit
							}
							else
							{
								FileName = FileDetails.FullName.Replace(OldRegDir,"");
							}
							Console.WriteLine("File {0} ({1}) being processed", i, FileName);
							ThreadData ThisData = new ThreadData();
							ThisData.FileName = FileName;
							ThisData.ThreadNumber = j;
							ThreadActive[j] = true;
							WorkThread[j] = new Thread(ProcessFile);
							WorkThread[j].Start(ThisData);
							i++;
						}
						if (i >= FilesList.Length) break;
					}

					int index = WaitHandle.WaitAny(EventFlag);
					Console.WriteLine("Thread {0} says it's done", index);
					ThreadActive[index] = false;
					if (Abort)
					{
						break;
					}
				}
				for (int j = 0; j < NumThreads; j++)
				{
					if (j >= FilesList.Length) break;
					if (WorkThread[j].IsAlive) Console.WriteLine("Thread {0} still alive", j);
				}
				for (int j = 0; j < NumThreads; j++)
				{
					if (j >= FilesList.Length) break;
					while (WorkThread[j].IsAlive)
					{
						Thread.Sleep(1000);
						Console.WriteLine("Waiting for thread {0}", j);
					}
				}
				LogFileLogger.Flush();
				LogFileLogger.Close();
				string LogOut = NewRegValue + @"\Output\STDoutput.txt";
				StreamWriter STDOut = new StreamWriter(LogOut);
				STDOut.Write(Output);
				STDOut.Flush();
				STDOut.Close();
				bComplete = true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void ProcessFile(object Params)
		{
			ThreadData ThisData = (ThreadData)Params;
			string FileName = ThisData.FileName;
			int i = ThisData.ThreadNumber;
			Console.WriteLine("Thread " + i + " started");

			Status[i] = FileName;
			if (!chkUseOld.Checked)
			{
				RunLily(txtOldExe.Text + @"\usr\bin", txtOldReg.Text + @"\" + FileName, i, txtOldReg.Text);
			}

			if (!chkUseExistNew.Checked)
			{
				RunLily(txtNewExe.Text + @"\usr\bin", NewRegValue.Replace(@"Other", "") + @"\" + FileName, i, NewRegValue);
			}

			string PNGName = FileName.Replace(".ly", ".png");
			bool Result = ComparePNGs(txtOldReg.Text + @"\Output\" + PNGName, NewRegValue + @"\Output\" + PNGName);
			if (Result)
			{
				CompareStatus[i] = "Different";
			}
			else
			{
				CompareStatus[i] = "Same";
			}
			Result = CompareLogs(txtOldReg.Text + @"\Output\" + FileName, NewRegValue + @"\Output\" + FileName);
			Console.WriteLine("Compare logs  " + txtOldReg.Text + @"\Output\" + FileName, NewRegValue + @"\Output\" + FileName + " result " + Result.ToString());
			Console.WriteLine("Thread " + i + " completed work");
			//Thread.Sleep(10000);
			//Console.WriteLine("Thread " + i + " completed sleep");
			EventFlag[i].Set();
		}

		private bool CompareLogs(string FirstFile, string SecondFile)
		{
			StreamReader OldFile;
			StreamReader NewFile;

			if (File.Exists(GetLogFileName(FirstFile)))
			{
				OldFile = new StreamReader(GetLogFileName(FirstFile));
			}
			else
			{
				LogFileLogger.WriteLine(GetLogFileName(FirstFile));
				return false;
			}

			if (File.Exists(GetLogFileName(SecondFile)))
			{
				NewFile = new StreamReader(GetLogFileName(SecondFile));
			}
			else
			{
				LogFileLogger.WriteLine(GetLogFileName(SecondFile));
				return false;
			}

			string OldContents = OldFile.ReadToEnd();
			string NewContents = NewFile.ReadToEnd();

			OldContents = OldContents.Replace(@"/", @"\");
			OldContents = OldContents.Replace(txtOldReg.Text, "");
			OldContents = OldContents.Replace(OldVersion, "");

			OldFile.Close();
			NewFile.Close();

			NewContents = NewContents.Replace(@"/", @"\");
			NewContents = NewContents.Replace(NewRegValue, "");
			NewContents = NewContents.Replace(NewVersion, "");

			if (OldContents == NewContents)
			{
				return true;
			}
			else
			{
				string[] OldLines = OldContents.Split(new string[] { "\r\n" }, StringSplitOptions.None);
				string[] NewLines = NewContents.Split(new string[] { "\r\n" }, StringSplitOptions.None);
				bool Diffs = false;
				string DiffLog = "";
				for (int i = 0; i < OldLines.Length; i++)
				{
					if (i >= NewLines.Length)
					{
						DiffLog += "- " + OldLines[i] + "\r\n";
						Diffs = true;
					}
					else
					{
						if (NewLines[i] != OldLines[i])
						{
							if (OldLines[i].IndexOf("Processing") != 0)
							{
								DiffLog += "- " + OldLines[i] + "\r\n";
								Diffs = true;
							}
							if (NewLines[i].IndexOf("Processing") != 0)
							{
								DiffLog += "+ " + NewLines[i] + "\r\n";
								Diffs = true;
							}
						}
					}
				}
				if (Diffs)
				{
					System.Object x = new Object(); ;
					lock (x)
					{
						LogFileLogger.WriteLine("Differences in " + GetLogFileName(FirstFile));
						LogFileLogger.WriteLine(DiffLog);
					}
				}
				return false;
			}

		}

		/*		private void WriteLog(string LocalFileName, int ThisThread)
				{
					lock (LockObj)
					{

						StreamWriter WriteLog = new StreamWriter(GetLogFileName(LocalFileName));
						WriteLog.Write(AllInfo[ThisThread].AllProgOutput);
						WriteLog.Close();
					}
				}*/

		private string GetLogFileName(string LocalFileName)
		{
			FileInfo FileNameInfo = new FileInfo(LocalFileName);
			string Dir = FileNameInfo.DirectoryName;
			string Ext = FileNameInfo.Extension;
			string LogFile = FileNameInfo.Name;
			LogFile = LogFile.Replace(Ext, ".log");
			return Dir + @"\Logfiles\" + LogFile;
		}


		private bool ComparePNGs(string FirstBitmap, string SecondBitmap)
		{
			int cropBottom = 0;
			Color colFirst;
			Color colSecond = new Color();
			int PixelsDifferent = 0;
			Point[] DiffPoints = new Point[10];
			int[] DiffValue = new int[10];
			Bitmap bmpFirst = null;
			Bitmap bmpSecond = null;
			Bitmap bmpResult = null;
			int xMax, yMax;
			bool NoFirstFile = false, NoSecondFile = false;
			string OtherError = "";
			int Pages;

			string[] FilesList = null;
			FileInfo FirstBitmapInfo = new FileInfo(FirstBitmap);
			string DirRoot = FirstBitmapInfo.DirectoryName.Replace(txtOldReg.Text, "");
			string FileNameStart = FirstBitmapInfo.Name;

			Console.WriteLine("Comparing " + FileNameStart);

			FileNameStart = FileNameStart.Replace(".png", "-page*.png");
			FilesList = Directory.GetFiles(txtOldReg.Text + DirRoot, FileNameStart);
			Pages = FilesList.Length;
			if (Pages == 0) Pages = 1;  // To check for missing pages

			for (int ThisPage = 0; ThisPage < Pages; ThisPage++)
			{
				PixelsDifferent = 0;
				if (FilesList.Length > 0)
				{
					FirstBitmap = FilesList[ThisPage];
					SecondBitmap = FilesList[ThisPage].Replace(txtOldReg.Text, NewRegValue);
				}
				try
				{
					bmpFirst = new Bitmap(FirstBitmap);
					bmpSecond = new Bitmap(SecondBitmap);
				}
				catch
				{
					try
					{
						bmpFirst = new Bitmap(FirstBitmap);
					}
					catch (Exception e)
					{
						if (e.Message.IndexOf("Parameter is not valid") == 0)
						{
							NoFirstFile = true;
						}
						else
						{
							OtherError = e.Message;
						}
					}
					try
					{
						bmpSecond = new Bitmap(SecondBitmap);
					}
					catch (Exception e)
					{
						if (e.Message.IndexOf("Parameter is not valid") == 0)
						{
							NoSecondFile = true;
						}
						else
						{
							OtherError = e.Message;
						}
					}
				}

				if (!NoFirstFile && !NoSecondFile && OtherError == "")
				{
					bmpResult = new Bitmap(bmpFirst);
					xMax = 0;
					yMax = 0;
					for (int i = 0; i < bmpFirst.Width; i++)
					{
						for (int j = 0; j < bmpFirst.Height - cropBottom; j++)
						{
							colFirst = bmpFirst.GetPixel(i, j);
							if (colFirst.GetBrightness() < 1)
							{
								if (i > xMax) xMax = i;
								if (j > yMax) yMax = j;
							}
							if (i < bmpSecond.Width)
							{
								if (j < bmpSecond.Height - cropBottom)
								{
									colSecond = bmpSecond.GetPixel(i, j);
									if (colSecond.GetBrightness() < 1)
									{
										if (i > xMax) xMax = i;
										if (j > yMax) yMax = j;
									}
								}
							}
							if (colFirst == colSecond)
							{
								// bmpResult.SetPixel(i, j, colFirst);
							}
							else
							{
								float FirstBrightness = colFirst.GetBrightness() * 255;
								float SecondBrightness = colSecond.GetBrightness() * 255;
								if (Math.Abs(FirstBrightness - SecondBrightness) > 2.5)
								{
									PixelsDifferent++;
									if (PixelsDifferent <= 10)
									{
										DiffPoints[PixelsDifferent - 1] = new Point(i, j);
										DiffValue[PixelsDifferent - 1] = (int)Math.Abs(FirstBrightness - SecondBrightness);
									}
									if (FirstBrightness > SecondBrightness)
									{
										bmpResult.SetPixel(i, j, pnlNewColour.BackColor);
									}
									else
									{
										bmpResult.SetPixel(i, j, pnlOldColour.BackColor);
									}
								}
							}
						}
					}
					/*
					if (Pages > 1)
					{
						DiffPoints[0] = new Point(ThisPage, ThisPage);
						PixelsDifferent = 1;
					}*/
					if (PixelsDifferent > UpDownDiffs.Value)
					{
						Graphics grphBitmap = Graphics.FromImage(bmpResult);
						xMax = Math.Min(bmpResult.Width - 1, xMax + 100);
						yMax = Math.Min(bmpResult.Height - 1, yMax + 100);
						if (PixelsDifferent < 10)
						{
							string Diffs = PixelsDifferent.ToString() + " differences: ";
							for (int i = 0; i < PixelsDifferent; i++)
							{
								Diffs += "(" + DiffPoints[i].X + "," + DiffPoints[i].Y + "[" + DiffValue[i] + "]) ";
								grphBitmap.DrawEllipse(new Pen(Color.FromArgb(255, 128, 0)), DiffPoints[i].X - 10, DiffPoints[i].Y - 10, 20, 20);
							}
							grphBitmap.DrawString(Diffs, new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(Color.FromArgb(255, 128, 0)), new Rectangle(10, yMax - 80, xMax - 10, 100));
						}
						grphBitmap.DrawString(OldVersion, new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(pnlOldColour.BackColor), new Rectangle(5, 5, xMax / 2, 20));
						grphBitmap.DrawString(NewVersion, new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(pnlNewColour.BackColor), new Rectangle((xMax / 2) + 1, 5, xMax, 20));
						grphBitmap.Dispose();
						Bitmap bmpSave = bmpResult.Clone(new Rectangle(0, 0, xMax, yMax), bmpResult.PixelFormat);
						string SaveName = GetSaveFileName(SecondBitmap);
						Console.WriteLine("Saving {0}", SaveName);
						bmpSave.Save(SaveName, System.Drawing.Imaging.ImageFormat.Png);
						bmpSave.Dispose();
					}
				}
				else if (NoFirstFile)
				{
					/* bmpResult = new Bitmap(500, 500);
					Graphics grphBitmap = Graphics.FromImage(bmpResult);
					grphBitmap.FillRectangle(new SolidBrush(Color.White), 0, 0, 499, 499);
					grphBitmap.DrawString("Filename " + FirstBitmap + " does not exist", new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(Color.FromArgb(255, 128, 0)), new Rectangle(10, 10, 490, 490));
					if (NoSecondFile)
					{
						grphBitmap.DrawString("Filename " + SecondBitmap + " does not exist", new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(Color.FromArgb(255, 128, 0)), new Rectangle(10, 250, 490, 490));
					}
					grphBitmap.Dispose();
					string SaveName = GetSaveFileName(SecondBitmap);  // This is the directory where diff are stored
					bmpResult.Save(SaveName, System.Drawing.Imaging.ImageFormat.Png);*/
					LogFileLogger.WriteLine("Filename " + FirstBitmap + " does not exist");
					if (NoSecondFile)
					{
						LogFileLogger.WriteLine("Filename " + SecondBitmap + " does not exist");
					}
				}
				else if (NoSecondFile)
				{
					/* bmpResult = new Bitmap(500, 500);
					Graphics grphBitmap = Graphics.FromImage(bmpResult);
					grphBitmap.FillRectangle(new SolidBrush(Color.White), 0, 0, 499, 499);
					grphBitmap.DrawString("Filename " + SecondBitmap + " does not exist", new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(Color.FromArgb(255, 128, 0)), new Rectangle(10, 10, 490, 490));
					grphBitmap.Dispose();
					string SaveName = GetSaveFileName(SecondBitmap);  // This is the directory where diff are stored
					bmpResult.Save(SaveName, System.Drawing.Imaging.ImageFormat.Png); */
					LogFileLogger.WriteLine("Filename " + SecondBitmap + " does not exist");
				}
				else
				{
					bmpResult = new Bitmap(500, 500);
					Graphics grphBitmap = Graphics.FromImage(bmpResult);
					grphBitmap.FillRectangle(new SolidBrush(Color.White), 0, 0, 499, 499);
					grphBitmap.DrawString("Error " + OtherError + " occurred", new Font(FontFamily.GenericSansSerif, 12.0F), new SolidBrush(Color.FromArgb(255, 128, 0)), new Rectangle(10, 10, 490, 490));
					grphBitmap.Dispose();
					string SaveName = GetSaveFileName(SecondBitmap);  // This is the directory where diff are stored
					bmpResult.Save(SaveName, System.Drawing.Imaging.ImageFormat.Png);
				}
			}
			if (bmpResult != null)
			{
				bmpResult.Dispose();
			}
			if (bmpFirst != null)
			{
				bmpFirst.Dispose();
			}
			if (bmpSecond != null)
			{
				bmpSecond.Dispose();
			}

			Console.WriteLine("Ended {0} - {1} differences", FileNameStart, PixelsDifferent);

			return (PixelsDifferent > 0);
		}

		private string GetSaveFileName(string FileName)
		{
			FileInfo ThisFile = new FileInfo(FileName);
			string ThisDir = ThisFile.DirectoryName;
			ThisDir += @"\Differences\";
			string ThisFileName = ThisFile.Name;
			return ThisDir + ThisFileName;
		}

		private void btnBrowseOldExe_Click(object sender, EventArgs e)
		{
			GetDirectory(txtOldExe, "Select old LilyPond installation root folder");
			GetVersions();
			btnGo.Enabled = CheckReady();
		}
		private void btnBrowseOldReg_Click(object sender, EventArgs e)
		{
			GetDirectory(txtOldReg, "Select old regtests folder");
			btnGo.Enabled = CheckReady();
		}

		private void btnBrowseNewExe_Click(object sender, EventArgs e)
		{
			GetDirectory(txtNewExe, "Select new LilyPond installation root folder");
			GetVersions();
			btnGo.Enabled = CheckReady();
		}

		private void btnBrowseNewReg_Click(object sender, EventArgs e)
		{
			GetDirectory(txtNewReg, "Select new regtests folder");
			btnGo.Enabled = CheckReady();
		}

		private void btnInclude_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofnInclude = new OpenFileDialog();
			ofnInclude.Filter = "LilyPond files (*.ly)|*.ly|All files (*.*)|*.*";
			if (ofnInclude.ShowDialog() == DialogResult.OK)
			{
				txtInclude.Text = ofnInclude.FileName;
				Application.UserAppDataRegistry.SetValue("txtInclude", txtInclude.Text);
			}

		}

		private void GetDirectory(TextBox txtFile, string Instructions)
		{
			FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
			string CurrentDir = @"C:\Program Files (x86)";
			FolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			if (txtFile.Text != "")
			{
				CurrentDir = txtFile.Text;
			}
			FolderDialog.SelectedPath = CurrentDir;
			FolderDialog.ShowNewFolderButton = false;
			FolderDialog.Description = Instructions;

			if (FolderDialog.ShowDialog() == DialogResult.OK)
			{
				txtFile.Text = FolderDialog.SelectedPath;
				Application.UserAppDataRegistry.SetValue(txtFile.Name, txtFile.Text);
			}
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			SetTextContents(txtOldExe);
			SetTextContents(txtNewExe);
			SetTextContents(txtOldReg);
			SetTextContents(txtNewReg);
			SetTextContents(txtFileSpec);
			SetTextContents(txtInclude);
			chkUseSame.Checked = GetCheckboxState(chkUseSame);
			chkUseExistNew.Checked = GetCheckboxState(chkUseExistNew);
			chkUseOld.Checked = GetCheckboxState(chkUseOld);
			chkSubdirs.Checked = GetCheckboxState(chkSubdirs);
			try
			{
				string Threads = "";
				Threads = Application.UserAppDataRegistry.GetValue("NumThreads").ToString();
				NumThreads = int.Parse(Threads);
				UpDownThreads.Value = NumThreads;
			}
			catch { }

			GetVersions();

			try
			{
				string Res = "";
				Res = Application.UserAppDataRegistry.GetValue("Resolution").ToString();
				Resolution = int.Parse(Res);
				UpDownResolution.Value = Resolution;
			}
			catch { }

			try
			{
				string RegVal = "";
				RegVal = Application.UserAppDataRegistry.GetValue("OldColour").ToString();
				pnlOldColour.BackColor = Color.FromArgb(Convert.ToInt32(RegVal));
			}
			catch { }
			try
			{
				string RegVal = "";
				RegVal = Application.UserAppDataRegistry.GetValue("NewColour").ToString();
				pnlNewColour.BackColor = Color.FromArgb(Convert.ToInt32(RegVal));
			}
			catch { }

			btnGo.Enabled = CheckReady();
		}

		private bool GetCheckboxState(CheckBox chkThis)
		{
			bool RetVal = false;
			try
			{
				RetVal = bool.Parse(Application.UserAppDataRegistry.GetValue(chkThis.Name).ToString());
			}
			catch { };
			return RetVal;
		}
		private void GetVersions()
		{
			try
			{
				FileVersionInfo OldFI = FileVersionInfo.GetVersionInfo(txtOldExe.Text + @"\usr\bin\lilypond.exe");
				OldVersion = OldFI.FileVersion;
				OldVersion = OldVersion.Substring(0, OldVersion.Length - 2);
			}
			catch (Exception VerEx)
			{
				// MessageBox.Show("Error " + VerEx.Message + " occured trying to get version information for " + txtOldExe.Text + @"\usr\bin\lilypond.exe");
				OldVersion = "OldVer";
			}

			try
			{
				FileVersionInfo NewFI = FileVersionInfo.GetVersionInfo(txtNewExe.Text + @"\usr\bin\lilypond.exe");
				NewVersion = NewFI.FileVersion;
				NewVersion = NewVersion.Substring(0, NewVersion.Length - 2);
			}
			catch (Exception VerEx)
			{
				// MessageBox.Show("Error " + VerEx.Message + " occured trying to get version information for " + txtNewExe.Text + @"\usr\bin\lilypond.exe");
				NewVersion = "NewVer";
			}
		}
		private void SetTextContents(TextBox txtFile)
		{
			string RegValue = "";
			RegistryKey AppKey = Application.UserAppDataRegistry;

			try
			{
				RegValue = AppKey.GetValue(txtFile.Name).ToString();
				txtFile.Text = RegValue.ToString();
			}
			catch {}
		}

		private void RunLily(string ExeLocation, string RegTestLilyFile, int ThisThread, string OutputLocation)
		{
			string ExtraDirectory = "\\";
			string MayBeOther = "";
			string PrunedOPLoc=OutputLocation;
			try
			{
				Process LilyPondProc = new Process();
				string Args = "";
				FileInfo FileNameInfo = new FileInfo(RegTestLilyFile);
				string ThisDirectory = FileNameInfo.DirectoryName;
				
				if (OutputLocation.EndsWith(@"\Other"))
				{
					PrunedOPLoc = OutputLocation.Substring(0, OutputLocation.Length - 6);
					MayBeOther = @"\Other";
				}

				if (ThisDirectory != PrunedOPLoc)
				{
					ExtraDirectory = ThisDirectory.Replace(PrunedOPLoc, "");
					if (!Directory.Exists(OutputLocation + @"\Output" + ExtraDirectory + "\\"))
					{
						Directory.CreateDirectory(OutputLocation + @"\Output" + ExtraDirectory + "\\");
						if (OutputLocation == NewRegValue)
						{
							Directory.CreateDirectory(OutputLocation + @"\Output" + ExtraDirectory + "\\Differences");
						}
					}
				}
				string LilyFileBase = FileNameInfo.Name.Replace(".ly", "");
				// DataReceivedEventHandler EventHandle = new DataReceivedEventHandler(ProcErrorDataHandler);
				FileVersionInfo Version = FileVersionInfo.GetVersionInfo(ExeLocation + @"\lilypond.exe");
				LilyPondProc.StartInfo.FileName = ExeLocation + @"\lilypond";
				if (IncludeFile.Length > 0)
				{
					Args += "-dinclude-settings=\"\"\"\"" + IncludeFile + "\"\"\"\" ";
				}
				Args += "-dlog-file=\"\"\"\"" + OutputLocation.Replace(@"\", @"/") + @"/Output/Logfiles/" + LilyFileBase + "\"\"\"\" ";
				// Args += "-dgui ";
				Args += "-o \"" + OutputLocation + @"\Output" + ExtraDirectory + "\\" + LilyFileBase + "\" ";
				Args += "-fpng ";
				Args += "-dresolution=" + UpDownResolution.Value.ToString() + " ";
				LilyPondProc.StartInfo.Arguments = Args + "\"" + RegTestLilyFile + "\"";
				Console.WriteLine("Arguments are " + LilyPondProc.StartInfo.Arguments);
				LilyPondProc.StartInfo.UseShellExecute = false;
				LilyPondProc.StartInfo.CreateNoWindow = true;
				LilyPondProc.StartInfo.WorkingDirectory = ThisDirectory;
				LilyPondProc.StartInfo.RedirectStandardError = true;
				LilyPondProc.StartInfo.RedirectStandardOutput = true;
				// LilyPondProc.ErrorDataReceived += EventHandle;
				LilyPondProc.OutputDataReceived += new DataReceivedEventHandler(ProcOutputDataHandler);
				// LilyPondProc.Exited += new EventHandler(ProgFinished);
				LilyPondProc.EnableRaisingEvents = true;
				LilyPondProc.Start();
				Console.WriteLine("Process {0} started on file {1}, version {2}", LilyPondProc.Id, FileNameInfo.Name, Version.FileVersion);
				Output += FileNameInfo.Name + "\r\n";

				LilyPondProc.BeginOutputReadLine();
				//LilyPondProc.BeginErrorReadLine();
				LilyPondProc.WaitForExit(40000);
				if (!LilyPondProc.HasExited)
				{
					Console.WriteLine("Process {0} on file {1} killed, version {2}", LilyPondProc.Id, FileNameInfo.Name, Version.FileVersion);
					LilyPondProc.Kill();
				}

/*				string PNGFullFileName = RegTestLilyFile.Replace(".ly", ".png");
				string PNGFile = FileNameInfo.Name.Replace(".ly", ".png");
				if (!File.Exists(PNGFullFileName))
				{
					PNGFile = PNGFile.Replace(".png", "-page*.png");
					string[] FilesList = Directory.GetFiles(ThisDirectory, PNGFile);
					int Pages = FilesList.Length;

					for (int k = 0; k < Pages; k++)
					{
						FileInfo PNGFileInfo = new FileInfo(FilesList[k]);
						if (File.Exists(ThisDirectory + @"\Images\" + PNGFileInfo.Name)) File.Delete(ThisDirectory + @"\Images\" + PNGFileInfo.Name);
						File.Move(FilesList[k], ThisDirectory + @"\Images\" + PNGFileInfo.Name);
					}
				}
				else
				{
					if (File.Exists(ThisDirectory + @"\Images\" + FileNameInfo.Name.Replace(".ly", ".png"))) File.Delete(ThisDirectory + @"\Images\" + FileNameInfo.Name.Replace(".ly", ".png"));
					File.Move(PNGFullFileName, ThisDirectory + @"\Images\" + FileNameInfo.Name.Replace(".ly", ".png"));
				}
				MoveFile(FileNameInfo, ".log", "Logfiles");
				MoveFile(FileNameInfo, ".mid", "MidiOutput");
				MoveFile(FileNameInfo, ".xml", "XMLOutput");*/
				Console.WriteLine("LilyPond version {0}, file {1}: process time {2}, process {3}", Version.FileVersion, FileNameInfo.Name, (LilyPondProc.TotalProcessorTime.TotalMilliseconds / 1000).ToString("f2"), LilyPondProc.Id);
				AddTimingInfo(Version.FileVersion, FileNameInfo.Name, (LilyPondProc.TotalProcessorTime.TotalMilliseconds / 1000).ToString("f2"));
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Regtest error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void MoveFile(FileInfo LilyFile, string Extension, string DestDir)
		{
			string ThisDirectory = LilyFile.DirectoryName;
			if (File.Exists(ThisDirectory + @"\" + DestDir + @"\" + LilyFile.Name.Replace(".ly", Extension))) File.Delete(ThisDirectory + @"\" + DestDir + @"\" + LilyFile.Name.Replace(".ly", Extension));
			if (File.Exists(LilyFile.FullName.Replace(".ly", Extension))) File.Move(LilyFile.FullName.Replace(".ly", Extension), ThisDirectory + @"\" + DestDir + @"\" + LilyFile.Name.Replace(".ly", Extension));
		}

		private void AddTimingInfo(string Version, string Filename, string Time)
		{
			ProcessTimes += Version + "," + Filename + "," + Time + "\r\n";
		}

/*		private void ProgFinished(object sendingProcess, EventArgs errLine)
		{
			lock (LockObj)
			{
				Process ThisProc = (Process)sendingProcess;
				Console.WriteLine("{0} process finished.", ThisProc.Id);
				for (int i = 0; i < NumThreads; i++)
				{
					if (AllInfo[i].ProcessID == ThisProc.Id)
					{
						AllInfo[i].AllProgOutput += "Program finished\r\n";
						break;
					}
				}
			}
		}

		private void ProcErrorDataHandler(object sendingProcess, DataReceivedEventArgs errLine)
		{
			lock (LockObj)
			{
				Process ThisProc = (Process)sendingProcess;
				if (errLine.Data != null)
				{
					string Output = errLine.Data;
					for (int i = 0; i < NumThreads; i++)
					{
						if (AllInfo[i].ProcessID == ThisProc.Id)
						{
							AllInfo[i].AllProgOutput += Output + "\r\n";
							Console.WriteLine("Error data for {0} is {1}", AllInfo[i].File, Output);
							break;
						}
					}
				}
			}
		}  */
		private void ProcOutputDataHandler(object sendingProcess, DataReceivedEventArgs LineOutput)
		{
			if (LineOutput.Data != null)
			{
				Output += "STDOut: " + LineOutput.Data + "\r\n";
			}
		}

		private void UpdateTimer_Tick(object sender, EventArgs e)
		{
			txtStatus1.Text = Status[0];
			if (bComplete)
			{
				this.Enabled = true;
				btnGo.Enabled = true;
				UpdateTimer.Enabled = false;
				txtStatus1.Text = "Done";
				StreamWriter TimeLog = new StreamWriter(NewRegValue + "\\Output\\Timings.csv", false);
				TimeLog.Write("Version, File, Time\r\n");
				TimeLog.Write(ProcessTimes);
				TimeLog.Flush();
				TimeLog.Close();
			}
		}

		private void txtBox_TextChanged(object sender, EventArgs e)
		{
			TextBoxChanged = true;
			btnGo.Enabled = CheckReady();
			GetVersions();
		}

		private void MainForm_Closing(object sender, FormClosingEventArgs e)
		{
			UpdateRegistry();
			if (!bComplete)
			{
				e.Cancel = true;
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			Abort = true;
			btnStop.Enabled = false;
		}

		private void UpDownThreads_ValueChanged(object sender, EventArgs e)
		{
			NumThreads = (int)UpDownThreads.Value;
			Application.UserAppDataRegistry.SetValue("NumThreads", NumThreads.ToString());
		}

		private void chkUseSame_CheckedChanged(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(CheckBox))
			{
				CheckBox Check = (CheckBox)sender;
				bool Checked = Check.Checked;
				Application.UserAppDataRegistry.SetValue(Check.Name, Checked.ToString());
			}
			if (chkUseSame.Checked)
			{
				txtNewReg.Enabled = false;
				txtNewReg.ForeColor = Color.Gray;
				txtNewReg.BackColor = Color.Gray;
			}
			else
			{
				txtNewReg.ForeColor = SystemColors.WindowText;
				txtNewReg.BackColor = SystemColors.Window;
				txtNewReg.Enabled = true;
			}
			btnGo.Enabled = CheckReady();
		}
		private void CheckChanged(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(CheckBox))
			{
				CheckBox Check = (CheckBox)sender;
				bool Checked = Check.Checked;
				Application.UserAppDataRegistry.SetValue(Check.Name, Checked.ToString());
			}
		}
		private bool CheckReady()
		{
			if (txtNewExe.Text.Length < 1) return false;
			if (txtOldExe.Text.Length < 1) return false;
			if (txtNewReg.Text.Length < 1)
			{
				if (chkUseSame.Checked == false) return false;
			}
			if (txtOldReg.Text.Length < 1) return false;
			return true;
		}
		private void UpdateRegistry()
		{
			if (TextBoxChanged)
			{
				for (int i = 0; i < this.Controls.Count; i++)
				{
					if (this.Controls[i].GetType() == typeof(TextBox))
					{
						Application.UserAppDataRegistry.SetValue(this.Controls[i].Name, this.Controls[i].Text);
					}
				}
				TextBoxChanged = false;
			}

		}

		private void UpDownResolution_ValueChanged(object sender, EventArgs e)
		{
			Resolution = (int)UpDownResolution.Value;
			Application.UserAppDataRegistry.SetValue("Resolution", Resolution.ToString());
		}

		private void pnlOldColour_Click(object sender, EventArgs e)
		{
			colorDialog.Color = pnlOldColour.BackColor;
			DialogResult Result = colorDialog.ShowDialog();
			if (Result == DialogResult.OK)
			{
				string Colour = colorDialog.Color.ToArgb().ToString();
				Application.UserAppDataRegistry.SetValue("OldColour", Colour);
				pnlOldColour.BackColor = Color.FromArgb(Convert.ToInt32(Colour));
			}
		}

		private void pnlNewColour_Click(object sender, EventArgs e)
		{
			colorDialog.Color = pnlNewColour.BackColor;
			DialogResult Result = colorDialog.ShowDialog();
			if (Result == DialogResult.OK)
			{
				string Colour = colorDialog.Color.ToArgb().ToString();
				Application.UserAppDataRegistry.SetValue("NewColour", Colour);
				pnlNewColour.BackColor = Color.FromArgb(Convert.ToInt32(Colour));
			}
		}
	}
}

