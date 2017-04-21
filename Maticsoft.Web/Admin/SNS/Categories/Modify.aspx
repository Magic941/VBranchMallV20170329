<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Categories.Modify"
    Title="修改页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/colorselect/iColorPicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_classshow">
                                分类名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="371px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                颜色值 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="textFontColor" CssClass="iColorPicker" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                是否为菜单 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="true" Text="是" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="false" Text="否"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                首页导航 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="rbIsMenuShow" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Value="true" Text="是" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="false" Text="否"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                状态 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="rbIsused" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="1" Text="有效" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="无效"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                描述 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" runat="server" Width="371px" TextMode="MultiLine"
                                    Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                           <tr>
                            <td class="td_class">
                                SEO 标题 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSeoTitle" runat="server" Width="371px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                SEO 关键字 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSeoKeywords" runat="server" Width="371px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                 SEO 描述 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSeoDescription" runat="server" Width="371px" TextMode="MultiLine"
                                    Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <script src="/js/calendar1.js" type="text/javascript"></script>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
