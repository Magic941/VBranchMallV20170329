<%@ Page Title="<%$ Resources:SysManage,ptFeedbackList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.User.UserList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
        <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
      <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
      <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function changUserName(id) {
            $("#img_" + id).hide();
            $("#txtUserName_" + id).show().focus();
            $("#editsave_" + id).show();
            $("#p_" + id).hide();
        }
        function saveChange(id) {
            var userName = $("#txtUserName_" + id).val();
            if (!userName) {
                ShowFailTip('请填写用户昵称！');
                return;
            }
            $.ajax({
                url: ("UserList.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "UpdateUserName", Callback: "true", ID: id, UpdateValue: userName },
                async: false,
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $("#p_" + id).text(userName);
                        $("#img_" + id).show();
                        $("#editsave_" + id).hide();
                        $("#txtUserName_" + id).hide();
                        $("#p_" + id).show();
                    }
                    else {
                        ShowFailTip("系统忙请稍后再试！");
                    }
                }
            });
        }

        $(document).ready(function () {
            $(".item-title-area").hover(function () {
                $(this).addClass('high-light');
            }, function () {
                $(this).removeClass("high-light");
            });
            $(".txtpname").blur(function () {
                textBulr(this);
            });
            $(".editsave").mouseenter(function () {
                var id = $(this).attr('i');
                $("#txtUserName_" + id).unbind('blur');

            }).mouseleave(function () {
                var id = $(this).attr('i');
                $("#txtUserName_" + id).bind('blur', function () {
                    $("#txtUserName_" + id).hide();
                    $("#img_" + id).show();
                    $("#editsave_" + id).hide();
                    $("#p_" + id).show();
                });
            });
        });

        function textBulr(thisenent) {
            $(thisenent).hide();
            var id = $(thisenent).attr('i');
            $("#img_" + id).show();
            $("#editsave_" + id).hide();
            $("#p_" + id).show();
        }
       
    </script>
    <style type="text/css">
        .autobrake
        {
            word-wrap: break-word;
            width: 260px;
            float: left;
        }
        .high-light
        {
            cursor: pointer;
        }
        .txtpname
        {
            width: 260px;
            display: none;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "600", height: "290", overlayClose: false });


            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtFrom").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtTo").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtTo").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtFrom").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtTo").val($(this).val());
                }
            });
            var dialogOpts = {
                title: "发送消息",
                width: 400,
                modal: true,
                resizable: false,
                buttons: {
                    "确定": function () {
                        SendMsg();
                    },
                    "取消": function () {
                        //  $(this).dialog("close"); //关闭层
                        $("#divAjaxMsg").dialog("close");
                    }
                }
            };
            $(".btnSendMsg").click(function () {
                var user = $(this).attr("user");
                if (user == "") {

                    return;
                }
                $("#hfUser").val(user);
                $.ajax({
                    url: ("UserList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "IsCanSend", Callback: "true", User: user },
                    async: false,
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            //$("#divAjaxMsg").dialog(dialogOpts);
                            window.location.href = "SendMsg.aspx?user=" + user;
                            //dialog层中项的设置
                        } else {
                            ShowFailTip("抱歉，只能向48小时内主动发送消息的用户进行发送，请先选择[最近48小时消息用户]选项。");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            });
        });

        function SendMsg() {
            var content = $('#txtContent').val();
            var user = $('#hfUser').val();


            if (content == '') {
                ShowFailTip("请输入消息内容！");
                return ;
            }

            $.ajax({
                url: ("UserList.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "SendMsg", Callback: "true", Content: content, User: user },
                async: false,
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $('#txtContent').val("");
                        $('#hfUser').val("");
                        ShowSuccessTip("发送消息成功！");
                        $("#divAjaxMsg").dialog("close");
                        $(".ui-dialog").empty();
                    }
                    else if (resultData.STATUS == "NoToken") {
                        $('#txtContent').val("");
                        $('#hfUser').val("");
                        ShowFailTip(" 授权失败，请检查您的微信设置！");
                        $("#divAjaxMsg").dialog("close");
                        $(".ui-dialog").empty();
                    }
                    else {
                        ShowFailTip("系统忙请稍后再试！");
                    }
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信用户管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="微信用户管理" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"  >
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td width="280px">
                    关注时间：
                    <asp:TextBox ID="txtFrom"   runat="server" Width="90" ></asp:TextBox>
                    --
                     <asp:TextBox ID="txtTo"   runat="server" Width="90" ></asp:TextBox>
                </td>
               
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    状态：
                    <asp:DropDownList ID="ddStatus" runat="server">
                        <asp:ListItem Value=""> 全部</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True"> 关注</asp:ListItem>
                        <asp:ListItem Value="0"> 取消关注</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="chkHours" runat="server" />最近48小时消息用户
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID"  >
            <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center"   ItemStyle-Width="40px">
                    <ItemTemplate >
                              <asp:Image ID="Image1" runat="server"  ImageUrl='<%#String.IsNullOrWhiteSpace(Eval("Headimgurl").ToString())?"/Images/weixin.jpg":Eval("Headimgurl") %>' Width="40" Height="40"/></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="center"   ItemStyle-Width="300px">
                    <ItemTemplate >
                          <div title="编辑用户名" class="item-title-area">
                            <p id="p_<%# Eval("ID")%>" class="autobrake">
                              <%#(String.IsNullOrWhiteSpace(Eval("NickName").ToString())? Eval("UserName") : Eval("NickName"))%></p>
                              <input id="txtUserName_<%# Eval("ID")%>" rows="2" 
                              class="txtpname" i="<%# Eval("ID")%>" value='<%#(String.IsNullOrWhiteSpace(Eval("NickName").ToString())? Eval("UserName") : Eval("NickName"))%>'/>
                        
                            &nbsp;<img alt="编辑用户名" id="img_<%# Eval("ID")%>" title="编辑用户名" src="/admin/Images/up_xiaobi.png"
                                onclick='changUserName(<%# Eval("ID")%>);' />
                            <br />
                        </div>
                        <a id="editsave_<%# Eval("ID")%>" href="javascript:void(0)" onclick="saveChange(<%# Eval("ID")%>);"
                            class="editsave" i="<%# Eval("ID")%>" style="   margin-left: 239px;   display: none; border: none; width: 41px;height: 20px;">保存</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分组" ItemStyle-HorizontalAlign="center"  ItemStyle-Width="120" >
                    <ItemTemplate>
                        <%#GetGroupName(Eval("GroupId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="绑定网站用户" ItemStyle-HorizontalAlign="center"  ItemStyle-Width="120">
                    <ItemTemplate>
                        <%#GetUserName(Eval("UserId"))%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center"  ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#GetUserStatus(Eval("Status"))%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所在地区" ItemStyle-HorizontalAlign="center" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#Eval("Country")%>  <%#Eval("Province")%>   <%#Eval("City")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关注时间" ItemStyle-HorizontalAlign="center" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#Eval("CreateTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
            
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center" ItemStyle-Width="240">
                    <ItemTemplate>
                      <a  class="iframe" href='BindUser.aspx?id=  <%#Eval("ID")%>'>绑定用户</a>
                      &nbsp;&nbsp;
                      <span style="display:none"><a   href='/Admin/JLT/Reports/List.aspx?id=<%#Eval("UserId")%>' >查看日报</a></span>
                         &nbsp;&nbsp;
                         <a  href='RequestMsg.aspx?user=<%#Eval("UserName")%>' >查看消息</a>
                           &nbsp;&nbsp;
                           <a  href='javascript:;' class="btnSendMsg" user='<%#Eval("UserName")%>'>发送消息</a>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>

        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                       <asp:Button ID="btnDelete" runat="server" Text="批量删除" class="adminsubmit"  OnClick="btnDelete_Click"  />
                    <asp:DropDownList ID="ddGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddGroup_Changed">
                      
                    </asp:DropDownList>
                     <asp:Button ID="btnGetUserInfo" runat="server" Text="批量获取" class="adminsubmit"   OnClick="btnGetUserInfo_Click"  />
                </td>
            </tr>
        </table>
        <div id="divAjaxMsg" style="display: none">
    <div style='margin-left: 10px; margin-top: 10px; font-weight: bold;'>
    <input id="hfUser" type="hidden" />
      <textarea id="txtContent" cols="20" rows="8" style=" width:320px;" ></textarea>
    </div>
    
</div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
