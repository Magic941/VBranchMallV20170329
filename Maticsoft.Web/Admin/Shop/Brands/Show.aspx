<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Brands.Show" Title="显示页" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Controls" Assembly="Maticsoft.Web" %>
<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="品牌详细信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以查看品牌详细信息" />
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
                                品牌编号 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblBrandId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                品牌名称 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblBrandName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                检索拼音 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblBrandSpell" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align:top;">
                               品牌图片 ：
                            </td>
                            <td height="25">
                                <asp:Image ID="imgLogo" runat="server"  Width="80" Height="47"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               品牌官方地址 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCompanyUrl" runat="server"  Width="90%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               显示顺序 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblDisplaySequence" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               关联商品 ：
                            </td>
                            <td height="25">
                                <Maticsoft:ProductTypesCheckBoxList ID="chkProductTpyes" runat="server" Enabled="false">
                                </Maticsoft:ProductTypesCheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align:top;">
                                页面标题 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblMeta_Keywords" runat="server" Width="90%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  style="height:6px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align:top;">
                                页面描述 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblMeta_Description" runat="server"  Width="90%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  style="height:6px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align:top;">
                                品牌描述 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblDescription" runat="server" Width="90%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  style="height:6px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_class">
                                Theme ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblTheme" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  style="height:15px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
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
