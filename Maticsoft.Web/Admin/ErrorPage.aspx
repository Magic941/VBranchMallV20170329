<%@ Page Title="系统提示" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ErrorPage.aspx.cs" Inherits="Maticsoft.Web.Admin.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="user_border" cellspacing="0" cellpadding="0" width="100%" align="center"
        border="0" id="table1">
        <tr>
            <td valign="top">
                <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                    <tr>
                        <td align="left">                        
                            <span style="font-size: 12pt; font-weight: bold; color: #3666AA">
                                <img src="/admin/images/warning.png" align="absmiddle" style="border-width: 0px;" />
                                <asp:Literal ID="Literal1" runat="server" Text="系统提示" /></span>
                        </td>
                        <td align="center">
                            <table align="left" id="table3">
                                <tr valign="top" align="left">
                                    <td width="80">
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg" style=" padding-left:20px; padding-top:10px; padding-bottom:20px">
               <%=ErrorMessage %>                
          </td>
        </tr>        
    </table>
    <br />
    <div style=" text-align:center">
    <asp:Button ID="btnCancle" runat="server" CausesValidation="false"  Text="<%$ Resources:Site, btnBackText %>"
                    OnClientClick="javascript:history.go(-1);return false;" class="adminsubmit"></asp:Button>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
