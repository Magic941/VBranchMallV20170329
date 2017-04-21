<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.ClassType.EditInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Admin/css/admin.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/MasterPage1.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/Admin/js/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/msgbox/script/msgbox.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $("[id$='btnSave']").click(function () {
                    if ($("[id$='txtClassTypeName']").val() == "" || $("[id$='txtClassTypeName']").val() == undefined || $("[id$='txtClassTypeName']").val() == null) {
                        clickautohide(5, "<%=Resources.CMS.ClasstooltipEnterName %>", 2000);
                        return false;
                    }
                });
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,ClassptModify %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,ClasslblModify %>" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <asp:HiddenField ID="HiddenField_ID" runat="server" />
                        <tr id="modify" runat="server" style="display:none;">
                            <td class="td_class">
                               <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ClasslblClassID %>" />：
                            </td>
                            <td>
                                <asp:Label ID="lblClassTypeID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,ClasslblClassName %>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtClassTypeName" runat="server" Width="200px" class="addinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                     class="adminsubmit_short" onclick="btnSave_Click" ></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClientClick="javascript:parent.$.colorbox.close();" class="adminsubmit_short" ></asp:Button><%--OnClientClick="javascript:parent.$.fancybox.close();"--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
