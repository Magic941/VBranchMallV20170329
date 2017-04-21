<%@ Page Title="添加配送方式" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddShippingType.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Shipping.AddShippingType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
        <!--Select2 Start-->
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <!--Select2 End-->
    <script src="/Scripts/angular/angular.min.js" type="text/javascript"></script>
    <script src="/Scripts/angular/ui/select2.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
    <style type="text/css">
        .border .ng-scope [type=text]{min-height: 5px;}
        input{ padding: 2px;}
    </style>
    <script type="text/javascript">
        var v;
        $(function () {
            v = $("[id$='ddlType']").select2({
                placeholder: "请选择物流", width: 200,
                formatSelection: format
            });

        });

        function format(state) {
            if (!state.id) return state.text;
            $("[id$=tName]").val(state.text);
            return state.text;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="添加配送方式" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加配送方式操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">支付方式</a></li>
                    <li class="normal" onclick="nTabs(this,2);" ><a href="javascript:;">地区价格</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr >
                                    <td class="td_class">
                                        <asp:Literal ID="Literal5" runat="server" Text="物流公司" />：
                                    </td>
                                    <td height="25">
                                        <asp:DropDownList ID="ddlType" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal2" runat="server" Text="配送方式名称" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                            Display="Dynamic" ControlToValidate="tName"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal3" runat="server" Text="起步重量" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="tWeight" TabIndex="2" runat="server" Width="250px" MaxLength="20"
                                            Text="0"></asp:TextBox>（克）
                                        <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" Type="Integer" ControlToValidate="tWeight"
                                            MinimumValue="0" MaximumValue="10000" runat="server" ErrorMessage="数字在0-10000之间"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal4" runat="server" Text="加价重量" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="tAddWeight" TabIndex="3" runat="server" Width="250px" MaxLength="20"
                                            Text="0"></asp:TextBox>（克）
                                        <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" Type="Integer" ControlToValidate="tAddWeight"
                                            MinimumValue="0" MaximumValue="10000" runat="server" ErrorMessage="数字在0-10000之间"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal9" runat="server" Text="起步价" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="tPrice" TabIndex="2" runat="server" Width="250px" MaxLength="20"
                                            Text="0"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal10" runat="server" Text="加价" />：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="tAddPrice" TabIndex="3" runat="server" Width="250px" MaxLength="20"
                                            Text="0"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal7" runat="server" Text="描述" />：
                                    </td>
                                    <td height="50">
                                        <asp:TextBox ID="tDesc" runat="server" Width="250px" TextMode="MultiLine" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content1" tabindex="1" class="none4">
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">
                        </td>
                        <td height="25">
                            <asp:CheckBoxList ID="ckPayType" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content2" tabindex="2" class="none4">
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td>
                            <div ng-app="RegionApp" style="margin-top: 20px; margin-left: 20px; margin-bottom: 20px">
                                <div ng-controller="RegionCtrl">
                                    <span>您已设置 {{regions.length}} 个地区价格</span>
                                        <table id="RegionSave" style="width: 380px;" cellpadding="2" cellspacing="1" class="GridViewTyle">
                                            <tr height="25px" style="background-color: #CCCCCC; height: 25px; background: #FFF">
                                                <th scope="col" style="border: 1px solid #dae2e8; border-right: 0px;">
                                                    地区
                                                </th>
                                                <th scope="col" style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;">
                                                    起步价
                                                </th>
                                                <th scope="col" style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;">
                                                    加价
                                                </th>
                                                <th scope="col" style="border: 1px solid #dae2e8; border-left: 0px;">
                                                    操作
                                                </th>
                                            </tr>
                                            <tr ng-repeat="item in regions" height="27px" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#CBE3F4';" onmouseout="this.style.backgroundColor=currentcolor" style="height: 25px; background-color: rgb(255, 255, 255);  background-position: initial initial; background-repeat: initial initial;">
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <select class="selectregion" multiple="true" style="width: 250px" ui-select2 ng-model="item.ids">
                                                         <option ng-repeat="region in regions.BaseRegions" value="{{region.id}}">{{region.text}}</option>
                                                    </select>
                                                </td>
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <input type="text" ng-model="item.price" size="10" style="text-align: right;" class="OnlyFloat" placeholder="起步价" />
                                                </td>
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <input type="text" ng-model="item.addprice" size="10" style="text-align: right;" class="OnlyFloat" placeholder="加价" />
                                                </td>
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <input type="button" class="adminsubmit_short" ng-click="removeRegion(item)" value="删除" />
                                                </td>
                                            </tr>
                                        </table>
                                    <div novalidate>
                                    <div style="margin-top: 20px;">
                                        <table id="Table1" style="width: 380px;" cellpadding="2" cellspacing="1" class="GridViewTyle">
                                            <tr height="25px" style="background-color: #CCCCCC; height: 25px; background: #FFF">
                                                <th scope="col" colspan="4" style="border: 1px solid #dae2e8;">
                                                    新增地区价格
                                                </th>
                                            </tr>
                                            <tr height="27px" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#CBE3F4';" onmouseout="this.style.backgroundColor=currentcolor" style="height: 25px; background-color: rgb(255, 255, 255);  background-position: initial initial; background-repeat: initial initial;">
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <asp:DropDownList ID="drpRegion" runat="server" multiple="true" Width="250px" ui-select2 ng-model="ids" Style="margin-right: 5px" />
                                                </td>
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <input type="text" ng-model="price" size="10" style="text-align: right;" class="OnlyFloat" placeholder="起步价" />
                                                </td>
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <input type="text" ng-model="addprice" size="10" style="text-align: right;" class="OnlyFloat" placeholder="加价" />
                                                </td>
                                                <td align="center" height="27px" style="padding-left: 5px; height: 27px;">
                                                    <input type="button" class="adminsubmit_short" ng-click="addRegion()" value="添加" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <input name="RegionData" id="hfRegionData" type="hidden" runat="server"/>
            <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1"
                class="border">
                <tr>
                    <td class="td_class">
                    </td>
                    <td height="25">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                            OnClick="btnSave_Click" OnClientClick="return sub()" class="adminsubmit_short"></asp:Button>
                        <asp:Button ID="Button2" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                            OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script src="/Scripts/json2.js" type="text/javascript"></script>
    <script src="/admin/js/Shop/Shipping/UpdateShipType.js" type="text/javascript"></script>
    <script type="text/javascript">
        function sub() {
            $('[id$=hfRegionData]').val(JSON.stringify($('#RegionSave').scope().regions));
            return true;
        }
    </script>
</asp:Content>
