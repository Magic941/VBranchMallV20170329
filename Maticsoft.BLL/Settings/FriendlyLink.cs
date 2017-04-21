using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Settings;
namespace Maticsoft.BLL.Settings
{
	/// <summary>
	/// ��������
	/// </summary>
	public partial class FriendlyLink
	{
        private readonly IFriendlyLink dal = DASettings.CreateFriendlyLink();
        public FriendlyLink()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(Maticsoft.Model.Settings.FriendlyLink model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(Maticsoft.Model.Settings.FriendlyLink model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Maticsoft.Model.Settings.FriendlyLink GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public Maticsoft.Model.Settings.FriendlyLink GetModelByCache(int ID)
		{
			
			string CacheKey = "FLinksModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Settings.FriendlyLink)objModel;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}

         /// <summary>
        /// ��������б�
        /// </summary>
        public List<Maticsoft.Model.Settings.FriendlyLink> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.FriendlyLink> modelList = new List<Maticsoft.Model.Settings.FriendlyLink>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Settings.FriendlyLink model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

		/// <summary>
		/// ��������б�
		/// </summary>
		public List<Maticsoft.Model.Settings.FriendlyLink> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return DataTableToList(ds.Tables[0]);
		}
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Maticsoft.Model.Settings.FriendlyLink> GetModelList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = GetList(Top, strWhere, filedOrder);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Settings.FriendlyLink> GetModelList(int Top,int Type)
        {

            string CacheKey = "GetModelList-" + Top + Type;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    string strWhere = string.Format(" IsDisplay=1 AND TypeID={0} ", Type);
                    string filedOrder = " OrderID ";
                    objModel= GetModelList(Top, strWhere, filedOrder);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Settings.FriendlyLink>)objModel;
        }

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist,string strWhere)
        {
            return dal.UpdateList(IDlist,strWhere);
        }

		#endregion  Method
	}
}

