using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// ʵ����Forms ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Forms
	{
		public Forms()
		{}
		#region Model
		private int _formid;
		private string _name;
		private string _description;
		/// <summary>
		/// 
		/// </summary>
		public int FormID
		{
			set{ _formid=value;}
			get{return _formid;}
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
		#endregion Model

	}
}

