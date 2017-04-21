<%@ Page Title="<%$ Resources:SysManage,ptCompanyInfoConfig%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="OperatorsInfo.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.OperatorsInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-timepicker-div .ui-widget-header{ margin-bottom: 8px; }
        .ui-timepicker-div{height:100px;}
        .ui-timepicker-div dl{ text-align: left; }
        .ui-timepicker-div dl dt{ height: 25px; }
        .ui-timepicker-div dl dd{ margin: -25px 0 10px 65px; }
        .ui-timepicker-div td { font-size: 90%; }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='txtBusinessHoursStart']").prop("readonly", true).timepicker({
                timeOnlyTitle: '选择时间',
                timeText: '时间',
                hourText: '小时',
                minuteText: '分钟',
                currentText: '现在',
                closeText: '完成',
                timeFormat: 'HH:mm'//格式化时间  
            });
            $("[id$='txtBusinessHoursEnd']").prop("readonly", true).timepicker({
                timeOnlyTitle: '选择时间',
                timeText: '时间',
                hourText: '小时',
                minuteText: '分钟', 
                currentText: '现在',
                closeText: '完成',
                timeFormat: 'HH:mm'//格式化时间  
            });
 
            $("[id$='txtServiceRadius']").OnlyFloat();
            $("[id$='txtSentPrices']").OnlyFloat();


        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="商家信息"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="设置商家基本信息，用于微信中展示"/>  
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
                            <td class="td_class ">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage,lblName%>" />
                                ：
                             
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Width="400" Height="21"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SysManage,lblAddress%>" />
                                ：
                             
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" Width="400" Height="30" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Litera8" runat="server" Text="<%$ Resources:SysManage,lblTelephone%>" />
                                ：
                             
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelephone" runat="server" Width="400" Height="30" ></asp:TextBox>
                            </td>
                        </tr>
                      

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:SysManage,lblBusinessHours%>" />
                                ：   
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBusinessHoursStart" runat="server" Width="70" Height="30"></asp:TextBox> - <asp:TextBox ID="txtBusinessHoursEnd" runat="server" Width="70" Height="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:SysManage,lblSentPrices%>" />
                                ：
                             
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSentPrices" runat="server" Width="400" Height="30"></asp:TextBox>元
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:SysManage,lblServiceRadius%>" />
                                ：
                             
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtServiceRadius" runat="server" Width="400" Height="30"></asp:TextBox>公里
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:SysManage,lblDeliveryArea%>" />
                                ：
                             
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDeliveryArea" runat="server" Width="400" Height="30"></asp:TextBox>
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
