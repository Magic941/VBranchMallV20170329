<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master"  AutoEventWireup="true" CodeBehind="AddFree.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.AddFree" %>

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
        .marginstyle {
            margin-bottom: 2px;
            width:200px;
            height:20px;
            line-height:20px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnAddItem").click(function () {
                $("#txtRuleItem").append($("#txtTemplate").html());
            });
            $("#ctl00_ContentPlaceHolder1_AMType").find('input').click(function () {
                var value = $(this).val();
                if (value == 0) {
                    $(".ItemType").text("");
                    $(".ItemUnit").text("%");
                }
                if (value == 1) {
                    $(".ItemType").text("减价");
                    $(".ItemUnit").text("元");
                }
            });
            $("#ctl00_ContentPlaceHolder1_AMUnit").find('input').click(function () {
                var value = $(this).val();
                if (value == 0) {
                    $(".AMDRuleUnit").text("件,");
                }
                if (value == 1) {
                    $(".AMDRuleUnit").text("元,");
                }
            });
        });
        function btnRemove(_self) {
            $(_self).parent().parent().remove();
        }

        function SubForm() {
          
            var startdate = $("#ctl00_ContentPlaceHolder1_txtAMStartdate").val();
            var enddate = $("#ctl00_ContentPlaceHolder1_txtAMEnddate").val();
            
            if (startdate == "" || enddate == "") {
                ShowFailTip("请正确选择活动时间!");
                return false;
            }

            var ruleValue = "";
            $(".RuleItems").each(function () {
                var AMDUnitValue = $(this).find(".AMDUnitValue").val();
                var AMDRateValue = $(this).find(".AMDRateValue").val();
                if (AMDUnitValue == "" || AMDRateValue == "") {
                    return false;
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
                                    <span style="font-size: 16px; font-weight: bold; padding-left: 20px">活动规则设置</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB"><b>*</b><asp:Literal ID="AMDate" runat="server" Text=" 活动时间" />：</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtAMStartdate" onclick="WdatePicker()"></asp:TextBox>&nbsp;至&nbsp;
                                    <asp:TextBox runat="server" ID="txtAMEnddate" onclick="WdatePicker()"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal4" runat="server" Text=" 是否包邮" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMFreeShipment" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="是" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal21" runat="server" Text=" 应用方式" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMApplyStyles" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="单个商品" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="全场商品"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr style="display: block">
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal1" runat="server" Text=" 是否启用" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMStatus" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="是" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="否"></asp:ListItem>
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
                                    <span style="font-size: 16px; color: blue; padding-left: 20px">设置包邮规则</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <b>*</b><asp:Literal ID="Literal23" runat="server" Text=" 规则单位" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:RadioButtonList ID="AMUnit" runat="server" RepeatDirection="Horizontal" align="left">
                                        <asp:ListItem Value="0" Text="件" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="元"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: auto; height: 25px" class="RuleItems" colspan="3">&nbsp;<b>*</b>满<span><asp:TextBox runat="server" ID="UnitValue"></asp:TextBox></span>
                                    <span class="AMDRuleUnit">&nbsp;件</span>
                                    <span style="font-weight: bold">包邮</span>
                                    &nbsp; &nbsp;
                                </td>

                            </tr>
                            </table>
                        
                            <table id="txtTemplate">
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left; font-weight: bold; font-size: 14px;">非包邮地区设置</td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="width: 50px">
                                        <asp:Literal ID="Literal5" runat="server" Text="区域" />：
                                    </td>
                                    <td height="25" colspan="2">
                                        <asp:DropDownList ID="ddl_area" Width="150px" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="添 加" class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdv_Shipping" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="497px" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gdv_Shipping_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="RegionName" HeaderText="区域" />
                                            <asp:BoundField DataField="username" HeaderText="创建人" />
                                            <%--<asp:BoundField DataField="id" HeaderText="id" />--%>
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_delete" runat="server"  ForeColor="#0066FF" OnClick="btn_delete_Click" CommandArgument='<%#Eval("id")%>' CommandName="onDelete">删除</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#cccccc" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Center" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
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
    
</asp:Content>

