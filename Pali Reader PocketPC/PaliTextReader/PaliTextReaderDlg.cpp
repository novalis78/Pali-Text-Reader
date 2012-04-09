// PaliTextReaderDlg.cpp : implementation file
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "PaliTextReaderDlg.h"
#include "SelectBookDlg.h"
#include <Htmlctrl.h>
#include "preferencesdlg.h"
#include "components\zip\unzip.h"
#include "palitextentrydlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

extern CPaliTextReaderApp theApp;
/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderDlg dialog
HINSTANCE CPaliTextReaderDlg::m_HtmlViewInstance = 0;

CPaliTextReaderDlg::CPaliTextReaderDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPaliTextReaderDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPaliTextReaderDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_bDictionaryVisible=FALSE;
	m_bBookLoaded=FALSE;
}

void CPaliTextReaderDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPaliTextReaderDlg)
	DDX_Control(pDX, IDC_DICTIONARY, m_ctlDictionaryPlaceholder);
	DDX_Control(pDX, IDC_CHAPTERSELECTOR, m_ctlChapterSelector);
	DDX_Control(pDX, IDC_TEXTVIEWER, m_ctlTextViewerPlaceholder);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPaliTextReaderDlg, CDialog)
	//{{AFX_MSG_MAP(CPaliTextReaderDlg)
	ON_COMMAND(IDM_EXIT, OnExit)
	ON_COMMAND(IDM_SELECTBOOK, OnSelectbook)
	ON_COMMAND(IDM_PREFERENCES, OnPreferences)
	ON_CBN_SELCHANGE(IDC_CHAPTERSELECTOR, OnSelchangeChapterselector)
	ON_COMMAND(IDM_SHOWDIC, OnShowdic)
	ON_UPDATE_COMMAND_UI(IDM_SHOWDIC, OnUpdateShowdic)
	ON_WM_INITMENUPOPUP()
	ON_COMMAND(ID_BOOKMARK_PAGE, OnBookmarkPage)
	ON_UPDATE_COMMAND_UI(ID_BOOKMARK_PAGE, OnUpdateBookmarkPage)
	ON_COMMAND(ID_SEARCH, OnSearch)
	ON_UPDATE_COMMAND_UI(ID_SEARCH, OnUpdateSearch)
	//}}AFX_MSG_MAP
	ON_MESSAGE(SELBOOKDLGCLOSED,OnBookSelected)
	ON_COMMAND(IDC_TRANSLATE,OnTranslate)
	ON_COMMAND(IDC_COPY,OnCopy)	
	ON_COMMAND_RANGE(ID_MOVE_FIRST,ID_MOVE_NEXT,OnSearchBookmarkMove)
	ON_UPDATE_COMMAND_UI_RANGE(ID_MOVE_FIRST,ID_MOVE_NEXT,OnUpdateSearchBookmarkMove)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderDlg message handlers

