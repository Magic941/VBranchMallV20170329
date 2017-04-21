<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ReservationLists.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Reservation.ReservationLists" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        用户预约管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对用户预约进行管理等操作
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a href="AddReservation.aspx">添加</a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:gridviewex id="gridView" runat="server" allowpaging="True" allowsorting="True"
            showtoolbar="True" autogeneratecolumns="False" onbind="BindData" onpageindexchanging="gridView_PageIndexChanging"
            onrowdeleting="gridView_RowDeleting" unexportedcolumnnames="Modify" onrowdatabound="gridView_RowDataBound"
            width="100%" pagesize="20" showexportexcel="False" showexportword="False" excelfilename="FileName1"
            cellpadding="3" borderwidth="1px" showcheckall="false" datakeynames="ReservalId">
                <columns>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="预约号" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ReservalId")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="用户名" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ContactName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="预约类型" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetReservaType(Eval("ServiceId"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="当前状态" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField ControlStyle-Width="120" HeaderText="预约日期" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("ReservalDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="创建时间" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("CreatedDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
           
                <asp:TemplateField ControlStyle-Width="50" HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="UpdateReservation.aspx?id=<%#Eval("ReservalId") %>" ItemStyle-HorizontalAlign="Center">编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确认要删除吗"   Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
                <footerstyle height="25px" horizontalalign="Right" />
                <headerstyle height="35px" />
                <pagerstyle height="25px" horizontalalign="Right" />
                <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
                <rowstyle height="25px" />
                <sortdirectionstr>DESC</sortdirectionstr>
            </cc1:gridviewex>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
