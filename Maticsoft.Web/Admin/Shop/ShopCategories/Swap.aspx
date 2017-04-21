<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Swap.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ShopCategories.Swap" Title="增加页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%@ Register Src="/Admin/../Controls/CategoriesDropList.ascx" TagName="CategoriesDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='btnSave']").click(function () {
                var res = true;
                var fromcate = $("#ctl00_ContentPlaceHolder1_CategoriesDropList2_hfSelectedNode").val();
                var tocate = $("#ctl00_ContentPlaceHolder1_CategoriesDropList1_hfSelectedNode").val();
                if (!fromcate) {
                    ShowFailTip("请选择需要替换的商品分类!");
                    return false;
                }
                if (!tocate) {
                    ShowFailTip("请选择目的商品分类!");
                    return false;
                }
                if (fromcate == tocate) {
                    ShowFailTip("同一商品分类不允许转移商品!");
                    return false;
                }
                $.ajax({
                    url: "/Shopmanage.aspx",
                    type: 'post',
                    dataType: 'json',
                    timeout: 1000,
                    data: {
                        action: "IsExistedProduct",
                        CategoryId: $("#ctl00_ContentPlaceHolder1_CategoriesDropList2_hfSelectedNode").val()
                    },
                    async: false,
                    success: function (data) {
                        if (data.STATUS == "SUCCESS") {
                            res = true;
                        } else {
                            res = false;
                        }
                    }
                });
                if (!res) {
                    ShowFailTip("此分类下没有可以替换的商品");
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="商品批量替换" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="将某一分类的商品批量转移到另一个商品分类中 " />
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
                                需要替换的商品分类 ：
                            </td>
                            <td height="25">
                                <Maticsoft:CategoriesDropList ID="CategoriesDropList2" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                替换至 ：
                            </td>
                            <td height="25">
                                <Maticsoft:CategoriesDropList ID="CategoriesDropList1" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click" Visible="false"></asp:Button>
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