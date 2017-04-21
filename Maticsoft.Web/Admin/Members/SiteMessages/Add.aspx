<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.SiteMessages.Add"
    Title="增加页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {

            $("#ctl00_ContentPlaceHolder1_chkRoleList_0").click(
                function () {
                    $(":checkbox").not($(this)).each(
                        function () {
                            if ($("#ctl00_ContentPlaceHolder1_chkRoleList_0").attr("checked") == true) {
                                $("#ctl00_ContentPlaceHolder1_txtUser").hide();
                                $(this).attr("checked", false);
                                $(this).attr("disabled", true);
                            } else {
                                $(this).attr("disabled", false);
                                $("#ctl00_ContentPlaceHolder1_txtUser").show();
                            }
                        });
                });
        })
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="增加系统消息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%; border-bottom: none; border-top: none;" cellpadding="2"
            cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                  <asp:RadioButton ID="rbUserType" runat="server" GroupName="UserType"  Checked="True" OnCheckedChanged="UserType_Changed" AutoPostBack="True"/> 用户类型 ：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlUserType" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                 <asp:RadioButton ID="rbUser" runat="server" GroupName="UserType"  OnCheckedChanged="UserType_Changed"  AutoPostBack="True"/>  指定用户 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUser" runat="server" Width="480px" Enabled="False"></asp:TextBox>（添用户名,多个用;隔开）
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                标题 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                内容 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtContent" runat="server" Width="600px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <table style="width: 100%; border-top: none; float: left;" cellpadding="2" cellspacing="1"
                class="border">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td style="height: 6px;">
                                </td>
                                <td height="6">
                                </td>
                            </tr>
                            <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Button ID="btnSave" runat="server" Text="发送" class="adminsubmit_short" OnClick="btnSave_Click">
                                    </asp:Button>
                                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                        class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </table>
        <br />
    </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>
