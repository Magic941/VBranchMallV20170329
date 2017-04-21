<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.AttendanceType.List" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="考勤类型管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以添加，修改，删除考勤类型" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:Literal ID="Literal10" runat="server" Text="状态" />：
                    <asp:DropDownList ID="DropStatus" runat="server" class="dropSelect">
                    </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server">
                        <asp:Literal ID="Literal3" runat="server" Text="类型名称" />:</asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>&nbsp
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
                        <a href="add.aspx">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/list.gif) no-repeat"><a href="list.aspx">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblScan%>" /></a><b>|</b></li>
                    <%--<li style="background: url(/admin/images/reload.png) no-repeat"><a href="#">
                    <asp:Literal ID="Literal7" runat="server" Text="返回" /></a><b>|</b></li>--%></ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="TypeID" ShowExportExcel="True" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="5" BorderWidth="1px" ShowCheckAll="true">
            <columns>
                <asp:TemplateField HeaderText="类型编号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                    <ItemTemplate>
                            <%# Eval("TypeID")%>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                    <ItemTemplate>
                            <%# Eval("TypeName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                    <ItemTemplate>
                            <%# GetStatus (Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="顺序" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                    <ItemTemplate>
                            <%# Eval("Sequence")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                    <ItemTemplate>
                            <%# Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%# Eval("Remark")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:HyperLinkField HeaderText="详细" ItemStyle-Width="50px"
                    DataNavigateUrlFields="TypeID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                    Text="详细" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ItemStyle-Width="50px"
                    DataNavigateUrlFields="TypeID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>">
                        </asp:LinkButton></ItemTemplate></asp:TemplateField></columns>
            <footerstyle height="25px" horizontalalign="Right" />
            <headerstyle height="35px" />
            <pagerstyle height="25px" horizontalalign="Right" />
            <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
            <rowstyle height="25px" />
            <sortdirectionstr>DESC</sortdirectionstr>
        </cc1:GridViewEx></div>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
        <tr>
            <td height="10px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 1px;">
            </td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="批量删除" class="adminsubmit" OnClick="btnDelete_Click" />
                &nbsp;&nbsp;<asp:Literal ID="ltlStatus" runat="server" Text="状态："></asp:Literal><asp:DropDownList
                    ID="drop_Status" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drop_Status_Changed"> 
                    <asp:ListItem Value="-1" Text="--请选择--" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Value="0" Text="无效"></asp:ListItem>
                    <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
