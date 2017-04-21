<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master"
    CodeBehind="GiftsList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Gift.GiftsList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UpdateState(id) {
            $("#img" + id).hide();
            $("#lb" + id).hide();
            $("#txt" + id).show();
        }

        function SetStock(controls, id) {
            var stock = $.trim($(controls).val()); //去掉字符串左右两边的空字符
            if (stock == "") {
                alert("请输入库存数！");
            }
            else {
                $.ajax({
                    url: ("GiftsList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "SetStock", Callback: "true", GiftId: id, Stock: stock },
                    async: false,
                    success: function (resultData) {
                        if (resultData.STATUS == "OK") {
                            $("#img" + id).show();
                            $("#lb" + id).show().text(stock);
                            $("#txt" + id).hide();
                        }
                        else {
                            alert("操作失败！");
                        }
                    }

                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="积分礼品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行添加，编辑，删除积分礼品信息操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    </asp:Label><asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a href="AddGift.aspx">
                            <asp:Literal ID="Literal5" runat="server" Text="添加" /></a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" DataKeyNames="GiftId" ShowExportExcel="False" ShowExportWord="False"
            OnRowDeleting="gridView_RowDeleting" ExcelFileName="FileName1" CellPadding="0"
            BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Width="50px" ImageAlign="Middle" ImageUrl='<%#GetImageUrl(Eval("ThumbnailsUrl").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="礼品名" ItemStyle-HorizontalAlign="left" />
                <asp:TemplateField HeaderText="库存" SortExpression="Stock" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="80px">
                    <ItemTemplate>
                        <span id="lb<%# Eval("GiftId")%>">
                            <%# Eval("Stock")%></span>
                        <input id="txt<%# Eval("GiftId")%>" type="text" value='<%# Eval("Stock")%>' style="width: 38px;
                            display: none;" onblur='SetStock(this, <%# Eval("GiftId")%>)' />
                        &nbsp;<img alt="" id="img<%# Eval("GiftId")%>" src="/admin/Images/up_xiaobi.png"
                            onclick="return UpdateState(<%# Eval("GiftId")%>)" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="成本价" SortExpression="CostPrice" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="60px">
                    <ItemTemplate>
                        ￥<asp:Literal runat="server" Text='<%#Decimal.Round(Convert.ToDecimal(Eval("CostPrice")), 2) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="销售价" SortExpression="SalePrice" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="60px">
                    <ItemTemplate>
                        ￥<asp:Literal runat="server" Text='<%#Decimal.Round(Convert.ToDecimal(Eval("SalePrice")), 2) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="兑换所需积分" SortExpression="NeedPoint" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litNeedPoint" Text='<%# Eval("NeedPoint") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SaleCounts" HeaderText="兑换量" SortExpression="SaleCounts"
                    ItemStyle-Width="60px" ItemStyle-HorizontalAlign="center" />
                <asp:TemplateField ControlStyle-Width="120" HeaderText="上架时间" SortExpression="CreateDate"
                    ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreateDate"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle />
                    <ItemTemplate>
                        &nbsp;&nbsp;<span runat="server" id="lbtnModify"><a href="UpdateGift.aspx?giftId=<%#Eval("GiftId") %>" style="color: Blue">
                            编辑&nbsp;&nbsp;</a> </span> 
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>" ForeColor="Blue"></asp:LinkButton>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
