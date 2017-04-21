
<%@ Page Title="查看配送方式" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ShowShippingType.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Shipping.ShowShippingType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr >
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="查看配送方式" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行查看配送方式操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">支付方式</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:;">地区价格</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                <tr style="display:none;">
                                    <td class="td_class">
                                        <asp:Literal ID="Literal5" runat="server" Text="物流公司" />：
                                    </td>
                                    <td height="25">
                                           <asp:Literal ID="lblCompanyName" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                         <asp:Literal ID="Literal8" runat="server" Text="配送方式名称" />：
                                    </td>
                                    <td height="25">
                                      <asp:Literal ID="lblName" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal3" runat="server" Text="起步重量" />：
                                    </td>
                                    <td height="25">
                                       <asp:Literal ID="lblWeight" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal4" runat="server" Text="加价重量" />：
                                    </td>
                                    <td height="25">
                                          <asp:Literal ID="lblAddWeight" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal9" runat="server" Text="起步价" />：
                                    </td>
                                    <td height="25">
                                             <asp:Literal ID="lblPrice" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal10" runat="server" Text="加价" />：
                                    </td>
                                    <td height="25">
                                                 <asp:Literal ID="lblAddPrice" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        <asp:Literal ID="Literal7" runat="server" Text="描述" />：
                                    </td>
                                    <td height="50">
                                          <asp:Literal ID="lblDesc" runat="server" Text="" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">
                        </td>
                        <td height="25">
                            <asp:CheckBoxList ID="ckPayType" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="myTab1_Content2" tabindex="2" class="none4">
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="td_class">
                            <asp:Literal ID="Literal21" runat="server" Text="支付方式" />：
                        </td>
                        <td height="25">
                            <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

