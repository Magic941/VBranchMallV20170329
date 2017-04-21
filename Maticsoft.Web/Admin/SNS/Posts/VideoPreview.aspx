<%@ Page Title="视频预览" Language="C#" AutoEventWireup="true" CodeBehind="VideoPreview.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.PostsVideo.VideoPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <title><asp:Literal ID="ltlTitle" runat="server" Text="视频预览"></asp:Literal> ：</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                                            width="760" height="589">
                                            <param name="wmode" value="opaque" />
                                            <param name="movie" value="<%=localVideoUrl %>" />
                                            <param name="quality" value="high" />
                                            <embed id="Test2" src="<%=localVideoUrl %>" allowfullscreen="true"
                                    quality="high" width="730" height="500" align="middle" wmode="transparent" allowscriptaccess="always"
                                    type="application/x-shockwave-flash"></embed></object>
        </center>
    </div>
    </form>
</body>
</html>