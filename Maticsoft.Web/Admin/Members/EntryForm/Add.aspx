<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Ms.EntryForm.Add" Title="<%$Resources:MsEntryForm,ptMsEntryFormAdd%>" %>

<%@ Register Src="~/Controls/Region.ascx" TagName="Region" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:MsEntryForm,ptMsEntryFormAdd %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:MsEntryForm,lblMsEntryFormAdd %>" />
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
                               <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:MsEntryForm,lblUsername%>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtUserName" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,Age %>"/>：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:DropDownList ID="dropAge" runat="server" Width="100px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,fieldEmail %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,fieldTelphone %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTelPhone" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,lblCellphone %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtPhone" runat="server" Width="200px" MaxLength="11"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                QQ ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtQQ" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                MSN ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtMSN" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:MsEntryForm,lblHouseAddress %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtHouseAddress" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:MsEntryForm,lblCompanyAddress %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtCompanyAddress" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:MsEnterprise,lblRegionID %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <uc3:Region ID="dropProvince" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,fieldSex %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:DropDownList ID="dropSex" runat="server">
                                    <asp:ListItem Selected="True" Value="" Text="<%$Resources:Site, PleaseSelect%>"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="<%$Resources:Site, SexMale%>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site, SexWoman%>"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                               <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,Content %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtDescription" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" valign="top">
                               <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Site,remark %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                               <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Site,State %>" />：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:DropDownList ID="dropState" runat="server">
                                    <asp:ListItem Selected="True" Value="0" Text="<%$Resources:Site,Untreated%>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,Processed%>"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short" />
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
