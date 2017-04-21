<%@ Page Title="礼品兑换管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ExchangeDetail.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Gift.ExchangeDetail" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UpdateState(id) {
            var old_status = $("#lb" + id).attr("value");
            $("#img" + id).hide();
            $("#lb" + id).hide();
            $("#txt" + id + ">option[value=" + old_status + "]").attr("selected", "selected");
            $("#txt" + id).show();
        }

        function SetStatus(controls, id) {
            var old_status = $("#lb" + id).attr("value");
            var status = $(controls).val(); //去掉字符串左右两边的空字符
            var txtstatus = $("#txt" + id + " option:selected").text();
            if (status < old_status) {
                ShowFailTip("请选择正确的状态值！");
                return;
            }
           
                $.ajax({
                    url: ("ExchangeDetail.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "SetStatus", Callback: "true", DetailID: id, Status: status },
                    async: false,
                    success: function (resultData) {
                        if (resultData.STATUS == "OK") {
                            $("#img" + id).show();
                            $("#lb" + id).show().text(txtstatus);
                            $("#txt" + id).hide();
                        }
                        else {
                            ShowFailTip("操作失败！");
                        }
                    }

                });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtUserName" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td width="180" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal1" runat="server" Text="状态" />：
                    <asp:DropDownList ID="DropDetailType" runat="server" class="dropSelect" Width="120">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="0">待处理</asp:ListItem>
                        <asp:ListItem Value="1">已处理，待发货</asp:ListItem>
                        <asp:ListItem Value="2">已发货</asp:ListItem>
                        <asp:ListItem Value="3">完成</asp:ListItem>
                        <asp:ListItem Value="4">已缺货</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" DataKeyNames="DetailID" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true">
            <Columns>
                <asp:BoundField DataField="GiftName" HeaderText="礼品名称" SortExpression="GiftName"
                    ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="CostScore" HeaderText="消费积分" SortExpression="CostScore"
                    ItemStyle-HorizontalAlign="center" />
                <asp:BoundField DataField="Description" HeaderText="兑换详情" SortExpression="Description"
                    ItemStyle-HorizontalAlign="center" />
                <asp:TemplateField ControlStyle-Width="120" HeaderText="订单" SortExpression="Type"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetOrder(Convert.ToInt32(Eval("OrderID")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="申请时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreatedDate"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="状态" SortExpression="Type"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%-- <%#GetStatusName(Convert.ToInt32(Eval("Status")))%>--%>
                        <span id="lb<%# Eval("DetailID")%>" value="<%# Eval("Status")%>">
                            <%#GetStatusName(Convert.ToInt32(Eval("Status")))%></span>
                        <select id="txt<%# Eval("DetailID")%>" style="width: 120px; display: none;"
                            onblur='SetStatus(this, <%# Eval("DetailID")%>)'>
                            <option value="0">待处理</option>
                            <option value="1">已处理，待发货</option>
                            <option value="2">已发货</option>
                            <option value="3">完成</option>
                            <option value="4">已缺货</option>
                        </select> 
                        &nbsp;<img alt="" id="img<%# Eval("DetailID")%>" src="/admin/Images/up_xiaobi.png"
                            onclick="return UpdateState(<%# Eval("DetailID")%>)" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                    <asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem Value="-1" Selected="True" Text="请选择状态值"></asp:ListItem>
                        <asp:ListItem Value="0">待处理</asp:ListItem>
                        <asp:ListItem Value="1">已处理，待发货</asp:ListItem>
                        <asp:ListItem Value="2">已发货</asp:ListItem>
                        <asp:ListItem Value="3">完成</asp:ListItem>
                        <asp:ListItem Value="4">已缺货</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBatch" runat="server" Text="批量设置" class="adminsubmit" OnClick="btnBatch_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
