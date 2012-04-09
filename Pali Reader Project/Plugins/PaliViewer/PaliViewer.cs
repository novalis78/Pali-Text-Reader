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
///Code makes use of the SloppyCode wrapper for the webbrowser control
///A reference for the Find Dialog implementation specifica can be found at http://support.microsoft.com/?scid=kb%3Ben-us%3B329014&x=15&y=2


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Common;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Sloppycode.Controls;
using System.IO;

[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
public struct OLECMDTEXT
{
	public uint cmdtextf;
	public uint cwActual;
	public uint cwBuf;
	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=100)]public char rgwz;
}

[StructLayout(LayoutKind.Sequential)]
public struct OLECMD
{
	public uint cmdID;
	public uint cmdf;
}

// Interop definition for IOleCommandTarget.
[ComImport,
Guid("b722bccb-4e68-101b-a2bc-00aa00404770"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleCommandTarget
{
	//IMPORTANT: The order of the methods is critical here. You
	//perform early binding in most cases, so the order of the methods
	//here MUST match the order of their vtable layout (which is determined
	//by their layout in IDL). The interop calls key off the vtable ordering,
	//not the symbolic names. Therefore, if you switched these method declarations
	//and tried to call the Exec method on an IOleCommandTarget interface from your
	//application, it would translate into a call to the QueryStatus method instead.
	void QueryStatus(ref Guid pguidCmdGroup, UInt32 cCmds,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)]
		OLECMD[] prgCmds, ref OLECMDTEXT CmdText);
	void Exec(ref Guid pguidCmdGroup,
		uint nCmdId, uint nCmdExecOpt, ref object pvaIn, ref object pvaOut);
}

public class Class1
{
	public Class1()
	{

	}
}
namespace PaliReaderPlugin
{

	public class PaliViewer : System.Windows.Forms.UserControl
	{
		private Sloppycode.Controls.WebBrowserEx webBrowserEx1;
		private System.Windows.Forms.MenuItem menuFind;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuTranslate;
		private System.Windows.Forms.MenuItem menuItemLookup;
		private System.Windows.Forms.MenuItem menuItemReferences;
		private System.Windows.Forms.MenuItem menuItemCommentary;
		private System.Windows.Forms.MenuItem menuItemSubcomm;
		private System.Windows.Forms.MenuItem menuItemHighlight;
		private System.Windows.Forms.MenuItem menuItemAnalyzer;
		private System.Windows.Forms.MenuItem menuItemWordTranslation;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemPrint;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuItem menuItemCopy;
		private System.Windows.Forms.MenuItem menuItemCConvert;
		private ArrayList m_HighlightText = new ArrayList();
		private Common.ITree<string> bookContents = null;
		private Common.INode<string> book = null;
		private mshtml.IHTMLDocument2 _doc = null;
		private mshtml.IHTMLElementCollection ps = null;
		private MouseOverHandler _mouseover = null;
		private MouseMoveHandler _mousemove = null;
		private KeyDownHandler   _keydown   = null;
		private KeyUpHandler     _keyup		= null;
		private string currentSelText = null;
		private int paragraphCounter = 0;
		private string loadedBook = "";
		private int paragraphTarget = -1;
		private string searchItemTarget = "";
		private ArrayList bookMarks = null;
		private bool selectionTriggered = false;
		private int bmc = 0;
		public  bool jumpToKeyword = false;
	
		public delegate void LookUpHandler(string message);
 		public delegate string SilentLookUpHandler(string message);
 		public delegate void AtthakathaHandler(int paragraph, string book);
 		public delegate void CompoundAnalyzeHandler(string selText);
 		public delegate void DelegateDisplayDocument(string text);
 		public delegate void EditionDetectionHandler(string currParagraph);
 		public delegate void ContentsParsedHandler();
 
		// Define an Event based on the above Delegate
		public event LookUpHandler LookUp;
		public event SilentLookUpHandler SilentLookUp;
		public event AtthakathaHandler TriggerAtthakatha;
		public event CompoundAnalyzeHandler CompoundAnalyzing;
		public event EditionDetectionHandler DetectEdition;
		public event ContentsParsedHandler ContentsParsed;

		private Guid cmdGuid = new Guid("ED016940-BD5B-11CF-BA4E-00C04FD70816");
		private enum MiscCommandTarget
		{
			Find = 1,
			ViewSource,
			Options
		}
    
