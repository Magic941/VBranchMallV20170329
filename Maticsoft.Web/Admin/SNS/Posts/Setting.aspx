<%@ Page Title="动态中心设置" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Setting.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.PostSetting.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            if ($("[id$='chk_OpenPost']").attr("checked") == "checked") {
                $(".setpost").show();
            }
            $("[id$='chk_OpenPost']").click(function () {
                if ($(this).attr("checked")) {
                    $(".setpost").show();
                } else {
                    $(".setpost").hide();
                }
            });

            if ($("[id$='cbx_Picture']").attr("checked") == "checked") {
                $(".setphoto").show();
            }
            $("[id$='cbx_Picture']").click(function () {
                if ($(this).attr("checked")) {
                    $(".setphoto").show();
                } else {
                    $(".setphoto").hide();
                }
            });

            if ($("[id$='cbx_Product']").attr("checked") == "checked") {
                $(".setproduct").show();
            }
            $("[id$='cbx_Product']").click(function () {
                if ($(this).attr("checked")) {
                    $(".setproduct").show();
                } else {
                    $(".setproduct").hide();
                }
            });
            
        })
    </script>
    <style type="text/css">
        .PostPerson
        {
            width: 100px;
        }
        .PostDate
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="个人中心分享设置 " />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="设置个人中心启用分享的各个功能模块。" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end-->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle" colspan="2">
                    <asp:CheckBox ID="chk_OpenPost" runat="server" Text="启用发表动态" />
                </td>
            </tr>
            <tr class="setpost" style="display: none">
                <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    动态分享：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:CheckBox ID="check_Narmal_word" runat="server" Checked="true" Enabled="false"
                        Text="发布文字" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_Narmal_Pricture" runat="server" Text="分享图片" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_Narmal_Video" runat="server" Text="分享视频" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_Narmal_Audio" runat="server" Text="分享音频" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chk_Blog" runat="server" Text="分享文章" />
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr class="setpost" style="display: none">
                <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    审核状态：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:CheckBox ID="chk_check_word" runat="server" Text="文字需审核" />
                    &nbsp;
                    <asp:CheckBox ID="chk_check_photo" runat="server" Text="图片需审核" />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chk_check_video" runat="server" Text="视频需审核 " />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chk_check_audio" runat="server" Text="音频需审核" />
                </td>
            </tr>
        </table>

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle" colspan="2">
                    <asp:CheckBox ID="cbx_Picture" runat="server" Text="启用照片分享" />
                </td>
            </tr>
             <tr class="setphoto" style="display: none">
            <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                审核状态：
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                <asp:CheckBox ID="cbx_check_picture" runat="server" Text="照片分享需审核" />
            </td>
            </tr>
        </table>
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle" colspan="2">
                     <asp:CheckBox ID="cbx_Product" runat="server" Text="启用商品分享" />
                </td>
            </tr>
            <tr class="setproduct" style="display: none">
                <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    商品分享：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                   <asp:CheckBox ID="chk_TaoProduct" runat="server" Text="淘宝商品"    />
                    &nbsp;&nbsp;&nbsp;
                     <asp:CheckBox ID="chk_CustomPro" runat="server" Text="自定义商品" />
                </td>
            </tr>
           <tr class="setproduct" style="display: none">
            <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
              审核状态：
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
              <asp:CheckBox ID="cbx_check_product" runat="server" Text="商品分享需审核 " />
            </td>
            </tr>
           
        </table>

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle" colspan="2">
                      <asp:CheckBox ID="chk_OpenComment" runat="server" Text="启用用户评论" />
                </td>
            </tr>
      
           
        </table>

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
                <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    动态列表显示：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:CheckBox ID="cbx_PostType_All" runat="server" Text="全部动态" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_PostType_User" runat="server" Text="我发表的" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_PostType_Fellow" runat="server" Text="我关注的" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_PostType_EachOther" runat="server" Text="互相关注" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbx_PostType_ReferMe" runat="server" Text="提到我的" />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
                <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    连续发帖冻结：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:TextBox ID="txtBanTopicTime" Style="text-align: right" CssClass="OnlyNum" Width="30px"
                        runat="server"></asp:TextBox>
                    分钟内发帖
                    <asp:TextBox ID="txtBanTopicCount" Width="30px" Style="text-align: right" CssClass="OnlyNum"
                        runat="server"></asp:TextBox>
                    个(包含审查词或替换词)自动冻结违规用户
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100" height="30">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" class="adminsubmit_short" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        $('.OnlyNum').OnlyNum();
    </script>
</asp:Content>
