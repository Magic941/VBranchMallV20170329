<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SupplierGifts.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.SupplierGifts" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <script src="../../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/admin/js/jquery/AMProduct.helper.js" type="text/javascript"></script>
    
    <style type="text/css">
        .borderImage{width: 81px;height: 81px;border: 1px solid #CCC;text-align: center;}
        
    </style>

    <script type="text/javascript">
        function openWindow() {
            GiftsItem.style.display = "block";
        }

    </script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div  style="background: white;width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height" style="margin-bottom: 5px">
                </h3>
        </div>
        <div class="Goodsgifts">
            <%--显示在左边的DIV--%>
            <div class="left" style="width:100%; margin:0 auto;overflow-y:hidden ">
                <div style="float:left"><h1><asp:Literal ID="litDesc" runat="server" Text="设置活动主商品"></asp:Literal></h1></div>
                <div id="divLeftbuttom" style="display:block">
                    <asp:panel id="Panel1" runat="server" defaultbutton="btnSearch">

                        <table style="clear:both" id="tbSelect">
                            <tr>
                                 <td>
                                   <asp:Literal ID="Literal5" runat="server" Text="商铺名称" />：
                                   <span>
                                       <asp:DropDownList ID="ddlSupplier" runat="server">
                                       </asp:DropDownList>
                                   </span>
                                 </td>
                                <td>
                                    &nbsp;<asp:Button ID="AddSupplier"  class="adminsubmit_short" Text="添 加" OnClick="btnSave_Click" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <%--<td colspan="3">
                                    <asp:Literal runat="server" ID="LitProductCategories" Text="商铺类别" />：
                                    <abbr class="formselect">
                                    <asp:dropdownlist ID="drpProductCategory" runat="server">
                                    </asp:dropdownlist>
                                </abbr>
                                </td>--%>
                                <td >
                                    <asp:Literal runat="server" ID="LitSupName" Text="商铺名称" />：
                                    <asp:textbox id="txtAMName" runat="server" Width="80px"/>
                                </td>
                                <td>
                                    &nbsp;<asp:button id="btnSearch" OnClick="btnSearch_Click"  runat="server" text="查询" cssclass="adminsubmit_short" />
                                </td>
                            </tr> 
                          </table>
                    </asp:panel>
                    <div class="content">
                        <div class="youhuiproductlist searchproductslist">
                        <asp:HiddenField ID="hfCurrentAllData" runat="server"/>
                             <table>
                                 <tr>
                                     <td style="text-align:right">
                                         <input type ="button" id="btnAddGifts" value="添加赠品" class="admininput" style="height:30px;" title="点击添加赠品" onclick ="openWindow();"/> &nbsp;
                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     </td>
                                 </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gdv_sup" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="800px" AutoGenerateColumns="False"
                                            OnBind="BindData" DataKeyNames="SupplierId" OnRowDataBound="gdv_Sup_RowDataBound" >
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="SupplierId" HeaderText="商家ID" />
                                            <asp:BoundField DataField="SupplierName" HeaderText="商铺名称" />
                                            <asp:BoundField DataField="AMName" HeaderText="活动名称" />
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_delete" runat="server"  ForeColor="#0066FF" OnClick="btn_delete_Click" CommandArgument='<%#Eval("SupplierId")%>' CommandName="onDelete">删除</asp:LinkButton>
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
                        </div>
                        <div class="r">
                           
                            
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:content>
