///
///
/// Copyright (C) 2005  Lennart Lopin <novalis78@gmx.net> 
/// All Rights Reserved.
///
/// This program is free software; you can redistribute it and/or
/// modify it under the terms of the GNU General Public License as
/// published by the Free Software Foundation; either version 2 of the
/// License, or (at your option) any later version.
/// 
/// This program is distributed in the hope that it will be useful, but
/// WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
/// General Public License for more details.
/// 
/// You should have received a copy of the GNU General Public License
/// along with this program; if not, write to the Free Software
/// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
/// 02111-1307, USA.
/// 
///

using System;
using BMSearch;
using System.IO;
using System.Text;
using System.Drawing;
using PluginInterface;
using PaliReaderUtils;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of UserControl1.	
	/// </summary>
	public class SearchDialog : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox QueryInputBox;
		private System.Windows.Forms.Button SearchBtn;
		private System.Windows.Forms.ProgressBar SearchProgressBar;
		private System.Windows.Forms.Button moreButton;
		private System.Windows.Forms.Label ProgressLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private string currentSearchItem = "";
		private ShowSearchResults ssr;
		private bool detailsDisplayed;
		private bool matchCase;
		private SearchResult bsr;
		
		private delegate void DelegateFeedback(object o, SearchDialog sd);

		public SearchDialog()
		{
			InitializeComponent();
			QueryInputBox.Focus();
			detailsDisplayed = false;
			StartPosition = FormStartPosition.CenterParent;
			ShowInTaskbar = false;
			Height = 178;
			if(ssr != null &&  !ssr.IsDisposed)
				ssr.Show();
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.ProgressLabel = new System.Windows.Forms.Label();
			this.moreButton = new System.Windows.Forms.Button();
			this.SearchProgressBar = new System.Windows.Forms.ProgressBar();
			this.SearchBtn = new System.Windows.Forms.Button();
			this.QueryInputBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.vinCheck = new System.Windows.Forms.CheckBox();
			this.sutCheck = new System.Windows.Forms.CheckBox();
			this.abhiCheck = new System.Windows.Forms.CheckBox();
			this.pcanCheck = new System.Windows.Forms.CheckBox();
			this.mulCheck = new System.Windows.Forms.CheckBox();
			this.attCheck = new System.Windows.Forms.CheckBox();
			this.tikCheck = new System.Windows.Forms.CheckBox();
			this.anuCheck = new System.Windows.Forms.CheckBox();
			this.allCheck = new System.Windows.Forms.CheckBox();
			this.tipCheck = new System.Windows.Forms.CheckBox();
			this.medCheck = new System.Windows.Forms.CheckBox();
			this.bookLabel = new System.Windows.Forms.Label();
			this.matCheck = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
			this.pictureBox1.Location = new System.Drawing.Point(56, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(304, 88);
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			// 
			// ProgressLabel
			// 
			this.ProgressLabel.Location = new System.Drawing.Point(8, 241);
			this.ProgressLabel.Name = "ProgressLabel";
			this.ProgressLabel.Size = new System.Drawing.Size(61, 16);
			this.ProgressLabel.TabIndex = 12;
			this.ProgressLabel.Text = "Progress:";
			//
			// moreButton
			// 
			this.moreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moreButton.Location = new System.Drawing.Point(363, 96);
			this.moreButton.Name = "moreButton";
			this.moreButton.Size = new System.Drawing.Size(31, 20);
			this.moreButton.TabIndex = 13;
			this.moreButton.Text = " ...";
			this.moreButton.Click += new System.EventHandler(this.MoreButtonClick);
			// 
			// SearchProgressBar
			// 
			this.SearchProgressBar.Location = new System.Drawing.Point(9, 260);
			this.SearchProgressBar.Name = "SearchProgressBar";
			this.SearchProgressBar.Size = new System.Drawing.Size(400, 23);
			this.SearchProgressBar.TabIndex = 9;
			// 
			// SearchBtn
			// 
			this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SearchBtn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.SearchBtn.Location = new System.Drawing.Point(312, 96);
			this.SearchBtn.Name = "SearchBtn";
			this.SearchBtn.Size = new System.Drawing.Size(48, 20);
			this.SearchBtn.TabIndex = 2;
			this.SearchBtn.Text = "Search";
			this.SearchBtn.Click += new System.EventHandler(this.SearchBtnClick);
			// 
			// QueryInputBox
			// 
			this.QueryInputBox.Location = new System.Drawing.Point(56, 96);
			this.QueryInputBox.Name = "QueryInputBox";
			this.QueryInputBox.Size = new System.Drawing.Size(256, 20);
			this.QueryInputBox.TabIndex = 0;
			this.QueryInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QueryInputBoxKeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Find:";
			// 
			// vinCheck
			// 
			this.vinCheck.Location = new System.Drawing.Point(57, 151);
			this.vinCheck.Name = "vinCheck";
			this.vinCheck.Size = new System.Drawing.Size(92, 24);
			this.vinCheck.TabIndex = 16;
			this.vinCheck.Text = "Vinayapitaka";
			this.vinCheck.UseVisualStyleBackColor = true;
			// 
			// sutCheck
			// 
			this.sutCheck.Location = new System.Drawing.Point(57, 171);
			this.sutCheck.Name = "sutCheck";
			this.sutCheck.Size = new System.Drawing.Size(92, 24);
			this.sutCheck.TabIndex = 17;
			this.sutCheck.Text = "Suttapitaka";
			this.sutCheck.UseVisualStyleBackColor = true;
			// 
			// abhiCheck
			// 
			this.abhiCheck.Location = new System.Drawing.Point(57, 190);
			this.abhiCheck.Name = "abhiCheck";
			this.abhiCheck.Size = new System.Drawing.Size(112, 24);
			this.abhiCheck.TabIndex = 18;
			this.abhiCheck.Text = "Abhidhammapitaka";
			this.abhiCheck.UseVisualStyleBackColor = true;
			// 
			// pcanCheck
			// 
			this.pcanCheck.Location = new System.Drawing.Point(57, 209);
			this.pcanCheck.Name = "pcanCheck";
			this.pcanCheck.Size = new System.Drawing.Size(112, 24);
			this.pcanCheck.TabIndex = 19;
			this.pcanCheck.Text = "Post Canonical";
			this.pcanCheck.UseVisualStyleBackColor = true;
			// 
			// mulCheck
			// 
			this.mulCheck.Location = new System.Drawing.Point(189, 151);
			this.mulCheck.Name = "mulCheck";
			this.mulCheck.Size = new System.Drawing.Size(92, 24);
			this.mulCheck.TabIndex = 20;
			this.mulCheck.Text = "Mula";
			this.mulCheck.UseVisualStyleBackColor = true;
			// 
			// attCheck
			// 
			this.attCheck.Location = new System.Drawing.Point(189, 171);
			this.attCheck.Name = "attCheck";
			this.attCheck.Size = new System.Drawing.Size(92, 24);
			this.attCheck.TabIndex = 21;
			this.attCheck.Text = "Atthakatha";
			this.attCheck.UseVisualStyleBackColor = true;
			// 
			// tikCheck
			// 
			this.tikCheck.Location = new System.Drawing.Point(189, 190);
			this.tikCheck.Name = "tikCheck";
			this.tikCheck.Size = new System.Drawing.Size(92, 24);
			this.tikCheck.TabIndex = 22;
			this.tikCheck.Text = "Tika";
			this.tikCheck.UseVisualStyleBackColor = true;
			// 
			// anuCheck
			// 
			this.anuCheck.Location = new System.Drawing.Point(189, 209);
			this.anuCheck.Name = "anuCheck";
			this.anuCheck.Size = new System.Drawing.Size(92, 24);
			this.anuCheck.TabIndex = 23;
			this.anuCheck.Text = "Anutika";
			this.anuCheck.UseVisualStyleBackColor = true;
			// 
			// allCheck
			// 
			this.allCheck.Location = new System.Drawing.Point(189, 121);
			this.allCheck.Name = "allCheck";
			this.allCheck.Size = new System.Drawing.Size(60, 24);
			this.allCheck.TabIndex = 24;
			this.allCheck.Text = "All";
			this.allCheck.UseVisualStyleBackColor = true;
			this.allCheck.CheckedChanged += new System.EventHandler(this.AllCheckCheckedChanged);
			// 
			// tipCheck
			// 
			this.tipCheck.Location = new System.Drawing.Point(56, 121);
			this.tipCheck.Name = "tipCheck";
			this.tipCheck.Size = new System.Drawing.Size(98, 24);
			this.tipCheck.TabIndex = 25;
			this.tipCheck.Text = "Tipitaka";
			this.tipCheck.UseVisualStyleBackColor = true;
			this.tipCheck.CheckedChanged += new System.EventHandler(this.TipCheckCheckedChanged);
			// 
			// medCheck
			// 
			this.medCheck.Location = new System.Drawing.Point(118, 121);
			this.medCheck.Name = "medCheck";
			this.medCheck.Size = new System.Drawing.Size(68, 24);
			this.medCheck.TabIndex = 26;
			this.medCheck.Text = "Medieval";
			this.medCheck.UseVisualStyleBackColor = true;
			this.medCheck.CheckedChanged += new System.EventHandler(this.MedCheckCheckedChanged);
			// 
			// bookLabel
			// 
			this.bookLabel.Location = new System.Drawing.Point(140, 241);
			this.bookLabel.Name = "bookLabel";
			this.bookLabel.Size = new System.Drawing.Size(266, 16);
			this.bookLabel.TabIndex = 27;
			this.bookLabel.Text = "...";
			this.bookLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// matCheck
			// 
			this.matCheck.Location = new System.Drawing.Point(302, 151);
			this.matCheck.Name = "matCheck";
			this.matCheck.Size = new System.Drawing.Size(92, 24);
			this.matCheck.TabIndex = 28;
			this.matCheck.Text = "Match case";
			this.matCheck.UseVisualStyleBackColor = true;
			this.matCheck.CheckedChanged += new System.EventHandler(this.MatCheckCheckedChanged);
			// 
			// SearchDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(418, 286);
			this.Controls.Add(this.matCheck);
			this.Controls.Add(this.bookLabel);
			this.Controls.Add(this.medCheck);
			this.Controls.Add(this.tipCheck);
			this.Controls.Add(this.allCheck);
			this.Controls.Add(this.anuCheck);
			this.Controls.Add(this.tikCheck);
			this.Controls.Add(this.attCheck);
			this.Controls.Add(this.mulCheck);
			this.Controls.Add(this.pcanCheck);
			this.Controls.Add(this.abhiCheck);
			this.Controls.Add(this.sutCheck);
			this.Controls.Add(this.vinCheck);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.ProgressLabel);
			this.Controls.Add(this.SearchProgressBar);
			this.Controls.Add(this.SearchBtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.QueryInputBox);
			this.Controls.Add(this.moreButton);
			this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SearchDialog";
			this.Text = "Search ...";
			this.Activated += new System.EventHandler(this.SearchDialogActivated);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox matCheck;
		private System.Windows.Forms.CheckBox medCheck;
		private System.Windows.Forms.Label bookLabel;
		private System.Windows.Forms.CheckBox tipCheck;
		private System.Windows.Forms.CheckBox allCheck;
		private System.Windows.Forms.CheckBox anuCheck;
		private System.Windows.Forms.CheckBox tikCheck;
		private System.Windows.Forms.CheckBox attCheck;
		private System.Windows.Forms.CheckBox mulCheck;
		private System.Windows.Forms.CheckBox pcanCheck;
		private System.Windows.Forms.CheckBox abhiCheck;
		private System.Windows.Forms.CheckBox sutCheck;
		private System.Windows.Forms.CheckBox vinCheck;
		#endregion
				
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "CanonSearch";
		string myDisplayName = "Search";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Fast and in-depth search within the entire Pali Canon";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\search.png");
		
        
		void PluginInterface.IPlugin.Dispose()
		{	
		}
		public void SetPluginParameter(Object o)
		{
		}
		public Object GetPluginParameter(Object o)
		{
			return currentSearchItem;
		}		
		public Image PluginIcon
		{
			get
			{
				return myPluginIcon;
			}
		}
		
		public string DisplayName
		{
			get
			{
				return myDisplayName;
			}
		}

		public string Description
		{
			get
			{
				return myPluginDescription;
			}
		}

		public string Author
		{
			get
			{
				return myPluginAuthor;
			}
		}

		public IPluginHost Host
		{
			get
			{
				return myPluginHost;
			}
			set
			{
				myPluginHost = value;
			}
		}
		//This method is being called after the plugin has been created!
		//Any reference to the host application cannot be invoked in the plugins own
		//constructor which would otherwise occur to early (during construction)..
		//This method can be used to do initial stuff WITH access to the proper host
		//reference...
		public void Initialize()
		{
			//test host
			if(this.Host == null) return;
			System.Windows.Forms.ToolBar tb = this.Host.GetToolBarReference();
			if(tb != null) tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(ToolbarClick);
			this.Host.AddToolBarButton(DisplayName, PluginIcon);
		}

	
		
		public string PluginName
		{
			get
			{
				return myPluginName;
			}
		}
		
		public System.Windows.Forms.Form MainInterface
		{
			get
			{
				return this;
			}
		}

		public string Version
		{
			get
			{
				return myPluginVersion;
			}
		}
		#endregion
		
		private void ToolbarClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Text == DisplayName)
			{
				if(detailsDisplayed && bsr != null)
				{
					ssr = null;
					ssr = new ShowSearchResults(bsr);
					ssr.OpenBookEvent += new ShowSearchResults.OpenBook(this.TriggerOpenBook);
					ssr.Closing += new System.ComponentModel.CancelEventHandler(this.Closing_ResultWindow);	
					ssr.ShowDialog();
				}
				else
				{
					this.ShowDialog();
					this.Host.StatusBarText("Please enter your search string");
				}
			}
		}
			
		void MoreButtonClick(object sender, System.EventArgs e)
		{
			if(detailsDisplayed == true)
			{
				this.Height = 178;
				detailsDisplayed = false;
			}
			else
			{
				this.Height = 315;
				detailsDisplayed = true;
			}
		}
		
		#region Main search logic
		/// <summary>
		/// Run search in background thread
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SearchBtnClick(object sender, System.EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			MethodInvoker mi = new MethodInvoker(Search);
			mi.BeginInvoke(new AsyncCallback(SearchCallBack), null);
		}
		
		/// <summary>
		/// Search callback triggers the result dialog
		/// </summary>
		/// <param name="ar"></param>
		private void SearchCallBack(IAsyncResult ar)
		{
			Cursor = Cursors.Default;
			this.Hide();
			detailsDisplayed = true;
			ssr = new ShowSearchResults(bsr);
			ssr.OpenBookEvent += new ShowSearchResults.OpenBook(this.TriggerOpenBook);
			ssr.Closing += new System.ComponentModel.CancelEventHandler(this.Closing_ResultWindow);	
			ssr.ShowDialog();
			
		}
		
		/// <summary>
		/// Main search method. Uses Boyer Moore approach.
		/// Full text search through entire canon.
		/// </summary>
		private void Search()
		{
			//TODO
			//so, wir spielen hier verschieden szenarien durch
			//letztendlich muss geschwindigkeit gegen speicherplatz abgewogen werden:
			//corpus als plain text durchsuchen
			//corpus aus html zip tmp extr. und durchsuchen
			//corpus aus bin zip tmp extr. und durchsuchen
			//corpus aus txt zip tmp extr. und durchsuchen
			//noch nötig: implementieren von stringsearch 4 java für bms und wildcards
			try
			{
				bool onlineMode = Convert.ToBoolean(PaliReaderUtils.PropertyManager.GetInstance().GetGeneralProperty("OnlineMode"));
				Cursor.Current = Cursors.WaitCursor;
				string[] files = Directory.GetFiles(@"Canon/index");
				
				if(files.Length <= 0 && onlineMode)
				{
					MessageBox.Show("It appears that you are working in the online Mode. Please note, that PTR's search ability is *currently* limited to offline editions only!");
					return;
				}
				if(files.Length <= 0)
				{
					MessageBox.Show("No pali canon files found to search in");
					return;
				}
				SearchProgressBar.Maximum = files.Length;
				int index = 0;
				bsr = new SearchResult();
				currentSearchItem = QueryInputBox.Text.Trim();
				int length = currentSearchItem.Length;
				try
				{
					for(int i = 0; i < files.Length; i++)
					{
						string book = files[i];
						SearchProgressBar.Increment(1);
						bookLabel.Text = NamingConverter.GetBookNameFromFile(book) + " " + i.ToString() + ". book";
						if(SkipFile(book))
							continue;
						String content = getContent(book);
						CIBMSearcher BMS = new CIBMSearcher(currentSearchItem, matchCase);
						//search through book as long as you find a match
						while(index > -1)
						{
							index = BMS.Search(content, index+1);
						
							if(index > 0 && index < content.Length)
							{
								string occ = getPhrase(index, length, content);
								occ = occ.Trim();
								bsr.AddMatch(book, occ);
							}
						}
						index = 0;
					}
				}
				catch(System.Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message.ToString());
				}
				SearchProgressBar.Value = 0;
				
				//SearchCallBack(null);
			}
			catch (System.Exception e)
			{
				MessageBox.Show(" caught a " + e.GetType() + "\n with message: " + e.Message);
			}
		}
		#endregion
		
		#region search utilities
		/// <summary>
		/// retrieve book's content
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		private string getContent(string b)
		{
			FileStream fs = new FileStream(b, FileMode.Open);
			StreamReader sr = new StreamReader(fs, System.Text.Encoding.Unicode);
			return sr.ReadToEnd();
		}
		
		/// <summary>
		/// get match - surrounding word/phrase 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="a"></param>
		/// <returns></returns>	
		private string getPhrase(int i, int l, String a)
		{
			int end = a.IndexOf(" ", i + l);
			int start   = a.LastIndexOf(" ", i + 1);
			if(start < end && start > -1 && end > -1)
				return a.Substring(start, end - start);
			else
				return "";
		}
		#endregion	
			
		/// <summary>
		/// 1.)Keep search result
		/// 2.)Close search result dialog
		/// 3.)Open book through UI thread based "Feedback" method
		/// BUT because SearchResultCallback brought us here, we are
		/// still on the background thread, which was initiated on pressing "Search" 
		/// (see above, method "Search")
		/// To access the main UI thread based Feedback method we have to marshal the data
		/// through an invoke!
		/// </summary>
		/// <param name="bookName"></param>
		public void TriggerOpenBook(string bookName)
		{
			detailsDisplayed = true;
			string bookPos = bookName + "#" + currentSearchItem;
			DelegateFeedback df = new DelegateFeedback(Host.Feedback);
			
			if(this.Host.GetMainMenuReference().GetForm().InvokeRequired)
			{
				this.Host.GetMainMenuReference().GetForm().Invoke(df, new object[]{bookPos, this});
			}
			else
				this.Host.Feedback(bookPos, this);
		}
		
		void QueryInputBoxKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
			else if (e.KeyCode == Keys.Enter)
			{
				Search();
			}
			else if( ( e.KeyCode == Keys.M ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ṃ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.A ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ā";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.I ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ī";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.U ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ū";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.D ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ḍ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.T ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ṭ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.L ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ḷ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.G ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ṅ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.N ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ṇ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.J ) && ( e.Modifiers == Keys.Alt ) )
			{
				QueryInputBox.Text += "ñ";
				string cmd = "{RIGHT " + QueryInputBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
		}
		
		private void Closing_ResultWindow(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(ssr != null)
				ssr.Dispose();
			if(ssr.keepResult)
				detailsDisplayed = true;
			else
				detailsDisplayed = false;
		}
		
		void AllCheckCheckedChanged(object sender, System.EventArgs e)
		{
			if(this.allCheck.Checked)
			{
				this.vinCheck.Checked = true;
				this.sutCheck.Checked = true;
				this.pcanCheck.Checked = true;
				this.mulCheck.Checked = true;
				this.attCheck.Checked = true;
				this.tikCheck.Checked = true;
				this.anuCheck.Checked = true;
				this.abhiCheck.Checked = true;
			}
			else
			{
				this.vinCheck.Checked = false;
				this.sutCheck.Checked = false;
				this.pcanCheck.Checked = false;
				this.mulCheck.Checked = false;
				this.attCheck.Checked = false;
				this.tikCheck.Checked = false;
				this.anuCheck.Checked = false;
				this.abhiCheck.Checked = false;
			}
		}
		
		/// <summary>
		/// Filter definition
		/// </summary>
		/// <param name="f"></param>
		/// <returns></returns>
		private bool SkipFile(string f)
		{
			f = f.ToLower();
			bool skip = false;
			if (this.allCheck.Checked)
				return skip;
			if (this.medCheck.Checked)
			{
				if (f.Contains("mul") && !this.mulCheck.Checked)
					skip = true;
				return skip;
			}
			if (this.tipCheck.Checked)
			{
				if (f.Contains("att") && !this.attCheck.Checked)
					skip = true;
				if (f.Contains("tik") && !this.tikCheck.Checked)
					skip = true;
				if (f.Contains("nrf") && !this.anuCheck.Checked)
					skip = true;
				return skip;
			}
			if (f.Contains("mul") && !this.mulCheck.Checked)
				skip = true;
			if (f.Contains("att") && !this.attCheck.Checked)
				skip = true;
			if (f.Contains("tik") && !this.tikCheck.Checked)
				skip = true;
			if (f.Contains("nrf") && !this.anuCheck.Checked)
				skip = true;
			if (f.Contains("s") && !this.sutCheck.Checked)
				skip = true;
			if (f.Contains("v") && !this.vinCheck.Checked)
				skip = true;
			if (f.Contains("a") && !this.attCheck.Checked)
				skip = true;
			return skip;
		}
		
		void TipCheckCheckedChanged(object sender, System.EventArgs e)
		{
			if(this.tipCheck.Checked)
			{
				this.vinCheck.Checked = true;
				this.sutCheck.Checked = true;
				this.mulCheck.Checked = true;
				this.abhiCheck.Checked = true;
			}
			else
			{
				this.vinCheck.Checked = false;
				this.sutCheck.Checked = false;
				this.mulCheck.Checked = false;
				this.abhiCheck.Checked = false;
			}
		}
		
		void MedCheckCheckedChanged(object sender, System.EventArgs e)
		{
			if(this.medCheck.Checked)
			{
				this.attCheck.Checked = true;
				this.tikCheck.Checked = true;
				this.anuCheck.Checked = true;
				this.pcanCheck.Checked = true;
			}
			else
			{
				this.attCheck.Checked = false;
				this.tikCheck.Checked = false;
				this.anuCheck.Checked = false;
				this.pcanCheck.Checked = false;
			}
		}
		
		void SearchDialogActivated(object sender, System.EventArgs e)
		{
			if(ssr != null && !ssr.IsDisposed)
			{
				ssr.ShowDialog();
				this.Hide();
			}
		}
		
		void MatCheckCheckedChanged(object sender, System.EventArgs e)
		{
			if(matchCase)
				matchCase = false;
			else
				matchCase = true;
		}
		
		
	}
}
