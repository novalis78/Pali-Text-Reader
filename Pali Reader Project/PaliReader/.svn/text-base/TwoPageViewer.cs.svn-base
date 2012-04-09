
///
///
/// Copyright (C) 2005  Lennart Lopin <novalis78@gmx.net> 
/// All Rights Reserved.
///
/// This program is free software; you can redistribute it and/or
/// modify it under the terms of the GNU General Public License as
/// published by the Free Software Foundation; either version 2 of the
/// License, or (at your option) any later  version.
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
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace PaliReader
{
	/// <summary>
	/// Description of TwoPageViewer.
	/// </summary>
	public class TwoPageViewer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.ComponentModel.Container components = null;
		

	  #region constant fields
		private const string standardTitle = "TwoPageViewer";
		// default text in titlebar
		private uint margin = 80;
		private string word = "Buddho";
		private string sentence = "";
			
		// horizontal and vertical margin in client area
      #endregion

      #region Member fields
		private ArrayList documentLines = new ArrayList();   // the 'document'
		private uint lineHeight;        // height in pixels of one line
		private Size documentSize;      // how big a client area is needed to 
		// display document
		private uint nLines;            // number of lines in document
		private Font mainFont;          // font used to display all lines
		private Font emptyDocumentFont; // font used to display empty message
		private Brush mainBrush = Brushes.Black; 
		// brush used to display document text
		private Brush emptyDocumentBrush = Brushes.Red;
		// brush used to display empty document message
		private Point mouseDoubleClickPosition;   
		// location mouse is pointing to when double-clicked
		private OpenFileDialog fileOpenDialog = new OpenFileDialog();
		// standard open file dialog
		private bool documentHasData = false; 
		// set to true if document has some data in it
		private int BookHeight;
		private int BookWidth;
		private StringCollection bookWords;
		
		private int wordsOnPageCounter = 0;
		private int wordPositionInDocumentCounter = 0;
		private int setBackgroundPicture = 0;
		private bool isOnProperPage = false;
      #endregion

		public TwoPageViewer(string a)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//this.StartPosition = FormStartPosition.CenterParent;
			this.BackgroundImage = Image.FromFile("Icons/book768_3.jpg");
			this.WindowState = FormWindowState.Maximized;
			//BookHeight = 570;
			//BookWidth  = 800;
			//this.Height = BookHeight;
			//this.Width  = BookWidth;
			
			this.BackColor = Color.White;
			CreateFonts();
			//this.LoadFile(@"Work\" + "ttt.txt");
			this.LoadFileWordWise(@"Work\" + "ttt.txt");
			
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
		private void CreateFonts()
		{
			mainFont = new Font("Arial Unicode MS", 10, FontStyle.Regular);
			lineHeight = (uint)mainFont.Height;
			emptyDocumentFont = new Font("Verdana", 12, FontStyle.Bold);
		}


		class TextLineInformation
		{
			public string Text;
			public uint Width;
		}
		
		protected void OpenFileDialog_FileOk(object Sender, CancelEventArgs e)
		{
			this.LoadFile(fileOpenDialog.FileName);
		}

		protected void menuFileOpen_Click(object sender, EventArgs e)
		{
			fileOpenDialog.ShowDialog();
		}      
      
		protected void menuFileExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	
		private void LoadFile(string FileName)
		{
			StreamReader sr = new StreamReader(FileName);
			string nextLine;
			documentLines.Clear();
			nLines = 0;
			TextLineInformation nextLineInfo;
			while ( (nextLine = sr.ReadLine()) != null)
			{
				nextLineInfo = new TextLineInformation();
				nextLineInfo.Text = nextLine;
				documentLines.Add(nextLineInfo);
				++nLines;
			}
			sr.Close();
			documentHasData = (nLines>0) ? true : false;

			//CalculateLineWidths();
			CalculateDocumentSize();

			this.Text = standardTitle + " - " + FileName;
			this.Invalidate();
		}
		
		private void LoadFileWordWise(string FileName)
		{
			StreamReader sr = new StreamReader(FileName);
			string nextLine;
			documentLines.Clear();
			nLines = 100;
			bookWords = new StringCollection();
			while ( (nextLine = sr.ReadLine()) != null)
			{
				string[] sentence = nextLine.Split(' ');
				for(int i = 0; i < sentence.Length; i++)
				{
					if(sentence[i] != "" && sentence[i] != " ")
						bookWords.Add(sentence[i].Trim());
				}
			}
			sr.Close();
			
/*			bookWords = new StringCollection();
			bookWords.Add("Namo");
			bookWords.Add("tassa");

			bookWords.Add("sutam");
			bookWords.Add("Namo");
			bookWords.Add("tassa");
			bookWords.Add("Bhagavato");
			bookWords.Add("Arahato");
			bookWords.Add("Sammasambuddhassa");
			bookWords.Add("Anguttaranikayo");
			bookWords.Add("Evam");
			bookWords.Add("me");
			bookWords.Add("sutam");*/
			documentHasData = (bookWords.Count>0) ? true : false;
			documentSize.Height = 400;
			documentSize.Width  = 400;
			this.AutoScrollMinSize = documentSize;
	
			this.Text = standardTitle + " - " + FileName;
			this.Invalidate();
		}


		private void CalculateLineWidths()
		{
			Graphics dc = this.CreateGraphics();
			foreach (TextLineInformation nextLine in documentLines)
			{
				nextLine.Width = (uint)dc.MeasureString(nextLine.Text, 
					mainFont).Width;
			}
		}

		private void CalculateDocumentSize()
		{
			if (!documentHasData)
			{
				documentSize = new Size(100, 200);
			}
			else
			{
				documentSize.Height = (int)(nLines*lineHeight) + 2*(int)margin;
				if(documentSize.Height > BookHeight)
					documentSize.Height = BookHeight - (int)margin;
				
				uint maxLineLength = 0;
				foreach (TextLineInformation nextWord in documentLines)
				{
					uint tempLineLength = nextWord.Width + 2*margin;
					if (tempLineLength > maxLineLength)
						maxLineLength = tempLineLength;
				}
				documentSize.Width = (int)maxLineLength;
				//if(documentSize.Width > BookWidth)
					documentSize.Width = 200;
			}
			this.AutoScrollMinSize = documentSize;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(isOnProperPage)
			{
				Graphics dc = e.Graphics;
				int scrollPositionX = this.AutoScrollPosition.X;
				int scrollPositionY = this.AutoScrollPosition.Y;
				dc.TranslateTransform(scrollPositionX, scrollPositionY);
				
				
				
				if (!documentHasData)
				{
					dc.DrawString("<Error loading document>", emptyDocumentFont, 
						emptyDocumentBrush, new Point(20,20));
					base.OnPaint(e);
					return;
				}
				if(bookWords.Count > 0)
					printLeftPage(dc);
				printRightPage(dc);
			}
		}
		private Point LineIndexToWorldCoordinates(int index)
		{
			Point TopLeftCorner = new Point(
				(int)margin, (int)(lineHeight*index + 30));
			return TopLeftCorner;
		}
		
		//x in a line and y as a line, represented by counter index
		private Point LineIndexToWorldCoordinates(int s, int index)
		{
			Point TopLeftCorner = new Point(
		 (int)(margin + s), (int)(lineHeight*index + 30));
			return TopLeftCorner;
		}

		private int WorldYCoordinateToLineIndex(int y)
		{
			if (y < margin)
				return -1;
			return (int)((y-margin)/lineHeight);
		}

		private int WorldCoordinatesToLineIndex(Point position)
		{
			if (!documentHasData)
				return -1;
			if (position.Y < margin || position.X < margin)
				return -1;
			int index = (int)(position.Y-margin)/(int)this.lineHeight;
			// check position isn't below document
			if (index >= documentLines.Count)
				return -1;
			// now check that horizontal position is within this line
			if (position.X > margin + "test".Length)
				return -1;

			// all is OK. We can return answer
			return index;
		}

		private Point LineIndexToPageCoordinates(int index)
		{
			return LineIndexToWorldCoordinates(index) + 
				new Size(AutoScrollPosition);
		}

		private int PageCoordinatesToLineIndex(Point position)
		{
			return WorldCoordinatesToLineIndex(position - new 
				Size(AutoScrollPosition));
		}


		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.mouseDoubleClickPosition = new Point(e.X, e.Y);
		}

		
		protected override void OnDoubleClick(EventArgs e)
		{
			int i = PageCoordinatesToLineIndex(this.mouseDoubleClickPosition);
			if (i >= 0)
			{
				TextLineInformation lineToBeChanged = 
					(TextLineInformation)documentLines[i];
				lineToBeChanged.Text = lineToBeChanged.Text.ToUpper();
				Graphics dc = this.CreateGraphics();
				uint newWidth =(uint)dc.MeasureString(lineToBeChanged.Text, 
					mainFont).Width;
				if (newWidth > lineToBeChanged.Width)
					lineToBeChanged.Width = newWidth;
				if (newWidth+2*margin > this.documentSize.Width)
				{
					this.documentSize.Width = (int)newWidth;
					this.AutoScrollMinSize = this.documentSize;
				}
				Rectangle changedRectangle = new Rectangle(
					LineIndexToPageCoordinates(i), 
					new Size((int)newWidth, 
					(int)this.lineHeight));
				this.Invalidate(changedRectangle);
			}
			base.OnDoubleClick(e);
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TwoPageViewer));
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Close ...";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Next ->";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "<- Previous";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.menuItem1,
						this.menuItem2,
						this.menuItem3});
			// 
			// TwoPageViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(432, 347);
			this.ContextMenu = this.contextMenu1;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "TwoPageViewer";
			this.Text = "TwoPageViewer";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TwoPageViewerKeyDown);
			this.Click += new System.EventHandler(this.TwoPageViewerClick);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TwoPageViewerKeyPress);
		}
		#endregion

		void TwoPageViewerKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			
		}
		
		void TwoPageViewerKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
				if(e.KeyCode == Keys.Left)
				{
					if(setBackgroundPicture == 1)
					{
						setIntroPage();
						setBackgroundPicture = 0;
					}
					else if(setBackgroundPicture == 2)
					{
						setOrdinaryPage();
						isOnProperPage = false;
						setBackgroundPicture = 1;
					}
					else if(setBackgroundPicture == 3)
					{
						this.Invalidate();
					}
				}
				
				if(e.KeyCode == Keys.Right)
				{	
					if(setBackgroundPicture == 0)
					{
						setIntroPage();
						setBackgroundPicture = 1;
					}
					else if(setBackgroundPicture == 1)
					{
						setOrdinaryPage();
						isOnProperPage = true;
						setBackgroundPicture = 2;
					}
					else if(setBackgroundPicture == 2 || setBackgroundPicture == 3)
					{
						//MessageBox.Show("turn to next page");
						this.Invalidate();
						setBackgroundPicture = 3;
					}
				}
			
			
		}
		
		private void printLeftPage(Graphics dc)
		{
			int maxLines = 24;
			margin = 90;
			
			for (int i=0; i <= maxLines ; i++)
			{
				int sentenceWidth = 0;			
				while(sentenceWidth < (margin + 180) && (bookWords.Count > wordPositionInDocumentCounter))
				{
					//MessageBox.Show("word:-" + bookWords[wordPositionInDocumentCounter] + "-");
					sentence += bookWords[wordPositionInDocumentCounter] + " ";//word + " ";
					int x = sentenceWidth; //
					sentenceWidth = (int)MeasureDisplayStringWidth(dc, sentence, mainFont);
					if(sentenceWidth < (margin + 180))
					{
						dc.DrawString(bookWords[wordPositionInDocumentCounter] + " ",
					 	 mainFont, mainBrush,this.LineIndexToWorldCoordinates(x, i));
						wordPositionInDocumentCounter++;
					}
					else
						break;
				}
				//new line, starting at margin once again
				sentence = "";
			}
			wordsOnPageCounter += wordPositionInDocumentCounter;
		}
		
		private void printRightPage(Graphics dc)
		{
			int maxLines = 24;
			//left margin on right page)
			margin = 50 + 180 + 150;
			//right margin on right page
			uint rmargin = margin + 100;
			int maxLineLength = 250;
			
			for (int i=0; i <= maxLines ; i++)
			{
				int sentenceWidth = 0;			
				while(sentenceWidth < maxLineLength)
				{
					sentence += word + " ";
					sentenceWidth = (int)MeasureDisplayStringWidth(dc, sentence, mainFont);
					dc.DrawString(word + " ",mainFont, mainBrush,this.LineIndexToWorldCoordinates(sentenceWidth,i));
				}
				//new line, starting at margin once again
				sentence = "";
			}
		}
		
		static public int MeasureDisplayStringWidth(Graphics graphics, string text, Font font)
		{
		    System.Drawing.StringFormat format  = new System.Drawing.StringFormat ();
		    System.Drawing.RectangleF   rect    = new System.Drawing.RectangleF(0, 0,
		                                                                  1000, 1000);
		    System.Drawing.CharacterRange[] ranges  = 
		                                       { new System.Drawing.CharacterRange(0, 
		                                                               text.Length) };
		    System.Drawing.Region[]         regions = new System.Drawing.Region[1];
		
		    format.SetMeasurableCharacterRanges (ranges);
		
		    regions = graphics.MeasureCharacterRanges (text, font, rect, format);
		    rect    = regions[0].GetBounds (graphics);
		
		    return (int)(rect.Right + 1.0f);
		}
		
		private void setIntroPage()
		{
			BookHeight = 932;
			BookWidth  = 768;
			this.Height = BookHeight;
			this.Width  = BookWidth;
			this.BackgroundImage = Image.FromFile("Icons/book768_2.jpg");
			this.WindowState = FormWindowState.Maximized;
			return;
		}
		private void setOrdinaryPage()
		{
			BookHeight = 804;
			BookWidth  = 768;
			this.Height = BookHeight;
			this.Width  = BookWidth;
			this.BackgroundImage = Image.FromFile("Icons/book768.jpg");
			this.WindowState = FormWindowState.Maximized;
			return;
		}
		
		void TwoPageViewerClick(object sender, System.EventArgs e)
		{
			MessageBox.Show("Use your left and right arrow key to navigate - see context menu for more options");
		}
		
	}
}
