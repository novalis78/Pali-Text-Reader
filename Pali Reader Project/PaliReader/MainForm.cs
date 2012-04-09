/*
 * Created by SharpDevelop.
 * Codename: Ganthamala
 * User: novalis78
 * Date: 21.06.2005
 * Time: 20:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaliReader
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public class Workbench : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem MenuFileOpen;
		private System.Windows.Forms.MenuItem MenuFile;
		private System.Windows.Forms.Panel BottomPanel;
		private System.Windows.Forms.Panel ReaderPanel;
		private System.Windows.Forms.MenuItem MenuToolsPlugins;
		private System.Windows.Forms.ToolBar ToolBar;
		private System.Windows.Forms.MenuItem MenuInfoHelp;
		private System.Windows.Forms.MenuItem MenuInfo;
		private System.Windows.Forms.MenuItem MenuEdit;
		private System.Windows.Forms.MenuItem MenuInfoAbout;
		private System.Windows.Forms.StatusBar StatusBar;
		private System.Windows.Forms.MenuItem MenuInfoUpdate;
		private System.Windows.Forms.MainMenu Menu;
		private System.Windows.Forms.MenuItem MenuTools;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new MainForm());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.MenuTools = new System.Windows.Forms.MenuItem();
			this.Menu = new System.Windows.Forms.MainMenu();
			this.MenuInfoUpdate = new System.Windows.Forms.MenuItem();
			this.StatusBar = new System.Windows.Forms.StatusBar();
			this.MenuInfoAbout = new System.Windows.Forms.MenuItem();
			this.MenuEdit = new System.Windows.Forms.MenuItem();
			this.MenuInfo = new System.Windows.Forms.MenuItem();
			this.MenuInfoHelp = new System.Windows.Forms.MenuItem();
			this.ToolBar = new System.Windows.Forms.ToolBar();
			this.MenuToolsPlugins = new System.Windows.Forms.MenuItem();
			this.ReaderPanel = new System.Windows.Forms.Panel();
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.MenuFile = new System.Windows.Forms.MenuItem();
			this.MenuFileOpen = new System.Windows.Forms.MenuItem();
			this.BottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuTools
			// 
			this.MenuTools.Index = 2;
			this.MenuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.MenuToolsPlugins});
			this.MenuTools.Text = "Tools";
			// 
			// Menu
			// 
			this.Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.MenuFile,
						this.MenuEdit,
						this.MenuTools,
						this.MenuInfo});
			// 
			// MenuInfoUpdate
			// 
			this.MenuInfoUpdate.Index = 0;
			this.MenuInfoUpdate.Text = "Update";
			// 
			// StatusBar
			// 
			this.StatusBar.Location = new System.Drawing.Point(0, 10);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(292, 22);
			this.StatusBar.TabIndex = 0;
			// 
			// MenuInfoAbout
			// 
			this.MenuInfoAbout.Index = 2;
			this.MenuInfoAbout.Text = "About";
			// 
			// MenuEdit
			// 
			this.MenuEdit.Index = 1;
			this.MenuEdit.Text = "Edit";
			// 
			// MenuInfo
			// 
			this.MenuInfo.Index = 3;
			this.MenuInfo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.MenuInfoUpdate,
						this.MenuInfoHelp,
						this.MenuInfoAbout});
			this.MenuInfo.Text = "Info";
			// 
			// MenuInfoHelp
			// 
			this.MenuInfoHelp.Index = 1;
			this.MenuInfoHelp.Text = "Help";
			// 
			// ToolBar
			// 
			this.ToolBar.DropDownArrows = true;
			this.ToolBar.Location = new System.Drawing.Point(0, 0);
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.ShowToolTips = true;
			this.ToolBar.Size = new System.Drawing.Size(292, 42);
			this.ToolBar.TabIndex = 2;
			// 
			// MenuToolsPlugins
			// 
			this.MenuToolsPlugins.Index = 0;
			this.MenuToolsPlugins.Text = "Plugins";
			this.MenuToolsPlugins.Click += new System.EventHandler(this.MenuToolsPluginsClick);
			// 
			// ReaderPanel
			// 
			this.ReaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReaderPanel.Location = new System.Drawing.Point(0, 42);
			this.ReaderPanel.Name = "ReaderPanel";
			this.ReaderPanel.Size = new System.Drawing.Size(292, 192);
			this.ReaderPanel.TabIndex = 3;
			// 
			// BottomPanel
			// 
			this.BottomPanel.Controls.Add(this.StatusBar);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomPanel.Location = new System.Drawing.Point(0, 234);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(292, 32);
			this.BottomPanel.TabIndex = 0;
			// 
			// MenuFile
			// 
			this.MenuFile.Index = 0;
			this.MenuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
						this.MenuFileOpen});
			this.MenuFile.Text = "File";
			// 
			// MenuFileOpen
			// 
			this.MenuFileOpen.Index = 0;
			this.MenuFileOpen.Text = "Open";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.ReaderPanel);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.BottomPanel);
			this.Menu = this.Menu;
			this.Name = "MainForm";
			this.Text = "PaliReader - Codename: Ganthamala";
			this.BottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion
		void MenuToolsPluginsClick(object sender, System.EventArgs e)
		{
			PluginViewer pv = new PluginViewer();
			pv.Show();
		}
		
	}
}
