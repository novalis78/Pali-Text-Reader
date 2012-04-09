
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
using System.Web.Mail;
using System.Diagnostics;
using System.Windows.Forms;

namespace PaliReaderUtils
{
    /// <summary>
    /// Description of ReportBugs.
    /// </summary>
    public class ReportBugs : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;

        public ReportBugs()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
        }
        
        public ReportBugs(string exception)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            ErrorExceptionBox.Text = exception;
        }

        #region Windows Forms Designer generated code
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportBugs));
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.label3 = new System.Windows.Forms.Label();
        	this.label4 = new System.Windows.Forms.Label();
        	this.linkLabel1 = new System.Windows.Forms.LinkLabel();
        	this.linkLabel2 = new System.Windows.Forms.LinkLabel();
        	this.ErrorExceptionBox = new System.Windows.Forms.RichTextBox();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
        	this.pictureBox1.Location = new System.Drawing.Point(12, 9);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(48, 37);
        	this.pictureBox1.TabIndex = 2;
        	this.pictureBox1.TabStop = false;
        	// 
        	// label1
        	// 
        	this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label1.Location = new System.Drawing.Point(62, 15);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(232, 22);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "Send your feedback ...";
        	// 
        	// label2
        	// 
        	this.label2.Location = new System.Drawing.Point(5, 49);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(280, 37);
        	this.label2.TabIndex = 3;
        	this.label2.Text = " You can use one of the links below to report this error, in order to help us mak" +
        	"e this program successful:";
        	this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// label3
        	// 
        	this.label3.Location = new System.Drawing.Point(8, 95);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(89, 22);
        	this.label3.TabIndex = 7;
        	this.label3.Text = "Send an e-mail:";
        	// 
        	// label4
        	// 
        	this.label4.Location = new System.Drawing.Point(8, 119);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(89, 22);
        	this.label4.TabIndex = 8;
        	this.label4.Text = "Post a message:";
        	// 
        	// linkLabel1
        	// 
        	this.linkLabel1.AutoSize = true;
        	this.linkLabel1.Location = new System.Drawing.Point(104, 95);
        	this.linkLabel1.Name = "linkLabel1";
        	this.linkLabel1.Size = new System.Drawing.Size(51, 13);
        	this.linkLabel1.TabIndex = 9;
        	this.linkLabel1.TabStop = true;
        	this.linkLabel1.Text = "click here";
        	this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
        	// 
        	// linkLabel2
        	// 
        	this.linkLabel2.AutoSize = true;
        	this.linkLabel2.Location = new System.Drawing.Point(104, 119);
        	this.linkLabel2.Name = "linkLabel2";
        	this.linkLabel2.Size = new System.Drawing.Size(51, 13);
        	this.linkLabel2.TabIndex = 10;
        	this.linkLabel2.TabStop = true;
        	this.linkLabel2.Text = "click here";
        	this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
        	// 
        	// ErrorExceptionBox
        	// 
        	this.ErrorExceptionBox.Location = new System.Drawing.Point(8, 144);
        	this.ErrorExceptionBox.Name = "ErrorExceptionBox";
        	this.ErrorExceptionBox.Size = new System.Drawing.Size(345, 106);
        	this.ErrorExceptionBox.TabIndex = 11;
        	this.ErrorExceptionBox.Text = "";
        	// 
        	// ReportBugs
        	// 
        	this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
        	this.ClientSize = new System.Drawing.Size(365, 281);
        	this.Controls.Add(this.ErrorExceptionBox);
        	this.Controls.Add(this.linkLabel2);
        	this.Controls.Add(this.linkLabel1);
        	this.Controls.Add(this.label4);
        	this.Controls.Add(this.label3);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.pictureBox1);
        	this.Controls.Add(this.label1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        	this.Name = "ReportBugs";
        	this.Text = "Report this error!";
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.RichTextBox ErrorExceptionBox;
        #endregion
        void SendButtonClick(object sender, System.EventArgs e)
        {

        }

        private Label label4;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			Process.Start("Mailto:palireader@gmail.com?Subject=Bug Report&Body=" +  ErrorExceptionBox.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			Process.Start("IExplore","http://sourceforge.net/tracker/?group_id=142883&atid=753998");
        }
    }
}
