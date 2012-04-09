// IndexReader.h: interface for the CIndexReader class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_INDEXREADER_H__2FB66F93_8320_4B9E_9169_081DCA12B431__INCLUDED_)
#define AFX_INDEXREADER_H__2FB66F93_8320_4B9E_9169_081DCA12B431__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include <Afxtempl.h>

class CIndexNode
{
public:
	CIndexNode();
	virtual ~CIndexNode();

	unsigned short ParentID;
	unsigned short NodeID;
	CString Name;
	bool HasChildren;
	unsigned short ZipFileIndex;
	CString BookmarkName;
	DWORD Offset;
	unsigned char LibFileIndex;
};

typedef CList<CIndexNode,CIndexNode&> NodeList;

class CIndexReader  
{// this class is designed in such a way so there is no place for memory leaks
public:
	void GetChildNodes(unsigned short NodeID, NodeList& rList);
	void GetNode(DWORD dwOffset,CIndexNode& rNode);
	unsigned short GetNodeID(DWORD dwOffset);
	BOOL Open(LPCTSTR lpszFileName);
	CIndexReader();
	virtual ~CIndexReader();

private:
	void ReadCurrentNode(CIndexNode& rNode);

	CFile m_objNodesFile;
	CMap<unsigned short,unsigned short,DWORD,DWORD> m_mapNodeIndex;
};

#endif // !defined(AFX_INDEXREADER_H__2FB66F93_8320_4B9E_9169_081DCA12B431__INCLUDED_)
