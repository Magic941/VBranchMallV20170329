<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MLShow.aspx.cs" Inherits="Maticsoft.Web.Admin.MLShow" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/admin/Style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="dropLanguage" runat="server">
        </asp:DropDownList>
        <asp:TextBox ID="txtMValue" runat="server" Width="150px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtMValue"></asp:RequiredFieldValidator>
        <asp:Button ID="btnAddMValue" runat="server" Text="<%$ Resources:Site, btnAddText %>"
            OnClick="btnAddMValue_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
            onmouseout="this.className='inputbutton'"></asp:Button>        
        <asp:Label ID="lblF" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblK" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
                        Width="100%" PageSize="10" DataKeyNames="MultiLang_iID" ShowExportExcel="False" ShowExportWord="False"
                        ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false"
                        ShowToolBar="True">
                        <Columns>                           
                            <asp:BoundField DataField="MultiLang_cLang" HeaderText="语言" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="MultiLang_cValue" HeaderText="内容"  ItemStyle-HorizontalAlign="Left" />                            
                            <asp:TemplateField ControlStyle-Width="35" HeaderText="<%$ Resources:Site, btnDeleteText %>" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                        <FooterStyle Height="25px" HorizontalAlign="Right" />
                        <HeaderStyle Height="35px" />
                        <PagerStyle Height="25px" HorizontalAlign="Right" />
                        <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
                        <RowStyle Height="25px" />
                        <SortDirectionStr>DESC</SortDirectionStr>
                    </cc1:GridViewEx>
                    <p><asp:Label ID="lblML" runat="server" Text="" ForeColor="Red"></asp:Label></p>
    </div>
    </form>
</body>
</html>