		public PaliViewer()
		{
			try
			{
				InitializeComponent();
				this.webBrowserEx1.ContextMenu = this.contextMenu;
				this.webBrowserEx1.ScrollBarsVisible = true;
				this.webBrowserEx1.XPThemed = true;
				this.webBrowserEx1.Border3D = true;
				this.webBrowserEx1.HtmlDocumentDoubleClick += new WebBrowserExEventHandler(webBrowserEx1_HtmlDocumentDoubleClick);
				this.webBrowserEx1.NavigateComplete +=new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(webBrowserEx1_NavigateComplete);
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message.ToString());
			}
		}

		public PaliViewer(string a, string b, int p, bool c)
		{
			InitializeComponent();
			this.webBrowserEx1.ContextMenu = this.contextMenu;
			this.webBrowserEx1.ScrollBarsVisible = true;
			this.webBrowserEx1.XPThemed = true;
			this.webBrowserEx1.Border3D = true;
			DelegateDisplayDocument ddd = new DelegateDisplayDocument(DisplayDocument);
			ddd.BeginInvoke(a, null, null);
			//DisplayDocument(a);
			this.webBrowserEx1.HtmlDocumentDoubleClick += new WebBrowserExEventHandler(webBrowserEx1_HtmlDocumentDoubleClick);
			this.webBrowserEx1.NavigateComplete +=new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(webBrowserEx1_NavigateComplete);
			jumpToKeyword = c;
			paragraphTarget = p;
			searchItemTarget = b;
		}
		public PaliViewer(string a):this(a, null, -1, false){}
		public PaliViewer(string a, int b):this(a, null, b, false){}
		public PaliViewer(string a, string b):this(a, b, -1, false){}
		public PaliViewer(string a, string b, bool c):this(a, b, -1, c){}
		

