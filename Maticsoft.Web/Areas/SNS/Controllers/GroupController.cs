using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Maticsoft.BLL.SNS;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.SNS;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.SNS;
using Maticsoft.Components.Filters;
using Webdiyer.WebControls.Mvc;
using EnumHelper = Maticsoft.Model.Ms.EnumHelper;
using System.Linq;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class GroupController : SNSControllerBase
    {
        private const int TopGroupCount = 6;
        private const int HotGroupCount = 6;
        private BLL.SNS.Groups bllGroups = new BLL.SNS.Groups();
        private BLL.SNS.GroupTags bllGroupTags = new BLL.SNS.GroupTags();

        private BLL.SNS.GroupTopics bllTopic = new GroupTopics();
        private BLL.SNS.GroupUsers bllGroupUser = new GroupUsers();
        private BLL.SNS.GroupTopicReply bllReply = new GroupTopicReply();

        #region 小组首页
        public ActionResult Index()
        {
            Maticsoft.ViewModel.SNS.GroupIndex groupIndex = new Maticsoft.ViewModel.SNS.GroupIndex();

            //优秀小组
            groupIndex.TopGroupList = bllGroups.GetTopList(TopGroupCount, "IsRecommand=1", "TopicCount desc");
            //精选小组
            groupIndex.ProGroupList = bllGroups.GetTopList(TopGroupCount, "IsRecommand=2", "TopicCount desc");
            //活跃小组
            groupIndex.HotGroupList = bllGroups.GetTopList(HotGroupCount, "", "TopicCount desc");

            if (CurrentUser != null)
            {
                //我的小组
                groupIndex.MyGroupList = bllGroups.GetUserJoinGroup(CurrentUser.UserID, 10);
            }
            //小组总数
            ViewBag.GroupCount = bllGroups.GetRecordCount("").ToString().ToCharArray();

            //最新话题
            groupIndex.NewGroupTopicList = bllTopic.GetList4Model(10, "Status=1", "TopicID desc");

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Group", ApplicationKeyType.SNS);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(groupIndex);
        }
        #endregion

        #region 创建小组
        [TokenAuthorize]
        public ActionResult Create()
        {
            RegisterGroup registerGroup = new RegisterGroup();

            List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");

            registerGroup.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 
            ViewBag.Title = "申请注册小组";
            return View(registerGroup);
        }

        [TokenAuthorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(RegisterGroup model)
        {
            ViewBag.Title = "申请注册小组";
            if (!ModelState.IsValid)
            {
                List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");
                
                model.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 
                return View(model);
            }

            Model.SNS.Groups groups = new Model.SNS.Groups();
            groups.GroupName = model.GroupName;
            groups.GroupDescription = model.GroupDescription;
            groups.GroupUserCount = 1;
            groups.CreatedUserId = CurrentUser.UserID;
            groups.CreatedNickName = CurrentUser.NickName;
            groups.CreatedDate = DateTime.Now;
            groups.Tags = model.Tags;

            if (!string.IsNullOrWhiteSpace(model.GroupLogo))
            {
                string tmpOriginalImg = string.Format(model.GroupLogo, "");

                string thumbPath = HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroupThumb);
                string originalPath = HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroup);

                try
                {
                    if (BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
                    {
                        groups.GroupLogoThumb = model.GroupLogo;
                        groups.GroupLogo = model.GroupLogo;
                    }
                    else
                    {
                        if (!Directory.Exists(thumbPath)) Directory.CreateDirectory(thumbPath);
                        if (!Directory.Exists(originalPath)) Directory.CreateDirectory(originalPath);

                        List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                     Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS);

                        if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                        {
                            string tmpThumbImg = "";
                            foreach (var thumbSize in ThumbSizeList)
                            {
                                tmpThumbImg = String.Format(model.GroupLogo, thumbSize.ThumName);
                                if (System.IO.File.Exists(Server.MapPath(tmpThumbImg)))
                                {
                                    System.IO.FileInfo tmpThumbFile = new FileInfo(HttpContext.Server.MapPath(tmpThumbImg));
                                    tmpThumbFile.MoveTo(thumbPath + tmpThumbFile.Name);
                                }
                            }
                        }

                        //Move Original File
                        System.IO.FileInfo tmpOriginalFile = new FileInfo(
                            HttpContext.Server.MapPath(tmpOriginalImg));
                        tmpOriginalFile.MoveTo(originalPath + tmpOriginalFile.Name);

                        //Save DB
                        groups.GroupLogoThumb = SNSAreaRegistration.PathUploadImgGroupThumb + "{0}" + tmpOriginalFile.Name;
                        groups.GroupLogo = SNSAreaRegistration.PathUploadImgGroup + tmpOriginalFile.Name;
                    }
                 
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Message", "您上传的文件保存失败, 请重新上传!");
                    List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");
                    model.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 
                    return View(model);
                }
            }

            groups.Status = 1;  //状态(0未审核，1.已经审核)
            groups.IsRecommand = 0;  //是否推荐到首页

            groups.GroupID = bllGroups.Add(groups);
            if (groups.GroupID > 0)
            {
                Model.SNS.GroupUsers groupUsers = new Model.SNS.GroupUsers();
                groupUsers.GroupID = groups.GroupID;
                groupUsers.JoinTime = DateTime.Now;
                groupUsers.UserID = groups.CreatedUserId;
                groupUsers.NickName = groups.CreatedNickName;
                groupUsers.Role = 2;    //超级管理员(组长)
                groupUsers.Status = 1;
                bllGroupUser.Add(groupUsers);
                return RedirectToAction("GroupInfo", new { GroupId = groups.GroupID });
            }
            return View(model);
        }

        #endregion

        #region 编辑小组
        [TokenAuthorize]
        public ActionResult Update(int groupId)
        {
            ViewBag.Title = "编辑小组信息";
            UpdateGroup updateGroup = new UpdateGroup();

            Model.SNS.Groups groups = bllGroups.GetModel(groupId);
            if (groups.CreatedUserId != CurrentUser.UserID)
            {
                //权限不足
                return RedirectToAction("GroupInfo", new { GroupId = groups.GroupID });
            }

            updateGroup.GroupId = groups.GroupID;
            updateGroup.GroupLogo = groups.GroupLogo;
            updateGroup.GroupName = groups.GroupName;
            updateGroup.Tags = groups.Tags;
            updateGroup.GroupDescription = groups.GroupDescription;
            List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");
            updateGroup.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 

            return View(updateGroup);
        }

        [TokenAuthorize]
        [HttpPost]
        public ActionResult Update(UpdateGroup model)
        {
            ViewBag.Title = "编辑小组信息";
            if (!ModelState.IsValid)
            {
                List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");

                model.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 
                return View(model);
            }

            Model.SNS.Groups groups = bllGroups.GetModel(model.GroupId);

            if (model.GroupName != groups.GroupName && bllGroups.Exists4Ignore(model.GroupName, model.GroupId))
            {
                ModelState.AddModelError("Message", "小组名称已经被Ta人抢注, 换个试试");
                List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");

                model.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 
                return View(model);
            }

            groups.GroupName = model.GroupName;
            groups.GroupDescription = model.GroupDescription;
            groups.Tags = string.Join(",", model.Tags);



            if (!string.IsNullOrWhiteSpace(model.GroupLogo) && model.GroupLogo != groups.GroupLogo)
            {
                string tmpOriginalImg = string.Format(model.GroupLogo, "");

                string thumbPath = HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroupThumb);
                string originalPath = HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroup);

                try
                {
                    //删除历史图片
                    Maticsoft.Web.Components.FileHelper.DeleteFile(EnumHelper.AreaType.SNS, groups.GroupLogoThumb);
                    Maticsoft.Web.Components.FileHelper.DeleteFile(EnumHelper.AreaType.SNS, groups.GroupLogo);
                    if (BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
                    {
                        groups.GroupLogoThumb = model.GroupLogo;
                        groups.GroupLogo = model.GroupLogo;
                    }
                    else
                    {
                        if (!Directory.Exists(thumbPath)) Directory.CreateDirectory(thumbPath);
                        if (!Directory.Exists(originalPath)) Directory.CreateDirectory(originalPath);

                        List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                     Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS);

                        if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                        {
                            string tmpThumbImg = "";
                            foreach (var thumbSize in ThumbSizeList)
                            {
                                tmpThumbImg = String.Format(model.GroupLogo, thumbSize.ThumName);
                                if (System.IO.File.Exists(Server.MapPath(tmpThumbImg)))
                                {
                                    System.IO.FileInfo tmpThumbFile = new FileInfo(HttpContext.Server.MapPath(tmpThumbImg));
                                    tmpThumbFile.MoveTo(thumbPath + tmpThumbFile.Name);
                                }
                            }
                        }

                        //Move Original File
                        System.IO.FileInfo tmpOriginalFile = new FileInfo(
                            HttpContext.Server.MapPath(tmpOriginalImg));
                        tmpOriginalFile.MoveTo(originalPath + tmpOriginalFile.Name);

                        //Save DB
                        groups.GroupLogoThumb = SNSAreaRegistration.PathUploadImgGroupThumb + "{0}" + tmpOriginalFile.Name;
                        groups.GroupLogo = SNSAreaRegistration.PathUploadImgGroup + tmpOriginalFile.Name;

                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Message", "您上传的文件保存失败, 请重新上传!");
                    List<Model.SNS.GroupTags> list = bllGroupTags.GetModelList("Status = 1");

                    model.TagList = list == null ? "" : String.Join(",", list.Select(c => c.TagName)); 
                    return View(model);
                }
            }



        
            if (bllGroups.Update(groups))
            {
                return RedirectToAction("GroupInfo", new { GroupId = groups.GroupID });
            }
            return View(model);
        }
        #endregion

        #region 小组首页
        public ActionResult GroupInfo(int GroupId, int? page, string type, string q)
        {
            Maticsoft.ViewModel.SNS.GroupInfo groupInfo = new ViewModel.SNS.GroupInfo();
            groupInfo.Group = bllGroups.GetModel(GroupId);

            ViewBag.GetType = type;
            ViewBag.IsCreator = (CurrentUser != null && groupInfo.Group.CreatedUserId == CurrentUser.UserID);
            //小组帖子数
            ViewBag.toalcount = groupInfo.Group.TopicCount;

            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = 0;
            switch (type)
            {
                case "Search":
                    //小组帖子搜索
                    //推荐帖子
                    q = Common.InjectionFilter.Filter(q);
                    toalcount = bllTopic.GetCountByKeyWord(q, GroupId);
                    ViewBag.q = q;
                    ViewBag.toalcount = toalcount;
                    groupInfo.TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(
                        bllTopic.SearchTopicByKeyWord(startIndex, endIndex, q, GroupId, "")
                        , page ?? 1, pagesize, toalcount);
                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/TopicList.cshtml", groupInfo.TopicList);
                    break;
                case "User":
                    //小组管理员
                    groupInfo.AdminUserList = bllGroupUser.GetAdminUserList(GroupId);
                    if (groupInfo.AdminUserList.Count > 0 && groupInfo.AdminUserList[0].Role == 2)
                    {
                        BLL.Members.UsersExp bllUser = new BLL.Members.UsersExp();
                        groupInfo.AdminUserList[0].User = bllUser.GetUsersExpModelByCache(groupInfo.AdminUserList[0].UserID);
                    }
                    //小组成员总数 非管理员用户
                    toalcount = bllGroupUser.GetRecordCount("GroupId=" + GroupId + " AND Role = 0");
                    //小组成员分页加载  非管理员用户
                    groupInfo.UserList = new PagedList<Model.SNS.GroupUsers>(
                        bllGroupUser.GetUserList(GroupId, startIndex, endIndex)
                        , page ?? 1, pagesize, toalcount);
                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/UserList.cshtml", groupInfo);
                    break;
                case "Recommand":
                    //推荐帖子
                    toalcount = bllTopic.GetRecommandCount(GroupId);
                    groupInfo.TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(
                        bllTopic.GetTopicListPageByGroup(GroupId, startIndex, endIndex, true)
                        , page ?? 1, pagesize, toalcount);
                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/TopicList.cshtml", groupInfo.TopicList);
                    break;
                default:
                    //帖子
                    toalcount = bllTopic.GetRecordCount("  Status=1 and GroupID=" + GroupId);
                    groupInfo.TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(
                        bllTopic.GetTopicListPageByGroup(GroupId, startIndex, endIndex, false)
                        , page ?? 1, pagesize, toalcount);

                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/TopicList.cshtml", groupInfo.TopicList);
                    break;
            }
            groupInfo.NewTopicList = bllTopic.GetNewTopListByGroup(GroupId, 10);
            groupInfo.NewUserList = bllGroupUser.GetNewUserListByGroup(GroupId, 9);
            ViewBag.IsJoin = (currentUser != null && bllGroupUser.Exists(GroupId, currentUser.UserID)) ? true : false;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("GroupList", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, groupInfo.Group.GroupName },        //小组名称
                new[] { PageSetting.RKEY_CTAG, groupInfo.Group.Tags },              //小组标签
                new[] { PageSetting.RKEY_CDES, groupInfo.Group.GroupDescription }); //小组描述
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(CurrentThemeViewPath + "/Group/GroupInfo.cshtml", groupInfo);
        }
        #endregion

        #region 小组帖子
        [TokenAuthorize]
        public ActionResult NewTopic(int GroupId)
        {
            ViewBag.Title = "发表主题";
            Maticsoft.Model.SNS.Groups model = new Model.SNS.Groups();
            model = bllGroups.GetModel(GroupId);
            return View(model);
        }
        
        public ActionResult TopicReply(int Id, int? page)
        {
            if (!bllTopic.Exists(Id) || !bllTopic.UpdatePVCount(Id))
            {
                return RedirectToAction("Index", "Group");
            }


            Maticsoft.ViewModel.SNS.TopicReply Model = new TopicReply();
            Model.Topic = bllTopic.GetModel(Id);

            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = bllReply.GetRecordCount(" Status=1 and TopicID =" + Id);
            List<Maticsoft.Model.SNS.GroupTopicReply> list = bllReply.GetTopicReplyByTopic(Id, startIndex, endIndex);
            if (list != null && list.Count > 0)
            {
                Model.TopicsReply = new PagedList<Maticsoft.Model.SNS.GroupTopicReply>(list, page ?? 1, pagesize, toalcount);
            }

            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/Group/TopicReplyList.cshtml", Model);
            Model.UserJoinGroups = bllGroups.GetUserJoinGroup(Model.Topic != null ? Model.Topic.CreatedUserID : 0, 9);
            Model.HotTopic = bllTopic.GetHotListByGroup(Model.Topic.GroupID, 9);
            Model.Group = bllGroups.GetModel(Model.Topic.GroupID);
            Model.UserPostTopics = bllTopic.GetTopicByUserId(Model.Topic.CreatedUserID, 9);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("GroupDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, Model.Group.GroupName },    //小组名称
                new[] { PageSetting.RKEY_CTNAME, Model.Topic.Title },       //帖子标题
                new[] { PageSetting.RKEY_CTAG, Model.Topic.Tags },          //帖子标签
                new[] { PageSetting.RKEY_CDES, Model.Topic.Description });  //帖子内容
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            
            return View(CurrentThemeViewPath + "/Group/TopicReply.cshtml", Model);
        }
        #endregion

        #region Ajax验证
        /// <summary>
        /// 验证小组名称是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistGroupName(string groupName)
        {
            bool valid = !(bllGroups.Exists(groupName));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 验证小组名称是否已存在
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsExistGroupName4Ignore(string groupName, int groupId)
        {
            bool valid = !(bllGroups.Exists4Ignore(groupName, groupId));
            return Json(valid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult AjaxUserRoleUpdate(FormCollection fm)
        {
            int GroupId = Common.Globals.SafeInt(fm["GroupID"], 0);
            int UserId = Common.Globals.SafeInt(fm["UserId"], 0);
            int Role = Common.Globals.SafeInt(fm["Role"], 0);
            if (!bllGroupUser.UpdateRole(GroupId, UserId, Role))
            {
                return Content("Fail");
            }
            return Content("Success");
        }

        #region 最新小组
        public PartialViewResult NewGroup(int Top = -1, string ViewName = "_NewGroup")
        {
          List<Maticsoft.Model.SNS.Groups> groupList= bllGroups.GetTopList(Top, "", "CreatedDate desc");
            return PartialView(ViewName, groupList);
        }
        #endregion

        #region 最新话题
        public PartialViewResult NewGroupTopic(int Top = -1, string ViewName = "_NewGroupTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> newGroupTopic = bllTopic.GetList4Model(Top, "Status=1", "TopicID desc");
            return PartialView(ViewName, newGroupTopic);
        }
        #endregion

        #region 最热话题
        public PartialViewResult HotGroupTopic(int Top = -1, string ViewName = "_HotGroupTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> GroupTopic = bllTopic.GetHotListByGroup(-1, Top);
            return PartialView(ViewName, GroupTopic);
        }
        #endregion

        #region 管理员推荐
        public PartialViewResult AdminRecTopic(int Top = -1, string ViewName = "_AdminRecTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> GroupTopic = bllTopic.GetList4Model(Top, "Status=1 and IsAdminRecommend=1", "TopicID desc");
            return PartialView(ViewName, GroupTopic);
        }
        #endregion


        #region Ajax  辅助方法

        #endregion
    }
}
