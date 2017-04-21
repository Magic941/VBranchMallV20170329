using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// 实体类Topics 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Topics
	{
		public Topics()
		{}
		#region Model
		private int _id;
		private string _title;
		private int? _type;
		private int? _formid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 类型：0单选  1多选  2填写内容
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 问卷ID
		/// </summary>
		public int? FormID
		{
			set{ _formid=value;}
			get{return _formid;}
		}
		#endregion Model

        private int _rowNum;

        public int RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }
	}
}

