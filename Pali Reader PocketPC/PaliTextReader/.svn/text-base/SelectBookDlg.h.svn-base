#if !defined(AFX_SELECTBOOKDLG_H__62C422DF_F6A8_4D08_98CD_87BFBEA2F592__INCLUDED_)
#define AFX_SELECTBOOKDLG_H__62C422DF_F6A8_4D08_98CD_87BFBEA2F592__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SelectBookDlg.h : header file
//
#include "BookmarkDoc.h"	// Added by ClassView
#include "IndexReader.h"	// Added by ClassView
/////////////////////////////////////////////////////////////////////////////
// CSelectBookDlg dialog

class CSelectBookDlg : public CDialog
{
// Construction
public:
	CSelectBookDlg(CWnd* pParent = NULL);   // standard constructor

	void GetSelectedNode(CIndexNode& rNode);
	void GetSelectedNode(CIndexNode& rNode,int& Line,unsigned short& ChapterNodeID);
	void BookmarkLine(int Line);

// Dialog Data
	//{{AFX_DATA(CSelectBookDlg)
	enum { IDD = IDD_SELECTBOOK };
	CTreeCtrl	m_ctlTree;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSelectBookDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	BOOL FindChildNodeRecursive(HTREEITEM hParent,LPCTSTR lpszSearchText);
	void LoadChildNodesRecursive(HTREEITEM hParent);
	void LoadChildNodes(HTREEITEM hParent);
	void LoadChildNodes(HTREEITEM hParent,unsigned short ParentID);
	CBookmarkDoc* m_pBookmarkDoc;	
	CFont m_objPaliFont;

	// Generated message map functions
	//{{AFX_MSG(CSelectBookDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnOk();
	afx_msg void OnCancel();
	afx_msg void OnItemexpandingBooks(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnEndlabeleditBooks(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnBeginlabeleditBooks(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg void OnFindItem();
	//}}AFX_MSG
	afx_msg void OnBookmarkItem();
	afx_msg BOOL OnContextMenu(NMHDR *pNotifyStruct, LRESULT *result);
	afx_msg void OnDelete();
	afx_msg void OnRename();

	DECLARE_MESSAGE_MAP()
private:
	void LoadBookmarks();
	HTREEITEM m_htBookmarks;
	HTREEITEM m_htClickedItem;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SELECTBOOKDLG_H__62C422DF_F6A8_4D08_98CD_87BFBEA2F592__INCLUDED_)