		private void DisplayDocument(string url)
		{
			if(url == "") return;
			loadedBook = url;
			//string url = fileName;//Directory.GetCurrentDirectory() + @"\Work\" + fileName;
			try 
			{
				Cursor.Current = Cursors.WaitCursor;
				// Get the URL from the control, and send the WebBrowser to fetch and display it	
				webBrowserEx1.Navigate(url);				
			} 
			finally 
			{
				Cursor.Current = Cursors.Default;
			}			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.webBrowserEx1 = new Sloppycode.Controls.WebBrowserEx();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuFind = new System.Windows.Forms.MenuItem();
			this.menuTranslate = new System.Windows.Forms.MenuItem();
			this.menuItemLookup = new System.Windows.Forms.MenuItem();
			this.menuItemReferences = new System.Windows.Forms.MenuItem();
			this.menuItemCommentary = new System.Windows.Forms.MenuItem();
			this.menuItemSubcomm = new System.Windows.Forms.MenuItem();
			this.menuItemHighlight = new System.Windows.Forms.MenuItem();
			this.menuItemAnalyzer = new System.Windows.Forms.MenuItem();
			this.menuItemWordTranslation = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemPrint = new System.Windows.Forms.MenuItem();
			this.menuItemCopy = new System.Windows.Forms.MenuItem();
			this.menuItemCConvert = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// webBrowserEx1
			// 
			this.webBrowserEx1.AddressBar = true;
			this.webBrowserEx1.Border3D = false;
			this.webBrowserEx1.DisableBackSpace = false;
			this.webBrowserEx1.DisableCtrlF = false;
			this.webBrowserEx1.DisableCtrlN = false;
			this.webBrowserEx1.DisableCtrlP = false;
			this.webBrowserEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowserEx1.EnableHtmlDocumentEventHandling = false;
			this.webBrowserEx1.FlatScrollBar = false;
			this.webBrowserEx1.FullScreen = false;
			this.webBrowserEx1.Location = new System.Drawing.Point(0, 0);
			this.webBrowserEx1.Name = "webBrowserEx1";
			this.webBrowserEx1.Offline = false;
			this.webBrowserEx1.OpenInNewWindow = false;
			this.webBrowserEx1.Options = Sloppycode.Controls.BrowserOptions.Images;
			this.webBrowserEx1.RegisterAsBrowser = false;
			this.webBrowserEx1.RegisterAsDropTarget = true;
			this.webBrowserEx1.ScrollBarsVisible = false;
			this.webBrowserEx1.ShowWebsiteInDesigner = false;
			this.webBrowserEx1.Silent = false;
			this.webBrowserEx1.Size = new System.Drawing.Size(728, 438);
			this.webBrowserEx1.TabIndex = 6;
			this.webBrowserEx1.TheaterMode = false;
			this.webBrowserEx1.Url = null;
			this.webBrowserEx1.XPThemed = true;
			this.webBrowserEx1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.webBrowserEx1_KeyDown);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.menuFind,
									this.menuTranslate,
									this.menuItemLookup,
									this.menuItemReferences,
									this.menuItemCommentary,
									this.menuItemSubcomm,
									this.menuItemHighlight,
									this.menuItemAnalyzer,
									this.menuItemWordTranslation,
									this.menuItem1,
									this.menuItemPrint,
									this.menuItemCopy,
									this.menuItemCConvert});
			// 
			// menuFind
			// 
			this.menuFind.Index = 0;
			this.menuFind.Text = "find";
			this.menuFind.Click += new System.EventHandler(this.menuFind_Click);
			// 
			// menuTranslate
			// 
			this.menuTranslate.Index = 1;
			this.menuTranslate.Text = "translate";
			this.menuTranslate.Click += new System.EventHandler(this.menuTranslate_Click);
			// 
			// menuItemLookup
			// 
			this.menuItemLookup.Index = 2;
			this.menuItemLookup.Text = "look up word";
			this.menuItemLookup.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItemReferences
			// 
			this.menuItemReferences.Index = 3;
			this.menuItemReferences.Text = "find references";
			this.menuItemReferences.Click += new System.EventHandler(this.menuItemReferences_Click);
			// 
			// menuItemCommentary
			// 
			this.menuItemCommentary.Index = 4;
			this.menuItemCommentary.Text = "see commentary";
			this.menuItemCommentary.Click += new System.EventHandler(this.menuItemCommentary_Click);
			// 
			// menuItemSubcomm
			// 
			this.menuItemSubcomm.Index = 5;
			this.menuItemSubcomm.Text = "see subcommentary";
			this.menuItemSubcomm.Click += new System.EventHandler(this.menuItemSubcomm_Click);
			// 
			// menuItemHighlight
			// 
			this.menuItemHighlight.Index = 6;
			this.menuItemHighlight.Text = "highlight this passage";
			this.menuItemHighlight.Click += new System.EventHandler(this.menuItemHighlight_Click);
			// 
			// menuItemAnalyzer
			// 
			this.menuItemAnalyzer.Index = 7;
			this.menuItemAnalyzer.Text = "analyze word compound";
			this.menuItemAnalyzer.Click += new System.EventHandler(this.menuItemAnalyzer_Click);
			// 
			// menuItemWordTranslation
			// 
			this.menuItemWordTranslation.Index = 8;
			this.menuItemWordTranslation.Text = "disable word translation";
			this.menuItemWordTranslation.Click += new System.EventHandler(this.menuItemWordTranslation_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 9;
			this.menuItem1.Text = "-";
			// 
			// menuItemPrint
			// 
			this.menuItemPrint.Index = 10;
			this.menuItemPrint.Text = "print selected text";
			this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
			// 
			// menuItemCopy
			// 
			this.menuItemCopy.Index = 11;
			this.menuItemCopy.Text = "copy selected text";
			this.menuItemCopy.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItemCConvert
			// 
			this.menuItemCConvert.Index = 12;
			this.menuItemCConvert.Text = "copy + convert to Velthuis";
			this.menuItemCConvert.Click += new System.EventHandler(this.menuItemCConvert_Click);
			// 
			// PaliViewer
			// 
			this.Controls.Add(this.webBrowserEx1);
			this.Name = "PaliViewer";
			this.Size = new System.Drawing.Size(728, 438);
			this.ResumeLayout(false);
		}
		#endregion

		private void buttonSaveAs_Click(object sender, System.EventArgs e)
		{
			this.webBrowserEx1.SaveAs("sample.html");
		}

		private void buttonPrintPreview_Click(object sender, System.EventArgs e)
		{
			this.webBrowserEx1.PrintPreview();
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			this.webBrowserEx1.Find();
		}

		private void buttonPageProperties_Click(object sender, System.EventArgs e)
		{
			this.webBrowserEx1.ShowPageProperties();
		}

		public void refreshPage()
		{
			webBrowserEx1.Refresh(RefreshType.Completely);
			this.webBrowserEx1.Refresh(RefreshType.IfExpired);
			this.webBrowserEx1.Refresh(RefreshType.Normal);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			mshtml.IHTMLDocument2 a =  this.webBrowserEx1.CurrentDocument;
			String text = "";
			bool success = a.execCommand("Copy", false, text);
			OnLookUp((string)Clipboard.GetDataObject().GetData(DataFormats.StringFormat));
		}

		protected void OnLookUp(string message)
		{
			if (LookUp != null)
				LookUp(message);
		}
		protected string OnSilentLookUp(string message)
		{
			if (LookUp != null)
				return SilentLookUp(message);
			else
				return "";
		}
		protected void OnTriggerAtthakatha(int paragraph, string book)
		{
			if (TriggerAtthakatha != null)
				TriggerAtthakatha(paragraph, book);
		}
		protected void OnCompoundAnalyzing(string a)
		{
			if(CompoundAnalyzing != null)
				CompoundAnalyzing(a);
		}
		protected void OnDetectEdition(string currParagraph)
		{
			if (DetectEdition != null)
				DetectEdition(currParagraph);
		}
		protected void OnContentsParsed()
		{
			if (ContentsParsed != null)
				ContentsParsed();
		}

		private void webBrowserEx1_HtmlDocumentDoubleClick(object sender, WebBrowserExEventArgs e)
		{
		}

		private void menuTranslate_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("test");
		}
		private void menuFind_Click(object sender, System.EventArgs e)
		{
			Find();
		}

		private mshtml.HTMLDocument GetDocument()
		{
			try
			{
				mshtml.HTMLDocument htm = (mshtml.HTMLDocument)this.webBrowserEx1.CurrentDocument;
				return htm;
			}
			catch
			{
				throw (new Exception("Cannot retrieve the document from the WebBrowser control"));
			}
		}

		public void Find()
		{
			CopySelection();
			string sel = (string)Clipboard.GetDataObject().GetData(DataFormats.Text);
			Highlight(sel, "Yellow", "");
			MessageBox.Show(sel);
			IOleCommandTarget cmdt;
			Object o = new object();
			Object i = new object();
			i = "test";
			try
			{
				cmdt = (IOleCommandTarget)GetDocument();
				cmdt.Exec(ref cmdGuid, (uint)MiscCommandTarget.Find,
					(uint)SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT,ref i, ref i);
				//System.Threading.Thread.Sleep(2000);
				PasteFromClipboard();	
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}

		private void CopySelection()
		{
			mshtml.IHTMLDocument2 a =  this.webBrowserEx1.CurrentDocument;
			String text = "";
			bool success = a.execCommand("Copy", false, text);
		}

		private void PasteFromClipboard()
		{
			SendKeys.SendWait("^V");
			//SendKeys.Send("%{F4}");
		}

		private void menuItemReferences_Click(object sender, System.EventArgs e)
		{
			if(currentSelText != null)
				Highlight(currentSelText, "Orange", "");
		}

		private void menuItemCommentary_Click(object sender, System.EventArgs e)
		{
			OnTriggerAtthakatha(paragraphCounter, loadedBook);
		}

		private void menuItemSubcomm_Click(object sender, System.EventArgs e)
		{
			OnTriggerAtthakatha(paragraphCounter, loadedBook);
		}

		private void menuItemHighlight_Click(object sender, System.EventArgs e)
		{
			HighlightSelectedText(false, "Yellow");
		}
	
		/// <summary>
		/// Calculate current paragraph number for corresponding commentary paragraph
		/// Return result of other pali editions book and page numbers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMouseOver(object sender, WebEventHandlerBase.WebEventArgs e)
		{
			try
			{
	 			MouseOverHandler h = (MouseOverHandler)sender;
	      	    string a = h.HtmlElement.innerHTML;
	      	    if(a != null)
	      	    {
	      	    	a = a.ToLower();
	      	    	string x = LocateEdition(a);
	      	    	a = a.Substring(0, a.IndexOf("."));
	      	    	paragraphCounter = int.Parse(a);
	      	    	x = x + paragraphCounter.ToString();
	      	    	OnDetectEdition(x);
	      	    }
			}catch(Exception ex){}
 		}
		//needed for google like dictionary lookup
		private void OnMouseMove(object sender, WebEventHandlerBase.WebEventArgs e)
 		{
			MouseMoveHandler h = (MouseMoveHandler)sender;
			if(selectionTriggered)
			{
				h.HtmlElement.title = "";
				currentSelText = null;
				return;
			}
 			mshtml.IHTMLTxtRange TextRange = (mshtml.IHTMLTxtRange)((mshtml.IHTMLSelectionObject)this.webBrowserEx1.CurrentDocument.selection).createRange();
 			TextRange.moveToPoint(e.ClientX, e.ClientY);
 			TextRange.expand("word");
      		TextRange.select(); 
      		currentSelText = TextRange.text;
      		string b = OnSilentLookUp(currentSelText);
      		h.HtmlElement.title = currentSelText + ((b == "") ? "?" : ": means ") + b;
 		}
		private void webBrowserEx1_NavigateComplete(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
		{
			_doc = (mshtml.IHTMLDocument2)this.webBrowserEx1.CurrentDocument;
			ps = (mshtml.IHTMLElementCollection)((mshtml.IHTMLElementCollection) _doc.body.all).tags("p");
			_mouseover = new MouseOverHandler(_doc);
			_mousemove = new MouseMoveHandler(_doc);
			_keydown   = new KeyDownHandler(_doc);
			_keyup     = new KeyUpHandler(_doc);
 			_doc.onmouseover = new DispatchWrapper(_mouseover);
 			_doc.onmousemove = new DispatchWrapper(_mousemove);
 			_doc.onkeydown = new DispatchWrapper(_keydown);
 			_doc.onkeyup   = new DispatchWrapper(_keyup);
 			_mouseover.OnWebEvent += new WebEventHandlerBase.WebEventHandler(OnMouseOver);
 			_mousemove.OnWebEvent += new WebEventHandlerBase.WebEventHandler(OnMouseMove);
 			_keydown.OnWebEvent += new WebEventHandlerBase.WebEventHandler(OnKeyDown);
 			_keyup.OnWebEvent += new WebEventHandlerBase.WebEventHandler(OnKeyUp);
 			if(paragraphTarget > 0)
 				JumpToParagraph(paragraphTarget);
 			//either we move directly to a destination paragraph
 			//or we want to highlight all findings, bookmark them, and move to the first one
 			if(searchItemTarget != null && searchItemTarget.Length > 0)
 			{
 				if (jumpToKeyword)
 					JumpToParagraph(searchItemTarget);
 				else
 					Highlight(searchItemTarget, "Yellow", "");
 				
 			}
 			//TODO
 			ExamineBookContents();
		}

		private void menuItemAnalyzer_Click(object sender, System.EventArgs e)
		{
			if(currentSelText == null)
				return;
			OnCompoundAnalyzing(currentSelText);
		}
		
		private void menuItemWordTranslation_Click(object sender, System.EventArgs e)
		{
			if(selectionTriggered) 
			{
				selectionTriggered = false;
				this.menuItemWordTranslation.Text = "disable word translation";
			}
			else
			{
				selectionTriggered = true;
				this.menuItemWordTranslation.Text = "enable word translation";
			}
		}

		private void menuItemPrint_Click(object sender, System.EventArgs e)
		{
			this.webBrowserEx1.Print(true);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			CopySelection();
		}

		private void webBrowserEx1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if((e.KeyCode == Keys.Alt) && (e.KeyCode == Keys.F))
			{
				Find();
			}
			if((e.KeyCode == Keys.Control) && (e.KeyCode == Keys.C))
			{
				CopySelection();
			}
			if(e.KeyCode == Keys.Print)
			{
				this.webBrowserEx1.Print(true);
			}

		}

		private void menuItemCConvert_Click(object sender, System.EventArgs e)
		{
			CopySelection();
			IDataObject ido = Clipboard.GetDataObject();
			if(ido.GetDataPresent(DataFormats.Text))
			{
				string uniText = (String)ido.GetData(DataFormats.UnicodeText);
				string newText = ConvertToVelthuis(uniText);
				Clipboard.SetDataObject(newText);
			}
		}

		private string ConvertToVelthuis(string a)
		{
			a = a.Replace("ā", "aa");
			a = a.Replace("ī", "ii");
			a = a.Replace("ū", "uu");
			a = a.Replace("ṃ", ".m");
			a = a.Replace("ñ", "~n");
			a = a.Replace("ṇ", ".n");
			a = a.Replace("ṭ", ".t");
			a = a.Replace("ḍ", ".d");
			a = a.Replace("ḷ", ".l");
			a = a.Replace("ṅ", "n.");
			return a;
		}

		
		private void HighlightSelectedText(bool HighlightAll, string ExactMatchColor)
		{
			mshtml.IHTMLTxtRange TextRange = (mshtml.IHTMLTxtRange)((mshtml.IHTMLSelectionObject)this.webBrowserEx1.CurrentDocument.selection).createRange();
			ExactMatchColor = "Yellow";
			while(TextRange.text.EndsWith(" ") == true)
			{
				TextRange.moveEnd("character", -1);
			}
			while(TextRange.text.StartsWith(" ") == true)
			{
				TextRange.moveStart("character", 1);
			}
			if(TextRange.text.Trim().Length != 0)
			{
				if(HighlightAll == false)
				{
					//add to highlight list...so we can highlight as we navigate
					// AddToHighlightList(TextRange.text)
					//highlight it...
					TextRange.execCommand("BackColor", false, ExactMatchColor);
				}
				else
				{
					//Highlight(TextRange.text, ExactMatchColor);
				}
			}
		}

		private void Highlight(string TextToHighlight, string ExactMatchColor, string WordMatchColors)
		{
			WordMatchColors = "lime, aqua, violet, aquamarine, paleturquoise, gold, lavender, wheat, lightgrey, tomato";
			try
			{
				bookMarks = new ArrayList();
				
				if(TextToHighlight.Trim() != "")
				{
	
					string Bookmark = "";
					mshtml.IHTMLTxtRange TextRange = (mshtml.IHTMLTxtRange)((mshtml.IHTMLBodyElement)this.webBrowserEx1.CurrentDocument.body).createTextRange();
	
					// add to highlight list...so we can highlight as we navigate
					if( ExactMatchColor !=  "" ) 
						AddToHighlightList(TextToHighlight);
	
					// if( we have multiple words ) lets handle one at a time...
					if( TextToHighlight.Trim().IndexOf(" ") >  -1 )
					{
						int Counter = 0;
						string[] Colors = WordMatchColors.Replace(" ", "").Split(',');
						//string Word = "";
						string[] Words = TextToHighlight.Trim().Split();
						foreach(string Word in Words)
						{
							if( Word.Trim() !=  "" )
							{
								while( TextRange.findText(Word.Trim(), 1, 2))
								{
									// save where we are if( needed...
									if( Bookmark.Trim() == "" )
									{
										Bookmark = TextRange.getBookmark();
										bookMarks.Add(TextRange.getBookmark());
									}
									// highlight the text
									TextRange.execCommand("BackColor", false, Colors[Counter]);
									// move cursor to end of this selection
									TextRange.collapse(false);
								}
								if( Bookmark.Trim() !=  "" )
								{
									// we have a bookmark lets go back to it...
									TextRange.moveToBookmark(Bookmark);
									// make sure we can see it...
									TextRange.scrollIntoView(false);
								}
								// increment the counter to get next color
								Counter++;
								// if( we are past the colorlist length start over...
								if( Counter >  Colors.Length - 1 ) 
									Counter = 0;
								// reset our text range...
								TextRange = (mshtml.IHTMLTxtRange)((mshtml.IHTMLBodyElement)this.webBrowserEx1.CurrentDocument.body).createTextRange();
							}
						}
					}
					
					// now lets find any exact matches and highlight correctly
					while( TextRange.findText(TextToHighlight.Trim(), 1, 0))
					{
						// save where we are if( needed...
						if( Bookmark.Trim() == "" )
						{
							Bookmark = TextRange.getBookmark();
						}
						bookMarks.Add(TextRange.getBookmark());
						// highlight the text
						TextRange.execCommand("BackColor", false, ExactMatchColor);
						// move cursor to end of this selection
						TextRange.collapse(false);
					}
					if( Bookmark.Trim() !=  "" )
                    // we have a bookmark lets go back to it...
					TextRange.moveToBookmark(Bookmark);
                    // make sure we can see it...
                    TextRange.scrollIntoView(true);
                    
				}
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message.ToString());
			}
		}

		private void AddToHighlightList(string TextToHighlight)
		{
			if(m_HighlightText.Contains(TextToHighlight) == false)
				m_HighlightText.Add(TextToHighlight);
		}
		
		private void RemoveHighlight()
		{
			if(m_HighlightText.Count > 0)
			{
				IEnumerator ale = m_HighlightText.GetEnumerator();
				while(ale.MoveNext())
				{
					Highlight(ale.Current.ToString(), "", "");
				}
				m_HighlightText.Clear();
			}
		}
		
		private void JumpToParagraph(int par)
		{
			try
			{
				//IEnumerator ie = ps.GetEnumerator();
				string l = "";
				for(int i = 0; i < ps.length; i++)
				{
					mshtml.IHTMLElement ce = (mshtml.IHTMLElement)ps.item((object)i,0);
				  	if(ce != null)
				  	{
						l = ce.innerHTML;
						if(l != null)
						{
				  			if((l.IndexOf(par.ToString() + ".") > -1)
							   || (l.IndexOf(par.ToString() + ".") > -1))
				  			{
								//MessageBox.Show(l);
								ce.scrollIntoView(true);
								return;
				  			}
						}
				  	}
				}
				return;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error: " + ex.StackTrace.ToString() + ex.Message.ToString());
			}
		}
		
		public void JumpToParagraph(string keyword)
		{
			try
			{
				string l = "";
				for(int i = 0; i < ps.length; i++)
				{
					mshtml.IHTMLElement ce = (mshtml.IHTMLElement)ps.item((object)i,0);
				  	if(ce != null)
				  	{
						l = ce.innerHTML;
						if(l != null)
						{
							if(l.Contains(keyword))
				  			{
								ce.scrollIntoView(true);
								return;
				  			}
						}
				  	}
				}
				return;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error: " + ex.StackTrace.ToString() + ex.Message.ToString());
			}
		}
		
		//move through list of bookmarks which derive either from
		//a search query or
		//a reference lookup
		void OnKeyDown(object sender, WebEventHandlerBase.WebEventArgs  e)
		{
			//F3
			if(e.KeyCode == 114)
			{
				//scroll to previous highlighted object
				if(bookMarks != null && bookMarks.Count > 0)
				{
					mshtml.IHTMLTxtRange TextRange = (mshtml.IHTMLTxtRange)((mshtml.IHTMLBodyElement)this.webBrowserEx1.CurrentDocument.body).createTextRange();
					TextRange.moveToBookmark((string)bookMarks[bmc]);
					//TextRange.execCommand("BackColor", false, "Red");
					//TextRange.collapse(false);
					
					TextRange.scrollIntoView(false);
					if(bmc > 0)
						bmc--;
				}
			}
			//F4
			if(e.KeyCode == 115)
			{
				//scroll to next highlighted object
				if(bookMarks != null && bookMarks.Count > 0)
				{
					mshtml.IHTMLTxtRange TextRange = (mshtml.IHTMLTxtRange)((mshtml.IHTMLBodyElement)this.webBrowserEx1.CurrentDocument.body).createTextRange();
					TextRange.moveToBookmark((string)bookMarks[bmc]);
					//TextRange.execCommand("BackColor", false, "Red");
					//TextRange.collapse(false);
					TextRange.scrollIntoView(false);
					if(bmc < (bookMarks.Count - 1))
						bmc++;
				}
					
			}
			if(e.KeyCode == 19)
			{
				this.webBrowserEx1.PrintPreview();
				this.webBrowserEx1.Print(true);
			}
			
		}
		void OnKeyUp(object sender, WebEventHandlerBase.WebEventArgs  e)
		{
			if((e.KeyCode == 16))
			{
				
			}
		}
		
		/// <summary>
		/// Locate pali edition informations on current paragraph and return
		/// as "edition#book#page"
		/// </summary>
		/// <param name="currParagraph"></param>
		/// <returns></returns>
		private string LocateEdition(string currParagraph)
		{
			//<a name="B1.005"></a>
			int posA = -1;
			int posE = -1;
			if ((posA = currParagraph.IndexOf("<a name=")) > -1)
			{
				posE = currParagraph.IndexOf("</a>", posA);
				//result = "edition#book#page";
				if (posE > posA && posA > -1 && posE > -1)
				{
					string edition = currParagraph.Substring(posA + 9, 1);
					string result = (edition == "b" ? "Burmese" : (edition == "t" ? "Thai" : (edition == "v" ? "VRI" : (edition == "p" ? "PTS" : "")))) + "#" + currParagraph.Substring(posA + 11, 1) + "#" + currParagraph.Substring(posA + 13, 4) + "#";
					return result;
				}
				else return null;
			}
			else
				return null;
		}
		
		private void ExamineBookContents()
		{
			try
			{
				bookContents = NodeTree<String>.NewTree();
				INode<String> section = null;
				INode<String> sutta = null;
				string l = "";
				string p = "";
				string prev = "";
				for(int i = 0; i < ps.length; i++)
				{
					mshtml.IHTMLElement ce = (mshtml.IHTMLElement)ps.item((object)i,0);
				  	if (ce != null)
				  	{
				  		l = ce.outerHTML;
						if (l != null)
						{
							if (l.Contains("=c10>"))
				  			{
								p = ce.innerHTML;
								if (bookContents != null)
								{
									book = bookContents.AddChild(p);
								}
				  			}
							else if ((l.Contains("=c11>")) || (l.Contains("=c15>")))
				  			{
								p = ce.innerHTML;
								if (book != null)
								{
									section = book.AddChild(p);
									prev = "section";
								}
				  			}
							else if ((l.Contains("=c14>")))
				  			{
								p = ce.innerHTML;
								if (book != null)
								{
									if (Regex.IsMatch(p, @"\d{1}\. "))
									{
										sutta = section.AddChild(p);
										prev = "sutta";
									}
								}
				  			}							
							else if (l.Contains("=c03>"))
							{
								p = ce.innerHTML;
								if (Regex.IsMatch(p, @"\d{1}\. "))
								{
									string par = p.Substring(0, p.IndexOf(".") + 2);
									if (sutta != null && prev == "sutta")
										sutta.AddChild(par);
									else if (section != null && prev == "section")
										section.AddChild(par);
								}
							}
						}
				  	}
				}
				OnContentsParsed();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error: " + ex.StackTrace.ToString() + ex.Message.ToString());
			}
		}
		
		public ITree<String> GetBookContents()
		{
			return bookContents;
		}
	}
}




