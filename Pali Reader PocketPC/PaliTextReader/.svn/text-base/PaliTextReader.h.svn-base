// PaliTextReader.h : main header file for the PALITEXTREADER application
//

#if !defined(AFX_PALITEXTREADER_H__E65B9241_DA27_41E2_B1A9_3EF7E7348B11__INCLUDED_)
#define AFX_PALITEXTREADER_H__E65B9241_DA27_41E2_B1A9_3EF7E7348B11__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols
#include "indexreader.h"
/////////////////////////////////////////////////////////////////////////////
// CPaliTextReaderApp:
// See PaliTextReader.cpp for the implementation of this class
//

class CPaliTextReaderApp : public CWinApp
{
public:
	int FindNoCase(LPCTSTR string, LPCTSTR strCharSet, int nOffset);
	CString AppDirectory;
	CPaliTextReaderApp();
	CIndexReader IndexReader;

	void SetOpenLastBook(BOOL bOpen);
	BOOL GetOpenLastBook();

	void SetLibraryLocation(CString& strLocation);
	CString GetLibraryLocation();

	CString GetDictionaryFileName();
	void SetDictionaryFileName(CString strLocation);

	int GetLastBookPosition();
	void SetLastBookPosition(int nPos);

	unsigned short GetLastBookChapterNodeID();
	void SetLastBookChapterNodeID(unsigned short NodeID);

	DWORD GetLastBookOffset();
	void SetLastBookOffset(DWORD dwOffset);

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPaliTextReaderApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CPaliTextReaderApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#define SELBOOKDLGCLOSED (WM_USER+1)
/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft eMbedded Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PALITEXTREADER_H__E65B9241_DA27_41E2_B1A9_3EF7E7348B11__INCLUDED_)
