<%@ Page Language="C#" Title="编辑批发规则" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true"CodeBehind="UpdateRule.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.WholeSale.UpdateRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <style type="text/css">
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
            var itemTypevalue = $("#ctl00_ContentPlaceHolder1_radItemType").find("input[checked='checked']").val();
            if (itemTypevalue == 0) {
                $(".ItemType").text("");
                $(".ItemUnit").text("%");
            }
            if (itemTypevalue == 1) {
                $(".ItemType").text("总减价");
                $(".ItemUnit").text("元");
            }
            if (itemTypevalue == 2) {
                $(".ItemType").text("每个减");
                $(".ItemUnit").text("元");
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
            $("#ctl00_ContentPlaceHolder1_radItemType").find('input').click(function () {
                var value = $(this).val();
                if (value == 0) {
                    $(".ItemType").text("");
                    $(".ItemUnit").text("%");
                }
                if (value == 1) {
                    $(".ItemType").text("总减价");
                    $(".ItemUnit").text("元");
                }
                if (value == 2) {
                    $(".ItemType").text("每个减");
                    $(".ItemUnit").text("元");
                }
            });
            $("#ctl00_ContentPlaceHolder1_radUnit").find('input').click(function () {
                var value = $(this).val();
                if (value == 0) {
                    $(".RuleUnit").text("个。");
                }
                if (value == 1) {
                    $(".RuleUnit").text("元。");
                }
            });
        });
        function btnRemove(_self) {
            $(_self).parent().parent().remove();
        }

        function SubForm() {
            var name = $("#ctl00_ContentPlaceHolder1_txtRuleName").val();
            if (name == "") {
                ShowFailTip("请填写规则名称");
                return false;
            }
            var ruleValue = "";
            $("#txtRuleItem").find(".RuleItems").each(function () {
                var unitValue = $(this).find(".UnitValue").val();
                var rateValue = $(this).find(".RateValue").val();
                if (unitValue == "" || rateValue == "") {
                    return ;
                }
                if (ruleValue == "") {
                    ruleValue = unitValue + "|" + rateValue;
                } else {
                    ruleValue = ruleValue + "," + unitValue + "|" + rateValue;
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
                                    <span style="font-size: 16px; padding-left: 20px">批发规则设置</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal2" runat="server" Text=" 规则名称" />：
                                </td>
                                <td height="35" class="td_content">
                                    <asp:TextBox ID="txtRuleName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal23" runat="server" Text=" 规则单位" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="radUnit" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="个" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="元"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal21" runat="server" Text=" 应用方式" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="radMode" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="单个商品" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="商品总计"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr style=" display:none">
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal1" runat="server" Text=" 是否启用" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="radStatus" runat="server" RepeatDirection="Horizontal" align="left">
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
                                    <a href="javascript:void(0)" class="pffule_button" id="btnAddItem"><span style="padding-left: 24px;">
                                        添加规则 </span></a><span style="float: left; padding-left: 8px">点击“新增规则”为您的商品添加一条优惠规则，可添加多条。</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td height="25">
                                    <asp:RadioButtonList ID="radItemType" runat="server" RepeatDirection="Horizontal"
                                        align="left">
                                        <asp:ListItem Value="0" Text="打折" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="减价"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="固定价"></asp:ListItem>
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
                <tr style=" display:none">
                    <td style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                            <tr>
                                <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                    <span style="font-size: 16px; padding-left: 20px">批发对象选择</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="80">
                                </td>
                                <td height="25">
                                    会员等级：
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td height="25">
                                    <asp:CheckBoxList ID="ChkUserRank" runat="server" RepeatDirection="Horizontal" align="left">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td height="25">
                                    经销商等级：
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td height="25">
                                    <asp:CheckBoxList ID="ChkDealerRank" runat="server" RepeatDirection="Horizontal"
                                        align="left">
                                    </asp:CheckBoxList>
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
                                    OnClientClick="return SubForm();"></asp:Button>
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
                满<span><input type="text" class="UnitValue" value="{0}"></span><span class="RuleUnit">个。</span>
                <span class="ItemType"></span>
                <input type="text" class="RateValue" value="{1}"><span class="ItemUnit">%</span>&nbsp;<span class="hy d_default"></span>
            </td>
            <td style="width: 55px;">
                <a onclick="btnRemove(this);"><span class="pfdel">删除</span></a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

