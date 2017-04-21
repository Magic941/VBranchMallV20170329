using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using Maticsoft.OAuth;
using System.Linq;
using Maticsoft.Model.Members.Enum;
using Maticsoft.OAuth.v2;

namespace Maticsoft.BLL.Members
{
    /// <summary>
    /// UserBind
    /// </summary>
    public partial class UserBind
    {
        private readonly IUserBind dal = DAMembers.CreateUserBind();
        public UserBind()
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
        public bool Exists(int BindId)
        {
            return dal.Exists(BindId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UserBind model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.UserBind model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int BindId)
        {

            return dal.Delete(BindId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string BindIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(BindIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.UserBind GetModel(int BindId)
        {

            return dal.GetModel(BindId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.UserBind GetModelByCache(int BindId)
        {

            string CacheKey = "UserBindModel-" + BindId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BindId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.UserBind)objModel;
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
        public List<Maticsoft.Model.Members.UserBind> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.UserBind> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UserBind> modelList = new List<Maticsoft.Model.Members.UserBind>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.UserBind model;
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
        #region  ExtensionMethod
        //发新浪微博
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.UserBind GetModel(int userId, int MediaID)
        {
            return dal.GetModel(userId, MediaID);
        }

        /// <summary>
        /// 自主开发微博同步接口
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public void SendWeiBo(int userId, string mediaIDs, string content, string url, string imageUrl = null)
        {
            string strWhere = " userid=" + userId;
            //如果是-1 表示是绑定全部的微博帐号
            if (!String.IsNullOrWhiteSpace(mediaIDs))
            {
                //获取选择的媒体
                strWhere += " and MediaID in  (" + mediaIDs + ")";
            }
            List<Maticsoft.Model.Members.UserBind> bindList = GetModelList(strWhere);
            if (bindList == null || bindList.Count == 0)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "http://" + Common.Globals.DomainFullName;
            }
            foreach (var userBind in bindList)
            {
                //TODO: 代码过度臃肿 应精简 TO: 涂 BEN ADD 20130823
                switch (userBind.MediaID)
                {
                    case (int)Maticsoft.Model.Members.Enum.MediaType.Sina://新浪微博
                        //获取新浪微博AppKey
                        string SinaAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaAppId");
                        string SinaSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaSercet");
                        IOAuth2ServiceProvider<OAuth.Sina.IWeibo> weiboProvider =
                            new OAuth.Sina.WeiboServiceProvider(SinaAppId, SinaSercet);
                        //加载用户信息
                        OAuth.Sina.IWeibo weiboClient = weiboProvider.GetApi(
                            new AccessGrant(userBind.TokenAccess,
                               new[] { userBind.MediaUserID }));
                        try
                        {
                            #region 发表文字内容

                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                weiboClient.UpdateStatusAsync(content + " " + url).Wait();
                            }

                            #endregion
                            #region 发表文字+图片内容
                            //System.Net.WebClient webclient = new System.Net.WebClient();
                            //webclient.DownloadFile("", "");   
                            else
                            {
                                //如果是云存储图片, 下载到本地后获取流
                                string weiBoImage = imageUrl;
                                if (imageUrl.Contains("http://"))
                                {
                                    System.Net.WebClient webclient = new System.Net.WebClient();
                                    string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                                    {
                                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                                    }
                                    weiBoImage = savePath + CreateIDCode() + ".jpg";
                                    webclient.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(weiBoImage));
                                }
                                //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
                                weiboClient.UploadStatusAsync(content + url,
                                    new System.IO.FileInfo(HttpContext.Current.Server.MapPath(weiBoImage))).Wait();
                            }


                            #endregion
                        }
                        catch (Exception ex)
                        {
                            //写错误日志
                            Maticsoft.Model.SysManage.ErrorLog errorLog = new ErrorLog();
                            errorLog.Loginfo = ex.Message;
                            if (ex.InnerException != null && ex.InnerException is Maticsoft.OAuth.Rest.Client.HttpResponseException)
                            {
                                errorLog.Loginfo =
                                    System.Text.Encoding.Default.GetString(
                                        ((Maticsoft.OAuth.Rest.Client.HttpResponseException)(ex.InnerException))
                                            .Response.Body);
                            }
                            errorLog.OPTime = DateTime.Now;
                            errorLog.StackTrace = ex.StackTrace;
                            errorLog.Url = "";
                            Maticsoft.BLL.SysManage.ErrorLog.Add(errorLog);
                        }
                        break;
                    //QQ 空间
                    case (int)Maticsoft.Model.Members.Enum.MediaType.QZone:
                        string QQAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQAppId");
                        string QQSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQSercet");
                        IOAuth2ServiceProvider<OAuth.Tencent.QQ.IQConnect> connectProvider =
           new OAuth.Tencent.QQ.QConnectServiceProvider(QQAppId, QQSercet);
                        OAuth.Tencent.QQ.IQConnect QQClient = connectProvider.GetApi(
                                 new AccessGrant(userBind.TokenAccess,
                                       new[] { userBind.MediaUserID }));
                        try
                        {
                            #region 发表文字内容

                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                QQClient.UpdateStatusAsync(content + url).Wait();
                            }
                            #endregion
                            #region 发表文字+图片内容
                            else
                            {
                                //如果是云存储图片, 下载到本地后获取流
                                string weiBoImage = imageUrl;
                                if (imageUrl.Contains("http://"))
                                {
                                    System.Net.WebClient webclient = new System.Net.WebClient();
                                    string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                                    {
                                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                                    }
                                    weiBoImage = savePath + CreateIDCode() + ".jpg";
                                    webclient.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(weiBoImage));
                                }
                                //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
                                QQClient.UploadStatusAsync(content + url,
                                    new System.IO.FileInfo(HttpContext.Current.Server.MapPath(weiBoImage))).Wait();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            //写错误日志
                            Maticsoft.Model.SysManage.ErrorLog errorLog = new ErrorLog();
                            errorLog.Loginfo = ex.Message;
                            if (ex.InnerException != null && ex.InnerException is Maticsoft.OAuth.Rest.Client.HttpResponseException)
                            {
                                errorLog.Loginfo =
                                    System.Text.Encoding.Default.GetString(
                                        ((Maticsoft.OAuth.Rest.Client.HttpResponseException)(ex.InnerException))
                                            .Response.Body);
                            }
                            errorLog.OPTime = DateTime.Now;
                            errorLog.StackTrace = ex.StackTrace;
                            errorLog.Url = "";
                            Maticsoft.BLL.SysManage.ErrorLog.Add(errorLog);
                        }
                        break;
                    //腾讯微博
                    case (int)MediaType.Tencent:
                        string TencentAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentAppId");
                        string TencentSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentSercet");

                        //腾讯微博API
                        IOAuth2ServiceProvider<OAuth.Tencent.Weibo.IWeibo> tencentProvider =
                            new OAuth.Tencent.Weibo.WeiboServiceProvider(TencentAppId, TencentSercet);

                        string[] openIdKeys = userBind.MediaUserID.Split(new[] { '|' },
                            StringSplitOptions.RemoveEmptyEntries);

                        if (openIdKeys.Length < 2) throw new ArgumentNullException(" OpenIdKeys is NULL !");

                        //加载用户信息
                        OAuth.Tencent.Weibo.IWeibo tencentClient = tencentProvider.GetApi(
                            new AccessGrant(userBind.TokenAccess,
                               new[] { openIdKeys[0], openIdKeys[1], Globals.ClientIP }));
                        try
                        {
                            #region 发表文字内容

                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                tencentClient.UpdateStatusAsync(content + url).Wait();
                            }
                            #endregion
                            #region 发表文字+图片内容
                            else
                            {
                                //如果是云存储图片, 下载到本地后获取流
                                string weiBoImage = imageUrl;
                                if (imageUrl.Contains("http://"))
                                {
                                    System.Net.WebClient webclient = new System.Net.WebClient();
                                    string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                                    {
                                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                                    }
                                    weiBoImage = savePath + CreateIDCode() + ".jpg";
                                    webclient.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(weiBoImage));
                                }
                                //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
                                tencentClient.UploadStatusAsync(content + url,
                                    new System.IO.FileInfo(HttpContext.Current.Server.MapPath(weiBoImage))).Wait();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            //写错误日志
                            Maticsoft.Model.SysManage.ErrorLog errorLog = new ErrorLog();
                            errorLog.Loginfo = ex.Message;
                            if (ex.InnerException != null && ex.InnerException is Maticsoft.OAuth.Rest.Client.HttpResponseException)
                            {
                                errorLog.Loginfo =
                                    System.Text.Encoding.Default.GetString(
                                        ((Maticsoft.OAuth.Rest.Client.HttpResponseException)(ex.InnerException))
                                            .Response.Body);
                            }
                            errorLog.OPTime = DateTime.Now;
                            errorLog.StackTrace = ex.StackTrace;
                            errorLog.Url = "";
                            Maticsoft.BLL.SysManage.ErrorLog.Add(errorLog);
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// 自主开发微博同步接口
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public void SendWeiBo(string bindIds, string content, string url, string imageUrl = null)
        {
            string strWhere = "";
            //如果是-1 表示是绑定全部的微博帐号
            if (!String.IsNullOrWhiteSpace(bindIds))
            {
                //获取选择的媒体
                strWhere += "  BindId in  (" + bindIds + ")";
            }
            List<Maticsoft.Model.Members.UserBind> bindList = GetModelList(strWhere);
            if (bindList == null || bindList.Count == 0)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "http://" + Common.Globals.DomainFullName;
            }
            foreach (var userBind in bindList)
            {
                switch (userBind.MediaID)
                {
                    case (int) Maticsoft.Model.Members.Enum.MediaType.Sina: //新浪微博
                        //获取新浪微博AppKey
                        string SinaAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaAppId");
                        string SinaSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaSercet");
                        IOAuth2ServiceProvider<OAuth.Sina.IWeibo> weiboProvider =
                            new OAuth.Sina.WeiboServiceProvider(SinaAppId, SinaSercet);
                        //加载用户信息
                        OAuth.Sina.IWeibo weiboClient = weiboProvider.GetApi(
                            new AccessGrant(userBind.TokenAccess,
                                new[] {userBind.MediaUserID}));
                        try
                        {
                            #region 发表文字内容

                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                weiboClient.UpdateStatusAsync(content + " " + url).Wait();
                            }

                                #endregion
                                #region 发表文字+图片内容
                                //System.Net.WebClient webclient = new System.Net.WebClient();
                                //webclient.DownloadFile("", "");   
                            else
                            {
                                //如果是云存储图片, 下载到本地后获取流
                                string weiBoImage = imageUrl;
                                if (imageUrl.Contains("http://"))
                                {
                                    System.Net.WebClient webclient = new System.Net.WebClient();
                                    string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                                    {
                                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                                    }
                                    weiBoImage = savePath + CreateIDCode() + ".jpg";
                                    webclient.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(weiBoImage));
                                }
                                //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
                                weiboClient.UploadStatusAsync(content + url,
                                    new System.IO.FileInfo(HttpContext.Current.Server.MapPath(weiBoImage))).Wait();
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            //写错误日志
                            Maticsoft.Model.SysManage.ErrorLog errorLog = new ErrorLog();
                            errorLog.Loginfo = ex.Message;
                            if (ex.InnerException != null &&
                                ex.InnerException is Maticsoft.OAuth.Rest.Client.HttpResponseException)
                            {
                                errorLog.Loginfo =
                                    System.Text.Encoding.Default.GetString(
                                        ((Maticsoft.OAuth.Rest.Client.HttpResponseException) (ex.InnerException))
                                            .Response.Body);
                            }
                            errorLog.OPTime = DateTime.Now;
                            errorLog.StackTrace = ex.StackTrace;
                            errorLog.Url = "";
                            Maticsoft.BLL.SysManage.ErrorLog.Add(errorLog);
                        }
                        break;
                        //QQ 空间
                    case (int) Maticsoft.Model.Members.Enum.MediaType.QZone:
                        string QQAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQAppId");
                        string QQSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQSercet");
                        IOAuth2ServiceProvider<OAuth.Tencent.QQ.IQConnect> connectProvider =
                            new OAuth.Tencent.QQ.QConnectServiceProvider(QQAppId, QQSercet);
                        OAuth.Tencent.QQ.IQConnect QQClient = connectProvider.GetApi(
                            new AccessGrant(userBind.TokenAccess,
                                new[] {userBind.MediaUserID}));
                        try
                        {
                            #region 发表文字内容

                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                QQClient.UpdateStatusAsync(content + url).Wait();
                            }
                                #endregion
                                #region 发表文字+图片内容

                            else
                            {
                                //如果是云存储图片, 下载到本地后获取流
                                string weiBoImage = imageUrl;
                                if (imageUrl.Contains("http://"))
                                {
                                    System.Net.WebClient webclient = new System.Net.WebClient();
                                    string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                                    {
                                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                                    }
                                    weiBoImage = savePath + CreateIDCode() + ".jpg";
                                    webclient.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(weiBoImage));
                                }
                                //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
                                QQClient.UploadStatusAsync(content + url,
                                    new System.IO.FileInfo(HttpContext.Current.Server.MapPath(weiBoImage))).Wait();
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            //写错误日志
                            Maticsoft.Model.SysManage.ErrorLog errorLog = new ErrorLog();
                            errorLog.Loginfo = ex.Message;
                            if (ex.InnerException != null &&
                                ex.InnerException is Maticsoft.OAuth.Rest.Client.HttpResponseException)
                            {
                                errorLog.Loginfo =
                                    System.Text.Encoding.Default.GetString(
                                        ((Maticsoft.OAuth.Rest.Client.HttpResponseException) (ex.InnerException))
                                            .Response.Body);
                            }
                            errorLog.OPTime = DateTime.Now;
                            errorLog.StackTrace = ex.StackTrace;
                            errorLog.Url = "";
                            Maticsoft.BLL.SysManage.ErrorLog.Add(errorLog);
                        }
                        break;
                        //腾讯微博
                    case (int) MediaType.Tencent:
                        string TencentAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentAppId");
                        string TencentSercet =
                            Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentSercet");

                        //腾讯微博API
                        IOAuth2ServiceProvider<OAuth.Tencent.Weibo.IWeibo> tencentProvider =
                            new OAuth.Tencent.Weibo.WeiboServiceProvider(TencentAppId, TencentSercet);

                        string[] openIdKeys = userBind.MediaUserID.Split(new[] {'|'},
                            StringSplitOptions.RemoveEmptyEntries);

                        if (openIdKeys.Length < 2) throw new ArgumentNullException(" OpenIdKeys is NULL !");

                        //加载用户信息
                        OAuth.Tencent.Weibo.IWeibo tencentClient = tencentProvider.GetApi(
                            new AccessGrant(userBind.TokenAccess,
                                new[] {openIdKeys[0], openIdKeys[1], Globals.ClientIP}));
                        try
                        {
                            #region 发表文字内容

                            if (string.IsNullOrWhiteSpace(imageUrl))
                            {
                                tencentClient.UpdateStatusAsync(content + url).Wait();
                            }
                                #endregion
                                #region 发表文字+图片内容

                            else
                            {
                                //如果是云存储图片, 下载到本地后获取流
                                string weiBoImage = imageUrl;
                                if (imageUrl.Contains("http://"))
                                {
                                    System.Net.WebClient webclient = new System.Net.WebClient();
                                    string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                                    {
                                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                                    }
                                    weiBoImage = savePath + CreateIDCode() + ".jpg";
                                    webclient.DownloadFile(imageUrl, HttpContext.Current.Server.MapPath(weiBoImage));
                                }
                                //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
                                tencentClient.UploadStatusAsync(content + url,
                                    new System.IO.FileInfo(HttpContext.Current.Server.MapPath(weiBoImage))).Wait();
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            //写错误日志
                            Maticsoft.Model.SysManage.ErrorLog errorLog = new ErrorLog();
                            errorLog.Loginfo = ex.Message;
                            if (ex.InnerException != null &&
                                ex.InnerException is Maticsoft.OAuth.Rest.Client.HttpResponseException)
                            {
                                errorLog.Loginfo =
                                    System.Text.Encoding.Default.GetString(
                                        ((Maticsoft.OAuth.Rest.Client.HttpResponseException) (ex.InnerException))
                                            .Response.Body);
                            }
                            errorLog.OPTime = DateTime.Now;
                            errorLog.StackTrace = ex.StackTrace;
                            errorLog.Url = "";
                            Maticsoft.BLL.SysManage.ErrorLog.Add(errorLog);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 形成时间戳，组成图片名字
        /// </summary>
        /// <returns></returns>
        public string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额   
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }
        /// <summary>
        /// mediaUserID 为从灯鹭获取 的mediaUserID，多个ID以“,”连接
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mediaUserID"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public void SendWeiBo(int userId, string mediaUserIDs, string content, string url = null, string imageUrl = null, string videourl = null)
        {
            //如果是-1 表示是绑定全部的微博帐号
            if (mediaUserIDs == "-1")
            {
                List<Maticsoft.Model.Members.UserBind> list = GetModelList(" userid=" + userId);
                if (list != null && list.Count > 0)
                {
                    mediaUserIDs = String.Join(",", list.Select(c => c.MediaUserID));
                }
            }
            if (!String.IsNullOrWhiteSpace(imageUrl) && !imageUrl.StartsWith("http://"))
            {
                imageUrl = "http://" + Common.Globals.DomainFullName + imageUrl;
            }
            if (String.IsNullOrWhiteSpace(url))
            {
                url = "http://" + Common.Globals.DomainFullName;
            }
            string AppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DengluAPPID");
            string ApiKey = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DengluAPIKEY");
            Denglu denglu = new Denglu(AppId, ApiKey, "MD5");
            denglu.share(mediaUserIDs, content, url, userId.ToString(), imageUrl, videourl);
        }

        public Maticsoft.ViewModel.UserCenter.UserBindList GetListEx(int userId)
        {
            Maticsoft.ViewModel.UserCenter.UserBindList UserBindList = new ViewModel.UserCenter.UserBindList();
            List<Maticsoft.Model.Members.UserBind> list = GetModelList(" userid=" + userId);
            if (list != null && list.Count > 0)
            {
                UserBindList.Count = list.Count;
                foreach (var item in list)
                {
                    switch (item.MediaID)
                    {
                        case (int)MediaType.Sina:
                            UserBindList.Sina = item;
                            break;
                        case (int)MediaType.Tencent:
                            UserBindList.TenCent = item;
                            break;
                        case (int)MediaType.QZone:
                            UserBindList.QZone = item;
                            break;
                        default:
                            break;
                    }
                }
            }
            return UserBindList;
        }

        public List<Maticsoft.Model.Members.UserBind> GetWeiBoList(int userId)
        {
            List<Maticsoft.Model.Members.UserBind> list = GetModelList(" userid=" + userId);
            if (list != null && list.Count > 0)
            {
                foreach (var userBind in list)
                {
                    switch (userBind.MediaID)
                    {
                        case (int)MediaType.Sina:
                            userBind.WeiboName = "新浪微博";
                            userBind.WeiboLogo = "<img alt='QZone' src='/Admin/images/sina_16.png' />" + userBind.MediaNickName;
                            break;
                        case (int)MediaType.Tencent:
                            userBind.WeiboName = "腾讯微博";
                            break;
                        case (int)MediaType.QZone:
                            userBind.WeiboName = "QQ空间";
                            userBind.WeiboLogo = "<img alt='QZone' src='/Admin/images/qq_16.png' />" + userBind.MediaNickName;
                            break;
                        default:
                            break;

                    }
                }
            }
            return list;
        }

        public bool Exists(int userId, string MediaUserID)
        {
            return dal.Exists(userId, MediaUserID);
        }
        /// <summary>
        /// 增加一条数据（判重）
        /// </summary>
        public bool AddEx(Maticsoft.Model.Members.UserBind model)
        {
            if (Exists(model.UserId, model.MediaUserID))
            {
                //更新动作
                return dal.UpdateEx(model);
            }
            else
            {
                return dal.Add(model) > 0;
            }

        }
        #endregion  ExtensionMethod
    }
}

