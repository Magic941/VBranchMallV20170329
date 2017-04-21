<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Settings.MainMenus.Add" Title="<%$Resources:CMS,WMptAdd%>" %>
  <%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var selectValue = $("#ctl00_ContentPlaceHolder1_ddlType").val();
            if (selectValue == "CMS") {
                $("#ctl00_ContentPlaceHolder1_ddNavType").find('option').each(function () {
                    var navtype = $(this).val();
                    if (navtype == 2 || navtype == 3 || navtype == 4) {
                        $(this).hide();
                    }
                    else {
                        $(this).show();
                    }
                });
            }
            //SNS
            if (selectValue == "SNS") {
                $("#ctl00_ContentPlaceHolder1_ddNavType").find('option').each(function () {
                    var navtype = $(this).val();
                    if (navtype == 1 || navtype == 4) {
                        $(this).hide();
                    }
                    else {
                        $(this).show();
                    }
                });
            }
            //Shop
            if (selectValue == "Shop") {

                $("#ctl00_ContentPlaceHolder1_ddNavType").find('option').each(function () {
                    var navtype = $(this).val();
                    if (navtype == 1 || navtype == 2 || navtype == 3) {
                        $(this).hide();
                    }
                    else {
                        $(this).show();
                    }
                });
            }
            var selectNavtype = $("#ctl00_ContentPlaceHolder1_ddNavType").val()
            if (selectNavtype == 0) {
                $("#ctl00_ContentPlaceHolder1_txtNavURL").show();
                $("#ctl00_ContentPlaceHolder1_ddValue").hide();
            } else {
                $("#ctl00_ContentPlaceHolder1_txtNavURL").hide();
                $("#ctl00_ContentPlaceHolder1_ddValue").show();
            }

            $("#ctl00_ContentPlaceHolder1_ddlType").click(function () {
                var value = $(this).val();
                $("#ctl00_ContentPlaceHolder1_ddNavType").val("0");
                $("#ctl00_ContentPlaceHolder1_txtNavURL").show();
                $("#ctl00_ContentPlaceHolder1_ddValue").hide();
                //CMS 
                if (value == "CMS") {
                    $("#ctl00_ContentPlaceHolder1_ddNavType").find('option').each(function () {
                        var navtype = $(this).val();
                        if (navtype == 2 || navtype == 3 || navtype == 4) {
                            $(this).hide();
                        }
                        else {
                            $(this).show();
                        }
                    });
                }
                //SNS
                if (value == "SNS") {
                    $("#ctl00_ContentPlaceHolder1_ddNavType").find('option').each(function () {
                        var navtype = $(this).val();
                        if (navtype == 1 || navtype == 4) {
                            $(this).hide();
                        }
                        else {
                            $(this).show();
                        }
                    });
                }
                //Shop
                if (value == "Shop") {
                    $("#ctl00_ContentPlaceHolder1_ddNavType").find('option').each(function () {
                        var navtype = $(this).val();
                        if (navtype == 1 || navtype == 2 || navtype == 3) {
                            $(this).hide();
                        }
                        else {
                            $(this).show();
                        }
                    });
                }
            });

            $("#ctl00_ContentPlaceHolder1_ddNavType").click(function () {
                var value = $(this).val();

                if (value == 0) {
                    $("#ctl00_ContentPlaceHolder1_txtNavURL").show();
                    $("#ctl00_ContentPlaceHolder1_ddValue").hide();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txtNavURL").hide();
                    $("#ctl00_ContentPlaceHolder1_ddValue").show();
                }
            });
        });
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,WMptAdd%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,WMlblAdd%>" />
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
                                <asp:Literal ID="Literal9" runat="server" Text="类型" />：
                            </td>
                            <td height="25">
                             <cc1:ConfigAreaList ID="ddlType" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlType_Change"   />
                                模板名称：
                                    <asp:DropDownList ID="ddlTheme" runat="server">
                              </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,WMenuName%>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtMenuName" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,WMTitleName%>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtTile" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblPageUrl%>" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddNavType" runat="server" OnSelectedIndexChanged="ddNavType_Change"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">自定义</asp:ListItem>
                                    <asp:ListItem Value="1">CMS栏目分类</asp:ListItem>
                                    <asp:ListItem Value="2">社区商品分类</asp:ListItem>
                                    <asp:ListItem Value="3">社区图片分类</asp:ListItem>
                                    <asp:ListItem Value="4">商城商品分类</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddValue" runat="server" Visible="false">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtNavURL" runat="server" Width="280px" Visible="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblShowID%>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtSqueeze" runat="server" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:CMS,WMlblTarget%>" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:DropDownList ID="ddlTarget" runat="server">
                                    <asp:ListItem Value="0" Text="<%$Resources:CMS,WMlblSelf %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:CMS,WMlblBlank %>"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:CMS,WMIsAvailable%>" />：
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkIsUsed" Text="" runat="server" Checked="False" class="checkbox_class" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
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