BOOL CPaliTextReaderDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	CenterWindow(GetDesktopWindow());	// center to the hpc screen

	SHDoneButton(m_hWnd, SHDB_HIDE);
	// TODO: Add extra initialization here

	m_nSearchResultsCount=0;
	CCeCommandBar *pCommandBar = (CCeCommandBar*)m_pWndEmptyCB;
	pCommandBar->InsertMenuBar(IDR_MAINMENU);
	pCommandBar->LoadToolBar(IDR_MAIN_TOOLBAR);
	pCommandBar->SetSizes(CSize(23,21), CSize(16,15));

	if(!theApp.GetLibraryLocation().IsEmpty())
		 theApp.IndexReader.Open(theApp.GetLibraryLocation()+_T("lib_index.dat"));

	CreateHtmlWindow();

	DWORD dwStyle = m_ctlTextViewerPlaceholder.GetStyle();
	CRect rect;
	m_ctlDictionaryPlaceholder.GetWindowRect(rect);

	m_ctlDictionary.Create(WS_CHILD | WS_VISIBLE | TCS_MULTILINE | TCS_VERTICAL,rect,this,IDC_DICTIONARY);

	LOGFONT lf;
	ZeroMemory(&lf,sizeof(LOGFONT));
	lf.lfHeight=16;
	lstrcpy(lf.lfFaceName,_T("CN-Times"));	
	m_objPaliFont.CreateFontIndirect(&lf);
	m_ctlChapterSelector.SetFont(&m_objPaliFont);

	BOOL bOpenLastBook=theApp.GetOpenLastBook();
	if(bOpenLastBook)
	{
		unsigned int ChapterNodeID=theApp.GetLastBookChapterNodeID();
		if(ChapterNodeID!=65535)
		{
			DWORD dwOffset=theApp.GetLastBookOffset();
			int Line=theApp.GetLastBookPosition();

			CIndexNode objNode;
			theApp.IndexReader.GetNode(dwOffset,objNode);
			
			LoadBook(objNode,ChapterNodeID,Line);
		}
	}

	pCommandBar->OnUpdateCmdUI((CFrameWnd*)this,FALSE);
	pCommandBar->UpdateWindow();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CPaliTextReaderDlg::CreateHtmlWindow()
{
	if (m_HtmlViewInstance == 0) 
		m_HtmlViewInstance = ::LoadLibrary(L"htmlview.dll");
	

	VERIFY(InitHTMLControl(AfxGetInstanceHandle()));
	
	CRect rect;
	m_ctlTextViewerPlaceholder.GetWindowRect(rect);

	m_hwndHtml = ::CreateWindow (DISPLAYCLASS, 
                                 NULL,
                                 WS_CHILD | WS_VISIBLE,
                                 rect.left, 
                                 rect.top, 
                                 rect.Width(), 
                                 rect.Height(),
                                 m_hWnd, 
                                 0, 
                                 m_HtmlViewInstance, 
                                 NULL);

	::SetWindowLong(m_hwndHtml, GWL_ID, IDC_TEXTVIEWER);
	::SetFocus (m_hwndHtml);
	::SendMessage(m_hwndHtml, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)_T("")); 
	::SendMessage(m_hwndHtml,DTM_ENABLECONTEXTMENU,0,(LPARAM)TRUE);				
}

void CPaliTextReaderDlg::SetHtml(char* pHtml)
{
	::SendMessage(m_hwndHtml, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)_T(""));	
	::SendMessage(m_hwndHtml, DTM_ADDTEXT, FALSE, (LPARAM)pHtml);
	::SendMessage(m_hwndHtml,DTM_ENABLECLEARTYPE,0,TRUE);


	TRY
	{
		CFile objFile;
		CString strFileName=theApp.AppDirectory+_T("style.css");
		if(objFile.Open(strFileName,CFile::modeRead))
		{
			int FileLength=objFile.GetLength();
			char* lpStyleSheet=new char[FileLength+1];
			objFile.Read((void*)lpStyleSheet,FileLength);
			lpStyleSheet[FileLength]=0;

			TCHAR* pWideChar=NULL;
			int nBufSize=0;
			nBufSize=MultiByteToWideChar(CP_ACP,NULL,lpStyleSheet,-1,pWideChar,nBufSize);
			pWideChar=new TCHAR[nBufSize+1];
			MultiByteToWideChar(CP_ACP,NULL,lpStyleSheet,-1,pWideChar,nBufSize);
			pWideChar[nBufSize]=0;

			::SendMessage(m_hwndHtml,DTM_ADDSTYLE,0,(LPARAM)pWideChar);

			delete[] pWideChar;
			delete[] lpStyleSheet;
			objFile.Close();
			m_bBookLoaded=TRUE;
		}
	}
	CATCH(CFileException,pFE)
	{
		pFE->ReportError();
	}
	END_CATCH


	::SendMessage(m_hwndHtml, DTM_ENDOFSOURCE, 0, 0);
}

void CPaliTextReaderDlg::OnExit() 
{
	if(m_bBookLoaded)
	{
		HWND hwChild=::GetWindow(m_hwndHtml,GW_CHILD);
		if(hwChild!=NULL)
		{
			SCROLLINFO si;
			si.cbSize=sizeof(SCROLLINFO);
			si.fMask=SIF_POS;
			::GetScrollInfo(hwChild,SB_VERT,&si);
			theApp.SetLastBookPosition(si.nPos);
		}
	}

	OnOK();
}

