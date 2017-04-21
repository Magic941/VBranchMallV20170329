using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;
using Maticsoft.WeChat.Model.Core;
using KeyValue = Maticsoft.WeChat.BLL.Core.KeyValue;
using MsgItem = Maticsoft.WeChat.BLL.Core.MsgItem;
using PostMsgItem = Maticsoft.WeChat.BLL.Core.PostMsgItem;

namespace Maticsoft.Web.Admin.WeChat.PostMsg
{
    public partial class PostMsgList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 644; } } //移动营销_智能客服规则关键字管理_列表页
        protected new int Act_AddData = 645;    //移动营销_智能客服规则关键字管理_添加数据
        protected new int Act_DelData = 646;   //移动营销_智能客服规则关键字管理_删除数据

        private Maticsoft.WeChat.BLL.Core.KeyValue valueBll = new KeyValue();
        private Maticsoft.WeChat.BLL.Core.PostMsg msgBll = new Maticsoft.WeChat.BLL.Core.PostMsg();
        private Maticsoft.WeChat.BLL.Core.PostMsgItem msgitemBll = new PostMsgItem();
        private Maticsoft.WeChat.BLL.Core.MsgItem itemBll = new Maticsoft.WeChat.BLL.Core.MsgItem();
        private Maticsoft.WeChat.BLL.Core.KeyRule ruleBll = new Maticsoft.WeChat.BLL.Core.KeyRule();
        protected StringBuilder AllMatchValue = new StringBuilder();
        protected StringBuilder NoMatchValue = new StringBuilder();
           protected StringBuilder PostMsg = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }

            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    spanAddValue.Visible = false;
                    pbtnAddMsg.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    hidDel.Value = "hidden";
                } 
               // Maticsoft.WeChat.Model.Core.KeyRule ruleModel = ruleBll.GetModel(RuleId);
               // this.txtRule.Text = ruleModel == null ? "" : ruleModel.Name;
                this.hfRuleId.Value = RuleId.ToString();
                ShowValue();
                ShowMsg();
            }
        }


        #region 编号

        /// <summary>
        /// 编号
        /// </summary>
        protected int RuleId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }
        #endregion
        /// <summary>
        /// 显示关键字
        /// </summary>
        public void ShowValue()
        {
            List<Maticsoft.WeChat.Model.Core.KeyValue> valueList = valueBll.GetModelList(" RuleId=" + RuleId);
            if (valueList != null && valueList.Count > 0)
            {
                foreach (var keyValue in valueList)
                {
                    //模糊匹配
                    if (keyValue.MatchType == 0)
                    {
                        NoMatchValue.AppendFormat("<span class='SKUValue'><span class='span1'  href='javascript:void(0)'  class='updatetype' valueId='{1}' title='点击设为全匹配'><a>{0}</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='{1}'>删除</a></span> </span>", keyValue.Value, keyValue.ValueId);
                    }
                    else
                    {
                        AllMatchValue.AppendFormat("<span class='SKUValue'><span class='span1'  href='javascript:void(0)'  class='updatetype' valueId='{1}' title='点击设为模糊匹配'><a >{0}</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='{1}'>删除</a></span> </span>", keyValue.Value, keyValue.ValueId);
                    }
                }
            }
        }


        /// <summary>
        /// 显示回复消息
        /// </summary>
        public void ShowMsg()
        {
            List<Maticsoft.WeChat.Model.Core.PostMsg> msgList = msgBll.GetModelList(" RuleId=" + RuleId);
            msgList = msgList.OrderByDescending(c => c.CreateTime).ToList();
            if (msgList != null && msgList.Count > 0)
            {
                foreach (var msg in msgList)
                {
                    PostMsg.AppendFormat(
                        "<li><div class='userPic'>▶</div><div class='content'><div class='msgInfo'> {0}</div><div class='times'> <a class='delMsg' href='javascript:;' msgId='{1}'>删除</a></div></div> </li>",
                        msg.Description, msg.PostMsgId);
                }
            }
        }

        
        #region Ajax 方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateType":
                    writeText = UpdateType();
                    break;
                case "AddValue":
                    writeText = AddValue();
                    break;
                case "DeleteValue":
                    writeText = DeleteValue();
                    break;
                case "AddMsg":
                    writeText = AddMsg();
                    break;
                case "DeleteMsg":
                    writeText = DeleteMsg();
                    break;
                default:
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }
        private string UpdateType()
        {
            JsonObject json = new JsonObject();
            int valueId = Common.Globals.SafeInt(this.Request.Form["ValueId"], 0);
            int type = Common.Globals.SafeInt(Request.Params["MatchType"], 0);
            if (valueId == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (valueBll.UpdateType(valueId, type))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }
        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <returns></returns>
        private string AddValue()
        {
            JsonObject json = new JsonObject();
            int ruleId = Common.Globals.SafeInt(this.Request.Form["RuleId"], 0);
            string value = Request.Form["Value"];
            json.Put("STATUS", "FAILED");
            if (ruleId == 0)
            {
                return json.ToString();
            }
            Maticsoft.WeChat.Model.Core.KeyValue valueModel = new Maticsoft.WeChat.Model.Core.KeyValue();
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            if (valueBll.Exists(value, openId))
            {
                json.Put("STATUS", "Exist");
                return json.ToString();
            }
            valueModel.Value = value;
            valueModel.MatchType = 1;
            valueModel.RuleId = ruleId;
            int valueId = valueBll.Add(valueModel);
            if (valueId > 0)
            {
                json.Put("STATUS", "Success");
                json.Put("DATA", valueId);
            }
            return json.ToString();
        }

        /// <summary>
        /// 添加回复
        /// </summary>
        /// <returns></returns>
        private string AddMsg()
        {
            JsonObject json = new JsonObject();
            int ruleId = Common.Globals.SafeInt(this.Request.Form["RuleId"], 0);
            string msg = Request.Form["Msg"];
            json.Put("STATUS", "FAILED");
            if (ruleId == 0)
            {
                return json.ToString();
            }
            Maticsoft.WeChat.Model.Core.PostMsg valueModel = new Maticsoft.WeChat.Model.Core.PostMsg();
            valueModel.Description = msg;
            valueModel.MsgType = "text";
            valueModel.RuleId = ruleId;
            valueModel.CreateTime = DateTime.Now;
            long msgId = msgBll.Add(valueModel);
            if (msgId > 0)
            {
                json.Put("STATUS", "Success");
                json.Put("DATA", msgId);
            }
            return json.ToString();
        }

        /// <summary>
        /// 删除关键字
        /// </summary>
        /// <returns></returns>
        private string DeleteValue()
        {
            JsonObject json = new JsonObject();
            int valueId = Common.Globals.SafeInt(this.Request.Form["ValueId"], 0);
            json.Put("STATUS", "FAILED");
            if (valueBll.Delete(valueId))
            {
                json.Put("STATUS", "Success");
            }
            return json.ToString();
        }
        /// <summary>
        /// 删除回复
        /// </summary>
        /// <returns></returns>
        private string DeleteMsg()
        {
            JsonObject json = new JsonObject();
            int msgId = Common.Globals.SafeInt(this.Request.Form["MsgId"], 0);
            json.Put("STATUS", "FAILED");
            if (msgBll.Delete(msgId))
            {
                json.Put("STATUS", "Success");
            }
            return json.ToString();
        }

        #endregion

    }
}