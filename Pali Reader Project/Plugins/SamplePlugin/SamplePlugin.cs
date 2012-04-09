using System;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;

//Plugin namespace
namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of SamplePlugin
	/// Every plugin derives from plugin interface definition
	/// IPlugin and windows forms
	/// Nota bene: In case you need to call other methods from the 
	/// main application than provided by the plugin interface,
	/// the plugin interface will have to be enlarged...
	/// </summary>
	public class SamplePlugin : System.Windows.Forms.Form, IPlugin
	{
		public SamplePlugin()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SamplePlugin());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			// 
			// SamplePlugin
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(330, 152);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SamplePlugin";
			this.Text = "SamplePlugin";
		}
		#endregion
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "SamplePlugin";
		string myDisplayName = "Sample";
		string myPluginAuthor = "Your name here";
		string myPluginDescription = "Your program description here";
		string myPluginVersion = "0.0.1";
		Image  myPluginIcon = Image.FromFile("Icons\\sample.png");
		
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
			if(e.Button.Text == DisplayName)
			{
				this.StartPosition = FormStartPosition.CenterParent;
				this.ShowDialog();
				this.Host.StatusBarText("SamplePlugin activated");
			}
		}
	}
}
