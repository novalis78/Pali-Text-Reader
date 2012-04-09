/*
 * Created by SharpDevelop.
 * User: novalis78
 * Date: 31.10.2004
 * Time: 18:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaliReader
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public class AboutBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel1;
		
		public AboutBox()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent;
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panel = new System.Windows.Forms.Panel();
			this.ackBtn = new System.Windows.Forms.Button();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel1.Location = new System.Drawing.Point(1, 314);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(152, 22);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Pali Text Reader Project Site";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(0, 283);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(262, 30);
			this.label4.TabIndex = 5;
			this.label4.Text = "© 2004-6 Lennart Lopin, Patrick Holloway, Alexandre Genaud, Frank Snow et al. Vis" +
			"it PTR online";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label2.Location = new System.Drawing.Point(0, 256);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(210, 19);
			this.label2.TabIndex = 3;
			this.label2.Text = "Version 0.1.1.50  \"Atthavahini\"";
			// 
			// panel
			// 
			this.panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel.BackgroundImage")));
			this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel.Controls.Add(this.label4);
			this.panel.Controls.Add(this.linkLabel1);
			this.panel.Controls.Add(this.label2);
			this.panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(294, 339);
			this.panel.TabIndex = 6;
			// 
			// ackBtn
			// 
			this.ackBtn.Location = new System.Drawing.Point(104, 345);
			this.ackBtn.Name = "ackBtn";
			this.ackBtn.Size = new System.Drawing.Size(75, 23);
			this.ackBtn.TabIndex = 7;
			this.ackBtn.Text = "OK";
			this.ackBtn.UseVisualStyleBackColor = true;
			this.ackBtn.Click += new System.EventHandler(this.AckBtnClick);
			// 
			// AboutBox
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(294, 376);
			this.Controls.Add(this.ackBtn);
			this.Controls.Add(this.panel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AboutBox";
			this.Text = "Pali Reader About ...";
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button ackBtn;
		private System.Windows.Forms.Panel panel;
		#endregion
		void LinkLabel1LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore"," http://www.nibbanam.com/PTReader/index.htm");
		}
		
		
		void AckBtnClick(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
