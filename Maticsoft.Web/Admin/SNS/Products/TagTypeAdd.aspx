<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="TagTypeAdd.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.TagType.Add" Title="增加标签类型" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%@ Register Src="~/Controls/SNSCategoryDropList.ascx" TagName="SNSCategoryDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="增加标签类型" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以增加标签类型" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td height="25" width="30%" align="right">
                                类型名称 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTypeName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                所属商品分类 ：
                            </td>
                            <td height="25">
                                <maticsoft:snscategorydroplist ID="dropCid" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                                状态：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Value="1" Text="可用" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="不可用"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right" valign="top">
                                备注：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
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
