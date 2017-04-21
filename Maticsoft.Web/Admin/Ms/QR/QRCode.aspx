<%@ Page Title="二维码生成" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.QR.QRCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.autosize-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.dynatextarea.js" type="text/javascript"></script>
    <style type="text/css">
        .progress { float: right; width: 1px; height: 14px; color: white; font-size: 12px; overflow: hidden; background-color: navy; padding-left: 5px; }
        .OnlyNum { text-align: right; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="二维码生成" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以在此生成二维码" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist_title">
            <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3" class="borderkuang" id="Table2">
                <tr style="display: none;">
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        编码模式：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoMod" runat="server">
                            <asp:ListItem Selected="True" Value="QR" Text="QR编码" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        图片大小：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSize" CssClass="OnlyNum" runat="server" Text="120" Width="50" Height="21" />
                        px (正方形)
                    </td>
                </tr>
                <tr style="display: none;">
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        边缘间隙：
                    </td>
                    <td>
                        <asp:TextBox ID="txtMargin" CssClass="OnlyNum" runat="server" Text="0" Width="50" Height="21" />
                        值越大图片四周的间隙越大 (0:无间隙,1较小间隙. 以此类推) 标准值:4
                    </td>
                </tr>
                <tr style="display: none;">
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        容错比率：
                    </td>
                    <td>
                        <asp:DropDownList ID="drpFaultRate" runat="server">
                            <asp:ListItem Value="L" Text="低 (7%)" />
                            <asp:ListItem Value="M" Text="默认 (15%)" />
                            <asp:ListItem Value="Q" Text="中 (25%)" />
                            <asp:ListItem Selected="True" Value="H" Text="最高 (30%)" />
                        </asp:DropDownList>
                        百分比是模糊的字码可被正确识别比率, 百分比越大, 识别率越高
                    </td>
                </tr>
                <tr style="display: none;">
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        图片格式：
                    </td>
                    <td>
                        <asp:DropDownList ID="droImgFormat" runat="server">
                            <asp:ListItem Selected="True" Value="png" Text="PNG" />
                            <asp:ListItem Value="jpeg" Text="JPG" />
                            <asp:ListItem Value="gif" Text="GIF" />
                            <asp:ListItem Value="bmp" Text="BMP" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        二维码内容：
                    </td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" Width="476" TextMode="MultiLine" Rows="6" />
                        <span id="progressbar1" class="progress"></span>(最大984个字符)
                    </td>
                </tr>
                <tr>
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        手机版网址：
                    </td>
                    <td>
                        <asp:TextBox ID="txtWebsiteURL" runat="server" Width="476" Height="21" MaxLength="984" />
                    </td>
                </tr>
                <tr>
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                        安卓客户端：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAndroidURL" runat="server" Width="476" Height="21" MaxLength="984" />
                    </td>
                </tr>
                <tr>
                    <td height="18px;" style="width: 80px">
                    </td>
                    <td style="width: 80px">
                    </td>
                    <td>
                        <asp:Button ID="btnGen" runat="server" Text="生成" class="adminsubmit_short" OnClick="btnGen_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist_title" style="display: none;">
            <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3" class="borderkuang" id="Table3">
                <tr>
                    <td class="tdbg">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="td_class">
                                </td>
                                <td height="25">
                                    <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>" class="adminsubmit_short" OnClientClick="window.location.reload();return false;"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table id="QRIMG" border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3" class="borderkuang" id="Table1">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                </td>
                <td>
                    <div style="margin-right: 50px; float: left;">
                        <h2 style="width: 100%; text-align: center;">
                            手机版网址</h2>
                        <img class="qrImg" onerror="qrImgErr(this)" />
                    </div>
                    <div style="float: left">
                        <h2 style="width: 100%; text-align: center;">
                            安卓客户端</h2>
                        <img class="qrImg" onerror="qrImgErr(this)" />
                    </div>
                </td>
            </tr>
        </table>
        <table id="Result" border="0" cellspacing="1" style="display: none; width: 100%; height: 100%;" cellpadding="3" class="borderkuang" id="Table1">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    生成结果：
                </td>
                <td>
                    您可在图片上右键另存为
                    <%--或 <a style="color: red">点此将图片转换为base64编码数据,粘贴到&lt;img /&gt;的src中即可立即使用</a>--%>
                    <br />
                    <asp:Image ID="imgResult" runat="server" />
                    <br />
                    <input id="Base64" type="text" style="width: 476px; display: none;" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        $('textarea').autosize();
        $.dynatextarea($('[id$=txtContent]'), 984, $('#progressbar1'));
        $(".OnlyNum").OnlyNum();

        if ($('[id$=imgResult]').attr('src')) {
            $('#Result').show();
        }
    </script>
    <script type="text/javascript">
        $('.qrImg:eq(0)').attr('src', '/Upload/QR/website.png?r=' + Math.random());
        $('.qrImg:eq(1)').attr('src', '/Upload/QR/android.png?r=' + Math.random());

        function qrImgErr(sender) {
            $(sender).hide().parent().hide();
            if ($('.qrImg:visible').length == 0) {
                $('#QRIMG').hide();
            }
        }
    </script>
</asp:Content>
