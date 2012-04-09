// SelectBookDlg.cpp : implementation file
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "SelectBookDlg.h"
#include <stdlib.h>
#include "palitextentrydlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

extern CPaliTextReaderApp theApp;
/////////////////////////////////////////////////////////////////////////////
// CSelectBookDlg dialog
const LPCTSTR BookmarkFileName=_T("bookmarks.dat");

CSelectBookDlg::CSelectBookDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSelectBookDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSelectBookDlg)
	//}}AFX_DATA_INIT
	m_pBookmarkDoc=NULL;
	m_htClickedItem=NULL;
}


void CSelectBookDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSelectBookDlg)
	DDX_Control(pDX, IDC_BOOKS, m_ctlTree);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CSelectBookDlg, CDialog)
	//{{AFX_MSG_MAP(CSelectBookDlg)
	ON_BN_CLICKED(IDC_OK, OnOk)
	ON_NOTIFY(TVN_ITEMEXPANDING, IDC_BOOKS, OnItemexpandingBooks)
	ON_NOTIFY(TVN_ENDLABELEDIT, IDC_BOOKS, OnEndlabeleditBooks)
	ON_NOTIFY(TVN_BEGINLABELEDIT, IDC_BOOKS, OnBeginlabeleditBooks)
	ON_COMMAND(ID_FIND_ITEM, OnFindItem)
	ON_BN_CLICKED(IDC_CANCEL, OnCancel)
	//}}AFX_MSG_MAP
	ON_NOTIFY(GN_CONTEXTMENU, IDC_BOOKS, OnContextMenu)
	ON_COMMAND(IDM_BOOKMARK,OnBookmarkItem)
	ON_COMMAND(IDM_RENAME,OnRename)
	ON_COMMAND(IDM_DELETEBOOKMARK,OnDelete)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSelectBookDlg message handlers

BOOL CSelectBookDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	// TODO: Add extra initialization here
	SHDoneButton(m_hWnd, SHDB_HIDE);

	LOGFONT lf;
	ZeroMemory(&lf,sizeof(LOGFONT));
	lf.lfHeight=16;
	lstrcpy(lf.lfFaceName,_T("CN-Times"));	
	m_objPaliFont.CreateFontIndirect(&lf);
	m_ctlTree.SetFont(&m_objPaliFont);

	CImageList* pImageList=new CImageList();
	pImageList->Create(IDB_TREEIMAGES,16,16,(COLORREF)0xFFFFFF);

	m_ctlTree.SetImageList(pImageList,TVSIL_NORMAL);

	HTREEITEM hRoot=m_ctlTree.InsertItem(_T("Pali text library"),0,0);

	LoadChildNodes(hRoot,0);

	m_htBookmarks=m_ctlTree.InsertItem(_T("Bookmarks"),0,0,hRoot);
	m_ctlTree.InsertItem(_T(""),m_htBookmarks);

	m_ctlTree.Expand(hRoot,TVE_EXPAND);
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void CSelectBookDlg::OnOk() 
{
	// TODO: Add your control notification handler code here
	HTREEITEM hSelected=m_ctlTree.GetSelectedItem();
	LPCTSTR lpszError=_T("Select a chapter or paragraph to open");
	if(hSelected!=NULL)
	{
		if(hSelected==m_ctlTree.GetRootItem() || hSelected==m_htBookmarks)
			AfxMessageBox(lpszError);
		else
		{
			HTREEITEM htParent=m_ctlTree.GetParentItem(hSelected);
			if(htParent!=m_htBookmarks)
			{
				CIndexNode objNode;
				GetSelectedNode(objNode);
				if(objNode.LibFileIndex==255)
				{
					AfxMessageBox(lpszError);
					return;
				}
			}

			ShowWindow(SW_HIDE);
			::SendMessage(GetParent()->m_hWnd,SELBOOKDLGCLOSED,TRUE,NULL);			
		}
	}
}

void CSelectBookDlg::OnCancel() 
{
	// TODO: Add your control notification handler code here
	ShowWindow(SW_HIDE);
	::SendMessage(GetParent()->m_hWnd,SELBOOKDLGCLOSED,FALSE,NULL);
}


