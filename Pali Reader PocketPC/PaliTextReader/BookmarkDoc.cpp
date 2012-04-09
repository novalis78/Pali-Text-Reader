// BookmarkDoc.cpp : implementation file
//

#include "stdafx.h"
#include "PaliTextReader.h"
#include "BookmarkDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//////////////////////////////////////////////////////////////////////
// CBookmark Class
//////////////////////////////////////////////////////////////////////
IMPLEMENT_SERIAL(CBookmark,CObject,1)
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CBookmark::CBookmark()
{

}

CBookmark::CBookmark(CString strBookmarkName,DWORD dwNodeOffset,unsigned short uChapterNodeID,int nLine)
{
	BookmarkName=strBookmarkName;
	NodeOffset=dwNodeOffset;
	ChapterNodeID=uChapterNodeID;
	Line=nLine;
}

CBookmark::~CBookmark()
{

}

void CBookmark::Serialize(CArchive &ar)
{
	CObject::Serialize(ar);
	if(ar.IsStoring())
	{
		ar<<BookmarkName;
		ar<<Line;
		ar<<NodeOffset;
		ar<<ChapterNodeID;
	}
	else
	{
		ar>>BookmarkName;
		ar>>Line;
		ar>>NodeOffset;
		ar>>ChapterNodeID;
	}
}

template <> void AFXAPI SerializeElements <CBookmark> (CArchive& ar,CBookmark* pNewBookmarks,INT_PTR nCount)
{
    for(int i=0;i<nCount;i++,pNewBookmarks++)
        pNewBookmarks->Serialize(ar);
}

CBookmark& CBookmark::operator =(CBookmark& rBookmark)
{
	BookmarkName=rBookmark.BookmarkName;
	Line=rBookmark.Line;
	NodeOffset=rBookmark.NodeOffset;
	ChapterNodeID=rBookmark.ChapterNodeID;
	return *this;
}

/////////////////////////////////////////////////////////////////////////////
// CBookmarkDoc

IMPLEMENT_DYNCREATE(CBookmarkDoc, CDocument)

CBookmarkDoc::CBookmarkDoc()
{
}

BOOL CBookmarkDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;
	return TRUE;
}

CBookmarkDoc::~CBookmarkDoc()
{
}

BEGIN_MESSAGE_MAP(CBookmarkDoc, CDocument)
	//{{AFX_MSG_MAP(CBookmarkDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CBookmarkDoc diagnostics

#ifdef _DEBUG
void CBookmarkDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CBookmarkDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CBookmarkDoc serialization

void CBookmarkDoc::Serialize(CArchive& ar)
{
	m_collBookmarks.Serialize(ar);
}

/////////////////////////////////////////////////////////////////////////////
// CBookmarkDoc commands

POSITION CBookmarkDoc::AddBookmark(CBookmark& rBookmark)
{
	return m_collBookmarks.AddTail(rBookmark);
}

void CBookmarkDoc::DeleteBookmark(POSITION pos)
{
	m_collBookmarks.RemoveAt(pos);
}

void CBookmarkDoc::InitDocument()
{
	m_collBookmarks.RemoveAll();
}

POSITION CBookmarkDoc::GetHeadBookmarkPosition()
{
	return m_collBookmarks.GetHeadPosition();
}

CBookmark& CBookmarkDoc::GetNextBookmark(POSITION& pos)
{
	return m_collBookmarks.GetNext(pos);
}


