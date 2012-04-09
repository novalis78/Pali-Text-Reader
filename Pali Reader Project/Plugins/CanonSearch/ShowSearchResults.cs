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
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using PaliReaderUtils;

namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of ShowSearchResults.
	/// </summary>
	public class ShowSearchResults : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.RichTextBox resultView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView resultListView;
		private string searchResult = "";
		private ArrayList resultSafe;
		
		public delegate void OpenBook(string a);
		public event OpenBook OpenBookEvent;
		
		public ShowSearchResults(string a)
		{
			searchResult = a;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.resultView.Text = searchResult;
		}
		
		public ShowSearchResults(ArrayList a)
		{
			InitializeComponent();
			PrepareResultView(a);
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.resultListView = new System.Windows.Forms.ListView();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.resultView = new System.Windows.Forms.RichTextBox();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// resultListView
			// 
			this.resultListView.BackColor = System.Drawing.SystemColors.Info;
			this.resultListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
						this.columnHeader1,
						this.columnHeader2,
						this.columnHeader3});
			this.resultListView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.resultListView.FullRowSelect = true;
			this.resultListView.GridLines = true;
			this.resultListView.Location = new System.Drawing.Point(0, 0);
			this.resultListView.MultiSelect = false;
			this.resultListView.Name = "resultListView";
			this.resultListView.Size = new System.Drawing.Size(480, 240);
			this.resultListView.TabIndex = 3;
			this.resultListView.View = System.Windows.Forms.View.Details;
			this.resultListView.DoubleClick += new System.EventHandler(this.ResultListViewDoubleClick);
			this.resultListView.SelectedIndexChanged += new System.EventHandler(this.ResultListViewSelectedIndexChanged);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 127;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 99;
			// 
			// resultView
			// 
			this.resultView.BackColor = System.Drawing.Color.WhiteSmoke;
			this.resultView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.resultView.ForeColor = System.Drawing.SystemColors.WindowText;
			this.resultView.Location = new System.Drawing.Point(0, 239);
			this.resultView.Name = "resultView";
			this.resultView.ReadOnly = true;
			this.resultView.Size = new System.Drawing.Size(474, 80);
			this.resultView.TabIndex = 2;
			this.resultView.Text = "Text segment preview";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Width = 218;
			// 
			// ShowSearchResults
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(474, 319);
			this.Controls.Add(this.resultListView);
			this.Controls.Add(this.resultView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ShowSearchResults";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Search results ...";
			this.Load += new System.EventHandler(this.ShowSearchResultsLoad);
			this.ResumeLayout(false);
		}
		#endregion
		void ShowSearchResultsLoad(object sender, System.EventArgs e)
		{
		}
		
		void PrepareResultView(ArrayList a)
		{
			this.resultSafe = a;
			columnHeader1.Text = "Hit";
			columnHeader2.Text = "Score";
			columnHeader3.Text = "Book";
			
			for(int i = 0; i < a.Count; i++)
			{
				//resultListView.Items.Add(NamingConverter.GetBookNameFromFile(((ResultSet)a[i]).BookName));
				ListViewItem lvi = new ListViewItem();
				lvi.Text = (i+1).ToString() + ".";
				lvi.SubItems.Add(((ResultSet)a[i]).Score.ToString());
				lvi.SubItems.Add(NamingConverter.GetBookNameFromFile(((ResultSet)a[i]).BookName));
				resultListView.Items.Add(lvi);
			}
			return;
		}
		
		//trigger loading of selected book
		//from search result item
		void ResultListViewDoubleClick(object sender, System.EventArgs e)
		{
			int idx = 0;
			foreach (ListViewItem lvi in resultListView.SelectedItems)
				idx = lvi.Index;
			string[] name = ((ResultSet)resultSafe[idx]).BookName.Split('.');
			string bookName = name[0].ToUpper() + "." + name[1].ToUpper();
			PaliReaderUtils.AalekhDecoder.UnzipFromZipLibrary(bookName);
			OnOpenBook(bookName);			
		}
		
		protected void OnOpenBook(string a)
        {
            OpenBookEvent(a);
        }
		
		void ResultListViewSelectedIndexChanged(object sender, System.EventArgs e)
		{
			int idx = 0;
			foreach (ListViewItem lvi in resultListView.SelectedItems)
				idx = lvi.Index;
			string text = ((ResultSet)resultSafe[idx]).textFragment;
			text = System.Text.RegularExpressions.Regex.Replace(text, "<B>", "{\\b");
			text = System.Text.RegularExpressions.Regex.Replace(text, "</B>", "}");
			
			//resultView.Rtf = "{\rtf " + text + "}"; 
			resultView.Text = text;
		}
		
	}
}