void CSelectBookDlg::OnItemexpandingBooks(NMHDR* pNMHDR, LRESULT* pResult) 
{
	NM_TREEVIEW* pNMTreeView = (NM_TREEVIEW*)pNMHDR;
	// TODO: Add your control notification handler code here
	HTREEITEM hCurItem=pNMTreeView->itemNew.hItem;

	if(hCurItem==m_htBookmarks)
		LoadBookmarks();
	else
		LoadChildNodes(hCurItem);

	*pResult = 0;
}

void CSelectBookDlg::LoadBookmarks()
{
	HTREEITEM htChild=m_ctlTree.GetChildItem(m_htBookmarks);
	if(m_ctlTree.GetItemText(htChild)==_T(""))
	{
		m_ctlTree.DeleteItem(htChild);

		CString strBookmarkFile=theApp.AppDirectory+BookmarkFileName;
		if(m_pBookmarkDoc==NULL)
		{
			m_pBookmarkDoc=new CBookmarkDoc();
			
			CFileStatus objStatus;
			if(CFile::GetStatus(strBookmarkFile,objStatus))
				m_pBookmarkDoc->OnOpenDocument(strBookmarkFile);

			POSITION pos=m_pBookmarkDoc->GetHeadBookmarkPosition();

			while(pos!=NULL)
			{
				POSITION posOld=pos;
				CBookmark& rBookmark=m_pBookmarkDoc->GetNextBookmark(pos);
				HTREEITEM htBookmark=m_ctlTree.InsertItem(rBookmark.BookmarkName,4,4,m_htBookmarks);
				m_ctlTree.SetItemData(htBookmark,(DWORD)posOld);
			}
		}
	}
}

BOOL CSelectBookDlg::OnContextMenu(NMHDR *pNotifyStruct, LRESULT *result)
{
	PNMRGINFO pInfo=(PNMRGINFO)pNotifyStruct;

	CMenu Menu;
	Menu.LoadMenu(IDR_TREEMENU);
		
	POINT ptClick;
	ptClick.x=pInfo->ptAction.x;
	ptClick.y=pInfo->ptAction.y;
	m_ctlTree.ScreenToClient(&ptClick);

	UINT nFlags=0;
	m_htClickedItem=m_ctlTree.HitTest(CPoint(ptClick),&nFlags);

	if(m_htClickedItem!=NULL)
	{
		if(m_htClickedItem!=m_htBookmarks && m_htClickedItem!=m_ctlTree.GetRootItem())
		{
			HTREEITEM htParent=m_ctlTree.GetParentItem(m_htClickedItem);
			if(htParent==m_htBookmarks)
			{
				Menu.GetSubMenu(0)->EnableMenuItem(IDM_RENAME,MF_BYCOMMAND | MF_ENABLED);
				Menu.GetSubMenu(0)->EnableMenuItem(IDM_DELETEBOOKMARK,MF_BYCOMMAND | MF_ENABLED);
			}
			else
			{
				CIndexNode objNode;
				DWORD dwOffset=m_ctlTree.GetItemData(m_htClickedItem);
				theApp.IndexReader.GetNode(dwOffset,objNode);
				if(objNode.LibFileIndex!=255)
					Menu.GetSubMenu(0)->EnableMenuItem(IDM_BOOKMARK,MF_BYCOMMAND | MF_ENABLED); 

				Menu.GetSubMenu(0)->EnableMenuItem(ID_FIND_ITEM,MF_BYCOMMAND | MF_ENABLED); 
			}
			m_ctlTree.SelectItem(m_htClickedItem);
		}
	}

	Menu.GetSubMenu(0)->TrackPopupMenu(TPM_LEFTALIGN, pInfo->ptAction.x, pInfo->ptAction.y, this);
	*result = TRUE; //This is important!
	return TRUE;

}

void CSelectBookDlg::OnBookmarkItem()
{
	if(m_htClickedItem!=NULL)
	{
		LoadBookmarks();//in case they are not loaded yet

		DWORD dwOffset=m_ctlTree.GetItemData(m_htClickedItem);
		CString strText=m_ctlTree.GetItemText(m_htClickedItem);
		HTREEITEM htNew=m_ctlTree.InsertItem(strText,4,4,m_htBookmarks);	

		CIndexNode objChapterNode;
		do
		{
			DWORD dwOffset=m_ctlTree.GetItemData(m_htClickedItem);
			m_htClickedItem=m_ctlTree.GetParentItem(m_htClickedItem);
			theApp.IndexReader.GetNode(dwOffset,objChapterNode);
		}
		while(!objChapterNode.BookmarkName.IsEmpty());

		CBookmark objBookmark(strText,dwOffset,objChapterNode.NodeID,0);
		POSITION pos=m_pBookmarkDoc->AddBookmark(objBookmark);
		m_ctlTree.SetItemData(htNew,(DWORD)pos);

		m_pBookmarkDoc->OnSaveDocument(theApp.AppDirectory+BookmarkFileName);
	}
}

