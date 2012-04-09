/*
 * Created by SharpDevelop.
 * User: novalis78
 * Date: 24.06.2005
 * Time: 19:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace PaliReaderUtils
{
	/// <summary>
	/// specify aalekh byte array and encoding
	/// formats according to aalek format info and desired output.
	/// </summary>
	public class AalekhFormatter
	{
		private char[] aalekhArray;
		//HTML Constants
		private char[] DocTypeDefinition40  = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0//EN\" \"http://www.w3.org/TR/REC-html40/strict.dtd\">".ToCharArray();
		private char[] DocTypeDefinition41  = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">".ToCharArray();
		private char[] CommentTags 	        = "<!-- Converted from Vipassana Research Institute's Chattha Sangayana CD with Pali Text Reader. See http://www.nibbanam.com/ for more information. -->\n".ToCharArray();
		private char[] HtmlHeaderStart      = "<HTML><HEAD><TITLE></TITLE>".ToCharArray();
		private char[] StyleSheetDefinition = "<LINK rel=\"stylesheet\" href=\"style.css\">".ToCharArray();
		private char[] MetaInfoDefinition   = "<META http-equiv=\"Content-Style-Type\" content=\"text/css\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">".ToCharArray();
		private char[] HtmlHeaderEnd  	    = "</HEAD>".ToCharArray();
		private char[] HtmlBodyStart  	    = "<BODY>".ToCharArray();
		private char[] HtmlBodyEnd  	    = "</BODY>".ToCharArray();
		//XHTML Constants
		private char[] DocTypeDefinitionX4  = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE html \n PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\">".ToCharArray();
		
		
		public AalekhFormatter(Char[] a)
		{
			aalekhArray = a;
 		}
		
		public char[] ToTxt()
		{
			return aalekhArray;
		}
		
		public string ToRTF()
		{
			return "";
		}
		
		public char[] ToHTML()
		{
			string HtmlDocument = "";
			HtmlDocument += new String(DocTypeDefinition41) + "\r\n";
			HtmlDocument += new String(CommentTags) 		+ "\r\n";
			HtmlDocument += new String(HtmlHeaderStart) 	+ "\r\n";
			HtmlDocument += new String(StyleSheetDefinition)+ "\r\n";
			HtmlDocument += new String(MetaInfoDefinition) 	+ "\r\n";
			HtmlDocument += new String(HtmlHeaderEnd) 		+ "\r\n";
			
			HtmlDocument += new String(HtmlBodyStart) 		+ "\r\n";
			HtmlDocument += new String(this.formatParagraphs(aalekhArray, "HTML"));
			HtmlDocument += new String(HtmlBodyEnd) 		+ "\r\n";
			return HtmlDocument.ToCharArray();
		}
		
		public string ToXHTML()
		{
			return "";
		}
		//zu langsam!
		private char[] formatParagraphs(char[] a, string targetFormat)
		{
			StringBuilder tmp = new StringBuilder(new String(a));
			string t = "";
			try
			{
				if(targetFormat == "HTML")
				{
					//change style tags into CSS definition
					t = Regex.Replace(new String(a), "\\\\c\\d{2}", "$&\">");
					t = t.Replace("\\c", "\r\n<P class = \"c");
					//hide book/page information into tooltip

					//PavelBure: doing this with ONE regex
					t = Regex.Replace(t, "([BVTPO])(\\d\\.\\d{4})", "<a href=\"#\" title=\"page: $2\">[$1]</a>");
					//format printed edition naming
//					t = t.Replace("sī0","sī.");
//					t = t.Replace("syā0","syā.");
//					t = t.Replace("kaṃ0","kaṃ.");
//					t = t.Replace("ka0","ka.");
//					t = t.Replace("pī0","pī.");
//					t = t.Replace("dī0 ni0", "Dīgha Nikāye");

					//this is much better since it applies this formatting to all references
					//including A. ni. , Vibha. etc
					t=Regex.Replace(t,"([^ \\d])(0)([ \\)\\.\\r])","$1.$3");
					t = t.Replace("}", "]</font>");
					t = t.Replace("{", "<font color=\"blue\">[");
					//do some optimizing
					t = t.Replace("--",":");
					t = t.Replace("...pe...."," - pe - ");
//					t = t.Replace("bhagavā", "<b>Bhagavā</b>");
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return t.ToCharArray();
		}
		/// <summary>
		/// Similar to formatParagraphs, but removes all text-critical information, thus
		/// making it easier to read.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="targetFormat"></param>
		/// <returns></returns>
		private char[] formatParagraphsReadOptimized(char[] a, string targetFormat)
		{
			StringBuilder tmp = new StringBuilder(new String(a));
			string t = "";
			try
			{
				if(targetFormat == "HTML")
				{
					t = Regex.Replace(new String(a), "\\\\c\\d{2}", "$&\">");
					t = t.Replace("\\c", "\r\n<P class = \"c");
					t = Regex.Replace(t, "B(\\d\\.\\d{4})", "");
					t = Regex.Replace(t, "V(\\d\\.\\d{4})", "");
					t = Regex.Replace(t, "T(\\d\\.\\d{4})", "");
					t = Regex.Replace(t, "P(\\d\\.\\d{4})", "");
					t = t.Replace("sī0","");
					t = t.Replace("syā0","");
					t = t.Replace("kaṃ0","");
					t = t.Replace("ka0","");
					t = t.Replace("pī0","");																									

					t = t.Replace("(katthaci)", "");
					t = t.Replace("(", "");
					t = t.Replace(")", "");
					
					t = t.Replace("}", "]</font color>");
					t = t.Replace("{", "<font color=\"blue\">[");
					
					t = t.Replace("--",":");
					t = t.Replace("...pe0..."," - pe - ");
					//t = t.Replace("bhagavā", "<b>Bhagavā</b>");
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return t.ToCharArray();
		}
		
		private string findAndReplaceText(string source, string replaceWith)
		{
			return "";
		}
	}
}
