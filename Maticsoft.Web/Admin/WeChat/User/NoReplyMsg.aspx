<%@ Page Title="未有效回复消息管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="NoReplyMsg.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.User.NoReplyMsg" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[id$='ddUser']").select2(); });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".iframe").colorbox({ iframe: true, width: "780", height: "630", overlayclose: false });
            $(".imageUrl").each(function () {
                var src = $(this).attr("src");
                if (src != "") {
                    $(this).show();
                }
            });
            $("[id$='ddlAction']").hide();
            var event = $("[id$='ddlEvent']").val();
            if (event == "CLICK") {
                $("[id$='ddlAction']").show();
            }

            $("[id$='ddlEvent']").change(function () {
                var value = $(this).val();
                $("[id$='ddlAction']").hide();
                if (value == "CLICK") {
                    $("[id$='ddlAction']").show();
                }
            })
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

            $(".btnSendMsg").click(function () {
                var user = $(this).attr("user");
                var msgId=$(this).attr("msgId");
                if (user == "") {
                    return;
                }
                $.ajax({
                    url: ("UserList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "IsCanSend", Callback: "true", User: user },
                    async: false,
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            //$("#divAjaxMsg").dialog(dialogOpts);
                            window.location.href = "SendMsg.aspx?user=" + user + "&msgId=" + msgId;
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
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信未有效回复消息管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="lblTitle" runat="server" Text="微信未有效回复消息管理" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td width="280px">
                    消息时间：
                    <asp:TextBox ID="txtFrom" runat="server" Width="90"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtTo" runat="server" Width="90"></asp:TextBox>
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="0">未处理</asp:ListItem>
                        <asp:ListItem Value="1">已处理</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAction" runat="server">
                    </asp:DropDownList>
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
            BorderWidth="1px" ShowCheckAll="True" DataKeyNames="MsgId">
            <Columns>
                <asp:BoundField DataField="CreateTime" HeaderText="发送时间" ItemStyle-Width="120px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="消息内容" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("Description")%>
                        <a href="<%#Eval("PicUrl")%>" class="iframe">
                            <img src="<%#Eval("PicUrl")%>" alt="" width="80" height="80" class="imageUrl" style="display: none" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%# GetNickName(Eval("UserName"))%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="消息类型" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%# GetMsgType(Eval("MsgType"))%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center"  
                    ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%></ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center" ItemStyle-Width="240">
                    <ItemTemplate>
                           <a  href='javascript:;' class="btnSendMsg" user='<%#Eval("UserName")%>' msgId='<%#Eval("MsgId") %>'>发送消息</a>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <asp:Button ID="btnDeleteMsg" runat="server" Text="批量删除" OnClick="btnDeleteMsg_Click"
            CssClass="adminsubmit" OnClientClick="return confirm('你确认要删除么？')" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
