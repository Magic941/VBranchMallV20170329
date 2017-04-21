<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="GetAdJs.aspx.cs" Inherits="Maticsoft.Web.Admin.AdvertisePosition.GetAdJs"
    Title="增加页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            function copy_code() {
                var copyText = $("[id$='txtJs']").val();
                if (window.clipboardData) {
                    window.clipboardData.setData("Text", copyText)
                }
                else {
                    var flashcopier = 'flashcopier';
                    if (!document.getElementById(flashcopier)) {
                        var divholder = document.createElement('div');
                        divholder.id = flashcopier;
                        document.body.appendChild(divholder);
                    }
                    document.getElementById(flashcopier).innerHTML = '';
                    var divinfo = '<embed src="/Scripts/_clipboard.swf" FlashVars="clipboard=' + encodeURIComponent(copyText) + '" width="0" height="0" type="application/x-shockwave-flash"></embed>';
                    document.getElementById(flashcopier).innerHTML = divinfo;
                }
                alert('复制成功！');
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="您的广告位代码如下所示" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以<asp:Literal ID="Literal3" runat="server" Text="您可以将下面的代码复制到要添加广告的位置" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr class="horizontalClass">
                            <td height="25" align=center>
                                <asp:TextBox ID="txtJs" runat="server" Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="30"><br />
                                     <input id="btnCopy" type="button" value="复制" onclick="copy_code()" class="adminsubmit" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>