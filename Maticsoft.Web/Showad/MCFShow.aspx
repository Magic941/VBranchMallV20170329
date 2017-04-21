<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MCFShow.aspx.cs" Inherits="Maticsoft.Web.Showad.MCFShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
</head>
<body id="<%= DateTime.Now.ToString("yyyyMMddHHmmssffffff") %>" oncontextmenu="window.event.returnValue=false" style="overflow: hidden; overflow-y: hidden; overflow-x: hidden; width: <%=strW%>px; height: <%=strH%>px;" leftmargin="0" topmargin="0">
    <%=strADContentHtml%>
    <script language="javascript" type="text/javascript">
        (function () {
            //            width:<%=strW%>px;height:px;
            $(window.top.document).find('iframe').each(function () {
                var target = $(this).contents().find("#" + $('body').attr('id'));
                if (target && target.length > 0) {
                    $(this).height('<%=strH%>');
                    $(this).width('<%=strW%>');
                }
            });
        })();
    </script>
</body>
</html>