/*
 * 
 * private void Underline(mshtml.IHTMLElement elem)
		{
			mshtml.IHTMLDocument4 doc4;
			mshtml.IDisplayServices ids;
			mshtml.IMarkupServices ims;
			mshtml.IMarkupContainer imc;
			mshtml.IHTMLRenderStyle irs;
			mshtml.IMarkupPointer impStart;
			mshtml.IMarkupPointer impEnd;
			doc4 = (mshtml.IHTMLDocument4)this.webBrowserEx1.CurrentDocument;
			ids = (mshtml.IDisplayServices)doc4;
			ims = (mshtml.IMarkupServices)doc4;
			imc = (mshtml.IMarkupContainer)doc4;

		//---------------------------------------------
		// Create the start markup pointer and position
		// it after the beginning of the element
		//---------------------------------------------
			ims.CreateMarkupPointer(out impStart);

			impStart.MoveAdjacentToElement(elem,
			mshtml._ELEMENT_ADJACENCY.ELEM_ADJ_AfterBegin);
			

			//Create a display pointer from the markup pointer
			mshtml.IDisplayPointer idpStart;
		    ids.CreateDisplayPointer(out idpStart);
			idpStart.MoveToMarkupPointer(impStart, null);

		//---------------------------------------------
		// Create the end markup pointer and position
		// it before the end of the element
		//---------------------------------------------
			ims.CreateMarkupPointer(out impEnd);

			impEnd.MoveAdjacentToElement(elem,
			mshtml._ELEMENT_ADJACENCY.ELEM_ADJ_BeforeEnd);

		    //Create a display pointer from the markup pointer
			mshtml.IDisplayPointer idpEnd;
			ids.CreateDisplayPointer(out idpEnd);
			idpEnd.MoveToMarkupPointer(impEnd, null);

			// Create a render style
			irs = doc4.createRenderStyle(null);

			// Must set this, despite it supposedly being the default setting!
			irs.defaultTextSelection = "false";

			irs.textBackgroundColor = "White";;
			irs.textColor = "Black";
			irs.textDecoration = "underline";
			irs.textDecorationColor = "red";
			irs.textUnderlineStyle = "wave";

			// Add the segment
			mshtml.IHighlightRenderingServices ihrs;
			mshtml.IHighlightSegment ihs;
			ihrs = (mshtml.IHighlightRenderingServices)doc4;
			ihrs.AddSegment(idpStart, idpEnd, irs, out ihs);
		}
*/
