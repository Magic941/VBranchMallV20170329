<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    Codebehind="ShowCount3D.aspx.cs" Inherits="Maticsoft.Web.Admin.Options.ShowCount3D" Title="<%$Resources:Poll,ptOptionsShowCount%>" %>
<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
  <table class="user_border" cellspacing="0" cellpadding="0" width="100%" align="center"
                border="0" id="table1">
                <tr>
                    <td valign="top">
                        <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                            <tr>
                                <td align="left">
                                    <span style="font-size: 12pt; font-weight: bold; color: #3666AA">
                                        <img src="/admin/images/icon.gif" align="absmiddle" style="border-width: 0px;" />
                                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Poll,ptOptionsShowCount%>" /></span>
                                </td>
                                <td align="middle">
                                    <table align="left" id="table3">
                                        <tr valign="top" align="left">
                                            <td width="80">
                                                <a href="../Forms/Index.aspx">
                                                    <img title="" src="/admin/images/view.gif" border="0" alt=""/></a>
                                            </td>                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>  
    <div style="text-align: center" class="style1">
        <br />
        【<asp:Label ID="lblFormName" runat="server" Font-Bold="true" Text="Label"></asp:Label>】<asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Poll,ptOptionsShowCount %>" /><asp:Label
            ID="lblFormID" runat="server" Visible="false" Text="Label"></asp:Label>
    </div>    
    <div align="center">
        <table cellspacing="0" cellpadding="5" width="100%" border="0">
            <tr>
            <td align="left">
            &nbsp;<asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Poll,lblOptionsCommon %>" /><b><%=usercount%> </b>
            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Poll,lblOptionsTotal %>" /><b><%=allsum%></b>
            </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridViewTopic" runat="server" Height="1px" Width="100%" CellPadding="4"
                        ForeColor="#363636" GridLines="None" AutoGenerateColumns="false" DataKeyNames="ID"
                        OnRowDataBound="gridViewTopic_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <table cellspacing="0" bordercolordark='#f9ce34' bordercolorlight='#fff2bb' cellpadding="0"
                                        width="100%" border="1">
                                        <tr>
                                            <td style="background-image: url(/admin/images/headbg.jpg)" height="27" align="left"
                                                class="style2">
                                                &nbsp;·
                                                <%#DataBinder.Eval(Container.DataItem,"Title") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <%# GetChartHtml((int)(DataBinder.Eval(Container.DataItem, "ID")))%>
                                                <br>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <input type="button" name="button1" value="<%$Resources:Site,btnBackText %>" onclick="history.back()" runat="server">
    </div>
    </div>
    <%--<uc1:copyright ID="Copyright1" runat="server" />--%>    
</asp:Content>