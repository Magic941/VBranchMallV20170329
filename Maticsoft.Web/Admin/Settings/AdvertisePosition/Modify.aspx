<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.AdvertisePosition.Modify"
    Title="修改页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     $(function () {
         Dispaly("[id$='ddlShowType']");
         $("[id$='ddlShowType']").change(function () {
             if ($(this).val() == "0" || $(this).val() == "2" || $(this).val() == "3") {
                 $(".horizontalClass").hide();
                 $(".codeClass").hide();
                // $(".verticalClass").show();
                 $(".loopshow").show();
             } if ($(this).val() == "1") {
                 $(".horizontalClass").show();
                 $(".codeClass").hide();
                 $(".loopshow").show();
             }
             if ($(this).val() == "4") {
                 //$(".verticalClass").hide();
                 $(".horizontalClass").hide();
                 $(".codeClass").show();
                 $(".verticalClass").hide();
                 $(".loopshow").hide();
             }
         });

         $("[id$='chkIsOne']").change(function () {
             if ($(this).attr("checked")) {
                 $(".timeintervalClass").show();
             } else {
                 $(".timeintervalClass").hide();
             }
         });
     });
     function Dispaly(sender) {
         if ($(sender).val() == "0" || $(sender).val() == "2" || $(sender).val() == "3") {
             $(".horizontalClass").hide();
             $(".codeClass").hide();
             $(".timeintervalClass").hide();
            // $(".verticalClass").show();
             $(".loopshow").show();
         } if ($(sender).val() == "1") {
             $(".horizontalClass").show();
             $(".codeClass").hide();
             $(".loopshow").show();
         } if ($(sender).val() == "4") {
             $(".verticalClass").hide();
             $(".horizontalClass").hide();
             $(".codeClass").show();
             $(".loopshow").hide();
         }
         if ($("[id$='chkIsOne']").attr("checked")) {
             $(".timeintervalClass").show();
         } else {
             $(".timeintervalClass").hide();
         }
     }
    </script>
    
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="广告位信息编辑" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="广告位信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                广告位编号 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAdvPositionId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class"> 广告位名称 ：</td>
                            <td height="25">
                                <asp:TextBox ID="txtAdvPositionName" runat="server" Width="265px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">显示类型：</td>
                            <td height="25">
                                <asp:DropDownList ID="ddlShowType" runat="server">
                                <asp:ListItem Value="0">纵向平铺</asp:ListItem>
                                <asp:ListItem Value="1">横向平铺</asp:ListItem>
                                <asp:ListItem Value="2">层叠显示</asp:ListItem>
                                <asp:ListItem Value="3">交替显示</asp:ListItem>
                                <asp:ListItem Value="4">自定义广告位</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="horizontalClass">
                            <td class="td_class">行显示个数 ：</td>
                            <td height="25">
                                <asp:TextBox ID="txtRepeatColumns" runat="server" Width="265px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr   class="verticalClass" style=" display:none">
                            <td class="td_class">广告位大小 ： </td>
                            <td height="25">
                                <asp:TextBox ID="txtWidth" runat="server" Width="119px"></asp:TextBox>&nbsp;<b style="vertical-align:middle">×</b> <asp:TextBox ID="txtHeight" runat="server" Width="119px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="loopshow">
                            <td class="td_class"></td>
                            <td height="35">
                                <asp:CheckBox ID="chkIsOne" Text="循环显示" runat="server" Checked="False" />
                                
                            <div style="margin-top:-22px;margin-left:157px;" class="timeintervalClass"><span style="vertical-align:middle"> 循环间隔：</span>
                                <asp:TextBox ID="txtTimeInterval" runat="server" Width="40px"></asp:TextBox></div>
                            </td>
                        </tr>
                        <tr class="codeClass">
                            <td class="td_class">广告位内容 ：</td>
                            <td style="height: 150px;float: left;">
                                <asp:TextBox ID="txtAdvHtml" runat="server" Width="460px" TextMode="MultiLine" Height="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"  OnClientClick="javascript:parent.$.colorbox.close();"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
        <script type="text/javascript">
            var editor = new baidu.editor.ui.Editor({//实例化编辑器
                iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', '|', 'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                'justifyleft', 'justifycenter', 'justifyright', '|', 'removeformat', '|', 'pasteplain', '|', 'link', 'unlink', '|', 'insertimage']
                 ],
                initialContent: '', autoHeightEnabled: false,
                minFrameHeight: 70,
                pasteplain: false
         , wordCount: false
         ,sourceEditor:true
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
            });
            //editor.render('ctl00_ContentPlaceHolder1_txtAdvHtml'); //将编译器渲染到容器
        </script>
</asp:Content>
