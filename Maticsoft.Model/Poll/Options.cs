using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// ʵ����Options ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Options
	{
		public Options()
		{}
		#region Model
		private int _id;
		private string _name;
		private int? _topicid;
		private int? _ischecked;
		private int? _submitnum;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public int? isChecked
		{
			set{ _ischecked=value;}
			get{return _ischecked;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SubmitNum
		{
			set{ _submitnum=value;}
			get{return _submitnum;}
		}
		#endregion Model

	}
}

