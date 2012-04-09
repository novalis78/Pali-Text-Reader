// DictionaryCtl.cpp : implementation file
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "DictionaryCtl.h"
#include "components\zip\unzip.h"
#include "palitextentrydlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

extern CPaliTextReaderApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CDictionaryCtl

CDictionaryCtl::CDictionaryCtl()
{
	m_bIndexLoaded=FALSE;
}

CDictionaryCtl::~CDictionaryCtl()
{
}


BEGIN_MESSAGE_MAP(CDictionaryCtl, CTabCtrl)
	//{{AFX_MSG_MAP(CDictionaryCtl)
	ON_NOTIFY_REFLECT(TCN_SELCHANGE, OnSelchange)
	ON_WM_CREATE()
	//}}AFX_MSG_MAP
	ON_LBN_SELCHANGE(IDC_TERMLIST,OnTermSelect)
	ON_BN_CLICKED(IDC_FINDBUTTON,OnFind)
	ON_BN_CLICKED(IDC_PALITEXTBUTTON,OnPaliButton)
	ON_NOTIFY(GN_CONTEXTMENU,IDC_SEARCHFIELD,OnEditContextMenu)
	ON_COMMAND(ID_EDIT_CUT,OnCut)
	ON_COMMAND(ID_EDIT_COPY,OnCopy)
	ON_COMMAND(ID_EDIT_PASTE,OnPaste)
	ON_COMMAND(ID_EDIT_UNDO,OnUndo)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDictionaryCtl message handlers
enum CompareResult
{
		MatchFound,
		NextWord,
		NoMoreMatches
};

void CDictionaryCtl::OnTermSelect()
{
	LPCTSTR pStr=(LPCTSTR)m_ctlTermList.GetItemDataPtr(m_ctlTermList.GetCurSel());
	CString strTranslation(pStr);
	strTranslation.Replace(_T("[c]"),_T("\r\n\r\n"));
	strTranslation.Replace(_T("[/c]"),_T("\r\n"));
	strTranslation.TrimLeft();
	m_ctlTranslation.SetWindowText(strTranslation);
	SetCurSel(1);

	LRESULT result;
	OnSelchange(NULL,&result);
}

void CDictionaryCtl::OnSelchange(NMHDR* pNMHDR, LRESULT* pResult) 
{
	// TODO: Add your control notification handler code here
	int nCurSel=GetCurSel();

	m_ctlFindButton.ShowWindow((nCurSel==0) ? SW_SHOW : SW_HIDE);
	m_ctlPaliTextButton.ShowWindow((nCurSel==0) ? SW_SHOW : SW_HIDE);
	m_ctlSearchField.ShowWindow((nCurSel==0) ? SW_SHOW : SW_HIDE);
	m_ctlTermList.ShowWindow((nCurSel==0) ? SW_SHOW : SW_HIDE);
	m_ctlTranslation.ShowWindow((nCurSel==0) ? SW_HIDE : SW_SHOW);
	
	*pResult = 0;
}

