
<%@ Page   Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="PushMsg.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.User.PushMsg" %>

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
                    url: ("PushMsg.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "IsCanSend", Callback: "true", User: user },
                    async: false,
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                           // $("#divAjaxMsg").dialog(dialogOpts);
                            //dialog层中项的设置
                            window.location.href = "SendMsg.aspx?user=" + user;
                        } else {
                            ShowFailTip("抱歉，只能向24小时内主动发送消息的用户进行发送，请先选择[最近24小时消息用户]选项。");
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
                return;
            }
            $.ajax({
                url: ("PushMsg.aspx?timestamp={0}").format(new Date().getTime()),
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
                        <asp:Literal ID="Literal1" runat="server" Text="微信用户消息推送" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="向微信用户进行消息推送，实现微信营销。" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a href="SendMsg.aspx">
                            <asp:Literal ID="Literal5" runat="server" Text="群发消息" /></a>
                    </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center"   ItemStyle-Width="40px">
                    <ItemTemplate >
                              <asp:Image ID="Image1" runat="server"  ImageUrl='<%#Eval("Headimgurl")%>' Width="40" Height="40"/></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="center"   ItemStyle-Width="300px">
                    <ItemTemplate >
                              <%#(String.IsNullOrWhiteSpace(Eval("NickName").ToString())? Eval("UserName") : Eval("NickName"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分组" ItemStyle-HorizontalAlign="center"  ItemStyle-Width="120" >
                    <ItemTemplate>
                        <%#GetGroupName(Eval("GroupId"))%>
                    </ItemTemplate>
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
