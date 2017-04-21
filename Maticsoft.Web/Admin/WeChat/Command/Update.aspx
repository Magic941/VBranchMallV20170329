<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Update.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Command.Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信指令管理" />
                    </td>
                </tr>

                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加微信指令操作" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="指令名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="对应操作" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="dropAction" runat="server" Width="200px" OnSelectedIndexChanged="dropAction_IndexChange" AutoPostBack="true">
                                </asp:DropDownList>
                                  <asp:DropDownList ID="ddTarget" runat="server" Visible="False">
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="显示顺序" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSequence"  runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="解析类型" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddParseType" runat="server" Width="200px"  >
                                    <asp:ListItem Value="0" Text="长度"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="特殊字符"></asp:ListItem>
                                </asp:DropDownList>
                               <asp:TextBox ID="txtParseType" runat="server"   ></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="是否启用" />：
                            </td>
                            <td height="25">
                                   <asp:CheckBox ID="chkStatus" Text="是" runat="server"   Checked="True"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="导航菜单描述" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDesc" runat="server" Width="250px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
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

