<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master"
    CodeBehind="PostMsgList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.PostMsg.PostMsgList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //添加关键字
            $("#btnAddValue").die("click").live("click", function () {
                var value = $("#txtValue").val();
                var ruleId = $("[id$='hfRuleId']").val();
                if (value == "") {
                    ShowFailTip("请填写关键字！");
                    return;
                }
                $.ajax({
                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "AddValue", Callback: "true", Value: value, RuleId: ruleId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#AllMatchTags").append("<span class='SKUValue'><span class='span1' href='javascript:void(0)'   valueId='" + resultData.DATA +
                                "'><a >" + value + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='" + resultData.DATA +
                                "'>删除</a></span> </span>");
                            $("#txtValue").val("");

                            if ($('[id$="hidDel"]').val() == "hidden") {
                                $(".del").css('display', 'none');
                                $(".delMsg").css('display', 'none');
                            }
                        }
                        if (resultData.STATUS == "Exist") {
                            ShowFailTip("该关键字已存在！");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });

            if ($('[id$="hidDel"]').val() == "hidden") {
                $(".del").css('display', 'none');
                $(".delMsg").css('display', 'none');
            }

            // 删除关键字
            $(".del").die("click").live("click", function () {
                var valueId = $(this).attr("valueId");
                var self = $(this);
                $.ajax({
                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "DeleteValue", Callback: "true", ValueId: valueId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            self.parent().parent().hide();
                            ShowSuccessTip("删除关键字成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //移动到模糊匹配
            $("#AllMatchTags").find(".span1").die("click").live("click", function () {
                var self = $(this);
                var valueId = self.attr("valueId");
                var value = self.find("a").text();
                $.ajax({
                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "UpdateType", Callback: "true", ValueId: valueId, MatchType: 0 },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            self.parent().hide();
                            $("#NoMatchTags").append("<span class='SKUValue'><span class='span1' href='javascript:void(0)'  title='点击设为全匹配'  valueId='" + valueId +
                                "'><a >" + value + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='" + valueId +
                                "'>删除</a></span> </span>");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //移动到全匹配
            $("#NoMatchTags").find(".span1").die("click").live("click", function () {
                var self = $(this);
                var valueId = self.attr("valueId");
                var value = self.find("a").text();
                $.ajax({
                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "UpdateType", Callback: "true", ValueId: valueId, MatchType: 1 },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            self.parent().hide();
                            $("#AllMatchTags").append("<span class='SKUValue'><span class='span1' href='javascript:void(0)' title='点击设为模糊匹配'  valueId='" + valueId +
                                "'><a >" + value + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del' valueId='" + valueId +
                                "'>删除</a></span> </span>");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
            //添加回复
            $("#btnAddMsg").die("click").live("click", function () {
                var msg = $("#txtPostMsg").val();
                var ruleId = $("[id$='hfRuleId']").val();

                if (msg == "") {
                    ShowFailTip("请填写回复内容！");
                    return;
                }
                if (msg.length >= 1024) {
                    ShowFailTip("您输入的文字太长！");
                    return;
                }

                $.ajax({
                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "AddMsg", Callback: "true", Msg: msg, RuleId: ruleId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            $("#txtPostMsg").val("");
                            var length = $("#msgBox").find("ul>li").length;
                            if (length == 0) {
                                $("#msgBox").find("ul").append("<li><div class='userPic'>▶</div><div class='content'> <div class='msgInfo'>" + msg +
                                    "</div><div class='times'> <a class='delMsg' href='javascript:;' msgId='" + resultData.DATA + "'>删除</a></div></div> </li>");
                            } else {
                                $("#msgBox").find("ul>li").eq(0).before("<li><div class='userPic'>▶</div><div class='content'> <div class='msgInfo'>" + msg +
                                "</div><div class='times'> <a class='delMsg' href='javascript:;' msgId='" + resultData.DATA + "'>删除</a></div></div> </li>");
                            }
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });

            //删除回复
            $(".delMsg").die("click").live("click", function () {
                var msgId = $(this).attr("msgId");
                var self = $(this);
                $.ajax({
                    url: ("PostMsgList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "DeleteMsg", Callback: "true", MsgId: msgId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            self.parent().parent().parent().hide();
                            ShowSuccessTip("删除回复成功");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            });
        })
    </script>
    <style type="text/css">
        #msgBox
        {
            background: #fff;
            border-radius: 5px;
            padding-top: 10px;
        }
        #msgBox img
        {
            border-radius: 3px;
        }
        #msgBox .list ul
        {
            overflow: hidden;
            zoom: 1;
            width: 720px;
            border: 1px solid #ccc;
        }
        #msgBox .list ul li
        {
            float: left;
            clear: both;
            width: 100%;
            border-bottom: 1px dashed #d8d8d8;
            padding: 10px 0;
            background: #fff;
            overflow: hidden;
        }
        #msgBox .list ul li.hover
        {
            background: #f5f5f5;
        }
        #msgBox .list .content
        {
            float: left;
            width: 680px;
            font-size: 14px;
            font-family: arial;
            word-wrap: break-word;
        }
        #msgBox .list .msgInfo
        {
            display: inline;
            word-wrap: break-word;
        }
        #msgBox .list .times
        {
            color: #889db6;
            font: 12px/18px arial;
            margin-top: 5px;
            overflow: hidden;
            zoom: 1;
        }
        #msgBox .list .times span
        {
            float: left;
        }
        #msgBox .list .times a
        {
            float: right;
            color: #889db6;
        }
        .content
        {
            background: #fff;
        }
        #txtPostMsg
        {
            width: 668px;
            resize: none;
            height: 65px;
            overflow: auto;
        }
        #userName, #conBox
        {
            color: #777;
            border: 1px solid #d0d0d0;
            border-radius: 6px;
            padding: 3px 5px;
            font: 14px/1.5 arial;
        }
        #msgBox .list .userPic {
            float:left;
            height:50px;display:inline;margin-left:10px;border-radius:3px;
            padding-top: 4px;
        }
        #userName.active, #conBox.active
        {
            border: 1px solid #7abb2c;
        }
        #userName
        {
            height: 20px;
        }
        #btnAddMsg
        {
            border: 0;
            height: 30px;
            cursor: pointer;
            margin-left: 10px;
        }
        #btnAddMsg.hover
        {
            background-position: 0 -30px;
        }
        .tr {
            overflow:hidden;zoom:1;
            width: 668px
        }
    .tr p{float:right;line-height:30px;}
    .tr *{float:left;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfRuleId" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidDel" runat="server"></asp:HiddenField>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="规则的关键字和回复内容管理" />
                    </td>
                    <td rowspan="2" width="120">
                        <a href="RuleList.aspx" style="color: grey">返回规则列表</a>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        设置用户微信咨询的关键字和系统的智能回复答案内容
                    </td>
                </tr>
            </table>
        </div>
        <!--Add end -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td>
                                <span class="newstitle">关键字列表</span>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="50%">
                                <input id="txtValue" type="text" style="width: 360px" />
                            <span id="spanAddValue" runat="server">   <input id="btnAddValue" type="button" value="添加关键字" class="adminsubmit" /></span>  
                            </td>
                        </tr>
                        <tr>
                             <td style="vertical-align: top">
                                <table width="100%" cellpadding="2" cellspacing="1" class="border borderkuang" style="margin-top: 20px">
                                    <tr>
                                        <td class="tdbg">
                                            <table cellspacing="0" width="100%" cellpadding="0" border="0">
                                                <tr>
                                                    <td style="width: 80px;vertical-align: top" >
                                                        全匹配：
                                                    </td>
                                                    <td id="AllMatchTags">
                                                        <%=AllMatchValue.ToString()%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table width="100%" cellpadding="2" cellspacing="1" class="border borderkuang" style="margin-top: 20px">
                                    <tr>
                                        <td class="tdbg">
                                            <table cellspacing="0" width="100%" cellpadding="0" border="0">
                                                <tr>
                                                    <td style="width: 80px;vertical-align: top">
                                                        模糊匹配：
                                                    </td>
                                                    <td id="NoMatchTags">
                                                        <%=NoMatchValue.ToString()%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td>
                                <span class="newstitle">消息回复列表</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <textarea id="txtPostMsg" class="f-text"></textarea></div>
                                <div class="tr">
                                    <p id="pbtnAddMsg" runat="server">
                                        <input id="btnAddMsg" type="button" value="添加回复" title="添加回复" class="adminsubmit"   />
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td id="msgBox">
                                <div class="list">
                                    <ul>
                                        <%=PostMsg.ToString()%>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
