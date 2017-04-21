<%@ Page Title="<%$ Resources:SysManage,ptCompanyInfoConfig%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="CompanyInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.CompanyInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <script src="../js/jquery.autosize.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id$='txtDes']").autosize();
        });
    </script>--%>
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.uploadify/uploadify-v2.1.0/swfobject.js" type="text/javascript"></script>
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"  type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.img.min.js" type="text/javascript"></script>

    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
         
            if ($("[id$='imgLogo']").attr("src")) {
                $("[id$='imgLogo']").attr("ref", $("[id$='imgLogo']").attr("src"));
                $("[id$='imgLogo']").removeAttr("src");
                $.scaleLoad('#pic_load', 200, 200);
            }
            //            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/WebLogo.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,
                'width': 76,
                'height': 25,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },
                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        $("[id$='imgLogo']").attr("ref", response.split('|')[1].format(''));
                        $("[id$='imgLogo']").removeAttr("src");
                        $("[id$='imgLogo']").removeAttr('loaded');
                        $.scaleLoad('#pic_load', 200, 200);
                        $("[id$='hfs_ICOPath']").val(response.split('|')[1]);
                    } else {
                        alert("图片上传失败！");
                    }
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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,ptCompanyInfoConfig%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage,lblCompanyInfoConfig%>"/>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HiddenField_ID" runat="server" />
        <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:SysManage,lblWebSiteLogo%>" />
                                ：
                             
                            </td>
                            <td height="25">
                                <asp:HiddenField ID="hfs_ICOPath" runat="server" />
                                <div id="fileQueue">
                                </div>
                                <input type="file" name="uploadify" id="uploadify" />
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SysManage,lblWerSitePrompt%>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25" id="pic_load">
                                <asp:Image ID="imgLogo" runat="server" Width="120" Height="120" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:SysManage,lblWebSiteDomain%>" />
                                ：
                             
                            </td>
                            <td>
                              <%-- <b style="font-size:16px;vertical-align:middle;"> </b>--%>
                                <asp:TextBox ID="txtWebSiteDomain" runat="server" Width="400" Height="21" Text="http://"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage,lblCompanyName%>" />
                                ：
                             
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanyName" runat="server" Width="400" Height="21"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SysManage,lblCompanyAddress%>" />
                                ：
                             
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanyAddress" runat="server" Width="400" Height="30" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Litera8" runat="server" Text="<%$ Resources:SysManage,lblCompanyTelephone%>" />
                                ：
                             
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanyTelephone" runat="server" Width="400" Height="30" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:SysManage,lblCompanyFax%>" />
                                ：
                             
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtCompanyFax" runat="server" Width="400" Height="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:SysManage,lblCompanyMail%>" />
                                ：
                             
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtCompanyMail" runat="server" Width="400" Height="30"></asp:TextBox>
                            </td>
                        </tr>
                        
                         
                        
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click" ></asp:Button>
                                <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>" 
                                    class="adminsubmit_short" OnClick="btnReset_Click"></asp:Button>
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
