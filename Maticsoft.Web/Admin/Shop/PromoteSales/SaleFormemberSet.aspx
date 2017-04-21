<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master" CodeBehind="SaleFormemberSet.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.PromoteSales.SaleFormemberSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" type="text/css" rel="stylesheet" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <script src="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <link href="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfSelectedAccessories" runat="server" />
    <asp:HiddenField ID="hfRelatedProducts" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        豪礼大放送商品设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以设置&nbsp;豪礼大放送商品、开抢时间以及活动时间
                    </td>
                </tr>
            </table>
        </div>
       
        <div class="TabContent formitem">
            <div id="myTab1_Content5" tabindex="5" >
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="Td5" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="100%" height="649px" frameborder="0" src="/Admin/Shop/Products/SaleForMembersSet.aspx?type=5">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