void CSelectBookDlg::OnRename()
{
	CEdit* pEdit=m_ctlTree.EditLabel(m_htClickedItem);
}

void CSelectBookDlg::OnDelete()
{
	if(AfxMessageBox(_T("Are you sure?"),MB_YESNO|MB_ICONSTOP)==IDYES)
	{
		DWORD dwData=m_ctlTree.GetItemData(m_htClickedItem);
		m_pBookmarkDoc->DeleteBookmark((POSITION)dwData);
		m_pBookmarkDoc->OnSaveDocument(theApp.AppDirectory+BookmarkFileName);
		m_ctlTree.DeleteItem(m_htClickedItem);		
	}
}

void CSelectBookDlg::OnEndlabeleditBooks(NMHDR* pNMHDR, LRESULT* pResult) 
{
	TV_DISPINFO* pTVDispInfo = (TV_DISPINFO*)pNMHDR;
	// TODO: Add your control notification handler code here

	if(pTVDispInfo->item.pszText!=NULL)	
	{		
		if(lstrlen(pTVDispInfo->item.pszText)>0)
		{
			DWORD dwData=m_ctlTree.GetItemData(pTVDispInfo->item.hItem);
			POSITION pos=(POSITION)dwData;
			CBookmark& rBookmark=m_pBookmarkDoc->GetNextBookmark(pos);
			rBookmark.BookmarkName=CString(pTVDispInfo->item.pszText);
			m_pBookmarkDoc->OnSaveDocument(theApp.AppDirectory+BookmarkFileName);

			*pResult=TRUE;
		}
		else
			*pResult = 0;
			
	}
	else
		*pResult = 0;
}


void CSelectBookDlg::OnBeginlabeleditBooks(NMHDR* pNMHDR, LRESULT* pResult) 
{
	TV_DISPINFO* pTVDispInfo = (TV_DISPINFO*)pNMHDR;
	// TODO: Add your control notification handler code here
	HTREEITEM htParent=m_ctlTree.GetParentItem(pTVDispInfo->item.hItem);
	if(htParent==m_htBookmarks)	
		*pResult = 0;
	else
		*pResult=TRUE;
}

void CSelectBookDlg::BookmarkLine(int Line)
{
	DWORD dwOffset=theApp.GetLastBookOffset();
	unsigned int ChapterNodeID=theApp.GetLastBookChapterNodeID();

	LoadBookmarks();//in case they are not loaded yet

	CIndexNode objNode;
	theApp.IndexReader.GetNode(dwOffset,objNode);
	CString strItemName;
	strItemName.Format(_T(" - line %d"),Line/20);
	strItemName=objNode.Name+strItemName;
	HTREEITEM htNew=m_ctlTree.InsertItem(strItemName,4,4,m_htBookmarks);	

	CBookmark objBookmark(strItemName,dwOffset,ChapterNodeID,Line);
	POSITION pos=m_pBookmarkDoc->AddBookmark(objBookmark);
	m_ctlTree.SetItemData(htNew,(DWORD)pos);

	m_pBookmarkDoc->OnSaveDocument(theApp.AppDirectory+BookmarkFileName);	
}

void CSelectBookDlg::LoadChildNodes(HTREEITEM hParent,unsigned short ParentID)
{
	NodeList objNodeList;
	theApp.IndexReader.GetChildNodes(ParentID,objNodeList);
	POSITION pos=objNodeList.GetHeadPosition();
	while(pos!=NULL)
	{
		CIndexNode& rNode=objNodeList.GetNext(pos);

		int nImageIndex,nSelectedImageIndex;
		if(rNode.LibFileIndex==255)
		{
			nImageIndex=0;
			nSelectedImageIndex=1;
		}
		else
		{
			if(rNode.BookmarkName.IsEmpty())
			{
				nImageIndex=2;
				nSelectedImageIndex=2;
			}
			else
			{
				nImageIndex=3;
				nSelectedImageIndex=3;
			}
		}

		HTREEITEM hItem=m_ctlTree.InsertItem(rNode.Name,nImageIndex,nSelectedImageIndex,hParent);
		m_ctlTree.SetItemData(hItem,rNode.Offset);
		if(rNode.HasChildren)
			m_ctlTree.InsertItem(_T(""),hItem);
	}
}

