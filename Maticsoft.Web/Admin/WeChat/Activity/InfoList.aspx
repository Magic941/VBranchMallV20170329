
<%@ Page Title="微信活动管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="InfoList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.InfoList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "680", height: "480", overlayClose: false });
            $(".delCou").css("width", "80px");
            $(".StartActivity").each(function () {
                var status = parseInt($(this).attr("status"));
                switch (status) {
                    case 0:
                        $(this).text("启用活动");
                        break;
                    case 1:
                        $(this).text("活动已开启");
                        break;
                    case 2:
                        $(this).text("活动已关闭");
                        break;
                    default:
                        $(this).text("启用活动");
                        break;
                }
            })
            $(".StartActivity").click(function () {
                var status = parseInt($(this).attr("status"));
                var activityId = parseInt($(this).attr("activityId"));
                var self = $(this);
                if (status == 1) {
                    ShowFailTip("该活动已经启用，请不要重复启用！")
                    return;
                }
                if (status == 2) {
                    ShowFailTip("该活动已经关闭！")
                    return;
                }
                $.ajax({
                    url: ("InfoList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "StartActivity", Callback: "true", ActivityId: activityId },
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            self.attr("status", "1");
                            ShowSuccessTip("启用活动成功");
                            self.text("活动已开启");
                        }
                        if (resultData.STATUS == "NoAward") {
                            ShowFailTip("亲，该活动没有设置奖品，请先设置奖品数据");
                        }
                        if (resultData.STATUS == "FAILED") {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                        }
                    }
                });
            })
        });
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信活动记录" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行查看微信活动记录" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:Label ID="Label1" runat="server">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />&nbsp;&nbsp;</asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox><asp:Button ID="Button1"
                                runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click"
                                class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                   <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a class="various" href='AddInfo.aspx?type=<%=Type %>'>
                            <asp:Literal ID="Literal8" runat="server" Text="添加" /></a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ActivityId" ShowExportExcel="False" ShowExportWord="False" 
            OnRowCommand="gridView_RowCommand"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="活动名称" SortExpression="Name" ItemStyle-HorizontalAlign="Left" />
              <asp:BoundField DataField="OpenId" HeaderText="公众号"   ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120"  Visible="false"/>
                <asp:TemplateField HeaderText="前缀" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <%#Eval("PreName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="获奖概率" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Probability", "{0:N2}")%>%
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="每人每天参与次数" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("EachCount")%>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="活动每天参与总数" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("DayTotal")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="每人总共参与次数" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UserTotal")%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="类型" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <%#GeTypeName(Eval("Type"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="奖项类型" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        <%#GeAwardType(Eval("AwardType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建者" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center"  Visible="False">
                    <ItemTemplate>
                        <%#GetUserName(Eval("CreatedUserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="使用时间" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("StartDate","{0:yyyy-MM-dd}")%>  至  <%#Eval("EndDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="创建时间" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <%#Eval("CreatedDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="状态" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <%#GeStatusName(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50"  Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定删除吗？"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                           
                    </ItemTemplate>
                </asp:TemplateField>

                     <asp:TemplateField ControlStyle-Width="80" HeaderText="启用活动" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80" >
                    <ItemTemplate>
                    <a class="StartActivity" status='<%#Eval("Status")%>' activityId='<%#Eval("ActivityId")%>' >启用活动</a>
                            <%--  <asp:LinkButton ID="LinkButton2" runat="server"     CommandName="StartActivity" CommandArgument='<%#Eval("ActivityId")+","+Eval("Status") %>'
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定启用该活动吗？启用之前请确保已经设置奖品"
                            Text="启用活动"  Visible='true'>
                             </asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>

                        <asp:TemplateField ControlStyle-Width="50" HeaderText="关闭活动" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50"  Visible="false">
                    <ItemTemplate>
                               <asp:LinkButton ID="LinkButton3" runat="server"     CommandName="CloseActivity" CommandArgument='<%#Eval("ActivityId")%>'
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定关闭该活动吗？"
                            Text="关闭活动" >
                             </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField ControlStyle-Width="210" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="210" >
                    <ItemTemplate>
                            <a href='UpdateInfo.aspx?id=<%#Eval("ActivityId")%>'>编辑</a>&nbsp;&nbsp;
                            <a href='AwardList.aspx?id=<%#Eval("ActivityId")%>&type=<%#Eval("AwardType")%>'>设置奖品</a>&nbsp;&nbsp;
                             <a href='CodeList.aspx?id=<%#Eval("ActivityId")%>'>奖品明细</a>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
