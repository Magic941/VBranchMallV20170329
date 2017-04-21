<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="AddAward.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.AddAward" %>

<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信活动礼品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行设置微信活动礼品操作" />
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
                                <asp:Literal ID="Literal5" runat="server" Text="奖品类型" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trCoupon">
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="优惠券活动" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlCoupon" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="trGiftName">
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="奖品名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtGiftName" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trCount">
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="奖品数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtCount" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="奖品描述" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAwardDesc" runat="server" Width="320px" TextMode="MultiLine"
                                    Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="添加" OnClick="btnSave_Click" class="adminsubmit_short">
                                </asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
