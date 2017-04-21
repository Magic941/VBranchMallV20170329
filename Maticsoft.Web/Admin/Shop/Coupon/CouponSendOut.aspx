<%@ Page Title="优惠券发送规则" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master" CodeBehind="CouponSendOut.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Coupon.CouponSendOut" %>
<asp:Content ID="contenthead" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(function () {
            $(".select2").select2({ placeholder: "请选择", width: '240px' });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>

                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="优惠券发放规则" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行优惠券发放操作" />
                    </td>
                </tr>
            </table>
        </div>

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>

                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">批次号：<asp:DropDownList ID="ddlBatch" CssClass="select2" runat="server" Width="124px">
                </asp:DropDownList>
                    &nbsp;&nbsp; 优惠券分类：<asp:DropDownList ID="ddlRule" CssClass="select2" runat="server" Width="124px">
                    </asp:DropDownList>
                    &nbsp; &nbsp;&nbsp; 数量：
                    <asp:TextBox ID="txtQuantity" value="1" runat="server"></asp:TextBox>&nbsp; &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="确定" OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">用户：&nbsp;&nbsp;<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
           优惠券号：<asp:TextBox ID="CouponCode" runat="server"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnSet" runat="server" Text="手动发送" OnClick="btnSet_Click" />
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li>规则列表</li>
                </ul>
            </div>
        </div>
        <asp:GridView ID="gv_CouponList" runat="server" CellPadding="3" GridLines="Horizontal" Width="866px" AutoGenerateColumns="False"  AllowSorting="True" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" OnRowDeleting="gv_CouponList_RowDeleting">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <asp:BoundField DataField="batchID" ControlStyle-ForeColor="White" HeaderText="批次号" />
                <asp:BoundField DataField="Name" ControlStyle-ForeColor="White" HeaderText="优惠券类别" />
                <asp:BoundField DataField="CouponCount" ControlStyle-ForeColor="White" HeaderText="数量" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" HorizontalAlign="Center" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle BackColor="#E7E7FF" HorizontalAlign="Center" ForeColor="#4A3C8C" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
    </div>
</asp:Content>
