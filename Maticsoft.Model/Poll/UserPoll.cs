using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// 实体类UserPoll 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class UserPoll
	{
		public UserPoll()
		{}
		#region Model
		private int _userid=0;
		private int? _topicid;
		private int? _optionid;
		private DateTime? _creattime;
        private string _userip;
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
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
		public int? OptionID
		{
			set{ _optionid=value;}
			get{return _optionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatTime
		{
			set{ _creattime=value;}
			get{return _creattime;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string UserIP
        {
            set { _userip = value; }
            get { return _userip; }
        }

        
		#endregion Model

	    private string _optionidlist;
        /// <summary>
        /// 扩展字段  多选题的答案列表
        /// </summary>
        public string OptionIDList
        {
            set { _optionidlist = value; }
            get { return _optionidlist; }
        }
	}
}

