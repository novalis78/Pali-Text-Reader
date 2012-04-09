///
///
/// Copyright (C) 2005  Lennart Lopin <novalis78@gmx.net> 
/// Bugfixes by Pavel Bure
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
using System.IO;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace PaliReaderUtils
{
	/// <summary>
	/// Utility class for converting aalekh-encoded bytes
	/// to UTF-8.
	/// </summary>
	public class AalekhDecoder
	{
		private byte[] aalekhEncodedBytes;
		private bool capitalMode = false;
		public long arraySize = 0;
		
		public AalekhDecoder(byte[] a)
		{
			aalekhEncodedBytes = a;
		}
		
		public AalekhDecoder()
		{
		}
		
		#region notused here
		/// <summary>
		/// To ITAN ascii
		/// </summary>
		/// <returns></returns>
		public byte[] ToAscii()
		{
			return Convert(aalekhEncodedBytes, Encoding.ASCII.EncodingName);
		}
		
		public byte[] ToUTF8()
		{
			return Convert(aalekhEncodedBytes, Encoding.UTF8.EncodingName);
		}
		
		public byte[] ToUTF16()
		{
			return Convert(aalekhEncodedBytes, Encoding.UTF8.EncodingName);
		}
		
		private int GetByteCount(string s)
		{
		      if (s == null)
		      {
		            throw new ArgumentNullException("s");
		      }
		      int num1 = s.Length * 2;
		      if (num1 < 0)
		      {
		            throw new ArgumentOutOfRangeException("ArgumentOutOfRange_GetByteCountOverflow");
		      }
		      return num1;
		}
		
		private byte[] GetConvertedBytes(string s)
		{
		      if (s == null)
		      {
		            throw new ArgumentNullException("ArgumentNull_String");
		      }
		      int num1 = this.GetByteCount(s);
		      byte[] buffer1 = new byte[num1];
		      byte[] bytes = ConvertBytes(s, 0, s.Length, buffer1, 0);
		      return bytes;
		}
		
		private byte[] ConvertBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
		      int num1 = charCount * 2;
		      if ((s == null) || (bytes == null))
		      {
		            throw new ArgumentNullException((s == null) ? "s" : "ArgumentNull_Array");
		      }
		      if ((charIndex < 0) || (charCount < 0))
		      {
		            throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "ArgumentOutOfRange_NeedNonNegNum");
		      }
		      if ((s.Length - charIndex) < charCount)
		      {
		            throw new ArgumentOutOfRangeException("sArgumentOutOfRange_IndexCount");
		      }
		      if ((byteIndex < 0) || (byteIndex > bytes.Length))
		      {
		            throw new ArgumentOutOfRangeException("ArgumentOutOfRange_Index");
		      }
		      if ((bytes.Length - byteIndex) < num1)
		      {
		            throw new ArgumentException("Argument_ConversionOverflow");
		      }
		      if (true)
		      {
		            int num2 = charIndex + charCount;
		            while (charIndex < num2)
		            {
		                  char ch1 = s[charIndex++];
		                  bytes[byteIndex++] = (byte) (ch1 >> 8);
		                  bytes[byteIndex++] = (byte) ch1;
		            }
		            return bytes;
		      }
		}
				
		private byte[] Convert(byte[] a, string b)
		{
			return a;
		}

		#endregion
		
		public char[] AalekhToUnicode(byte[] alekhArray, bool countOnly)
		{
		  DateTime a = DateTime.Now;
		  ArrayList rArray = new ArrayList();
		  for(int y = 0; y < alekhArray.Length; y++)
          {		  
          	switch(alekhArray[y])
			{
				case 0x0d: rArray.Add((char)  (0x0d)); break;//CR
				case 0x0a: rArray.Add((char)  (0x20)); break;//space
				
				case 0xa5: 
				{
					rArray.Add((char) 0x5c);//"\c" - style tag
					rArray.Add((char) 0x63); capitalMode = true; break;
				}
				case 0xb0: rArray.Add((char)  (0xb0-0x80)); break;//0
				case 0xb1: rArray.Add((char)  (0xb1-0x80)); break;//1
				case 0xb2: rArray.Add((char)  (0xb2-0x80)); break;//2
				case 0xb3: rArray.Add((char)  (0xb3-0x80)); break;//3
				case 0xb4: rArray.Add((char)  (0xb4-0x80)); break;//4
				case 0xb5: rArray.Add((char)  (0xb5-0x80)); break;//5
				case 0xb6: rArray.Add((char)  (0xb6-0x80)); break;//6
				case 0xb7: rArray.Add((char)  (0xb7-0x80)); break;//7
				case 0xb8: rArray.Add((char)  (0xb8-0x80)); break;//8
				case 0xb9: rArray.Add((char)  (0xb9-0x80)); break;//9

				case 0x20:
				{
					if(y>4 && y<alekhArray.Length-1)
					{
						if((char)alekhArray[y-4]==(char)0x0d && (char)alekhArray[y-3]==(char)0x0a && 
							(char)alekhArray[y-2]==(char)0x0d && (char)alekhArray[y-1]==(char)0x0a)
						{
							char cNext=(char)alekhArray[y+1];
							if(cNext!=(char)0x0a && cNext!=(char)0x0d && cNext!=(char)0x20 && cNext!=(char)0xa5)
							{//this is the place we need to add our own style tag								
								rArray.Add((char) 0x5c);//\ - style tag
								rArray.Add((char) 0x63); //c
								rArray.Add((char) (0xb0-0x80));//0
								rArray.Add((char) (0xb3-0x80));//3
								capitalMode = true;
							}
							else
								rArray.Add((char)  0x20); 
						}
						else
							rArray.Add((char)  0x20); 
					}
					else
						rArray.Add((char)  0x20); 
					
					break;//Space
				}
				
				case 0x1b: rArray.Add((char)  0x2e); capitalMode = true; break;//.
				//case 0x67: rArray.Add((char)  0x2e); break;//.
				//case 0x43: rArray.Add((char)  0x2e); break;//.
				case 0xbf: rArray.Add((char)  0x3f); break;//?
				case 0xa1: rArray.Add((char)  0x21); break;//!
				case 0xa8: rArray.Add((char)  0x28); break;//(
				case 0xa9: rArray.Add((char)  0x29); break;//)
				case 0xab: rArray.Add((char)  0x2b); break;//+
				case 0xad: rArray.Add((char)  0x2d); break;//-
				case 0xbc: rArray.Add((char)  0x7b); break;//{
				case 0xbe: rArray.Add((char)  0x7d); break;//}
				case 0xac: rArray.Add((char)  0x2c); break;//,
				case 0xdc: rArray.Add((char)  0x5c); break;//\

				case 0xae: rArray.Add((char)  0x2e); break;//.
				case 0xa3: rArray.Add((char)  'V'); break;//VRI
				case 0xa4: rArray.Add((char)  'B'); break;//Burmese
				case 0xa6: rArray.Add((char)  'T'); break;//Thai
				case 0xc0: rArray.Add((char)  'P'); break;//PTS
				
				case 0xc4: rArray.Add((char)  0x1e43); capitalMode = false; break;//m.	
				case 0xc6: rArray.Add(capitalMode ? (char) 0x41 : (char)  0x61); capitalMode = false; break;//a
        	   	case 0xc7: rArray.Add(capitalMode ? (char) 0x100 : (char) 0x101);capitalMode = false; break;//aa
        	   	case 0xc8: rArray.Add(capitalMode ? (char) 0x49 : (char)  0x69); capitalMode = false; break;//i
        	   	case 0xc9: rArray.Add(capitalMode ? (char) 0x12a: (char)  0x12b);capitalMode = false; break;//ii
        	   	case 0xca: rArray.Add(capitalMode ? (char) 0x55 : (char)  0x75); capitalMode = false; break;//u
        	   	case 0xcb: rArray.Add(capitalMode ? (char) 0x16a: (char)  0x16b);capitalMode = false; break;//uu
        	   	case 0xcd: rArray.Add(capitalMode ? (char) 0x45 : (char)  0x65); capitalMode = false; break;//e
				case 0xcf: rArray.Add(capitalMode ? (char) 0x4F :(char) 0x6f); capitalMode = false; break;//o
        	   	
        	   	case 0xd3: rArray.Add(capitalMode ? (char) 0x4b : (char)  0x6b); capitalMode = false; break;//k
        	   	case 0xd4: {rArray.Add(capitalMode ? (char) 0x4b : (char) 0x6b); 
        	   		        rArray.Add((char) 0x68); capitalMode = false; break;}//kh
        	   	case 0xd5: rArray.Add(capitalMode ? (char) 0x47 : (char)  0x67); capitalMode = false; break;//g
        	   	case 0xd6: {rArray.Add(capitalMode ? (char) 0x47 : (char) 0x67); 
        	   				rArray.Add((char) 0x68); capitalMode = false; break;}//gh
        	   	case 0xd7: rArray.Add((char) 0x1e45); break;//n.
        	   	case 0xd8: rArray.Add(capitalMode ? (char) 0x43 : (char)  0x63); capitalMode = false; break;//c
        	   	case 0xd9: {rArray.Add(capitalMode ? (char) 0x43 : (char) 0x63);
        	   			 	rArray.Add((char) 0x68); capitalMode = false; break;}//ch
        	   	case 0xda: rArray.Add(capitalMode ? (char) 0x4a : (char)  0x6a); capitalMode = false; break;//j
        	   	

        	   	case 0xe0: rArray.Add((char) 0x27); break;//'
        	   	//case 0xa7: rArray.Add((char) 0x27); break;//'

				case 0xe1: {rArray.Add(capitalMode ? (char) 0x4a : (char) 0x6a);
        	   			    rArray.Add((char) 0x68); capitalMode = false; break;}//jh
        	   	case 0xe2: rArray.Add(capitalMode ? (char) 0xd1 : (char) 0xf1); capitalMode = false; break;//n~
        	   	case 0xe3: rArray.Add(capitalMode ? (char) 0x1e6c : (char) 0x1e6d); capitalMode = false; break;//t.
        	   	case 0xe4: {rArray.Add(capitalMode ? (char) 0x1e6c : (char) 0x1e6d); 
        	   				rArray.Add((char) 0x68); capitalMode = false; break;}//t.h
        	   	case 0xe5: rArray.Add(capitalMode ? (char) 0x1e0c : (char) 0x1e0d); capitalMode = false; break;//d.
        	   	case 0xe6: {rArray.Add((char) 0x1e0d);
        	   	 		   rArray.Add((char) 0x68); break;}//d.h
        	   	case 0xe7: rArray.Add((char) 0x1e47); break;//n.
        	   	case 0xe8: rArray.Add(capitalMode ? (char) 0x54 : (char) 0x74); capitalMode = false; break;//t
        	   	case 0xe9: {rArray.Add(capitalMode ? (char) 0x54 : (char) 0x74);
        	   			   rArray.Add((char) 0x68); capitalMode = false; break;}//th
        	   	case 0xea: rArray.Add(capitalMode ? ((char)  0x44) : (char) 0x64); capitalMode = false; break;//d
        	   	case 0xeb: {rArray.Add(capitalMode ? (char) 0x44   : (char) 0x64);
        	   			   rArray.Add((char) 0x68); capitalMode = false; break;}//dh
        	   	case 0xec: rArray.Add(capitalMode ? (char)  0x4e : (char) 0x6e); capitalMode = false; break;//n
        	   	case 0xed: rArray.Add(capitalMode ? (char) 0x50  : (char) 0x70); capitalMode = false; break;//p
        	   	case 0xee: {rArray.Add(capitalMode ? (char) 0x50 : (char) 0x70);
        	   			   rArray.Add((char) 0x68); capitalMode = false; break;}//ph
        	   	case 0xef: rArray.Add(capitalMode ? (char) 0x42  : (char) 0x62); capitalMode = false; break;//b
        	   	
				case 0xf0: {rArray.Add(capitalMode ? (char) 0x42 : (char) 0x62);
        	   			   rArray.Add((char) 0x68); capitalMode = false; break;}//bh
        	   	case 0xf1: rArray.Add(capitalMode ? (char) 0x4d :  (char) 0x6d); capitalMode = false; break;//m
        	   	case 0xf2: rArray.Add(capitalMode ? (char) 0x59 :  (char) 0x79); capitalMode = false; break;//y
        	   	case 0xf3: rArray.Add(capitalMode ? (char) 0x52 :  (char) 0x72); capitalMode = false; break;//r
        	   	case 0xf4: rArray.Add(capitalMode ? (char) 0x4c :  (char) 0x6c); capitalMode = false; break;//l
        	   	case 0xf5: rArray.Add((char) 0x1e37); break;//l.
        	   	case 0xf6: rArray.Add(capitalMode ? (char) 0x56  : (char) 0x76); capitalMode = false; break;//v
        	   	case 0xf7: rArray.Add((char) 0x15b); break;//s'
        	   	case 0xf8: rArray.Add((char) 0x1e63); break;//s.
        	   	case 0xf9: rArray.Add(capitalMode ? (char) 0x53  : (char) 0x73); capitalMode = false; break;//s
        	   	case 0xfa: rArray.Add(capitalMode ? (char) 0x48  : (char) 0x68); capitalMode = false; break;//h     	   	
        	}
			
		  }
		  DateTime b = DateTime.Now;
		  TimeSpan c = b - a;
		  Console.WriteLine("time: " + c.Milliseconds.ToString());
    	  Char[] rA = new Char[rArray.Count];
		  if(!countOnly)
			  rArray.CopyTo(rA, 0);
		  else
			  this.arraySize = rArray.Count;
		  a = DateTime.Now;
		  c = a - b;
		  Console.WriteLine("time: " + c.Milliseconds.ToString());
			return rA;
		}
		
		/// <summary>
		/// Unzip any book from the zipped canon archive
		/// </summary>
		/// <param name="bookName"></param>
		public static void UnzipFromZipLibrary(string bookName)
		{
			if(bookName == "")
			{
				MessageBox.Show("The book you selected is not (yet) available.");
				return;
			}
			string internalBookName = bookName;
			bookName = bookName.ToUpper();
			//MessageBox.Show(bookName);
			if(bookName.EndsWith(".MUL")) bookName = "canon/mul/"+bookName + ".htm";
			else if(bookName.EndsWith(".ATT")) bookName = "canon/att/"+ bookName  + ".htm";
			else if(bookName.EndsWith(".NRF")) bookName = "canon/anu/"+ bookName  + ".htm";
			else if(bookName.EndsWith(".TIK")) bookName = "canon/tik/"+ bookName  + ".htm";
			//MessageBox.Show(bookName);
			string libDir = Directory.GetCurrentDirectory();
			try
			{
				ZipInputStream s = new ZipInputStream(File.OpenRead(libDir + @"\Canon\canon.zip"));
				ZipEntry theEntry;
				if(s == null) return;
				int size = 16;
				byte[] data = new byte[16];
				while ((theEntry = s.GetNextEntry()) != null)
				{
					if(theEntry.Name == bookName)
					{
						FileStream fs = new FileStream(libDir + @"\Work\" + internalBookName + ".htm" , FileMode.OpenOrCreate, FileAccess.Write);
						while (true)
						{
							size = s.Read(data, 0, data.Length);
							if (size > 0) 
							{
								fs.Write(data, 0, data.Length);	
							} 
							else 
							{
								break;
							}
						}
						fs.Close();
					}
				}	
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

		
	}
}

