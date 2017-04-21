<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Advertisement.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script type="text/javascript">
      $(document).ready(function () {
          baseLoad();
          $("[id$='rbTextContent']").click(function () {
              baseLoad();
          });
          $("[id$='rbImgContent']").click(function () {
              baseLoad();
          });
          $("[id$='rbFlashContent']").click(function () {
              baseLoad();
          });
          $("[id$='rbCodeContent']").click(function () {
              baseLoad();
          });


      });
      function baseLoad() {
          if ($("[id$='rbTextContent']").attr("checked")) {
              $(".filePath").hide();
              $(".SwffilePath").hide();
              $(".advHtml").hide();
              $(".NavigateUrl").show();
              $(".AlternateText").show();
          }
          if ($("[id$='rbImgContent']").attr("checked")) {

              $(".filePath").show();
              $(".advHtml").hide();
              $(".SwffilePath").hide();
              $(".NavigateUrl").show();
              $(".AlternateText").show();
              $(".fileUrl").text("图片地址：");
          }
          if ($("[id$='rbFlashContent']").attr("checked")) {
              $(".filePath").hide();
              $(".SwffilePath").show();
              $(".advHtml").hide();
              $(".NavigateUrl").hide();
              $(".AlternateText").hide();
              $(".fileUrl").text("Flash地址：");
          }
          if ($("[id$='rbCodeContent']").attr("checked")) {
              $(".filePath").hide();
              $(".SwffilePath").hide();
              $(".advHtml").show();
              $(".NavigateUrl").hide();
              $(".AlternateText").hide();
          }
      }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="广告详细信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="查看广告详细信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_classshow">
                                广告编号 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAdvertisementId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                广告名称 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAdvertisementName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                广告位：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAdvPositionId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                展示方式：
                            </td>
                            <td height="25">
                                <asp:RadioButton ID="rbTextContent" runat="server" GroupName="AdType" Enabled="false" />文字
                                <asp:RadioButton ID="rbImgContent" runat="server" GroupName="AdType" Enabled="false" />图片
                                <asp:RadioButton ID="rbFlashContent" runat="server" GroupName="AdType" Enabled="false" />Flash
                                <asp:RadioButton ID="rbCodeContent" runat="server" GroupName="AdType" Enabled="false" />代码
                            </td>
                        </tr>
                        <tr class="filePath">
                            <td class="td_classshow"> <span class="fileUrl"></span></td>
                            <td height="25">
                            </td>
                        </tr>
                        <tr class="filePath">
                            <td class="td_classshow"></td>
                            <td height="25">
                                <asp:Image ID="Image1" runat="server" />
                            </td>
                        </tr>
                        
                        <tr class="SwffilePath">
                            <td class="td_classshow"> <span class="fileUrl"></span></td>
                            <td height="25">
                                <asp:Literal ID="litFlash" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr class="AlternateText">
                            <td class="td_classshow">  广告语：</td>
                            <td height="25">
                                <asp:Label ID="lblAlternateText" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr class="NavigateUrl">
                            <td class="td_classshow"> 链接地址：</td>
                            <td height="25">
                                <asp:Label ID="lblNavigateUrl" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="advHtml">
                            <td class="td_classshow" style="vertical-align:top;"> 广告HTML代码 ：</td>
                            <td height="25">
                                <asp:Label ID="lblAdvHtml" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow"> 显示频率 ：</td>
                            <td height="25">
                                <asp:Label ID="lblImpressions" runat="server"></asp:Label>
                                  <div style="margin-top:-23px;margin-left:90px;" class="timeintervalClass">
                                
                                <asp:RadioButton ID="rbStatusY" runat="server"  GroupName="status"  Enabled="false"/>有效</div>
                                <div style="margin-top:-21px;margin-left:145px;" class="timeintervalClass">
                                <asp:RadioButton ID="rbStatusN" runat="server"  GroupName="status" Enabled="false"/>无效</div>
                                <div style="margin-top:-21px;margin-left:195px;" class="timeintervalClass">
                                <asp:RadioButton ID="rbStop" runat="server"  GroupName="status" Enabled="false"/>欠费停止</div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">  创建时间 ： </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">创建人 ：</td>
                            <td height="25">
                                <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="td_classshow">状态: </td>
                            <td height="25">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="td_classshow">开始时间 ：</td>
                            <td height="25">
                                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">  结束时间 ： </td>
                            <td height="25">
                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">最大PV ：</td>
                            <td height="25">
                                <asp:Label ID="lblDayMaxPV" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">  最大IP ：</td>
                            <td height="25">
                                <asp:Label ID="lblDayMaxIP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">每千次展示价格 ：</td>
                            <td height="25">
                                <asp:Label ID="lblCPMPrice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">到期自动停止：</td>
                            <td height="25">
                                <asp:RadioButton ID="rbAutoStop" runat="server"  GroupName="IsAutoStop" Enabled="false"/>是
                                <asp:RadioButton ID="rbNoStup" runat="server" GroupName="IsAutoStop" Enabled="false"/>否
                                <asp:RadioButton ID="rbNoLimit" runat="server" GroupName="IsAutoStop" Enabled="false"/>无限制
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow"> 顺序 ：</td>
                            <td height="25">
                                <asp:Label ID="lblSequence" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">广告主 ：</td>
                            <td height="25">
                                <asp:Label ID="lblEnterpriseID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short"   
                                    OnClientClick="javascript:parent.$.colorbox.close();" onclick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
