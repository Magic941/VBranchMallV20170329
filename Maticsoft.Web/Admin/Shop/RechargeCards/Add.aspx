<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.RechargeCards.Add"
    Title="增加页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table  width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="生成充值卡" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="生成充值卡" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                充值卡号前缀 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPreName" runat="server" Width="200px"></asp:TextBox>
                                <span style="color: red; padding-left: 8px">最好为大写字母</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                充值卡号长度 ：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlLength" runat="server" Width="80">
                                    <asp:ListItem Value="10" Text="10位" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11位"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12位"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13位"></asp:ListItem>
                                    <%--<asp:ListItem Value="14" Text="14位"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15位"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <span style="color: red; padding-left: 8px">（不包括前缀）</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                密码长度 ：
                            </td>
                            <td height="25" >
                                <asp:DropDownList ID="ddlPwd" runat="server" Width="80">
                                    <asp:ListItem Value="6" Text="6位" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7位"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8位"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                金额 ：
                            </td>
                            <td height="25" >
                                <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>
                                <span style="color: red; padding-left: 8px">必须为数字</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                数量 ：
                            </td>
                            <td height="25" >
                                <asp:TextBox ID="txtCount" runat="server" Width="200px"></asp:TextBox>
                                <span style="color: red; padding-left: 8px">必须为数字</span>
                            </td>
                        </tr>
                        <%--<tr>
                    <td height="25" width="30%" align="right">
                        立即启用 ：
                    </td>
                <td height="25" width="*" align="left">
                    <asp:CheckBox ID="chkStatus" Text="是" runat="server" Checked="True" />
                </td>
                </tr>--%>
                        <tr>
                            <td class="td_class">
                                备注 ：
                            </td>
                            <td height="25" >
                                <asp:TextBox ID="txtRemark" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="生成" OnClick="btnSave_Click" class="adminsubmit_short">
                                </asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script src="/js/calendar1.js" type="text/javascript"></script>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
