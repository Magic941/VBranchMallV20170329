<%@ Page Title="淘宝客设置" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ViewConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.ViewConfig.ViewConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        界面设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        界面信息设置
                    </td>
                </tr>
            </table>
        </div>

        <asp:HiddenField ID="HiddenField_ID" runat="server" />
        <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                    <tr>
                           <td class="td_class"  style=" width:250px">
                                专辑、商品、图片评论每页展示数量：
                            </td>
                            <td height="25">
              
                                  <asp:DropDownList ID="ddlCommentDataSize" runat="server" Height="28px" 
                                      Width="293px">
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList>
                              
                            </td>
                        </tr>
                      <tr>
                            <td class="td_class"  style=" width:250px">
                                全站瀑布流初始展示数量：
                            </td>
                            <td height="25">
              
                                  <asp:DropDownList ID="ddlFallInitDataSize" runat="server" Height="28px" 
                                      Width="293px">
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList>
                              
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class"  style=" width:250px">
                                全站瀑布流最大加载数量：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFallDataSize" runat="server" Height="28px" 
                                    Width="293px">
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList>
                           
                            </td>
                        </tr>
                        <tr>
                           <td class="td_class"  style=" width:250px">
                               个人中心动态每页展示数量：
                            </td>
                            <td>
                              <asp:DropDownList ID="ddlPostDataSize" runat="server" Height="28px" Width="293px">
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList>
                              
                            </td>
                        </tr>
                        


                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
