using System;
namespace Maticsoft.Model.CMS
{
	/// <summary>
    /// À¸Ä¿ÀàÐÍ
	/// </summary>
	[Serializable]
	public partial class ClassType
	{
		public ClassType()
		{}
		#region Model
		private int _classtypeid;
		private string _classtypename;
		/// <summary>
		/// 
		/// </summary>
		public int ClassTypeID
		{
			set{ _classtypeid=value;}
			get{return _classtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClassTypeName
		{
			set{ _classtypename=value;}
			get{return _classtypename;}
		}
		#endregion Model

	}
}

