using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;

namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of PaliTranslator
	/// </summary>
	public class PaliTranslator : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Button TranslateButton;
		private System.Windows.Forms.RichTextBox TargetText;
		private System.Windows.Forms.RichTextBox SourceText;
		public PaliTranslator()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new PaliTranslator());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.SourceText = new System.Windows.Forms.RichTextBox();
			this.TargetText = new System.Windows.Forms.RichTextBox();
			this.TranslateButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// SourceText
			// 
			this.SourceText.Location = new System.Drawing.Point(8, 8);
			this.SourceText.Name = "SourceText";
			this.SourceText.Size = new System.Drawing.Size(312, 96);
			this.SourceText.TabIndex = 0;
			this.SourceText.Text = "";
			this.SourceText.TextChanged += new System.EventHandler(this.RichTextBox1TextChanged);
			// 
			// TargetText
			// 
			this.TargetText.Location = new System.Drawing.Point(8, 144);
			this.TargetText.Name = "TargetText";
			this.TargetText.Size = new System.Drawing.Size(312, 96);
			this.TargetText.TabIndex = 1;
			this.TargetText.Text = "";
			this.TargetText.TextChanged += new System.EventHandler(this.RichTextBox1TextChanged);
			// 
			// TranslateButton
			// 
			this.TranslateButton.Location = new System.Drawing.Point(112, 112);
			this.TranslateButton.Name = "TranslateButton";
			this.TranslateButton.TabIndex = 2;
			this.TranslateButton.Text = "Translate";
			this.TranslateButton.Click += new System.EventHandler(this.TranslateButtonClick);
			// 
			// PaliTranslator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(330, 248);
			this.Controls.Add(this.TranslateButton);
			this.Controls.Add(this.TargetText);
			this.Controls.Add(this.SourceText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PaliTranslator";
			this.Text = "Pali Translator ...";
			this.ResumeLayout(false);
		}
		#endregion
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "Pali Translator";
		string myDisplayName = "Translate";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Machine based translation plugin for converting pali to english";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\translation.png");
		
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
		
		private void ToolbarClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button.Text == DisplayName)
			{
				this.StartPosition = FormStartPosition.CenterParent;
				this.ShowDialog();
				this.Host.StatusBarText("Please enter your search string");
			}
		}


		void RichTextBox1TextChanged(object sender, System.EventArgs e)
		{
			
		}
		
		
		void TranslateButtonClick(object sender, System.EventArgs e)
		{
			TargetText.Text = doTranslation(SourceText.Text, "en");
		}
		
		private string doTranslation(string a, string b)
		{
			return "TODO - you are welcome to lend us a helping hand.";
		}
		
	}
}
