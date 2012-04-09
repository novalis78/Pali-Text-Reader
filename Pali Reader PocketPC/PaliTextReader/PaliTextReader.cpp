// PaliTextReader.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "PaliTextReaderDlg.h"
#include "selectbookdlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

const LPCTSTR RegKey=_T("PaliTextReader");
const LPCTSTR LibraryLocation=_T("LibraryLocation");
const LPCTSTR OpenLastBook=_T("OpenLastBook");
const LPCTSTR LastBookPosition=_T("LastBookPosition");
const LPCTSTR DictionaryFileName=_T("DictionaryFileName");
const LPCTSTR LastBookOffset=_T("LastBookOffset");
const LPCTSTR LastBookChapterID=_T("LastBookChapterID");

/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderApp

BEGIN_MESSAGE_MAP(CPaliTextReaderApp, CWinApp)
	//{{AFX_MSG_MAP(CPaliTextReaderApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderApp construction

CPaliTextReaderApp::CPaliTextReaderApp()
	: CWinApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance

}

/////////////////////////////////////////////////////////////////////////////
// The one and only CPaliTextReaderApp object

CPaliTextReaderApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderApp initialization

BOOL CPaliTextReaderApp::InitInstance()
{
	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

	SetRegistryKey(RegKey);

	::GetModuleFileName(NULL,AppDirectory.GetBuffer(MAX_PATH),MAX_PATH);
	AppDirectory.ReleaseBuffer();
	AppDirectory=AppDirectory.Left(AppDirectory.ReverseFind('\\')+1);

	CPaliTextReaderDlg dlg;

	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}

CString CPaliTextReaderApp::GetLibraryLocation()
{
	return theApp.GetProfileString(RegKey,LibraryLocation);
}

void CPaliTextReaderApp::SetLibraryLocation(CString &strLocation)
{
	theApp.WriteProfileString(RegKey,LibraryLocation,strLocation);
}

CString CPaliTextReaderApp::GetDictionaryFileName()
{
	return theApp.GetProfileString(RegKey,DictionaryFileName);
}

void CPaliTextReaderApp::SetDictionaryFileName(CString strLocation)
{
	theApp.WriteProfileString(RegKey,DictionaryFileName,strLocation);
}

BOOL CPaliTextReaderApp::GetOpenLastBook()
{
	return (BOOL)theApp.GetProfileInt(RegKey,OpenLastBook,FALSE);
}

void CPaliTextReaderApp::SetOpenLastBook(BOOL bOpen)
{
	theApp.WriteProfileInt(RegKey,OpenLastBook,bOpen);
}

DWORD CPaliTextReaderApp::GetLastBookOffset()
{
	return theApp.GetProfileInt(RegKey,LastBookOffset,0);
}

void CPaliTextReaderApp::SetLastBookOffset(DWORD dwOffset)
{
	theApp.WriteProfileInt(RegKey,LastBookOffset,dwOffset);
}

unsigned short CPaliTextReaderApp::GetLastBookChapterNodeID()
{
	return (unsigned short)theApp.GetProfileInt(RegKey,LastBookChapterID,65535);
}

void CPaliTextReaderApp::SetLastBookChapterNodeID(unsigned short NodeID)
{
	theApp.WriteProfileInt(RegKey,LastBookChapterID,NodeID);
}

int CPaliTextReaderApp::GetLastBookPosition()
{
	return theApp.GetProfileInt(RegKey,LastBookPosition,0);
}

void CPaliTextReaderApp::SetLastBookPosition(int nPos)
{
	theApp.WriteProfileInt(RegKey,LastBookPosition,nPos);
}



int CPaliTextReaderApp::FindNoCase(LPCTSTR string, LPCTSTR strCharSet, int nOffset)
{
	int nLen = lstrlen(strCharSet);

	const TCHAR* s = string + nOffset;
	while (*s)
	{
		if (_tcsnicmp(s, strCharSet, nLen) == 0)
			break;
		s++;
	}

	if (*s == _T('\0'))
		return -1;

	return (s - string);
}
