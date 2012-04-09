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
using System.Runtime.InteropServices;

namespace PaliReaderPlugin
{

 /// <summary>
 /// Interop interface for DHTML event handlers.
 /// </summary>
 public interface IWebEventHandler
 {
  [DispId(0)]
  void OnEvent();
 }

 /// <summary>
 /// Base class for DHTML event handling.
 /// </summary>
 /// <remarks>Use this class as a base for handling HTML events fired by a
 /// web control. You must create a derived class for each event you want
 /// to handle, and implement the handler in the virtual HandleEvent function.
 /// <para>As soon as the event is handled, the base handler will capture the mouse
 /// position and store it in the protected members _nClientX and _nClientY.</para>
 /// <para>To handle the web control events, follow this sample procedure:</para>
 /// <list type="number">
 /// <item>
 /// <description>place the web control on your form (e.g. <c>_wbb</c>) and add a
 /// reference to <c>Microsoft.mshtml</c> in your project;</description>
 /// </item>
 /// <item>
 /// <description>add to your form a <c>mshtml.IHTMLDocument2</c> member (e.g.
 /// <c>_doc</c>) and instantiate in the <c>NavigateComplete2</c> event handler for
 ///  the web control (<c>_doc = (mshtml.IHTMLDocument2)_wbb.Document;</c>);</description>
 ///  </item>
 ///  <item>
 /// <description>add to your form a member of your derived handler class for the
 /// event you want to handle, instantiate it in <c>NavigateComplete2</c> handler as
 /// above, attach it to the web control and handle the event it fires. E.g. for a
 /// <c>mouseover</c> event handler class -see sample code below-:
 /// <code>
 /// _mouseover = new MouseOverHandler(_doc);
 /// _doc.onmouseover = new DispatchWrapper(_mouseover);
 /// _mouseover.OnWebEvent += new WebEventHandlerBase.WebEventHandler(OnMouseOver);
 /// </code>);</description>
 /// </item>
 /// <item>
 /// <description>add to your form the function for handling the event, e.g.:
 /// <code>
 /// private void OnMouseOver(object sender, WebEventHandlerBase.WebEventArgs e)
 /// {
 /// MouseOverHandler h = (MouseOverHandler)sender;
 /// _lblInfo.Text = String.Format("{0} id={1}", h.HtmlElement.tagName, h.HtmlElement.id);
 /// }
 /// </code>
 /// </description>
 /// </item>
 /// </list>
 /// <para>For the above sample, the class for the <c>mouseover</c> event is:
 /// <code>
 /// public class MouseOverHandler : WebEventHandlerBase
 /// {
 ///  private mshtml.IHTMLElement _el;
 ///
 ///  public mshtml.IHTMLElement HtmlElement
 ///  {
 ///   get { return _el; }
 ///  }
 ///
 ///  public MouseOverHandler(mshtml.IHTMLDocument2 doc) : base(doc)
 ///  {
 ///   _el = null;
 ///  }
 ///
 ///  protected override void HandleEvent()
 ///  {
 ///   if (OnWebEvent == null) return;
 ///
 ///   _el = _doc.elementFromPoint(_nClientX, _nClientY);
 ///   OnWebEvent(this, new WebEventArgs(_doc, _nClientX, _nClientY));
 ///  }
 /// }
 /// </code></para>
 /// </remarks>
 public class WebEventHandlerBase : IWebEventHandler
 {
  #region WebEventArgs Class
  /// <summary>
  /// DHTML web event arguments.
  /// </summary>
  public class WebEventArgs : EventArgs
  {
   private mshtml.IHTMLDocument2 _doc;
   private int _nClientX;
   private int _nClientY;
   private int _nKeyCode;

   /// <summary>
   /// The HTML document raising the event.
   /// </summary>
   public mshtml.IHTMLDocument2 HtmlDocument
   {
    get { return _doc; }
   }

   /// <summary>
   /// Mouse client x-coords.
   /// </summary>
   public int ClientX
   {
    get { return _nClientX; }
   }

   /// <summary>
   /// Mouse client y-coords.
   /// </summary>
   public int ClientY
   {
    get { return _nClientY; }
   }

   public int KeyCode
   {
    get { return _nKeyCode; }
   }

   /// <summary>
   /// Build a WebEventArgs object with the specified document and
   /// mouse coords.
   /// </summary>
   /// <param name="doc">HTML document</param>
   /// <param name="x">mouse x-coords</param>
   /// <param name="y">mouse y-coords</param>
   public WebEventArgs(mshtml.IHTMLDocument2 doc, int x, int y)
   {
    _doc = doc;
    _nClientX = x;
    _nClientY = y;
   }
   public WebEventArgs(mshtml.IHTMLDocument2 doc, int k)
   {
    _doc = doc;
    _nKeyCode = k;
   }
  }
  #endregion

  public delegate void WebEventHandler(object sender, WebEventArgs e);
  /// <summary>
  /// DHTML web event.
  /// </summary>
  public WebEventHandler OnWebEvent;

  protected mshtml.IHTMLDocument2 _doc;
  protected int _nClientX;
  protected int _nClientY;
  protected int _nKeyCode;

  /// <summary>
  /// Protected constructor. This class cannot be directly instantiated.
  /// </summary>
  /// <param name="doc">HTML document raising the events</param>
  protected WebEventHandlerBase(mshtml.IHTMLDocument2 doc)
  {
   _doc = doc;
  }

  #region IWebEventHandler Members
  /// <summary>
  /// This function is called by the web control component.
  /// </summary>
  [DispId(0)]
  public void OnEvent()
  {
   // immediately capture mouse position
   mshtml.IHTMLEventObj obj = _doc.parentWindow.@event;
   _nClientX = obj.clientX;
   _nClientY = obj.clientY;
   _nKeyCode = obj.keyCode;

   // handle the event
   HandleEvent();
  }
  #endregion

  /// <summary>
  /// Handle the event according to its type.
  /// </summary>
  protected virtual void HandleEvent()
  {
   // do nothing, implemented in derived classes
  }
 }
} 