void CPaliTextReaderDlg::OnSelectbook() 
{
	if(!IsWindow(m_dlgSelectBook.m_hWnd))
		m_dlgSelectBook.Create(IDD_SELECTBOOK,this);

	m_dlgSelectBook.ShowWindow(SW_SHOW);
}

void CPaliTextReaderDlg::OnPreferences() 
{
	// TODO: Add your command handler code here
	CPreferencesDlg dlg;
	dlg.DoModal();
}


void CPaliTextReaderDlg::LoadBook(CIndexNode& rNode,unsigned int ChapterNodeID, int Line,LPCTSTR lpszSearchString)
{
		CString strTextLibrary=theApp.GetLibraryLocation();

		if(!strTextLibrary.IsEmpty())
		{
			CWaitCursor wait;

			//this improves performance
			m_ctlDictionary.ShowWindow(SW_HIDE);
			::ShowWindow(m_hwndHtml,SW_HIDE);

			CString strFileName;
			strFileName.Format(_T("PocketLib%d.zip"),rNode.LibFileIndex);
			HZIP hz=OpenZip(strTextLibrary+strFileName,NULL);
			if(hz!=NULL)
			{
				ZIPENTRY objEntry;
				ZRESULT zr=GetZipItem(hz,rNode.ZipFileIndex,&objEntry);
				if(!zr)
				{
					char* pBuf=new char[objEntry.unc_size+1];
					ZeroMemory(pBuf,objEntry.unc_size+1);

					zr=UnzipItem(hz,rNode.ZipFileIndex,pBuf,objEntry.unc_size);
					if(!zr)					
					{
						if(lpszSearchString!=NULL)
							SearchText(pBuf,objEntry.unc_size,lpszSearchString);
						else
							SetHtml(pBuf);					
					}

					delete pBuf;
				}
				CloseZip(hz);
			} 

			m_ctlChapterSelector.ResetContent();

			int nBookmarkIndex=-1;

			//it has child nodes or is a child node itself
			if(rNode.HasChildren || rNode.NodeID!=ChapterNodeID)
			{
				NodeList objChildNodeList;
				theApp.IndexReader.GetChildNodes(ChapterNodeID,objChildNodeList);
				POSITION posChild=objChildNodeList.GetHeadPosition();

				while(posChild!=NULL)
				{
					CIndexNode& rChildNode=objChildNodeList.GetNext(posChild);
					m_ctlChapterSelector.AddString(rChildNode.Name);					
					m_ctlChapterSelector.SetItemData(m_ctlChapterSelector.GetCount()-1,rChildNode.Offset);
					if(rNode.NodeID==rChildNode.NodeID)
						nBookmarkIndex=m_ctlChapterSelector.GetCount()-1;
					
					//this assumes that there may be only one level below
					if(rChildNode.HasChildren)
					{
						NodeList objGrandChildNodeList;
						theApp.IndexReader.GetChildNodes(rChildNode.NodeID,objGrandChildNodeList);
						POSITION posGrandChild=objGrandChildNodeList.GetHeadPosition();
						while(posGrandChild!=NULL)
						{
							CIndexNode& rGrandChildNode=objGrandChildNodeList.GetNext(posGrandChild);
							m_ctlChapterSelector.AddString(_T("    ")+rGrandChildNode.Name);					
							m_ctlChapterSelector.SetItemData(m_ctlChapterSelector.GetCount()-1,rGrandChildNode.Offset);						

							if(rNode.NodeID==rGrandChildNode.NodeID)
								nBookmarkIndex=m_ctlChapterSelector.GetCount()-1;
						}
					}
				}
			}

			if(nBookmarkIndex!=-1 && lpszSearchString==NULL)
			{
				m_ctlChapterSelector.SetCurSel(nBookmarkIndex);
				OnSelchangeChapterselector();
			}
			else
			{
				if(Line!=0)
				{
					HWND hwChild=::GetWindow(m_hwndHtml,GW_CHILD);
					if(hwChild!=NULL)
					{				
						for(int i=0;i<Line/20;i++)
							::SendMessage(hwChild,WM_VSCROLL,SB_LINEDOWN,0);
					}
				}
			}


			theApp.SetLastBookOffset(rNode.Offset);
			theApp.SetLastBookChapterNodeID(ChapterNodeID);

			m_ctlDictionary.ShowWindow(SW_SHOW);
			::ShowWindow(m_hwndHtml,SW_SHOW);
			::SetFocus(m_hwndHtml); //for scrolling
		}
		else
			AfxMessageBox(_T("You need to set up pali canon library"));
}

