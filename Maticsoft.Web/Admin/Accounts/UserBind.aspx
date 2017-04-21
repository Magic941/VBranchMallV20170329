<%@ Page Title="<%$ Resources:Site, ptUserInfo%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UserBind.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.UserBind" %>

<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnGosina").click(function () {
                location.href = '/social/sina';
            });
            $("#btnGoqq").click(function () {
                location.href = '/social/qq';
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
                        <asp:Literal ID="Literal2" runat="server" Text="微博帐号绑定" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="txtTitle" runat="server" Text="您可以添加绑定新浪微博和腾讯空间，将内容分享到众多社交媒体" />
                    </td>
                </tr>
            </table>
        </div>

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <a href="javascript:;" id="btnGoqq">
                        <img alt="" src="/Admin/images/QQ.png" /><span style="vertical-align:top;line-height: 3 ; padding-left: 8px" >添加绑定</span>
                    </a>
                    <br/>
                    <a href="javascript:;" id="btnGosina">
                        <img alt="" src="/Admin/images/sina.png" /><span style="vertical-align:top;line-height: 3 ; padding-left: 8px">添加绑定</span>
                    </a>
                </td>
            </tr>
        </table>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="False" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            OnRowDeleting="gridView_RowDeleting" PageSize="10" ShowExportExcel="false" ShowExportWord="False"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="BindId">
            <Columns>
                <asp:TemplateField HeaderText="微博平台" ItemStyle-HorizontalAlign="center" ItemStyle-Width="50">
                    <ItemTemplate>
                        <%#GetImage(Eval("MediaID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("MediaNickName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120">
                    <ItemTemplate>
                        <a href="javascript:;" class="txtStatus" valueid='<%# Eval("BindId")%>' status='<%# Eval("Status")%>'>
                            <span>
                                <%#Convert.ToInt32(Eval("Status")) == 1 ? "可用" : "不可用"%></span> </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确定解决绑定吗？"
                            Text="解除绑定"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
               <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
    </div>
</asp:Content>
