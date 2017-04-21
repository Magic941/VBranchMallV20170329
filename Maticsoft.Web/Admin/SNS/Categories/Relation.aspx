<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Relation.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Categories.Relation" %>

<%@ Register Src="~/Controls/SNSCategoryDropList.ascx" TagName="SNSCategoryDropList"
    TagPrefix="Maticsoft" %>
<%@ Register Src="~/Controls/TaoBaoCategoryDropList.ascx" TagName="TaoBaoCategoryDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("[id$='btnSave']").click(function () {
                var res = true;
                var fromcate = $("#ctl00_ContentPlaceHolder1_CategoriesDropList2_hfSelectedNode").val();
                var tocate = $("#ctl00_ContentPlaceHolder1_CategoriesDropList1_hfSelectedNode").val();
                if (!fromcate) {
                    ShowFailTip("请选择需要的商品分类!");
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
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="社区分类对应" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="将社区某一分类对应到淘宝商品分类 " />
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
                                社区商品分类 ：
                            </td>
                            <td height="25">
                                <Maticsoft:SNSCategoryDropList ID="SNSCate" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                淘宝商品分类 ：
                            </td>
                            <td height="25">
                                <Maticsoft:TaoBaoCategoryDropList ID="TaoBaoCate" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                是否对应子分类 ：
                            </td>
                            <td height="25">
                                       <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                                <asp:ListItem Value="true" Text="是"  ></asp:ListItem>
                                                <asp:ListItem Value="false" Text="否" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
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
