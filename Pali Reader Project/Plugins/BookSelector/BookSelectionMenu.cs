

using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using PluginInterface;
using System.Windows.Forms;
using PaliReaderUtils;
using ICSharpCode.SharpZipLib.Zip;

namespace PaliReaderPlugin
{
	/// <summary>
	/// BookSelector plugins display a menu to select a pali canon book
	/// It retrieves the book from whatever custom (compressed) directory
	/// Finally it copies the selected book into the temporary working directory
	/// And signals its task done to the main application which may, for instance,
	/// Invoke a text viewer
	/// </summary>
	public class BookSelectionMenu : System.Windows.Forms.Form, IPlugin
	{
		private System.Windows.Forms.Button CollapseButton;
		private System.Windows.Forms.RichTextBox labelDesc;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button ExpandButton;
		private System.Windows.Forms.Button OpenButton;
		private System.Windows.Forms.TreeView treeView1;
		private System.Resources.ResourceManager resources;
		private bool isOnlineMode = false;
		public delegate void BookReady();
		public event BookReady BookReadyEvent;
		

		private string selectedBook;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
		private string lastBook;
		
		public BookSelectionMenu()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			if(lastBook != "")
			{
				ExpandLastBook(treeView1.Nodes, lastBook);
			}
			
		}
		
