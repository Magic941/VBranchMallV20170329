<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Order.aspx.cs" Inherits="Maticsoft.Web.Admin.Statistics.Order" Title="订单统计" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="订单统计" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以查看订单统计信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="5" width="280px" border="0"  style="border-collapse: collapse;margin-left: 5%;margin-top: 10px;margin-bottom: 10px">
                        <tr>
                            <td class="td_class" style="width: auto">&emsp;</td>
                            <td style="text-align: center;font-weight: bold;">单数</td>
                            <td style="text-align: right;font-weight: bold;">金额</td>
                        </tr>
                        <tr>
                            <td  class="td_class" style="width: auto;text-align: center;font-weight: bold;">
                                未支付
                            </td>
                            <td height="25" style="text-align: right">
                                <asp:Label  ID="lblNotPayQuantity" runat="server"></asp:Label>&emsp;
                            </td>
                            <td height="25" style="text-align: right">
                                &emsp;<asp:Label  ID="lblNotPayPrice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_class" style="width: auto;text-align: center;font-weight: bold;">
                                进行中
                            </td>
                            <td height="25" style="text-align: right">
                                <asp:Label  ID="lblPaymentQuantity" runat="server"></asp:Label>&emsp;
                            </td>
                            <td height="25" style="text-align: right">
                                &emsp;<asp:Label  ID="lblPaymentPrice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_class" style="width: auto;text-align: center;font-weight: bold;">
                                已完成
                            </td>
                            <td height="25" style="text-align: right">
                                <asp:Label  ID="lblCompleteQuantity" runat="server"></asp:Label>&emsp;
                            </td><td height="25" style="text-align: right">
                            &emsp;<asp:Label ID="lblCompletePrice" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width: 100%;display: none;" cellpadding="2" cellspacing="1" class="border">
        
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnEdit" runat="server" Text="重新统计" class="adminsubmit" >
                                </asp:Button>
                            </td>
                        </tr>
                        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
