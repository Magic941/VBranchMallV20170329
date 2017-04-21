<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Supplier.SupplierInfo.Show" Title="商家详细信息" %>

<%@ Register Src="/Admin/../Controls/Region.ascx" TagName="Region" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 150px;
            text-align: right;
            padding-bottom: 10px;
            padding-top: 10px;
            height: 25px;
        }
        .style2
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="商家信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以查看商家详细信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr style="display: none;">
                            <td class="td_class">
                                商家编号 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblSupplierId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                商家名称 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                商家介绍 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblIntroduction" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                注册资本 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblRegisteredCapital" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                账户余额 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                电话 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblTelPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                手机 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCellPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                联系邮箱 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblContactMail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                省/市 ：
                            </td>
                            <td height="25">
                                <uc1:Region ID="RegionID" runat="server" VisibleAll="true" VisibleAllText="--无数据--" AreaEnabled="false" CityEnabled="false"
                                    ProvinceEnabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                注册地 ：
                            </td>
                            <td height="25">
                                <uc1:Region ID="RegionEstablishedCity" runat="server" VisibleAll="true" VisibleAllText="--无数据--" AreaEnabled="false" CityEnabled="false"
                                    ProvinceEnabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                地址 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                备注 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                联系人 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblContact" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                用户名 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                成立时间 ：
                            </td>
                            <td class="style2">
                                <asp:Label ID="lblEstablishedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                标志 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblLOGO" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                传真 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblFax" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                邮编 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblPostCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                主页 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblHomePage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                法人 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblArtiPerson" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                商家等级 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblEnteRank" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                商家分类 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblEnteClassName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                公司性质：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCompanyType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                营业执照 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblBusinessLicense" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                税务登记 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblTaxNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                开户银行 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAccountBank" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                账号信息 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAccountInfo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr  style="display:none;">
                            <td class="td_class">
                                客服电话 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblServicePhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr  style="display:none;">
                            <td class="td_class">
                                QQ ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblQQ" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                MSN ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblMSN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                状态 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                代理商ID ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAgentID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                创建日期 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                创建用户 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                更新日期 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUpdatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                更新用户 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUpdatedUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" Text="返回" class="adminsubmit_short" OnClick="btnCancle_Click">
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
