using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using Common;

//Plugin namespace
namespace PaliReaderPlugin
{
	/// <summary>
	///
	/// </summary>
	public class QuickNav : System.Windows.Forms.UserControl, IPlugin
	{
		private System.Windows.Forms.TabControl infoPaneTC = null; 
		private System.Windows.Forms.TabPage quickNavTabPage = null;
		private System.Windows.Forms.TreeView contentTree = null;
		private Common.ITree<string> bookContent = null;
			
		public QuickNav()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickNav));
			this.contentTree = new System.Windows.Forms.TreeView();
			this.bookMark = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// contentTree
			// 
			this.contentTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentTree.ImageIndex = 0;
			this.contentTree.ImageList = this.imageList1;
			this.contentTree.Location = new System.Drawing.Point(0, 0);
			this.contentTree.Name = "contentTree";
			this.contentTree.SelectedImageIndex = 0;
			this.contentTree.Size = new System.Drawing.Size(362, 467);
			this.contentTree.TabIndex = 0;
			this.contentTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ContentTreeAfterSelect);
			// 
			// bookMark
			// 
			this.bookMark.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bookMark.Location = new System.Drawing.Point(0, 444);
			this.bookMark.Name = "bookMark";
			this.bookMark.Size = new System.Drawing.Size(362, 23);
			this.bookMark.TabIndex = 1;
			this.bookMark.Text = "add to favorites";
			this.bookMark.UseVisualStyleBackColor = true;
			this.bookMark.Click += new System.EventHandler(this.BookMarkClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "forward.png");
			this.imageList1.Images.SetKeyName(1, "contents.png");
			// 
			// QuickNav
			// 
			this.AutoSize = true;
			this.Controls.Add(this.bookMark);
			this.Controls.Add(this.contentTree);
			this.Name = "QuickNav";
			this.Size = new System.Drawing.Size(362, 467);
			this.ResumeLayout(false);
		}
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button bookMark;
		#endregion
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "QuickNav";
		string myDisplayName = "QuickNav";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Info pane plugin for quick navigation in open books";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\sample.png");
		
		/// <summary>
		/// If you need to hand over parameters from the hosting application
		/// (Pali Text Reader) towards the plugin, this is the method to call.
		/// </summary>
		/// <param name="o"></param
        public void SetPluginParameter(Object o)
		{
        	bookContent = (ITree<String>)o;
        	if (bookContent != null)
        	{
        		FillTreeView(this.contentTree, bookContent);
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
				this.contentTree.Dock = DockStyle.Fill;
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
				MessageBox.Show("hi there, i am quick nav!");
			}
		}
		
		
		private void FillTreeView( TreeView treeView, ITree<String> tree )
		{
			object oSelected = null;
			if ( treeView.SelectedNode != null ) oSelected = treeView.SelectedNode.Tag;

			using ( new TreeViewUpdate( treeView ) )
			{
				treeView.Nodes.Clear();

				if ( tree == null ) return;

				TreeNode root = treeView.Nodes.Add("Start here ...");//tree.Root.ToString()
				root.Tag = tree.Root;
				root.ImageIndex = 1;
				root.SelectedImageIndex = 1;
				root.Expand();

				TreeNode nSelected = AddNodes( root.Nodes, tree.Root, oSelected );

				//treeView.ExpandAll();

				if ( nSelected != null )
					treeView.SelectedNode = nSelected;
				else
					if ( treeView.Nodes.Count > 0 )
						treeView.SelectedNode = treeView.Nodes[ 0 ];
			}
		}

		private TreeNode AddNodes( TreeNodeCollection nodes, INode<String> parent, object oSelected )
		{
			TreeNode nSelected = null;

			foreach ( INode<String> child in parent.DirectChildren.Nodes )
			{
				TreeNode node = nodes.Add( child.ToString() );
				node.Tag = child;

				if ( child == oSelected ) 
					nSelected = node;

				TreeNode n = AddNodes( node.Nodes, child, oSelected );
				if ( n != null ) 
					nSelected = n;
			}

			return nSelected;
		}
		
		public class TreeViewUpdate : IDisposable
		{
			private TreeView TreeView;
	
			public TreeViewUpdate( TreeView TreeView )
			{
				this.TreeView = TreeView;
				TreeView.BeginUpdate();
			}
	
			public void Dispose()
			{
				TreeView.EndUpdate();
			}
		}

		
		void ContentTreeAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			Host.Feedback(e.Node.Text, this);
		}
		
		void BookMarkClick(object sender, System.EventArgs e)
		{
			if (contentTree != null && contentTree.SelectedNode != null)
			{
				Host.Feedback("BM#" + contentTree.SelectedNode.FullPath, this);
			}
		}
	}
}
