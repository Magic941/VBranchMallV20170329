<%@ Page Title="订单管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="OrderReturnDetail.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Order.OrderReturnDetail" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
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
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="退货详情" />
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
            <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                cellspacing="1" class="border">
                <tr>
                    <td style="vertical-align: top;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
                            style="padding-top: 8px">
                            <tr>
                                <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                    <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                                        cellspacing="1" class="border">
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
                            <%--<tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal18" runat="server" Text=" 退货状态" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnStauts" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="rerwer" runat="server" Text=" 买家" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbBuyerName" runat="server"></asp:Label>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="td_class" style="background-color: #E2E8EB">
                                    <asp:Literal ID="Literal18" runat="server" Text=" 退货状态" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnStauts" runat="server"  ForeColor="Red"></asp:Label>
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
                                <td class="td_class">
                                    <asp:Literal ID="Literal11" runat="server" Text=" 卖家" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbSupplierName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal5" runat="server" Text=" 退款总金额" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnAmounts" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class" style="color:red">
                                    <asp:Literal ID="Literal10" runat="server" Text="实际退款金额" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbAmountsActual" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal22" runat="server" Text="退货原因" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnReason" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal24" runat="server" Text="退货描述" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal1" runat="server" Text="图片描述" />：
                                </td>
                                <td height="25" class="td_content">
                                    <div id="imgdiv" runat="server"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal2" runat="server" Text="退货申请时间" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnApplyTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal6" runat="server" Text="退货审核时间" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnApproveTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal7" runat="server" Text="退货审核人" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnApproveUser" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal8" runat="server" Text="退款操作人" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnAccountUser" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal14" runat="server" Text="物流公司" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbInformation" runat="server"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal15" runat="server" Text="快递单号" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbExpressNO" runat="server"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td class="td_class">
                                    <asp:Literal ID="Literal9" runat="server" Text="退款时间" />：
                                </td>
                                <td height="25" class="td_content">
                                    <asp:Label ID="lbReturnAccountTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>


