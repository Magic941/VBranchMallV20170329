
<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
  CodeBehind="UpdateCount.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Activity.UpdateCount" %>
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
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行增发微信活动礼品操作" />
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
                              <asp:Literal ID="txtName" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="奖品名称" />：
                            </td>
                            <td height="25">
                             <asp:Literal ID="txtGiftName" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="奖品数量" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtCount" runat="server" Width="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="增发"  OnClick="btnSave_Click" class="adminsubmit_short">
                                </asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>

