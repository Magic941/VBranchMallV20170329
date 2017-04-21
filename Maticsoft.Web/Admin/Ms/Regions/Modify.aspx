<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Ms.Regions.Modify" Title="编辑" %>

<%@ Register src="/Controls/Region.ascx" tagname="Region" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       编辑地区信息
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        编辑地区信息
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
                                选择地区 ：
                            </td>
                            <td height="25">
                            <uc1:Region ID="Regions1" runat="server" />
                            (添加地区说明 :添加省份：不需要选择任何地区；添加市区：选择省份即可；添加县城：选择省份和市区)
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                地区名称 ：
                            </td>
                            <td height="25">
                             
                            <asp:TextBox ID="txtRegionName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_class">
                                地区简称 ：
                            </td>
                            <td height="25">
                            <asp:TextBox ID="txtSpellShort" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                显示顺序 ：
                            </td>
                            <td height="25">
                            <asp:TextBox ID="txtDisplaySequence" runat="server" Width="200px" Text="1"></asp:TextBox>
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
