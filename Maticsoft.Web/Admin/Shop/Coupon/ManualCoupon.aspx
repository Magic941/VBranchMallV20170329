<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="ManualCoupon.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Coupon.ManualCoupon" %>

<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            text-align: right;
            padding-bottom: 10px;
            padding-top: 10px;
        }
    </style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="auto-style1">
                            </td>
                            <td height="25">
                                <a href="/Upload/Template/ManualCoupon.xls" style=" color:Red; font-weight:bold">Excel模版下载 </a>
                            </td>
                        </tr>
                        <tr>
                           <td class="auto-style1">
                    Excel数据文件：
                </td>
                <td height="25">
                    <asp:FileUpload ID="uploadExcel" runat="server" CssClass="uploadExcel" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="uploadExcel"
                        runat="server" ErrorMessage="请选择正确的格式" ValidationExpression="^.+(xls|xlsx)$"></asp:RegularExpressionValidator>
                </td>
                        </tr>
                        
                        <tr class="Point">
                            <td class="auto-style1">
                                <asp:Literal ID="Literal17" runat="server" Text="优惠券分类" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlClass" runat="server" Width="124px">
                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="Point">
                            <td class="auto-style1">
                                <asp:Literal ID="LiteralBatch" runat="server" Text="发放批号" />：
                            </td>
                            <td height="25">
                               <asp:TextBox ID="txtBatch" runat="server" Width="250px" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="发放" OnClick="btnSave_Click" class="adminsubmit_short">
                                </asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" style="word-break:break-all;word-wrap:break-word" ForeColor="Red" Height="300px" Width="1200px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>