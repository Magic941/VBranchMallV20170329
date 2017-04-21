using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.CMS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;
using System.Linq;
namespace Maticsoft.BLL.CMS
{
    /// <summary>
    /// 文章内容
    /// </summary>
    public partial class Content
    {

        private readonly IContent dal = DataAccess<IContent>.Create("CMS.Content");
        public Content()
        { }


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
        public bool Exists(int ContentID)
        {
            return dal.Exists(ContentID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.CMS.Content model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.CMS.Content model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ContentID)
        {

            return dal.Delete(ContentID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ContentIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ContentIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModel(int ContentID)
        {
            return dal.GetModel(ContentID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModelByCache(int ContentID)
        {
            string CacheKey = "ContentModel-" + ContentID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ContentID);
                    if (objModel != null)
                    {
                             int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.CMS.Content)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Content> modelList = new List<Maticsoft.Model.CMS.Content>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.CMS.Content model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
      
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod



        #region MethodEx

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPageEx(strWhere, orderby, startIndex, endIndex);
        }

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdatePV(int ContentID)
        {
            return dal.UpdatePV(ContentID);
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateTotalSupport(int ContentID)
        {
            return dal.UpdateTotalSupport(ContentID);
        }
        #endregion

        public bool UpdateFav(int ContentID)
        {
            return dal.UpdateFav(ContentID);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetRssList()
        {
            return GetList(0, " State=0 ", " CreatedDate DESC ");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetModelList()
        {
            DataSet ds = GetList(10, " State=0 ", " PvCount DESC ");
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return DataTableToList(ds.Tables[0]);
        }

        #region 批量处理审核状态
        /// <summary>
        /// 批量处理审核状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }
        #endregion

        #region 批量更新状态
        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, int State)
        {
            string strWhere = "State=" + State;
            return UpdateList(IDlist, strWhere);
        }
        #endregion

        #region  批量设置为是否推荐
        /// <summary>
        /// 批量设置为是否推荐
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="IsRecomend"></param>
        /// <returns></returns>
        public bool UpdateListByIsRecomend(string IDlist, int IsRecomend)
        {
            string strWhere = "IsRecomend=" + IsRecomend;
            return dal.UpdateList(IDlist, strWhere);
        }
        #endregion

        #region 根据ClassID判断是否存在该记录
        /// <summary>
        /// 根据ClassID判断是否存在该记录
        /// </summary>
        public bool ExistsByClassID(int ClassID)
        {
            return dal.ExistsByClassID(ClassID);
        }
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByView(string strWhere)
        {
            return dal.GetListByView(strWhere);
        }
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListByView(Top, strWhere, filedOrder);
        }
        #endregion

        #region 根据某字段获得前几行数据
        ///<summary>
        ///根据某字段获得前几行数据
        /// </summary>
        public DataSet GetListByItem(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListByItem(Top, strWhere, filedOrder);
        }
        #endregion



        #region 获取记录总数
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(int? ClassID, string Keywords)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND ClassID={0} ", ClassID.Value);
                }
                else
                {
                    strWhere.AppendFormat(" ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND Title like '%{0}%' ", Keywords);
                }
                else
                {
                    strWhere.AppendFormat(" Title like '%{0}%' ", Keywords);
                }
            }
            return GetRecordCount(strWhere.ToString());
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount4Menu(int? ClassID, string Keywords)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" T.State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND T.ClassID={0} ", ClassID.Value);
                }
                else
                {
                    strWhere.AppendFormat(" T.ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND T.Title like '%{0}%' ", Keywords);
                }
                else
                {
                    strWhere.AppendFormat(" T.Title like '%{0}%' ", Keywords);
                }
            }
            //DONE: 获取导航分类的文章总数
            if (strWhere.Length > 0) strWhere.Append(" AND ");
            strWhere.Append(" CMCC.ClassTypeID = 0 ");
            return dal.GetRecordCount4Menu(strWhere.ToString());
        }
        #endregion

        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(int? ClassID, int startIndex, int endIndex, string Keywords)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("  T.State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND T.ClassID={0} ", ClassID.Value);
                }
                else
                {
                    strWhere.AppendFormat(" T.ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND T.Title like '%{0}%' ", Keywords);
                }
                else
                {
                    strWhere.AppendFormat(" T.Title like '%{0}%' ", Keywords);
                }
            }
            return GetListByPageEx(strWhere.ToString(), "", startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetListByPage4Menu(int? ClassID, int startIndex, int endIndex, string Keywords)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("  T.State={0} ", Globals.SafeInt(EnumHelper.ContentStateType.Approve.ToString(), 0));
            if (ClassID.HasValue)
            {
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND T.ClassID={0} ", ClassID.Value);
                }
                else
                {
                    strWhere.AppendFormat(" T.ClassID={0} ", ClassID.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(Keywords))
            {
                Keywords = Globals.HtmlEncode(InjectionFilter.SqlFilter(Keywords));
                if (strWhere.Length != 0)
                {
                    strWhere.AppendFormat(" AND T.Title like '%{0}%' ", Keywords);
                }
                else
                {
                    strWhere.AppendFormat(" T.Title like '%{0}%' ", Keywords);
                }
            }
            //DONE: 获取导航分类的文章列表
            if (strWhere.Length > 0) strWhere.Append(" AND ");
            strWhere.Append(" CMCC.ClassTypeID = 0 ");
            DataSet ds = GetListByPageEx(strWhere.ToString(), "", startIndex, endIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return dal.DataTableToListEx(ds.Tables[0]);
        }
        #endregion


        #region 分页获取数据列表
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetList(int? ClassID, int startIndex, int endIndex, string Keywords)
        {
            DataSet ds = GetListByPage(ClassID, startIndex, endIndex, Keywords);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return dal.DataTableToListEx(ds.Tables[0]);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModelEx(int ContentID)
        {
            return dal.GetModelEx(ContentID);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModelExByCache(int ContentID)
        {

            string CacheKey = "ContentModelEx-" + ContentID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModelEx(ContentID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.CMS.Content)objModel;
        }
        #endregion

        #region 得到上一个ContentID
        /// <summary>
        /// 得到上一个ContentID
        /// </summary>
        public int GetPrevID(int ContentID,int ClassId=-1)
        {
            return dal.GetPrevID(ContentID, ClassId);
        }
        #endregion

        #region 得到下一个ContentID
        /// <summary>
        /// 得到下一个ContentID
        /// </summary>
        public int GetNextID(int ContentID, int ClassId=-1)
        {
            return dal.GetNextID(ContentID, ClassId);
        }
        #endregion

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetModelList(int ClassID)
        {
            return GetModelList(string.Format(" ClassID={0} and  State=0", ClassID));
        }

        /// <summary>
        /// 是否存在同名标题记录
        /// </summary>
        public bool ExistTitle(string Title)
        {
            return dal.ExistTitle(Title);
        }

        public string GetContentUrl(Maticsoft.Model.CMS.Content model)
        {
            if (model == null)
                return "";
            int rule = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("CMS_Static_ContentRule"); //获取商品静态的根目录
            if (rule == 0)
            {
                return model.ContentID.ToString();
            }
            if (rule == 1)
            {
                return Common.PinyinHelper.GetPinyin(model.Title) + "_" + model.ContentID;
            }
            if (rule == 2)
            {
                return String.IsNullOrWhiteSpace(model.SeoUrl) ? model.ContentID.ToString() : model.SeoUrl + "_" + model.ContentID;
            }
            return model.ContentID.ToString();
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetModelList(int ClassID,int Top,bool isRec=false,bool isHot=false)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" State=0 ");
            if (ClassID > 0)
            {
                strWhere.AppendFormat(" and ClassID={0} ",ClassID);
            }
            if (isRec)
            {
                strWhere.AppendFormat(" and IsRecomend=1 ");
            }
            else
            {
                if (isHot)
                {
                    strWhere.AppendFormat(" and IsHot=1 ");
                }
            }

            DataSet ds = dal.GetList(Top, strWhere.ToString(), " CreatedDate desc ");
            return DataTableToList(ds.Tables[0]);
        }

      
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetMoreList(int classId, int contentId, int top)
        {
            string strWhere = " State=0 ";

            if (classId > 0)
            {
                strWhere += " and ClassID= " + classId;
            }
            if (contentId > 0)
            {
                strWhere += " and ContentID<> " + contentId;
            }
            DataSet ds = dal.GetList(top, strWhere, " Sequence ");
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.CMS.Content> GetRecList(int ClassId, Maticsoft.Model.CMS.EnumHelper.ContentRec Rec,
                                                            int Top = -1,bool hasImage=false)
        {
            StringBuilder strWhere=new StringBuilder();
            strWhere.Append(" State=0 ");
            string rec_str = "";
            switch (Rec)
            {
                case EnumHelper.ContentRec.Recomend:
                    rec_str = " IsRecomend=1";
                    break;
                case EnumHelper.ContentRec.Hot:
                    rec_str = " IsHot=1";
                    break;
                case EnumHelper.ContentRec.Color:
                    rec_str = " IsColor=1";
                    break;
                case EnumHelper.ContentRec.Top:
                    rec_str = " IsTop=1";
                    break;
                default:
                    rec_str = " IsRecomend=1";
                    break;
            }
            strWhere.AppendFormat(" and {0}" , rec_str);
            if (ClassId > 0)
            {
                strWhere.AppendFormat(" and ClassID={0}",ClassId);
            }
            if (hasImage)
            {
                strWhere.Append(" and ImageUrl is not null");
            }

            DataSet ds = GetList(Top, strWhere.ToString(), " Sequence");
            List<Maticsoft.Model.CMS.Content> ContentList = DataTableToList(ds.Tables[0]);
            List<Maticsoft.Model.CMS.ContentClass> classList = Maticsoft.BLL.CMS.ContentClass.GetAllClass();
            foreach (var content in ContentList)
            {
                Maticsoft.Model.CMS.ContentClass classModel = classList.FirstOrDefault(c => c.ClassID == content.ClassID);
                if (classModel != null)
                {
                    content.ClassName = classModel.ClassName;
                }
            }
            return ContentList;
        }

        public List<Maticsoft.Model.CMS.Content> GetHotComList(int ClassId,   int Top = -1)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" State=0 ");
         
            if (ClassId > 0)
            {
                strWhere.AppendFormat(" and ClassID={0}", ClassId);
            }

            DataSet ds = GetList(Top, strWhere.ToString(), "  TotalComment Desc");
            List<Maticsoft.Model.CMS.Content> ContentList = DataTableToList(ds.Tables[0]);
            return ContentList;
        }


        public List<Maticsoft.Model.CMS.Content> GetHotCom(string comType, int Top)
        {
            int diffDate = 1;
            switch (comType)
            {
                case  "day":
                    diffDate = 1;
                    break;
                case "week":
                    diffDate = 7;
                    break;
            }
            DataSet ds = dal.GetHotCom(diffDate, Top);
            return ContentToList(ds.Tables[0]);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public  List<Maticsoft.Model.CMS.Content> ContentToList(DataTable dt)
        {
              List<Maticsoft.Model.CMS.Content> modelList = new List<Maticsoft.Model.CMS.Content>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.CMS.Content model;
                for (int n = 0; n < rowsCount; n++)
                {
                    var row = dt.Rows[n];
                    if (row != null)
                    {
                        model= new Maticsoft.Model.CMS.Content();
                        if (row["ContentID"] != null && row["ContentID"].ToString() != "")
                        {
                            model.ContentID = int.Parse(row["ContentID"].ToString());
                        }
                        if (row["Title"] != null)
                        {
                            model.Title = row["Title"].ToString();
                        }
                        if (row["SubTitle"] != null)
                        {
                            model.SubTitle = row["SubTitle"].ToString();
                        }
                        if (row["Summary"] != null)
                        {
                            model.Summary = row["Summary"].ToString();
                        }
                        if (row["Description"] != null)
                        {
                            model.Description = row["Description"].ToString();
                        }
                        if (row["ImageUrl"] != null)
                        {
                            model.ImageUrl = row["ImageUrl"].ToString();
                        }
                        if (row["ThumbImageUrl"] != null)
                        {
                            model.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                        }
                        if (row["NormalImageUrl"] != null)
                        {
                            model.NormalImageUrl = row["NormalImageUrl"].ToString();
                        }
                        if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                        {
                            model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                        }
                        if (row["CreatedUserID"] != null && row["CreatedUserID"].ToString() != "")
                        {
                            model.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                        }
                        if (row["LastEditUserID"] != null && row["LastEditUserID"].ToString() != "")
                        {
                            model.LastEditUserID = int.Parse(row["LastEditUserID"].ToString());
                        }
                        if (row["LastEditDate"] != null && row["LastEditDate"].ToString() != "")
                        {
                            model.LastEditDate = DateTime.Parse(row["LastEditDate"].ToString());
                        }
                        if (row["LinkUrl"] != null)
                        {
                            model.LinkUrl = row["LinkUrl"].ToString();
                        }
                        if (row["PvCount"] != null && row["PvCount"].ToString() != "")
                        {
                            model.PvCount = int.Parse(row["PvCount"].ToString());
                        }
                        if (row["State"] != null && row["State"].ToString() != "")
                        {
                            model.State = int.Parse(row["State"].ToString());
                        }
                        if (row["ClassID"] != null && row["ClassID"].ToString() != "")
                        {
                            model.ClassID = int.Parse(row["ClassID"].ToString());
                        }
                        if (row["Keywords"] != null)
                        {
                            model.Keywords = row["Keywords"].ToString();
                        }
                        if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                        {
                            model.Sequence = int.Parse(row["Sequence"].ToString());
                        }
                        if (row["IsRecomend"] != null && row["IsRecomend"].ToString() != "")
                        {
                            if ((row["IsRecomend"].ToString() == "1") ||
                                (row["IsRecomend"].ToString().ToLower() == "true"))
                            {
                                model.IsRecomend = true;
                            }
                            else
                            {
                                model.IsRecomend = false;
                            }
                        }
                        if (row["IsHot"] != null && row["IsHot"].ToString() != "")
                        {
                            if ((row["IsHot"].ToString() == "1") || (row["IsHot"].ToString().ToLower() == "true"))
                            {
                                model.IsHot = true;
                            }
                            else
                            {
                                model.IsHot = false;
                            }
                        }
                        if (row["IsColor"] != null && row["IsColor"].ToString() != "")
                        {
                            if ((row["IsColor"].ToString() == "1") || (row["IsColor"].ToString().ToLower() == "true"))
                            {
                                model.IsColor = true;
                            }
                            else
                            {
                                model.IsColor = false;
                            }
                        }
                        if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                        {
                            if ((row["IsTop"].ToString() == "1") || (row["IsTop"].ToString().ToLower() == "true"))
                            {
                                model.IsTop = true;
                            }
                            else
                            {
                                model.IsTop = false;
                            }
                        }
                        if (row["Attachment"] != null)
                        {
                            model.Attachment = row["Attachment"].ToString();
                        }
                        if (row["Remary"] != null)
                        {
                            model.Remary = row["Remary"].ToString();
                        }
                        if (row["TotalComment"] != null && row["TotalComment"].ToString() != "")
                        {
                            model.TotalComment = int.Parse(row["TotalComment"].ToString());
                        }
                        if (row["TotalSupport"] != null && row["TotalSupport"].ToString() != "")
                        {
                            model.TotalSupport = int.Parse(row["TotalSupport"].ToString());
                        }
                        if (row["TotalFav"] != null && row["TotalFav"].ToString() != "")
                        {
                            model.TotalFav = int.Parse(row["TotalFav"].ToString());
                        }
                        if (row["TotalShare"] != null && row["TotalShare"].ToString() != "")
                        {
                            model.TotalShare = int.Parse(row["TotalShare"].ToString());
                        }
                        if (row["BeFrom"] != null)
                        {
                            model.BeFrom = row["BeFrom"].ToString();
                        }
                        if (row["FileName"] != null)
                        {
                            model.FileName = row["FileName"].ToString();
                        }
                        if (row["Meta_Title"] != null)
                        {
                            model.Meta_Title = row["Meta_Title"].ToString();
                        }
                        if (row["Meta_Description"] != null)
                        {
                            model.Meta_Description = row["Meta_Description"].ToString();
                        }
                        if (row["Meta_Keywords"] != null)
                        {
                            model.Meta_Keywords = row["Meta_Keywords"].ToString();
                        }
                        if (row["SeoUrl"] != null)
                        {
                            model.SeoUrl = row["SeoUrl"].ToString();
                        }
                        if (row["SeoImageAlt"] != null)
                        {
                            model.SeoImageAlt = row["SeoImageAlt"].ToString();
                        }
                        if (row["SeoImageTitle"] != null)
                        {
                            model.SeoImageTitle = row["SeoImageTitle"].ToString();
                        }
                        if (row["StaticUrl"] != null)
                        {
                            model.StaticUrl = row["StaticUrl"].ToString();
                        }
                        //扩展属性
                        if (row["ComCount"] != null)
                        {
                            model.ComCount = Common.Globals.SafeInt(row["ComCount"].ToString(), 0);
                        }
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SetRecList(string ids)
        {
            return dal.SetRecList(ids);
        }
        public bool SetRec(int id,bool rec)
        {
            return dal.SetRec(id, rec);
        }
        /// <summary>
        /// 热门
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SetHotList(string ids)
        {
            return dal.SetHotList(ids);
        }
        public bool SetHot(int id, bool hot)
        {
            return dal.SetHot(id, hot);
        }
        /// <summary>
        /// 醒目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SetColorList(string ids)
        {
            return dal.SetColorList(ids);
        }
        public bool SetColor(int id, bool rec)
        {
            return dal.SetColor(id, rec);
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SetTopList(string ids)
        {
            return dal.SetTopList(ids);
        }
        public bool SetTop(int id, bool rec)
        {
            return dal.SetTop(id, rec);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.CMS.Content GetModelByCache(int? ContentID,out string  className)
        {
            className = "";
            if (ContentID.HasValue)
            {
                BLL.CMS.ContentClass contbll = new ContentClass();    
                dal.UpdatePV(ContentID.Value);//更新浏览量
                string CacheKey = "ContentModel-" + ContentID.Value;
                object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
                if (objModel == null)
                {
                    try
                    {
                        objModel = dal.GetModel(ContentID.Value);
                        if (objModel != null)
                        {
                            int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        }
                    }
                    catch { }
                }
                Maticsoft.Model.CMS.Content modelCont = (Maticsoft.Model.CMS.Content) objModel;
                if (modelCont != null)
                {
                    className = contbll.GetClassnameById(modelCont.ClassID);
                }
                return modelCont;
            }
            return null;
        }

 
       /// <summary>
       /// 得到一个对象实体，从缓存中
       /// </summary>
       /// <param name="ClassID">classId</param>
       /// <param name="className"></param>
       /// <returns></returns>
        public Maticsoft.Model.CMS.Content GetModelByClassIDByCache(int ClassID, out string className)
        {
            BLL.CMS.ContentClass classbll = new ContentClass();
            className = classbll.GetClassnameById(ClassID);
            string CacheKey = "ContentModelClassID-" + ClassID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModelByClassID(ClassID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.CMS.Content)objModel;
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        /// <param name="ClassID">classId</param>
        /// <param name="className"></param>
        /// <returns></returns>
        public Maticsoft.Model.CMS.Content GetModelByClassIDByCache(int ClassID)
        {
            string CacheKey = "ContentModelClassID-" + ClassID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModelByClassID(ClassID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.CMS.Content)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetListByPage(int classID,int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(" State=0  and ClassID= " + classID, "  CreatedDate desc   ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

       /// <summary>
       /// 获得数据列表 根据根栏目判断是否存在子栏目 如果存在子栏目则返回子栏目内容，否则返回根栏目内容
       /// </summary>
       /// <param name="top">前几条</param>
       /// <param name="ClassID">classid</param>
        /// <param name="topclass">前几个分类</param>
       /// <returns></returns>
        public List<Maticsoft.Model.CMS.Content> GetModelListEx(int top, int ClassID, bool HasImageUrl, int topclass)
        {
           StringBuilder strBuilder = new StringBuilder();   
           if (new BLL.CMS.ContentClass ().GetRecordCount(string.Format(" State=0  and  ParentId={0}  ", ClassID)) > 0)//存在子栏目
           {
               strBuilder.AppendFormat(
                   " EXISTS ( SELECT TOP {0} ClassID   FROM   CMS_ContentClass AS contclas WHERE  State=0  and  ParentId={1} AND contclas.ClassID=cont.ClassID  ORDER BY   Sequence  )",
                   topclass,ClassID);
               if (HasImageUrl)
               {
                   strBuilder.Append(" and  ImageUrl is not null ");
               }
               DataSet ds = dal.GetListEx(top, strBuilder.ToString(), " ContentID desc  ");
               return DataTableToList(ds.Tables[0]);
           }
           else//不存在子栏目
           {
               strBuilder.AppendFormat( "  State=0  and  ClassID={0} ", ClassID);
               if (HasImageUrl)
               {
                   strBuilder.Append(" and  ImageUrl is not null ");
               }
               DataSet ds = dal.GetListEx(top, strBuilder.ToString(), " ContentID desc  ");
               return DataTableToList(ds.Tables[0]);
           }
        }
        /// <summary>
        /// 获得数据列表   根据根栏目判断是否存在子栏目 如果存在子栏目则返回子栏目内容，否则返回根栏目内容
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetListByPage(int classID, int startIndex, int endIndex, bool  HasImageUrl, int topclass, out int totalCount)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (new BLL.CMS.ContentClass().GetRecordCount(string.Format(" State=0  and  ParentId={0}  ", classID)) > 0) //存在子栏目
            {
                strBuilder.AppendFormat(
                    " EXISTS ( SELECT TOP {0} ClassID   FROM   CMS_ContentClass AS contclas WHERE  State=0  and  ParentId={1} AND contclas.ClassID=T.ClassID  ORDER BY   Sequence  )",
                    topclass,classID);
                if (HasImageUrl)
                {
                    strBuilder.Append(" and  ImageUrl is not null ");
                }
                totalCount = dal.GetRecordCountEx(strBuilder.ToString());
                DataSet ds = dal.GetListByPage(strBuilder.ToString(), "  CreatedDate desc   ", startIndex, endIndex);
                return DataTableToList(ds.Tables[0]);
            }
            else//不存在子栏目
            {
                strBuilder.AppendFormat("  State=0  and  ClassID={0} ", classID);
                if (HasImageUrl)
                {
                    strBuilder.Append(" and  ImageUrl is not null ");
                }
                totalCount = dal.GetRecordCountEx(strBuilder.ToString());
                DataSet ds = dal.GetListByPage(strBuilder.ToString(), "  CreatedDate desc   ", startIndex, endIndex);
                return DataTableToList(ds.Tables[0]);
            }

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetModelList(int ClassID, int Top, bool HasImageUrl)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" State=0 ");
            if (ClassID > 0)
            {
                strWhere.AppendFormat(" and ClassID={0} ", ClassID);
            }
            if (HasImageUrl)
            {
                strWhere.Append(" and  ImageUrl is not null ");
            }
            DataSet ds = dal.GetList(Top, strWhere.ToString(), " CreatedDate desc ");
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetListByPageEx(int ClassID, int startIndex, int endIndex, string Keywords, int topclass, out int toalCount)
        {
            StringBuilder strWhere = new StringBuilder();
            if (new BLL.CMS.ContentClass().GetRecordCount(string.Format(" State=0  and  ParentId={0}  ", ClassID)) > 0) //存在子栏目
            {
                strWhere.AppendFormat(
                    " EXISTS ( SELECT TOP {0} ClassID   FROM   CMS_ContentClass AS contclas WHERE  State=0  and  ParentId={1} AND contclas.ClassID=T.ClassID  ORDER BY   Sequence  ) AND  T.Title like '%{2}%'",
                    topclass, ClassID,  InjectionFilter.SqlFilter(Keywords));
                toalCount = dal.GetRecordCountEx(strWhere.ToString());
                DataSet ds = GetListByPageEx(strWhere.ToString(), "", startIndex, endIndex);
                return DataTableToList(ds.Tables[0]);
            }
            else//不存在子栏目
            {
                strWhere.AppendFormat("  State=0  and  ClassID={0} ", ClassID);
                toalCount = dal.GetRecordCountEx(strWhere.ToString());
                DataSet ds = dal.GetListByPage(strWhere.ToString(), "  CreatedDate desc   ", startIndex, endIndex);
                return DataTableToList(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 根据当前内容获取上一条或下一条广告
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.CMS.Content> GetNextAndUp(int classId,int contentId)
        {
            var ds = dal.GetNextAndUp(classId, contentId);
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Content> GetWeChatList(int ClassID,string keyword,int Top)
        {
            DataSet ds = dal.GetWeChatList(ClassID, keyword, Top);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取顺序最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxSeq()
        {
            return dal.GetMaxSeq();
        }

        #endregion


    }
}

