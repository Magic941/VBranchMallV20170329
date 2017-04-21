<%@ Page Title="审核详情" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master" CodeBehind="OrderReturnExamine.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Order.OrderReturnExamine" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="Region" Src="~/Controls/Region.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <link href="/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/jBox/i18n/jquery.jBox-zh-CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        function UpdateAmountAdjusted(sender) {
            var amount = $(sender).parent().find('[id$=lbReturnAmounts]').text();
            var context = $(sender).hide().parent();
            context.find('[id$=txtAmountAdjusted]').show().val(amount);
            context.find('[id$=lnkSaveAmountAdjusted]').show();
            context.find('[id$=lbReturnAmounts]').hide();
        }

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfOrderMainStatus" runat="server" />
    <asp:HiddenField ID="hfTabIndex" runat="server" Value="" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="退货商品详情" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以查询退货信息，审核等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist_title">
              <asp:Label ID="lbID" runat="server" style="display:none;"></asp:Label>
            <table style="width: 100%; float: left; border: 1px solid #808080" cellpadding="2"
                cellspacing="1" class="border">
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
                            style="padding-top: 8px">
                            <tr>
                                <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                    <table class="newstitle_table" width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                                        <tr>
                                            <th>图片</th>
                                            <th>商品名称</th>
                                            <th>价格</th>
                                            <th>数量</th>
                                            <th>优惠</th>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Image ID="productimg" Width="100" Height="100" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbProductName" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbSellPrice" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbQuntity" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbReducePrice" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal18" runat="server" Text=" 退货状态" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnStauts" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal26" runat="server" Text=" 申请类型" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:DropDownList runat="server" Enabled="false" ID="liReturnType">
                                        <asp:ListItem Value="1">申请退货</asp:ListItem>
                                        <asp:ListItem Value="2">申请退款</asp:ListItem>
                                        <asp:ListItem Value="3">申请调货</asp:ListItem>
                                        <asp:ListItem Value="4">申请维修</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="rerwer" runat="server" Text=" 买家" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbBuyerName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal11" runat="server" Text=" 卖家" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbSupplierName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal5" runat="server" Text=" 退款总金额" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnAmounts" runat="server"></asp:Label>
                                   <%-- <asp:TextBox ID="txtAmountAdjusted" MaxLength="10" CssClass="OnlyFloat" runat="server" Style="width: 80px; display: none;"></asp:TextBox>
                                        <asp:Image ID="imgModifyAmountAdjusted" ToolTip="修改" AlternateText="修改" ImageUrl="/admin/Images/up_xiaobi.png" Visible="False" OnClick="return UpdateAmountAdjusted(this);" runat="server" />
                                        <asp:LinkButton ID="lnkSaveAmountAdjusted" OnClick="lnkSaveAmountAdjusted_OnClick" Style="display: none;" runat="server" Text="保存"></asp:LinkButton>

                               --%> </td>
                                

                            </tr>
                            <tr>
                                <td  class="td_class" style="background-color: #E2E8EB"><asp:Literal ID="Literal25" runat="server" Text="实际退款金额" />：</td>
                                <td><asp:TextBox ID="lbAmountActual" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal22" runat="server" Text="退货原因" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnReason" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal24" runat="server" Text="退货描述" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal1" runat="server" Text="图片描述" />：
                                </td>
                                <td height="25" class="td_content">
                                    <div id="imgdiv" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal2" runat="server" Text="退货申请时间" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnApplyTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal6" runat="server" Text="退货审核时间" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnApproveTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal7" runat="server" Text="退货审核人" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnApproveUser" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal8" runat="server" Text="退款操作人" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnAccountUser" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal14" runat="server" Text="物流公司" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbInformation" runat="server"></asp:Label>

                                </td>
                            </tr>
                             <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal15" runat="server" Text="快递单号" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbExpressNO" runat="server"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal9" runat="server" Text="退款时间" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnAccountTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang" id="returnNone" style="display:block;">
                            <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">拒绝退货或退款(请填写拒绝理由)</span>
                                    </td>
                                </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal10" runat="server" Text="拒绝理由" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:TextBox ID="txtReturnreasonREJ" TextMode="MultiLine" Width="400px" Height="100px" MaxLength="500" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal13" runat="server" Text="退货地址" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:TextBox ID="txtReturnAddress" TextMode="MultiLine" Width="400px" Height="100px" runat="server"></asp:TextBox>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">同意退货(请填写退货收件人信息)</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal13" runat="server" Text="收货人" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipName" runat="server" Width="320px"></asp:TextBox>
                                        <%--   <asp:Label ID="lblShipName" runat="server"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal16" runat="server" Text="收货人地区" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <%--      <asp:Label ID="lblShipRegion" runat="server"></asp:Label>--%>
                                        <%--         <Maticsoft:RegionDropList ID="RegionList" runat="server" IsNull="true" />--%>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <uc1:Region ID="RegionList" runat="server" VisibleAll="true" VisibleAllText="--请选择--" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal17" runat="server" Text="详细地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipAddress" runat="server" Width="320px"></asp:TextBox>
                                        <%--    <asp:Label ID="lblShipAddress" runat="server"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal19" runat="server" Text="电话号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipTelPhone" runat="server" Width="320px"></asp:TextBox>
                                        <%--  <asp:Label ID="lblShipTelPhone" runat="server"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal20" runat="server" Text="手机号码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipCellPhone" runat="server" Width="320px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal21" runat="server" Text="邮政编码" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipZipCode" runat="server" Width="320px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal23" runat="server" Text="邮箱地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:TextBox ID="txtShipEmail" runat="server" Width="320px"></asp:TextBox>
                                    </td>
                                </tr>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal12" runat="server" Text="备注" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:TextBox ID="txtReturnRemark" TextMode="MultiLine" Width="400px" Height="100px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="25" colspan="2" align="center">
                        <asp:Button ID="btnPass" runat="server" Text="审核通过"
                            OnClick="btnPass_OnClick" class="adminsubmit_short"></asp:Button><asp:Button ID="btnRefusal" runat="server" Text="拒绝"
                                OnClick="btnRefusal_OnClick" class="adminsubmit_short"></asp:Button></td>
                </tr>
            </table>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
