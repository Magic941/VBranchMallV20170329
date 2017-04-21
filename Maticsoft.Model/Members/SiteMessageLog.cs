using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// SiteMessageLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SiteMessageLog
	{
		public SiteMessageLog()
		{}
		#region Model
		private int _id;
		private int? _messageid;
		private string _messagetype;
		private string _messagestate;
		private int? _receiverid;
		private string _ext1;
		private string _ext2;
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
		public int? MessageID
		{
			set{ _messageid=value;}
			get{return _messageid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MessageType
		{
			set{ _messagetype=value;}
			get{return _messagetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MessageState
		{
			set{ _messagestate=value;}
			get{return _messagestate;}
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
		public string ReceiverUserName
		{
			set{ _receiverusername=value;}
			get{return _receiverusername;}
		}
		#endregion Model

	}
}

