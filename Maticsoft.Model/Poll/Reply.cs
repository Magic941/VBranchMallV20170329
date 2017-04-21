using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// 实体类Reply 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Reply
	{
		public Reply()
		{}
		#region Model
		private int _id;
		private int? _topicid;
		private string _recontent;
		private DateTime? _retime;
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
		public int? TopicID
		{
			set{ _topicid=value;}
			get{return _topicid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReContent
		{
			set{ _recontent=value;}
			get{return _recontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReTime
		{
			set{ _retime=value;}
			get{return _retime;}
		}
		#endregion Model

	}
}

