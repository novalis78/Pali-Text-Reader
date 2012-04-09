using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using System.IO;
using Common;

//Plugin namespace
namespace PaliReaderPlugin
{
	/// <summary>
	///
	/// </summary>
	public class FavMan : System.Windows.Forms.UserControl, IPlugin
	{
		private System.Windows.Forms.TabControl infoPaneTC = null; 
		private System.Windows.Forms.TabPage quickNavTabPage = null;
		private System.Windows.Forms.TreeView favoritesTree = null;
		private System.Windows.Forms.TreeNode root	= null;
		private System.Windows.Forms.TreeNode treeNode1 = null;
		public FavMan()
		{
			InitializeComponent();
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			treeNode1 = new System.Windows.Forms.TreeNode("Bookmarks:", 1, 1);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavMan));
			this.favoritesTree = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.saveFavList = new System.Windows.Forms.Button();
			this.openFavList = new System.Windows.Forms.Button();
			this.delFav = new System.Windows.Forms.Button();
			this.showFav = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// favoritesTree
			// 
			this.favoritesTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.favoritesTree.ImageIndex = 0;
			this.favoritesTree.ImageList = this.imageList1;
			this.favoritesTree.LabelEdit = true;
			this.favoritesTree.Location = new System.Drawing.Point(0, 0);
			this.favoritesTree.Name = "favoritesTree";
			treeNode1.ImageIndex = 1;
			treeNode1.Name = "Node0";
			treeNode1.SelectedImageIndex = 1;
			treeNode1.Text = "Bookmarks:";
			this.favoritesTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
									treeNode1});
			this.favoritesTree.SelectedImageIndex = 0;
			this.favoritesTree.ShowNodeToolTips = true;
			this.favoritesTree.Size = new System.Drawing.Size(362, 467);
			this.favoritesTree.TabIndex = 0;
			this.favoritesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ContentTreeAfterSelect);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "ledorange.png");
			this.imageList1.Images.SetKeyName(1, "infoabout.png");
			// 
			// saveFavList
			// 
			this.saveFavList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.saveFavList.Location = new System.Drawing.Point(0, 444);
			this.saveFavList.Name = "saveFavList";
			this.saveFavList.Size = new System.Drawing.Size(362, 23);
			this.saveFavList.TabIndex = 1;
			this.saveFavList.Text = "save bookmark list";
			this.saveFavList.UseVisualStyleBackColor = true;
			this.saveFavList.Click += new System.EventHandler(this.SaveFavListClick);
			// 
			// openFavList
			// 
			this.openFavList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.openFavList.Location = new System.Drawing.Point(0, 421);
			this.openFavList.Name = "openFavList";
			this.openFavList.Size = new System.Drawing.Size(362, 23);
			this.openFavList.TabIndex = 2;
			this.openFavList.Text = "open bookmark list";
			this.openFavList.UseVisualStyleBackColor = true;
			this.openFavList.Click += new System.EventHandler(this.OpenFavListClick);
			// 
			// delFav
			// 
			this.delFav.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.delFav.Location = new System.Drawing.Point(0, 398);
			this.delFav.Name = "delFav";
			this.delFav.Size = new System.Drawing.Size(362, 23);
			this.delFav.TabIndex = 3;
			this.delFav.Text = "delete";
			this.delFav.UseVisualStyleBackColor = true;
			this.delFav.Click += new System.EventHandler(this.DelFavClick);
			// 
			// showFav
			// 
			this.showFav.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.showFav.Location = new System.Drawing.Point(0, 375);
			this.showFav.Name = "showFav";
			this.showFav.Size = new System.Drawing.Size(362, 23);
			this.showFav.TabIndex = 2;
			this.showFav.Text = "show";
			this.showFav.UseVisualStyleBackColor = true;
			this.showFav.Click += new System.EventHandler(this.OpenFavClick);
			// 
			// FavMan
			// 
			this.AutoSize = true;
			this.Controls.Add(this.showFav);
			this.Controls.Add(this.delFav);
			this.Controls.Add(this.openFavList);
			this.Controls.Add(this.saveFavList);
			this.Controls.Add(this.favoritesTree);
			this.Name = "FavMan";
			this.Size = new System.Drawing.Size(362, 467);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button showFav;
		private System.Windows.Forms.Button saveFavList;
		private System.Windows.Forms.Button openFavList;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button delFav;
		#endregion
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "FavMan";
		string myDisplayName = "Favorites";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Info pane plugin for favorite links to pali passages/suttas/books";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\sample.png");
		
		/// <summary>
		/// If you need to hand over parameters from the hosting application
		/// (Pali Text Reader) towards the plugin, this is the method to call.
		/// </summary>
		/// <param name="o"></param
        public void SetPluginParameter(Object o)
		{
        	string favLocation = (string)o;
        	favLocation = favLocation.Replace("Start here ...\\", "");
        	System.Windows.Forms.TreeNode tn = new System.Windows.Forms.TreeNode(favLocation);
        	if (tn != null)
        	{
        		tn.ToolTipText = favLocation;
        		if (treeNode1 != null)
        		{
        			treeNode1.Nodes.Add(tn);
        			this.Focus();
        			tn.Expand();
        			MessageBox.Show("I added your bookmark to the favorites");
        		}
        	}
		}
        
		public Object GetPluginParameter(Object o)
		{
			return null;
		}		
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
			infoPaneTC = this.Host.GetInfoPaneReference();
			if(infoPaneTC != null) 
			{
				infoPaneTC.SelectedIndexChanged += new EventHandler(TabSelectedClick);
				quickNavTabPage = new TabPage(DisplayName);
				this.favoritesTree.Dock = DockStyle.Fill;
				this.Dock = DockStyle.Fill;
				quickNavTabPage.Dock = DockStyle.Fill;
				if(quickNavTabPage != null)
				{
					quickNavTabPage.Controls.Add(this);
					infoPaneTC.TabPages.Add(quickNavTabPage);
				}
			}
			
		}
		//TODO
		//Whenever reader tab changes, check whether we can get
		//a treeview object 
	
		
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
				return null;
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
		
		/// <summary>
		/// If user clicks selects this tab on info pane
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabSelectedClick(object sender, System.EventArgs e)
		{
			TabPage tp = infoPaneTC.SelectedTab;
			if(tp.Name == DisplayName)
			{
				MessageBox.Show("hi there, i am fav manager!");
			}
		}
		
		
		void ContentTreeAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
		}
		
		void OpenFavClick(object sender, System.EventArgs e)
		{
			Host.Feedback(favoritesTree.SelectedNode.ToolTipText, this);
		}
		
		void DelFavClick(object sender, System.EventArgs e)
		{
			if (DialogResult.OK == MessageBox.Show("Do you really want to delete the selected bookmark?", "Favorites Manager", MessageBoxButtons.OKCancel))
				favoritesTree.SelectedNode.Remove();
		}
		
		void OpenFavListClick(object sender, System.EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog1 = new OpenFileDialog();
				openFileDialog1.Filter = "Pali Text Reader bookmarks (*.prb)|*.prb";
				openFileDialog1.RestoreDirectory = true ;
				
				if(openFileDialog1.ShowDialog() == DialogResult.OK)
		        {
					FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
					StreamReader sr = new StreamReader(fs);
					string line = "";
					while ((line = sr.ReadLine()) != null) 
					{
						string text = line.Split('#')[0];
						string tool = line.Split('#')[1];
	        			System.Windows.Forms.TreeNode tn = new System.Windows.Forms.TreeNode(text);
	        			if (tn != null)
	        			{
	        				tn.ToolTipText = tool;
	        				if (treeNode1 != null)
	        				{
	        					treeNode1.Nodes.Add(tn);	
							}
	        			}
					}
				}
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString());
			}
		}
		
		void SaveFavListClick(object sender, System.EventArgs e)
		{
			try
			{
				FileStream myStream;
	            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
	
	            saveFileDialog1.Filter = "Pali Text Reader bookmarks (*.prb)|*.prb";
	            saveFileDialog1.RestoreDirectory = true ;
	
	            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
	            {
	            	if((myStream = (FileStream)saveFileDialog1.OpenFile()) != null)
	                  {
	                        StreamWriter wText =new StreamWriter(myStream);
	                        foreach(TreeNode tn in treeNode1.Nodes)
	                        {
	                        	wText.WriteLine(tn.Text + "#" + tn.ToolTipText);
	                        }
	                        wText.Close();
	                        myStream.Close();
	                  }
	            }
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.ToString());
			}
		}
	}
}
