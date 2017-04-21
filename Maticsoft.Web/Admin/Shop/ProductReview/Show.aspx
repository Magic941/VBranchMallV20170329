<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductReview.Show"
    Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         $(function () {
             if ($("[id$='hidImagesPath']").val()) {
                 var path = $("[id$='hidImagesPath']").val();
                 var fileNames = $("[id$='hidImagesNames']").val();
                 if (path && fileNames) {
                     var namesArray = fileNames.split('|');
                     for (var i = 0; i < namesArray.length; i++) {
                         $(".picture_wen").append(('<img src="{0}" width="300px" height="400px" /> &nbsp;&nbsp;').format(
                        path.format(namesArray[i])));
                     }

                 }
             }
         })

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="商品评论详细信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        查看商品评论详细信息
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr style="display:none;">
                            <td class="td_class">
                                评论ID ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblReviewId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                评论商品 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblProductId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">
                                评论人 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                评论内容 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblReviewText" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                评论分数 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblScore" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;" >
                            <td class="td_class">
                                评论人姓名 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_class">
                                评论人邮箱 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                评论时间 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="td_class">
                                ParentId ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblParentId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               状态 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">
                               用户晒单 ：
                            </td>
                            <td height="25">
                                <div class="picture_wen"></div>
                                <input type="hidden" id="hidImagesNames" runat="server"/>
                                <input type="hidden" id="hidImagesPath" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                                 <asp:Button ID="Button1" runat="server" Text="通过审核"
                                    class="adminsubmit_short" OnClick="btnPassApproved_Click"></asp:Button>
                                <asp:Button ID="Button2" runat="server" Text="通过未审核"
                                    class="adminsubmit_short" OnClick="btnNoPassApproved_Click"></asp:Button>
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