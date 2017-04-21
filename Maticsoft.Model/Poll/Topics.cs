using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// ʵ����Topics ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// ���ͣ�0��ѡ  1��ѡ  2��д����
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// �ʾ�ID
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

