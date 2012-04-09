/*
 *  Pali Text Reader - Buddhist pali canon study tool
 *  Copyright (C) 2550  Lennart Lopin
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License along
 *  with this program; if not, write to the Free Software Foundation, Inc.,
 *  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System;
using AMS.Profile;
namespace PaliReaderUtils
{
	/// <summary>
	/// Description of PropertyManager.
	/// </summary>
	public sealed class PropertyManager
	{
		private static PropertyManager pm = null;
		private AMS.Profile.Xml configFile = null;
		
		private PropertyManager()
		{
			configFile = new Xml();
		}
		
		public static PropertyManager GetInstance()
		{
			try
			{
				if(pm != null)
				{
					return pm;
				}
				else
				{
					pm = new PropertyManager();
					return pm;
				}
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show("Error: " + e.Message.ToString());
				return null;
			}
		}
		
		public void SetGeneralProperty(string propName, string propValue)
		{
			if(configFile != null)
				configFile.SetValue("General", propName, propValue);
		}
		public string GetGeneralProperty(string propName)
		{
			if(configFile != null)
				return (string)configFile.GetValue("General", propName);
			else
				return "";
		}
	}
}
