
<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Dimission.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.Dimission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!--Select2 Start-->
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='dropParentUser']").select2({ placeholder: "可选项" });
        });
        
    </script>
    <!--Select2 End-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="入职管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                         <asp:Literal ID="Literal2" runat="server" Text="您可以对用户操作 进行入职" />
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
                                <asp:Literal ID="Literal10" runat="server" Text="用户Id" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="litID" runat="server" Text="" />
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="用户名" />：
                            </td>
                            <td height="25">
                               <asp:Literal ID="litUserName" runat="server" Text="" />
                            </td>
                        </tr>
                            <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="电话" />：
                            </td>
                            <td height="25">
                               <asp:Literal ID="litPhone" runat="server" Text="" />
                            </td>
                        </tr>
                         </tr>
                            <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="性别" />：
                            </td>
                            <td height="25">
                               <asp:Literal ID="litSex" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="所在公司" />：
                            </td>
                            <td>
                                  <asp:Literal ID="litcommpany" runat="server" Text="" />

                            </td>
                          
                           
                           
                        </tr>
        
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="离职"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                         
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
