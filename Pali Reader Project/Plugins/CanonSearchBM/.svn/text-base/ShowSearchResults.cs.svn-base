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
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox BookListBox;
        private System.Windows.Forms.ListBox WordListBox;
        private System.Windows.Forms.Label label3;
        private SearchResult searchResult;
        public bool keepResult = false;

        public delegate void OpenBook(string a);
        public event OpenBook OpenBookEvent;

        public ShowSearchResults(SearchResult bsr)
        {
            this.searchResult = bsr;
            InitializeComponent();
            PrepareResultView();
        }

        #region Windows Forms Designer generated code
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.BookListBox = new System.Windows.Forms.ListBox();
            this.WordListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.KeepBtn = new System.Windows.Forms.Button();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.DetailsBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.DiscardBtn = new System.Windows.Forms.Button();
            this.SelectedWordLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 96);
            this.listBox1.TabIndex = 0;
            // 
            // listBox3
            // 
            this.listBox3.Location = new System.Drawing.Point(0, 0);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(120, 96);
            this.listBox3.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.Location = new System.Drawing.Point(0, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 96);
            this.listBox2.TabIndex = 0;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 96);
            this.checkedListBox1.TabIndex = 0;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(120, 96);
            this.checkedListBox2.TabIndex = 0;
            // 
            // BookListBox
            // 
            this.BookListBox.Location = new System.Drawing.Point(264, 20);
            this.BookListBox.Name = "BookListBox";
            this.BookListBox.Size = new System.Drawing.Size(272, 173);
            this.BookListBox.TabIndex = 0;
            this.BookListBox.DoubleClick += new System.EventHandler(this.BookListBoxDoubleClick);
            this.BookListBox.SelectedIndexChanged += new System.EventHandler(this.BookListBox_SelectedIndexChanged);
            // 
            // WordListBox
            // 
            this.WordListBox.Location = new System.Drawing.Point(8, 20);
            this.WordListBox.Name = "WordListBox";
            this.WordListBox.Size = new System.Drawing.Size(258, 173);
            this.WordListBox.TabIndex = 1;
            this.WordListBox.SelectedIndexChanged += new System.EventHandler(this.WordListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(353, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Matching words - found in books";
            // 
            // KeepBtn
            // 
            this.KeepBtn.Location = new System.Drawing.Point(7, 196);
            this.KeepBtn.Name = "KeepBtn";
            this.KeepBtn.Size = new System.Drawing.Size(60, 26);
            this.KeepBtn.TabIndex = 5;
            this.KeepBtn.Text = "Keep";
            this.KeepBtn.UseVisualStyleBackColor = true;
            this.KeepBtn.Click += new System.EventHandler(this.KeepBtn_Click);
            // 
            // PrintBtn
            // 
            this.PrintBtn.Location = new System.Drawing.Point(142, 196);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(60, 26);
            this.PrintBtn.TabIndex = 6;
            this.PrintBtn.Text = "Print";
            this.PrintBtn.UseVisualStyleBackColor = true;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // DetailsBtn
            // 
            this.DetailsBtn.Location = new System.Drawing.Point(447, 196);
            this.DetailsBtn.Name = "DetailsBtn";
            this.DetailsBtn.Size = new System.Drawing.Size(89, 26);
            this.DetailsBtn.TabIndex = 7;
            this.DetailsBtn.Text = "Show details";
            this.DetailsBtn.UseVisualStyleBackColor = true;
            this.DetailsBtn.Click += new System.EventHandler(this.DetailsBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(206, 196);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(60, 26);
            this.SaveBtn.TabIndex = 8;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // OpenBtn
            // 
            this.OpenBtn.Location = new System.Drawing.Point(369, 196);
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(75, 26);
            this.OpenBtn.TabIndex = 9;
            this.OpenBtn.Text = "Open book";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // DiscardBtn
            // 
            this.DiscardBtn.Location = new System.Drawing.Point(73, 196);
            this.DiscardBtn.Name = "DiscardBtn";
            this.DiscardBtn.Size = new System.Drawing.Size(60, 26);
            this.DiscardBtn.TabIndex = 10;
            this.DiscardBtn.Text = "Discard";
            this.DiscardBtn.UseVisualStyleBackColor = true;
            this.DiscardBtn.Click += new System.EventHandler(this.DiscardBtn_Click);
            // 
            // SelectedWordLabel
            // 
            this.SelectedWordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedWordLabel.Location = new System.Drawing.Point(241, 0);
            this.SelectedWordLabel.Name = "SelectedWordLabel";
            this.SelectedWordLabel.Size = new System.Drawing.Size(295, 17);
            this.SelectedWordLabel.TabIndex = 11;
            this.SelectedWordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShowSearchResults
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(543, 226);
            this.ControlBox = false;
            this.Controls.Add(this.SelectedWordLabel);
            this.Controls.Add(this.DiscardBtn);
            this.Controls.Add(this.OpenBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.DetailsBtn);
            this.Controls.Add(this.PrintBtn);
            this.Controls.Add(this.KeepBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WordListBox);
            this.Controls.Add(this.BookListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowSearchResults";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search results ...";
            this.Load += new System.EventHandler(this.ShowSearchResultsLoad);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button KeepBtn;
        #endregion
        void ShowSearchResultsLoad(object sender, System.EventArgs e)
        {
        }

        private void PrepareResultView()
        {
            try
            {
                FillWordListBox();
                FillBookListBox();
                this.Text += "No. of matching words: " + searchResult.SumOfMatchingWords() +
                    " (Occuring " + searchResult.SumOfOccurences() + " times) in "
                    + searchResult.SumOfBooks() + " books";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
            return;
        }

        private void FillWordListBox()
        {
            BookListBox.BackColor = Color.White;
            WordListBox.BackColor = Color.White;
            WordListBox.Items.Add("All words");
            IEnumerator ie = searchResult.GetWordList().GetEnumerator();
            while (ie.MoveNext())
            {
                WordListBox.Items.Add((string)ie.Current);
            }
        }

        private void FillWordListBox(string currentBook)
        {
            BookListBox.BackColor = Color.White;
            WordListBox.BackColor = Color.Yellow;
            WordListBox.Items.Add("All words");
            SortedList slt = new SortedList();
            int pos = -1;
            if ((pos = currentBook.IndexOf("(")) > -1)
                currentBook = currentBook.Substring(0, pos - 1);
            currentBook = currentBook.Trim();
            currentBook = NamingConverter.GetFileNameFromBook(currentBook);
            string[] l = searchResult.GetMatchingWords(currentBook);
            if (l != null)
            {
                IEnumerator ie = l.GetEnumerator();
                while (ie.MoveNext())
                {
                    string word = (string)ie.Current;
                    if (slt.Contains(word))
                        continue;
                    else
                    {
                        slt.Add(word, 0);
                        WordListBox.Items.Add(word);
                    }
                }
            }
        }

        private void FillBookListBox()
        {
            BookListBox.BackColor = Color.White;
            WordListBox.BackColor = Color.White;
            BookListBox.Items.Add("All books");
            IEnumerator iee = searchResult.GetBookList().GetEnumerator();
            while (iee.MoveNext())
            {
                int n = 1;
                string book = (string)iee.Current;
                string[] t = searchResult.GetMatchingWords(book);
                if (t != null)
                    n = t.Length;
                BookListBox.Items.Add(NamingConverter.GetBookNameFromFile(book) + " (" + n.ToString() + ")");
            }

        }

        private void FillBookListBox(string currentWord)
        {
            BookListBox.BackColor = Color.Yellow;
            WordListBox.BackColor = Color.White;
            BookListBox.Items.Add("All books");
            SortedList slt = new SortedList();
            int n = -1;
            IEnumerator ie = searchResult.GetMatchingBooks(currentWord).GetEnumerator();
            while (ie.MoveNext())
            {
                string book = (string)ie.Current;
                if (slt.Contains(book))
                    continue;
                else
                {
                    slt.Add(book, 0);
                    n = 1; //shift one upward because of zero based index
                    n += searchResult.CountMatchesOfWordInBook(book, currentWord);
                    BookListBox.Items.Add(NamingConverter.GetBookNameFromFile(book) + " (" + n.ToString() + ")");
                }
            }
        }

        protected void OnOpenBook(string a)
        {
            OpenBookEvent(a);
        }


        void BookListBoxDoubleClick(object sender, System.EventArgs e)
        {
            try
            {
            	if (BookListBox.SelectedIndex == -1)
            	{
            		MessageBox.Show("Select a book in the right column");
            		return;
            	}
            	Cursor.Current = Cursors.WaitCursor;
                string bookName = (string)BookListBox.Items[BookListBox.SelectedIndex];
                //string bookName = name[0].ToUpper() + "." + name[1].ToUpper();
                bookName = PaliReaderUtils.NamingConverter.GetFileNameFromBook(bookName);
                PaliReaderUtils.AalekhDecoder.UnzipFromZipLibrary(bookName);
                OnOpenBook(bookName);
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("2Error: " + ex.Message.ToString());
            }
        }

        void Button1Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private Button PrintBtn;
        private Button DetailsBtn;
        private Button SaveBtn;

        private void WordListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookListBox.Items.Clear();
            string currentItem = (string)WordListBox.Items[WordListBox.SelectedIndex];
            if (currentItem == "All words")
                FillBookListBox();
            else
            {
                FillBookListBox(currentItem);
                SelectedWordLabel.Text = currentItem;
            }
        }

        private void BookListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WordListBox.Items.Clear();
            string currentItem = (string)BookListBox.Items[BookListBox.SelectedIndex];
            if (currentItem == "All books")
                FillWordListBox();
            else
            {
                FillWordListBox(currentItem);
            }
        }

        private Button OpenBtn;
        private Button DiscardBtn;

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented - will print list");
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented - will save list to file");
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
        	keepResult = true;
            BookListBoxDoubleClick(null, null);
        }

        private void DetailsBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented - will show surrounding text");
        }

        private void DiscardBtn_Click(object sender, EventArgs e)
        {
            keepResult = false;
            Close();
        }

        private void KeepBtn_Click(object sender, EventArgs e)
        {
            keepResult = true;
            Close();
        }
        
        private void ColorSelectedItem()
        {
        	string s = this.SelectedWordLabel.Text;
        	int sPos = s.IndexOf(searchResult.GetSearchTerm());
        	if(sPos > -1)
        	{
        		string partA = s.Substring(0, sPos);
        	}
        }

        private Label SelectedWordLabel;
    }
}
