<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
CodeBehind="AddMenu.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Menu.AddMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>

<script type="text/javascript">
    $(function () {
        $("[id$='ddCategory']").hide();
        $("[id$='ddCMSClass']").hide();
        $(".select2-container").hide();
        $("[id$='ddMenuAction']").change(function () {
            var value = $(this).val();
            $("[id$='ddCategory']").hide();
            $("[id$='ddCMSClass']").hide();
            $("[id$='txtRemark']").hide();
            $("#ddCMSArticle").hide();
            //获取商品分类
            if (value == -4) {
                $("[id$='ddCategory']").show();
                return;
            }
            if (value == 1) {
                $("[id$='ddCMSClass']").show();
                return;
            }
            if (value == -9) {
                $("[id$='ddCMSClass']").show();
                $("[id$='ddCMSClass']").change(function () {
                    var classId = $(this).val();
                    $("#ddCMSArticle").empty();
                    $("[id$='hfArticleId']").val("");
                    if (classId == "0") {
                        return;
                    }
                    var self = $(this);
                    $.ajax({
                        url: ("AddMenu.aspx?timestamp={0}").format(new Date().getTime()),
                        type: 'POST', dataType: 'json',
                        data: { Action: "GetArticles", Callback: "true", ClassId: classId },
                        success: function (resultData) {
                            if (resultData.Data.length > 0) {
                                var arry_obj = $.parseJSON(resultData.Data)
                                if (arry_obj.length > 0) {
                                    $("[id$='hfArticleId']").val(arry_obj[0].articleId);
                                }
                                $.each(arry_obj, function (i, item) {
                                    $("#ddCMSArticle").append("  <option value='" + item.articleId + "'>" + item.title + "</option>");
                                });
                                $("#ddCMSArticle").select2();
                                $(".select2-container").css("vertical-align", "middle").css("width", "120px")
                            }
                            if (resultData.STATUS == "FAILED") {
                                ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试");
                            }
                        }
                    });
                })
                return;
            }
            if (value == 0) {
                $("[id$='txtRemark']").show();
                return;
            }
        })
        $("#ddCMSArticle").change(function () {
            $("[id$='hfArticleId']").val($(this).val());
        })
    })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信菜单管理" /> 
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加微信菜单操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="上级菜单" />：
                            </td>
                            <td height="25">
                                  <asp:DropDownList ID="ddlParent" runat="server">
                                  </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="菜单名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="显示顺序" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSequence"  runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="菜单类型" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddMenuAction" runat="server" Width="120px"  >
                                </asp:DropDownList>
                                 <asp:DropDownList ID="ddCategory" runat="server" Width="120px"  >
                                </asp:DropDownList>
                                  <asp:DropDownList ID="ddCMSClass" runat="server" Width="120px"  >
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfArticleId" runat="server" />
                                <select id="ddCMSArticle"  style=" display:none">
                              
                                </select>
                                   <asp:TextBox ID="txtRemark" runat="server"  Width="240px"   ></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="是否启用" />：
                            </td>
                            <td height="25">
                                   <asp:CheckBox ID="chkStatus" Text="是" runat="server"   Checked="True"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
