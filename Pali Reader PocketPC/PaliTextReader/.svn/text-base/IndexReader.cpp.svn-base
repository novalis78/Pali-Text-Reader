// IndexReader.cpp: implementation of the CIndexReader class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "PaliTextReader.h"
#include "IndexReader.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

extern CPaliTextReaderApp theApp;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
CIndexNode::CIndexNode()
{
	ParentID=-1;
	NodeID=-1;
	HasChildren=false;
	ZipFileIndex=-1;
	Offset=0L;
}

CIndexNode::~CIndexNode()
{
}

CIndexReader::CIndexReader()
{
}

CIndexReader::~CIndexReader()
{
}

BOOL CIndexReader::Open(LPCTSTR lpszFileName)
{
	BOOL bOpenResult=m_objNodesFile.Open(lpszFileName,CFile::modeRead);
	if(bOpenResult)
	{//load index for faster access
		CString strIndexFileName=CString(lpszFileName);
		strIndexFileName.Replace(_T(".dat"),_T(".idx"));

		CFile objIndexFile;	
		CFileStatus fs;
		if(CFile::GetStatus(strIndexFileName,fs))
		{//file exists
			if(objIndexFile.Open(strIndexFileName,CFile::modeRead))
			{		
				CArchive ar(&objIndexFile,CArchive::load);
				m_mapNodeIndex.Serialize(ar);
				ar.Close();
				objIndexFile.Close();
			}
		}
		else
		{//index the file now
			CWaitCursor wait;
			unsigned short PrevParentID=-1;
			unsigned short ParentID;
			int Counter=0;
			while(true)
			{
				DWORD dwRead=m_objNodesFile.Read(&ParentID,2);
				if(dwRead==0)
					break;

				if(PrevParentID!=ParentID)
				{
					if((Counter % 10)==0)
					{//I had to do this since loading all the nodes (5000+) in the map takes ~10 seconds
						m_mapNodeIndex.SetAt(ParentID,m_objNodesFile.GetPosition()-2);
					}
					PrevParentID=ParentID;
					Counter++;
				}

				m_objNodesFile.Seek(2,CFile::current);//skip nodeID
				//skip name
				int NameLength=0;
				m_objNodesFile.Read(&NameLength,1); //name length in bytes
				m_objNodesFile.Seek(NameLength,CFile::current);
				//skip HasChildren & ZipFileIndex
				m_objNodesFile.Seek(4,CFile::current);
				//skip bookmark name
				int BookmarkLength=0;
				m_objNodesFile.Read(&BookmarkLength,1);
				m_objNodesFile.Seek(BookmarkLength,CFile::current);				
			}

			if(objIndexFile.Open(strIndexFileName,CFile::modeCreate | CFile::modeWrite))
			{		
				CArchive ar(&objIndexFile,CArchive::store);
				m_mapNodeIndex.Serialize(ar);
				ar.Close();
				objIndexFile.Close();
			}
		}
	}

	return bOpenResult;
}

unsigned short CIndexReader::GetNodeID(DWORD dwOffset)
{
	m_objNodesFile.Seek(dwOffset,CFile::begin);
	m_objNodesFile.Seek(2,CFile::current);
		
	unsigned short NodeID=-1;
	m_objNodesFile.Read(&NodeID,2);
	return NodeID;
}


void CIndexReader::GetNode(DWORD dwOffset, CIndexNode &rNode)
{
	m_objNodesFile.Seek(dwOffset,CFile::begin);
	ReadCurrentNode(rNode);
}

void CIndexReader::ReadCurrentNode(CIndexNode &rNode)
{
	rNode.Offset=m_objNodesFile.GetPosition();
	//ParentID
	m_objNodesFile.Read(&rNode.ParentID,2);
	//NodeID
	m_objNodesFile.Read(&rNode.NodeID,2);
	//Name
	int NameLength=0;
	m_objNodesFile.Read(&NameLength,1); //name length in bytes

	if(NameLength>0)
	{
		char* pUTF8Buf=new char[NameLength];
		m_objNodesFile.Read(pUTF8Buf,NameLength); //read it
		//convert to Unicode
		int nWideLength=MultiByteToWideChar(CP_UTF8,NULL,pUTF8Buf,NameLength,NULL,0);
		MultiByteToWideChar(CP_UTF8,NULL,pUTF8Buf,-1,rNode.Name.GetBuffer(nWideLength),nWideLength);
		rNode.Name.ReleaseBuffer(nWideLength);
		delete pUTF8Buf;
		rNode.Name.Replace(_T("\\c03 "),_T(""));
	}
	else
		rNode.Name.Empty();

	//HasChildren
	char cHasChildren=0;
	m_objNodesFile.Read(&cHasChildren,1);
	rNode.HasChildren=(cHasChildren!=0);
	//ZipFileIndex
	m_objNodesFile.Read(&rNode.ZipFileIndex,2);
	m_objNodesFile.Read(&rNode.LibFileIndex,1);

	//Bookmark
	int BookmarkLength=0;
	m_objNodesFile.Read(&BookmarkLength,1);
	if(BookmarkLength>0)
	{
		char* pUTF8Buf=new char[BookmarkLength];
		m_objNodesFile.Read(pUTF8Buf,BookmarkLength); //read it
		//convert to Unicode
		int nWideLength=MultiByteToWideChar(CP_UTF8,NULL,pUTF8Buf,BookmarkLength,NULL,0);
		MultiByteToWideChar(CP_UTF8,NULL,pUTF8Buf,-1,rNode.BookmarkName.GetBuffer(nWideLength),nWideLength);
		rNode.BookmarkName.ReleaseBuffer(nWideLength);
		delete pUTF8Buf;
	}
	else
		rNode.BookmarkName.Empty();
}

void CIndexReader::GetChildNodes(unsigned short NodeID, NodeList &rList)
{
	m_objNodesFile.SeekToBegin();

	DWORD dwOffset;
	unsigned short NodeIterator=NodeID;
	while(NodeIterator>=0)
	{
		if(m_mapNodeIndex.Lookup(NodeIterator,dwOffset))
		{
			m_objNodesFile.Seek(dwOffset,CFile::begin);
			break; //found
		}
		NodeIterator--;
	}

	unsigned short ParentID=-1;
	bool bMatchFound=false;
	while(true)
	{
		m_objNodesFile.Read(&ParentID,2);
		if(ParentID==NodeID)
		{
			bMatchFound=true;
			m_objNodesFile.Seek(-2,CFile::current);
			CIndexNode objNewNode;
			ReadCurrentNode(objNewNode);
			rList.AddTail(objNewNode);
			if(m_objNodesFile.GetPosition()==m_objNodesFile.GetLength())
				break;
		}
		else if(bMatchFound)
				break;
		else
		{//skip current node
			m_objNodesFile.Seek(2,CFile::current);//skip nodeID

			//skip name
			int NameLength=0;
			m_objNodesFile.Read(&NameLength,1); //name length in bytes
			m_objNodesFile.Seek(NameLength,CFile::current);
			//skip HasChildren & ZipFileIndex
			m_objNodesFile.Seek(4,CFile::current);
			//skip bookmark name
			int BookmarkLength=0;
			m_objNodesFile.Read(&BookmarkLength,1);
			m_objNodesFile.Seek(BookmarkLength,CFile::current);
			//skip file index
		}		
	}
}
