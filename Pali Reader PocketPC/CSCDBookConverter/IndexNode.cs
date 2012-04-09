using System;
using System.IO;
using System.Text;

namespace CSCDBookConverter
{
	/// <summary>
	/// Summary description for IndexRecord.
	/// </summary>
	public class IndexNode : IComparable
	{
		private ushort m_nParentID;
		private ushort m_nNodeID;
		private string m_strNodeText="";
		private bool m_bHasChildren;
		private ushort m_nZipFileIndex;
		private string m_strBookmarkName="";
		private byte m_nLibFileIndex=0x01;

		public IndexNode(int ParentID,int NodeID,string NodeText,
			bool HasChildren,int ZipFileIndex,byte LibFileIndex)
		{
			m_nParentID=(ushort)ParentID;
			m_nNodeID=(ushort)NodeID;
			m_strNodeText=NodeText;
			m_bHasChildren=HasChildren;
			m_nZipFileIndex=(ushort)ZipFileIndex;
			m_nLibFileIndex=LibFileIndex;
		}

		public IndexNode(int ParentID,int NodeID,string NodeText,
			bool HasChildren,int ZipFileIndex,string strBookmarkName,byte LibFileIndex)
		{
			m_nParentID=(ushort)ParentID;
			m_nNodeID=(ushort)NodeID;
			m_strNodeText=NodeText;
			m_bHasChildren=HasChildren;
			m_nZipFileIndex=(ushort)ZipFileIndex;
			m_strBookmarkName=strBookmarkName;
			m_nLibFileIndex=LibFileIndex;
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write(m_nParentID);
			writer.Write(m_nNodeID);

			byte[] arrNodeText=Encoding.UTF8.GetBytes(m_strNodeText);
			writer.Write((byte)arrNodeText.Length);
			writer.Write(arrNodeText,0,arrNodeText.Length);

			if(arrNodeText.Length>255)
				System.Windows.Forms.MessageBox.Show(m_strNodeText);

			writer.Write((byte)(m_bHasChildren ? 0xFF : 0x00));
			writer.Write(m_nZipFileIndex);	
			writer.Write(m_nLibFileIndex);	

			byte[] arrBookmark=Encoding.UTF8.GetBytes(m_strBookmarkName);
			writer.Write((byte)arrBookmark.Length);
			writer.Write(arrBookmark,0,arrBookmark.Length);

			if(arrBookmark.Length>255)
				System.Windows.Forms.MessageBox.Show(m_strBookmarkName);
		}

		public void Write(StreamWriter writer)
		{
			writer.Write(m_nParentID);
			writer.Write(",");
			writer.Write(m_nNodeID);
			writer.Write(",");
			writer.Write(m_strNodeText);
			writer.Write(",");
			writer.Write(m_bHasChildren);
			writer.Write(",");
			writer.Write(m_nZipFileIndex);
			writer.Write(",");
			writer.Write(m_nLibFileIndex);			
			writer.Write(",");
			writer.Write(m_strBookmarkName);
			writer.Write("\r\n");
		}

		public int ParentID
		{
			get
			{
				return m_nParentID;
			}
		}

		public int NodeID
		{
			get
			{
				return m_nNodeID;
			}
		}

		public string Name
		{
			get
			{
				return m_strNodeText;
			}
		}

		public bool HasChildren
		{
			get
			{
				return m_bHasChildren;
			}
			set
			{
				m_bHasChildren=value;
			}
		}

		public int ZipFileIndex
		{
			get
			{
				return m_nZipFileIndex;
			}
			set
			{
				m_nZipFileIndex=(ushort)value;
			}
		}

		public string BookmarkName
		{
			get
			{
				return m_strBookmarkName;
			}
		}

		public byte LibFileIndex
		{
			get
			{
				return m_nLibFileIndex;
			}
			set
			{
				m_nLibFileIndex=value;
			}
		}
		#region IComparable Members

		public int CompareTo(object obj)
		{
			IndexNode objAnotherNode=(IndexNode)obj;
			if(ParentID<objAnotherNode.ParentID)
				return  -1;

			if(ParentID>objAnotherNode.ParentID)
				return 1;

			if(ParentID==objAnotherNode.ParentID)
			{
				if(NodeID<objAnotherNode.NodeID)
					return -1;

				if(NodeID>objAnotherNode.NodeID)
					return 1;
			}

			return 0;
		}

		#endregion
	}
}