void CDictionaryCtl::LookUp(LPCTSTR lpszTerm)
{
	if(!m_bIndexLoaded)
		LoadIndex();

	for(int i=0;i<m_ctlTermList.GetCount();i++)		
		delete m_ctlTermList.GetItemDataPtr(i);

	m_ctlTermList.ResetContent();	
	CString strTerm(lpszTerm);
	strTerm.MakeLower();

	m_ctlSearchField.SetWindowText(strTerm);

	int nSelTextLength=strTerm.GetLength();

	CString strFileName=theApp.GetDictionaryFileName();
	int nIndexPos=GetPaliAlphaPos(strTerm[0]);

	FILE *stream;
	char line[500];
	TCHAR UniBuf[500];

	if( (stream=_wfopen((LPCTSTR)strFileName,_T("r"))) != NULL )
	{	
		fsetpos(stream,&m_arDictionaryIndex[nIndexPos]);

		while(fgets(line,500,stream)!=NULL)
		{
			CompareResult eResult=NextWord;

			MultiByteToWideChar(CP_UTF8,NULL,(LPSTR)&line,-1,(LPWSTR)&UniBuf,500);
			for(int i=0;i<nSelTextLength;i++)
			{
				int nTermCharIndex=GetPaliAlphaPos(strTerm[i]);
				int nDicCharIndex=GetPaliAlphaPos(UniBuf[i]);

				if(UniBuf[i]==_T('\t') || UniBuf[i]==_T(' ')) //end of term in the dictionary
				{
					eResult=NextWord;
					break;
				}

				if(nTermCharIndex==nDicCharIndex)
					eResult=MatchFound;
				else if(nTermCharIndex>nDicCharIndex)
				{

					eResult=NextWord;
					break;
				}
				else
				{
					eResult=NoMoreMatches;
					break;
				}					
			}

			if(eResult==NoMoreMatches)
				break;
			else if(eResult==MatchFound)
			{//add to resultset and continue search
				CString strMatch=CString(UniBuf);
				int nTabPos=strMatch.Find(_T('\t'));
				int nStringPos=m_ctlTermList.AddString(strMatch.Left(nTabPos));
				CString strTranslation=strMatch.Mid(nTabPos+1);
				strTranslation.Replace(_T("\n"),_T(""));
				TCHAR* pTranslation=new TCHAR[strTranslation.GetLength()+1];
				ZeroMemory(pTranslation,sizeof(TCHAR)*(strTranslation.GetLength()+1));
				lstrcpy(pTranslation,strTranslation);
				m_ctlTermList.SetItemDataPtr(nStringPos,(void*)pTranslation);
			}
		}

		fclose(stream);
	}

	if(m_ctlTermList.GetCount()==1)
	{
		m_ctlTermList.SetCurSel(0);
		OnTermSelect();
	}
	else
	{
		if(m_ctlTermList.GetCount()==0)
			AfxMessageBox(_T("Not found"));
		else
		{
			SetCurSel(0);
			LRESULT result;
			OnSelchange(NULL,&result);
		}
	}
}

int CDictionaryCtl::GetPaliAlphaPos(TCHAR ch)
{
	static const TCHAR arrAlphabet[]={_T('a'),0x0101,_T('i'),0x012B,_T('u'),0x016B,
		_T('e'),_T('o'),0x1E43,_T('k'),_T('g'),0x1E45,_T('c'),_T('j'),
		0x00F1,0x1E6D,0x1E0D,0x1E47,_T('t'),_T('d'),_T('n'),_T('p'),
		_T('b'),_T('m'),_T('y'),_T('r'),_T('l'),_T('v'),_T('s'),_T('h'),
		0x1E37};

	for(int i=0;i<31;i++)
		if(ch==arrAlphabet[i])
			return i;

	return -1;
}

int CDictionaryCtl::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CTabCtrl::OnCreate(lpCreateStruct) == -1)
		return -1;
	
	// TODO: Add your specialized creation code here
	InsertItem(0,_T("Lst")); //list
	InsertItem(1,_T("Trn")); //translation

	CRect rClient;
	GetClientRect(rClient);
	AdjustRect(FALSE,rClient);

	LOGFONT lf;
	ZeroMemory(&lf,sizeof(LOGFONT));
	lf.lfHeight=16;
	lstrcpy(lf.lfFaceName,_T("CN-Times"));
	
	m_objFont.CreateFontIndirect(&lf);

	m_ctlTranslation.Create(WS_CHILD | ES_READONLY | ES_MULTILINE | WS_VSCROLL | WS_BORDER,rClient,this,IDC_TRANSLATION);
	
	CRect rSearchField(rClient);
	rSearchField.right=rClient.right-80;
	rSearchField.bottom=rClient.top+20;
	m_ctlSearchField.Create(WS_CHILD | WS_BORDER | WS_VISIBLE,rSearchField,this,IDC_SEARCHFIELD);

	CRect rPaliText(rClient);
	rPaliText.left=rSearchField.Width()+30;
	rPaliText.bottom=rSearchField.bottom;
	rPaliText.right=rSearchField.Width()+60;
	m_ctlPaliTextButton.Create(_T("Kb"),WS_CHILD | WS_BORDER | WS_VISIBLE | BS_CENTER,rPaliText,this,IDC_PALITEXTBUTTON);

	CRect rFindButton(rClient);
	rFindButton.left=rPaliText.right+5;
	rFindButton.bottom=rSearchField.bottom;
	m_ctlFindButton.Create(_T("Find"),WS_CHILD | WS_BORDER | WS_VISIBLE | BS_CENTER,rFindButton,this,IDC_FINDBUTTON);

	CRect rTermList(rClient);
	rTermList.top=rSearchField.Height()+10;
	m_ctlTermList.Create(WS_CHILD | WS_BORDER | WS_VSCROLL | LBS_NOTIFY | WS_VISIBLE | LBS_NOINTEGRALHEIGHT,rTermList,this,IDC_TERMLIST);

	m_ctlSearchField.SetFont(&m_objFont);
	m_ctlTranslation.SetFont(&m_objFont);
	m_ctlTermList.SetFont(&m_objFont);
	
	return 0;
}

