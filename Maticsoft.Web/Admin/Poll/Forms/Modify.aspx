<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Forms.Modify" Title="<%$Resources:Poll,ptFormsAdd%>" %>

<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        修改问卷
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        修改问卷信息
                    </td>
                </tr>
            </table>
        </div>
        <table cellspacing="0" cellpadding="5" width="100%" border="0" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Poll,lblFormsName%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Poll,lblFormsExplain%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:CheckBox ID="chkIsActive" runat="server" Text="是否有效" />
                            </td>
                        </tr
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                    <asp:Button ID="btnAdd" class="adminsubmit_short" runat="server" Text="保存" OnClick="btnAdd_Click">
                                    </asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" onclick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
