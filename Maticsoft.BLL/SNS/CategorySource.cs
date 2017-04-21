using System;
using System.Data;
using System.Collections.Generic;
using System.Threading;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using Maticsoft.TaoBao;
using Maticsoft.TaoBao.Request;
using Maticsoft.TaoBao.Response;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 分类数据来源
	/// </summary>
	public partial class CategorySource
	{
        private readonly ICategorySource dal = DASNS.CreateCategorySource();
	    public int AddCount = 0;
	    public int UpdateCount = 0;
		public CategorySource()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SourceId,int CategoryId)
		{
			return dal.Exists(SourceId,CategoryId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.CategorySource model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.CategorySource model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SourceId,int CategoryId)
		{
			
			return dal.Delete(SourceId,CategoryId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.CategorySource GetModel(int SourceId,int CategoryId)
		{
			
			return dal.GetModel(SourceId,CategoryId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.CategorySource GetModelByCache(int SourceId,int CategoryId)
		{
			
			string CacheKey = "CategorySourceModel-" + SourceId+CategoryId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SourceId,CategoryId);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.CategorySource)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.CategorySource> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.CategorySource> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.CategorySource> modelList = new List<Maticsoft.Model.SNS.CategorySource>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.CategorySource model;
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
        #region 扩展方法
        /// <summary>
        /// 判断分类下是否存在礼品
        /// </summary>
        public bool IsExistedCate(int categoryid)
        {
            Maticsoft.BLL.SNS.CategorySource SNSCateBll = new CategorySource();
            int count = SNSCateBll.GetRecordCount("CategoryID=" + categoryid);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Maticsoft.Model.SNS.CategorySource> GetCategorysByDepth(int depth)
        {
            //ADD Cache
            return GetModelList("Depth = " + depth);
        }

        public DataSet GetCategorysByParentId(int parentCategoryId)
        {
            //ADD Cache
            return GetList("ParentID = " + parentCategoryId);
        }
        /// <summary>
        /// 添加分类（更新树形结构）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCategory(Maticsoft.Model.SNS.CategorySource model)
        {
            return dal.AddCategory(model);
        }
        /// <summary>
        /// 更新分类(更新树形结构)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCategory(Maticsoft.Model.SNS.CategorySource model)
        {
            return dal.UpdateCategory(model);
        }
        /// <summary>
        /// 根据条件获取分类列表（是否排序）
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="IsOrder"></param>
        /// <returns></returns>
        public DataSet GetCategoryList(string strWhere)
        {
            return dal.GetCategoryList(strWhere);
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int categoryId)
        {
            return dal.DeleteCategory(categoryId);
        }
        /// <summary>
        /// 对应淘宝分类ID
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="SNSCateId"></param>
        /// <returns></returns>
        public bool UpdateSNSCate(int CategoryId, int SNSCateId, bool IsLoop)
        {
            return dal.UpdateSNSCate(CategoryId, SNSCateId, IsLoop);
        }
        /// <summary>
        ///批量 对应淘宝分类ID
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="SNSCateId"></param>
        /// <returns></returns>
        public bool UpdateSNSCateList(string ids, int SNSCateId, bool IsLoop)
        {
            return dal.UpdateSNSCateList(ids, SNSCateId, IsLoop);
        }
        ///// <summary>
        ///// 对分类信息进行排序
        ///// </summary>
        //public bool SwapSequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex)
        //{
        //    return dal.SwapSequence(CategoryId, zIndex);
        //}

        /// <summary>
        /// 对商品的类别进行全部更新或初始化
        /// </summary>
        public void ResetCategory(out int addCount,out int updateCount)
        {
            CategoryLoop(0L, "", 0);
            addCount = AddCount;
            updateCount = UpdateCount;
        }

        /// <summary>
        /// 是否需要更新
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsUpdate(long CategoryId, string name, int SourceId, int ParentID)
        {
            return dal.IsUpdate(CategoryId, name, SourceId, ParentID);
        }

        /// <summary>
        /// 获取淘宝的全部分类，采用递归调用的方式,(初始化或更新某一个类别),管理员操作
        /// </summary>
        /// <param name="CategoryId">类别的名称 如果是初始化类别，则是0L</param>
        /// <param name="Path">路径，如果是初始化则为""</param>
        /// <param name="Depth">深度，如果是初始化则为0</param>
        public void CategoryLoop(long CategoryId, string Path, int Depth)
        {
            string TaoBaoAppkey = SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaoBaoAppkey");
            string TaobaoAppsecret = SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoAppsecret");
            string TaobaoApiUrl = SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoApiUrl");
            Maticsoft.Model.SNS.CategorySource CateModel = new Model.SNS.CategorySource();
            ITopClient client = new DefaultTopClient(TaobaoApiUrl, TaoBaoAppkey, TaobaoAppsecret);

            ItemcatsGetRequest req = new ItemcatsGetRequest();
            req.Fields = "cid,parent_cid,name,is_parent";
            req.ParentCid = CategoryId;
            ItemcatsGetResponse response = client.Execute(req);
       
            if (response.ItemCats.Count > 0)
            {
                foreach (var item in response.ItemCats)
                {
                    CateModel.CategoryId = Common.Globals.SafeInt(item.Cid.ToString(), 0);
                      CateModel.ParentID = Common.Globals.SafeInt(item.ParentCid.ToString(), 0);
                    // 存在且名称做了修改，就需要做执行修改动作
                      if (!Exists(3, CateModel.CategoryId))
                      {
                          if (string.IsNullOrEmpty(Path))
                          {
                              CateModel.Path = item.Cid.ToString();
                          }
                          else
                          {
                              CateModel.Path = Path + "|" + item.Cid.ToString();
                          }
                          CateModel.Depth = Depth + 1;
                          CateModel.CreatedDate = DateTime.Now;
                          CateModel.CreatedUserID = 1;
                          CateModel.Description = "暂无描述";
                          CateModel.HasChildren = item.IsParent;
                          CateModel.IsMenu = false;
                          CateModel.MenuIsShow = false;
                          CateModel.MenuSequence = 0;
                          CateModel.Name = item.Name;
                          CateModel.Status = 1;
                          CateModel.Type = 0;
                          CateModel.SourceId = 3;
                          Add(CateModel);
                          AddCount++;
                      }
                     else if (IsUpdate(CateModel.CategoryId, item.Name, 3, CateModel.ParentID)) 
                    {
                        if (string.IsNullOrEmpty(Path))
                        {
                            CateModel.Path = item.Cid.ToString();
                        }
                        else
                        {
                            CateModel.Path = Path + "|" + item.Cid.ToString();
                        }
                        CateModel.Depth = Depth + 1;
                        CateModel.CreatedDate = DateTime.Now;
                        CateModel.CreatedUserID = 1;
                        CateModel.Description = "暂无描述";
                        CateModel.HasChildren = item.IsParent;
                        CateModel.IsMenu = false;
                        CateModel.MenuIsShow = false;
                        CateModel.MenuSequence = 0;
                        CateModel.Name = item.Name;
                        CateModel.Status = 1;
                        CateModel.Type = 0;
                        CateModel.SourceId = 3;
                        Update(CateModel);
                        UpdateCount++;
                    }
                    ///测试阶段，淘宝的限制是没分钟400次访问
                      Thread primaryThread = Thread.CurrentThread;
                      Thread.Sleep(500);
                       
                    //下面是递归调用和相应的出口（没有子集的情况下，直接返回）
                    if (item.IsParent)
                    {
                        CategoryLoop(item.Cid, CateModel.Path, CateModel.Depth);
                    }
                }
            }
        }


	    public void GetAllCategory()
        {
            string TaoBaoAppkey = SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaoBaoAppkey");
            string TaobaoAppsecret = SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoAppsecret");
            string TaobaoApiUrl = SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoApiUrl");
            ITopClient client = new DefaultTopClient(TaobaoApiUrl, TaoBaoAppkey, TaobaoAppsecret);

            TopatsItemcatsGetRequest req = new TopatsItemcatsGetRequest();
            TopatsItemcatsGetResponse response = client.Execute(req);

        }

	    #endregion
	}
}