void CPaliTextReaderDlg::OnSelchangeChapterselector() 
{
	// TODO: Add your control notification handler code here
	DWORD dwOffset=m_ctlChapterSelector.GetItemData(m_ctlChapterSelector.GetCurSel());
	CIndexNode objNode;
	theApp.IndexReader.GetNode(dwOffset,objNode);
	::SendMessage(m_hwndHtml,DTM_ANCHORW,0,(WPARAM)(LPCTSTR)objNode.BookmarkName);
	::SetFocus(m_hwndHtml);
}

LRESULT CPaliTextReaderDlg::OnBookSelected(WPARAM wParam, LPARAM lParam)
{
	if(wParam==TRUE)
	{
		CIndexNode objNode;
		int Line=0;
		unsigned short ChapterNodeID;
		m_dlgSelectBook.GetSelectedNode(objNode,Line,ChapterNodeID);

		unsigned int PrevChapterNodeID=theApp.GetLastBookChapterNodeID();
		if(m_bBookLoaded && ChapterNodeID==PrevChapterNodeID)
		{//jump to this bookmark	
			for(int i=0;i<m_ctlChapterSelector.GetCount();i++)
			{
				DWORD dwOffset=m_ctlChapterSelector.GetItemData(i);
				if(dwOffset==objNode.Offset)
				{
					m_ctlChapterSelector.SetCurSel(i);
					OnSelchangeChapterselector();
					break;
				}
			}
		}
		else
			LoadBook(objNode,ChapterNodeID,Line);
	}

	CCeCommandBar *pCommandBar = (CCeCommandBar*)m_pWndEmptyCB;
	pCommandBar->ShowWindow(SW_SHOW);
	m_nSearchResultsCount=0;

	return 0;
}

void CPaliTextReaderDlg::OnTranslate()
{
	LPWSTR pSelectedText=CopySelection();
	if(pSelectedText!=NULL)
	{
		if(lstrlen(pSelectedText)>0)
		{
			if(!m_bDictionaryVisible)
			{
				m_bDictionaryVisible=TRUE;
				ToggleDictionary();
			}

			m_ctlDictionary.LookUp(pSelectedText);
		}
		else
			AfxMessageBox(_T("Select a term to translate!"));

		LocalFree(pSelectedText);
	}
}

void CPaliTextReaderDlg::OnCopy()
{
	LPWSTR pSelectedText=CopySelection();
	if(pSelectedText!=NULL)
	{
		if(!OpenClipboard())
		{
			AfxMessageBox(_T("Cannot open the Clipboard"));
			return;
		}

		if(!EmptyClipboard())
		{
			AfxMessageBox(_T("Cannot empty the Clipboard"));
			return;  
		}

		if(SetClipboardData(CF_UNICODETEXT,pSelectedText)==NULL)
		{
			AfxMessageBox(_T("Unable to set Clipboard data"));    
			CloseClipboard();
		    return;  
		}

		CloseClipboard();
	}
}


LPWSTR CPaliTextReaderDlg::CopySelection()
{
	LPWSTR pSelectedText = NULL; 
	LPSTREAM  pStream = 0; 
	DWORD rsd = 0; 
	::SendMessage(m_hwndHtml, DTM_COPYSELECTIONTONEWISTREAM, (WPARAM)&rsd, (LPARAM)&pStream); 
	if(pStream) 
	{ 
		STATSTG stat = { 0 }; 
		if (SUCCEEDED(pStream->Stat (&stat, STATFLAG_NONAME))) 
		{ 
			if (pSelectedText = (LPWSTR)LocalAlloc(LPTR,(ULONG)stat.cbSize.QuadPart + 2)) 
			{ 
				ULONG ulNumChars; //&& ulNumChars != stat.cbSize.QuadPart
				if(FAILED(pStream->Read(pSelectedText,(ULONG)stat.cbSize.QuadPart,&ulNumChars))) 
				{
					pSelectedText=NULL;
					LocalFree(pSelectedText);					
				}
				
			} 
		} 
		pStream->Release(); 
	} 

	return pSelectedText;
}

