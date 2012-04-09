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
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using PluginInterface;

namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of PaliTranslator
	/// </summary>
	public class PaliDictionary : System.Windows.Forms.Form, IPlugin
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button EditEntryBtn;
		private System.Windows.Forms.Button RemoveBtn;
		private System.Windows.Forms.ToolTip KeyToolTip;
		private System.Windows.Forms.RichTextBox meaningBox;
		private System.Windows.Forms.ListBox ListOfEntries;
		private System.Windows.Forms.ComboBox LanguageSelectionBox;
		private System.Windows.Forms.Button AddBtn;
		private System.Windows.Forms.RichTextBox SearchBox;
		private System.Windows.Forms.Button CancelBtn;
		private static int entryNumber = 0;
		public  static string keyword = "";
		private static SortedList dictionaryTable = null;
		private static ArrayList searchResults = null;
		private static int tempSearchResult = -1;
		private static string tmpSearchBoxText;
		private static string tmpMeaningBoxText;
		private static bool dictionaryUpdated = false;
		private static string lastSearchBoxEntry = "";
		private static String startUpWord = "";
		
		public PaliDictionary()
		{
			try
			{
				InitializeComponent();
			
				StartPosition = FormStartPosition.CenterParent;
				LoadListOfDictionaries();
				LoadDictionary("pi");
				LoadListOfEntries();
			}
			catch(System.Exception e)
			{
				MessageBox.Show(e.Message.ToString());
			}
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new PaliDictionary());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.SearchBox = new System.Windows.Forms.RichTextBox();
			this.AddBtn = new System.Windows.Forms.Button();
			this.LanguageSelectionBox = new System.Windows.Forms.ComboBox();
			this.ListOfEntries = new System.Windows.Forms.ListBox();
			this.meaningBox = new System.Windows.Forms.RichTextBox();
			this.KeyToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.RemoveBtn = new System.Windows.Forms.Button();
			this.EditEntryBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CancelBtn
			// 
			this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.CancelBtn.Location = new System.Drawing.Point(112, 200);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(64, 21);
			this.CancelBtn.TabIndex = 3;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
			// 
			// SearchBox
			// 
			this.SearchBox.Location = new System.Drawing.Point(64, 15);
			this.SearchBox.Multiline = false;
			this.SearchBox.Name = "SearchBox";
			this.SearchBox.Size = new System.Drawing.Size(344, 22);
			this.SearchBox.TabIndex = 0;
			this.SearchBox.Text = "look up word";
			this.KeyToolTip.SetToolTip(this.SearchBox, "Special Characters: \r\n ALT+m for ṃ,\r\n ALT+a for ā,\r\n ALT+i for ī,\r\n ALT+u for ū,\r" +
"\nALT+g for ṅ,\r\n ALT+j for ñ,\r\n ALT+n for ṇ,\r\n ALT+t for ṭ,\r\n ALT+d for ḍ,\r\n ALT+" +
"l for ḷ");
			this.SearchBox.WordWrap = false;
			this.SearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBoxKeyDown);
			this.SearchBox.Leave += new System.EventHandler(this.SearchBoxLeave);
			this.SearchBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SearchBoxMouseDown);
			this.SearchBox.Enter += new System.EventHandler(this.SearchBoxEnter);
			// 
			// AddBtn
			// 
			this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.AddBtn.Location = new System.Drawing.Point(8, 200);
			this.AddBtn.Name = "AddBtn";
			this.AddBtn.Size = new System.Drawing.Size(40, 21);
			this.AddBtn.TabIndex = 9;
			this.AddBtn.Text = "Add";
			this.AddBtn.Click += new System.EventHandler(this.AddBtnClick);
			// 
			// LanguageSelectionBox
			// 
			this.LanguageSelectionBox.Location = new System.Drawing.Point(440, 15);
			this.LanguageSelectionBox.Name = "LanguageSelectionBox";
			this.LanguageSelectionBox.Size = new System.Drawing.Size(40, 21);
			this.LanguageSelectionBox.TabIndex = 8;
			this.LanguageSelectionBox.DropDown += new System.EventHandler(this.LanguageSelectionBoxDropDown);
			this.LanguageSelectionBox.SelectedIndexChanged += new System.EventHandler(this.LanguageSelectionBoxSelectedIndexChanged);
			// 
			// ListOfEntries
			// 
			this.ListOfEntries.Location = new System.Drawing.Point(8, 45);
			this.ListOfEntries.Name = "ListOfEntries";
			this.ListOfEntries.Size = new System.Drawing.Size(168, 95);
			this.ListOfEntries.TabIndex = 6;
			this.ListOfEntries.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListOfEntriesKeyDown);
			this.ListOfEntries.SelectedIndexChanged += new System.EventHandler(this.ListBox1SelectedIndexChanged);
			// 
			// meaningBox
			// 
			this.meaningBox.Location = new System.Drawing.Point(184, 45);
			this.meaningBox.Name = "meaningBox";
			this.meaningBox.ReadOnly = true;
			this.meaningBox.Size = new System.Drawing.Size(296, 185);
			this.meaningBox.TabIndex = 2;
			this.meaningBox.Text = "meaning..";
			// 
			// KeyToolTip
			// 
			this.KeyToolTip.AutomaticDelay = 1000;
			// 
			// RemoveBtn
			// 
			this.RemoveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.RemoveBtn.Location = new System.Drawing.Point(52, 200);
			this.RemoveBtn.Name = "RemoveBtn";
			this.RemoveBtn.Size = new System.Drawing.Size(55, 21);
			this.RemoveBtn.TabIndex = 10;
			this.RemoveBtn.Text = "Remove";
			this.RemoveBtn.Click += new System.EventHandler(this.RemoveClick);
			// 
			// EditEntryBtn
			// 
			this.EditEntryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.EditEntryBtn.Location = new System.Drawing.Point(8, 171);
			this.EditEntryBtn.Name = "EditEntryBtn";
			this.EditEntryBtn.Size = new System.Drawing.Size(168, 21);
			this.EditEntryBtn.TabIndex = 4;
			this.EditEntryBtn.Text = "Edit entry";
			this.EditEntryBtn.Click += new System.EventHandler(this.EditEntryBtnClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "Look up:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(416, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 21);
			this.label2.TabIndex = 7;
			this.label2.Text = "in";
			// 
			// PaliDictionary
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(482, 230);
			this.Controls.Add(this.RemoveBtn);
			this.Controls.Add(this.AddBtn);
			this.Controls.Add(this.LanguageSelectionBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ListOfEntries);
			this.Controls.Add(this.EditEntryBtn);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.meaningBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SearchBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PaliDictionary";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaliDictionaryKeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PaliDictionaryClosing);
			this.Load += new System.EventHandler(this.PaliDictionaryLoad);
			this.Activated += new System.EventHandler(this.PaliDictionaryActivated);
			this.ResumeLayout(false);
		}
		#endregion
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "Pali Dictionary";
		string myDisplayName = "Dictionary";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Editable open Pali-English dictionary";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\dict.png");
		
        
		void PluginInterface.IPlugin.Dispose()
		{	
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
		public void SetPluginParameter(Object o)
		{
			startUpWord = (String)o;
		}
		public Object GetPluginParameter(Object o)
		{
			return DoFastDictionaryLookup((String)o);
		}
		#endregion
		
		private void ToolbarClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button.Text == DisplayName)
			{
				this.StartPosition = FormStartPosition.CenterParent;
				this.ShowDialog();
				this.Host.StatusBarText("Please enter a word for look-up");
			}
		}
		
		void CancelBtnClick(object sender, System.EventArgs e)
		{
			if(CancelBtn.Text == "Cancel update")
			{
				ListOfEntries.Show();
				LanguageSelectionBox.Show();
				SearchBox.Text = tmpSearchBoxText;
				SearchBox.BackColor = Color.White;
				SearchBox.ReadOnly = false;
				meaningBox.BackColor = Color.White;
				meaningBox.ReadOnly = true;
				label1.Text = "Look up:";
				label2.Text = "in";
				EditEntryBtn.Text = "Edit entry";
				CancelBtn.Text = "Cancel";
			}
			else
				this.Close();
		}
		
		public void LoadListOfEntries()
		{
			if(dictionaryTable ==  null)
				return;
			ListOfEntries.Items.Clear();
			IDictionaryEnumerator ie = dictionaryTable.GetEnumerator();
			
			while(ie.MoveNext())
			{
				ListOfEntries.Items.Add((String)ie.Key);
			}	
		}
		private string DoFastDictionaryLookup(string a)
		{
			try
			{
				String s = a.Trim();
				int    l = s.Length;
				
				for(int q = s.Length; q > 0; q--)
				{
					string strap = s.Substring(0, q);
					if(dictionaryTable.ContainsKey(strap))
					{
						return (String)dictionaryTable[(String)strap];
					}
				}
			}
			catch(Exception e)
			{
				;
			}
			return "";
		}
	
		private void DoDictionaryLookup(string a)
		{
			try
			{
				String s = a.Trim();
				int    l = s.Length;
				int index = 0;
				index    = ListOfEntries.SelectedIndex;
				
				for(int q = s.Length; q > 0; q--)
				{
					string strap = s.Substring(0, q);
					if(dictionaryTable.ContainsKey(strap))
					{
						ListOfEntries.SelectedIndex = dictionaryTable.IndexOfKey(strap);
						break;//found identical entry in dic
					}
				}
				
			//there was no 100% match
			//now look for softer match - go through Dictionary
			//and see whether the search string is part of a key
			if(index == 0 || index == ListOfEntries.SelectedIndex)
			{
				if(searchResults != null)
					searchResults.Clear();
				//happens, if we moved after a search with up/down keys
				//and then, try to look up again...
				else
				{
					searchResults = new ArrayList();
				}
				//now: check whether any dictionary entry contains
				//our search word...
				IDictionaryEnumerator ide = dictionaryTable.GetEnumerator();
				while(ide.MoveNext())
				{
					if(((String)ide.Key).IndexOf(SearchBox.Text.Trim()) > -1)
					{
						searchResults.Add(dictionaryTable.IndexOfKey(ide.Key));
					}
				}
				//show first hit
				if(searchResults.Count > 0 )
				{
					ListOfEntries.SelectedIndex = (int)searchResults[0];
					tempSearchResult = 0;
					SearchBox.Text += "  (...found more matches -> use <F3>)";
				}
				//finally, if we still did not find any result
				//check all dictionary entries which may be part OF our search
				//string (there will be too many matches if we start with this
				//lookup first
				else
				{
					ide = null;
					ide = dictionaryTable.GetEnumerator();
					while(ide.MoveNext())
					{
						if(SearchBox.Text.Trim().IndexOf(((String)ide.Key)) > -1)
						{
							searchResults.Add(dictionaryTable.IndexOfKey(ide.Key));
						}
					}
					//show first hit
					if(searchResults.Count > 0 )
					{
						ListOfEntries.SelectedIndex = (int)searchResults[0];
						tempSearchResult = 0;
						SearchBox.Text += "  (...found more matches -> use <F3>)";
					}	
				}
			  }
			}
			catch(System.Exception ex)
			{
				meaningBox.Text = ex.Message.ToString();
			}
		}
		
		void SearchBoxKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				DoDictionaryLookup(SearchBox.Text.Trim());
			}
			//browse through results
			if(e.KeyCode == Keys.F3)
			{
				if(tempSearchResult != -1 && tempSearchResult < searchResults.Count)
				{
					ListOfEntries.SelectedIndex = (int)searchResults[tempSearchResult++];
				}
				else if(tempSearchResult == searchResults.Count)
					tempSearchResult = 0;
			}
			//search in dic ends here
			//rest are KEY options
			else if( ( e.KeyCode == Keys.M ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ṃ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.A ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ā";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.I ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ī";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.U ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ū";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.D ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ḍ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.T ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ṭ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.L ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ḷ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.G ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ṅ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.N ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ṇ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			else if( ( e.KeyCode == Keys.J ) && ( e.Modifiers == Keys.Alt ) )
			{
				SearchBox.Text += "ñ";
				string cmd = "{RIGHT " + SearchBox.Text.Length.ToString() + "}";
				SendKeys.Send(cmd);
			}
			//key up and down - usability for navigation in List of Entry Box
			else if( e.KeyCode == Keys.Up )
			{
				if(ListOfEntries.SelectedIndex > 0)
					ListOfEntries.SelectedIndex--;
			}
			else if( e.KeyCode == Keys.Down )
			{
				if(ListOfEntries.SelectedIndex < ListOfEntries.Items.Count)
					ListOfEntries.SelectedIndex++;
			}
			else if( e.KeyCode == Keys.Escape )
			{
				this.Close();
			}
		}
		
		
		//load dictionary from folder
		//dict. has file type "*.dic"
		//dict. format: key_blank_value
		//thats it - 
		private void LoadDictionary(string dic)
		{
			string path =  @"Dictionary\" + dic.ToLower() + ".dict";
			dictionaryTable = new SortedList();
			if(File.Exists(path))
	    	{
	    	   	FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
	    	   	StreamReader rs = new StreamReader(fs);
	    	   	string line;
	    	   	
	    	   	while ((line = rs.ReadLine()) != null) 
                {
	    	   		String key = "";
	    	   		String entry = "";
	    	   		if(line != "")
	    	   		{
	    	   			key = line.Split(' ')[0];
						if(key.Length + 1 < line.Length)
	    	   				entry = line.Substring(key.Length + 1);
						if(key != null && key != "" && !dictionaryTable.ContainsKey(key))
	    	   				dictionaryTable.Add(key, entry);
	    	   		}
	    	   	}
	    	   	rs.Close();
	    	   	fs.Close();
	    	    this.Text = ("Pali Dictionary :: " + dictionaryTable.Count.ToString() + " Entries loaded ...");
	    	    int ind = LanguageSelectionBox.Items.IndexOf(dic.ToUpper());
	    	    LanguageSelectionBox.SelectedIndex = ind;
			}
		}
		
		private void SaveDictionary(string dic)
		{
			try
			{
				string path =  @"Dictionary\" + dic.ToLower() + ".dict";
				if(File.Exists(path))
		    	{
					File.Delete(path);
				}
				FileStream fs = new FileStream(path , FileMode.OpenOrCreate, FileAccess.Write); 
	            StreamWriter sw = new StreamWriter(fs); 
				sw.BaseStream.Seek(0, SeekOrigin.End); 
				IDictionaryEnumerator ide = dictionaryTable.GetEnumerator();
				while(ide.MoveNext())
				{
					string entry = ide.Key + " " + ide.Value;
					sw.WriteLine(entry);
				}	
				sw.Flush();
				sw.Close();
				fs.Close();	
			}
			catch(System.Exception ex)
			{
				meaningBox.Text = ex.Message.ToString();
			}

		}
		
		private void LoadListOfDictionaries()
		{
			string path =  @"\Dictionary";
			foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + path))
			{
					FileInfo fileOn = new FileInfo(file);
					if (fileOn.Extension.Equals(".dict"))
					{	
						LanguageSelectionBox.Items.Add(fileOn.Name.Substring(0, fileOn.Name.IndexOf(".")).ToUpper());
					}
			}
		}
		//moving among entries in dict
		void ListBox1SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			entryNumber = ListOfEntries.SelectedIndex;
			meaningBox.Text = (string)dictionaryTable.GetByIndex(entryNumber);
			this.Text = ListOfEntries.Items.Count.ToString() + " Entries loaded, " + " :: Entry No.: " + entryNumber.ToString();		
		}
		
		void SearchBoxEnter(object sender, System.EventArgs e)
		{
			if(SearchBox.Text == "look up word")
				SearchBox.Text = "";
			if(startUpWord != "")
				return;
			if(EditEntryBtn.Text == "Edit entry")
				SearchBox.Text = ""; 
			if(SearchBox.Text.IndexOf("F3") > 0)
				SearchBox.Text = "";
		}
		
		void SearchBoxLeave(object sender, System.EventArgs e)
		{
			lastSearchBoxEntry = SearchBox.Text;
			if(EditEntryBtn.Text == "Edit entry")
				SearchBox.Text = "look up word";
		}
		
		void EditEntryBtnClick(object sender, System.EventArgs e)
		{
			if(EditEntryBtn.Text == "Edit entry")
			{
				//make entry editable
				tmpSearchBoxText = SearchBox.Text;
				tmpMeaningBoxText = meaningBox.Text;
					
				ListOfEntries.Hide();
				LanguageSelectionBox.Hide();
				SearchBox.Text = (string)ListOfEntries.SelectedItem;
				SearchBox.ReadOnly = true;
				SearchBox.BackColor = Color.Red;
				meaningBox.ReadOnly = false;
				meaningBox.BackColor = Color.Bisque;
				label1.Text = "Entry:";
				label2.Text = "->";
				EditEntryBtn.Text = "Save entry";
				CancelBtn.Text = "Cancel update";
			}
			else
			{
				//update dictionary and return to dictionary view
				if(dictionaryTable != null)
				{
					dictionaryTable.SetByIndex(ListOfEntries.SelectedIndex, meaningBox.Text);
					dictionaryUpdated = true;
				}
				ListOfEntries.Show();
				LanguageSelectionBox.Show();
				SearchBox.Text = tmpSearchBoxText;
				SearchBox.BackColor = Color.White;
				SearchBox.ReadOnly = false;
				meaningBox.BackColor = Color.White;
				meaningBox.ReadOnly = true;
				label1.Text = "Look up:";
				label2.Text = "in";
				EditEntryBtn.Text = "Edit entry";
				CancelBtn.Text = "Cancel";
			}
		}
		//Load the newly selected dictionary into memory
		void LanguageSelectionBoxSelectedIndexChanged(object sender, System.EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			LoadDictionary(((String)LanguageSelectionBox.SelectedItem).ToLower());
			LoadListOfEntries();
			Cursor = Cursors.Default;
		}
		
		void PaliDictionaryKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Escape )
				this.Close();
		}
		
		void PaliDictionaryClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(dictionaryTable != null && dictionaryUpdated)
			{
				SaveDictionary(((String)LanguageSelectionBox.SelectedItem).ToLower());
			}
		}
		
		void LanguageSelectionBoxDropDown(object sender, System.EventArgs e)
		{
			if(dictionaryUpdated)
				SaveDictionary(((String)LanguageSelectionBox.SelectedItem).ToLower());
		}
		
		void ListOfEntriesKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Escape )
				this.Close();
		}
		
		void RemoveClick(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Do you really want to delete this dictionary entry?", "Attention", MessageBoxButtons.OKCancel);
			if(dr == DialogResult.OK && ListOfEntries.SelectedIndex >= 0)
			{
				if(dictionaryTable.Count <= 0)
				{
					MessageBox.Show("The dictionary is already empty");
				}
				else
				{
					int si = ListOfEntries.SelectedIndex;
					dictionaryTable.RemoveAt(si);
					SaveDictionary(((String)LanguageSelectionBox.SelectedItem).ToLower());
					LoadListOfEntries();
					si--;
					ListOfEntries.SelectedIndex = si;
				}
			}
		}
		
		void AddBtnClick(object sender, System.EventArgs e)
		{
			if(lastSearchBoxEntry != "" && dictionaryTable != null)
			{
				if(!dictionaryTable.ContainsKey(lastSearchBoxEntry) && lastSearchBoxEntry != "look up word")
				{
					dictionaryTable.Add(lastSearchBoxEntry, "");
					SaveDictionary(((String)LanguageSelectionBox.SelectedItem).ToLower());
					LoadListOfEntries();
					int si = dictionaryTable.IndexOfKey(lastSearchBoxEntry);
					ListOfEntries.SelectedIndex = si;
				}
				else
				{
					MessageBox.Show("This entry already exists. / Input invalid: " + lastSearchBoxEntry);
				}
			}
			else
			{
				MessageBox.Show("Please enter a valid word into the lookup field");
			}
		}
		
		void SearchBoxMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(SearchBox.Text.IndexOf("F3") > 0)
				SearchBox.Text = SearchBox.Text.Split(' ')[0];
		}
		
		void PaliDictionaryLoad(object sender, System.EventArgs e)
		{
			if(startUpWord != "")
			{
				SearchBox.Text = startUpWord;
				DoDictionaryLookup(startUpWord);
			}

		}
		
		void PaliDictionaryActivated(object sender, System.EventArgs e)
		{
	
		}
		
	}
}
