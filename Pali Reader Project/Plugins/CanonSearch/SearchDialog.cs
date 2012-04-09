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
using System.IO;
using System.Text;
using System.Drawing;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using Document = Lucene.Net.Documents.Document;
using QueryParser = Lucene.Net.QueryParsers.QueryParser;
using Hits = Lucene.Net.Search.Hits;
using IndexSearcher = Lucene.Net.Search.IndexSearcher;
using Query = Lucene.Net.Search.Query;
using Searcher = Lucene.Net.Search.Searcher;
using Lucene.Net.Search.Highlight;
using TokenStream = Lucene.Net.Analysis.TokenStream;
using System.Windows.Forms;
using System.ComponentModel;
using PluginInterface;
using System.Text.RegularExpressions;
using System.Collections;

namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of UserControl1.	
	/// </summary>
	public class SearchDialog : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox QueryInputBox;
		private System.Windows.Forms.Button SearchBtn;
		private System.Windows.Forms.ProgressBar SearchProgressBar;
		private System.Windows.Forms.Button moreButton;
		private System.Windows.Forms.Label ProgressLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private bool showingDetails = false;
		private ShowSearchResults ssr;

		public SearchDialog()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			this.Height = 178;
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SearchDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.ProgressLabel = new System.Windows.Forms.Label();
			this.moreButton = new System.Windows.Forms.Button();
			this.SearchProgressBar = new System.Windows.Forms.ProgressBar();
			this.SearchBtn = new System.Windows.Forms.Button();
			this.QueryInputBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(56, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(304, 88);
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			// 
			// ProgressLabel
			// 
			this.ProgressLabel.Location = new System.Drawing.Point(8, 208);
			this.ProgressLabel.Name = "ProgressLabel";
			this.ProgressLabel.Size = new System.Drawing.Size(400, 16);
			this.ProgressLabel.TabIndex = 12;
			this.ProgressLabel.Text = "Progress:";
			this.ProgressLabel.Click += new System.EventHandler(this.Label4Click);
			// 
			// moreButton
			// 
			this.moreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moreButton.Location = new System.Drawing.Point(312, 128);
			this.moreButton.Name = "moreButton";
			this.moreButton.Size = new System.Drawing.Size(104, 24);
			this.moreButton.TabIndex = 13;
			this.moreButton.Text = "Details ...";
			this.moreButton.Click += new System.EventHandler(this.MoreButtonClick);
			// 
			// SearchProgressBar
			// 
			this.SearchProgressBar.Location = new System.Drawing.Point(8, 229);
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
			this.QueryInputBox.Text = "";
			this.QueryInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QueryInputBoxKeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Find:";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(288, 250);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(120, 16);
			this.linkLabel1.TabIndex = 14;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "powered by DotLucene";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// SearchDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(418, 264);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.ProgressLabel);
			this.Controls.Add(this.SearchProgressBar);
			this.Controls.Add(this.SearchBtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.QueryInputBox);
			this.Controls.Add(this.moreButton);
			this.Controls.Add(this.linkLabel1);
			this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SearchDialog";
			this.Text = "Search ...";
			this.Load += new System.EventHandler(this.SearchDialogLoad);
			this.ResumeLayout(false);
		}
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
			return null;
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
			if(e.Button.Text == DisplayName)
			{
				this.ShowDialog();
				this.Host.StatusBarText("Please enter your search string");
			}
		}

		void CheckBox1CheckedChanged(object sender, System.EventArgs e)
		{
			
		}
		
		void Button2Click(object sender, System.EventArgs e)
		{
			
		}
		
		void SearchDialogLoad(object sender, System.EventArgs e)
		{
			
		}
		
		void MoreButtonClick(object sender, System.EventArgs e)
		{
			if(showingDetails == true)
			{
				this.Height = 178;
				showingDetails = false;
			}
			else
			{
				this.Height = 288;
				showingDetails = true;
			}
				
		}
		
		void SearchBtnClick(object sender, System.EventArgs e)
		{
			Search();
		}
		
		private void Search()
		{
			try
			{
				SearchProgressBar.Maximum = 11;
				ProgressLabel.Text = "Progress: Initialize Search ...";
				Searcher searcher = new IndexSearcher(@"Canon\index");
				Analyzer analyzer = new StandardAnalyzer();
				ArrayList resultList = new ArrayList();
				
				System.IO.StreamReader in_Renamed = new System.IO.StreamReader(new System.IO.StreamReader(System.Console.OpenStandardInput(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(System.Console.OpenStandardInput(), System.Text.Encoding.Default).CurrentEncoding);
				
				String line = QueryInputBox.Text;
				if (line.Length == - 1)
					return;
				ProgressLabel.Text = "Progress: Parsing Query ...";
				Query query = QueryParser.Parse(line, "contents", analyzer);
				//int[] ix = qtm.GetTermFrequencies();
				
				Hits hits = searcher.Search(query);
				SearchProgressBar.Increment(1);
				ProgressLabel.Text = "Progress: Searched. Analyzing results ...";
				
				//QueryHighlightExtractor highlighter = new QueryHighlightExtractor(query, new WhitespaceAnalyzer(), "<B>", "</B>");
				Highlighter highlighter = new Highlighter(new QueryScorer(query));
				highlighter.SetTextFragmenter(new SimpleFragmenter(80));
				int maxNumFragmentsRequired = 1;
								
					//int HITS_PER_PAGE = 10;
					for (int i = 0; i < 10; i++)
					{
							SearchProgressBar.Increment(1);
							ProgressLabel.Text = "Progress: Analyzing hit " + (i+1).ToString();
							// get the document from index
							Document doc = hits.Doc(i);
							//SegmentReader ir = new SegmentReader();
							//Lucene.Net.Index.TermFreqVector tfv = 
							//tfv.GetTermFrequencies
							string score = hits.Score(i).ToString();
							//Box += "Hit no. " + i + " scored: " + score + " occ: " + /*highlighter.tokenFrequency */ " best fragment: \n";
							ResultSet a = new ResultSet();
							a.BookName = doc.Get("path").Replace(@"c:\cscd\temp\","");
							a.Score = hits.Score(i);
							a.numberOfHits = hits.Length();
							
							// get the document filename
							// we can't get the text from the index 
							//because we didn't store it there
							//so get it from archive
							string path = doc.Get("path");
							string name = GetInternalName(path);
							PaliReaderUtils.AalekhDecoder.UnzipFromZipLibrary(name);
							path = System.IO.Directory.GetCurrentDirectory() + @"\Work\" + name + ".htm";
							string plainText = "";
							//load text from zip archive temporarily
							using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
							{
								plainText = parseHtml(sr.ReadToEnd());
							}
			//-------------------------------Highlighter Code 1.4
							TokenStream tokenStream = analyzer.TokenStream(new StringReader(plainText));				
							a.textFragment = highlighter.GetBestFragments(tokenStream, plainText, maxNumFragmentsRequired, "...");
							if(File.Exists(path))
								File.Delete(path);
			//-------------------------------
							resultList.Add(a);
						}
				SearchProgressBar.Value = 0;
				searcher.Close();
				ssr = new ShowSearchResults(/*Box*/resultList);
				//this.Hide();
				ssr.OpenBookEvent += new ShowSearchResults.OpenBook(this.TriggerOpenBook);
				ssr.Closing += new System.ComponentModel.CancelEventHandler(this.Closing_ResultWindow);
				this.Hide();
				ssr.ShowDialog();
			
			}
			catch (System.Exception e)
			{
				MessageBox.Show(" caught a " + e.GetType() + "\n with message: " + e.Message);
			}
		}
		
		private string parseHtml(string html)
		{
			string temp = Regex.Replace(html, "<[^>]*>", "");
			return temp.Replace("&nbsp;", " ");
		}
		
		public void TriggerOpenBook(string bookName)
		{
			ssr.Hide();
			this.Host.SignalReadyBookExtraction(bookName);
		}
		
		private string GetInternalName(string a)
		{
			
			string[] name = a.Replace(@"c:\cscd\temp\","").Split('.');
			string bookName = name[0].ToUpper() + "." + name[1].ToUpper();
			return bookName;
		}
		void Label4Click(object sender, System.EventArgs e)
		{
			
		}
		
		void QueryInputBoxKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
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
			this.Show();
		}
		
		void LinkLabel1LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("iexplore.exe", "http://www.dotlucene.net/");
		}
		
		private string Utf8ToAnsi(string value)
		{
//			MessageBox.Show(value);
			value = value.Replace("\\r", "");
			value = value.Replace("\\n", "");
			byte[] utf8Bytes = new byte[value.Length];
			for (int i=0; i < utf8Bytes.Length; i++)
			{
				utf8Bytes[i] = (byte)value[i];
			}
			return Encoding.UTF8.GetString(utf8Bytes);
		}
		
		private string Utf8ToUnicode(string value)
		{
		  byte[] utf8Bytes = Encoding.GetEncoding(65001).GetBytes(value.ToCharArray());
		  return  Encoding.GetEncoding(65001).GetString(utf8Bytes);	
		}
		
		private string anotherConversion(string value)
		{
			// Create two different encodings.
         	Encoding utf8 = Encoding.UTF8;
         	Encoding unicode = Encoding.Unicode;

         	// Convert the string into a byte[].
         	byte[] utf8Bytes = utf8.GetBytes(value);

        	// Perform the conversion from one encoding to the other.
            byte[] unicodeBytes = Encoding.Convert(utf8, unicode, utf8Bytes);
            
         	char[] inChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
	        unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, inChars, 0);
         	string uniString = new string(inChars);
			return uniString;
		}
	}
	public class ResultSet
	{
		public float Score;
		public string BookName;
		public int numberOfHits;
		public string textFragment;
	
	}
}