void CPaliTextReaderDlg::OnShowdic() 
{
	// TODO: Add your command handler code here
	m_bDictionaryVisible=!m_bDictionaryVisible;
	ToggleDictionary();
}

void CPaliTextReaderDlg::ToggleDictionary()
{
	//resize HTML control to make the dictionary visible
	CRect rHtml;
	::GetWindowRect(m_hwndHtml,rHtml);

	CRect rDic;
	m_ctlDictionaryPlaceholder.GetWindowRect(rDic);

	if(m_bDictionaryVisible)
		::SetWindowPos(m_hwndHtml,HWND_TOP,0,0,rHtml.Width(),rHtml.Height()-rDic.Height(),SWP_NOMOVE);
	else
		::SetWindowPos(m_hwndHtml,HWND_TOP,0,0,rHtml.Width(),rHtml.Height()+rDic.Height(),SWP_NOMOVE);
	
	CCeCommandBar *pCommandBar = (CCeCommandBar*)m_pWndEmptyCB;
	pCommandBar->OnUpdateCmdUI((CFrameWnd*)this,FALSE);
	pCommandBar->UpdateWindow();
}

void CPaliTextReaderDlg::OnUpdateShowdic(CCmdUI* pCmdUI) 
{
    if(::IsWindow(pCmdUI->m_pOther->GetSafeHwnd()))
        pCmdUI->m_pOther->SendMessage(TB_CHECKBUTTON, pCmdUI->m_nID, MAKELONG(m_bDictionaryVisible , 0));
    else
        pCmdUI->SetCheck(m_bDictionaryVisible);
}

void CPaliTextReaderDlg::OnInitMenuPopup(CMenu* pMenu, UINT nIndex, BOOL bSysMenu) 
{
    if (bSysMenu)
        return;     // don't support system menu

    ASSERT(pMenu != NULL);
    // check the enabled state of various menu items

    CCmdUI state;
    state.m_pMenu = pMenu;
    ASSERT(state.m_pOther == NULL);
    ASSERT(state.m_pParentMenu == NULL);

    // determine if menu is popup in top-level menu and set m_pOther to
    //  it if so (m_pParentMenu == NULL indicates that it is secondary popup)
    HMENU hParentMenu;
    if (AfxGetThreadState()->m_hTrackingMenu == pMenu->m_hMenu)
        state.m_pParentMenu = pMenu;    // parent == child for tracking popup
    else if ((hParentMenu = ::WCE_FCTN(GetMenu)(m_hWnd)) != NULL)
    {
        CWnd* pParent = GetTopLevelParent();
            // child windows don't have menus -- need to go to the top!
        if (pParent != NULL &&
            (hParentMenu = ::WCE_FCTN(GetMenu)(pParent->m_hWnd)) != NULL)
        {
            int nIndexMax = ::WCE_FCTN(GetMenuItemCount)(hParentMenu);
            for (int nIndex = 0; nIndex < nIndexMax; nIndex++)
            {
                if (::GetSubMenu(hParentMenu, nIndex) == pMenu->m_hMenu)
                {
                    // when popup is found, m_pParentMenu is containing menu
                    state.m_pParentMenu = CMenu::FromHandle(hParentMenu);
                    break;
                }
            }
        }
    }

    state.m_nIndexMax = pMenu->GetMenuItemCount();
    for (state.m_nIndex = 0; state.m_nIndex < state.m_nIndexMax;
      state.m_nIndex++)
    {
        state.m_nID = pMenu->GetMenuItemID(state.m_nIndex);
        if (state.m_nID == 0)
            continue; // menu separator or invalid cmd - ignore it

        ASSERT(state.m_pOther == NULL);
        ASSERT(state.m_pMenu != NULL);
        if (state.m_nID == (UINT)-1)
        {
            // possibly a popup menu, route to first item of that popup
            state.m_pSubMenu = pMenu->GetSubMenu(state.m_nIndex);
            if (state.m_pSubMenu == NULL ||
                (state.m_nID = state.m_pSubMenu->GetMenuItemID(0)) == 0 ||
                state.m_nID == (UINT)-1)
            {
                continue;     // first item of popup can't be routed to
            }
            state.DoUpdate(this, FALSE);    // popups are never auto disabled
        }
        else
        {
            // normal menu item
            // Auto enable/disable if frame window has 'm_bAutoMenuEnable'
            //    set and command is _not_ a system command.
            state.m_pSubMenu = NULL;
            state.DoUpdate(this, TRUE);
        }

        // adjust for menu deletions and additions
        UINT nCount = pMenu->GetMenuItemCount();
        if (nCount < state.m_nIndexMax)
        {
            state.m_nIndex -= (state.m_nIndexMax - nCount);
            while (state.m_nIndex < nCount &&
                pMenu->GetMenuItemID(state.m_nIndex) == state.m_nID)
            {
                state.m_nIndex++;
            }
        }
        state.m_nIndexMax = nCount;
    }
}