		private void ToolbarClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button.Text == DisplayName)
			{
				this.ShowDialog();
				this.Host.StatusBarText("Please select a book to open");
			}
		}
		
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() 
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Samantapāsādikā)", 9, 9);
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("ṭīkā (Sāratthadīpanī I)", 9, 9);
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ṭīkā (Sāratthadīpanī II)", 9, 9);
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ṭīkā (Vajirabuddhi)", 9, 9);
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ṭīkā (Vimativinodanī)", 9, 9);
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Pārājika", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode1,
									treeNode2,
									treeNode3,
									treeNode4,
									treeNode5});
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Samantapāsādikā)", 9, 9);
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("ṭīkā (Sāratthadīpanī III)", 9, 9);
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("ṭīkā Pācittiyādiyojanā", 9, 9);
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Pācittiya", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode7,
									treeNode8,
									treeNode9});
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("ṭīkā (Vinayavinicchayo - uttaravinicchaya)", 9, 9);
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("ṭīkā (Kankhāvitaranī purana-abhinava)", 9, 9);
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("ṭīkā (Vinayavinicchayo)", 9, 9);
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Kankhāvitaranī (aṭṭhakathā)", 9, 9, new System.Windows.Forms.TreeNode[] {
									treeNode11,
									treeNode12,
									treeNode13});
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Pātimokkha", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode14});
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Suttavibhanga", new System.Windows.Forms.TreeNode[] {
									treeNode6,
									treeNode10,
									treeNode15});
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Samantapāsādikā)", 9, 9);
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("ṭīkā (Sāratthadīpanī III)", 9, 9);
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Mahāvagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode17,
									treeNode18});
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Samantapāsādikā)", 9, 9);
			System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("ṭīkā (Sāratthadīpanī III)", 9, 9);
			System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Cūḷavagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode20,
									treeNode21});
			System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Khandhaka", new System.Windows.Forms.TreeNode[] {
									treeNode19,
									treeNode22});
			System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Samantapāsādikā)", 9, 9);
			System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("ṭīkā (Sāratthadīpanī III)", 9, 9);
			System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Parivāra", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode24,
									treeNode25});
			System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Vinaya", 7, 7, new System.Windows.Forms.TreeNode[] {
									treeNode16,
									treeNode23,
									treeNode26});
			System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("ṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("abhinavaṭīkā I", 9, 9);
			System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("abhinavaṭīkā II", 9, 9);
			System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Sīlakkhandhavagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode28,
									treeNode29,
									treeNode30,
									treeNode31});
			System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("ṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Mahāvagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode33,
									treeNode34});
			System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("ṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Pāthikavagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode36,
									treeNode37});
			System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Dīgha Nikāya", new System.Windows.Forms.TreeNode[] {
									treeNode32,
									treeNode35,
									treeNode38});
			System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("ṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("Mūlapaṇṇāsa", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode40,
									treeNode41});
			System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("ṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("Majjhimapaṇṇāsa", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode43,
									treeNode44});
			System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("ṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("Uparipaṇṇāsa", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode46,
									treeNode47});
			System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("Majjhima Nikāya", new System.Windows.Forms.TreeNode[] {
									treeNode42,
									treeNode45,
									treeNode48});
			System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("aṭṭhakathā", 9, 9);
			System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("Sagāthāvagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode50,
									treeNode51});
			System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Nidānavagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode53,
									treeNode54});
			System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Khandhavagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode56,
									treeNode57});
			System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode61 = new System.Windows.Forms.TreeNode("Saḷāyatanavagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode59,
									treeNode60});
			System.Windows.Forms.TreeNode treeNode62 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode63 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode64 = new System.Windows.Forms.TreeNode("Mahāvagga", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode62,
									treeNode63});
			System.Windows.Forms.TreeNode treeNode65 = new System.Windows.Forms.TreeNode("Saṃyutta Nikāya", new System.Windows.Forms.TreeNode[] {
									treeNode52,
									treeNode55,
									treeNode58,
									treeNode61,
									treeNode64});
			System.Windows.Forms.TreeNode treeNode66 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode67 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode68 = new System.Windows.Forms.TreeNode("Ekanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode66,
									treeNode67});
			System.Windows.Forms.TreeNode treeNode69 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode70 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode71 = new System.Windows.Forms.TreeNode("Dukanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode69,
									treeNode70});
			System.Windows.Forms.TreeNode treeNode72 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode73 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode74 = new System.Windows.Forms.TreeNode("Tikanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode72,
									treeNode73});
			System.Windows.Forms.TreeNode treeNode75 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode76 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode77 = new System.Windows.Forms.TreeNode("Catukkanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode75,
									treeNode76});
			System.Windows.Forms.TreeNode treeNode78 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode79 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode80 = new System.Windows.Forms.TreeNode("Pañcakanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode78,
									treeNode79});
			System.Windows.Forms.TreeNode treeNode81 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode82 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode83 = new System.Windows.Forms.TreeNode("Chakkanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode81,
									treeNode82});
			System.Windows.Forms.TreeNode treeNode84 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode85 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode86 = new System.Windows.Forms.TreeNode("Sattakanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode84,
									treeNode85});
			System.Windows.Forms.TreeNode treeNode87 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode88 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode89 = new System.Windows.Forms.TreeNode("Aṭṭhakanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode87,
									treeNode88});
			System.Windows.Forms.TreeNode treeNode90 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode91 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode92 = new System.Windows.Forms.TreeNode("Navakanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode90,
									treeNode91});
			System.Windows.Forms.TreeNode treeNode93 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode94 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode95 = new System.Windows.Forms.TreeNode("Dasakanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode93,
									treeNode94});
			System.Windows.Forms.TreeNode treeNode96 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode97 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode98 = new System.Windows.Forms.TreeNode("Ekādasakanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode96,
									treeNode97});
			System.Windows.Forms.TreeNode treeNode99 = new System.Windows.Forms.TreeNode("Aṅguttara Nikāya", new System.Windows.Forms.TreeNode[] {
									treeNode68,
									treeNode71,
									treeNode74,
									treeNode77,
									treeNode80,
									treeNode83,
									treeNode86,
									treeNode89,
									treeNode92,
									treeNode95,
									treeNode98});
			System.Windows.Forms.TreeNode treeNode100 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode101 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode102 = new System.Windows.Forms.TreeNode("Khuddakapāṭha", new System.Windows.Forms.TreeNode[] {
									treeNode100,
									treeNode101});
			System.Windows.Forms.TreeNode treeNode103 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode104 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode105 = new System.Windows.Forms.TreeNode("Dhammapada", new System.Windows.Forms.TreeNode[] {
									treeNode103,
									treeNode104});
			System.Windows.Forms.TreeNode treeNode106 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode107 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode108 = new System.Windows.Forms.TreeNode("Udāna", new System.Windows.Forms.TreeNode[] {
									treeNode106,
									treeNode107});
			System.Windows.Forms.TreeNode treeNode109 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode110 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode111 = new System.Windows.Forms.TreeNode("Itivuttaka", new System.Windows.Forms.TreeNode[] {
									treeNode109,
									treeNode110});
			System.Windows.Forms.TreeNode treeNode112 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode113 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode114 = new System.Windows.Forms.TreeNode("Suttanipāta", new System.Windows.Forms.TreeNode[] {
									treeNode112,
									treeNode113});
			System.Windows.Forms.TreeNode treeNode115 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode116 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode117 = new System.Windows.Forms.TreeNode("Vimānavatthu", new System.Windows.Forms.TreeNode[] {
									treeNode115,
									treeNode116});
			System.Windows.Forms.TreeNode treeNode118 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode119 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode120 = new System.Windows.Forms.TreeNode("Petavatthu", new System.Windows.Forms.TreeNode[] {
									treeNode118,
									treeNode119});
			System.Windows.Forms.TreeNode treeNode121 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode122 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode123 = new System.Windows.Forms.TreeNode("Theragāthā", new System.Windows.Forms.TreeNode[] {
									treeNode121,
									treeNode122});
			System.Windows.Forms.TreeNode treeNode124 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode125 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode126 = new System.Windows.Forms.TreeNode("Therīgāthā", new System.Windows.Forms.TreeNode[] {
									treeNode124,
									treeNode125});
			System.Windows.Forms.TreeNode treeNode127 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode128 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode129 = new System.Windows.Forms.TreeNode("Apadāna I", new System.Windows.Forms.TreeNode[] {
									treeNode127,
									treeNode128});
			System.Windows.Forms.TreeNode treeNode130 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode131 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode132 = new System.Windows.Forms.TreeNode("Apadāna II", new System.Windows.Forms.TreeNode[] {
									treeNode130,
									treeNode131});
			System.Windows.Forms.TreeNode treeNode133 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode134 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode135 = new System.Windows.Forms.TreeNode("Buddhavaṃsa", new System.Windows.Forms.TreeNode[] {
									treeNode133,
									treeNode134});
			System.Windows.Forms.TreeNode treeNode136 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode137 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode138 = new System.Windows.Forms.TreeNode("Cariyāpiṭaka", new System.Windows.Forms.TreeNode[] {
									treeNode136,
									treeNode137});
			System.Windows.Forms.TreeNode treeNode139 = new System.Windows.Forms.TreeNode("aṭṭhakathā I");
			System.Windows.Forms.TreeNode treeNode140 = new System.Windows.Forms.TreeNode("aṭṭhakathā II");
			System.Windows.Forms.TreeNode treeNode141 = new System.Windows.Forms.TreeNode("aṭṭhakathā III");
			System.Windows.Forms.TreeNode treeNode142 = new System.Windows.Forms.TreeNode("aṭṭhakathā IV");
			System.Windows.Forms.TreeNode treeNode143 = new System.Windows.Forms.TreeNode("Jātaka I", new System.Windows.Forms.TreeNode[] {
									treeNode139,
									treeNode140,
									treeNode141,
									treeNode142});
			System.Windows.Forms.TreeNode treeNode144 = new System.Windows.Forms.TreeNode("aṭṭhakathā V");
			System.Windows.Forms.TreeNode treeNode145 = new System.Windows.Forms.TreeNode("aṭṭhakathā VI");
			System.Windows.Forms.TreeNode treeNode146 = new System.Windows.Forms.TreeNode("aṭṭhakathā VII");
			System.Windows.Forms.TreeNode treeNode147 = new System.Windows.Forms.TreeNode("Jātaka II", new System.Windows.Forms.TreeNode[] {
									treeNode144,
									treeNode145,
									treeNode146});
			System.Windows.Forms.TreeNode treeNode148 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode149 = new System.Windows.Forms.TreeNode("Mahāniddesa", new System.Windows.Forms.TreeNode[] {
									treeNode148});
			System.Windows.Forms.TreeNode treeNode150 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode151 = new System.Windows.Forms.TreeNode("Cūḷaniddesa", new System.Windows.Forms.TreeNode[] {
									treeNode150});
			System.Windows.Forms.TreeNode treeNode152 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode153 = new System.Windows.Forms.TreeNode("Paṭisambhidāmagga", new System.Windows.Forms.TreeNode[] {
									treeNode152});
			System.Windows.Forms.TreeNode treeNode154 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode155 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode156 = new System.Windows.Forms.TreeNode("Nettippakaraṇa", new System.Windows.Forms.TreeNode[] {
									treeNode154,
									treeNode155});
			System.Windows.Forms.TreeNode treeNode157 = new System.Windows.Forms.TreeNode("aṭṭhakathā");
			System.Windows.Forms.TreeNode treeNode158 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode159 = new System.Windows.Forms.TreeNode("Milindapañhā", new System.Windows.Forms.TreeNode[] {
									treeNode157,
									treeNode158});
			System.Windows.Forms.TreeNode treeNode160 = new System.Windows.Forms.TreeNode("Peṭakopadesa");
			System.Windows.Forms.TreeNode treeNode161 = new System.Windows.Forms.TreeNode("Khuddaka Nikāya", new System.Windows.Forms.TreeNode[] {
									treeNode102,
									treeNode105,
									treeNode108,
									treeNode111,
									treeNode114,
									treeNode117,
									treeNode120,
									treeNode123,
									treeNode126,
									treeNode129,
									treeNode132,
									treeNode135,
									treeNode138,
									treeNode143,
									treeNode147,
									treeNode149,
									treeNode151,
									treeNode153,
									treeNode156,
									treeNode159,
									treeNode160});
			System.Windows.Forms.TreeNode treeNode162 = new System.Windows.Forms.TreeNode("Sutta", 7, 7, new System.Windows.Forms.TreeNode[] {
									treeNode39,
									treeNode49,
									treeNode65,
									treeNode99,
									treeNode161});
			System.Windows.Forms.TreeNode treeNode163 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Aṭṭhasālinī)");
			System.Windows.Forms.TreeNode treeNode164 = new System.Windows.Forms.TreeNode("mūlaṭīkā");
			System.Windows.Forms.TreeNode treeNode165 = new System.Windows.Forms.TreeNode("anuṭīkā");
			System.Windows.Forms.TreeNode treeNode166 = new System.Windows.Forms.TreeNode("Dhammasaṅgaṇi", new System.Windows.Forms.TreeNode[] {
									treeNode163,
									treeNode164,
									treeNode165});
			System.Windows.Forms.TreeNode treeNode167 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Sammohavinodanī)");
			System.Windows.Forms.TreeNode treeNode168 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode169 = new System.Windows.Forms.TreeNode("Vibhaṅga", new System.Windows.Forms.TreeNode[] {
									treeNode167,
									treeNode168});
			System.Windows.Forms.TreeNode treeNode170 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode171 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode172 = new System.Windows.Forms.TreeNode("Dhātukathā", new System.Windows.Forms.TreeNode[] {
									treeNode170,
									treeNode171});
			System.Windows.Forms.TreeNode treeNode173 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode174 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode175 = new System.Windows.Forms.TreeNode("Puggalapaññatti", new System.Windows.Forms.TreeNode[] {
									treeNode173,
									treeNode174});
			System.Windows.Forms.TreeNode treeNode176 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode177 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode178 = new System.Windows.Forms.TreeNode("Kathāvatthu", new System.Windows.Forms.TreeNode[] {
									treeNode176,
									treeNode177});
			System.Windows.Forms.TreeNode treeNode179 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode180 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode181 = new System.Windows.Forms.TreeNode("Yamaka I", new System.Windows.Forms.TreeNode[] {
									treeNode179,
									treeNode180});
			System.Windows.Forms.TreeNode treeNode182 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode183 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode184 = new System.Windows.Forms.TreeNode("Yamaka II", new System.Windows.Forms.TreeNode[] {
									treeNode182,
									treeNode183});
			System.Windows.Forms.TreeNode treeNode185 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode186 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode187 = new System.Windows.Forms.TreeNode("Yamaka III", new System.Windows.Forms.TreeNode[] {
									treeNode185,
									treeNode186});
			System.Windows.Forms.TreeNode treeNode188 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode189 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode190 = new System.Windows.Forms.TreeNode("Paṭṭhāna I", new System.Windows.Forms.TreeNode[] {
									treeNode188,
									treeNode189});
			System.Windows.Forms.TreeNode treeNode191 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode192 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode193 = new System.Windows.Forms.TreeNode("Paṭṭhāna II", new System.Windows.Forms.TreeNode[] {
									treeNode191,
									treeNode192});
			System.Windows.Forms.TreeNode treeNode194 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode195 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode196 = new System.Windows.Forms.TreeNode("Paṭṭhāna III", new System.Windows.Forms.TreeNode[] {
									treeNode194,
									treeNode195});
			System.Windows.Forms.TreeNode treeNode197 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode198 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode199 = new System.Windows.Forms.TreeNode("Paṭṭhāna IV", new System.Windows.Forms.TreeNode[] {
									treeNode197,
									treeNode198});
			System.Windows.Forms.TreeNode treeNode200 = new System.Windows.Forms.TreeNode("aṭṭhakathā (Pañcapakaraṇa)");
			System.Windows.Forms.TreeNode treeNode201 = new System.Windows.Forms.TreeNode("ṭīkā");
			System.Windows.Forms.TreeNode treeNode202 = new System.Windows.Forms.TreeNode("Paṭṭhāna V", new System.Windows.Forms.TreeNode[] {
									treeNode200,
									treeNode201});
			System.Windows.Forms.TreeNode treeNode203 = new System.Windows.Forms.TreeNode("Abhidhamma", 7, 7, new System.Windows.Forms.TreeNode[] {
									treeNode166,
									treeNode169,
									treeNode172,
									treeNode175,
									treeNode178,
									treeNode181,
									treeNode184,
									treeNode187,
									treeNode190,
									treeNode193,
									treeNode196,
									treeNode199,
									treeNode202});
			System.Windows.Forms.TreeNode treeNode204 = new System.Windows.Forms.TreeNode("Tipiṭaka", 8, 8, new System.Windows.Forms.TreeNode[] {
									treeNode27,
									treeNode162,
									treeNode203});
			System.Windows.Forms.TreeNode treeNode205 = new System.Windows.Forms.TreeNode("mahāṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode206 = new System.Windows.Forms.TreeNode("Visuddhimagga-1", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode205});
			System.Windows.Forms.TreeNode treeNode207 = new System.Windows.Forms.TreeNode("mahāṭīkā", 9, 9);
			System.Windows.Forms.TreeNode treeNode208 = new System.Windows.Forms.TreeNode("Visuddhimagga-2", 1, 1, new System.Windows.Forms.TreeNode[] {
									treeNode207});
			System.Windows.Forms.TreeNode treeNode209 = new System.Windows.Forms.TreeNode("Visuddhimagga nidānakathā", 17, 17);
			System.Windows.Forms.TreeNode treeNode210 = new System.Windows.Forms.TreeNode("Visuddhimagga", 12, 12, new System.Windows.Forms.TreeNode[] {
									treeNode206,
									treeNode208,
									treeNode209});
			System.Windows.Forms.TreeNode treeNode211 = new System.Windows.Forms.TreeNode("Dighanikaya (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode212 = new System.Windows.Forms.TreeNode("Majjhimanikaya (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode213 = new System.Windows.Forms.TreeNode("Samyuttanikaya (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode214 = new System.Windows.Forms.TreeNode("Anguttaranikaya (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode215 = new System.Windows.Forms.TreeNode("Vinayapitaka (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode216 = new System.Windows.Forms.TreeNode("Abhidhammapitaka (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode217 = new System.Windows.Forms.TreeNode("Atthakatha (pu-vi)", 1, 1);
			System.Windows.Forms.TreeNode treeNode218 = new System.Windows.Forms.TreeNode("Sangayana-puccha vissajjana", 14, 14, new System.Windows.Forms.TreeNode[] {
									treeNode211,
									treeNode212,
									treeNode213,
									treeNode214,
									treeNode215,
									treeNode216,
									treeNode217});
			System.Windows.Forms.TreeNode treeNode219 = new System.Windows.Forms.TreeNode("Niruttidīpani", 1, 1);
			System.Windows.Forms.TreeNode treeNode220 = new System.Windows.Forms.TreeNode("Paramatthadīpani sangahamahāṭīkāpāṭha", 1, 1);
			System.Windows.Forms.TreeNode treeNode221 = new System.Windows.Forms.TreeNode("Anudīpanipāṭha", 1, 1);
			System.Windows.Forms.TreeNode treeNode222 = new System.Windows.Forms.TreeNode("Maggangadīpani", 1, 1);
			System.Windows.Forms.TreeNode treeNode223 = new System.Windows.Forms.TreeNode("Sammadiṭṭhidīpani", 1, 1);
			System.Windows.Forms.TreeNode treeNode224 = new System.Windows.Forms.TreeNode("Niyamadīpani", 1, 1);
			System.Windows.Forms.TreeNode treeNode225 = new System.Windows.Forms.TreeNode("Bodhipakkhiyadīpani", 1, 1);
			System.Windows.Forms.TreeNode treeNode226 = new System.Windows.Forms.TreeNode("Catusaccadīpani", 1, 1);
			System.Windows.Forms.TreeNode treeNode227 = new System.Windows.Forms.TreeNode("Paṭṭhānuddesadīpanipāṭha", 1, 1);
			System.Windows.Forms.TreeNode treeNode228 = new System.Windows.Forms.TreeNode("Ledī sayādo ganthasangaho", 11, 11, new System.Windows.Forms.TreeNode[] {
									treeNode219,
									treeNode220,
									treeNode221,
									treeNode222,
									treeNode223,
									treeNode224,
									treeNode225,
									treeNode226,
									treeNode227});
			System.Windows.Forms.TreeNode treeNode229 = new System.Windows.Forms.TreeNode("Namakkāraṭīkā", 1, 1);
			System.Windows.Forms.TreeNode treeNode230 = new System.Windows.Forms.TreeNode("Mahāpaṇāmapāṭha", 1, 1);
			System.Windows.Forms.TreeNode treeNode231 = new System.Windows.Forms.TreeNode("Lakkhaṇāto buddhathomanāgāthā", 1, 1);
			System.Windows.Forms.TreeNode treeNode232 = new System.Windows.Forms.TreeNode("Sutavandanā", 1, 1);
			System.Windows.Forms.TreeNode treeNode233 = new System.Windows.Forms.TreeNode("Kamalāñjali", 1, 1);
			System.Windows.Forms.TreeNode treeNode234 = new System.Windows.Forms.TreeNode("Jinālaṅkāra", 1, 1);
			System.Windows.Forms.TreeNode treeNode235 = new System.Windows.Forms.TreeNode("Pajjamadhu", 1, 1);
			System.Windows.Forms.TreeNode treeNode236 = new System.Windows.Forms.TreeNode("Buddhaguṇagāthāvalī", 1, 1);
			System.Windows.Forms.TreeNode treeNode237 = new System.Windows.Forms.TreeNode("Buddhavandanā ganthasangaho", 13, 13, new System.Windows.Forms.TreeNode[] {
									treeNode229,
									treeNode230,
									treeNode231,
									treeNode232,
									treeNode233,
									treeNode234,
									treeNode235,
									treeNode236});
			System.Windows.Forms.TreeNode treeNode238 = new System.Windows.Forms.TreeNode("Cūḷaganthavaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode239 = new System.Windows.Forms.TreeNode("Mahāvaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode240 = new System.Windows.Forms.TreeNode("Sāsanavaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode241 = new System.Windows.Forms.TreeNode("Vaṃsa-ganthasaṅgaho", 14, 14, new System.Windows.Forms.TreeNode[] {
									treeNode238,
									treeNode239,
									treeNode240});
			System.Windows.Forms.TreeNode treeNode242 = new System.Windows.Forms.TreeNode("Kaccayanabyakaranam");
			System.Windows.Forms.TreeNode treeNode243 = new System.Windows.Forms.TreeNode("Moggallanabyakaranam");
			System.Windows.Forms.TreeNode treeNode244 = new System.Windows.Forms.TreeNode("Saddanitippakaranam (padamala)");
			System.Windows.Forms.TreeNode treeNode245 = new System.Windows.Forms.TreeNode("Saddanitippakaranam (dhatumala)");
			System.Windows.Forms.TreeNode treeNode246 = new System.Windows.Forms.TreeNode("Padarupasiddhi");
			System.Windows.Forms.TreeNode treeNode247 = new System.Windows.Forms.TreeNode("Moggallanapancika");
			System.Windows.Forms.TreeNode treeNode248 = new System.Windows.Forms.TreeNode("Payogasiddhipatha");
			System.Windows.Forms.TreeNode treeNode249 = new System.Windows.Forms.TreeNode("Vuttodayapatha");
			System.Windows.Forms.TreeNode treeNode250 = new System.Windows.Forms.TreeNode("patha");
			System.Windows.Forms.TreeNode treeNode251 = new System.Windows.Forms.TreeNode("tika");
			System.Windows.Forms.TreeNode treeNode252 = new System.Windows.Forms.TreeNode("Abhidanappadipika", new System.Windows.Forms.TreeNode[] {
									treeNode250,
									treeNode251});
			System.Windows.Forms.TreeNode treeNode253 = new System.Windows.Forms.TreeNode("patha");
			System.Windows.Forms.TreeNode treeNode254 = new System.Windows.Forms.TreeNode("tika");
			System.Windows.Forms.TreeNode treeNode255 = new System.Windows.Forms.TreeNode("Subodhalankara", new System.Windows.Forms.TreeNode[] {
									treeNode253,
									treeNode254});
			System.Windows.Forms.TreeNode treeNode256 = new System.Windows.Forms.TreeNode("Balavatara ganthipadatthavinicchayasara");
			System.Windows.Forms.TreeNode treeNode257 = new System.Windows.Forms.TreeNode("Byakarana gantha sangaho", 16, 16, new System.Windows.Forms.TreeNode[] {
									treeNode242,
									treeNode243,
									treeNode244,
									treeNode245,
									treeNode246,
									treeNode247,
									treeNode248,
									treeNode249,
									treeNode252,
									treeNode255,
									treeNode256});
			System.Windows.Forms.TreeNode treeNode258 = new System.Windows.Forms.TreeNode("Lokaniti");
			System.Windows.Forms.TreeNode treeNode259 = new System.Windows.Forms.TreeNode("Suttantaniti");
			System.Windows.Forms.TreeNode treeNode260 = new System.Windows.Forms.TreeNode("Surassatiniti");
			System.Windows.Forms.TreeNode treeNode261 = new System.Windows.Forms.TreeNode("Maharahaniti");
			System.Windows.Forms.TreeNode treeNode262 = new System.Windows.Forms.TreeNode("Dhammaniti");
			System.Windows.Forms.TreeNode treeNode263 = new System.Windows.Forms.TreeNode("Kavidappananiti");
			System.Windows.Forms.TreeNode treeNode264 = new System.Windows.Forms.TreeNode("Nitimanjari");
			System.Windows.Forms.TreeNode treeNode265 = new System.Windows.Forms.TreeNode("Naradakkhadipani");
			System.Windows.Forms.TreeNode treeNode266 = new System.Windows.Forms.TreeNode("Catuarakkhadipani");
			System.Windows.Forms.TreeNode treeNode267 = new System.Windows.Forms.TreeNode("Canakyaniti");
			System.Windows.Forms.TreeNode treeNode268 = new System.Windows.Forms.TreeNode("Nitigantha sangaho", 12, 12, new System.Windows.Forms.TreeNode[] {
									treeNode258,
									treeNode259,
									treeNode260,
									treeNode261,
									treeNode262,
									treeNode263,
									treeNode264,
									treeNode265,
									treeNode266,
									treeNode267});
			System.Windows.Forms.TreeNode treeNode269 = new System.Windows.Forms.TreeNode("Rasavāhinī", 1, 1);
			System.Windows.Forms.TreeNode treeNode270 = new System.Windows.Forms.TreeNode("Simavisodhanipatha", 1, 1);
			System.Windows.Forms.TreeNode treeNode271 = new System.Windows.Forms.TreeNode("Vesantaragiti", 1, 1);
			System.Windows.Forms.TreeNode treeNode272 = new System.Windows.Forms.TreeNode("Vinayasaṅgaha-aṭṭhakathā", 1, 1);
			System.Windows.Forms.TreeNode treeNode273 = new System.Windows.Forms.TreeNode("Vinayālaṅkāra-ṭīkā", 1, 1);
			System.Windows.Forms.TreeNode treeNode274 = new System.Windows.Forms.TreeNode("Khuddasikkhā-mūlasikkhā", 1, 1);
			System.Windows.Forms.TreeNode treeNode275 = new System.Windows.Forms.TreeNode("Pakinnakagantha sangaho", 13, 13, new System.Windows.Forms.TreeNode[] {
									treeNode269,
									treeNode270,
									treeNode271,
									treeNode272,
									treeNode273,
									treeNode274});
			System.Windows.Forms.TreeNode treeNode276 = new System.Windows.Forms.TreeNode("Moggallana vuttivivaranapancika", 1, 1);
			System.Windows.Forms.TreeNode treeNode277 = new System.Windows.Forms.TreeNode("Thupavamsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode278 = new System.Windows.Forms.TreeNode("Dathavamsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode279 = new System.Windows.Forms.TreeNode("Dhatupathavilasiniya", 1, 1);
			System.Windows.Forms.TreeNode treeNode280 = new System.Windows.Forms.TreeNode("Dhatuvamsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode281 = new System.Windows.Forms.TreeNode("Hatthavanagallaviharavamsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode282 = new System.Windows.Forms.TreeNode("Jinacaritaya", 1, 1);
			System.Windows.Forms.TreeNode treeNode283 = new System.Windows.Forms.TreeNode("Jinavamsadipam", 1, 1);
			System.Windows.Forms.TreeNode treeNode284 = new System.Windows.Forms.TreeNode("Telakatahagatha", 1, 1);
			System.Windows.Forms.TreeNode treeNode285 = new System.Windows.Forms.TreeNode("Milidatika", 1, 1);
			System.Windows.Forms.TreeNode treeNode286 = new System.Windows.Forms.TreeNode("Padamanjari", 1, 1);
			System.Windows.Forms.TreeNode treeNode287 = new System.Windows.Forms.TreeNode("Padasadhanam", 1, 1);
			System.Windows.Forms.TreeNode treeNode288 = new System.Windows.Forms.TreeNode("Saddabindupakaranam", 1, 1);
			System.Windows.Forms.TreeNode treeNode289 = new System.Windows.Forms.TreeNode("Kaccayanadhatumanjusa", 1, 1);
			System.Windows.Forms.TreeNode treeNode290 = new System.Windows.Forms.TreeNode("Samantakutavannana", 1, 1);
			System.Windows.Forms.TreeNode treeNode291 = new System.Windows.Forms.TreeNode("Mahabodhivaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode292 = new System.Windows.Forms.TreeNode("Chakesadhatuvaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode293 = new System.Windows.Forms.TreeNode("Gandhavaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode294 = new System.Windows.Forms.TreeNode("Anagatavaṃsa", 1, 1);
			System.Windows.Forms.TreeNode treeNode295 = new System.Windows.Forms.TreeNode("Upāsakajanālaṅkara", 1, 1);
			System.Windows.Forms.TreeNode treeNode296 = new System.Windows.Forms.TreeNode("Sihalagantha sangaho", 15, 15, new System.Windows.Forms.TreeNode[] {
									treeNode276,
									treeNode277,
									treeNode278,
									treeNode279,
									treeNode280,
									treeNode281,
									treeNode282,
									treeNode283,
									treeNode284,
									treeNode285,
									treeNode286,
									treeNode287,
									treeNode288,
									treeNode289,
									treeNode290,
									treeNode291,
									treeNode292,
									treeNode293,
									treeNode294,
									treeNode295});
			System.Windows.Forms.TreeNode treeNode297 = new System.Windows.Forms.TreeNode("Post-Canonical", 6, 6, new System.Windows.Forms.TreeNode[] {
									treeNode210,
									treeNode218,
									treeNode228,
									treeNode237,
									treeNode241,
									treeNode257,
									treeNode268,
									treeNode275,
									treeNode296});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookSelectionMenu));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.OpenButton = new System.Windows.Forms.Button();
			this.ExpandButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelDesc = new System.Windows.Forms.RichTextBox();
			this.CloseButton = new System.Windows.Forms.Button();
			this.CollapseButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			treeNode1.ImageIndex = 9;
			treeNode1.Name = "";
			treeNode1.SelectedImageIndex = 9;
			treeNode1.Text = "aṭṭhakathā (Samantapāsādikā)";
			treeNode2.ImageIndex = 9;
			treeNode2.Name = "";
			treeNode2.SelectedImageIndex = 9;
			treeNode2.Text = "ṭīkā (Sāratthadīpanī I)";
			treeNode3.ImageIndex = 9;
			treeNode3.Name = "";
			treeNode3.SelectedImageIndex = 9;
			treeNode3.Text = "ṭīkā (Sāratthadīpanī II)";
			treeNode4.ImageIndex = 9;
			treeNode4.Name = "";
			treeNode4.SelectedImageIndex = 9;
			treeNode4.Text = "ṭīkā (Vajirabuddhi)";
			treeNode5.ImageIndex = 9;
			treeNode5.Name = "";
			treeNode5.SelectedImageIndex = 9;
			treeNode5.Text = "ṭīkā (Vimativinodanī)";
			treeNode6.ImageIndex = 1;
			treeNode6.Name = "";
			treeNode6.SelectedImageIndex = 1;
			treeNode6.Text = "Pārājika";
			treeNode7.ImageIndex = 9;
			treeNode7.Name = "";
			treeNode7.SelectedImageIndex = 9;
			treeNode7.Text = "aṭṭhakathā (Samantapāsādikā)";
			treeNode8.ImageIndex = 9;
			treeNode8.Name = "";
			treeNode8.SelectedImageIndex = 9;
			treeNode8.Text = "ṭīkā (Sāratthadīpanī III)";
			treeNode9.ImageIndex = 9;
			treeNode9.Name = "";
			treeNode9.SelectedImageIndex = 9;
			treeNode9.Text = "ṭīkā Pācittiyādiyojanā";
			treeNode10.ImageIndex = 1;
			treeNode10.Name = "";
			treeNode10.SelectedImageIndex = 1;
			treeNode10.Text = "Pācittiya";
			treeNode11.ImageIndex = 9;
			treeNode11.Name = "";
			treeNode11.SelectedImageIndex = 9;
			treeNode11.Text = "ṭīkā (Vinayavinicchayo - uttaravinicchaya)";
			treeNode12.ImageIndex = 9;
			treeNode12.Name = "";
			treeNode12.SelectedImageIndex = 9;
			treeNode12.Text = "ṭīkā (Kankhāvitaranī purana-abhinava)";
			treeNode13.ImageIndex = 9;
			treeNode13.Name = "";
			treeNode13.SelectedImageIndex = 9;
			treeNode13.Text = "ṭīkā (Vinayavinicchayo)";
			treeNode14.ImageIndex = 9;
			treeNode14.Name = "";
			treeNode14.SelectedImageIndex = 9;
			treeNode14.Text = "Kankhāvitaranī (aṭṭhakathā)";
			treeNode15.ImageIndex = 1;
			treeNode15.Name = "";
			treeNode15.SelectedImageIndex = 1;
			treeNode15.Text = "Pātimokkha";
			treeNode16.Name = "";
			treeNode16.Text = "Suttavibhanga";
			treeNode17.ImageIndex = 9;
			treeNode17.Name = "";
			treeNode17.SelectedImageIndex = 9;
			treeNode17.Text = "aṭṭhakathā (Samantapāsādikā)";
			treeNode18.ImageIndex = 9;
			treeNode18.Name = "";
			treeNode18.SelectedImageIndex = 9;
			treeNode18.Text = "ṭīkā (Sāratthadīpanī III)";
			treeNode19.ImageIndex = 1;
			treeNode19.Name = "";
			treeNode19.SelectedImageIndex = 1;
			treeNode19.Text = "Mahāvagga";
			treeNode20.ImageIndex = 9;
			treeNode20.Name = "";
			treeNode20.SelectedImageIndex = 9;
			treeNode20.Text = "aṭṭhakathā (Samantapāsādikā)";
			treeNode21.ImageIndex = 9;
			treeNode21.Name = "";
			treeNode21.SelectedImageIndex = 9;
			treeNode21.Text = "ṭīkā (Sāratthadīpanī III)";
			treeNode22.ImageIndex = 1;
			treeNode22.Name = "";
			treeNode22.SelectedImageIndex = 1;
			treeNode22.Text = "Cūḷavagga";
			treeNode23.Name = "";
			treeNode23.Text = "Khandhaka";
			treeNode24.ImageIndex = 9;
			treeNode24.Name = "";
			treeNode24.SelectedImageIndex = 9;
			treeNode24.Text = "aṭṭhakathā (Samantapāsādikā)";
			treeNode25.ImageIndex = 9;
			treeNode25.Name = "";
			treeNode25.SelectedImageIndex = 9;
			treeNode25.Text = "ṭīkā (Sāratthadīpanī III)";
			treeNode26.ImageIndex = 1;
			treeNode26.Name = "";
			treeNode26.SelectedImageIndex = 1;
			treeNode26.Text = "Parivāra";
			treeNode27.ImageIndex = 7;
			treeNode27.Name = "";
			treeNode27.SelectedImageIndex = 7;
			treeNode27.Text = "Vinaya";
			treeNode28.ImageIndex = 9;
			treeNode28.Name = "";
			treeNode28.SelectedImageIndex = 9;
			treeNode28.Text = "aṭṭhakathā";
			treeNode29.ImageIndex = 9;
			treeNode29.Name = "";
			treeNode29.SelectedImageIndex = 9;
			treeNode29.Text = "ṭīkā";
			treeNode30.ImageIndex = 9;
			treeNode30.Name = "";
			treeNode30.SelectedImageIndex = 9;
			treeNode30.Text = "abhinavaṭīkā I";
			treeNode31.ImageIndex = 9;
			treeNode31.Name = "";
			treeNode31.SelectedImageIndex = 9;
			treeNode31.Text = "abhinavaṭīkā II";
			treeNode32.ImageIndex = 1;
			treeNode32.Name = "";
			treeNode32.SelectedImageIndex = 1;
			treeNode32.Text = "Sīlakkhandhavagga";
			treeNode33.ImageIndex = 9;
			treeNode33.Name = "";
			treeNode33.SelectedImageIndex = 9;
			treeNode33.Text = "aṭṭhakathā";
			treeNode34.ImageIndex = 9;
			treeNode34.Name = "";
			treeNode34.SelectedImageIndex = 9;
			treeNode34.Text = "ṭīkā";
			treeNode35.ImageIndex = 1;
			treeNode35.Name = "";
			treeNode35.SelectedImageIndex = 1;
			treeNode35.Text = "Mahāvagga";
			treeNode36.ImageIndex = 9;
			treeNode36.Name = "";
			treeNode36.SelectedImageIndex = 9;
			treeNode36.Text = "aṭṭhakathā";
			treeNode37.ImageIndex = 9;
			treeNode37.Name = "";
			treeNode37.SelectedImageIndex = 9;
			treeNode37.Text = "ṭīkā";
			treeNode38.ImageIndex = 1;
			treeNode38.Name = "";
			treeNode38.SelectedImageIndex = 1;
			treeNode38.Text = "Pāthikavagga";
			treeNode39.Name = "";
			treeNode39.Text = "Dīgha Nikāya";
			treeNode40.ImageIndex = 9;
			treeNode40.Name = "";
			treeNode40.SelectedImageIndex = 9;
			treeNode40.Text = "aṭṭhakathā";
			treeNode41.ImageIndex = 9;
			treeNode41.Name = "";
			treeNode41.SelectedImageIndex = 9;
			treeNode41.Text = "ṭīkā";
			treeNode42.ImageIndex = 1;
			treeNode42.Name = "";
			treeNode42.SelectedImageIndex = 1;
			treeNode42.Text = "Mūlapaṇṇāsa";
			treeNode43.ImageIndex = 9;
			treeNode43.Name = "";
			treeNode43.SelectedImageIndex = 9;
			treeNode43.Text = "aṭṭhakathā";
			treeNode44.ImageIndex = 9;
			treeNode44.Name = "";
			treeNode44.SelectedImageIndex = 9;
			treeNode44.Text = "ṭīkā";
			treeNode45.ImageIndex = 1;
			treeNode45.Name = "";
			treeNode45.SelectedImageIndex = 1;
			treeNode45.Text = "Majjhimapaṇṇāsa";
			treeNode46.ImageIndex = 9;
			treeNode46.Name = "";
			treeNode46.SelectedImageIndex = 9;
			treeNode46.Text = "aṭṭhakathā";
			treeNode47.ImageIndex = 9;
			treeNode47.Name = "";
			treeNode47.SelectedImageIndex = 9;
			treeNode47.Text = "ṭīkā";
			treeNode48.ImageIndex = 1;
			treeNode48.Name = "";
			treeNode48.SelectedImageIndex = 1;
			treeNode48.Text = "Uparipaṇṇāsa";
			treeNode49.Name = "";
			treeNode49.Text = "Majjhima Nikāya";
			treeNode50.ImageIndex = 9;
			treeNode50.Name = "";
			treeNode50.SelectedImageIndex = 9;
			treeNode50.Text = "aṭṭhakathā";
			treeNode51.Name = "";
			treeNode51.Text = "ṭīkā";
			treeNode52.ImageIndex = 1;
			treeNode52.Name = "";
			treeNode52.SelectedImageIndex = 1;
			treeNode52.Text = "Sagāthāvagga";
			treeNode53.Name = "";
			treeNode53.Text = "aṭṭhakathā";
			treeNode54.Name = "";
			treeNode54.Text = "ṭīkā";
			treeNode55.ImageIndex = 1;
			treeNode55.Name = "";
			treeNode55.SelectedImageIndex = 1;
			treeNode55.Text = "Nidānavagga";
			treeNode56.Name = "";
			treeNode56.Text = "aṭṭhakathā";
			treeNode57.Name = "";
			treeNode57.Text = "ṭīkā";
			treeNode58.ImageIndex = 1;
			treeNode58.Name = "";
			treeNode58.SelectedImageIndex = 1;
			treeNode58.Text = "Khandhavagga";
			treeNode59.Name = "";
			treeNode59.Text = "aṭṭhakathā";
			treeNode60.Name = "";
			treeNode60.Text = "ṭīkā";
			treeNode61.ImageIndex = 1;
			treeNode61.Name = "";
			treeNode61.SelectedImageIndex = 1;
			treeNode61.Text = "Saḷāyatanavagga";
			treeNode62.Name = "";
			treeNode62.Text = "aṭṭhakathā";
			treeNode63.Name = "";
			treeNode63.Text = "ṭīkā";
			treeNode64.ImageIndex = 1;
			treeNode64.Name = "";
			treeNode64.SelectedImageIndex = 1;
			treeNode64.Text = "Mahāvagga";
			treeNode65.Name = "";
			treeNode65.Text = "Saṃyutta Nikāya";
			treeNode66.Name = "";
			treeNode66.Text = "aṭṭhakathā";
			treeNode67.Name = "";
			treeNode67.Text = "ṭīkā";
			treeNode68.Name = "";
			treeNode68.Text = "Ekanipāta";
			treeNode69.Name = "";
			treeNode69.Text = "aṭṭhakathā";
			treeNode70.Name = "";
			treeNode70.Text = "ṭīkā";
			treeNode71.Name = "";
			treeNode71.Text = "Dukanipāta";
			treeNode72.Name = "";
			treeNode72.Text = "aṭṭhakathā";
			treeNode73.Name = "";
			treeNode73.Text = "ṭīkā";
			treeNode74.Name = "";
			treeNode74.Text = "Tikanipāta";
			treeNode75.Name = "";
			treeNode75.Text = "aṭṭhakathā";
			treeNode76.Name = "";
			treeNode76.Text = "ṭīkā";
			treeNode77.Name = "";
			treeNode77.Text = "Catukkanipāta";
			treeNode78.Name = "";
			treeNode78.Text = "aṭṭhakathā";
			treeNode79.Name = "";
			treeNode79.Text = "ṭīkā";
			treeNode80.Name = "";
			treeNode80.Text = "Pañcakanipāta";
			treeNode81.Name = "";
			treeNode81.Text = "aṭṭhakathā";
			treeNode82.Name = "";
			treeNode82.Text = "ṭīkā";
			treeNode83.Name = "";
			treeNode83.Text = "Chakkanipāta";
			treeNode84.Name = "";
			treeNode84.Text = "aṭṭhakathā";
			treeNode85.Name = "";
			treeNode85.Text = "ṭīkā";
			treeNode86.Name = "";
			treeNode86.Text = "Sattakanipāta";
			treeNode87.Name = "";
			treeNode87.Text = "aṭṭhakathā";
			treeNode88.Name = "";
			treeNode88.Text = "ṭīkā";
			treeNode89.Name = "";
			treeNode89.Text = "Aṭṭhakanipāta";
			treeNode90.Name = "";
			treeNode90.Text = "aṭṭhakathā";
			treeNode91.Name = "";
			treeNode91.Text = "ṭīkā";
			treeNode92.Name = "";
			treeNode92.Text = "Navakanipāta";
			treeNode93.Name = "";
			treeNode93.Text = "aṭṭhakathā";
			treeNode94.Name = "";
			treeNode94.Text = "ṭīkā";
			treeNode95.Name = "";
			treeNode95.Text = "Dasakanipāta";
			treeNode96.Name = "";
			treeNode96.Text = "aṭṭhakathā";
			treeNode97.Name = "";
			treeNode97.Text = "ṭīkā";
			treeNode98.Name = "";
			treeNode98.Text = "Ekādasakanipāta";
			treeNode99.Name = "";
			treeNode99.Text = "Aṅguttara Nikāya";
			treeNode100.Name = "";
			treeNode100.Text = "aṭṭhakathā";
			treeNode101.Name = "";
			treeNode101.Text = "aṭṭhakathā";
			treeNode102.Name = "";
			treeNode102.Text = "Khuddakapāṭha";
			treeNode103.Name = "";
			treeNode103.Text = "aṭṭhakathā";
			treeNode104.Name = "";
			treeNode104.Text = "aṭṭhakathā";
			treeNode105.Name = "";
			treeNode105.Text = "Dhammapada";
			treeNode106.Name = "";
			treeNode106.Text = "aṭṭhakathā";
			treeNode107.Name = "";
			treeNode107.Text = "aṭṭhakathā";
			treeNode108.Name = "";
			treeNode108.Text = "Udāna";
			treeNode109.Name = "";
			treeNode109.Text = "aṭṭhakathā";
			treeNode110.Name = "";
			treeNode110.Text = "aṭṭhakathā";
			treeNode111.Name = "";
			treeNode111.Text = "Itivuttaka";
			treeNode112.Name = "";
			treeNode112.Text = "aṭṭhakathā";
			treeNode113.Name = "";
			treeNode113.Text = "aṭṭhakathā";
			treeNode114.Name = "";
			treeNode114.Text = "Suttanipāta";
			treeNode115.Name = "";
			treeNode115.Text = "aṭṭhakathā";
			treeNode116.Name = "";
			treeNode116.Text = "aṭṭhakathā";
			treeNode117.Name = "";
			treeNode117.Text = "Vimānavatthu";
			treeNode118.Name = "";
			treeNode118.Text = "aṭṭhakathā";
			treeNode119.Name = "";
			treeNode119.Text = "aṭṭhakathā";
			treeNode120.Name = "";
			treeNode120.Text = "Petavatthu";
			treeNode121.Name = "";
			treeNode121.Text = "aṭṭhakathā";
			treeNode122.Name = "";
			treeNode122.Text = "aṭṭhakathā";
			treeNode123.Name = "";
			treeNode123.Text = "Theragāthā";
			treeNode124.Name = "";
			treeNode124.Text = "aṭṭhakathā";
			treeNode125.Name = "";
			treeNode125.Text = "aṭṭhakathā";
			treeNode126.Name = "";
			treeNode126.Text = "Therīgāthā";
			treeNode127.Name = "";
			treeNode127.Text = "aṭṭhakathā";
			treeNode128.Name = "";
			treeNode128.Text = "aṭṭhakathā";
			treeNode129.Name = "";
			treeNode129.Text = "Apadāna I";
			treeNode130.Name = "";
			treeNode130.Text = "aṭṭhakathā";
			treeNode131.Name = "";
			treeNode131.Text = "aṭṭhakathā";
			treeNode132.Name = "";
			treeNode132.Text = "Apadāna II";
			treeNode133.Name = "";
			treeNode133.Text = "aṭṭhakathā";
			treeNode134.Name = "";
			treeNode134.Text = "aṭṭhakathā";
			treeNode135.Name = "";
			treeNode135.Text = "Buddhavaṃsa";
			treeNode136.Name = "";
			treeNode136.Text = "aṭṭhakathā";
			treeNode137.Name = "";
			treeNode137.Text = "aṭṭhakathā";
			treeNode138.Name = "";
			treeNode138.Text = "Cariyāpiṭaka";
			treeNode139.Name = "";
			treeNode139.Text = "aṭṭhakathā I";
			treeNode140.Name = "";
			treeNode140.Text = "aṭṭhakathā II";
			treeNode141.Name = "";
			treeNode141.Text = "aṭṭhakathā III";
			treeNode142.Name = "";
			treeNode142.Text = "aṭṭhakathā IV";
			treeNode143.Name = "";
			treeNode143.Text = "Jātaka I";
			treeNode144.Name = "";
			treeNode144.Text = "aṭṭhakathā V";
			treeNode145.Name = "";
			treeNode145.Text = "aṭṭhakathā VI";
			treeNode146.Name = "";
			treeNode146.Text = "aṭṭhakathā VII";
			treeNode147.Name = "";
			treeNode147.Text = "Jātaka II";
			treeNode148.Name = "";
			treeNode148.Text = "aṭṭhakathā";
			treeNode149.Name = "";
			treeNode149.Text = "Mahāniddesa";
			treeNode150.Name = "";
			treeNode150.Text = "aṭṭhakathā";
			treeNode151.Name = "";
			treeNode151.Text = "Cūḷaniddesa";
			treeNode152.Name = "";
			treeNode152.Text = "aṭṭhakathā";
			treeNode153.Name = "";
			treeNode153.Text = "Paṭisambhidāmagga";
			treeNode154.Name = "";
			treeNode154.Text = "aṭṭhakathā";
			treeNode155.Name = "";
			treeNode155.Text = "ṭīkā";
			treeNode156.Name = "";
			treeNode156.Text = "Nettippakaraṇa";
			treeNode157.Name = "Node0";
			treeNode157.Text = "aṭṭhakathā";
			treeNode158.Name = "Node1";
			treeNode158.Text = "ṭīkā";
			treeNode159.Name = "";
			treeNode159.Text = "Milindapañhā";
			treeNode160.Name = "";
			treeNode160.Text = "Peṭakopadesa";
			treeNode161.Name = "";
			treeNode161.Text = "Khuddaka Nikāya";
			treeNode162.ImageIndex = 7;
			treeNode162.Name = "";
			treeNode162.SelectedImageIndex = 7;
			treeNode162.Text = "Sutta";
			treeNode163.Name = "";
			treeNode163.Text = "aṭṭhakathā (Aṭṭhasālinī)";
			treeNode164.Name = "";
			treeNode164.Text = "mūlaṭīkā";
			treeNode165.Name = "";
			treeNode165.Text = "anuṭīkā";
			treeNode166.Name = "";
			treeNode166.Text = "Dhammasaṅgaṇi";
			treeNode167.Name = "";
			treeNode167.Text = "aṭṭhakathā (Sammohavinodanī)";
			treeNode168.Name = "";
			treeNode168.Text = "ṭīkā";
			treeNode169.Name = "";
			treeNode169.Text = "Vibhaṅga";
			treeNode170.Name = "";
			treeNode170.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode171.Name = "";
			treeNode171.Text = "ṭīkā";
			treeNode172.Name = "";
			treeNode172.Text = "Dhātukathā";
			treeNode173.Name = "";
			treeNode173.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode174.Name = "";
			treeNode174.Text = "ṭīkā";
			treeNode175.Name = "";
			treeNode175.Text = "Puggalapaññatti";
			treeNode176.Name = "";
			treeNode176.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode177.Name = "";
			treeNode177.Text = "ṭīkā";
			treeNode178.Name = "";
			treeNode178.Text = "Kathāvatthu";
			treeNode179.Name = "";
			treeNode179.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode180.Name = "";
			treeNode180.Text = "ṭīkā";
			treeNode181.Name = "";
			treeNode181.Text = "Yamaka I";
			treeNode182.Name = "";
			treeNode182.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode183.Name = "";
			treeNode183.Text = "ṭīkā";
			treeNode184.Name = "";
			treeNode184.Text = "Yamaka II";
			treeNode185.Name = "";
			treeNode185.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode186.Name = "";
			treeNode186.Text = "ṭīkā";
			treeNode187.Name = "";
			treeNode187.Text = "Yamaka III";
			treeNode188.Name = "";
			treeNode188.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode189.Name = "";
			treeNode189.Text = "ṭīkā";
			treeNode190.Name = "";
			treeNode190.Text = "Paṭṭhāna I";
			treeNode191.Name = "";
			treeNode191.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode192.Name = "";
			treeNode192.Text = "ṭīkā";
			treeNode193.Name = "";
			treeNode193.Text = "Paṭṭhāna II";
			treeNode194.Name = "";
			treeNode194.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode195.Name = "";
			treeNode195.Text = "ṭīkā";
			treeNode196.Name = "";
			treeNode196.Text = "Paṭṭhāna III";
			treeNode197.Name = "";
			treeNode197.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode198.Name = "";
			treeNode198.Text = "ṭīkā";
			treeNode199.Name = "";
			treeNode199.Text = "Paṭṭhāna IV";
			treeNode200.Name = "";
			treeNode200.Text = "aṭṭhakathā (Pañcapakaraṇa)";
			treeNode201.Name = "";
			treeNode201.Text = "ṭīkā";
			treeNode202.Name = "";
			treeNode202.Text = "Paṭṭhāna V";
			treeNode203.ImageIndex = 7;
			treeNode203.Name = "";
			treeNode203.SelectedImageIndex = 7;
			treeNode203.Text = "Abhidhamma";
			treeNode204.ImageIndex = 8;
			treeNode204.Name = "";
			treeNode204.SelectedImageIndex = 8;
			treeNode204.Text = "Tipiṭaka";
			treeNode205.ImageIndex = 9;
			treeNode205.Name = "";
			treeNode205.SelectedImageIndex = 9;
			treeNode205.Text = "mahāṭīkā";
			treeNode206.ImageIndex = 1;
			treeNode206.Name = "";
			treeNode206.SelectedImageIndex = 1;
			treeNode206.Text = "Visuddhimagga-1";
			treeNode207.ImageIndex = 9;
			treeNode207.Name = "";
			treeNode207.SelectedImageIndex = 9;
			treeNode207.Text = "mahāṭīkā";
			treeNode208.ImageIndex = 1;
			treeNode208.Name = "";
			treeNode208.SelectedImageIndex = 1;
			treeNode208.Text = "Visuddhimagga-2";
			treeNode209.ImageIndex = 17;
			treeNode209.Name = "";
			treeNode209.SelectedImageIndex = 17;
			treeNode209.Text = "Visuddhimagga nidānakathā";
			treeNode210.ImageIndex = 12;
			treeNode210.Name = "";
			treeNode210.SelectedImageIndex = 12;
			treeNode210.Text = "Visuddhimagga";
			treeNode211.ImageIndex = 1;
			treeNode211.Name = "";
			treeNode211.SelectedImageIndex = 1;
			treeNode211.Text = "Dighanikaya (pu-vi)";
			treeNode212.ImageIndex = 1;
			treeNode212.Name = "";
			treeNode212.SelectedImageIndex = 1;
			treeNode212.Text = "Majjhimanikaya (pu-vi)";
			treeNode213.ImageIndex = 1;
			treeNode213.Name = "";
			treeNode213.SelectedImageIndex = 1;
			treeNode213.Text = "Samyuttanikaya (pu-vi)";
			treeNode214.ImageIndex = 1;
			treeNode214.Name = "";
			treeNode214.SelectedImageIndex = 1;
			treeNode214.Text = "Anguttaranikaya (pu-vi)";
			treeNode215.ImageIndex = 1;
			treeNode215.Name = "";
			treeNode215.SelectedImageIndex = 1;
			treeNode215.Text = "Vinayapitaka (pu-vi)";
			treeNode216.ImageIndex = 1;
			treeNode216.Name = "";
			treeNode216.SelectedImageIndex = 1;
			treeNode216.Text = "Abhidhammapitaka (pu-vi)";
			treeNode217.ImageIndex = 1;
			treeNode217.Name = "";
			treeNode217.SelectedImageIndex = 1;
			treeNode217.Text = "Atthakatha (pu-vi)";
			treeNode218.ImageIndex = 14;
			treeNode218.Name = "";
			treeNode218.SelectedImageIndex = 14;
			treeNode218.Text = "Sangayana-puccha vissajjana";
			treeNode219.ImageIndex = 1;
			treeNode219.Name = "";
			treeNode219.SelectedImageIndex = 1;
			treeNode219.Text = "Niruttidīpani";
			treeNode220.ImageIndex = 1;
			treeNode220.Name = "";
			treeNode220.SelectedImageIndex = 1;
			treeNode220.Text = "Paramatthadīpani sangahamahāṭīkāpāṭha";
			treeNode221.ImageIndex = 1;
			treeNode221.Name = "";
			treeNode221.SelectedImageIndex = 1;
			treeNode221.Text = "Anudīpanipāṭha";
			treeNode222.ImageIndex = 1;
			treeNode222.Name = "";
			treeNode222.SelectedImageIndex = 1;
			treeNode222.Text = "Maggangadīpani";
			treeNode223.ImageIndex = 1;
			treeNode223.Name = "";
			treeNode223.SelectedImageIndex = 1;
			treeNode223.Text = "Sammadiṭṭhidīpani";
			treeNode224.ImageIndex = 1;
			treeNode224.Name = "";
			treeNode224.SelectedImageIndex = 1;
			treeNode224.Text = "Niyamadīpani";
			treeNode225.ImageIndex = 1;
			treeNode225.Name = "";
			treeNode225.SelectedImageIndex = 1;
			treeNode225.Text = "Bodhipakkhiyadīpani";
			treeNode226.ImageIndex = 1;
			treeNode226.Name = "";
			treeNode226.SelectedImageIndex = 1;
			treeNode226.Text = "Catusaccadīpani";
			treeNode227.ImageIndex = 1;
			treeNode227.Name = "";
			treeNode227.SelectedImageIndex = 1;
			treeNode227.Text = "Paṭṭhānuddesadīpanipāṭha";
			treeNode228.ImageIndex = 11;
			treeNode228.Name = "";
			treeNode228.SelectedImageIndex = 11;
			treeNode228.Text = "Ledī sayādo ganthasangaho";
			treeNode229.ImageIndex = 1;
			treeNode229.Name = "";
			treeNode229.SelectedImageIndex = 1;
			treeNode229.Text = "Namakkāraṭīkā";
			treeNode230.ImageIndex = 1;
			treeNode230.Name = "";
			treeNode230.SelectedImageIndex = 1;
			treeNode230.Text = "Mahāpaṇāmapāṭha";
			treeNode231.ImageIndex = 1;
			treeNode231.Name = "";
			treeNode231.SelectedImageIndex = 1;
			treeNode231.Text = "Lakkhaṇāto buddhathomanāgāthā";
			treeNode232.ImageIndex = 1;
			treeNode232.Name = "";
			treeNode232.SelectedImageIndex = 1;
			treeNode232.Text = "Sutavandanā";
			treeNode233.ImageIndex = 1;
			treeNode233.Name = "";
			treeNode233.SelectedImageIndex = 1;
			treeNode233.Text = "Kamalāñjali";
			treeNode234.ImageIndex = 1;
			treeNode234.Name = "";
			treeNode234.SelectedImageIndex = 1;
			treeNode234.Text = "Jinālaṅkāra";
			treeNode235.ImageIndex = 1;
			treeNode235.Name = "";
			treeNode235.SelectedImageIndex = 1;
			treeNode235.Text = "Pajjamadhu";
			treeNode236.ImageIndex = 1;
			treeNode236.Name = "";
			treeNode236.SelectedImageIndex = 1;
			treeNode236.Text = "Buddhaguṇagāthāvalī";
			treeNode237.ImageIndex = 13;
			treeNode237.Name = "";
			treeNode237.SelectedImageIndex = 13;
			treeNode237.Text = "Buddhavandanā ganthasangaho";
			treeNode238.ImageIndex = 1;
			treeNode238.Name = "";
			treeNode238.SelectedImageIndex = 1;
			treeNode238.Text = "Cūḷaganthavaṃsa";
			treeNode239.ImageIndex = 1;
			treeNode239.Name = "";
			treeNode239.SelectedImageIndex = 1;
			treeNode239.Text = "Mahāvaṃsa";
			treeNode240.ImageIndex = 1;
			treeNode240.Name = "";
			treeNode240.SelectedImageIndex = 1;
			treeNode240.Text = "Sāsanavaṃsa";
			treeNode241.ImageIndex = 14;
			treeNode241.Name = "";
			treeNode241.SelectedImageIndex = 14;
			treeNode241.Text = "Vaṃsa-ganthasaṅgaho";
			treeNode242.Name = "";
			treeNode242.Text = "Kaccayanabyakaranam";
			treeNode243.Name = "";
			treeNode243.Text = "Moggallanabyakaranam";
			treeNode244.Name = "";
			treeNode244.Text = "Saddanitippakaranam (padamala)";
			treeNode245.Name = "";
			treeNode245.Text = "Saddanitippakaranam (dhatumala)";
			treeNode246.Name = "";
			treeNode246.Text = "Padarupasiddhi";
			treeNode247.Name = "";
			treeNode247.Text = "Moggallanapancika";
			treeNode248.Name = "";
			treeNode248.Text = "Payogasiddhipatha";
			treeNode249.Name = "";
			treeNode249.Text = "Vuttodayapatha";
			treeNode250.Name = "";
			treeNode250.Text = "patha";
			treeNode251.Name = "";
			treeNode251.Text = "tika";
			treeNode252.Name = "";
			treeNode252.Text = "Abhidanappadipika";
			treeNode253.Name = "";
			treeNode253.Text = "patha";
			treeNode254.Name = "";
			treeNode254.Text = "tika";
			treeNode255.Name = "";
			treeNode255.Text = "Subodhalankara";
			treeNode256.Name = "";
			treeNode256.Text = "Balavatara ganthipadatthavinicchayasara";
			treeNode257.ImageIndex = 16;
			treeNode257.Name = "";
			treeNode257.SelectedImageIndex = 16;
			treeNode257.Text = "Byakarana gantha sangaho";
			treeNode258.Name = "";
			treeNode258.Text = "Lokaniti";
			treeNode259.Name = "";
			treeNode259.Text = "Suttantaniti";
			treeNode260.Name = "";
			treeNode260.Text = "Surassatiniti";
			treeNode261.Name = "";
			treeNode261.Text = "Maharahaniti";
			treeNode262.Name = "";
			treeNode262.Text = "Dhammaniti";
			treeNode263.Name = "";
			treeNode263.Text = "Kavidappananiti";
			treeNode264.Name = "";
			treeNode264.Text = "Nitimanjari";
			treeNode265.Name = "";
			treeNode265.Text = "Naradakkhadipani";
			treeNode266.Name = "";
			treeNode266.Text = "Catuarakkhadipani";
			treeNode267.Name = "";
			treeNode267.Text = "Canakyaniti";
			treeNode268.ImageIndex = 12;
			treeNode268.Name = "";
			treeNode268.SelectedImageIndex = 12;
			treeNode268.Text = "Nitigantha sangaho";
			treeNode269.ImageIndex = 1;
			treeNode269.Name = "";
			treeNode269.SelectedImageIndex = 1;
			treeNode269.Text = "Rasavāhinī";
			treeNode270.ImageIndex = 1;
			treeNode270.Name = "";
			treeNode270.SelectedImageIndex = 1;
			treeNode270.Text = "Simavisodhanipatha";
			treeNode271.ImageIndex = 1;
			treeNode271.Name = "";
			treeNode271.SelectedImageIndex = 1;
			treeNode271.Text = "Vesantaragiti";
			treeNode272.ImageIndex = 1;
			treeNode272.Name = "";
			treeNode272.SelectedImageIndex = 1;
			treeNode272.Text = "Vinayasaṅgaha-aṭṭhakathā";
			treeNode273.ImageIndex = 1;
			treeNode273.Name = "";
			treeNode273.SelectedImageIndex = 1;
			treeNode273.Text = "Vinayālaṅkāra-ṭīkā";
			treeNode274.ImageIndex = 1;
			treeNode274.Name = "";
			treeNode274.SelectedImageIndex = 1;
			treeNode274.Text = "Khuddasikkhā-mūlasikkhā";
			treeNode275.ImageIndex = 13;
			treeNode275.Name = "";
			treeNode275.SelectedImageIndex = 13;
			treeNode275.Text = "Pakinnakagantha sangaho";
			treeNode276.ImageIndex = 1;
			treeNode276.Name = "";
			treeNode276.SelectedImageIndex = 1;
			treeNode276.Text = "Moggallana vuttivivaranapancika";
			treeNode277.ImageIndex = 1;
			treeNode277.Name = "";
			treeNode277.SelectedImageIndex = 1;
			treeNode277.Text = "Thupavamsa";
			treeNode278.ImageIndex = 1;
			treeNode278.Name = "";
			treeNode278.SelectedImageIndex = 1;
			treeNode278.Text = "Dathavamsa";
			treeNode279.ImageIndex = 1;
			treeNode279.Name = "";
			treeNode279.SelectedImageIndex = 1;
			treeNode279.Text = "Dhatupathavilasiniya";
			treeNode280.ImageIndex = 1;
			treeNode280.Name = "";
			treeNode280.SelectedImageIndex = 1;
			treeNode280.Text = "Dhatuvamsa";
			treeNode281.ImageIndex = 1;
			treeNode281.Name = "";
			treeNode281.SelectedImageIndex = 1;
			treeNode281.Text = "Hatthavanagallaviharavamsa";
			treeNode282.ImageIndex = 1;
			treeNode282.Name = "";
			treeNode282.SelectedImageIndex = 1;
			treeNode282.Text = "Jinacaritaya";
			treeNode283.ImageIndex = 1;
			treeNode283.Name = "";
			treeNode283.SelectedImageIndex = 1;
			treeNode283.Text = "Jinavamsadipam";
			treeNode284.ImageIndex = 1;
			treeNode284.Name = "";
			treeNode284.SelectedImageIndex = 1;
			treeNode284.Text = "Telakatahagatha";
			treeNode285.ImageIndex = 1;
			treeNode285.Name = "";
			treeNode285.SelectedImageIndex = 1;
			treeNode285.Text = "Milidatika";
			treeNode286.ImageIndex = 1;
			treeNode286.Name = "";
			treeNode286.SelectedImageIndex = 1;
			treeNode286.Text = "Padamanjari";
			treeNode287.ImageIndex = 1;
			treeNode287.Name = "";
			treeNode287.SelectedImageIndex = 1;
			treeNode287.Text = "Padasadhanam";
			treeNode288.ImageIndex = 1;
			treeNode288.Name = "";
			treeNode288.SelectedImageIndex = 1;
			treeNode288.Text = "Saddabindupakaranam";
			treeNode289.ImageIndex = 1;
			treeNode289.Name = "";
			treeNode289.SelectedImageIndex = 1;
			treeNode289.Text = "Kaccayanadhatumanjusa";
			treeNode290.ImageIndex = 1;
			treeNode290.Name = "";
			treeNode290.SelectedImageIndex = 1;
			treeNode290.Text = "Samantakutavannana";
			treeNode291.ImageIndex = 1;
			treeNode291.Name = "Node0";
			treeNode291.SelectedImageIndex = 1;
			treeNode291.Text = "Mahabodhivaṃsa";
			treeNode292.ImageIndex = 1;
			treeNode292.Name = "Node1";
			treeNode292.SelectedImageIndex = 1;
			treeNode292.Text = "Chakesadhatuvaṃsa";
			treeNode293.ImageIndex = 1;
			treeNode293.Name = "Node2";
			treeNode293.SelectedImageIndex = 1;
			treeNode293.Text = "Gandhavaṃsa";
			treeNode294.ImageIndex = 1;
			treeNode294.Name = "Node3";
			treeNode294.SelectedImageIndex = 1;
			treeNode294.Text = "Anagatavaṃsa";
			treeNode295.ImageIndex = 1;
			treeNode295.Name = "Node4";
			treeNode295.SelectedImageIndex = 1;
			treeNode295.Text = "Upāsakajanālaṅkara";
			treeNode296.ImageIndex = 15;
			treeNode296.Name = "";
			treeNode296.SelectedImageIndex = 15;
			treeNode296.Text = "Sihalagantha sangaho";
			treeNode297.ImageIndex = 6;
			treeNode297.Name = "";
			treeNode297.SelectedImageIndex = 6;
			treeNode297.Text = "Post-Canonical";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
									treeNode204,
									treeNode297});
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new System.Drawing.Size(272, 332);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
			this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView1KeyDown);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.imageList1.Images.SetKeyName(4, "");
			this.imageList1.Images.SetKeyName(5, "");
			this.imageList1.Images.SetKeyName(6, "");
			this.imageList1.Images.SetKeyName(7, "");
			this.imageList1.Images.SetKeyName(8, "");
			this.imageList1.Images.SetKeyName(9, "");
			this.imageList1.Images.SetKeyName(10, "");
			this.imageList1.Images.SetKeyName(11, "");
			this.imageList1.Images.SetKeyName(12, "");
			this.imageList1.Images.SetKeyName(13, "");
			this.imageList1.Images.SetKeyName(14, "");
			this.imageList1.Images.SetKeyName(15, "");
			this.imageList1.Images.SetKeyName(16, "");
			this.imageList1.Images.SetKeyName(17, "");
			this.imageList1.Images.SetKeyName(18, "");
			// 
			// OpenButton
			// 
			this.OpenButton.Location = new System.Drawing.Point(0, 335);
			this.OpenButton.Name = "OpenButton";
			this.OpenButton.Size = new System.Drawing.Size(64, 25);
			this.OpenButton.TabIndex = 1;
			this.OpenButton.Text = "Open";
			this.OpenButton.Click += new System.EventHandler(this.OpenButtonClick);
			// 
			// ExpandButton
			// 
			this.ExpandButton.Location = new System.Drawing.Point(64, 335);
			this.ExpandButton.Name = "ExpandButton";
			this.ExpandButton.Size = new System.Drawing.Size(80, 25);
			this.ExpandButton.TabIndex = 4;
			this.ExpandButton.Text = "Expand all";
			this.ExpandButton.Click += new System.EventHandler(this.ExpandButtonClick);
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.labelDesc);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(274, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(272, 360);
			this.panel1.TabIndex = 6;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(274, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(272, 288);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// labelDesc
			// 
			this.labelDesc.BackColor = System.Drawing.Color.White;
			this.labelDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.labelDesc.Location = new System.Drawing.Point(48, 32);
			this.labelDesc.Name = "labelDesc";
			this.labelDesc.Size = new System.Drawing.Size(192, 237);
			this.labelDesc.TabIndex = 0;
			this.labelDesc.Text = "richTextBox1";
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(216, 335);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(56, 25);
			this.CloseButton.TabIndex = 2;
			this.CloseButton.Text = "Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
			// 
			// CollapseButton
			// 
			this.CollapseButton.Location = new System.Drawing.Point(144, 335);
			this.CollapseButton.Name = "CollapseButton";
			this.CollapseButton.Size = new System.Drawing.Size(72, 25);
			this.CollapseButton.TabIndex = 5;
			this.CollapseButton.Text = "Collapse";
			this.CollapseButton.Click += new System.EventHandler(this.CollapseButtonClick);
			// 
			// BookSelectionMenu
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(546, 360);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.CollapseButton);
			this.Controls.Add(this.ExpandButton);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.OpenButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BookSelectionMenu";
			this.Text = "Pali Canon Books ...";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BookSelectionMenuKeyDown);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
		void OpenButtonClick(object sender, System.EventArgs e)
		{
			TreeNode a = treeView1.SelectedNode;
			selectedBook = a.FullPath;
			if (selectedBook == "") return;
			string book = matchBookNameToCSCDFile(selectedBook);
			LoadBookFromLibrary(book);
			if(book == "")
				return;
			else
				this.Close();
		}
		
		void CloseButtonClick(object sender, System.EventArgs e)
		{
			this.Close();
			selectedBook = "";
		}
		
		public string matchBookNameToCSCDFile(string selectedBookPath)
		{
			lastBook = selectedBookPath;
			switch(selectedBookPath)
			{
					//Vinaya switch
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pārājika": return "VIN01M.MUL";		
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pārājika\aṭṭhakathā (Samantapāsādikā)": return "VIN01A.ATT";	
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pārājika\ṭīkā (Sāratthadīpanī I)"	   : return "VIN01T1.TIK";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pārājika\ṭīkā (Sāratthadīpanī II)"	   : return "VIN01T2.TIK";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pārājika\ṭīkā (Vajirabuddhi)"		   : return "VIN06T.NRF";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pārājika\ṭīkā (Vimativinodanī)"	   : return "VIN07T.NRF";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pācittiya"								: return "VIN02M1.MUL";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pācittiya\aṭṭhakathā (Samantapāsādikā)": return "VIN02A1.ATT";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pācittiya\ṭīkā (Sāratthadīpanī III)"      : return "VIN02T.TIK";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pācittiya\ṭīkā Pācittiyādiyojanā"     : return "VIN12T.NRF";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pātimokkha\Kankhāvitaranī (aṭṭhakathā)": return "VIN04T.NRF";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pātimokkha\Kankhāvitaranī (aṭṭhakathā)\ṭīkā (Vinayavinicchayo - uttaravinicchaya)": return "VIN11T.NRF";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pātimokkha\Kankhāvitaranī (aṭṭhakathā)\ṭīkā (Kankhāvitaranī purana-abhinava)": return "VIN09T.NRF";
				case @"Tipiṭaka\Vinaya\Suttavibhanga\Pātimokkha\Kankhāvitaranī (aṭṭhakathā)\ṭīkā (Vinayavinicchayo)": return "VIN10T.NRF";
				case @"Tipiṭaka\Vinaya\Khandhaka\Mahāvagga": return "VIN02M2.MUL";
				case @"Tipiṭaka\Vinaya\Khandhaka\Mahāvagga\aṭṭhakathā (Samantapāsādikā)": return "VIN02A2.ATT";
				case @"Tipiṭaka\Vinaya\Khandhaka\Mahāvagga\ṭīkā (Sāratthadīpanī III)": return "VIN02T.TIK";
				case @"Tipiṭaka\Vinaya\Khandhaka\Cūḷavagga": return "VIN02M3.MUL";
				case @"Tipiṭaka\Vinaya\Khandhaka\Cūḷavagga\aṭṭhakathā (Samantapāsādikā)": return "VIN02A3.ATT";
				case @"Tipiṭaka\Vinaya\Khandhaka\Cūḷavagga\ṭīkā (Sāratthadīpanī III)": return "VIN02T.TIK";
				case @"Tipiṭaka\Vinaya\Parivāra": return "VIN02M4.MUL";
				case @"Tipiṭaka\Vinaya\Parivāra\aṭṭhakathā (Samantapāsādikā)": return "VIN02A4.ATT";
				case @"Tipiṭaka\Vinaya\Parivāra\ṭīkā (Sāratthadīpanī III)": return "VIN02T.TIK";
				
					//Sutta switch
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Sīlakkhandhavagga"			: return "S0101M.MUL";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Sīlakkhandhavagga\aṭṭhakathā" : return "S0101A.ATT";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Sīlakkhandhavagga\ṭīkā"		 : return "S0101T.TIK";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Sīlakkhandhavagga\abhinavaṭīkā I": return "S0104N.NRF";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Sīlakkhandhavagga\abhinavaṭīkā II": return "S0105N.NRF";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Mahāvagga"						: 	return "S0102M.MUL";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Mahāvagga\aṭṭhakathā"			: 	return "S0102A.ATT";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Mahāvagga\ṭīkā"					: return "S0102T.TIK";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Pāthikavagga"					: return "S0103M.MUL";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Pāthikavagga\aṭṭhakathā"			: return "S0103A.ATT";
				case @"Tipiṭaka\Sutta\Dīgha Nikāya\Pāthikavagga\ṭīkā"				: 	return "S0103T.TIK";
				
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Mūlapaṇṇāsa"					: return "S0201M.MUL";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Mūlapaṇṇāsa\aṭṭhakathā"		: 	return "S0201A.ATT";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Mūlapaṇṇāsa\ṭīkā"				: 	return "S0201T.TIK";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Majjhimapaṇṇāsa"				: return "S0202M.MUL";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Majjhimapaṇṇāsa\aṭṭhakathā"	: return "S0202A.ATT";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Majjhimapaṇṇāsa\ṭīkā"			: return "S0202T.TIK";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Uparipaṇṇāsa"					: 	return  "S0203M.MUL";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Uparipaṇṇāsa\aṭṭhakathā"		: 	return  "S0203A.ATT";
				case @"Tipiṭaka\Sutta\Majjhima Nikāya\Uparipaṇṇāsa\ṭīkā"			: 	return  "S0203T.TIK";
				
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Sagāthāvagga": 	return  "S0301M.MUL";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Sagāthāvagga\aṭṭhakathā": 	return  "S0301A.ATT";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Sagāthāvagga\ṭīkā": 	return  "S0301T.TIK";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Nidānavagga": 	return  "S0302M.MUL";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Nidānavagga\aṭṭhakathā": 	return  "S0302A.ATT";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Nidānavagga\ṭīkā": 	return  "S0302T.TIK";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Khandhavagga": 	return  "S0303M.MUL";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Khandhavagga\aṭṭhakathā": 	return  "S0303A.ATT";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Khandhavagga\ṭīkā": 	return  "S0303T.TIK";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Saḷāyatanavagga": return "S0304M.MUL";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Saḷāyatanavagga\aṭṭhakathā": return "S0304A.ATT";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Saḷāyatanavagga\ṭīkā": return "S0304T.TIK";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Mahāvagga": 		return  "S0305M.MUL";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Mahāvagga\aṭṭhakathā": 		return  "S0305A.ATT";
				case @"Tipiṭaka\Sutta\Saṃyutta Nikāya\Mahāvagga\ṭīkā": 		return  "S0305T.TIK";
				
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Ekanipāta"					: 	return  "S0401M.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Ekanipāta\aṭṭhakathā"		: 	return  "S0401A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Ekanipāta\ṭīkā"				: 	return  "S0401T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Dukanipāta"					: 	return  "S0402M1.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Dukanipāta\aṭṭhakathā"			: 	return  "S0402A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Dukanipāta\ṭīkā"				: 	return  "S0402T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Tikanipāta"					: 	return  "S0402M2.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Tikanipāta\aṭṭhakathā"		: 	return  "S0402A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Tikanipāta\ṭīkā"				: 	return  "S0402T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Catukkanipāta"				: return  "S0402M3.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Catukkanipāta\aṭṭhakathā"		: return  "S0402A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Catukkanipāta\ṭīkā"			: return  "S0402T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Pañcakanipāta"				: 	return  "S0403M1.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Pañcakanipāta\aṭṭhakathā"	: 	return  "S0403A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Pañcakanipāta\ṭīkā"			: 	return  "S0403T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Chakkanipāta"				: 	return  "S0403M2.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Chakkanipāta\aṭṭhakathā"		: 	return  "S0403A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Chakkanipāta\ṭīkā"			: 	return  "S0403T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Sattakanipāta"				: return  "S0403M3.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Sattakanipāta\aṭṭhakathā"		: return  "S0403A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Sattakanipāta\ṭīkā"			: return  "S0403T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Aṭṭhakanipāta"				: 	return  "S0404M1.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Aṭṭhakanipāta\aṭṭhakathā"		: 	return  "S0404A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Aṭṭhakanipāta\ṭīkā"			: 	return  "S0404T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Navakanipāta"				: 	return  "S0404M2.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Navakanipāta\aṭṭhakathā"		: 	return  "S0404A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Navakanipāta\ṭīkā"			: 	return  "S0404T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Dasakanipāta"				: 	return  "S0404M3.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Dasakanipāta\aṭṭhakathā"		: 	return  "S0404A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Dasakanipāta\ṭīkā"			: 	return  "S0404T.TIK";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Ekādasakanipāta"				: 	return  "S0404M4.MUL";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Ekādasakanipāta\aṭṭhakathā"	: 	return  "S0404A.ATT";
				case @"Tipiṭaka\Sutta\Aṅguttara Nikāya\Ekādasakanipāta\ṭīkā"		: 	return  "S0404T.TIK";
				
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Khuddakapāṭha"	:  return  "S0501M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Khuddakapāṭha\aṭṭhakathā"	:  return  "S0501A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Dhammapada"	:  return  "S0502M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Dhammapada\aṭṭhakathā"	:  return  "S0502A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Udāna":  return  "S0503M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Udāna\aṭṭhakathā":  return  "S0503A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Itivuttaka":  return  "S0504M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Itivuttaka\aṭṭhakathā":  return  "S0504A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Suttanipāta":  return  "S0505M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Suttanipāta\aṭṭhakathā":  return  "S0505A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Vimānavatthu":  return  "S0506M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Vimānavatthu\aṭṭhakathā":  return  "S0506A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Petavatthu":  return  "S0507M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Petavatthu\aṭṭhakathā":  return  "S0507A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Theragāthā":  return  "S0508M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Theragāthā\aṭṭhakathā":  return  "S0508A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Therīgāthā":  return  "S0509M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Therīgāthā\aṭṭhakathā":  return  "S0509A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Apadāna I":  return  "S0510M1.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Apadāna I\aṭṭhakathā":  return  "S0510A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Apadāna II":  return  "S0510M2.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Apadāna II\aṭṭhakathā":  return  "S0510A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Buddhavamsa":  return  "S0511M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Buddhavamsa\aṭṭhakathā":  return  "S0511A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Cariyapitaka":  return  "S0512M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Cariyapitaka\aṭṭhakathā":  return  "S0512A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka I":  		return  "S0513M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka I\aṭṭhakathā I":  return "S0513A1.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka I\aṭṭhakathā II":  return "S0513A2.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka I\aṭṭhakathā III":  return "S0513A3.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka I\aṭṭhakathā IV":  return "S0513A4.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka II":  return "S0514M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka II\aṭṭhakathā V":  return "S0514A1.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka II\aṭṭhakathā VI":  return "S0514A1.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Jātaka II\aṭṭhakathā VII":  return "S0514A1.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Mahāniddesa":  return  "S0515M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Mahāniddesa\aṭṭhakathā":  return  "S0515A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Cūḷaniddesa":  return  "S0516M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Cūḷaniddesa\aṭṭhakathā":  return  "S0516A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Paṭisambhidāmagga":  return  "S0517M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Paṭisambhidāmagga\aṭṭhakathā":  return  "S0517A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Nettippakaraṇa":  return  "S0519M.MUL";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Nettippakaraṇa\aṭṭhakathā":  return  "S0519A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Nettippakaraṇa\ṭīkā":  return  "S0519T.TIK";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Milindapañhā":  return  "S0518M.NRF";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Milindapañhā\aṭṭhakathā":  return  "S0518A.ATT";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Milindapañhā\ṭīkā":  return  "S0518T.TIK";
				case @"Tipiṭaka\Sutta\Khuddaka Nikāya\Peṭakopadesa":  return  "S0520M.NRF";
				
					//Abhidhamma switch
				case @"Tipiṭaka\Abhidhamma\Dhammasaṅgaṇi":  return  "ABH01M.MUL";
				case @"Tipiṭaka\Abhidhamma\Dhammasaṅgaṇi\aṭṭhakathā (Aṭṭhasālinī)":  return  "ABH01A.ATT";
				case @"Tipiṭaka\Abhidhamma\Dhammasaṅgaṇi\mūlaṭīkā":  return  "ABH01T.TIK";
				case @"Tipiṭaka\Abhidhamma\Dhammasaṅgaṇi\anuṭīkā":  return  "ABH04T.NRF";
				case @"Tipiṭaka\Abhidhamma\Vibhaṅga":  return  "ABH02M.MUL";
				case @"Tipiṭaka\Abhidhamma\Vibhaṅga\aṭṭhakathā":  return  "ABH02A.ATT";
				case @"Tipiṭaka\Abhidhamma\Vibhaṅga\ṭīkā":  return  "ABH02T.TIK";
				case @"Tipiṭaka\Abhidhamma\Dhātukathā":  return  "ABH03M1.MUL";
				case @"Tipiṭaka\Abhidhamma\Dhātukathā\aṭṭhakathā (Pañcapakaraṇa)":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Dhātukathā\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Dhātukathā\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Puggalapaññatti":  return  "ABH03M2.MUL";
				case @"Tipiṭaka\Abhidhamma\Puggalapaññatti\aṭṭhakathā (Pañcapakaraṇa)":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Puggalapaññatti\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Puggalapaññatti\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Kathāvatthu":  return  "ABH03M3.MUL";
				case @"Tipiṭaka\Abhidhamma\Kathāvatthu\aṭṭhakathā (Pañcapakaraṇa)":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Kathāvatthu\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Kathāvatthu\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Yamaka I":  return  "ABH03M4.MUL";
				case @"Tipiṭaka\Abhidhamma\Yamaka I\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Yamaka I\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Yamaka I\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Yamaka II":  return  "ABH03M5.MUL";
				case @"Tipiṭaka\Abhidhamma\Yamaka II\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Yamaka II\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Yamaka II\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Yamaka III":  return  "ABH03M6.MUL";
				case @"Tipiṭaka\Abhidhamma\Yamaka III\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Yamaka III\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Yamaka III\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna I":  return  "ABH03M7.MUL";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna I\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna I\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna I\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna II":  return  "ABH03M8.MUL";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna II\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna II\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna II\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna III":  return  "ABH03M9.MUL";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna III\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna III\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna III\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna IV":  return  "ABH03M10.MUL";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna IV\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna IV\mūlaṭīkā":  return  "ABH03T.TIK";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna IV\anuṭīkā":  return  "ABH05T.NRF";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna V":  return  "ABH03M11.MUL";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna V\aṭṭhakathā":  return  "ABH03T.ATT";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna V\mūlaṭīkā":  return  "ABH03T.NRF";
				case @"Tipiṭaka\Abhidhamma\Paṭṭhāna V\anuṭīkā":  return  "ABH05T.NRF";
				
				//Post-Canonical switch
				case @"Post-Canonical\Visuddhimagga\Visuddhimagga-1":  return  "E0101N.MUL";
				case @"Post-Canonical\Visuddhimagga\Visuddhimagga-1\mahāṭīkā":  return  "E0103N.ATT";
				case @"Post-Canonical\Visuddhimagga\Visuddhimagga-2":  return  "E0102N.MUL";
				case @"Post-Canonical\Visuddhimagga\Visuddhimagga-2\mahāṭīkā":  return  "E0104N.ATT";
				case @"Post-Canonical\Visuddhimagga\Visuddhimagga\nidānakathā":  return  "E0105N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Dighanikaya (pu-vi)":  return  "E0901N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Majjhimanikaya (pu-vi)":  return  "E0902N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Samyuttanikaya (pu-vi)":  return  "E0903N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Anguttaranikaya (pu-vi)":  return  "E0904N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Vinayapitaka (pu-vi)":  return  "E0905N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Abhidhammapitaka (pu-vi)":  return  "E0906N.NRF";
				case @"Post-Canonical\Sangayana-puccha vissajjana\Atthakatha (pu-vi)":  return  "E0907N.NRF";
				
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Namakkāraṭīkā": return "E0601N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Mahāpaṇāmapāṭha": return "E0602N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Lakkhaṇāto buddhathomanāgāthā": return "E0603N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Sutavandanā": return "E0604N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Kamalāñjali": return "E0605N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Jinālaṅkāra": return "E0606N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Pajjamadhu": return "E0607N.NRF";
				case @"Post-Canonical\Buddhavandanā ganthasangaho\Buddhaguṇagāthāvalī": return "E0608N.NRF";
				
				case @"Post-Canonical\Vaṃsa-ganthasaṅgaho\Cūḷaganthavaṃsa": return "E0701N.NRF";
				case @"Post-Canonical\Vaṃsa-ganthasaṅgaho\Mahāvaṃsa": return "E0703N.NRF";
				case @"Post-Canonical\Vaṃsa-ganthasaṅgaho\Sāsanavaṃsa": return "E0702N.NRF";
				
				case @"Post-Canonical\Pakinnakagantha sangaho\Rasavāhinī": return "E1101N.NRF";
				
				
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Niruttidīpani": return "E0201N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Paramatthadīpani sangahamahāṭīkāpāṭha": return "E0301N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Anudīpanipāṭha": return "E0401N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Maggangadīpani": return "E0402N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Sammadiṭṭhidīpani": return "E0403N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Niyamadīpani": return "E0404N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Bodhipakkhiyadīpani": return "E0405N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Catusaccadīpani": return "E0406N.NRF";
				case @"Post-Canonical\Ledī sayādo ganthasangaho\Paṭṭhānuddesadīpanipāṭha": return "E0407N.NRF";
				
				case @"Post-Canonical\Sihalagantha sangaho\Moggallana vuttivivaranapancika":  return  "E1201N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Thupavamsa":  return  "E1202N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Dathavamsa":  return  "E1203N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Dhatupathavilasiniya":  return  "E1204N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Dhatuvamsa":  return  "E1205N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Hatthavanagallaviharavamsa":  return  "E1206N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Jinacaritaya":  return  "E1207N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Jinavamsadipam":  return  "E1208N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Telakatahagatha":  return  "E1209N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Milidatika":  return  "E1210N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Padamanjari":  return  "E1211N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Padasadhanam":  return  "E1212N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Saddabindupakaranam":  return  "E1213N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Kaccayanadhatumanjusa":  return  "E1214N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Samantakutavannana":  return  "E1215N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Upāsakajanālaṅkara":  return  "E1216N.NRF";	
				case @"Post-Canonical\Sihalagantha sangaho\Anagatavaṃsa":  return  "E1217N.NRF";
				case @"Post-Canonical\Sihalagantha sangaho\Gandhavaṃsa":  return  "E1218N.NRF";
				default: return "";
			}
		}
		
		void ExpandButtonClick(object sender, System.EventArgs e)
		{
			treeView1.ExpandAll();
		}
		
		void CollapseButtonClick(object sender, System.EventArgs e)
		{
			treeView1.CollapseAll();
		}
		
		private void ExpandLastBook(TreeNodeCollection a, string lastBookPath)
		{
			foreach(TreeNode node in a)
			{	
				if (node.FullPath == lastBook)
				{
					//MessageBox.Show(node.FullPath);
					ExpandItem(node);
					break;
				}
				else if(node.GetNodeCount(true) > 0)
				{
					ExpandLastBook(node.Nodes, lastBookPath);
				}
				else 
				{
					return;
				}
			}
			return;
		}
		
		private void ExpandItem(TreeNode node)
		{	
			TreeView tvw = node.TreeView;
			tvw.SelectedNode = node;
			while(node.Parent != null)
			{
				node = node.Parent;
				node.Expand();
			}
			tvw.SelectedNode.EnsureVisible();
		}
		
		protected void OnBookReadyEvent()
		{
			BookReadyEvent();
		}
		
		private void LoadBookFromLibrary(string bookName)
		{
			try
			{
				if(!isOnlineMode)
					AalekhDecoder.UnzipFromZipLibrary(bookName);
				this.Host.StatusBarText("Loaded " + bookName + " into workspace.");
				this.Host.SignalReadyBookExtraction(bookName);	
			}
			catch(System.IO.FileNotFoundException fnf)
			{
				
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error while trying to uncompress book \n" + ex.ToString(), "Error");
			}
			return;
		}
		
		
		#region IPlugin Members
		
		IPluginHost myPluginHost = null;
		string myPluginName = "BookSelector";
		string myDisplayName = "Open Book";
		string myPluginAuthor = "Lennart Lopin";
		string myPluginDescription = "Main menu control to access Tipiṭaka books from compressed archive.";
		string myPluginVersion = "0.1.2";
		Image  myPluginIcon = Image.FromFile("Icons\\book.png");
		
		
        
		void PluginInterface.IPlugin.Dispose()
		{	
		}

		public Object GetPluginParameter(Object o)
		{
			return null;
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
		
		public void SetPluginParameter(Object o)
		{
			isOnlineMode = (bool)o;
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
		void BookSelectionMenuKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
			else if(e.KeyCode == Keys.Enter)
			{
				OpenButtonClick(null, null);
			}
		}
		
		void TreeView1KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
			else if(e.KeyCode == Keys.Enter)
			{
				OpenButtonClick(null, null);
			}
		}
		
		void TreeView1AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			try
			{
				panel1.BackgroundImage = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + @"\Icons\bsb.jpg");
				labelDesc.Visible = true;
				TreeNode a = treeView1.SelectedNode;
				selectedBook = a.FullPath;
				if (selectedBook == "") return;
				if(selectedBook == "Tipiṭaka")
				{
					panel1.BackgroundImage = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + @"\Icons\tipit.jpg");
					labelDesc.Visible = false;
				}
				string name = matchBookNameToCSCDFile(selectedBook);
				if(name == "")
					name = selectedBook;
				if(name.IndexOf("\\") > -1)
					name = name.Substring(name.LastIndexOf("\\") + 1);
				name = "[" + name + "]";
				//MessageBox.Show(name);
				FileStream fs = File.Open(Directory.GetCurrentDirectory() + @"\Canon\desc.rtf", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				string line = "";
				string result = "";
				while((line = sr.ReadLine()) != null)
				{
					if(line.IndexOf(name) > -1)
					{
						//MessageBox.Show(name);
						string desc = "";
						
						while((desc = sr.ReadLine()) != null && !(desc.IndexOf("$") > -1))
						{
							//MessageBox.Show(desc);
							result += desc;
						}
						labelDesc.Rtf = "{\\rtf" + "{" + result + "}";
						break;
					}
				}
				sr.Close();
				fs.Close();
				if(result == "")
					labelDesc.Text = name;
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("Error: + " + ex.Message.ToString());
			}
		}
	}
}
