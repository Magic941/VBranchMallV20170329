<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msgbox.aspx.cs" Inherits="Maticsoft.Web.Admin.js.msgbox.msgbox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/Admin/js/msgbox/css/msgbox.css" rel="stylesheet" />
    <script type="text/javascript" src="/Admin/js/msgbox/script/msgbox.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="警告" OnClick="Button1_Click" />
    </div>
    <br />
    <div>
        <asp:Button ID="Button2" runat="server" Text="操作失败" OnClick="Button2_Click" />
    </div>
    <br />
    <div>
        <asp:Button ID="Button3" runat="server" Text="操作成功！" OnClick="Button3_Click" />
    </div>
    <br />
    <div>
        <asp:Button ID="Button4" runat="server" Text="正在加载中..." OnClick="Button4_Click" />
    </div>
    </form>
</body>
</html>
