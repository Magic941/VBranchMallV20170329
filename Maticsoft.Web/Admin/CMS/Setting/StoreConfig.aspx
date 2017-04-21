<%@ Page Title="图片存储设置" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="StoreConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Setting.StoreConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {

            var value = $("#ctl00_ContentPlaceHolder1_thumbList").val();
            if (value != "" && value != undefined) {
                var arry = value.split(",");
                $.each(arry, function (i, n) {
                    var html = "<tr class='thumbSize'><td class='td_class'>  缩略图标识符：</td><td width='80' class='thumb'>{0}</td><td class='td_class'> 对应云存储后缀名：</td>" +
                    " <td width='160'><input class='cloud' type='text' value='{1}' /> </td><td></td></tr>";
                    html = html.format(n.split("&")[0], n.split("&")[1]);
                    $("#txtThumb").append(html);
                });
            }
            

            $("#ctl00_ContentPlaceHolder1_rdtLocal").change(function () {
                Init();
            });
            $("#ctl00_ContentPlaceHolder1_rdtWeb").change(function () {
                Init();
            });
            Init();
        });

        function Init() {
            if ($("#ctl00_ContentPlaceHolder1_rdtLocal").prop("checked")) {
                $(".webprop").hide();
                $("#txtThumb").hide();
            } else {
                $(".webprop").show();
                $("#txtThumb").show();
            }

        }
        function SubForm() {
            var thumblist = "";
            var i = 0;
            $(".thumbSize").each(function () {
                var thumb = $(this).find(".thumb").text();
                var cloud = $(this).find(".cloud").val();
                if (i == 0) {
                    thumblist = thumb + "&" + cloud;
                } else {
                    thumblist = thumblist + "," + thumb + "&" + cloud;
                }
                i++;
            });
            $("#ctl00_ContentPlaceHolder1_thumbList").val(thumblist);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:HiddenField ID="thumbList" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       相册图片存储位置设置
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        设置网站相册图片上传存储的位置信息。
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%; padding-top: 10px" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <div class="newsadd_title">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="">
                            <tr>
                                <td bgcolor="#FFFFFF" class="newstitle">
                                    相册图片存储设置
                                </td>
                            </tr>
                        </table>
                        <div class="member_info_show">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                
                                <tr>
                                    <td class="td_class" Width="88">
                                        上传图片存储：
                                    </td>
                                    <td>
                                      <asp:RadioButton ID="rdtLocal" GroupName="StoreWay" runat="server" Text="本地存储" />  <asp:RadioButton ID="rdtWeb" runat="server"  Text="又拍云存储"  GroupName="StoreWay"/>
                                    </td>
                                    <td></td>
                                </tr>
                                

                                <tr class="webprop">
                                    <td class="td_class">
                                        空间名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSpaceName" runat="server" Width="240" Height="21"></asp:TextBox>
                                     
                                    </td>
                                 <td  rowspan="4" style=" float: left">
                                        <a style="color: rgb(68, 68, 68); font-family: Simsun; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); display: inline !important; float: none;">
                                        又拍云存储是通用的大规模存储服务。为网站提供静态文件存储+CDN加速的服务，静态文件主要是指图片，APP，音频，视频，小文件等等服务。<a href="https://www.upyun.com/?s=maticsoft"  style="color: blue" target="_blank">点击申请</a></td>
                                </tr>
                                <tr  class="webprop">
                                    <td class="td_class">
                                       操作员名称：
                                    </td>
                                    <td>
                                       <asp:TextBox ID="txtOperaterName" runat="server" Width="240" Height="21"></asp:TextBox>
                                    </td>
                                </tr>
                                      <tr  class="webprop">
                                    <td class="td_class">
                                       操作员密码：
                                    </td>
                                    <td style="  color:Gray">
                                           <asp:TextBox ID="txtOperaterPassword"  runat="server" Width="240" Height="21" TextMode="Password"></asp:TextBox>
                                      
                                    </td>
                                    <td></td>
                                </tr>
                                
                                
                                  <tr  class="webprop">
                                    <td class="td_class">
                                       访问域名：
                                    </td>
                                    <td style="  color:Gray">
                                           <asp:TextBox ID="txtPhotoDomain" runat="server" Width="240" Height="21"></asp:TextBox>
                                      
                                    </td>
                                    <td></td>
                                </tr>

                            </table>
                                         <br />
                            <table cellspacing="0" cellpadding="3" width="100%" border="0" id="txtThumb">
                                <tr>
                                    <td bgcolor="#FFFFFF" class="newstitle">
                                        缩略图路径对应：
                                    </td>
                                    <td>
                                     
                                    </td>
                                    <td colspan="5">
                                           Tip  对应云存储后缀名表示： 填写您在又拍云管理台定义的缩略图后缀名，不填将使用原图。 注：缩略图的名字请不要包含特殊字符，比如 ","  "&" 等。
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    
                    <table style="width: 100%; border: none; float: left;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        
            <tr>
                <td class="td_class">
                </td>
                <td height="25">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>" class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>" class="adminsubmit_short" OnClick="btnReset_Click" Visible="False"></asp:Button>
                </td>
            </tr>
                    </table>
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
