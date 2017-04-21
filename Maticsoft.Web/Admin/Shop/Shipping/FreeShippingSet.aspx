<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master" CodeBehind="FreeShippingSet.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Shipping.FreeShippingSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="区域免邮设置" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对区域免邮规则进行添加，删除等操作" />
                    </td>
                </tr>
            </table>
            
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class" style="width:50px">
                                <asp:Literal ID="Literal3" runat="server" Text="区域" />：
                            </td>
                            <td height="25">
                                
                                <asp:DropDownList ID="ddl_area" Width="200px" runat="server">
                                </asp:DropDownList>
                                <asp:Literal ID="Literal4" runat="server" Text="免邮值" />：
                                <asp:TextBox ID="txt_Needed" runat="server"></asp:TextBox>
                                &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>" class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                            </td>
                        </tr>
                   
                    </table>
               
             <br/>
            <asp:GridView ID="gdv_Shipping" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="497px" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gdv_Shipping_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="RegionName" HeaderText="区域" />
                    <asp:BoundField DataField="totalmoney" HeaderText="免邮值" />
                    <asp:BoundField DataField="username" HeaderText="创建人" />
                    <asp:BoundField DataField="createdate" HeaderText="创建时间" />
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
        </div>
    </div>
</asp:Content>
