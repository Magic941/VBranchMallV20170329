using System;
namespace Maticsoft.Model.SysManage
{
	/// <summary>
	/// TaskQueue:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TaskQueue
	{
		public TaskQueue()
		{}
		#region Model
		private int _id;
		private int _type;
		private int _taskid;
		private int _status;
		private DateTime? _rundate;
		/// <summary>
		/// 流水账ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 类型  （预留字段） 0：表示文章 
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 任务ID
		/// </summary>
		public int TaskId
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 状态 0：未生成 1：生成成功
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 执行时间
		/// </summary>
		public DateTime? RunDate
		{
			set{ _rundate=value;}
			get{return _rundate;}
		}
		#endregion Model

	}
}

