<%@ Page Title="<%$Resources:SysManage,ptOnlineUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="OnlineUser.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.OnlineUser" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,ptOnlineUser%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SysManage,lblOnlineUser%>" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Search -->
        <%--<div class="newslistabout" >
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                &nbsp;
            </td>
            <td class="tdbg">
            </td>
        </tr>
    </table>--%>
        <!--Search end-->
                <div class="newslist">
            <div class="newsicon">
                <ul>
                    <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a> <b>|</b> </li>
                   <li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>--%>
                    
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView1" runat="server" AllowPaging="True" Width="100%" CellPadding="3" ShowToolBar="True"
            OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="SessionID"  OnBind="BindData"  
            OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
            RowStyle-HorizontalAlign="Center" CheckColumnVAlign="Middle" ShowCheckAll="false">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Site,lblSerialNumber%>" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# gridView1.PageIndex*gridView1.PageSize+ Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SessionID" HeaderText="<%$Resources:SysManage,fieldSessionID%>" SortExpression="SessionID"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UserIP" HeaderText="<%$Resources:SysManage,fieldUserIP%>" SortExpression="UserIP" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Browser" HeaderText="<%$Resources:SysManage,fieldBrowser%>" SortExpression="Browser" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OSName" HeaderText="<%$Resources:SysManage,fieldOSName%>" SortExpression="OSName" ItemStyle-HorizontalAlign="Center" />
            </Columns>
           <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
