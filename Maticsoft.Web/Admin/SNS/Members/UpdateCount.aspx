
<%@ Page Title="更新统计数量" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="UpdateCount.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.UpdateCount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="更新统计数量" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="更新统计数量" />
                    </td>
                </tr>
            </table>
        </div>
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                 <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="FansCkeck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal3" runat="server" Text="更新用户的粉丝数据" /></b>
                    </td>
                         <td></td>
                </tr>
                <tr>
                   <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="ShareCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal4" runat="server" Text="更新用户的分享数据" /></b>
                    </td>
                                   
                </tr>
                <tr>
                   <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="ProductCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal5" runat="server" Text="更新用户的商品数据" /></b>
                    </td>
                                        
                </tr>
                <tr>
                   <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="FavouritesCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal6" runat="server" Text="更新用户的喜欢数据" /></b>
                    </td>
                                        
                </tr>
                <tr>
                    <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="AblumsCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal7" runat="server" Text="更新用户的专辑数据" /></b>
                    </td>
                                        
                </tr>
                <tr>
                <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="HotStarCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal8" runat="server" Text="更新明星达人排行" /></b>
                    </td>
                                        
                </tr>
                <tr>
                  <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="ShareProductCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal9" runat="server" Text="更新晒货达人排行" /></b>
                    </td>
                                        
                </tr>

                <tr>
                   <td style="width: 12px" align="right" class="tdbg">
                     <asp:CheckBox ID="CollocationCheck" runat="server"  />
                    </td>
                    <td style="width: 150px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal10" runat="server" Text="更新搭配达人排行" /></b>
                    </td>
                                        
                </tr>
                <tr>
                 <td style="width: 12px" align="right" class="tdbg">
                    </td>
                    <td class="tdbg"   >                    
                    <asp:Button ID="Button5" runat="server" Text="全部更新" OnClick="btnAll_Click"
                    class="adminsubmit" />                        
                    </td>                    
                </tr>
                 <tr>
                  <td style="width: 150px" align="right" class="tdbg">
                        
                    </td>
                    <td style="text-align:left; padding-left:20px" class="tdbg">
                <asp:Label ID="Label1" runat="server" ForeColor="Green"></asp:Label>
                </td>
                </tr>
            </table>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

