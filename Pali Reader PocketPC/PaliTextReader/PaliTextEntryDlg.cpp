// PaliTextEntryDlg.cpp : implementation file
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "PaliTextEntryDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CPaliTextEntryDlg dialog


CPaliTextEntryDlg::CPaliTextEntryDlg(CWnd* pParent,LPCTSTR lpszCaption,LPCTSTR lpszInitialText)
	: CDialog(CPaliTextEntryDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPaliTextEntryDlg)
	m_strHint = _T("");
	m_strPaliText = _T("");
	//}}AFX_DATA_INIT

	m_strHint=lpszCaption;
	m_strPaliText=lpszInitialText;
}


void CPaliTextEntryDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPaliTextEntryDlg)
	DDX_Control(pDX, IDC_PALI_TEXT, m_ctlPaliText);
	DDX_Control(pDX, IDC_BUTTON_UU, m_ctlButtonUU);
	DDX_Control(pDX, IDC_BUTTON_TDOT, m_ctlButtonTDot);
	DDX_Control(pDX, IDC_BUTTON_NTILDE, m_ctlButtonNTilde);
	DDX_Control(pDX, IDC_BUTTON_NDOTBELOW, m_ctlButtonNDotBelow);
	DDX_Control(pDX, IDC_BUTTON_NDOTABOVE, m_ctlButtonNDotAbove);
	DDX_Control(pDX, IDC_BUTTON_MDOT, m_ctlButtonMDot);
	DDX_Control(pDX, IDC_BUTTON_LDOT, m_ctlButtonLDot);
	DDX_Control(pDX, IDC_BUTTON_II, m_ctlButtonII);
	DDX_Control(pDX, IDC_BUTTON_DDOT, m_ctlButtonDDot);
	DDX_Control(pDX, IDC_BUTTON_AA, m_ctlButtonAA);
	DDX_Text(pDX, IDC_HINT, m_strHint);
	DDX_Text(pDX, IDC_PALI_TEXT, m_strPaliText);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPaliTextEntryDlg, CDialog)
	//{{AFX_MSG_MAP(CPaliTextEntryDlg)
	//}}AFX_MSG_MAP
	ON_CONTROL_RANGE(BN_CLICKED, IDC_BUTTON_AA, IDC_BUTTON_LDOT, OnPaliButtonClicked)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPaliTextEntryDlg message handlers

void CPaliTextEntryDlg::OnPaliButtonClicked(UINT nCmdID)
{
	TCHAR chPaliChar;
	switch(nCmdID)
	{
		case IDC_BUTTON_AA:
			chPaliChar=0x0101;
			break;
		case IDC_BUTTON_II:
			chPaliChar=0x012B;
			break;
		case IDC_BUTTON_UU:
			chPaliChar=0x016B;
			break;
		case IDC_BUTTON_MDOT:
			chPaliChar=0x1E43;
			break;
		case IDC_BUTTON_NDOTABOVE:
			chPaliChar=0x1E45;
			break;
		case IDC_BUTTON_NTILDE:
			chPaliChar=0x00F1;
			break;
		case IDC_BUTTON_TDOT:
			chPaliChar=0x1E6D;
			break;
		case IDC_BUTTON_DDOT:
			chPaliChar=0x1E0D;
			break;
		case IDC_BUTTON_NDOTBELOW:
			chPaliChar=0x1E47;
			break;
		case IDC_BUTTON_LDOT:
			chPaliChar=0x1E37;
			break;
	}

	UpdateData(TRUE);
	m_strPaliText+=chPaliChar;
	UpdateData(FALSE);
	m_ctlPaliText.SetFocus();
	m_ctlPaliText.SetSel(m_strPaliText.GetLength(),m_strPaliText.GetLength());
}

void CPaliTextEntryDlg::OnOK() 
{
	// TODO: Add extra validation here
	UpdateData(TRUE);
	CDialog::OnOK();
}

BOOL CPaliTextEntryDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	// TODO: Add extra initialization here
		
	CString strCaption;
	strCaption=(TCHAR)0x0101;
	m_ctlButtonAA.SetWindowText(strCaption);
	strCaption=(TCHAR)0x012B;
	m_ctlButtonII.SetWindowText(strCaption);
	strCaption=(TCHAR)0x016B;
	m_ctlButtonUU.SetWindowText(strCaption);
	strCaption=(TCHAR)0x1E43;
	m_ctlButtonMDot.SetWindowText(strCaption);
	strCaption=(TCHAR)0x1E45;
	m_ctlButtonNDotAbove.SetWindowText(strCaption);
	strCaption=(TCHAR)0x00F1;
	m_ctlButtonNTilde.SetWindowText(strCaption);
	strCaption=(TCHAR)0x1E6D;
	m_ctlButtonTDot.SetWindowText(strCaption);
	strCaption=(TCHAR)0x1E0D;
	m_ctlButtonDDot.SetWindowText(strCaption);
	strCaption=(TCHAR)0x1E47;
	m_ctlButtonNDotBelow.SetWindowText(strCaption);
	strCaption=(TCHAR)0x1E37;
	m_ctlButtonLDot.SetWindowText(strCaption);
	
	m_ctlPaliText.SetFocus();
	m_ctlPaliText.SetSel(m_strPaliText.GetLength(),m_strPaliText.GetLength());

	return FALSE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX Property Pages should return FALSE
}
