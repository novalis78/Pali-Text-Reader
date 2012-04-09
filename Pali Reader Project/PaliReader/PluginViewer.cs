/*
 * Created by SharpDevelop.
 * User: novalis78
 * Date: 21.06.2005
 * Time: 21:05
 * 
 */

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace PaliReader
{
	/// <summary>
	/// Displays currently loaded and available Plugins.
	/// </summary>
	public class PluginViewer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox PluginInfoLabel;
		private System.Windows.Forms.Label PluginDescriptionLabel;
		private System.Windows.Forms.Label PluginVersionLabel;
		private System.Windows.Forms.Label PluginNameLabel;
		private System.Windows.Forms.CheckedListBox PluginList;
		private System.Windows.Forms.Label PluginAuthorLabel;
		private Hashtable availablePlugins;
		
		public PluginViewer()
		{
			InitializeComponent();
		}
		
		public PluginViewer(Hashtable a) : this()
		{
			availablePlugins = a;
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.PluginAuthorLabel = new System.Windows.Forms.Label();
			this.PluginList = new System.Windows.Forms.CheckedListBox();
			this.PluginNameLabel = new System.Windows.Forms.Label();
			this.PluginVersionLabel = new System.Windows.Forms.Label();
			this.PluginDescriptionLabel = new System.Windows.Forms.Label();
			this.PluginInfoLabel = new System.Windows.Forms.GroupBox();
			this.PluginInfoLabel.SuspendLayout();
			this.SuspendLayout();
			// 
			// PluginAuthorLabel
			// 
			this.PluginAuthorLabel.Location = new System.Drawing.Point(8, 56);
			this.PluginAuthorLabel.Name = "PluginAuthorLabel";
			this.PluginAuthorLabel.Size = new System.Drawing.Size(128, 16);
			this.PluginAuthorLabel.TabIndex = 3;
			this.PluginAuthorLabel.Text = "Author:";
			// 
			// PluginList
			// 
			this.PluginList.Location = new System.Drawing.Point(16, 16);
			this.PluginList.Name = "PluginList";
			this.PluginList.Size = new System.Drawing.Size(192, 148);
			this.PluginList.TabIndex = 0;
			this.PluginList.SelectedIndexChanged += new System.EventHandler(this.PluginListSelectedIndexChanged);
			// 
			// PluginNameLabel
			// 
			this.PluginNameLabel.Location = new System.Drawing.Point(8, 40);
			this.PluginNameLabel.Name = "PluginNameLabel";
			this.PluginNameLabel.Size = new System.Drawing.Size(136, 23);
			this.PluginNameLabel.TabIndex = 1;
			this.PluginNameLabel.Text = "Name:";
			// 
			// PluginVersionLabel
			// 
			this.PluginVersionLabel.Location = new System.Drawing.Point(8, 24);
			this.PluginVersionLabel.Name = "PluginVersionLabel";
			this.PluginVersionLabel.Size = new System.Drawing.Size(136, 23);
			this.PluginVersionLabel.TabIndex = 0;
			this.PluginVersionLabel.Text = "Version:";
			this.PluginVersionLabel.Click += new System.EventHandler(this.PluginVersionLabelClick);
			// 
			// PluginDescriptionLabel
			// 
			this.PluginDescriptionLabel.Location = new System.Drawing.Point(8, 72);
			this.PluginDescriptionLabel.Name = "PluginDescriptionLabel";
			this.PluginDescriptionLabel.Size = new System.Drawing.Size(136, 64);
			this.PluginDescriptionLabel.TabIndex = 2;
			this.PluginDescriptionLabel.Text = "Description:";
			// 
			// PluginInfoLabel
			// 
			this.PluginInfoLabel.Controls.Add(this.PluginAuthorLabel);
			this.PluginInfoLabel.Controls.Add(this.PluginNameLabel);
			this.PluginInfoLabel.Controls.Add(this.PluginVersionLabel);
			this.PluginInfoLabel.Controls.Add(this.PluginDescriptionLabel);
			this.PluginInfoLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.PluginInfoLabel.Location = new System.Drawing.Point(216, 16);
			this.PluginInfoLabel.Name = "PluginInfoLabel";
			this.PluginInfoLabel.Size = new System.Drawing.Size(152, 144);
			this.PluginInfoLabel.TabIndex = 1;
			this.PluginInfoLabel.TabStop = false;
			this.PluginInfoLabel.Text = "Plugin Info:";
			// 
			// PluginViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(386, 168);
			this.Controls.Add(this.PluginInfoLabel);
			this.Controls.Add(this.PluginList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PluginViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Extensions available ...";
			this.Load += new System.EventHandler(this.PluginViewerLoad);
			this.PluginInfoLabel.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion
		
		void PluginViewerLoad(object sender, System.EventArgs e)
		{
			if(availablePlugins.Count > 0)
			{
				try
				{
					IDictionaryEnumerator de = availablePlugins.GetEnumerator();
					while(de.MoveNext())
					{
						PluginList.Items.Add(de.Key, true);
					}
				}
				catch(System.Exception ex)
				{
					MessageBox.Show("Reading plugin list failed " + ex.ToString(), "Error");
				}
			}
		}
		
		void PluginVersionLabelClick(object sender, System.EventArgs e)
		{
			
		}
		
		void PluginListSelectedIndexChanged(object sender, System.EventArgs e)
		{
			int i = PluginList.SelectedIndex;
			string itemCaption;
			if(i != -1)
			{
				itemCaption = PluginList.Items[i].ToString();
				ArrayList a = (ArrayList)availablePlugins[itemCaption];
				PluginNameLabel.Text ="Name: " + itemCaption;
				PluginAuthorLabel.Text = "Author: " + a[0].ToString();
				PluginVersionLabel.Text =  "Version: " + a[1].ToString();
				PluginDescriptionLabel.Text =  a[2].ToString();
			}
		}
		
	}
}
