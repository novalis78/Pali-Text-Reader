using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;
using PaliReaderUtils;

//Plugin namespace
namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of AdjustFonts
	/// Every plugin derives from plugin interface definition
	/// IPlugin and windows forms
	/// Nota bene: In case you need to call other methods from the 
	/// main application than provided by the plugin interface,
	/// the plugin interface will have to be enlarged...
	/// </summary>
	public class AdjustFonts : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.OpenFileDialog openPictureLoad;
		private System.Windows.Forms.PictureBox backgroundPictureBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox FontBox;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox SizeBox;
		private ColorButton colorButton;
		private Color fontColor;
		private string styleSheet = "";
		private string styleSheetPath  = Directory.GetCurrentDirectory() + @"\Work\style.css";
		
		public AdjustFonts()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			try
			{
				Application.Run(new AdjustFonts());
			}
			catch(System.Exception ex)
			{
				ReportBugs rb = new ReportBugs(ex.ToString());
				rb.ShowDialog();
			}
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdjustFonts));
			this.SizeBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.FontBox = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboBoxBook = new System.Windows.Forms.ComboBox();
			this.comboBoxNikaya = new System.Windows.Forms.ComboBox();
			this.comboBoxNamo = new System.Windows.Forms.ComboBox();
			this.comboBoxGatha = new System.Windows.Forms.ComboBox();
			this.comboBoxText = new System.Windows.Forms.ComboBox();
			this.comboBoxSection = new System.Windows.Forms.ComboBox();
			this.comboBoxSutta = new System.Windows.Forms.ComboBox();
			this.comboNitthita = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.backgroundPictureBox2 = new System.Windows.Forms.PictureBox();
			this.openPictureLoad = new System.Windows.Forms.OpenFileDialog();
			colorButton = new ColorButton();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.backgroundPictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// SizeBox
			// 
			this.SizeBox.Items.AddRange(new object[] {
									"1 (8 pt)",
									"2 (10 pt)",
									"3 (12 pt)",
									"4 (14 pt)",
									"5 (18 pt)",
									"6 (24 pt)",
									"7 (32 pt)"});
			this.SizeBox.Location = new System.Drawing.Point(82, 34);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new System.Drawing.Size(69, 21);
			this.SizeBox.TabIndex = 3;
			this.SizeBox.Text = "Normal";
			this.SizeBox.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(184, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 12);
			this.label4.TabIndex = 17;
			this.label4.Text = "Font color:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(178, 15);
			this.label5.TabIndex = 18;
			this.label5.Text = "Individual paragraph font size:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Font:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 11);
			this.label2.TabIndex = 2;
			this.label2.Text = "Overall size:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 11);
			this.label3.TabIndex = 8;
			this.label3.Text = "Background:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(16, 56);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(304, 236);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// FontBox
			// 
			this.FontBox.Items.AddRange(new object[] {
									"Times Ext Roman",
									"Arial Unicode MS",
									"Indic Times",
									"Utopia Unicode IndoLatin",
									"Tahoma"});
			this.FontBox.Location = new System.Drawing.Point(89, 18);
			this.FontBox.Name = "FontBox";
			this.FontBox.Size = new System.Drawing.Size(200, 21);
			this.FontBox.TabIndex = 0;
			this.FontBox.Text = "Times Ext Roman";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.pictureBox1);
			this.groupBox2.Location = new System.Drawing.Point(8, 89);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(336, 319);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Page settings";
			// 
			// comboBoxBook
			// 
			this.comboBoxBook.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboBoxBook.Location = new System.Drawing.Point(183, 200);
			this.comboBoxBook.Name = "comboBoxBook";
			this.comboBoxBook.Size = new System.Drawing.Size(48, 21);
			this.comboBoxBook.TabIndex = 7;
			this.comboBoxBook.Text = "-";
			this.comboBoxBook.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboBoxNikaya
			// 
			this.comboBoxNikaya.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboBoxNikaya.Location = new System.Drawing.Point(183, 174);
			this.comboBoxNikaya.Name = "comboBoxNikaya";
			this.comboBoxNikaya.Size = new System.Drawing.Size(48, 21);
			this.comboBoxNikaya.TabIndex = 6;
			this.comboBoxNikaya.Text = "-";
			this.comboBoxNikaya.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboBoxNamo
			// 
			this.comboBoxNamo.Items.AddRange(new object[] {
									"-"});
			this.comboBoxNamo.Location = new System.Drawing.Point(183, 148);
			this.comboBoxNamo.Name = "comboBoxNamo";
			this.comboBoxNamo.Size = new System.Drawing.Size(48, 21);
			this.comboBoxNamo.TabIndex = 5;
			this.comboBoxNamo.Text = "-";
			this.comboBoxNamo.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboBoxGatha
			// 
			this.comboBoxGatha.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboBoxGatha.Location = new System.Drawing.Point(280, 310);
			this.comboBoxGatha.Name = "comboBoxGatha";
			this.comboBoxGatha.Size = new System.Drawing.Size(48, 21);
			this.comboBoxGatha.TabIndex = 14;
			this.comboBoxGatha.Text = "-";
			this.comboBoxGatha.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboBoxText
			// 
			this.comboBoxText.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboBoxText.Location = new System.Drawing.Point(280, 279);
			this.comboBoxText.Name = "comboBoxText";
			this.comboBoxText.Size = new System.Drawing.Size(48, 21);
			this.comboBoxText.TabIndex = 13;
			this.comboBoxText.Text = "-";
			this.comboBoxText.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboBoxSection
			// 
			this.comboBoxSection.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboBoxSection.Location = new System.Drawing.Point(279, 249);
			this.comboBoxSection.Name = "comboBoxSection";
			this.comboBoxSection.Size = new System.Drawing.Size(48, 21);
			this.comboBoxSection.TabIndex = 12;
			this.comboBoxSection.Text = "-";
			this.comboBoxSection.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboBoxSutta
			// 
			this.comboBoxSutta.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboBoxSutta.Location = new System.Drawing.Point(232, 223);
			this.comboBoxSutta.Name = "comboBoxSutta";
			this.comboBoxSutta.Size = new System.Drawing.Size(48, 21);
			this.comboBoxSutta.TabIndex = 11;
			this.comboBoxSutta.Text = "-";
			this.comboBoxSutta.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// comboNitthita
			// 
			this.comboNitthita.Items.AddRange(new object[] {
									"8",
									"9",
									"10",
									"11",
									"12",
									"14",
									"16",
									"18",
									"20",
									"22",
									"24",
									"26",
									"28"});
			this.comboNitthita.Location = new System.Drawing.Point(280, 342);
			this.comboNitthita.Name = "comboNitthita";
			this.comboNitthita.Size = new System.Drawing.Size(48, 21);
			this.comboNitthita.TabIndex = 15;
			this.comboNitthita.Text = "-";
			this.comboNitthita.SelectedIndexChanged += new System.EventHandler(this.SizeBoxSelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.SizeBox);
			this.groupBox1.Location = new System.Drawing.Point(8, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(336, 72);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Font settings";
			// 
			// backgroundPictureBox2
			// 
			this.backgroundPictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.backgroundPictureBox2.Location = new System.Drawing.Point(120, 104);
			this.backgroundPictureBox2.Name = "backgroundPictureBox2";
			this.backgroundPictureBox2.Size = new System.Drawing.Size(72, 17);
			this.backgroundPictureBox2.TabIndex = 9;
			this.backgroundPictureBox2.TabStop = false;
			this.backgroundPictureBox2.Click += new System.EventHandler(this.BackgroundPictureBoxClick);
			// 
			// button1
			// 
			colorButton.Size = new Size(50, 30);
			colorButton.Location = new Point(256, 41);			
			colorButton.Click += new EventHandler(ColorButton_Click);
			colorButton.BackColor = SystemColors.Control;				
			colorButton.CenterColor = Color.Black;
			colorButton.FlatStyle = FlatStyle.Popup;
			Controls.Add(colorButton);		
			// 
			// AdjustFonts
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(362, 416);
			this.Controls.Add(this.comboNitthita);
			this.Controls.Add(this.comboBoxGatha);
			this.Controls.Add(this.comboBoxText);
			this.Controls.Add(this.comboBoxSection);
			this.Controls.Add(this.comboBoxSutta);
			this.Controls.Add(this.backgroundPictureBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBoxBook);
			this.Controls.Add(this.comboBoxNikaya);
			this.Controls.Add(this.comboBoxNamo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.FontBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AdjustFonts";
			this.Text = "Font & Page settings ...";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.AdjustFontsClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.backgroundPictureBox2)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboNitthita;
		private System.Windows.Forms.ComboBox comboBoxBook;
		private System.Windows.Forms.ComboBox comboBoxNikaya;
		private System.Windows.Forms.ComboBox comboBoxNamo;
		private System.Windows.Forms.ComboBox comboBoxGatha;
		private System.Windows.Forms.ComboBox comboBoxText;
		private System.Windows.Forms.ComboBox comboBoxSection;
		private System.Windows.Forms.ComboBox comboBoxSutta;
		#endregion
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "AdjustFonts";
		string myDisplayName = "Font";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Changes general font settings of style.css";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\fonts.png");
		
		/// <summary>
		/// If you need to hand over parameters from the hosting application
		/// (Pali Text Reader) towards the plugin, this is the method to call.
		/// </summary>
		/// <param name="o"></param>
        public void SetPluginParameter(Object o)
		{
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
			System.Windows.Forms.ToolBar tb = this.Host.GetToolBarReference();
			if(tb != null) tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(ToolbarClick);
			this.Host.AddToolBarButton(DisplayName, PluginIcon);
		}

	
		
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
				return this;
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
		/// If user clicks on toolbar button
		/// the plugin's form will be activated
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolbarClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			try
			{
				if(e.Button.Text == DisplayName)
				{
					readCurrentStyleSheet();
					    //HACK done to rewrite image url on first load
						setImageProperty(getImageProperty());
					loadCurrentBackgroundImage();
					loadCurrentFontSizes();
					loadCurrentFontColor();
					this.StartPosition = FormStartPosition.CenterParent;
					this.ShowDialog();
					this.Host.StatusBarText("AdjustFonts activated");	
				}
			}
			catch(System.Exception ex)
			{
				ReportBugs rb = new ReportBugs(ex.ToString());
				rb.ShowDialog();
			}
		}
		void SizeBoxSelectedIndexChanged(object sender, System.EventArgs e)
		{
			//replace existing font size with newly selected font size
			//in our central style sheet file
			string size = ((ComboBox)sender).Text;
			size = size + "pt;";
			if(((ComboBox)sender).Name == this.comboBoxText.Name)
				resetFontStyleProperty("PF", size);
			else if(((ComboBox)sender).Name == this.comboBoxBook.Name)
				resetFontStyleProperty("BF", size);
			else if(((ComboBox)sender).Name == this.comboBoxNamo.Name)
				resetFontStyleProperty("NF", size);
			//trigger refresh of websites
			triggerPaliViewerRefresh();
		}
		
		void BackgroundPictureBoxClick(object sender, System.EventArgs e)
		{
			string curDir = Directory.GetCurrentDirectory();
			openPictureLoad.Filter = "Image Files (*.jpg) | *.jpg";
			openPictureLoad.InitialDirectory = @"c:\" ;
			if(DialogResult.OK == openPictureLoad.ShowDialog())
			{
				setImageProperty(openPictureLoad.FileName);
				triggerPaliViewerRefresh();
			}
			Directory.SetCurrentDirectory(curDir);
		}
		
		private void setNewBackgroundImage(string path)
		{
			try
			{
				int a = path.LastIndexOf("\\");
				int b = path.LastIndexOf(".");
				string c = path.Substring(a+1, b - (a+1));
				styleSheet = Regex.Replace(styleSheet, "background-image:url(.*)", "background-image:url(" + c + ".jpg)");
				if(!File.Exists(Directory.GetCurrentDirectory() + "\\" + c + ".jpg"))
					File.Copy(path, Directory.GetCurrentDirectory() + "\\" + c + ".jpg");
				this.backgroundPictureBox2.Image = System.Drawing.Image.FromFile(c + ".jpg");
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error " + ex.Message.ToString());
			}
		}
		
		private void loadCurrentBackgroundImage()
		{			
			string image = getImageProperty();
			backgroundPictureBox2.Image = System.Drawing.Image.FromFile(image);
		}
		
		private void loadCurrentFontSizes()
		{
			SizeBox.Text 			= getFontStyleProperty("BODYF");
			comboBoxText.Text 		= getFontStyleProperty("PF");
			comboBoxBook.Text 		= getFontStyleProperty("BF");
			comboBoxGatha.Text 		= getFontStyleProperty("GF");
			comboBoxNamo.Text 		= getFontStyleProperty("NF");
			comboBoxNikaya.Text 	= getFontStyleProperty("NIF");
			comboBoxSection.Text	= getFontStyleProperty("SF");
			comboBoxSutta.Text 		= getFontStyleProperty("SUF");
		}
		
		private void loadCurrentFontColor()
		{
			fontColor = getColorStyleProperty();
			colorButton.CenterColor = fontColor;
		}
			
		private void readCurrentStyleSheet()
		{
			try
			{
				styleSheet = "";
				if(File.Exists(styleSheetPath))
				{
					FileStream fs = new FileStream(styleSheetPath, FileMode.Open);
					StreamReader sr = new StreamReader(fs);
					string line = "";
					while((line = sr.ReadLine()) != null)
					{
						styleSheet += line + "\r\n";
					}
					
					fs.Close();
					sr.Close();	
				}
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message.ToString());
			}
		}
		
		private void writeNewStyleSheet()
		{
			try
			{
				if(styleSheet != null && styleSheet != "")
				{
						File.Delete(styleSheetPath);
						FileStream ffs = new FileStream(styleSheetPath, FileMode.Create);
						StreamWriter sw = new StreamWriter(ffs);
						sw.Write(styleSheet);
						sw.Close();
						ffs.Close();
				}
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message.ToString());
			}
		}
		
		private void triggerPaliViewerRefresh()
		{
			this.Host.Feedback((object)"refresh", this);
		}
		
		void AdjustFontsClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			writeNewStyleSheet();
		}
		
		
		private void resetFontStyleProperty(string tagname, string val)
		{
			int posA = -1;
			int posE = -1;
			int offset = "/*<>*/font-size:".Length + tagname.Length;
			if((posA = styleSheet.IndexOf( "/*<" + tagname + ">*/font-size:")) > -1)
			{
				posE = styleSheet.IndexOf("/*</" + tagname + ">*/");
				string sizeOld = styleSheet.Substring(posA + offset, posE - (posA + offset));
				styleSheet = styleSheet.Remove(posA + offset, sizeOld.Length);
				styleSheet = styleSheet.Insert(posA + offset, val.ToString());
			}
			writeNewStyleSheet();
		}
		
		private void setColorStyleProperty(Color c)
		{
			string tagname = "FCOL";
			int posA = -1;
			int posE = -1;
			int offset = "/*<>*/color:".Length + tagname.Length;
			if((posA = styleSheet.IndexOf( "/*<" + tagname + ">*/color:")) > -1)
			{
				posE = styleSheet.IndexOf("/*</" + tagname + ">*/");
				string colOld = styleSheet.Substring(posA + offset, posE - (posA + offset));
				styleSheet = styleSheet.Remove(posA + offset, colOld.Length);
				styleSheet = styleSheet.Insert(posA + offset, System.Drawing.ColorTranslator.ToHtml(c).ToString());
			}
			writeNewStyleSheet();
		}
		
		private string getFontStyleProperty(string tagname)
		{
			int posA = -1;
			int posE = -1;
			int offset = "/*<>*/font-size:".Length + tagname.Length;
			if((posA = styleSheet.IndexOf( "/*<" + tagname + ">*/font-size:")) > -1)
			{
				posE = styleSheet.IndexOf("/*</" + tagname + ">*/");
				string sizeOld = styleSheet.Substring(posA + offset, posE - (posA + offset));
				sizeOld = sizeOld.Replace("pt;", "");
				sizeOld =sizeOld.Trim();
				return sizeOld;
			}
			else return "";
		}
		
		private string getImageProperty()
		{
			string tagname = "BIMG";
			int posA = -1;
			int posE = -1;
			int offset = "/*<>*/background-image:url(".Length + tagname.Length;
			if((posA = styleSheet.IndexOf( "/*<" + tagname + ">*/background-image:url(")) > -1)
			{
				posE = styleSheet.IndexOf("/*</" + tagname + ">*/");
				string imgLocationOld = styleSheet.Substring(posA + offset, posE - (posA + offset));
				imgLocationOld = imgLocationOld.Replace(")", "");
				imgLocationOld = imgLocationOld.Replace(";", "");
				imgLocationOld = imgLocationOld.Replace("file:///", "");
				imgLocationOld = imgLocationOld.Trim();
				imgLocationOld = System.Web.HttpUtility.UrlDecode(imgLocationOld);
				return imgLocationOld;
			}
			else return "";
		}
		
		private void setImageProperty(string path)
		{
			if (path.Contains("<pathToImage>"))
					path = path.Replace("<pathToImage>", Directory.GetCurrentDirectory() + "/Icons/paper.jpg");
			path = path.Replace(" ", "%20");
			path = path.Replace(@"\", "/");
			path = "file:///" + path + ")";
			string tagname = "BIMG";
			int posA = -1;
			int posE = -1;
			int offset = "/*<>*/background-image:url(".Length + tagname.Length;
			if((posA = styleSheet.IndexOf( "/*<" + tagname + ">*/background-image:url(")) > -1)
			{
				posE = styleSheet.IndexOf("/*</" + tagname + ">*/");
				string imgLocationOld = styleSheet.Substring(posA + offset, posE - (posA + offset));
				imgLocationOld = imgLocationOld.Replace(")", "");
				imgLocationOld = imgLocationOld.Trim();
				styleSheet = styleSheet.Remove(posA + offset, imgLocationOld.Length);
				styleSheet = styleSheet.Insert(posA + offset, path);
				writeNewStyleSheet();
			}
			return;
		}
		
		private Color getColorStyleProperty()
		{
			string tagname = "FCOL";
			int posA = -1;
			int posE = -1;
			int offset = "/*<>*/color:".Length + tagname.Length;
			if((posA = styleSheet.IndexOf( "/*<" + tagname + ">*/color:")) > -1)
			{
				posE = styleSheet.IndexOf("/*</" + tagname + ">*/");
				string colOld = styleSheet.Substring(posA + offset, posE - (posA + offset));
				colOld =colOld.Trim();
				colOld = colOld.Replace(";", "");
				return System.Drawing.ColorTranslator.FromHtml(colOld);
			}
			else return Color.Black;
		}
		
		void ColorButton_Click(object sender, System.EventArgs e)
    	{       		
    		ColorButton callingButton = (ColorButton)sender;
    		Point p = new Point(callingButton.Left, callingButton.Top + callingButton.Height);
    		p = PointToScreen( p);
    		    		
    		ColorPaletteDialog clDlg = new ColorPaletteDialog(p.X, p.Y);

    		clDlg.ShowDialog();    	 
    		
    		if(clDlg.DialogResult == DialogResult.OK)
    		{
    			fontColor = clDlg.Color;
    			setColorStyleProperty(fontColor);
    			//MessageBox.Show(fontColor.ToString());
    		}
    		callingButton.CenterColor = fontColor;
    		triggerPaliViewerRefresh();
		}
		
	}
}