BOOL CPaliTextReaderDlg::OnNotify(WPARAM wParam, LPARAM lParam, LRESULT* pResult) 
{
	// TODO: Add your specialized code here and/or call the base class
	NM_HTMLVIEW * pnmHTML = (NM_HTMLVIEW *) lParam;
	LPNMHDR pnmh = (LPNMHDR) &(pnmHTML->hdr);
 
	if(pnmh->code==NM_CONTEXTMENU)
	{		
		NM_HTMLCONTEXT* pParams=(NM_HTMLCONTEXT*)lParam;
		CMenu menu;
		VERIFY(menu.LoadMenu(IDR_CONTEXTMENU));
		CMenu* pPopup = menu.GetSubMenu(0);
		ASSERT(pPopup != NULL);
		pPopup->TrackPopupMenu(TPM_LEFTALIGN, pParams->pt.x, pParams->pt.y, this);
		*pResult=TRUE;
		return TRUE;
	}	

	return CDialog::OnNotify(wParam, lParam, pResult);
}


void CPaliTextReaderDlg::OnBookmarkPage() 
{
	HWND hwChild=::GetWindow(m_hwndHtml,GW_CHILD);
	if(hwChild!=NULL)
	{
		SCROLLINFO si;
		si.cbSize=sizeof(SCROLLINFO);
		si.fMask=SIF_POS;
		::GetScrollInfo(hwChild,SB_VERT,&si);
		m_dlgSelectBook.BookmarkLine(si.nPos);
		AfxMessageBox(_T("Bookmark added successfully!"));
	}
}

void CPaliTextReaderDlg::OnUpdateBookmarkPage(CCmdUI* pCmdUI) 
{
	pCmdUI->Enable(m_bBookLoaded);
}

BOOL CPaliTextReaderDlg::PreTranslateMessage(MSG* pMsg) 
{
	static BOOL bDoIdle = TRUE;

	MSG msg;
	if(!::PeekMessage(&msg, NULL, NULL, NULL, PM_NOREMOVE) && bDoIdle)
	{
		m_pWndEmptyCB->OnUpdateCmdUI((CFrameWnd *)this, TRUE);
		bDoIdle = FALSE;
	}
	else
	{
		if(AfxGetApp()->IsIdleMessage(pMsg) && pMsg->message != 0x3FC)
		{
			bDoIdle = TRUE;
		}
	}
	
	return CDialog::PreTranslateMessage(pMsg);
}

void CPaliTextReaderDlg::OnSearch() 
{
	// TODO: Add your command handler code here
	CPaliTextEntryDlg dlg(this,_T("Search words:"),_T(""));
	if(dlg.DoModal()==IDOK)
	{
		unsigned int ChapterNodeID=theApp.GetLastBookChapterNodeID();
		if(ChapterNodeID!=65535)
		{
			DWORD dwOffset=theApp.GetLastBookOffset();
			CIndexNode objNode;
			theApp.IndexReader.GetNode(dwOffset,objNode);			
			LoadBook(objNode,ChapterNodeID,-1,dlg.m_strPaliText);
		}
	}
}

