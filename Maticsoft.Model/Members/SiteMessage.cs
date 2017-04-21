using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// SiteMessage:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SiteMessage
	{
		public SiteMessage()
		{}
		#region Model
		private int _id;
		private int? _senderid;
        private int? _receiverid;
		private string _title;
		private string _content;
		private string _msgtype;
		private DateTime? _sendtime;
		private DateTime? _readtime;
		private bool _receiverisread;
		private bool _senderisdel;
		private bool _readerisdel;
		private string _ext1;
		private string _ext2;
		private string _senderusername;
		private string _receiverusername;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SenderID
		{
			set{ _senderid=value;}
			get{return _senderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReceiverID
		{
			set{ _receiverid=value;}
			get{return _receiverid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MsgType
		{
			set{ _msgtype=value;}
			get{return _msgtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendTime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReadTime
		{
			set{ _readtime=value;}
			get{return _readtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ReceiverIsRead
		{
			set{ _receiverisread=value;}
			get{return _receiverisread;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool SenderIsDel
		{
			set{ _senderisdel=value;}
			get{return _senderisdel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ReaderIsDel
		{
			set{ _readerisdel=value;}
			get{return _readerisdel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ext1
		{
			set{ _ext1=value;}
			get{return _ext1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ext2
		{
			set{ _ext2=value;}
			get{return _ext2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SenderUserName
		{
			set{ _senderusername=value;}
			get{return _senderusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceiverUserName
		{
			set{ _receiverusername=value;}
			get{return _receiverusername;}
		}
		#endregion Model

	}
}

