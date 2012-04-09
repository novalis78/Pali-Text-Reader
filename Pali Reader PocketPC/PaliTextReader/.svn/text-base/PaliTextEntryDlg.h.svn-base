#if !defined(AFX_PALITEXTENTRYDLG_H__AC09AA01_ECA0_4B67_995D_4E2F0D9F9FEC__INCLUDED_)
#define AFX_PALITEXTENTRYDLG_H__AC09AA01_ECA0_4B67_995D_4E2F0D9F9FEC__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// PaliTextEntryDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CPaliTextEntryDlg dialog

class CPaliTextEntryDlg : public CDialog
{
// Construction
public:
	CPaliTextEntryDlg(CWnd* pParent, LPCTSTR lpszCaption,LPCTSTR lpszInitialText);   // standard constructor
	
// Dialog Data
	//{{AFX_DATA(CPaliTextEntryDlg)
	enum { IDD = IDD_PALI_ENTRY };
	CEdit	m_ctlPaliText;
	CButton	m_ctlButtonUU;
	CButton	m_ctlButtonTDot;
	CButton	m_ctlButtonNTilde;
	CButton	m_ctlButtonNDotBelow;
	CButton	m_ctlButtonNDotAbove;
	CButton	m_ctlButtonMDot;
	CButton	m_ctlButtonLDot;
	CButton	m_ctlButtonII;
	CButton	m_ctlButtonDDot;
	CButton	m_ctlButtonAA;
	CString	m_strHint;
	CString	m_strPaliText;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPaliTextEntryDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	void OnPaliButtonClicked(UINT nCmdID);

	// Generated message map functions
	//{{AFX_MSG(CPaliTextEntryDlg)
	virtual void OnOK();
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PALITEXTENTRYDLG_H__AC09AA01_ECA0_4B67_995D_4E2F0D9F9FEC__INCLUDED_)
