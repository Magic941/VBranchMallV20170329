<%@ Page  Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="BindUser.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.User.BindUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(document).ready(function () { $("[id$='ddUser']").select2(); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信用户绑定管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行绑定网站用户操作" />
                    </td>
                </tr>
            </table>
        </div>
                <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="网站用户" />：
                            </td>
                            <td height="25">
                          <asp:DropDownList ID="ddUser" runat="server">
                            </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>


