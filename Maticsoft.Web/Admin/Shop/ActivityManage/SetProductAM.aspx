
<%@ Page Language="C#" Title="设置商品" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true"CodeBehind="SetProductAM.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.SetProductAM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        设置参与活动的商铺
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、删除&nbsp;活动商铺
                    </td>
                </tr>
            </table>
        </div>
        <div class="TabContent formitem">
            
        <%--通过选择单个商品添加--%>
            <div id="myTab1_Content0" tabindex="0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr class="RelatedProductTR">
                        <td id="contetRelatedProduct" colspan="2">
                            <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1"
                                class="border">
                                <tr>
                                    <td colspan="4" style="width: 100%; text-align: center;">
                                        <iframe width="95%" height="649px" frameborder="0" src="/Admin/Shop/ActivityManage/SuppliersAM.aspx?AMId=<%=AMId%>">
                                        </iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

        <%--通过选择单个商家添加--%>
        </div>
    </div>
</asp:Content>
