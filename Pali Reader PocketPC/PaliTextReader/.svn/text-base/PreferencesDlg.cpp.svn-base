// PreferencesDlg.cpp : implementation file
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "PreferencesDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CPreferencesDlg dialog

extern CPaliTextReaderApp theApp;

CPreferencesDlg::CPreferencesDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPreferencesDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPreferencesDlg)
	m_bOpenLast = FALSE;
	m_strLibraryLocation = _T("");
	m_strDictionaryFile = _T("");
	//}}AFX_DATA_INIT
}


void CPreferencesDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPreferencesDlg)
	DDX_Check(pDX, IDC_OPENLAST, m_bOpenLast);
	DDX_Text(pDX, IDC_LIBRARY_LOCATION, m_strLibraryLocation);
	DDX_Text(pDX, IDC_DICTIONARY_FILE, m_strDictionaryFile);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CPreferencesDlg, CDialog)
	//{{AFX_MSG_MAP(CPreferencesDlg)
	ON_BN_CLICKED(IDC_LIBRARY_BROWSE, OnLibraryBrowse)
	ON_BN_CLICKED(IDC_DICTIONARY_BROWSE, OnDictionaryBrowse)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPreferencesDlg message handlers

BOOL CPreferencesDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	// TODO: Add extra initialization here
	m_strLibraryLocation=theApp.GetLibraryLocation();
	m_strDictionaryFile=theApp.GetDictionaryFileName();
	m_bOpenLast=theApp.GetOpenLastBook();

	UpdateData(FALSE);
	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}

void CPreferencesDlg::OnLibraryBrowse() 
{
	CFileDialog dlg(TRUE,NULL,NULL,OFN_HIDEREADONLY,_T("zip files (*.zip)|*.zip||"));
	if(dlg.DoModal()==IDOK)
	{
		m_strLibraryLocation=dlg.GetPathName();
		m_strLibraryLocation=m_strLibraryLocation.Left(m_strLibraryLocation.ReverseFind('\\')+1);
		UpdateData(FALSE);
	}
}

void CPreferencesDlg::OnOK()
{
	UpdateData(TRUE);
	theApp.SetLibraryLocation(m_strLibraryLocation);
	theApp.SetOpenLastBook(m_bOpenLast);
	theApp.SetDictionaryFileName(m_strDictionaryFile);
	CDialog::OnOK();
}
void CPreferencesDlg::OnDictionaryBrowse() 
{
	// TODO: Add your control notification handler code here
	CFileDialog dlg(TRUE,NULL,NULL,OFN_HIDEREADONLY,_T("text files (*.txt)|*.txt||"));
	if(dlg.DoModal()==IDOK)
	{
		m_strDictionaryFile=dlg.GetPathName();
		UpdateData(FALSE);
	}	
}
