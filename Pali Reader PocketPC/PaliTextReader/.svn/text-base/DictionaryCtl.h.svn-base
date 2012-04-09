#if !defined(AFX_DICTIONARYCTL_H__34D0264F_838C_45F3_8D2E_CF9F4233DA90__INCLUDED_)
#define AFX_DICTIONARYCTL_H__34D0264F_838C_45F3_8D2E_CF9F4233DA90__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// DictionaryCtl.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CDictionaryCtl window
#include <stdio.h>

class CDictionaryCtl : public CTabCtrl
{
// Construction
public:
	CDictionaryCtl();

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDictionaryCtl)
	//}}AFX_VIRTUAL

// Implementation
public:
	void LookUp(LPCTSTR lpszTerm);
	virtual ~CDictionaryCtl();

	// Generated message map functions
protected:	
	afx_msg void OnPaliButton();
	CButton m_ctlPaliTextButton;
	void LoadIndex();
	fpos_t m_arDictionaryIndex[31];
	BOOL m_bIndexLoaded;
	CButton m_ctlFindButton;
	CEdit m_ctlSearchField;
	int GetPaliAlphaPos(TCHAR ch);
	CFont m_objFont;
	CEdit m_ctlTranslation;
	CListBox m_ctlTermList;
	//{{AFX_MSG(CDictionaryCtl)
	afx_msg void OnSelchange(NMHDR* pNMHDR, LRESULT* pResult);
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	//}}AFX_MSG
	afx_msg void OnTermSelect();
	afx_msg void OnFind();
	afx_msg void OnUndo();
	afx_msg void OnDelete();
	afx_msg void OnPaste();
	afx_msg void OnCopy();
	afx_msg void OnCut();
	afx_msg void OnEditContextMenu(NMHDR* pNotifyStruct,LRESULT* result);

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DICTIONARYCTL_H__34D0264F_838C_45F3_8D2E_CF9F4233DA90__INCLUDED_)