void CPaliTextReaderDlg::OnUpdateSearch(CCmdUI* pCmdUI) 
{
	// TODO: Add your command update UI handler code here
	pCmdUI->Enable(m_bBookLoaded);	
}

void CPaliTextReaderDlg::SearchText(char *pBuf,int Length,LPCTSTR lpszSearchString)
{
	TCHAR* pWideChar=NULL;
	int nBufSize=0;
	nBufSize=MultiByteToWideChar(CP_UTF8,NULL,pBuf,Length,pWideChar,nBufSize);
	pWideChar=new TCHAR[nBufSize+1];
	MultiByteToWideChar(CP_UTF8,NULL,pBuf,Length,pWideChar,nBufSize);
	pWideChar[nBufSize]=0;

	CString strText(pWideChar);

	delete[] pWideChar;
	pWideChar=0;

	int nFindResult=0;
	m_nSearchResultsCount=0;
	m_nCurrentSearchResult=0;
	CString strBookmark;
	int nSearchStringLength=lstrlen(lpszSearchString);
	LPCTSTR lpszCloseTag=_T("</a>");

	do
	{
		nFindResult=theApp.FindNoCase(strText,lpszSearchString,nFindResult);
		if(nFindResult!=-1 && nFindResult!=0)
		{
			m_nSearchResultsCount++;
			//we create a bookmark around the found text
			strBookmark.Format(_T("<a name='b%d' class='sr'>"),m_nSearchResultsCount);
			strText.Insert(nFindResult,strBookmark);
			strText.Insert(nFindResult+strBookmark.GetLength()+nSearchStringLength,lpszCloseTag);
			nFindResult=nFindResult+strBookmark.GetLength()+nSearchStringLength+lstrlen(lpszCloseTag);
		}
	}
	while(nFindResult!=-1);

	char* pNewBuf=NULL;
	nBufSize=WideCharToMultiByte(CP_UTF8,NULL,strText,-1,NULL,0,NULL,NULL);
	pNewBuf=new char[nBufSize+1];
	WideCharToMultiByte(CP_UTF8,NULL,strText,-1,pNewBuf,nBufSize,NULL,NULL);
	pNewBuf[nBufSize]=0;

	SetHtml(pNewBuf);

	delete pNewBuf;

	if(m_nSearchResultsCount>0)
	{
		::SendMessage(m_hwndHtml,DTM_ANCHORW,0,(WPARAM)_T("b1"));
		::SetFocus(m_hwndHtml);
	}
	else
	{
		AfxMessageBox(_T("No results..."));
	}

	CCeCommandBar *pCommandBar = (CCeCommandBar*)m_pWndEmptyCB;
	pCommandBar->OnUpdateCmdUI((CFrameWnd*)this,FALSE);
	pCommandBar->UpdateWindow();
}

void CPaliTextReaderDlg::OnSearchBookmarkMove(UINT nCmdID)
{
	
	switch(nCmdID)
	{
	case ID_MOVE_FIRST:
		m_nCurrentSearchResult=1;
		break;
	case ID_MOVE_PREV:
		if(m_nCurrentSearchResult>1)
			m_nCurrentSearchResult--;
		break;
	case ID_MOVE_NEXT:
		if(m_nCurrentSearchResult<m_nSearchResultsCount)
			m_nCurrentSearchResult++;
		break;
	case ID_MOVE_LAST:
		m_nCurrentSearchResult=m_nSearchResultsCount;
		break;
	}

	CString strBookmarkName;
	strBookmarkName.Format(_T("b%d"),m_nCurrentSearchResult);
	::SendMessage(m_hwndHtml,DTM_ANCHORW,0,(WPARAM)(LPCTSTR)strBookmarkName);
	::SetFocus(m_hwndHtml);
}

void CPaliTextReaderDlg::OnUpdateSearchBookmarkMove(CCmdUI *pCmdUI)
{
	pCmdUI->Enable(m_nSearchResultsCount>0);
}
