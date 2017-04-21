
<%@ Page Language="C#" Title="修改活动规则" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true" CodeBehind="EditGifts.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.EditGifts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <script src="../../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        b {
            color:red;
        }
        .td_class
        {
            width: 80px;
            border-right: 1px solid #DBE2E7;
            border-left: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #fff;
            padding-bottom: 4px;
            padding-top: 4px;
        }
        .td_content
        {
            border-right: 1px solid #DBE2E7;
            border-left: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #fff;
        }
        a.pffule_button
        {
            width: 81px;
            background: url(/admin/images/addpfru.png) no-repeat;
            cursor: pointer;
            height: 25px;
            float: left;
            padding-top: 5px;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            var itemUnitvalue = $("#ctl00_ContentPlaceHolder1_AMUnit").find("input[checked='checked']").val();
            
            if (itemUnitvalue == 0) {
                $(".AMDRuleUnit").text("元");
            }
            else {
                $(".AMDRuleUnit").text("件");
            }

            //初始化规则项
            var ruleValues = $("#ctl00_ContentPlaceHolder1_hfItems").val();
            var ruleitems = ruleValues.split(",");
            $.each(ruleitems, function (i, item) {
                $("#txtRuleItem").append($("#txtTemplate").html().format(item.split("|")[0], item.split("|")[1]));
            });

            $("#btnAddItem").click(function () {
                $("#txtRuleItem").append($("#txtTemplate").html().format("", ""));
            });
            $("#ctl00_ContentPlaceHolder1_AMUnit").find('input').click(function () {
                var value = $(this).val();
                if (value == 0) {
                    $(".AMDRuleUnit").text("元,赠送");
                }
                if (value == 1) {
                    $(".AMDRuleUnit").text("件,赠送");
                }
            });
        });
        function btnRemove(_self) {
            $(_self).parent().parent().remove();
        }

        function SubForm() {
            var name = $("#ctl00_ContentPlaceHolder1_txtAMName").val();
            if (name == "") {
                ShowFailTip("请填写活动规则名称");
                return false;
            }
            var ruleValue = "";
            $("#txtRuleItem").find(".RuleItems").each(function () {
                var AMDUnitValue = $(this).find(".AMDUnitValue").val();
                var AMDRateValue = $(this).find(".AMDRateValue").val();
                if (AMDUnitValue == "" || AMDRateValue == "") {
                    return;
                }
                if (ruleValue == "") {
                    ruleValue = AMDUnitValue + "|" + AMDRateValue;
                } else {
                    ruleValue = ruleValue + "," + AMDUnitValue + "|" + AMDRateValue;
                }
            });
            if (ruleValue == "") {
                ShowFailTip("请设置优惠规则项");
                return false;
            }
            $("#ctl00_ContentPlaceHolder1_hfItems").val(ruleValue);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfSuccess" runat="server" />
    <asp:HiddenField ID="hfItems" runat="server" />
    <div class="newslistabout">
    
        <div class="TabContent">
            <%--基本信息--%>
            <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                cellspacing="1" class="border">
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                            <tr>
                                <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                    <span style="font-size: 16px; padding-left: 20px">满赠规则设置</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal2" runat="server" Text=" 活动名称" />：
                                <td height="35" class="td_content">
                                    <asp:TextBox ID="txtAMName" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                  <asp:Literal ID="Literal4" runat="server" Text=" 活动标签" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:TextBox ID="txtAMLable" runat="server" CssClass="marginstyle"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  class="td_class" style="background-color: #E2E8EB">活动时间：</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAMStartdate" onclick="WdatePicker()"></asp:TextBox>&nbsp;至&nbsp;
                                    <asp:TextBox runat="server" ID="txtAMEnddate" onclick="WdatePicker()"></asp:TextBox>
                                </td>
                            </tr>
                            
                           <%-- <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal5" runat="server" Text=" 是否包邮" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMFreeShipment" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="是" ></asp:ListItem>
                                        <asp:ListItem Value="1" Text="否" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal21" runat="server" Text=" 应用方式" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMApplyStyles" runat="server" RepeatDirection="Horizontal" align="left">
                                        <%--<asp:ListItem Value="0" Text="单个商品" Selected="True"></asp:ListItem>--%>
                                        <asp:listItem Value="0" Text="商家" Selected="True"></asp:listItem>
                                        <asp:ListItem Value="1" Text="全场"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="单品"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr style=" display:none">
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal1" runat="server" Text=" 是否启用" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMStatus" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="1" Text="是" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                            <tr>
                                <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                    <span style="font-size: 16px; padding-left: 20px">设置优惠规则</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 80px">
                                </td>
                                <td>
                                    <a href="javascript:void(0)" class="pffule_button" id="btnAddItem"><span style="padding-left: 24px; color:red;">
                                        添加规则 </span></a><span style="float: left; padding-left: 8px">点击“新增规则”为您的商品添加一条优惠规则，可添加多条。</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal23" runat="server" Text=" 规则单位" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMUnit" runat="server" RepeatDirection="Horizontal" align="left">
                                       <%-- <asp:ListItem Value="0" Text="件" Selected="True"></asp:ListItem>--%>
                                        <asp:ListItem Value="0" Text="元" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="件"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>  
                                <td  class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal3" runat="server" Text=" 优惠方式" />：</td>
                                <td height="25">
                                    <asp:RadioButtonList ID="AMType" runat="server" RepeatDirection="Horizontal"
                                        align="left">
                                        <asp:ListItem Value="3" Text="赠送" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td height="25">
                                    <table cellpadding="0" border="0" cellspacing="0" width="540px" id="txtRuleItem">
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="newslistabout">
        <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1"
            class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td style="height: 6px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="25" style="text-align: center">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" Text="保存" class="adminsubmit"
                                    OnClientClick="return SubForm();"></asp:Button>(请点击保存之后返回列表页设置商品或商家)
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table id="txtTemplate" style="display: none">
        <tr>
            <td style="width: auto" class="RuleItems">
                满<span><input type="text" class="AMDUnitValue" value="{0}"></span>
                <span class="AMDRuleUnit">&nbsp;件,赠送</span>

                <span class="ItemType"></span>
                <input type="text" style="display:none" class="AMDRateValue" value="{1}">&nbsp;<span class="hy d_default"></span>
            </td>
            <td style="width: 55px;">
                <a onclick="btnRemove(this);"><span class="pfdel">删除</span></a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

