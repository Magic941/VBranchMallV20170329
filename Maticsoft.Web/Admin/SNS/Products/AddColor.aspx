<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddColor.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Products.AddColor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/MasterPage1.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/admin.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript">
        $(function () {
            if ($("#hidValue").val() != "") {
                $("input[value='" + $("#hidValue").val() + "']").attr("checked", "true");
            }
            $("input[name='color']").click(function () {
                $("#hidValue").val($(this).val());
            });
        })
    </script>
    <style type="text/css">
    .colorDiv{width: 15px; height: 15px; background-color: red; float: right;margin-right: 5px;};
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="颜色值选择" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="添加商品颜色" />
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="2" cellspacing="1" class="border" class="borderkuang"
                style="margin-top: 20px">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" width="100%" cellpadding="0" border="0" style="margin-top: 20px;margin-bottom: 10px">
                            <tr>
                                <td id="divColor" style="width: 350px; margin-top: 30px;" colspan="2">
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio2" style="float: right">红色</label>
                                        <input id="Radio2" type="radio" name="color" style="float: left" value="red" />
                                        <div style="background-color: red;" class="colorDiv">
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio3" style="float: right">蓝色</label>
                                        <input id="Radio3" type="radio" name="color" style="float: left" value="blue" />
                                        <div style="background-color: blue;" class="colorDiv">
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio4" style="float: right">黄色</label>
                                        <input id="Radio4" type="radio" name="color" style="float: left" value="yellow" />
                                        <div style="background-color: yellow;" class="colorDiv">
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio5" style="float: right">绿色</label>
                                        <input id="Radio5" type="radio" name="color" style="float: left" value="green" />
                                        <div style="background-color: green;" class="colorDiv">
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio6" style="float: right">棕色</label>
                                        <input id="Radio6" type="radio" name="color" style="float: left" value="brown" />
                                        <div style="background-color: brown;" class="colorDiv">
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio7" style="float: right">黑色</label>
                                        <input id="Radio7" type="radio" name="color" style="float: left" value="black" />
                                        <div style="background-color: black;" class="colorDiv">
                                        </div>
                                    </div>
                                    <div style="float: left; margin-right: 20px">
                                        <label for="Radio8" style="float: right">白色</label>
                                        <input id="Radio8" type="radio" name="color" style="float: left" value="white" />
                                        <div style="background-color: white;" class="colorDiv">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" height="30">
                                  </td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="保存" class="adminsubmit" OnClick="btnSave_Click"
                                        OnClientClick="javascript:parent.$.colorbox.close();" />
                                    <asp:HiddenField ID="hidValue" runat="server" />
                                    &nbsp;&nbsp;<label visible="false" id="lblTip" style="color: #38BE2D" runat="server">保存成功</label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>