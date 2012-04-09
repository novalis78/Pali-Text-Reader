// PaliTextReaderDlg.h : header file
//

#if !defined(AFX_PALITEXTREADERDLG_H__25D24DEC_72CD_4F3C_B1B9_7AD42E657242__INCLUDED_)
#define AFX_PALITEXTREADERDLG_H__25D24DEC_72CD_4F3C_B1B9_7AD42E657242__INCLUDED_

#include "SelectBookDlg.h"	// Added by ClassView
#include "DictionaryCtl.h"	// Added by ClassView
#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderDlg dialog

class CPaliTextReaderDlg : public CDialog
{
// Construction
public:
	CPaliTextReaderDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPaliTextReaderDlg)
	enum { IDD = IDD_PALITEXTREADER_DIALOG };
	CStatic	m_ctlDictionaryPlaceholder;
	CComboBox	m_ctlChapterSelector;
	CStatic	m_ctlTextViewerPlaceholder;
	//}}AFX_DATA
	CDictionaryCtl m_ctlDictionary;

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPaliTextReaderDlg)
	public:
	virtual BOOL PreTranslateMessage(MSG* pMsg);
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	virtual BOOL OnNotify(WPARAM wParam, LPARAM lParam, LRESULT* pResult);
	//}}AFX_VIRTUAL

// Implementation
protected:
	int m_nCurrentSearchResult;
	int m_nSearchResultsCount;
	void SearchText(char* pBuf,int Length,LPCTSTR lpszSearchString);
	BOOL m_bBookLoaded;
	CFont m_objPaliFont;
	BOOL m_bDictionaryVisible;
	void ToggleDictionary();
	LPWSTR CopySelection();
	void LoadBook(CIndexNode& rNode,unsigned int ChapterNodeID, int Line,LPCTSTR lpszSearchString=NULL);

		//Initializes HTML view control
	void CreateHtmlWindow();
	void SetHtml(char* pHtml);

	//Instance of dll to support HTML view control
	static HINSTANCE m_HtmlViewInstance;

	//Handle to HTML view control
	HWND m_hwndHtml;

	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CPaliTextReaderDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnExit();
	afx_msg void OnSelectbook();
	afx_msg void OnPreferences();
	afx_msg void OnSelchangeChapterselector();
	afx_msg void OnShowdic();
	afx_msg void OnUpdateShowdic(CCmdUI* pCmdUI);
	afx_msg void OnInitMenuPopup(CMenu* pPopupMenu, UINT nIndex, BOOL bSysMenu);
	afx_msg void OnBookmarkPage();
	afx_msg void OnUpdateBookmarkPage(CCmdUI* pCmdUI);
	afx_msg void OnSearch();
	afx_msg void OnUpdateSearch(CCmdUI* pCmdUI);
	//}}AFX_MSG
	LPARAM afx_msg OnBookSelected(WPARAM wParam, LPARAM lParam);
	afx_msg void OnCopy();
	afx_msg void OnTranslate();
	afx_msg void OnUpdateSearchBookmarkMove(CCmdUI* pCmdUI);
	afx_msg void OnSearchBookmarkMove(UINT nCmdID);

	DECLARE_MESSAGE_MAP()
private:
	CSelectBookDlg m_dlgSelectBook;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft eMbedded Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PALITEXTREADERDLG_H__25D24DEC_72CD_4F3C_B1B9_7AD42E657242__INCLUDED_)
