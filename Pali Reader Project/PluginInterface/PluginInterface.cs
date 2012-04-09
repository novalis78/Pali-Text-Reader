/*
 * Created by SharpDevelop.
 * User: novalis78
 * Date: 21.06.2005
 * Time: 22:15
 *  
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace PluginInterface
{
	public interface IPlugin
	{
		//supply an instance reference of the calling host
		IPluginHost Host {get;set;}
		
		//following fields are just informatory - telling
		//the host app about this plugin
		string PluginName {get;}
		string Description {get;}
		string Author {get;}
		string Version {get;}
		string DisplayName {get;}
		Image  PluginIcon {get;}
		
		//allow the host app to access the main form of the plugin
		System.Windows.Forms.Form MainInterface {get;}
		//allow the host app to supply any parameter via this method call
		//object type allows for sending any specific data type to the different
		//plugin's necessities
		void SetPluginParameter(Object o);
		Object GetPluginParameter(Object o);
		
		//well, self explanatory
		void Initialize();
		void Dispose();
	
	}
	
	public interface IPluginHost
	{
		
		void Feedback(object info, IPlugin Plugin);	
		//allow plugin to access host status bar
		void StatusBarText(string a);
		//
		System.Windows.Forms.ToolBar GetToolBarReference();
		System.Windows.Forms.MainMenu GetMainMenuReference();
		System.Windows.Forms.ImageList GetToolBarImageListReference();
		System.Windows.Forms.TabControl GetInfoPaneReference();
		void AddToolBarButton(string a, Image b);
		void SignalReadyBookExtraction(string a);
		//TODO change in something more general like "set/getHostParameter"
	}
}
