<%@ Page Title="<%$ Resources:CMSVideo, ptVideoQuickPreview %>" Language="C#" AutoEventWireup="true" CodeBehind="VideoPreview.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Video.VideoPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <title><asp:Literal ID="ltlTitle" runat="server" Text="<%$ Resources:CMSVideo, ptVideoQuickPreview %>"></asp:Literal> ：</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <script src="/Tools/flowplayer/flowplayer-3.2.11/flowplayer-3.2.10.min.js" type="text/javascript"></script>
            <a href="/UploadFolder/<%=localVideoUrl %>" style="display: block; width: 550px;
                height: 380px" id="player"></a>
            <script type="text/javascript">
                flowplayer("player", "/Tools/flowplayer/flowplayer-3.2.11/flowplayer-3.2.11.swf");
            </script>
        </center>
    </div>
    </form>
</body>
</html>
