
<%@ Page Title="微信渠道推广场景管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
 CodeBehind="DetailCount.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Scene.DetailCount" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/Highcharts/highcharts.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#txtFrom").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#txtTo").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#txtTo").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#txtFrom").datepicker("option", "maxDate", selectedDate);
                    $("#txtTo").val($(this).val());
                }
            });



            $("#btnGetCount").click(function () {
                var txtFrom = $("#txtFrom").val();
                var txtTo = $("#txtTo").val();
                var sceneId = $("[id$='ddlScene']").val();
                if (txtFrom == "" && txtTo == "" && sceneId == "") {
                    ShowFailTip("请选择时间范围或者选择推广渠道！");
                    return;
                }
                $.ajax({
                    url: ("DetailCount.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "GetCount", Callback: "true", txtFrom: txtFrom, txtTo: txtTo, SceneId: sceneId },
                    async: false,
                    success: function (resultData) {
                        if (resultData.STATUS == "Success") {
                            var value = $.parseJSON(resultData.Data);
                            $('#container').highcharts({
                                chart: {
                                    plotBackgroundColor: null,
                                    plotBorderWidth: null,
                                    plotShadow: false
                                },
                                title: {
                                    text: '推广渠道比例-饼状图'
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%  数值为:{point.y}</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            color: '#000000',
                                            connectorColor: '#000000',
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                        }
                                    }
                                },
                                series: [{
                                    type: 'pie',
                                    name: '推广渠道比例',
                                    data: value
                                }]
                            });
                            $("text").last().hide();
                            return;
                        }
                       else if (resultData.STATUS == "NoDate") {
                            ShowFailTip("请选择时间范围！");
                            return;
                        }
                        else {
                            ShowFailTip("系统忙请稍后再试！");
                        }
                    }
                });
            })




        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信渠道推广统计管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以设置不同微信渠道推广统计操作" />
                    </td>
                </tr>
            </table>
        </div>
       <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td width="280px">
                    关注时间：
                    <input id="txtFrom" type="text" style=" width:90px" />
                    --
                     <input id="txtTo" type="text" style=" width:90px" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    渠道场景：
                    <asp:DropDownList ID="ddlScene" runat="server">
                    </asp:DropDownList>

                    <input id="btnGetCount" type="button" value="统计" class="adminsubmit_short" />
                </td>
            </tr>
        </table>
        <br />
        <div id="container">

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
