<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ShowCount.aspx.cs" Inherits="Maticsoft.Web.Admin.Options.ShowCount"
    Title="<%$Resources:Poll,ptOptionsShowCount%>" %>

<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Poll,ptOptionsShowCount%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Poll,lblOptionsShowCount%>" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center" class="tdbg">
            <br />
            【<asp:Label ID="lblFormName" runat="server" Font-Bold="true" Text=""></asp:Label>】<asp:Literal
                ID="Literal3" runat="server" Text="<%$Resources:Poll,ptOptionsShowCount%>" /><asp:Label
                    ID="lblFormID" runat="server" Visible="false" Text=""></asp:Label>
        </div>
        <div align="center">
            <table width="100%" border="0" class="border">
                <tr>
                    <td align="left" class="tdbg">
                        <br />
                        &nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Poll,lblTotalOfUsers%>" /><b><%=alluser%>
                        </b>
                        <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Poll,Person%>" />，
                        <b>
                            <%=polluser%>
                        </b>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Poll,lblParticipation%>" />
                        <b>
                            <%=formuser%>
                        </b>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Poll,Person%>" />
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
                                                    &nbsp;
                                                    <%#DataBinder.Eval(Container.DataItem,"Title") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gridViewOption" runat="server" Width="100%" CellPadding="5" ForeColor="#363636"
                                                        AutoGenerateColumns="false" BorderWidth="0px" BorderColor="#d5d9d8" HeaderStyle-BackColor="#f6f6f6">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" HeaderStyle-Font-Bold="false"
                                                                HeaderText="<%$Resources:Poll,lblOption %>" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#((OptionList)Container.DataItem).name%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false"
                                                                HeaderStyle-HorizontalAlign="Center" HeaderText="<%$Resources:Poll,Proportion %>">
                                                                <ItemTemplate>
                                                                
                                                                 <asp:Image runat="server" ID="image" ImageUrl="/admin/images/poll.jpg" Height="20" Width="<%#FormatImage(FormatCount(((OptionList)Container.DataItem).count, ((OptionList)Container.DataItem).totalcount))%>" /> 
                                                                    <%#FormatCount(((OptionList)Container.DataItem).count, ((OptionList)Container.DataItem).totalcount)%>%
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="right" HeaderStyle-Font-Bold="false"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HeaderText="<%$Resources:Poll,PollCount %>">
                                                                <ItemTemplate>
                                                                    <%#((OptionList)Container.DataItem).count%>&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
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
            <input type="button" name="button1" value="<%$Resources:Site,btnBackText %>" class="adminsubmit_short"
                onclick="history.back()" runat="server">
        </div>
    </div>
</asp:Content>
