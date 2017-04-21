<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="StarTypeModify.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.StarType.Modify"
    Title="修改页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="达人类型数据" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="达人类型数据" />
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
                                类型名称：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTypeName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                    <td class="td_class">
                                状态 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">可用</asp:ListItem>
                                    <asp:ListItem Value="0">不可用</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                         <td class="td_class">
                                规则描述 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtCheckRule" runat="server" Width="200px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                         <td class="td_class">
                                备注 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" OnClientClick="return PageIsValid();" class="adminsubmit_short">
                                </asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short" ValidationGroup="Group1">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
