using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using PaliReaderUtils;
using System.Text.RegularExpressions;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace CSCDBookConverter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainWnd : System.Windows.Forms.Form
	{
		private ArrayList m_arrChaptersList=null;
		private int m_nParentNodeID=0;
		private int m_nNodeCounter=1;
		private int m_nZipFileIndex=0;
		private int m_nC13NodeID=-1;
		private byte m_nLibFileIndex=1;
		private int m_nParagraphID=0;

		ZipOutputStream m_objLibraryStream;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ctlCSCDFolder;
		private System.Windows.Forms.Button ctlCSCDBrowse;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ctlDestFolder;
		private System.Windows.Forms.Button ctlDestFolderBrowse;
		private System.Windows.Forms.ErrorProvider ctlErrorProvider;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox ctlPlatform;
		private System.Windows.Forms.CheckBox ctlStripPageNumbers;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox ctlDiffsHandling;
		private System.Windows.Forms.FolderBrowserDialog ctlBrowseFolder;
		private System.Windows.Forms.Label ctlMessage;
		private System.Windows.Forms.ProgressBar ctlConversionProgress;
		private System.Windows.Forms.Button ctlStartConversion2;
		private System.Windows.Forms.CheckBox ctlVinaya;
		private System.Windows.Forms.CheckBox ctlSutta;
		private System.Windows.Forms.CheckBox ctlAbidhamma;
		private System.Windows.Forms.CheckBox ctlAtthakatha;
		private System.Windows.Forms.CheckBox ctlTika;
		private System.Windows.Forms.CheckBox ctlOther;
		private System.Windows.Forms.CheckBox ctlMula;
		private System.Windows.Forms.CheckBox ctlOtherContent;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.ctlCSCDFolder = new System.Windows.Forms.TextBox();
			this.ctlCSCDBrowse = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ctlDestFolder = new System.Windows.Forms.TextBox();
			this.ctlDestFolderBrowse = new System.Windows.Forms.Button();
			this.ctlErrorProvider = new System.Windows.Forms.ErrorProvider();
			this.label3 = new System.Windows.Forms.Label();
			this.ctlPlatform = new System.Windows.Forms.ComboBox();
			this.ctlStripPageNumbers = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.ctlDiffsHandling = new System.Windows.Forms.ComboBox();
			this.ctlBrowseFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.ctlMessage = new System.Windows.Forms.Label();
			this.ctlConversionProgress = new System.Windows.Forms.ProgressBar();
			this.ctlStartConversion2 = new System.Windows.Forms.Button();
			this.ctlVinaya = new System.Windows.Forms.CheckBox();
			this.ctlSutta = new System.Windows.Forms.CheckBox();
			this.ctlAbidhamma = new System.Windows.Forms.CheckBox();
			this.ctlOther = new System.Windows.Forms.CheckBox();
			this.ctlAtthakatha = new System.Windows.Forms.CheckBox();
			this.ctlTika = new System.Windows.Forms.CheckBox();
			this.ctlMula = new System.Windows.Forms.CheckBox();
			this.ctlOtherContent = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "CSCD folder:";
			// 
			// ctlCSCDFolder
			// 
			this.ctlCSCDFolder.Location = new System.Drawing.Point(136, 16);
			this.ctlCSCDFolder.Name = "ctlCSCDFolder";
			this.ctlCSCDFolder.Size = new System.Drawing.Size(272, 22);
			this.ctlCSCDFolder.TabIndex = 1;
			this.ctlCSCDFolder.Text = "";
			this.ctlCSCDFolder.Validating += new System.ComponentModel.CancelEventHandler(this.ctlCSCDFolder_Validating);
			// 
			// ctlCSCDBrowse
			// 
			this.ctlCSCDBrowse.CausesValidation = false;
			this.ctlCSCDBrowse.Location = new System.Drawing.Point(432, 8);
			this.ctlCSCDBrowse.Name = "ctlCSCDBrowse";
			this.ctlCSCDBrowse.Size = new System.Drawing.Size(96, 32);
			this.ctlCSCDBrowse.TabIndex = 2;
			this.ctlCSCDBrowse.Text = "Browse...";
			this.ctlCSCDBrowse.Click += new System.EventHandler(this.ctlCSCDBrowse_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 24);
			this.label2.TabIndex = 3;
			this.label2.Text = "Destination folder:";
			// 
			// ctlDestFolder
			// 
			this.ctlDestFolder.Location = new System.Drawing.Point(136, 56);
			this.ctlDestFolder.Name = "ctlDestFolder";
			this.ctlDestFolder.Size = new System.Drawing.Size(272, 22);
			this.ctlDestFolder.TabIndex = 4;
			this.ctlDestFolder.Text = "";
			this.ctlDestFolder.Validating += new System.ComponentModel.CancelEventHandler(this.ctlDestFolder_Validating);
			// 
			// ctlDestFolderBrowse
			// 
			this.ctlDestFolderBrowse.CausesValidation = false;
			this.ctlDestFolderBrowse.Location = new System.Drawing.Point(432, 56);
			this.ctlDestFolderBrowse.Name = "ctlDestFolderBrowse";
			this.ctlDestFolderBrowse.Size = new System.Drawing.Size(96, 32);
			this.ctlDestFolderBrowse.TabIndex = 5;
			this.ctlDestFolderBrowse.Text = "Browse...";
			this.ctlDestFolderBrowse.Click += new System.EventHandler(this.ctlDestFolderBrowse_Click);
			// 
			// ctlErrorProvider
			// 
			this.ctlErrorProvider.ContainerControl = this;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Platform:";
			this.label3.Visible = false;
			// 
			// ctlPlatform
			// 
			this.ctlPlatform.CausesValidation = false;
			this.ctlPlatform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ctlPlatform.Items.AddRange(new object[] {
															 "Pocket PC",
															 "Desktop PC"});
			this.ctlPlatform.Location = new System.Drawing.Point(136, 88);
			this.ctlPlatform.Name = "ctlPlatform";
			this.ctlPlatform.Size = new System.Drawing.Size(200, 24);
			this.ctlPlatform.TabIndex = 7;
			this.ctlPlatform.Visible = false;
			// 
			// ctlStripPageNumbers
			// 
			this.ctlStripPageNumbers.CausesValidation = false;
			this.ctlStripPageNumbers.Checked = true;
			this.ctlStripPageNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlStripPageNumbers.Location = new System.Drawing.Point(136, 128);
			this.ctlStripPageNumbers.Name = "ctlStripPageNumbers";
			this.ctlStripPageNumbers.Size = new System.Drawing.Size(24, 24);
			this.ctlStripPageNumbers.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 24);
			this.label4.TabIndex = 9;
			this.label4.Text = "Strip page numbers:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 160);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 32);
			this.label5.TabIndex = 10;
			this.label5.Text = "Differences between tipitaka editions: ";
			// 
			// ctlDiffsHandling
			// 
			this.ctlDiffsHandling.CausesValidation = false;
			this.ctlDiffsHandling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ctlDiffsHandling.Items.AddRange(new object[] {
																  "Leave intact",
																  "Strip book abbreviations only",
																  "Strip completely"});
			this.ctlDiffsHandling.Location = new System.Drawing.Point(136, 168);
			this.ctlDiffsHandling.Name = "ctlDiffsHandling";
			this.ctlDiffsHandling.Size = new System.Drawing.Size(272, 24);
			this.ctlDiffsHandling.TabIndex = 11;
			// 
			// ctlMessage
			// 
			this.ctlMessage.Location = new System.Drawing.Point(8, 376);
			this.ctlMessage.Name = "ctlMessage";
			this.ctlMessage.Size = new System.Drawing.Size(488, 32);
			this.ctlMessage.TabIndex = 13;
			// 
			// ctlConversionProgress
			// 
			this.ctlConversionProgress.Location = new System.Drawing.Point(8, 416);
			this.ctlConversionProgress.Name = "ctlConversionProgress";
			this.ctlConversionProgress.Size = new System.Drawing.Size(488, 24);
			this.ctlConversionProgress.TabIndex = 14;
			// 
			// ctlStartConversion2
			// 
			this.ctlStartConversion2.Location = new System.Drawing.Point(8, 312);
			this.ctlStartConversion2.Name = "ctlStartConversion2";
			this.ctlStartConversion2.Size = new System.Drawing.Size(520, 48);
			this.ctlStartConversion2.TabIndex = 15;
			this.ctlStartConversion2.Text = "Start conversion";
			this.ctlStartConversion2.Click += new System.EventHandler(this.ctlStartConversion2_Click);
			// 
			// ctlVinaya
			// 
			this.ctlVinaya.Checked = true;
			this.ctlVinaya.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlVinaya.Location = new System.Drawing.Point(8, 200);
			this.ctlVinaya.Name = "ctlVinaya";
			this.ctlVinaya.Size = new System.Drawing.Size(192, 24);
			this.ctlVinaya.TabIndex = 16;
			this.ctlVinaya.Text = "Include Vinaya pitaka";
			// 
			// ctlSutta
			// 
			this.ctlSutta.Checked = true;
			this.ctlSutta.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlSutta.Location = new System.Drawing.Point(8, 232);
			this.ctlSutta.Name = "ctlSutta";
			this.ctlSutta.Size = new System.Drawing.Size(184, 24);
			this.ctlSutta.TabIndex = 17;
			this.ctlSutta.Text = "Include Sutta Pitaka";
			// 
			// ctlAbidhamma
			// 
			this.ctlAbidhamma.Checked = true;
			this.ctlAbidhamma.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlAbidhamma.Location = new System.Drawing.Point(200, 200);
			this.ctlAbidhamma.Name = "ctlAbidhamma";
			this.ctlAbidhamma.Size = new System.Drawing.Size(208, 24);
			this.ctlAbidhamma.TabIndex = 18;
			this.ctlAbidhamma.Text = "Include abudhamma pitaka";
			// 
			// ctlOther
			// 
			this.ctlOther.Checked = true;
			this.ctlOther.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlOther.Location = new System.Drawing.Point(200, 232);
			this.ctlOther.Name = "ctlOther";
			this.ctlOther.Size = new System.Drawing.Size(216, 24);
			this.ctlOther.TabIndex = 19;
			this.ctlOther.Text = "Include other texts";
			// 
			// ctlAtthakatha
			// 
			this.ctlAtthakatha.Checked = true;
			this.ctlAtthakatha.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlAtthakatha.Location = new System.Drawing.Point(128, 272);
			this.ctlAtthakatha.Name = "ctlAtthakatha";
			this.ctlAtthakatha.Size = new System.Drawing.Size(136, 24);
			this.ctlAtthakatha.TabIndex = 20;
			this.ctlAtthakatha.Text = "Include atthakatha";
			// 
			// ctlTika
			// 
			this.ctlTika.Checked = true;
			this.ctlTika.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlTika.Location = new System.Drawing.Point(272, 272);
			this.ctlTika.Name = "ctlTika";
			this.ctlTika.Size = new System.Drawing.Size(96, 24);
			this.ctlTika.TabIndex = 21;
			this.ctlTika.Text = "Include tika";
			// 
			// ctlMula
			// 
			this.ctlMula.Checked = true;
			this.ctlMula.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlMula.Location = new System.Drawing.Point(8, 272);
			this.ctlMula.Name = "ctlMula";
			this.ctlMula.Size = new System.Drawing.Size(112, 24);
			this.ctlMula.TabIndex = 22;
			this.ctlMula.Text = "Include mula";
			// 
			// ctlOtherContent
			// 
			this.ctlOtherContent.Checked = true;
			this.ctlOtherContent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ctlOtherContent.Location = new System.Drawing.Point(368, 272);
			this.ctlOtherContent.Name = "ctlOtherContent";
			this.ctlOtherContent.TabIndex = 23;
			this.ctlOtherContent.Text = "Include other";
			// 
			// MainWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(544, 453);
			this.Controls.Add(this.ctlOtherContent);
			this.Controls.Add(this.ctlMula);
			this.Controls.Add(this.ctlTika);
			this.Controls.Add(this.ctlAtthakatha);
			this.Controls.Add(this.ctlOther);
			this.Controls.Add(this.ctlAbidhamma);
			this.Controls.Add(this.ctlSutta);
			this.Controls.Add(this.ctlVinaya);
			this.Controls.Add(this.ctlStartConversion2);
			this.Controls.Add(this.ctlConversionProgress);
			this.Controls.Add(this.ctlMessage);
			this.Controls.Add(this.ctlDiffsHandling);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ctlStripPageNumbers);
			this.Controls.Add(this.ctlPlatform);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ctlDestFolderBrowse);
			this.Controls.Add(this.ctlDestFolder);
			this.Controls.Add(this.ctlCSCDFolder);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ctlCSCDBrowse);
			this.Controls.Add(this.label1);
			this.Name = "MainWnd";
			this.Text = "CSCD Book converter";
			this.Load += new System.EventHandler(this.MainWnd_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainWnd());
		}

		private void MainWnd_Load(object sender, System.EventArgs e)
		{
			ctlDiffsHandling.SelectedIndex=1;
		}

		private void ctlCSCDBrowse_Click(object sender, System.EventArgs e)
		{
			if(ctlBrowseFolder.ShowDialog()==DialogResult.OK)
				ctlCSCDFolder.Text=ctlBrowseFolder.SelectedPath;
		}

		private void ctlDestFolderBrowse_Click(object sender, System.EventArgs e)
		{
			if(ctlBrowseFolder.ShowDialog()==DialogResult.OK)
				ctlDestFolder.Text=ctlBrowseFolder.SelectedPath;
		}

		private string DiffsEvaluator(Match objDiffMatch)
		{//strip all book references in parentheses except (?)
			return Regex.Replace(objDiffMatch.Value,@"\ *\([^\?]*?\)\ *","");
		}

		private string FormatName(string strName)
		{
			return Regex.Replace(strName,@"([^ \d])0([ \)\.\r])","$1.$2");
		}

		private void ctlCSCDFolder_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ctlErrorProvider.SetError(ctlCSCDFolder,
				IsCSCDFolderValid ? "" : "this should point to CSCD root directory");
		}

		private void ctlDestFolder_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ctlErrorProvider.SetError(ctlDestFolder,
				IsDestFolderValid ? "" : "must be a valid directory");			
		}

		private bool IsCSCDFolderValid
		{
			get
			{
				return (ctlCSCDFolder.Text!="" && Directory.Exists(ctlCSCDFolder.Text+@"\CSCD"));
			}
		}

		private bool IsDestFolderValid
		{
			get
			{
				return (ctlDestFolder.Text!="" && Directory.Exists(ctlDestFolder.Text));
			}
		}

		private void ctlStartConversion2_Click(object sender, System.EventArgs e)
		{
			Validate();
			
			if(IsCSCDFolderValid && IsDestFolderValid)
			{
				try
				{
					ctlStartConversion2.Enabled=false;
					if(!ctlCSCDFolder.Text.EndsWith(@"\"))
						ctlCSCDFolder.Text+=@"\";

					if(!ctlDestFolder.Text.EndsWith(@"\"))
						ctlDestFolder.Text+=@"\";
					
					FileStream fs=File.Open(ctlCSCDFolder.Text+@"\CSCD\open.ale",FileMode.Open,FileAccess.Read);
					byte[] arrContent=new byte[fs.Length];
					fs.Read(arrContent,0,(int)fs.Length);
					fs.Close();

					AalekhDecoder objDecoder=new AalekhDecoder();
					char[] arrDecoded=objDecoder.AalekhToUnicode(arrContent,false,false);
					string strFileContent=new string(arrDecoded);
					MatchCollection mc=Regex.Matches(strFileContent,@"^(\d{4})~([^#]*)#([^#]+)",RegexOptions.IgnoreCase | RegexOptions.Multiline);

					ArrayList objIndexList=new ArrayList(3000);

					if(ctlVinaya.Checked)
						CreatePitakaContent2(mc,"1","Vinaya pitaka",objIndexList);

					if(ctlSutta.Checked)
						CreatePitakaContent2(mc,"2","Sutta pitaka",objIndexList);

					if(ctlAbidhamma.Checked)
						CreatePitakaContent2(mc,"3","Abidhamma pitaka",objIndexList);

					if(ctlOther.Checked)
						CreatePitakaContent2(mc,"4","Others",objIndexList);

					if(m_objLibraryStream!=null)
					{
						m_objLibraryStream.Finish();
						m_objLibraryStream.Close();
					}

					objIndexList.Sort();

					if(objIndexList.Count>0)
					{
						fs = new FileStream(ctlDestFolder.Text+"lib_index.dat", FileMode.Create);
						BinaryWriter bw=new BinaryWriter(fs,Encoding.UTF8);					
										
						foreach(IndexNode objNode in objIndexList)
						{
							objNode.Write(bw);
						}

						bw.Close();
						fs.Close();
				
//						StreamWriter objSW=new StreamWriter(ctlDestFolder.Text+"lib_index.txt",false);
//						foreach(IndexNode objNode in objIndexList)
//							objNode.Write(objSW);
//						objSW.Close();
					}
					if(!File.Exists(ctlDestFolder.Text+"pali_dic.txt"))
						File.Copy(Application.ExecutablePath.Replace("CSCDBookConverter.exe","pali_dic.txt"),ctlDestFolder.Text+"pali_dic.txt");

					ctlMessage.Text="Done!";
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					ctlStartConversion2.Enabled=true;
				}
			}					
		}

		private void CreatePitakaContent2(MatchCollection mc,string strPitakaID,string strPitakaName,ArrayList objIndexList)
		{
			IndexNode objPitakaNode=new IndexNode(0,m_nNodeCounter++,strPitakaName,true,-1,0xFF);
			objIndexList.Add(objPitakaNode);

			ctlMessage.Text="Converting "+strPitakaName;
			ctlConversionProgress.Maximum=mc.Count;
			ctlConversionProgress.Value=0;
			ctlConversionProgress.Step=1;
			
			IndexNode objNikayaNode=null;

			foreach(Match m in mc)
			{
				if(m.Groups[1].Value.Substring(0,1)==strPitakaID)
				{
					string strName=FormatName(m.Groups[3].Value);

					if(Regex.IsMatch(m.Groups[1].Value,@"\d{2}00")) //nikaya
					{					
						objNikayaNode=new IndexNode(objPitakaNode.NodeID,m_nNodeCounter++,strName,true,-1,0xFF);
						objIndexList.Add(objNikayaNode);
					}
					else
					{
						string strAalekhFileName=m.Groups[2].Value;

						bool bConvert=true;
						if(!ctlMula.Checked && strAalekhFileName.IndexOf("mul")>-1)
							bConvert=false;

						if(!ctlAtthakatha.Checked && strAalekhFileName.IndexOf("att")>-1)
							bConvert=false;

						if(!ctlTika.Checked && strAalekhFileName.IndexOf("tik")>-1)
							bConvert=false;

						if(!ctlOtherContent.Checked && strAalekhFileName.IndexOf("nrf")>-1)
							bConvert=false;

						if(bConvert)
						{
							IndexNode objBookNode=null;
							if(objNikayaNode!=null)
								objBookNode=new IndexNode(objNikayaNode.NodeID,m_nNodeCounter++,strName,true,-1,0xFF);							
							else
								objBookNode=new IndexNode(objPitakaNode.NodeID,m_nNodeCounter++,strName,true,-1,0xFF);							
						
							objIndexList.Add(objBookNode);
					
							ConvertBook2(strAalekhFileName,objBookNode,objIndexList);						
						}
					}
				}
				ctlConversionProgress.PerformStep();
				Application.DoEvents();
			}
		}

		private void ConvertBook2(string strAalekhFileName,IndexNode objParentNode,ArrayList objIndexList)
		{
			FileStream fs=File.Open(ctlCSCDFolder.Text+@"CSCD\"+strAalekhFileName,FileMode.Open,FileAccess.Read);
			byte[] arrContent=new byte[fs.Length];
			fs.Read(arrContent,0,(int)fs.Length);
			fs.Close();

			AalekhDecoder objDecoder=new AalekhDecoder();
			char[] arrDecoded=objDecoder.AalekhToUnicode(arrContent,false,true);	
			string strFileContent=new string(arrDecoded);

			string[] arrChapters=Regex.Split(strFileContent,@"\\c11");				

			if(arrChapters.Length>1)
			{//split the book into chapters
				for(int nChapterCounter=1;nChapterCounter<arrChapters.Length;nChapterCounter++)
				{
					string strFileName="";
					if(arrChapters.Length==2)
						strFileName=strAalekhFileName+".htm";
					else
						strFileName=strAalekhFileName.Replace(".",nChapterCounter.ToString("_000")+".")+".htm";

					Match m=Regex.Match(arrChapters[nChapterCounter],".*",RegexOptions.Multiline);//chapter name
					ConvertChapter2(objParentNode.NodeID,m.Value.Trim(),strFileName,
						@"\c11"+arrChapters[nChapterCounter],objIndexList);
				}
			}
			else
			{//no chapters in this book. add it
				ConvertChapter2(objParentNode.NodeID,objParentNode.Name,strAalekhFileName+".htm",strFileContent,objIndexList);
			}
		}


		private void ConvertChapter2(int ParentNodeID,string strChapterName,string strFileName,string strChapterContent,ArrayList objIndexList)
		{
			strChapterName=Regex.Replace(strChapterName,@"[BVPTO]\d\.\d{4}","");
			strChapterName=Regex.Replace(strChapterName,@"{.*?}","").Trim();

			m_nParagraphID=0;
			m_arrChaptersList=new ArrayList();
			m_nParentNodeID=m_nNodeCounter++;
			m_nC13NodeID=-1;

			strChapterContent=Regex.Replace(strChapterContent,@"(\\c1(?:3|4)) (.+)",new MatchEvaluator(ParagraphEvaluator2),RegexOptions.Multiline | RegexOptions.IgnoreCase);

			if(ctlStripPageNumbers.Checked)
				strChapterContent = Regex.Replace(strChapterContent, "([BVTPO])(\\d\\.\\d{4})", "");
			
			switch(ctlDiffsHandling.SelectedIndex)
			{
				case 1:
					strChapterContent=Regex.Replace(strChapterContent,@"\{.*?\}",new MatchEvaluator(DiffsEvaluator),RegexOptions.Singleline);
					break;
				case 2:
					strChapterContent = Regex.Replace(strChapterContent, @"\{.*?\}", "",RegexOptions.Singleline);
					break;
			}
			//now format it
			AalekhFormatter objFormatter=new AalekhFormatter(strChapterContent.ToCharArray());
			char[] arrHTML=objFormatter.ToHTML();			

			//save content to archive
			byte[] arrFileMarker={0xEF,0xBB,0xBF};
			byte[] arrChapterBuffer=System.Text.Encoding.UTF8.GetBytes(arrHTML);
			
			if(m_objLibraryStream==null)
			{
				m_nZipFileIndex=0;
				string strLibFileName=String.Format("PocketLib{0}.zip",m_nLibFileIndex);
				m_objLibraryStream=new ZipOutputStream(File.Create(ctlDestFolder.Text+strLibFileName));
				m_objLibraryStream.SetLevel(5);
			}

			ZipEntry objChapterEntry=new ZipEntry(strFileName);
			m_objLibraryStream.PutNextEntry(objChapterEntry);
			m_objLibraryStream.Write(arrFileMarker,0,3);
			m_objLibraryStream.Write(arrChapterBuffer,0,arrChapterBuffer.Length);
			//save index to archive
			IndexNode objIndexNode=new IndexNode(ParentNodeID,m_nParentNodeID,strChapterName,(m_nParagraphID>0),m_nZipFileIndex,m_nLibFileIndex);
			objIndexList.Add(objIndexNode);
			foreach(IndexNode objParagraphNode in m_arrChaptersList)
			{
				objParagraphNode.ZipFileIndex=m_nZipFileIndex;
				objParagraphNode.LibFileIndex=m_nLibFileIndex;
				objIndexList.Add(objParagraphNode);
			}

			if(m_nZipFileIndex>499)
			{
				m_objLibraryStream.Finish();
				m_objLibraryStream.Close();
				m_objLibraryStream=null;
				m_nLibFileIndex++;
			}

			m_nZipFileIndex++;
		}

		private string ParagraphEvaluator2(Match objParaMatch)
		{
			m_nParagraphID++;

			//strip page numbers
			string strParaName=Regex.Replace(objParaMatch.Groups[2].Value,@"[BVPTO]\d\.\d{4}","");
			//strip differences
			strParaName=Regex.Replace(strParaName,@"{.*?}","").Trim();
			int ParentID=m_nParentNodeID;

			if(objParaMatch.Groups[1].Value=="\\c14" && m_nC13NodeID!=-1)
			{
				ParentID=m_nC13NodeID;
				IndexNode objPrevNode=(IndexNode)m_arrChaptersList[m_arrChaptersList.Count-1];
				if(objPrevNode.NodeID==m_nC13NodeID)
					objPrevNode.HasChildren=true;
			}

			IndexNode objIndexNode=new IndexNode(ParentID,m_nNodeCounter++,strParaName,false,-1,"p"+m_nParagraphID.ToString(),m_nLibFileIndex);
			m_arrChaptersList.Add(objIndexNode);	
			
			if(objParaMatch.Groups[1].Value=="\\c13")
				m_nC13NodeID=objIndexNode.NodeID;

			return objParaMatch.Groups[1].Value+"<a name=\"p"+m_nParagraphID.ToString()+"\">"+objParaMatch.Groups[2].Value.Trim()+"</a>";
		}
	}
}
