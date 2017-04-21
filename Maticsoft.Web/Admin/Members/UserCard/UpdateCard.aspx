
<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
 CodeBehind="UpdateCard.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UserCard.UpdateCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        修改会员卡信息
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以修改会员卡相关信息
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                卡号 ：
                            </td>
                            <td height="25">
                                <asp:Literal ID="lblCardCode" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                用户名 ：
                            </td>
                            <td height="25">
                               <asp:Literal ID="lblUserName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" >
                                卡类型 ：
                            </td>
                            <td height="25">
                                 <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" align="left">
                                           <asp:ListItem Value="0" Text="金额卡" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="计次卡"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="打折卡"></asp:ListItem>
                                        </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                卡值 ：
                            </td>
                            <td height="25">
                               <asp:TextBox ID="txtValue" runat="server" Width="320px"  ></asp:TextBox>  
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class">
                                卡状态 ：
                            </td>
                            <td height="25">
                              <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" align="left">
                                           <asp:ListItem Value="0" Text="冻结" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="已激活"></asp:ListItem>
                                        </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                创建时间 ：
                            </td>
                            <td height="25">
                                <asp:Literal ID="lblCreatedDate" runat="server"></asp:Literal>
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
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
