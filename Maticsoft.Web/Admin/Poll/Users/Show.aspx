
<%@ Page Language="C#" MasterPageFile="~/admin/Basic.Master" AutoEventWireup="true"
    Codebehind="Show.aspx.cs" Inherits="Maticsoft.Web.Users.Show" Title="<%$REsources:Poll,ptOptionsShow%>" %>
<%@ MasterType VirtualPath="~/admin/Basic.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$REsources:Poll,ptOptionsShow%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$REsources:Poll,lblOptionsShow%>"/>
                    </td>
                </tr>
            </table>
        </div>
        <table cellspacing="0" cellpadding="5" width="100%" border="0" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="5" width="100%" >
                        <tr>
                            <td  height="22" align="left" class="style1">
                               <asp:Literal ID="Literal3" runat="server" Text="<%$REsources:Poll,lblPollResults%>"/>
                            </td>
                        </tr>
                        <tr>
                            <td height="22">
                             <table cellspacing="0" cellpadding="5" width="100%" border="0">
                                <asp:Repeater ID="rptVote" runat="server" OnItemDataBound="rptVote_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <th style="text-align: left">
                                                <b>
                                                    <%# GetTopicTitle((int)DataBinder.Eval(Container, "DataItem.TopicID"))%></b>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="left" bgcolor="White">
                                                <%# GetOptionName((int)DataBinder.Eval(Container, "DataItem.TopicID"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
<%--    <uc1:copyright ID="Copyright1" runat="server" />
    <uc2:checkright ID="Checkright1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
