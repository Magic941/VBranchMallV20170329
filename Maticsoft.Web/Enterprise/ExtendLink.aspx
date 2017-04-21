<%@ Page Title="" Language="C#" MasterPageFile="~/Enterprise/Basic.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="ExtendLink.aspx.cs" Inherits="Maticsoft.Web.Enterprise.ExtendLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function copy_code() {
            var copyText = $("[id$='txtUrl']").val();
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
        <div class="admintitle">
            <div class="sj" style="margin-right: 20px;">
                <img src="/images/icon6.gif" width="21" height="28" /></div>
            <strong id="TitleText">推广链接</strong>
        </div>
        <div class="newsadd_title" id="ISshow">
            <ul>
                <li style="width: 30%; text-align: right">推广地址：</li>
                <li style="text-align: left">
                    <asp:TextBox ID="txtUrl" runat="server" Width="350px"></asp:TextBox>
                </li>
            </ul>
            <ul>
                <li style="width: 30%; text-align: right"></li>
                <li style="text-align: left">
                    <input id="btnCopy" type="button" value="复制链接" onclick="copy_code()" class="adminsubmit" />
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
