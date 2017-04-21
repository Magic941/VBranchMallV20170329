/**  版本信息模板在安装目录下，可自行修改。
* Services.cs
*
* 功 能： N/A
* 类 名： Services
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/2 17:36:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Appt;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Appt;
using System.IO;
using System.Web;
namespace Maticsoft.BLL.Appt
{
	/// <summary>
	/// Services
	/// </summary>
	public partial class Services
	{
        private readonly IServices dal = DAAppt.CreateServices();
		public Services()
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
		public bool Exists(int ServiceId)
		{
			return dal.Exists(ServiceId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Appt.Services model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Appt.Services model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ServiceId)
		{
			
			return dal.Delete(ServiceId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ServiceIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(ServiceIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Appt.Services GetModel(int ServiceId)
		{
			
			return dal.GetModel(ServiceId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Appt.Services GetModelByCache(int ServiceId)
		{
			
			string CacheKey = "ServicesModel-" + ServiceId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ServiceId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Appt.Services)objModel;
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
		public List<Maticsoft.Model.Appt.Services> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Appt.Services> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Appt.Services> modelList = new List<Maticsoft.Model.Appt.Services>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Appt.Services model;
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
        public static string MoveImage(string ImageUrl, string savePath, string saveThumbsPath)
        {
            try
            {
                if (BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ImageStoreWay") == "1")
                {
                    return ImageUrl + "|" + ImageUrl;
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));

                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    //首先移动原图片

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        originalUrl = String.Format(savePath + imgname, "");
                        System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(originalUrl));

                    }
                    if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                    {
                        foreach (var thumbSize in ThumbSizeList)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                                destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                //为了防止编辑时 未修改的旧图片移动会导致已存在(目标文件与源文件路径路径相同)
                                if (!File.Exists(HttpContext.Current.Server.MapPath(destImage)))
                                {
                                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)), HttpContext.Current.Server.MapPath(destImage));
                                }
                            }
                        }
                    }
                    return originalUrl + "|" + thumbUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }
		#endregion  ExtensionMethod
	}
}

