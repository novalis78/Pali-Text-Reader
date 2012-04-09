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

namespace PaliReaderPlugin
{
	/// <summary>
	/// Description of MouseOverHandler.
	/// </summary>
	public class MouseOverHandler : WebEventHandlerBase
 	{
 		private mshtml.IHTMLElement _el;
 
 		public mshtml.IHTMLElement HtmlElement
 		{
 			get { return _el; }
 		}
 
 		public MouseOverHandler(mshtml.IHTMLDocument2 doc) : base(doc)
 		{
 			_el = null;
 		}
 
		protected override void HandleEvent()
 		{
 			if (OnWebEvent == null) return;
 
 			_el = _doc.elementFromPoint(_nClientX, _nClientY);
 			OnWebEvent(this, new WebEventArgs(_doc, _nClientX, _nClientY));
 		}
 	}
	
}
