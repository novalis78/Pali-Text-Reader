#if !defined(AFX_BOOKMARKDOC_H__805E00C6_46B5_4C1A_AD14_DF9F7F4CE21A__INCLUDED_)
#define AFX_BOOKMARKDOC_H__805E00C6_46B5_4C1A_AD14_DF9F7F4CE21A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// BookmarkDoc.h : header file
//
#include <Afxtempl.h>

class CBookmark : public CObject  
{
public:
	CBookmark();
	CBookmark(CString strBookmarkName,DWORD dwNodeOffset,unsigned short uChapterNodeID,int nLine);
	CBookmark& operator =(CBookmark& rBookmark);
	virtual void Serialize(CArchive& ar);
	virtual ~CBookmark();

	DWORD NodeOffset;
	unsigned short ChapterNodeID;
	CString BookmarkName;
	int Line;

	DECLARE_SERIAL(CBookmark)
};

/////////////////////////////////////////////////////////////////////////////
// CBookmarkDoc document


class CBookmarkDoc : public CDocument
{
public:
	CBookmarkDoc();           // protected constructor used by dynamic creation
protected:
	DECLARE_DYNCREATE(CBookmarkDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CBookmarkDoc)
	public:
	virtual void Serialize(CArchive& ar);   // overridden for document i/o
	protected:
	virtual BOOL OnNewDocument();
	//}}AFX_VIRTUAL

// Implementation
public:
	CBookmark& GetNextBookmark(POSITION& pos);
	POSITION GetHeadBookmarkPosition();
	void DeleteBookmark(POSITION pos);
	POSITION AddBookmark(CBookmark& pBookmark);
	virtual ~CBookmarkDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:
	CList<CBookmark,CBookmark&> m_collBookmarks;

	// Generated message map functions
protected:
	void InitDocument();
	//{{AFX_MSG(CBookmarkDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};




//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_BOOKMARKDOC_H__805E00C6_46B5_4C1A_AD14_DF9F7F4CE21A__INCLUDED_)
