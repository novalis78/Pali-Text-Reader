#if !defined(AFX_PREFERENCESDLG_H__FE34E689_95E2_43DB_B72B_1192312038E8__INCLUDED_)
#define AFX_PREFERENCESDLG_H__FE34E689_95E2_43DB_B72B_1192312038E8__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// PreferencesDlg.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CPreferencesDlg dialog

class CPreferencesDlg : public CDialog
{
// Construction
public:
	CPreferencesDlg(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CPreferencesDlg)
	enum { IDD = IDD_PREFERENCES };
	BOOL	m_bOpenLast;
	CString	m_strLibraryLocation;
	CString	m_strDictionaryFile;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPreferencesDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

	virtual void OnOK();
// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CPreferencesDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnLibraryBrowse();
	afx_msg void OnDictionaryBrowse();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PREFERENCESDLG_H__FE34E689_95E2_43DB_B72B_1192312038E8__INCLUDED_)
