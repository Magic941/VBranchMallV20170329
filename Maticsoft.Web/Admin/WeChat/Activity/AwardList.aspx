<%@ Page Title="微信活动奖品管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
  CodeBehind="AwardList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.AwardList" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "480", height: "380", overlayClose: false });
            $(".iframe1").colorbox({ iframe: true, width: "680", height: "500", overlayClose: false });
            var type = parseInt(("#hfAwardType").val());
            if (type == 0) {
                $(".CountList").show();
            }
           
        });
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信活动奖品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行查看微信活动奖品记录" />
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
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />:</asp:Label><asp:TextBox
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
                    <input id="hfAwardType" type="hidden" value='<%=AwardType %>' />
                   <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a class="various iframe1" href='AddAward.aspx?id=<%=ActivityId%>&type=<%=AwardType %>'>
                            <asp:Literal ID="Literal8" runat="server" Text="添加" /></a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="AwardId" ShowExportExcel="False" ShowExportWord="False" 
            OnRowCommand="gridView_RowCommand"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:TemplateField HeaderText="活动名称" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetActivity(Eval("ActivityId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="奖品类型" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("AwardName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="奖品名称" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("GiftName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖品数量" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Count")%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="奖品描述"   ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                      <%#Eval("AwardDesc")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定删除吗？"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField ControlStyle-Width="180" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="180" >
                    <ItemTemplate>
                            <a href='UpdateAward.aspx?id=<%#Eval("AwardId")%>' class="iframe1">编辑</a>&nbsp;&nbsp;
                            <a href='UpdateCount.aspx?id=<%#Eval("AwardId")%>' class="iframe CountList"   style=" display:none">增发数量</a>
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

