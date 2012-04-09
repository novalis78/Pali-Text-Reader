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
using Splash;
using System.IO;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using PaliReaderPlugin;
using PaliReaderUtils;
using PluginInterface;




namespace PaliReader
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public class Workbench : System.Windows.Forms.Form, IPluginHost
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.MenuItem MenuInfoHelp;
		private System.Windows.Forms.StatusBar StatusBar;
		private System.Windows.Forms.MenuItem MenuViewBook;
		private System.Windows.Forms.SaveFileDialog SaveFileDialog;
		private System.Windows.Forms.MenuItem RPanelTPView;
		private System.Windows.Forms.ImageList ToolBarImageList;
		private System.Windows.Forms.OpenFileDialog OpenFileDialog;
		private System.Windows.Forms.MenuItem MenuHelp;
		private System.Windows.Forms.ContextMenu ReaderPanelContext;
		private System.Windows.Forms.MenuItem RPanelContextClose;
		private System.Windows.Forms.ToolBar WorkBenchToolBar;
		private System.Windows.Forms.Panel BottomPanel;
		private System.Windows.Forms.MenuItem MenuToolsPlugins;
		private System.Windows.Forms.MenuItem MenuFileSaveAs;
		private System.Windows.Forms.MenuItem MenuHelpComment;
		private System.Windows.Forms.MenuItem MenuHelpMemorize;
		private System.Windows.Forms.MenuItem MenuInfoUpdate;
		private System.Windows.Forms.MenuItem MenuInfo;
		private System.Windows.Forms.MenuItem MenuFile;
		private System.Windows.Forms.MenuItem MenuHelpHelp;
		private System.Windows.Forms.MenuItem MenuFileOpen;
		private System.Windows.Forms.MenuItem MenuFileOnOffline;
		private System.Windows.Forms.Panel ReaderPanel;
		private System.Windows.Forms.MenuItem RPanelContextCloseAll;
		private System.Windows.Forms.Timer statusTimer;
		private System.Windows.Forms.MenuItem MenuFileClose;
		private System.Windows.Forms.MenuItem MenuFileSave;
		private System.Windows.Forms.MenuItem MenuView;
		private System.Windows.Forms.MenuItem MenuHelpAbout;
		private System.Windows.Forms.MenuItem MenuHelpUpdate;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.MainMenu MenuBar;
		private System.Windows.Forms.MenuItem MenuInfoAbout;
		private System.Windows.Forms.MenuItem MenuFullScreen;
		private System.Windows.Forms.MenuItem MenuTools;
		private System.Windows.Forms.TabControl ReaderTabPanel;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Collections.Hashtable availablePlugins;
		public const string APP_VERSION = "0.1.1.50";
		public const int PLUGIN_AUTHOR		= 0;
		public const int PLUGIN_VERSION		= 1;
		public const int PLUGIN_DESCRIPTION = 2;
		public const int PLUGIN_DISPLAYNAME = 3;
		private string lastBookName = "";
		private bool onlineMode = false;
		private bool fullscreenMode = false;
		private bool infoPaneActive = false;
		private BCAccessor ba = null;
		private int selectedTabNo = -1;
		
		//SplashScreen variables
		private bool m_bLayoutCalled = false;
		private DateTime m_dt;
		//
		
		public Workbench()
		{
			InitializeComponent();			
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			SplashScreen.ShowSplashScreen(); 
			Application.EnableVisualStyles();
			Application.DoEvents();
			SplashScreen.SetStatus("Starting Pali Text Reader v. " + APP_VERSION);
			Thread.Sleep(100);
			SplashScreen.SetStatus("developed by Lopin, Holloway, Snow, Genaud, Bure et al.");
			Thread.Sleep(100);
			try
			{
				Application.Run(new Workbench());
				SplashScreen.SetReferencePoint();
			}
			catch(System.Exception ex)
			{
				//SplashScreen.CloseForm();
				ReportBugs rb = new ReportBugs(ex.ToString());
				rb.ShowDialog();
				Application.Exit();
				//MessageBox.Show(ex.Message.ToString());
			}
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workbench));
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.ReaderPanelContext = new System.Windows.Forms.ContextMenu();
			this.RPanelContextClose = new System.Windows.Forms.MenuItem();
			this.RPanelContextCloseAll = new System.Windows.Forms.MenuItem();
			this.RPanelTPView = new System.Windows.Forms.MenuItem();
			this.MenuTools = new System.Windows.Forms.MenuItem();
			this.MenuToolsPlugins = new System.Windows.Forms.MenuItem();
			this.MenuFullScreen = new System.Windows.Forms.MenuItem();
			this.MenuInfoAbout = new System.Windows.Forms.MenuItem();
			this.MenuBar = new System.Windows.Forms.MainMenu(this.components);
			this.MenuFile = new System.Windows.Forms.MenuItem();
			this.MenuFileOpen = new System.Windows.Forms.MenuItem();
			this.MenuFileSave = new System.Windows.Forms.MenuItem();
			this.MenuFileSaveAs = new System.Windows.Forms.MenuItem();
			this.MenuFileOnOffline = new System.Windows.Forms.MenuItem();
			this.MenuFileClose = new System.Windows.Forms.MenuItem();
			this.MenuView = new System.Windows.Forms.MenuItem();
			this.MenuViewBook = new System.Windows.Forms.MenuItem();
			this.MenuHelp = new System.Windows.Forms.MenuItem();
			this.MenuHelpHelp = new System.Windows.Forms.MenuItem();
			this.MenuHelpUpdate = new System.Windows.Forms.MenuItem();
			this.MenuHelpComment = new System.Windows.Forms.MenuItem();
			this.MenuHelpMemorize = new System.Windows.Forms.MenuItem();
			this.MenuHelpAbout = new System.Windows.Forms.MenuItem();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.statusTimer = new System.Windows.Forms.Timer(this.components);
			this.ReaderPanel = new System.Windows.Forms.Panel();
			this.splitContainerWorkbench = new System.Windows.Forms.SplitContainer();
			this.ReaderTabPanel = new System.Windows.Forms.TabControl();
			this.InfoPane = new System.Windows.Forms.TabControl();
			this.MenuInfo = new System.Windows.Forms.MenuItem();
			this.MenuInfoUpdate = new System.Windows.Forms.MenuItem();
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.StatusBar = new System.Windows.Forms.StatusBar();
			this.statusBarInfo = new System.Windows.Forms.StatusBarPanel();
			this.statusBarTicker = new System.Windows.Forms.StatusBarPanel();
			this.statusBarEdition = new System.Windows.Forms.StatusBarPanel();
			this.statusBarMode = new System.Windows.Forms.StatusBarPanel();
			this.WorkBenchToolBar = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarImageList = new System.Windows.Forms.ImageList(this.components);
			this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.MenuInfoHelp = new System.Windows.Forms.MenuItem();
			this.ReaderPanel.SuspendLayout();
			this.splitContainerWorkbench.Panel1.SuspendLayout();
			this.splitContainerWorkbench.Panel2.SuspendLayout();
			this.splitContainerWorkbench.SuspendLayout();
			this.BottomPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarTicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarEdition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarMode)).BeginInit();
			this.SuspendLayout();
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Name = "toolBarButton3";
			this.toolBarButton3.Text = "3";
			this.toolBarButton3.Visible = false;
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Name = "toolBarButton2";
			this.toolBarButton2.Text = "2";
			this.toolBarButton2.Visible = false;
			// 
			// ReaderPanelContext
			// 
			this.ReaderPanelContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.RPanelContextClose,
									this.RPanelContextCloseAll,
									this.RPanelTPView});
			// 
			// RPanelContextClose
			// 
			this.RPanelContextClose.Index = 0;
			this.RPanelContextClose.Text = "Close";
			this.RPanelContextClose.Click += new System.EventHandler(this.RPanelContextCloseClick);
			// 
			// RPanelContextCloseAll
			// 
			this.RPanelContextCloseAll.Index = 1;
			this.RPanelContextCloseAll.Text = "Close All";
			this.RPanelContextCloseAll.Click += new System.EventHandler(this.RPanelContextCloseAllClick);
			// 
			// RPanelTPView
			// 
			this.RPanelTPView.Index = 2;
			this.RPanelTPView.Text = "Book view mode";
			this.RPanelTPView.Click += new System.EventHandler(this.RPanelTPViewClick);
			// 
			// MenuTools
			// 
			this.MenuTools.Index = 1;
			this.MenuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.MenuToolsPlugins});
			this.MenuTools.Text = "Tools";
			// 
			// MenuToolsPlugins
			// 
			this.MenuToolsPlugins.Index = 0;
			this.MenuToolsPlugins.Text = "Plugins";
			this.MenuToolsPlugins.Click += new System.EventHandler(this.MenuToolsPluginsClick);
			// 
			// MenuFullScreen
			// 
			this.MenuFullScreen.Index = 0;
			this.MenuFullScreen.Text = "Fullscreen";
			this.MenuFullScreen.Click += new System.EventHandler(this.MenuFullScreenClick);
			// 
			// MenuInfoAbout
			// 
			this.MenuInfoAbout.Index = -1;
			this.MenuInfoAbout.Text = "";
			// 
			// MenuBar
			// 
			this.MenuBar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.MenuFile,
									this.MenuTools,
									this.MenuView,
									this.MenuHelp});
			// 
			// MenuFile
			// 
			this.MenuFile.Index = 0;
			this.MenuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.MenuFileOpen,
									this.MenuFileSave,
									this.MenuFileSaveAs,
									this.MenuFileOnOffline,
									this.MenuFileClose});
			this.MenuFile.Text = "File";
			// 
			// MenuFileOpen
			// 
			this.MenuFileOpen.Index = 0;
			this.MenuFileOpen.Text = "Open";
			this.MenuFileOpen.Click += new System.EventHandler(this.MenuFileOpenClick);
			// 
			// MenuFileSave
			// 
			this.MenuFileSave.Index = 1;
			this.MenuFileSave.Text = "Save";
			// 
			// MenuFileSaveAs
			// 
			this.MenuFileSaveAs.Index = 2;
			this.MenuFileSaveAs.Text = "Save As";
			this.MenuFileSaveAs.Click += new System.EventHandler(this.MenuFileSaveAsClick);
			// 
			// MenuFileOnOffline
			// 
			this.MenuFileOnOffline.Index = 3;
			this.MenuFileOnOffline.Text = "Work Online";
			this.MenuFileOnOffline.Click += new System.EventHandler(this.MenuFileOnOfflineClick);
			// 
			// MenuFileClose
			// 
			this.MenuFileClose.Index = 4;
			this.MenuFileClose.Text = "Close";
			this.MenuFileClose.Click += new System.EventHandler(this.MenuFileCloseClick);
			// 
			// MenuView
			// 
			this.MenuView.Index = 2;
			this.MenuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.MenuFullScreen,
									this.MenuViewBook});
			this.MenuView.Text = "View";
			// 
			// MenuViewBook
			// 
			this.MenuViewBook.Index = 1;
			this.MenuViewBook.Text = "Display info pane";
			this.MenuViewBook.Click += new System.EventHandler(this.MenuViewBookClick);
			// 
			// MenuHelp
			// 
			this.MenuHelp.Index = 3;
			this.MenuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.MenuHelpHelp,
									this.MenuHelpUpdate,
									this.MenuHelpComment,
									this.MenuHelpMemorize,
									this.MenuHelpAbout});
			this.MenuHelp.Text = "?";
			// 
			// MenuHelpHelp
			// 
			this.MenuHelpHelp.Index = 0;
			this.MenuHelpHelp.Text = "Help";
			this.MenuHelpHelp.Click += new System.EventHandler(this.MenuHelpHelpClick);
			// 
			// MenuHelpUpdate
			// 
			this.MenuHelpUpdate.Index = 1;
			this.MenuHelpUpdate.Text = "Update";
			this.MenuHelpUpdate.Click += new System.EventHandler(this.MenuHelpUpdateClick);
			// 
			// MenuHelpComment
			// 
			this.MenuHelpComment.Index = 2;
			this.MenuHelpComment.Text = "Comment";
			this.MenuHelpComment.Click += new System.EventHandler(this.MenuHelpCommentClick);
			// 
			// MenuHelpMemorize
			// 
			this.MenuHelpMemorize.Index = 3;
			this.MenuHelpMemorize.Text = "Memorizing pali";
			this.MenuHelpMemorize.Click += new System.EventHandler(this.MenuHelpMemorizeClick);
			// 
			// MenuHelpAbout
			// 
			this.MenuHelpAbout.Index = 4;
			this.MenuHelpAbout.Text = "About";
			this.MenuHelpAbout.Click += new System.EventHandler(this.MenuInfoAboutClick);
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.Name = "toolBarButton6";
			this.toolBarButton6.Text = "6";
			this.toolBarButton6.Visible = false;
			// 
			// statusTimer
			// 
			this.statusTimer.Interval = 15000;
			this.statusTimer.Tick += new System.EventHandler(this.StatusTimerTick);
			// 
			// ReaderPanel
			// 
			this.ReaderPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ReaderPanel.Controls.Add(this.splitContainerWorkbench);
			this.ReaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReaderPanel.Location = new System.Drawing.Point(0, 7);
			this.ReaderPanel.Name = "ReaderPanel";
			this.ReaderPanel.Size = new System.Drawing.Size(656, 719);
			this.ReaderPanel.TabIndex = 3;
			// 
			// splitContainerWorkbench
			// 
			this.splitContainerWorkbench.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerWorkbench.Location = new System.Drawing.Point(0, 0);
			this.splitContainerWorkbench.Name = "splitContainerWorkbench";
			// 
			// splitContainerWorkbench.Panel1
			// 
			this.splitContainerWorkbench.Panel1.Controls.Add(this.ReaderTabPanel);
			this.splitContainerWorkbench.Panel1MinSize = 100;
			// 
			// splitContainerWorkbench.Panel2
			// 
			this.splitContainerWorkbench.Panel2.Controls.Add(this.InfoPane);
			this.splitContainerWorkbench.Panel2Collapsed = true;
			this.splitContainerWorkbench.Panel2MinSize = 0;
			this.splitContainerWorkbench.Size = new System.Drawing.Size(656, 719);
			this.splitContainerWorkbench.SplitterDistance = 342;
			this.splitContainerWorkbench.TabIndex = 1;
			// 
			// ReaderTabPanel
			// 
			this.ReaderTabPanel.ContextMenu = this.ReaderPanelContext;
			this.ReaderTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReaderTabPanel.HotTrack = true;
			this.ReaderTabPanel.Location = new System.Drawing.Point(0, 0);
			this.ReaderTabPanel.Multiline = true;
			this.ReaderTabPanel.Name = "ReaderTabPanel";
			this.ReaderTabPanel.SelectedIndex = 0;
			this.ReaderTabPanel.Size = new System.Drawing.Size(656, 719);
			this.ReaderTabPanel.TabIndex = 1;
			this.ReaderTabPanel.Selected += new System.Windows.Forms.TabControlEventHandler(this.ReaderTabPanelSelected);
			this.ReaderTabPanel.SelectedIndexChanged += new System.EventHandler(this.ReaderTabPanelSelectedIndexChanged);
			// 
			// InfoPane
			// 
			this.InfoPane.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InfoPane.Location = new System.Drawing.Point(0, 0);
			this.InfoPane.Name = "InfoPane";
			this.InfoPane.SelectedIndex = 0;
			this.InfoPane.Size = new System.Drawing.Size(46, 93);
			this.InfoPane.TabIndex = 0;
			// 
			// MenuInfo
			// 
			this.MenuInfo.Index = -1;
			this.MenuInfo.Text = "";
			// 
			// MenuInfoUpdate
			// 
			this.MenuInfoUpdate.Index = -1;
			this.MenuInfoUpdate.Text = "";
			// 
			// BottomPanel
			// 
			this.BottomPanel.Controls.Add(this.StatusBar);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomPanel.Location = new System.Drawing.Point(0, 726);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(656, 20);
			this.BottomPanel.TabIndex = 0;
			// 
			// StatusBar
			// 
			this.StatusBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StatusBar.Location = new System.Drawing.Point(0, 0);
			this.StatusBar.MaximumSize = new System.Drawing.Size(0, 20);
			this.StatusBar.MinimumSize = new System.Drawing.Size(0, 20);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
									this.statusBarInfo,
									this.statusBarTicker,
									this.statusBarEdition,
									this.statusBarMode});
			this.StatusBar.ShowPanels = true;
			this.StatusBar.Size = new System.Drawing.Size(656, 20);
			this.StatusBar.TabIndex = 0;
			// 
			// statusBarInfo
			// 
			this.statusBarInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusBarInfo.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarInfo.Name = "statusBarInfo";
			this.statusBarInfo.Width = 10;
			// 
			// statusBarTicker
			// 
			this.statusBarTicker.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarTicker.Name = "statusBarTicker";
			this.statusBarTicker.Width = 249;
			// 
			// statusBarEdition
			// 
			this.statusBarEdition.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarEdition.Name = "statusBarEdition";
			this.statusBarEdition.ToolTipText = "Show current paragraph edition information";
			this.statusBarEdition.Width = 300;
			// 
			// statusBarMode
			// 
			this.statusBarMode.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.statusBarMode.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.statusBarMode.Name = "statusBarMode";
			this.statusBarMode.Width = 80;
			// 
			// WorkBenchToolBar
			// 
			this.WorkBenchToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
									this.toolBarButton1,
									this.toolBarButton2,
									this.toolBarButton3,
									this.toolBarButton4,
									this.toolBarButton5,
									this.toolBarButton6});
			this.WorkBenchToolBar.DropDownArrows = true;
			this.WorkBenchToolBar.ImageList = this.ToolBarImageList;
			this.WorkBenchToolBar.Location = new System.Drawing.Point(0, 0);
			this.WorkBenchToolBar.Name = "WorkBenchToolBar";
			this.WorkBenchToolBar.ShowToolTips = true;
			this.WorkBenchToolBar.Size = new System.Drawing.Size(656, 7);
			this.WorkBenchToolBar.TabIndex = 2;
			this.WorkBenchToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBarButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Name = "toolBarButton1";
			this.toolBarButton1.Text = "Default";
			this.toolBarButton1.Visible = false;
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.Name = "toolBarButton4";
			this.toolBarButton4.Text = "4";
			this.toolBarButton4.Visible = false;
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Name = "toolBarButton5";
			this.toolBarButton5.Text = "5";
			this.toolBarButton5.Visible = false;
			// 
			// ToolBarImageList
			// 
			this.ToolBarImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ToolBarImageList.ImageSize = new System.Drawing.Size(32, 32);
			this.ToolBarImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// OpenFileDialog
			// 
			this.OpenFileDialog.Multiselect = true;
			// 
			// MenuInfoHelp
			// 
			this.MenuInfoHelp.Index = -1;
			this.MenuInfoHelp.Text = "";
			// 
			// Workbench
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(656, 746);
			this.Controls.Add(this.ReaderPanel);
			this.Controls.Add(this.WorkBenchToolBar);
			this.Controls.Add(this.BottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.MenuBar;
			this.Name = "Workbench";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pali Text Reader (BETA -  0.1.1.50 )";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.WorkbenchLayout);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkbenchKeyDown);
			this.Load += new System.EventHandler(this.WorkbenchLoad);
			this.ReaderPanel.ResumeLayout(false);
			this.splitContainerWorkbench.Panel1.ResumeLayout(false);
			this.splitContainerWorkbench.Panel2.ResumeLayout(false);
			this.splitContainerWorkbench.ResumeLayout(false);
			this.BottomPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarTicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarEdition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarMode)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TabControl InfoPane;
		private System.Windows.Forms.SplitContainer splitContainerWorkbench;
		private System.Windows.Forms.StatusBarPanel statusBarEdition;
		private System.Windows.Forms.StatusBarPanel statusBarMode;
		private System.Windows.Forms.StatusBarPanel statusBarTicker;
		private System.Windows.Forms.StatusBarPanel statusBarInfo;
		#endregion
		
		void MenuToolsPluginsClick(object sender, System.EventArgs e)
		{
			PluginViewer pv = new PluginViewer(availablePlugins);
			pv.ShowDialog();
		} 
		
		/// <summary>
		/// Look for plugins and load all you find
		///	then register plugins for display in pluginviewer
		///	Call the find plugins routine, to search in our Plugins Folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void WorkbenchLoad(object sender, System.EventArgs e)
		{
			try
			{
				FindPlugins(Application.StartupPath + @"\Plugins");
				availablePlugins = new Hashtable();
				
				foreach (Types.AvailablePlugin pluginOn in AvailablePlugins)
				{
					SplashScreen.SetStatus("Loading Plugin: " + pluginOn.Instance.DisplayName);
					ArrayList a = new ArrayList();
					a.Add(pluginOn.Instance.Author);
					a.Add(pluginOn.Instance.Version);
					a.Add(pluginOn.Instance.Description);
					a.Add(pluginOn.Instance.DisplayName);
					availablePlugins.Add(pluginOn.Instance.PluginName, a);
					SplashScreen.SetReferencePoint();
					Thread.Sleep(10);
				}
				OpenWelcomePage();
				LoadExistingBooks();
				SplashScreen.SetStatus("Pali Text Reader successfully initialized");
				SplashScreen.CloseForm();
				ba = new BCAccessor();
				statusBarInfo.Text = "B.E. " + ba.GetBuddhistYear();
				initProperties();
				statusTimer.Start();
				this.WorkBenchToolBar.Update();
			}
			catch(System.Exception ex)
			{
				ReportBugs rb = new ReportBugs("An error occured: " + ex.ToString());
				rb.ShowDialog();
				Close();
			}
		}
		 
		private void OpenWelcomePage()
		{
			//Panel con = new Panel();
			//open default html page with general info and welcome to new users..
			UpdatePaliViewer(Directory.GetCurrentDirectory() + @"\Work\" + "welcome.htm","Welcome!");
		}
		
		private void LoadExistingBooks()
		{
			try
			{
				foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Work"))
				{
					FileInfo fileOn = new FileInfo(file);
					if (fileOn.Extension.Equals(".htm") && !fileOn.Name.StartsWith("welcome"))
					{	
						UpdatePaliViewer(fileOn.FullName, fileOn.Name);
					}
					else if (fileOn.Extension.Equals(".html") && !fileOn.Name.StartsWith("welcome"))
					{	
						UpdatePaliViewer(fileOn.FullName, fileOn.Name);
					}
					SplashScreen.SetReferencePoint();
				}
			}
			catch(System.Exception ex)
			{
				ReportBugs rb = new ReportBugs(ex.ToString());
				rb.StartPosition = FormStartPosition.CenterParent;
				rb.ShowDialog();
			}
		}
		
		private void UpdatePaliViewer(string filePath, string fileName, string searchWord, int par, bool jumpToKeyword)
		{
			//create instance of PaliViewer, hand file over to pali viewer
			//put pali viewer instance on new tab page
			//host new tab page in tab control
			//look into dir and open specified book 
			PaliViewer pv = null;
			//Open view with paragraph as target?
			if(par == -1)
			{
				//Open view with searched word as target?
				if(searchWord == null)
					pv = new PaliViewer(filePath);
				else
				{
					if (jumpToKeyword)
					{
						//MessageBox.Show(jumpToKeyword.ToString());
						pv = new PaliViewer(filePath, searchWord, true);
					}
					else
						pv = new PaliViewer(filePath, searchWord);
				}
			}
			else
			{
				pv = new PaliViewer(filePath, par);
			}
			pv.LookUp += new PaliViewer.LookUpHandler(Lookup);
			pv.SilentLookUp += new PaliViewer.SilentLookUpHandler(SilentLookup);
			pv.TriggerAtthakatha += new PaliViewer.AtthakathaHandler(TriggerAtthakatha);
			pv.CompoundAnalyzing += new PaliViewer.CompoundAnalyzeHandler(AnalyzeCompound);
			pv.DetectEdition += new PaliViewer.EditionDetectionHandler(ShowEditionInformation);
			pv.ContentsParsed += new PaliViewer.ContentsParsedHandler(ShowBookContents);
			TabPage newTabPage = new TabPage();
  			newTabPage.Dock = DockStyle.Fill;
  			newTabPage.Text = NamingConverter.GetBookNameFromFile(fileName);
  			newTabPage.Tag = fileName;
  			newTabPage.Controls.Add(pv);
  			pv.Dock = DockStyle.Fill;
  			ReaderTabPanel.TabPages.Add(newTabPage);
  			ReaderTabPanel.SelectedTab = newTabPage;
		}
		
		private void UpdatePaliViewer(string filePath, string fileName, string searchWord, bool jumpToKeyword)
		{
			UpdatePaliViewer(filePath, fileName, searchWord, -1, jumpToKeyword);
		}
		
		private void UpdatePaliViewer(string filePath, string fileName, string searchWord)
		{
			UpdatePaliViewer(filePath, fileName, searchWord, -1, false);
		}
		
		private void UpdatePaliViewer(string filePath, string fileName, int par)
		{
			UpdatePaliViewer(filePath, fileName, null, par, false);
		}
		
		private void UpdatePaliViewer(string filePath, string fileName)
		{
			UpdatePaliViewer(filePath, fileName, null, -1, false);
		}
		
		void ToolBarButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
		}
		
		void WorkbenchLayout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			if( m_bLayoutCalled == false )
			{
				m_bLayoutCalled = true;
				m_dt = DateTime.Now;
				if( SplashScreen.SplashForm != null )
					SplashScreen.SplashForm.Owner = this;
			}
		}
		
		void ReaderTabPanelSelectedIndexChanged(object sender, System.EventArgs e)
		{		
			ShowBookContents();
			GC.Collect();
		}
		
		#region Main Menu methods
		void MenuInfoAboutClick(object sender, System.EventArgs e)
		{
			AboutBox ab = new AboutBox();
			ab.ShowDialog();
		}
		
		void MenuInfoHelpClick(object sender, System.EventArgs e)
		{
			try
			{
				Process.Start(Directory.GetCurrentDirectory() + @"\ptr.chm");
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Help file not found \n" + ex.ToString());
			}
		}
		
		void MenuFileCloseClick(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		void MenuFileOpenClick(object sender, System.EventArgs e)
		{
			string[] filesToLoad;
		    OpenFileDialog ofd = new OpenFileDialog();
		    ofd.Title = "PaliReader" ;
		    ofd.InitialDirectory = @"c:\" ;
		    ofd.Filter = "HTML (*.html)|*.html|XHTML (*.xhtml)|*.xhtml" ;
		    ofd.FilterIndex = 2 ;
		    ofd.RestoreDirectory = true ;
		    ofd.Multiselect = true;
		    if(ofd.ShowDialog() == DialogResult.OK)
		    {
		         filesToLoad  = ofd.FileNames ;
		         for(int i = 0; i < filesToLoad.Length; i++)
		         {
		         	FileInfo f = new FileInfo(filesToLoad[i]);
		         	UpdatePaliViewer(f.FullName, f.Name);
		         }
		    } 	
		}
		#endregion
		
		#region Find, Load, Unload Plugins
		private Types.AvailablePlugins colAvailablePlugins = new Types.AvailablePlugins();
		
		/// <summary>
		/// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
		/// </summary>
		public Types.AvailablePlugins AvailablePlugins
		{
			get {return colAvailablePlugins;}
			set {colAvailablePlugins = value;}
		}
		
		/// <summary>
		/// Searches the Application's Startup Directory for Plugins
		/// </summary>
		public void FindPlugins()
		{
			FindPlugins(AppDomain.CurrentDomain.BaseDirectory);
		}
		/// <summary>
		/// Searches the passed Path for Plugins
		/// </summary>
		/// <param name="Path">Directory to search for Plugins in</param>
		public void FindPlugins(string Path)
		{
			colAvailablePlugins.Clear();
			foreach (string fileOn in Directory.GetFiles(Path))
			{
				FileInfo file = new FileInfo(fileOn);
				if (file.Extension.Equals(".dll"))
				{	
					this.AddPlugin(fileOn);				
				}
			}
		}
		
		/// <summary>
		/// Unloads and Closes all AvailablePlugins
		/// </summary>
		public void ClosePlugins()
		{
			foreach (Types.AvailablePlugin pluginOn in colAvailablePlugins)
			{
				pluginOn.Instance.Dispose(); 
				pluginOn.Instance = null;
			}
			colAvailablePlugins.Clear();
		}
		
		private void AddPlugin(string FileName)
		{
			Assembly pluginAssembly = Assembly.LoadFrom(FileName);
			foreach (Type pluginType in pluginAssembly.GetTypes())
			{
				if (pluginType.IsPublic) //Only look at public types
				{
					if (!pluginType.IsAbstract)  //Only look at non-abstract types
					{
						Type typeInterface = pluginType.GetInterface("PluginInterface.IPlugin", true);
						if (typeInterface != null)
						{
							Types.AvailablePlugin newPlugin = new Types.AvailablePlugin();
							newPlugin.AssemblyPath = FileName;
							newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
							newPlugin.Instance.Host = this;
							newPlugin.Instance.Initialize();
							this.colAvailablePlugins.Add(newPlugin);
							newPlugin = null;
						}							
						typeInterface = null;
					}				
				}			
			}
			
			pluginAssembly = null;
		}
		#endregion
		
		#region IPluginInterface defined methods
		public void Feedback(object info, IPlugin Plugin)
		{
			try
			{
				if (Plugin.PluginName == "CanonSearch" && info != null)
				{
					string bookName  = ((string)info).Split('#')[0];
				 	string searchWrd = ((string)info).Split('#')[1];
					lastBookName = bookName;
			    	UpdatePaliViewer(Directory.GetCurrentDirectory() + @"\Work\" + bookName + ".htm", bookName, searchWrd);
				}
				else if (Plugin.PluginName == "AdjustFonts" && info != null)
				{
					if (((string)info) == "refresh")
					{
						PaliViewer pv = (PaliViewer)(Control)ReaderTabPanel.TabPages[selectedTabNo].GetNextControl(null, true);
						pv.refreshPage();
					}
				}
				else if (Plugin.PluginName == "QuickNav" && info != null && (string)info != "")
				{
					if (((string)info).Contains("BM#"))
					{
						string bookMarkPosition = ((string)info).Split('#')[1];
						Types.AvailablePlugin selectedPlugin = AvailablePlugins.Find("FavMan");
						if (selectedPlugin != null)
						{
							selectedPlugin.Instance.SetPluginParameter(bookMarkPosition);
							this.InfoPane.SelectedIndex = 2;
						}
					}
					else
					{
						PaliViewer pv = (PaliViewer)(Control)ReaderTabPanel.TabPages[selectedTabNo].GetNextControl(null, true);
						pv.JumpToParagraph((string)info);
					}
				}
				else if (Plugin.PluginName == "FavMan" && info != null && (string)info != "")
				{
					string book = ((string)info).Split('\\')[0];
					int l = ((string)info).Split('\\').Length;
					string searchWord = ((string)info).Split('\\')[l - 1];
					book = PaliReaderUtils.NamingConverter.GetFileNameFromBook(book);
					lastBookName = book;
					AalekhDecoder.UnzipFromZipLibrary(book);
			    	if(!onlineMode)
			    		UpdatePaliViewer(Directory.GetCurrentDirectory() + @"\Work\" + book + ".htm", book, searchWord, true);
			    	else
			    		UpdatePaliViewer("http://tipitaka.nibbanam.com/books/" + book + ".htm", book, searchWord, true);
				}
			}
			catch(System.Exception ex)
			{
				ReportBugs rb = new ReportBugs(ex.ToString());
				rb.ShowDialog();
			}
		}
		public void StatusBarText(string a)
		{
			StatusBar.Text = a;
		}
		//Grant plugins Toolbar access
		public ToolBar GetToolBarReference()
		{
			return this.WorkBenchToolBar;
		}
		//Grant Plugins Menu access
		public MainMenu GetMainMenuReference()
		{
			return this.MenuBar;
		}
		public ImageList GetToolBarImageListReference()
		{
			return this.ToolBarImageList;
		}
		public TabControl GetInfoPaneReference()
		{
			return this.InfoPane;
		}
		public void AddToolBarButton(string a, Image b)
		{
			ToolBarButton tbb = new ToolBarButton(a);
			tbb.Visible = true;
			ToolBarImageList.Images.Add(b);
			tbb.ImageIndex = ToolBarImageList.Images.Count-1;
			WorkBenchToolBar.Buttons.Add(tbb);	
		}
		public void SignalReadyBookExtraction(string bookName)
		{
			try
			{
		    	lastBookName = bookName;
		    	if(!onlineMode)
		    		UpdatePaliViewer(Directory.GetCurrentDirectory() + @"\Work\" + bookName + ".htm", bookName);
		    	else
		    		UpdatePaliViewer("http://tipitaka.nibbanam.com/books/" + bookName + ".htm", bookName);
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error while trying to show book \n" + ex.ToString());
			}
		    return;
		}
		
		#endregion
		
		
		void RPanelContextCloseClick(object sender, System.EventArgs e)
		{
			TabPage tp = ReaderTabPanel.SelectedTab;
			string fileName = (String)tp.Tag;
			ReaderTabPanel.TabPages.RemoveAt(ReaderTabPanel.SelectedIndex);
			string dir = Directory.GetCurrentDirectory();
			if(File.Exists(dir + @"\Work\" + fileName))
				File.Delete(dir + @"\Work\" + fileName);
			GC.Collect();
		}
		
		void RPanelContextCloseAllClick(object sender, System.EventArgs e)
		{
			ReaderTabPanel.TabPages.Clear();
			GC.Collect();
		}
		
		void MenuFileSaveAsClick(object sender, System.EventArgs e)
		{

			TabPage tp = ReaderTabPanel.SelectedTab;
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "HTML Files (*.htm)|*.htm| Unicode (text critical) (*.htm)|*.htm| Unicode (read optimal) (*.htm)|*.htm";
			sfd.FileName = "test.html";
			sfd.ShowDialog();
		}
		
		void MenuFullScreenClick(object sender, System.EventArgs e)
		{
			if(fullscreenMode)
			{
				fullscreenMode = false;
			}
			else
			{
				fullscreenMode = true;
			}
			if(fullscreenMode)
			{
				MenuFullScreen.Text = "Normal view";
				TopMost = true;
				WorkBenchToolBar.Hide();
				Visible           = false;
				FormBorderStyle   = FormBorderStyle.None;
				WindowState       = FormWindowState.Maximized;
				Visible           = true;
			}
			else
			{
				this.MenuFullScreen.Text = "Fullscreen";
				this.TopMost = false;
				this.WorkBenchToolBar.Show();
				this.FormBorderStyle = FormBorderStyle.Sizable;
				this.WindowState = FormWindowState.Maximized;
			}
		}
		
		void MenuViewBookClick(object sender, System.EventArgs e)
		{
			//make paliviewer smaller -
			//new idea - to improve memory, do not add info pane on each tab but
			//add info pane as separate tabpage for all tabs to be visible
			//this will need a sync mechanism between selected text and info, but will
			//make much more sense.
			//info pane will include: bookmarkmanager, quick navigation treeview, translation viewer
			ToggleInfoPane();
			
			//TabPage tp = ReaderTabPanel.SelectedTab;
			/*PaliViewer pv = (PaliViewer)tp.Controls[0];
			pv.Dock = DockStyle.Left;
			pv.Width = (ReaderTabPanel.Width * 3) / 5;
			
			PaliViewer pv1 = new PaliViewer(@"C:\CSCD\Temp\dn.02.htm");
			pv1.Width = (ReaderTabPanel.Width * 2) / 5;
			pv1.Dock = DockStyle.Right;
			
			tp.Controls.Add(pv1);
			*/
			//add second paliviewer
			//load english translation
			//synchronize both texts on a per paragrahp basis
			//eventually, highlight current paragraph with 
			// colored rectangle using DOM manipulation
		}
		//Two page viewer
		void RPanelTPViewClick(object sender, System.EventArgs e)
		{
			try
			{
				TabPage tp = ReaderTabPanel.SelectedTab;
				TwoPageViewer tpv = new TwoPageViewer(tp.Text);
				tpv.ShowDialog();
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message.ToString());
			}
		}
		
		void MenuHelpCommentClick(object sender, System.EventArgs e)
		{
			SendComment sc = new SendComment();
			sc.ShowDialog();
		}
		
		void MenuHelpUpdateClick(object sender, System.EventArgs e)
		{
			Process.Start("IExplore","http://sourceforge.net/projects/palireader/");
		}
		
		
		void MenuHelpHelpClick(object sender, System.EventArgs e)
		{
			try
			{
				Process.Start(Directory.GetCurrentDirectory() + @"\Work\ptr.chm");
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Help file not found \n" + ex.ToString());
			}
		}
		
		void MenuHelpMemorizeClick(object sender, System.EventArgs e)
		{
			Process.Start("IExplore","http://tipitaka.nibbanam.com/memorize_the_tipitaka.htm");
		}
		
		
		
		void WorkbenchKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
				this.Close();
			if(e.KeyCode == Keys.F11)
				this.MenuFullScreenClick(null, null);
		}
		
		void ReaderTabPanelKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				RPanelContextCloseClick(null, null);
			}
		}
		
		
		void Lookup(string a)
		{
			Types.AvailablePlugin selectedPlugin = AvailablePlugins.Find("Pali Dictionary");
			if(selectedPlugin != null)
			{
				selectedPlugin.Instance.SetPluginParameter((String)a);
				selectedPlugin.Instance.MainInterface.ShowDialog();
			}
		}
		string SilentLookup(string b)
		{
			Types.AvailablePlugin selectedPlugin = AvailablePlugins.Find("Pali Dictionary");
			if(selectedPlugin != null)
			{
				return (String)selectedPlugin.Instance.GetPluginParameter(b);
			}
			else
				return null;
		}
		void TriggerAtthakatha(int paragraph, string fileName)
		{
			string attha = "";
			attha = NamingConverter.GetMatchingCommentary(fileName);
			AalekhDecoder.UnzipFromZipLibrary(attha);
			lastBookName = attha;
			if(!onlineMode)
		    		UpdatePaliViewer(Directory.GetCurrentDirectory() + @"\Work\" + attha + ".htm", attha, paragraph );
		    	else
		    		UpdatePaliViewer("http://tipitaka.nibbanam.com/books/" + attha + ".htm", attha, paragraph);
		}
		void AnalyzeCompound(string compound)
		{
			string result = PaliReaderUtils.CompoundAnalyzer.GetCompositaType(compound);
			MessageBox.Show(result);
		}
		private void ShowEditionInformation(string paragraphInformation)
		{
			if (paragraphInformation != null)
			{
				
				this.statusBarEdition.Text = "Edition: " + paragraphInformation.Split('#')[0] 
					+ " Book: " + paragraphInformation.Split('#')[1] 
					+ " Page: " + paragraphInformation.Split('#')[2]
					+ " Par: " + paragraphInformation.Split('#')[3];
			}
		}
		void StatusTimerTick(object sender, System.EventArgs e)
		{
			Random r = new Random();
			int outp = r.Next(0, 11);
			TimeSpan daysLeft = DateTime.Parse(ba.GetNextPoya()) - DateTime.Now;
			string text = "";
			switch(outp)
			{
				case 0: text = "To open a book click on the Book selection button!"; break;
				case 1: text = "You can enable/disable the automatic word translation using the context menu"; break;
				case 2: text = "Want to browse pali texts online? Select 'File | Work online' from the menu"; break;
				case 3: text = "If you want to mark interesting passages right click and click 'highlight passage'"; break;
			    case 4: text = "Learn more about all the nice features of PTR from the help menu"; break;
			    case 5: text = "Next full moon is " + ba.GetNextPoya() + " which is in " + daysLeft.Days + " days." ; break;
			    case 6: text = "Want to look up commentaries (atthakatha) for a paragraph? Just right click and select 'see commentary'"; break;
			    case 7: text = "Need more space? Click 'View | Fullscreen'"; break;
			   	case 8: text = "Want to copy paste pali text? Disable automatic word translation first!"; break;
			    case 9: text = "Next full moon is " + ba.GetNextPoya() + " which is in " + daysLeft.Days + " days." ; break;
			    case 10: text = "To support this project: keep the sila, do good and purify your mind with samatha and vipassana"; break;
			    case 11: text = "Any good idea what's missing? Drop us an e-mail: palireader@gmail.com"; break;
			    default: text = "-"; break;
			}
			statusBarTicker.Text = text;
		}
		
		void MenuEditCopyClick(object sender, System.EventArgs e)
		{
			MessageBox.Show("Use the copy function in your context menu (select text passage and click right mouse button)");
		}
		
		void MenuEditPasteClick(object sender, System.EventArgs e)
		{
			MessageBox.Show("TODO");
		}
		
		void MenuFileOnOfflineClick(object sender, System.EventArgs e)
		{
			if(onlineMode)
			{
				onlineMode = false;
			}
			else
			{
				onlineMode = true;
			}
			Types.AvailablePlugin selectedPlugin = AvailablePlugins.Find("BookSelector");
			if(selectedPlugin != null)
			{
				if(onlineMode)
				{
					selectedPlugin.Instance.SetPluginParameter(true);
					MenuFileOnOffline.Text = "Work offline";
					statusBarMode.Text = "Reading online";
				}
				else
				{
					selectedPlugin.Instance.SetPluginParameter(false);
					MenuFileOnOffline.Text = "Work online";
					statusBarMode.Text = "Reading offline";
				}
			}
		}
		private void initProperties()
		{
			onlineMode = Convert.ToBoolean(PaliReaderUtils.PropertyManager.GetInstance().GetGeneralProperty("OnlineMode"));
			ToggleOnlineMode();
			infoPaneActive = Convert.ToBoolean(PaliReaderUtils.PropertyManager.GetInstance().GetGeneralProperty("InfoPane"));
			infoPaneActive = (infoPaneActive == true) ? false : true;
			ToggleInfoPane();
		}
		
		void ReaderTabPanelSelected(object sender, System.Windows.Forms.TabControlEventArgs e)
		{
		}
		
		/// <summary>
		/// This method is called either
		/// After each tab index selection changed
		/// Or, during loading books, after the book contents have been generated in the paliviewer
		/// </summary>
		private void ShowBookContents()
		{
			selectedTabNo = ReaderTabPanel.SelectedIndex;
			Types.AvailablePlugin selectedPlugin = AvailablePlugins.Find("QuickNav");
			if(selectedPlugin != null)
			{
				PaliViewer pv = (PaliViewer)(Control)ReaderTabPanel.TabPages[selectedTabNo].GetNextControl(null, true);
				if (pv != null)
				{
					selectedPlugin.Instance.SetPluginParameter(pv.GetBookContents());
				}
			}
		}
		
		private void ToggleInfoPane()
		{
			if (!infoPaneActive)
			{
				splitContainerWorkbench.SplitterDistance = (ReaderTabPanel.Width * 4)/ 5;
				splitContainerWorkbench.Panel2Collapsed = false;
				infoPaneActive = true;
				PaliReaderUtils.PropertyManager.GetInstance().SetGeneralProperty("InfoPane", "true");
			}
			else
			{
				splitContainerWorkbench.Panel2Collapsed = true;
				infoPaneActive = false;
				PaliReaderUtils.PropertyManager.GetInstance().SetGeneralProperty("InfoPane", "false");
			}
		}
		
		private void ToggleOnlineMode()
		{
			if(onlineMode)
			{
					MenuFileOnOffline.Text = "Work offline";
					statusBarMode.Text = "Reading Online";
			}
			else
			{
					MenuFileOnOffline.Text = "Work online";
					statusBarMode.Text = "Reading Offline";
			}
		}
	}
}