void CDictionaryCtl::OnFind()
{
	CString strTerm;
	m_ctlSearchField.GetWindowText(strTerm);
	if(!strTerm.IsEmpty())
		LookUp(LPCTSTR(strTerm));
}

void CDictionaryCtl::OnEditContextMenu(NMHDR *pNotifyStruct, LRESULT *result)
{
	PNMRGINFO pInfo=(PNMRGINFO)pNotifyStruct;
	CMenu menu;
	VERIFY(menu.LoadMenu(IDR_EDITMENU));
	CMenu* pPopup = menu.GetSubMenu(0);
	ASSERT(pPopup != NULL);
	pPopup->TrackPopupMenu(TPM_LEFTALIGN, pInfo->ptAction.x, pInfo->ptAction.y, this);
}

void CDictionaryCtl::OnCut()
{
	m_ctlSearchField.Cut();
}

void CDictionaryCtl::OnCopy()
{
	m_ctlSearchField.Copy();
}

void CDictionaryCtl::OnPaste()
{
	m_ctlSearchField.Paste();
}

void CDictionaryCtl::OnDelete()
{
	m_ctlSearchField.ReplaceSel(_T(""),TRUE);
}

void CDictionaryCtl::OnUndo()
{
	m_ctlSearchField.Undo();
}

void CDictionaryCtl::LoadIndex()
{
	CString strFileName=theApp.GetDictionaryFileName();
	if(!strFileName.IsEmpty())
	{//file is specified
		strFileName.Replace(_T(".txt"),_T(".idx"));
		CFileStatus fs;
		if(CFile::GetStatus(strFileName,fs))
		{//file exists, load it
			CFile objIndexFile;
			if(objIndexFile.Open(strFileName,CFile::modeRead))
			{
				objIndexFile.Read(&m_arDictionaryIndex,sizeof(m_arDictionaryIndex));
				objIndexFile.Close();
			}
			m_bIndexLoaded=TRUE;
		}
		else
		{//file does not exist, create it
			CWaitCursor wait;

			FILE *stream;
			char line[500];
			TCHAR UniBuf[10];
			fpos_t nCurFilePos=0;
			int nCurAlphaPos=0;
			ZeroMemory(&m_arDictionaryIndex,sizeof(m_arDictionaryIndex));

			if( (stream=_wfopen((LPCTSTR)theApp.GetDictionaryFileName(),_T("r"))) != NULL )
			{			
				while(fgets(line,500,stream)!=NULL)
				{
					MultiByteToWideChar(CP_UTF8,NULL,(LPSTR)&line,10,(LPWSTR)&UniBuf,10);
					int nCharIndex=GetPaliAlphaPos(UniBuf[0]);
					if(nCharIndex>nCurAlphaPos)
						nCurAlphaPos=nCharIndex;

					if(nCharIndex==nCurAlphaPos)
					{
						m_arDictionaryIndex[nCurAlphaPos]=nCurFilePos;
						nCurAlphaPos++;
					}
					fgetpos(stream,&nCurFilePos);
				}
			}
			fclose(stream);

			CFile objIndexFile;
			if(objIndexFile.Open(strFileName,CFile::modeCreate | CFile::modeWrite))
			{
				objIndexFile.Write(&m_arDictionaryIndex,sizeof(m_arDictionaryIndex));
				objIndexFile.Close();
			}

			m_bIndexLoaded=TRUE;
		}
	}
}

void CDictionaryCtl::OnPaliButton()
{
	CString strTerm;
	m_ctlSearchField.GetWindowText(strTerm);
	CPaliTextEntryDlg dlg(this,_T("Pali text to search:"),strTerm);
	if(dlg.DoModal()==IDOK)	
		m_ctlSearchField.SetWindowText(dlg.m_strPaliText);	
}
