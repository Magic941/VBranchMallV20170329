/**
* Photo.cs
*
* 功 能： [N/A]
* 类 名： Photo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/24 16:23:03  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.ViewModel.SNS
{
    public class GroupIndex
    {
        public Model.Members.UsersExpModel UserModel { get; set; }
        public List<Model.SNS.Groups> TopGroupList { get; set; }
        public List<Model.SNS.Groups> ProGroupList { get; set; }
        public List<Model.SNS.Groups> HotGroupList { get; set; }
        public List<Model.SNS.Groups> MyGroupList { get; set; }
        public List<Model.SNS.GroupTopics> NewGroupTopicList { get; set; }
    }


    public class GroupInfo
    {
        public PagedList<Maticsoft.Model.SNS.GroupTopics> TopicList { set; get; }
        public Maticsoft.Model.SNS.Groups Group { set; get; }
        public List<Model.SNS.GroupUsers> AdminUserList { set; get; }
        public PagedList<Model.SNS.GroupUsers> UserList { set; get; }
        public List<Model.SNS.GroupUsers> NewUserList { set; get; }
        public List<Model.SNS.GroupTopics> NewTopicList { set; get; }

    }

    public class RegisterGroup
    {
        /// <summary>
        /// 小组名称
        /// </summary>
        [Required(ErrorMessage = "小组名称不能为空")]
        [Display(Name = "小组名称")]
        [Remote("IsExistGroupName", "Group", ErrorMessage = "小组名称已经被Ta人抢注, 换个试试")]
        [DataType(DataType.Text)]
        public string GroupName
        {
            set;
            get;
        }

        /// <summary>
        /// 小组的简介
        /// </summary>
        [Display(Name = "小组简介")]
        [DataType(DataType.MultilineText)]
        public string GroupDescription
        {
            set;
            get;
        }

        /// <summary>
        /// 小组的logo
        /// </summary>
        [Required(ErrorMessage = "请上传小组Logo")]
        [Display(Name = "小组Logo")]
        [DataType(DataType.Text)]
        public string GroupLogo
        {
            set;
            get;
        }

        ///// <summary>
        ///// 小组的背景图片
        ///// </summary>
        ///// <remarks>预留字段</remarks>
        //[Display(Name = "小组背景图片")]
        //[DataType(DataType.Text)]
        //public string GroupBackground
        //{
        //    set;
        //    get;
        //}

        ///// <summary>
        ///// 申请创建小组的理由
        ///// </summary>
        ////[Required(ErrorMessage = "申请创建小组的理由不能为空")]
        //[Display(Name = "申请创建小组的理由")]
        //[DataType(DataType.MultilineText)]
        //public string ApplyGroupReason
        //{
        //    set;
        //    get;
        //}

        ///// <summary>
        ///// 邀请链接才可加入
        ///// </summary>
        ///// <remarks>预留字段</remarks>
        //public int Privacy
        //{
        //    set;
        //    get;
        //}

        /// <summary>
        /// 标签
        /// </summary>
        [Required(ErrorMessage = "请选择小组标签")]
        [Display(Name = "小组标签")]
        public string Tags
        {
            set;
            get;
        }
        public string TagList { get; set; }
    }


    public class UpdateGroup
    {
        public int GroupId { get; set; }

        /// <summary>
        /// 小组名称
        /// </summary>
        [Required(ErrorMessage = "小组名称不能为空")]
        [Display(Name = "小组名称")]
        //[Remote("IsExistGroupName4Ignore", "Group", ErrorMessage = "小组名称已经被Ta人抢注, 换个试试",
        //    AdditionalFields = "GropuId")]
        [DataType(DataType.Text)]
        public string GroupName
        {
            set;
            get;
        }

        /// <summary>
        /// 小组的简介
        /// </summary>
        [Display(Name = "小组简介")]
        [DataType(DataType.MultilineText)]
        public string GroupDescription
        {
            set;
            get;
        }

        /// <summary>
        /// 小组的logo
        /// </summary>
        [Required(ErrorMessage = "请上传小组Logo")]
        [Display(Name = "小组Logo")]
        [DataType(DataType.Text)]
        public string GroupLogo
        {
            set;
            get;
        }

        ///// <summary>
        ///// 小组的背景图片
        ///// </summary>
        ///// <remarks>预留字段</remarks>
        //[Display(Name = "小组背景图片")]
        //[DataType(DataType.Text)]
        //public string GroupBackground
        //{
        //    set;
        //    get;
        //}

        ///// <summary>
        ///// 申请创建小组的理由
        ///// </summary>
        ////[Required(ErrorMessage = "申请创建小组的理由不能为空")]
        //[Display(Name = "申请创建小组的理由")]
        //[DataType(DataType.MultilineText)]
        //public string ApplyGroupReason
        //{
        //    set;
        //    get;
        //}

        ///// <summary>
        ///// 邀请链接才可加入
        ///// </summary>
        ///// <remarks>预留字段</remarks>
        //public int Privacy
        //{
        //    set;
        //    get;
        //}

        /// <summary>
        /// 标签
        /// </summary>
        [Required(ErrorMessage = "请选择小组标签")]
        [Display(Name = "小组标签")]
        public string Tags
        {
            set;
            get;
        }
        public string  TagList { get; set; }
    }
}
