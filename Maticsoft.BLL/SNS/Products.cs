/**
* Products.cs
*
* 功 能： N/A
* 类 名： Products
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:49   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using Maticsoft.Model.SNS;
using Maticsoft.TaoBao;
using Maticsoft.TaoBao.Response;
using Maticsoft.TaoBao.Request;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 商品
    /// </summary>
    public partial class Products
    {
        private readonly IProducts dal = DASNS.CreateProducts();

        public Products()
        {
        }

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ProductID)
        {
            return dal.Exists(ProductID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.SNS.Products model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Products model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductID)
        {

            return dal.Delete(ProductID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ProductIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ProductIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Products GetModel(long ProductID)
        {
            return dal.GetModel(ProductID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Products GetModelByCache(long ProductID)
        {

            string CacheKey = "ProductsModel-" + ProductID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ProductID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                            TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.Products)objModel;
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
        public List<Maticsoft.Model.SNS.Products> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Products> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Products> modelList = new List<Maticsoft.Model.SNS.Products>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Products model;
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

        #region ExtensionMethod

        /// <summary>
        /// 通过查询对象构建查询条件语句
        /// </summary>
        /// <param name="queryobj">查询条件对象</param>
        /// <returns></returns>
        private string BindstrWhere(ProductQuery queryobj)
        {
            StringBuilder strWhere = new StringBuilder();
            if (queryobj.CategoryID.HasValue && queryobj.CategoryID.Value != 0 && queryobj.IsTopCategory == false &&
                queryobj.CategoryID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat(" CategoryID={0}", queryobj.CategoryID.Value);
            }
            else
            {
                if (queryobj.CategoryID > 0)
                {
                    if (strWhere.Length > 0)
                    {
                        strWhere.Append(" And ");
                    }
                    strWhere.AppendFormat(
                        " CategoryID in(select CategoryID from SNS_Categories where Path like '{0}%')",
                        queryobj.CategoryID);
                }
            }
            if (!string.IsNullOrEmpty(queryobj.Keywords))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat("( ProductName like '%{0}%' or Tags like '%{0}%')", queryobj.Keywords);
            }
            if (queryobj.IsRecomend.HasValue)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat(" IsRecomend={0}", queryobj.IsRecomend.Value);
            }
            if (queryobj.MaxPrice.HasValue && queryobj.MaxPrice.Value != 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat(" Price<{0}", queryobj.MaxPrice.Value);
            }
            if (queryobj.MinPrice.HasValue && queryobj.MinPrice.Value != 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat(" Price>{0}", queryobj.MinPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryobj.Tags))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat(" Tags like '%{0}%'", queryobj.Tags);
            }
            if (!string.IsNullOrEmpty(queryobj.Color) && queryobj.Color != "all")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" And ");
                }
                strWhere.AppendFormat(" Color='{0}'", queryobj.Color);
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" And ");
            }
            strWhere.AppendFormat(" Status<>{0}", (int)Maticsoft.Model.SNS.EnumHelper.ProductStatus.UnChecked);

            return strWhere.ToString();
        }

        /// <summary>
        /// 获得商品的url
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public string GetProductImageUrl(long Pid)
        {
            try
            {
                //ITopClient client = BLL.SysManage.TaoBaoConfig.GetTopClient();
                ITopClient client = BLL.SNS.TaoBaoConfig.GetTopClient();
             
                    ItemGetRequest req1 = new ItemGetRequest();
                    req1.Fields =
                        "num_iid,title,price,num_iid,title,cid,nick,desc,location,price,post_fee,express_fee,ems_fee,freight_payer,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url";
                    req1.NumIid = Pid;
                    ItemGetResponse response1 = client.Execute(req1);
                    if (response1.Item != null)
                    {
                        return response1.Item.PicUrl + "|" + NoHTML(response1.Item.Title);
                    }
                    return "No";
            }
            catch (Exception)
            {
                return "No";
            }
        }

        /// <summary>
        /// 根据条件得到商品列表
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Products> GetProductByPage(Maticsoft.Model.SNS.ProductQuery Query,
                                                                   int StartIndex, int EndIndex)
        {
            return
                DataTableToList(
                    dal.GetProductByPage(BindstrWhere(Query), GetOrderby(Query.Order), StartIndex, EndIndex).Tables[0]);
        }

        /// <summary>
        ///根据条件得到商品列表（缓存）
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Products> GetCacheProductByPage(Maticsoft.Model.SNS.ProductQuery Query,
                                                                        int StartIndex, int EndIndex)
        {
            string CacheKey = "CacheProductByPage-" + Query.Tags + Query.QueryType + Query.MinPrice + Query.MaxPrice +
                              Query.Keywords + Query.CategoryID + Query.Order + Query.IsTopCategory + Query + StartIndex +
                              EndIndex;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetProductByPage(Query, StartIndex, EndIndex);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                            TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.Products>)objModel;
        }

        public string GetOrderby(string Order)
        {
            StringBuilder strWhere = new StringBuilder();
            string Condition;
            switch (Order)
            {
                case "popular":
                    Condition = " FavouriteCount ";
                    break;

                case "new":
                    Condition = " ProductID ";
                    break;

                case "hot":
                    Condition = " CommentCount ";
                    break;
                default:
                    Condition = " FavouriteCount ";
                    break;
            }
            strWhere.AppendFormat(" Order by {0} Desc", Condition);
            return strWhere.ToString();
        }

        /// <summary>
        /// 缓存得到符合条件的商品的个数
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public int GetCacheProductCount(Maticsoft.Model.SNS.ProductQuery Query)
        {
            string CacheKey = "CacheProductCount-" + Query.Tags + Query.QueryType + Query.MinPrice + Query.MaxPrice +
                              Query.Keywords + Query.CategoryID + Query.Order + Query.IsTopCategory + Query;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetProductCount(Query);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                            TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (int)objModel;
        }

        /// <summary>
        /// 得到符合条件的商品的个数
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public int GetProductCount(Maticsoft.Model.SNS.ProductQuery Query)
        {
            return GetRecordCount(BindstrWhere(Query));
        }

        /// <summary>
        /// 根据图片的类型和id获得其相应的信息，包括图片拥护者的信息和图片所在专辑的信息
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.SNS.TargetDetail GetTargetAssiationInfo(int pid)
        {
            Maticsoft.ViewModel.SNS.TargetDetail DetailModel = new ViewModel.SNS.TargetDetail();
            Maticsoft.BLL.Members.UsersExp UserEx = new Members.UsersExp();
            Maticsoft.BLL.SNS.UserAlbums UserAlbumBll = new UserAlbums();
            Maticsoft.BLL.SNS.UserAlbumDetail DetailAlbumBll = new UserAlbumDetail();
            Maticsoft.BLL.SNS.Photos PhotoBll = new BLL.SNS.Photos();
            DetailModel.Product = GetModel(pid);
            UpdatePvCount(pid);
            DetailModel.UserModel = UserEx.GetUsersExpModel(DetailModel.Userid);
            DetailModel.UserAlums = UserAlbumBll.GetUserAlbum((int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product,
                                                              DetailModel.TargetId, DetailModel.Userid);
            if (DetailModel.UserAlums != null)
            {
                DetailModel.CovorImageList = DetailAlbumBll.GetThumbImageByAlbum(DetailModel.UserAlums.AlbumID);
            }
            return DetailModel;
        }

        public List<Maticsoft.Model.SNS.Products> GetProductListByCid(int Cid)
        {
            return GetModelList("CategoryID=" + Cid + "");
        }

        public bool UpdatePvCount(int pid)
        {
            return dal.UpdatePvCount(pid);
        }

        public Maticsoft.Model.SNS.Products GetProductModel(Maticsoft.Model.SNS.Products PModel)
        {
            Maticsoft.BLL.SNS.Tags tagBll = new Tags();
            ITopClient client = BLL.SNS.TaoBaoConfig.GetTopClient();

            Maticsoft.BLL.SNS.CategorySource CateBll = new Maticsoft.BLL.SNS.CategorySource();
            Maticsoft.TaoBao.Domain.Item Item = new Maticsoft.TaoBao.Domain.Item();

            ItemGetRequest reqEx = new ItemGetRequest();
            reqEx.Fields =
                "title,price,num_iid,title,cid,nick,desc,price,detail_url,pic_url";
            reqEx.NumIid = PModel.ProductID;
            ItemGetResponse responseEx = client.Execute(reqEx);
            Item = responseEx.Item;
            PModel.ProductUrl = Item.DetailUrl;

            var cateModel = CateBll.GetModel(3, Convert.ToInt32(Item.Cid));
            PModel.CategoryID = cateModel != null ? cateModel.SnsCategoryId : 0;
            PModel.NormalImageUrl = Item.PicUrl;
            PModel.ThumbImageUrl = Item.PicUrl;
            PModel.Price = Common.Globals.SafeDecimal(Item.Price, 0);
            PModel.ProductName = NoHTML(Item.Title);
            PModel.ProductSourceID = (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.TaoBao;
            PModel.CreatedDate = DateTime.Now;

            //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
            string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
            if (!string.IsNullOrEmpty(Status))
            {
                PModel.Status = Common.Globals.SafeInt(Status, 1);
            }
            else
            {
                PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
            }

            // 最后的tags是 成色:全新|钱包款式:长款钱包|里料材质:合成革 这种形式

            #region 获取属性

            ItemGetRequest reqPro = new ItemGetRequest();
            reqPro.Fields = "props_name";
            reqPro.NumIid = PModel.ProductID;
            ItemGetResponse response2 = client.Execute(reqPro);
            string Prop = response2.Item.PropsName;
            PModel.Tags = tagBll.GetTagStr(Prop);

            #endregion 获取属性

            return PModel;
        }




        /// <summary>
        /// 根据条件获取淘宝商品数据
        /// </summary>
        /// <param name="cid">商品分类ID</param>
        /// <param name="keyword">商品关键字</param>
        /// <param name="page_no">页数</param>
        /// <param name="page_size">每页返回结果数</param>
        /// <param name="shop_type">店铺类型.默认all,商城:b,集市:c </param>
        /// <param name="area">商品所在地</param>
        /// <param name="start_coupon_rate">设置折扣比例范围下限,如：7000表示70.00% </param>
        /// <param name="end_coupon_rate">设置折扣比例范围上限,如：8000表示80.00%.注：要起始折扣比率和最高折扣比率一起设置才有效 </param>
        /// <param name="start_commission_rate">起始佣金比率选项，如：1234表示12.34% </param>
        /// <param name="end_commission_rate">最高佣金比率选项，如：2345表示23.45%。注：要起始佣金比率和最高佣金比率一起设置才有效。 </param>
        /// <param name="start_credit">卖家信用: 1heart(一心) 2heart (两心) 3heart(三心) 4heart(四心) 5heart(五心) 1diamond(一钻) 2diamond(两钻) 3diamond(三钻) 4diamond(四钻) 5diamond(五钻) 1crown(一冠) 2crown(两冠) 3crown(三冠) 4crown(四冠) 5crown(五冠) 1goldencrown(一黄冠) 2goldencrown(二黄冠) 3goldencrown(三黄冠) 4goldencrown(四黄冠) 5goldencrown(五黄冠) </param>
        /// <param name="end_credit">可选值和start_credit一样.start_credit的值一定要小于或等于end_credit的值。注：end_credit与start_credit一起使用才生效 </param>
        public List<TaoBao.Domain.TbkItem> GetProductDates(int cid, string keyword, string area = "",
                                                                int page_no = 1, int page_size = 40,
                                                                string sort = "price_desc",
                                                                int start_commission_rate = 0,
                                                                int end_commission_rate = 0, string start_credit = "",
                                                                string end_credit = "", int start_commissionNum = 0,
                                                                int end_commissionNum = 0)
        {
            List<TaoBao.Domain.TbkItem> ProductDates = new List<TaoBao.Domain.TbkItem>();
            ITopClient client = BLL.SNS.TaoBaoConfig.GetTopClient();
            TbkItemsGetRequest req = new TbkItemsGetRequest();
            if (cid > 0)
            {
                req.Cid = cid;
            }

            req.PageSize = page_size;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                req.Keyword = keyword;
            }
            if (!String.IsNullOrWhiteSpace(area))
            {
                req.Area = area;
            }

            if (start_commission_rate > 0)
            {
                req.StartCommissionRate = start_commission_rate.ToString();
            }
            if (end_commission_rate > 0)
            {
                req.EndCommissionRate = end_commission_rate.ToString();
            }
            if (!String.IsNullOrWhiteSpace(start_credit))
            {
                req.StartCredit = start_credit;
            }
            if (!String.IsNullOrWhiteSpace(end_credit))
            {
                req.EndCredit = end_credit;
            }
            if (start_commissionNum > 0)
            {
                req.StartCommissionNum = start_commissionNum.ToString();
            }
            if (end_commissionNum > 0)
            {
                req.EndCommissionNum = end_commissionNum.ToString();
            }
            req.Sort = sort;
            //req.Fields = "num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume,coupon_price,coupon_rate,coupon_start_time,coupon_end_time,shop_type";//其余字段以后备用
            req.Fields = "num_iid,title,pic_url,price,item_url";
            for (int i = 1; i <= page_no; i++)
            {
                req.PageNo = i;
                TbkItemsGetResponse response = client.Execute(req);
                if (response.TbkItems.Count > 0)
                {
                    ProductDates.AddRange(response.TbkItems);
                }
            }
            return ProductDates;
        }

        /// <summary>
        /// 批量导入淘宝商品数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int ImportData(int userid, int albumId, int cid, List<TaoBao.Domain.TbkItem> list, bool ReRepeat = true)
        {
            int count = 0;
            ITopClient client = BLL.SNS.TaoBaoConfig.GetTopClient();
            //一次性只能填入10个Id字符串的值 暂时采用循环形式填值
            // req.NumIids = ids;
            if (list.Count > 0)
            {
                Maticsoft.BLL.SNS.Tags tagBll = new Tags();
                Maticsoft.BLL.SNS.CategorySource CateBll = new Maticsoft.BLL.SNS.CategorySource();

                Accounts.Bus.User userModel = new Accounts.Bus.User(userid);
                if (userModel == null)
                {
                    return 0;
                }
                Model.SNS.Products PModel = null;
                foreach (var item in list)
                {
                    #region 需要去重
                    if (ReRepeat)
                    {
                        if (!Exsit(item.NumIid, (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.TaoBao))
                        {
                            PModel = new Model.SNS.Products();
                            var cateModel = CateBll.GetModel(3, Convert.ToInt32(cid));
                            PModel.CategoryID = cateModel != null ? cateModel.SnsCategoryId : 0;
                            PModel.NormalImageUrl = item.PicUrl;
                            PModel.ThumbImageUrl = item.PicUrl;
                            PModel.Price = Common.Globals.SafeDecimal(item.Price, 0);
                            PModel.ProductName = NoHTML(item.Title);
                            PModel.ProductUrl = item.ItemUrl;
                            PModel.CreateUserID = userModel.UserID;
                            PModel.CreatedNickName = userModel.NickName;
                            PModel.ProductSourceID = (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.TaoBao;
                            PModel.OriginalID = item.NumIid;
                            PModel.SourceType = 1;
                            PModel.CreatedDate = DateTime.Now;

                            //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
                            string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                            if (!string.IsNullOrEmpty(Status))
                            {
                                PModel.Status = Common.Globals.SafeInt(Status, 1);
                            }
                            else
                            {
                                PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
                            }

                            #region 获取属性

                            ItemGetRequest reqPro = new ItemGetRequest();
                            reqPro.Fields = "props_name";
                            reqPro.NumIid = item.NumIid;
                            ItemGetResponse response2 = client.Execute(reqPro);

                            string Prop = response2.Item == null ? "" : response2.Item.PropsName;
                            if (!String.IsNullOrWhiteSpace(Prop))
                            {
                                PModel.Tags = tagBll.GetTagStr(Prop);
                            }


                            #endregion 获取属性

                            long productId = Add(PModel);
                            if (productId > 0)
                            {
                                Maticsoft.BLL.SNS.UserAlbumDetail detailBll = new UserAlbumDetail();
                                Maticsoft.Model.SNS.UserAlbumDetail detailModel = new Model.SNS.UserAlbumDetail();
                                detailModel.AlbumID = albumId;
                                detailModel.TargetID = (int)productId;
                                detailModel.Type = 1;
                                detailModel.AlbumUserId = userid;
                                detailBll.AddEx(detailModel);
                                count++;
                            }
                        }
                    }
                    #endregion
                    #region 不需要去重
                    else
                    {
                        PModel = new Model.SNS.Products();
                        var cateModel = CateBll.GetModel(3, Convert.ToInt32(cid));
                        PModel.CategoryID = cateModel != null ? cateModel.SnsCategoryId : 0;
                        PModel.NormalImageUrl = item.PicUrl;
                        PModel.ThumbImageUrl = item.PicUrl;
                        PModel.Price = Common.Globals.SafeDecimal(item.Price, 0);
                        PModel.ProductName = NoHTML(item.Title);
                        PModel.ProductUrl = item.ClickUrl;
                        PModel.CreateUserID = userModel.UserID;
                        PModel.CreatedNickName = userModel.NickName;
                        PModel.ProductSourceID = (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.TaoBao;
                        PModel.OriginalID = item.NumIid;
                        PModel.SourceType = 1;
                        PModel.CreatedDate = DateTime.Now;

                        //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
                        string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                        if (!string.IsNullOrEmpty(Status))
                        {
                            PModel.Status = Common.Globals.SafeInt(Status, 1);
                        }
                        else
                        {
                            PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
                        }

                        #region 获取属性

                        ItemGetRequest reqPro = new ItemGetRequest();
                        reqPro.Fields = "props_name";
                        reqPro.NumIid = item.NumIid;
                        ItemGetResponse response2 = client.Execute(reqPro);
                        string Prop = response2.Item.PropsName;
                        PModel.Tags = tagBll.GetTagStr(Prop);

                        #endregion 获取属性

                        long productId = Add(PModel);
                        if (productId > 0)
                        {
                            Maticsoft.BLL.SNS.UserAlbumDetail detailBll = new UserAlbumDetail();
                            Maticsoft.Model.SNS.UserAlbumDetail detailModel = new Model.SNS.UserAlbumDetail();
                            detailModel.AlbumID = albumId;
                            detailModel.TargetID = (int)productId;
                            detailModel.Type = 1;
                            detailModel.AlbumUserId = userid;
                            detailBll.AddEx(detailModel);
                            count++;
                        }
                    }
                    #endregion
                }
            }
            return count;
        }
        #region 淘宝数据Excel 批量导入
        /// <summary>
        /// 淘宝Excel数据导入
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="albumId"></param>
        /// <param name="categoryId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int ImportExcelData(int userid, int albumId, int categoryId, DataTable dt, bool ReRepeat = true)
        {
            int count = 0;
            //一次性只能填入10个Id字符串的值 暂时采用循环形式填值
            // req.NumIids = ids;
            if (dt.Rows.Count > 0)
            {
                //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
                string StatusStr = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
                int status = 0;
                if (!string.IsNullOrEmpty(StatusStr))
                {
                    status = Common.Globals.SafeInt(StatusStr, 1);
                }
                else
                {
                    status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
                }
                count = dal.ImportExcelData(userid, albumId, categoryId, dt, status, ReRepeat);
            }

            //Accounts.Bus.User userModel = new Accounts.Bus.User(userid);
            //    foreach (var item in productList)
            //    {
            //        if (!ExsitUrl(item.ProductUrl.Trim()))
            //        {

            //            item.CreateUserID = userModel.UserID;
            //            item.CreatedNickName = userModel.NickName;
            //            item.ProductSourceID = (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.TaoBao;
            //            item.SourceType = 1;
            //            item.CreatedDate = DateTime.Now;
            //            item.Price = item.Price.HasValue ? item.Price : -1;
            //            item.CategoryID = categoryId;

            //            //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
            //            string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductDefaultStatus");
            //            if (!string.IsNullOrEmpty(Status))
            //            {
            //                item.Status = Common.Globals.SafeInt(Status, 1);
            //            }
            //            else
            //            {
            //                item.Status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
            //            }

            //            long productId = Add(item);
            //            if (productId > 0)
            //            {
            //                Maticsoft.BLL.SNS.UserAlbumDetail detailBll = new UserAlbumDetail();
            //                Maticsoft.Model.SNS.UserAlbumDetail detailModel = new Model.SNS.UserAlbumDetail();
            //                detailModel.AlbumID = albumId;
            //                detailModel.TargetID = (int)productId;
            //                detailModel.Type = 1;
            //                detailModel.AlbumUserId = userid;
            //                detailBll.AddEx(detailModel);
            //                count++;
            //            }
            //        }
            //    }
            //}
            return count;
        }
        /// <summary>
        /// 获得Excel数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Products> ExcelToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Products> modelList = new List<Maticsoft.Model.SNS.Products>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Products model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.SNS.Products();
                    if (dt.Rows[n][1] != null && dt.Rows[n][1].ToString() != "")
                    {
                        model.NormalImageUrl = dt.Rows[n][1].ToString();
                        model.ThumbImageUrl = dt.Rows[n][1].ToString() + "_300x300.jpg";
                    }
                    if (dt.Rows[n][2] != null && dt.Rows[n][2].ToString() != "")
                    {
                        model.ProductName = dt.Rows[n][2].ToString();
                    }
                    if (dt.Rows[n][3] != null && dt.Rows[n][3].ToString() != "")
                    {
                        model.Price = Common.Globals.SafeDecimal(dt.Rows[n][3].ToString(), -1);
                    }
                    if (dt.Rows[n][4] != null && dt.Rows[n][4].ToString() != "")
                    {
                        model.ProductUrl = dt.Rows[n][4].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion

        /// <summary>
        /// 通过获取淘宝端的商品信息,同时加入相应的专辑，并且还要想动态表中插入相应的动态消息
        /// </summary>
        /// <param name="pid">产品id</param>
        /// <param name="AblumId">专辑id</param>
        /// <param name="UserId">用户id</param>
        /// <param name="NickName">用户昵称</param>
        /// <param name="ShareDescription">分享词</param>
        /// <param name="UserIp">用户的id</param>
        /// <returns></returns>
        //public int Add(long pid,int AblumId, int UserId,string NickName,string ShareDescription,string UserIp)
        //{
        //    Model.SNS.Products PModel = new Model.SNS.Products();
        //    //BLL.SNS.Products PBll = new BLL.SNS.Products();
        //    BLL.SNS.ProductSources SourcesBll=new ProductSources();
        //    Model.SNS.UserAlbumDetail DetailModel = new Model.SNS.UserAlbumDetail();
        //    BLL.SNS.UserAlbumDetail DetailBll = new UserAlbumDetail();
        //    Model.SNS.Posts PostModel = new Model.SNS.Posts();
        //    BLL.SNS.Posts PostBll = new BLL.SNS.Posts();
        //    ITopClient client = BLL.SysManage.TaoBaoConfig.GetTopClient();
        //    ItemGetRequest req = new ItemGetRequest();
        //    req.Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,location,price,post_fee,express_fee,ems_fee,freight_payer,item_img.url,click_url,shop_click_url,seller_credit_score,num,props_name,detail_url,pic_url";
        //    req.NumIid = pid;
        //    ItemGetResponse response = client.Execute(req);
        //    string Prop = response.Item.PropsName;
        //    PModel.CategoryID =Convert.ToInt32( response.Item.Cid);
        //    PModel.CreatedDate = DateTime.Now;
        //    PModel.CreatedNickName = NickName;
        //    PModel.CreateUserID = UserId;
        //    PModel.NormalImageUrl = response.Item.PicUrl;
        //    PModel.Price = Common.Globals.SafeDecimal(response.Item.Price, 0);
        //    PModel.ProductID = pid;
        //    PModel.ProductName = response.Item.Title;
        //    PModel.ProductSourceID = SourcesBll.GetIdByWebSiteName("淘宝网");
        //    PModel.ProductUrl = response.Item.DetailUrl;
        //    PModel.ShareDescription = ShareDescription;
        //    //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
        //    if (string.IsNullOrEmpty(SysManage.ConfigSystem.GetValue("SNS_ProductDefaultStatus")))
        //    {
        //        PModel.Status = Common.Globals.SafeInt(SysManage.ConfigSystem.GetValue("SNS_ProductDefaultStatus"), 0);
        //    }
        //    else
        //    {
        //        PModel.Status=(int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
        //    }
        //    ///最后的tags是 成色:全新|钱包款式:长款钱包|里料材质:合成革 这种形式
        //    string[] Props = Maticsoft.Common.StringPlus.GetStrArray(Prop);
        //    foreach (string item in Props)
        //    {
        //         string[] Taglist = item.Split(':');
        //        if(Taglist.Length>3)
        //        {
        //            if (string.IsNullOrEmpty(PModel.Tags))
        //            {
        //                PModel.Tags = Taglist[2] + ":" + Taglist[3];
        //            }
        //            else
        //            {
        //                PModel.Tags = PModel.Tags + "|" + Taglist[2] + ":" + Taglist[3];
        //            }
        //        }
        //    }
        //    int TargetId;
        //    #region 先加入产品表再加入专辑和动态
        //    if ((TargetId = Add(PModel)) > 0)
        //    {
        //        DetailModel.AlbumID = AblumId;
        //        DetailModel.Description = ShareDescription;
        //        DetailModel.TargetID = TargetId;
        //        DetailModel.Type = (int)Model.SNS.EnumHelper.ImageType.Product;
        //        int Id;
        //       //加入专辑
        //        if ((Id = DetailBll.Add(DetailModel)) > 0)
        //        {
        //            PostModel.CreatedDate = DateTime.Now;
        //            PostModel.CreatedNickName = NickName;
        //            PostModel.CreatedUserID = UserId;
        //            PostModel.Description =NickName+"分享了商品</br>"+ ShareDescription;
        //            if (ShareDescription.Contains("@"))
        //            {
        //                //如果包括@说明有可能提到某人，在展现数据的时候
        //                PostModel.HasReferUsers = true;
        //            }
        //            else
        //            {
        //                PostModel.HasReferUsers = false;
        //            }
        //            PostModel.ImageUrl = PModel.NormalImageUrl;
        //            PostModel.IsRecommend = false;
        //            PostModel.OriginalID = 0;
        //            PostModel.Price = PModel.Price;
        //            PostModel.ProductLinkUrl = PModel.ProductUrl;
        //            PostModel.ProductName = PModel.ProductName;
        //            PostModel.Status = (int)Model.SNS.EnumHelper.Status.Unable;
        //            PostModel.TargetId = TargetId;
        //            PostModel.Type = (int)Model.SNS.EnumHelper.ImageType.Product;
        //            PostModel.UserIP = UserIp;
        //            int ResultID;
        //            //加入动态
        //            if ((ResultID = PostBll.Add(PostModel)) > 0)
        //            {
        //                return ResultID;
        //            }
        //            else
        //            {
        //                //如果失败删除产品信息和加入专辑信息
        //                Delete(TargetId);
        //                DetailBll.Delete(Id);
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //           //删除产品信息
        //            Delete(TargetId);
        //            return 0;
        //        }
        //    #endregion
        //    }
        //    return 0;
        //}

        /// <summary>
        /// 删除一条数据（事务删除）
        /// </summary>
        public bool DeleteEX(int ProductID)
        {
            return dal.DeleteEX(ProductID);
        }

        /// <summary>
        /// 批量删除数据（事务删除）
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        public bool DeleteListEX(string ProductIds)
        {
            return dal.DeleteListEX(ProductIds);
        }

        /// <summary>
        /// 批量转移分类
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        public bool UpdateCateList(string ProductIds, int CateId)
        {
            return dal.UpdateCateList(ProductIds, CateId);
        }

        /// <summary>
        /// 批量推荐到首页
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        public bool UpdateRecomendList(string ProductIds, int Recomend)
        {
            return dal.UpdateRecomendList(ProductIds, Recomend);
        }

        public bool UpdateRecomend(int ProductId, int Recomend)
        {
            return dal.UpdateRecomend(ProductId, Recomend);
        }

        public bool UpdateStatus(int ProductId, int Status)
        {
            return dal.UpdateStatus(ProductId, Status);
        }

        /// <summary>
        /// 批量转移分类
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        public bool UpdateEX(int ProductId, int CateId)
        {
            return dal.UpdateEX(ProductId, CateId);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountEx(string strWhere, int CateId)
        {
            return dal.GetRecordCountEx(strWhere, CateId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere, int CateId)
        {
            return dal.GetListEx(strWhere, CateId);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPageEx(strWhere, CateId, orderby, startIndex, endIndex);
        }

        public bool UpdateRecommandState(int id, int State)
        {
            return dal.UpdateRecommandState(id, State);
        }


        /// <summary>
        /// 根据专辑ID获取该用户自定义上传的商品路径
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <returns>结果集</returns>
        public List<Model.SNS.Products> UserUploadPhotoList(int ablumId)
        {
            DataSet ds = dal.UserUploadProductsImage(ablumId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 此人同样的商品是否发了两遍
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public bool Exsit(string ProductName, int Uid)
        {
            return dal.Exsit(ProductName, Uid);

        }

        /// <summary>
        /// 此人同样的商品是否发了两遍
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public bool Exsit(long originalID, int type)
        {

            return dal.Exsit(originalID, type);

        }

        /// <summary>
        /// 此人同样的商品是否发了两遍
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public bool ExsitUrl(string ProductUrl, int Uid)
        {
            return dal.ExsitUrl(ProductUrl, Uid);

        }
        /// <summary>
        /// 删除商品ids
        /// </summary>
        public DataSet DeleteListEx(string Ids, out int Result, bool IsSendMess = false, int SendUserID = 1)
        {
            List<int> UserIdList = GetProductUserIds(Ids);
            DataSet ds = dal.DeleteListEx(Ids, out Result);
            if (Result > 0 && IsSendMess)
            {
                Maticsoft.BLL.Members.SiteMessage siteBll = new SiteMessage();
                foreach (var userId in UserIdList)
                {
                    siteBll.AddMessageByUser(SendUserID, userId, "商品删除",
                                             "您分享的商品涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
                }
            }
            return ds;
        }

        /// <summary>
        ///更新导购数
        /// </summary>
        /// <param name="ProuductId"></param>
        /// <returns></returns>
        public bool UpdateClickCount(int ProuductId)
        {
            return dal.UpdateClickCount(ProuductId);
        }

        /// <summary>
        /// 获取需要静态化的商品数据
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<int> GetListToStatic(string strWhere)
        {
            DataSet ds = dal.GetListToStatic(strWhere);
            List<int> ProductIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["ProductID"] != null && ds.Tables[0].Rows[n]["ProductID"].ToString() != "")
                    {
                        ProductIdList.Add(int.Parse(ds.Tables[0].Rows[n]["ProductID"].ToString()));
                    }
                }
            }
            return ProductIdList;
        }

        /// <summary>
        /// 更新静态页面地址
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="staticUrl"></param>
        /// <returns></returns>
        public bool UpdateStaticUrl(int productId, string staticUrl)
        {
            return dal.UpdateStaticUrl(productId, staticUrl);
        }

        /// <summary>
        /// 获取商品的链接地址
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public string GetProductUrl(long productId)
        {
            return dal.GetProductUrl(productId);
        }

        /// <summary>
        /// 获取商品的链接地址(缓存)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public string GetProductUrlByCache(long productId)
        {
            string CacheKey = "GetProductUrlByCache-" + productId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetProductUrl(productId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                            TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return objModel.ToString();
        }

        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public string NoHTML(string Htmlstring)
        {

            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
                                       RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
                                       RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        public List<int> GetProductUserIds(string ids)
        {
            DataSet ds = dal.GetProductUserIds(ids);
            List<int> UserIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["CreateUserID"] != null &&
                        ds.Tables[0].Rows[n]["CreateUserID"].ToString() != "")
                    {
                        UserIdList.Add(int.Parse(ds.Tables[0].Rows[n]["CreateUserID"].ToString()));
                    }
                }
            }
            return UserIdList;
        }

        /// <summary>
        /// Excel导入使用
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public bool ExsitUrl(string ProductUrl)
        {

            return dal.ExsitUrl(ProductUrl);

        }


        /// <summary>
        /// 根据条件获取淘宝普通商品数据
        /// </summary>

        public List<TaoBao.Domain.Product> GetTaoDataList(int cid, string keyword, int page_no = 1, int page_size = 40, int vertical_market = 3, string market_id = "1", string status = "0,3")
        {
            List<TaoBao.Domain.Product> TaoDataList = new List<TaoBao.Domain.Product>();
            ITopClient client = BLL.SNS.TaoBaoConfig.GetTopClient();
            ProductsSearchRequest req = new ProductsSearchRequest();
            if (cid > 0)
            {
                req.Cid = cid;
            }

            req.PageSize = page_size;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                req.Q = keyword;
            }
            req.Status = status;
            if (vertical_market > 0)
            {
                req.VerticalMarket = vertical_market;
            }
            req.Fields = "product_id,cid,price,name,pic_url";
            for (int i = 1; i <= page_no; i++)
            {
                req.PageNo = i;
                ProductsSearchResponse response = client.Execute(req);
                if (response.Products.Count > 0)
                {
                    TaoDataList.AddRange(response.Products);
                }
            }
            return TaoDataList;
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatusList(string ids, int status)
        {
            return dal.UpdateStatusList(ids, status);
        }

        /// <summary>
        /// 获取商品图片列表
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="top"></param>
        /// <param name="hasChild"></param>
        /// <returns></returns>
        //public List<Maticsoft.Model.SNS.Products> GetProductList(int cid,int top=10,bool hasChild=true )
        //{
        //    DataSet ds = dal.GetProductList(cid, top, hasChild);
        //    return DataTableToList(ds.Tables[0]);
        //}
        #endregion ExtensionMethod
    }
}