void CSelectBookDlg::LoadChildNodes(HTREEITEM hParent)
{
	HTREEITEM htChild=m_ctlTree.GetChildItem(hParent);
	if(m_ctlTree.GetItemText(htChild)==_T(""))
	{//this pitaka is not yet loaded, load it
		m_ctlTree.DeleteItem(htChild);
		DWORD dwOffset=m_ctlTree.GetItemData(hParent);
		LoadChildNodes(hParent,theApp.IndexReader.GetNodeID(dwOffset));
	}
}

void CSelectBookDlg::GetSelectedNode(CIndexNode& rNode)
{
	int Line;
	unsigned short ChapterNodeID;
	GetSelectedNode(rNode,Line,ChapterNodeID);
}

void CSelectBookDlg::GetSelectedNode(CIndexNode& rNode,int& Line,unsigned short& ChapterNodeID)
{
	HTREEITEM hSelected=m_ctlTree.GetSelectedItem();
	if(hSelected!=NULL)
	{
		DWORD dwOffset=m_ctlTree.GetItemData(hSelected);

		if(m_ctlTree.GetParentItem(hSelected)==m_htBookmarks)
		{
			POSITION pos=(POSITION)dwOffset;
			CBookmark& rBookmark=m_pBookmarkDoc->GetNextBookmark(pos);
			theApp.IndexReader.GetNode(rBookmark.NodeOffset,rNode);
			Line=rBookmark.Line;
			ChapterNodeID=rBookmark.ChapterNodeID;
		}
		else
		{
			theApp.IndexReader.GetNode(dwOffset,rNode);

			CIndexNode objChapterNode;		
			do
			{
				DWORD dwOffset=m_ctlTree.GetItemData(hSelected);
				hSelected=m_ctlTree.GetParentItem(hSelected);
				theApp.IndexReader.GetNode(dwOffset,objChapterNode);
			}
			while(!objChapterNode.BookmarkName.IsEmpty());
			ChapterNodeID=objChapterNode.NodeID;
		}
	}
}

void CSelectBookDlg::OnFindItem() 
{
	CPaliTextEntryDlg dlg(this,_T("Search book/sutta/paragraph:"),_T(""));
	if(dlg.DoModal()==IDOK && !dlg.m_strPaliText.IsEmpty())
	{
		CWaitCursor wait;
		LoadChildNodesRecursive(m_htClickedItem);
		BOOL bResult=FindChildNodeRecursive(m_htClickedItem,dlg.m_strPaliText);
		if(!bResult)
			AfxMessageBox(_T("No items found"));

		m_ctlTree.SetFocus();
	}
}

void CSelectBookDlg::LoadChildNodesRecursive(HTREEITEM hParent)
{
	if(m_ctlTree.ItemHasChildren(hParent))
	{
		LoadChildNodes(hParent);
		HTREEITEM htChild=m_ctlTree.GetChildItem(hParent);
		while(htChild!=NULL)
		{
			LoadChildNodesRecursive(htChild);
			htChild=m_ctlTree.GetNextSiblingItem(htChild);
		}
	}	
}

BOOL CSelectBookDlg::FindChildNodeRecursive(HTREEITEM hParent, LPCTSTR lpszSearchText)
{
	BOOL bResult=FALSE;

	CString strItemText=m_ctlTree.GetItemText(hParent);
	if(theApp.FindNoCase(strItemText,lpszSearchText,0)!=-1)
	{
		m_ctlTree.EnsureVisible(hParent);
		m_ctlTree.SelectItem(hParent);
		bResult=TRUE;
	}
	else
		if(m_ctlTree.ItemHasChildren(hParent))
		{
			HTREEITEM htChild=m_ctlTree.GetChildItem(hParent);
			while(htChild!=NULL && !bResult)
			{
				bResult=FindChildNodeRecursive(htChild,lpszSearchText);
				htChild=m_ctlTree.GetNextSiblingItem(htChild);
			}
		}	

	return bResult;
}
