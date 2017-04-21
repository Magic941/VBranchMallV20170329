<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.Map" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //绑定日期控件
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth();
            var day = today.getDate();
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtDate']").prop("readonly", true).datepicker({
                numberOfMonths: 1, //显示月份数量
                onClose: function () {
                    $(this).css("color", "#000");
                }
            }).focus(function () { $(this).val(''); });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/list.gif) no-repeat"><a href="javascript:;" onclick="location.reload();">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblScan%>" /></a> </li>
                    <li id="a_return" runat="server" style="background: url(/admin/images/reload.png) no-repeat">
                        <a href="CollectAttendance.aspx?username=<%= UserName %>">
                        <asp:Literal ID="Literal7" runat="server" Text="返回" />
                        </a>
                        <b>|</b>
                    </li>
                        <li style="width: auto">
                        <input id="chkTime" type="checkbox" checked="checked" /><label for="chkTime">显示考勤时间</label></li>
                </ul>
            </div>
        </div>
        <div class="newsadd_title" style="margin-left: 0px; margin-right: 0px;display: none;">
            <div class="member_info_show" style="padding: 0">
                <ul>
                    <li style="margin: 0; text-align: left">城市：</li><li>
                        <input id="txtCity" type="text" />（如北京）&nbsp;</li><li>具体位置：</li><li>
                            <input id="txtArea" type="text" />（如颐和园）&nbsp; </li>
                    <li>
                        <input id="btnMapSearch" type="button" class="adminsubmit" value="地图搜索" /></li>
                    
                </ul>
            </div>
        </div>
        <!-- 地图信息start -->
        <div class="newsadd_title MapDiv" style="margin-left: 0px; margin-right: 0px">
            <ul style="display: none;">
                <li class="loading" style="margin: 0; width: 100%;">
                    <p style="text-align: center">
                        <img src="/Images/data-loading.gif" alt="数据装载中" /></p>
                </li>
            </ul>
            <ul style="width: 100%;">
                <li style="width: 100%;">
                    <div id="MapContent" style="width: 99%; height: 500px; margin: 0px;">
                    </div>
                </li>
            </ul>
            <ul style="display: none;">
                <li class="loading" style="margin: 0; width: 100%;">
                    <p style="text-align: center">
                        <img src="/Images/data-loading.gif" alt="数据装载中" /></p>
                </li>
            </ul>
            <ul style="display: none;">
                <li style="margin: 0; width: 100%; text-align: left;">&nbsp;&nbsp;&nbsp; 经 度
                    <asp:TextBox ID="txtMarkersLongitude" runat="server">
                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 维度：<asp:TextBox ID="txtMarkersDimension" runat="server"></asp:TextBox>
                </li>
            </ul>
        </div>
        <!-- 地图信息end -->
        <cc1:GridViewEx  ID="gridView" runat="server" AllowPaging="True" AllowSorting="True" ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%" PageSize="1000" DataKeyNames="ID" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="true">
            <Columns>
                <asp:TemplateField HeaderText="考勤编号" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("ID")%>
                        <input type="hidden" id="ID" value='<%# Eval("ID")%>' />
                        <input type="hidden" id="Longitude" value='<%# Eval("Longitude")%>' />
                        <input type="hidden" id="Latitude" value='<%# Eval("Latitude")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Eval("UserName")%>
                        <input type="hidden" id="UserName" value='<%# Eval("UserName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="真实姓名" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetTrueName( Eval("UserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="考勤类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetTypeName(Eval("TypeID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建日期" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("CreatedDate")%>
                        <input type="hidden" id="CreatedDate" value='<%# Eval("CreatedDate")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetStatus( Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="批复状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetRevStatus( Eval("ReviewedStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="考勤评分" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%# Eval("Score")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="批复人" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetUserName( Eval("ReviewedUserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批复时间" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("ReviewedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" CssClass="DataRow" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
        <script src="http://api.map.baidu.com/api?v=1.4" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.map.baidu-1.6.js" type="text/javascript"></script>
    <style type="text/css">
        .loading { height: 24px; width: 400px; /*margin-left: auto;*/ /*margin-right: auto;*/ margin: 0 auto; visibility: hidden; }
        .progress { float: right; width: 1px; height: 14px; color: white; font-size: 12px; overflow: hidden; background-color: navy; padding-left: 5px; }
    </style>
    <script type="text/javascript">
        $(function () {
            //每次ajax请求时
            $(".loading").ajaxStart(function () {
                $(this).css("visibility", "visible");
                $(this).animate({
                    opacity: 1
                }, 0);
            }).ajaxStop(function () {
                $(this).animate({
                    opacity: 0
                }, 500);
            });
        });

        function SetMarkerPoint(lng, lat) {
            $("[id$=MarkersLongitude]").val(lng);
            $("[id$=MarkersDimension]").val(lat);
        }

        function InitMapOption() {
            return {
                Container: "MapContent",        //地图的容器
                Longitude: 116.404,           //经度
                Dimension: 39.915,           //纬度
                Level: undefined,               //缩放级别
                SearchCity: undefined,          //搜索城市
                SearchArea: undefined,          //搜索地区
                EnableKeyboard: true,           // 是否开启键盘 上下键
                NavigationControl: true,        //是否有鱼骨工具（进行上下左右放大或小的图标）
                ScaleControl: true,             //是否显示比例尺
                MapTypeControl: true,           //是否有卫星地图等的图标
                EnableMarkers: true,
                EnablePolyline: true,
                DisableMarkerLabel: false,
                DisableMarkerWindow: true,
                EnableScrollWheelZoom: true,
                Markers: undefined,             //标注点集合
                MenuItem: {
                    MenuItemsetPoint: {
                        EnableDragging: true,   //启用标注点拖拽
                        SetAnimation: true,     //标注点动画
                        MenuEvent: "dragend",   //设置坐标事件名称 目前是标注点拖拽 用于记录最后坐标
                        CallBack: function (lng, lat) { //坐标事件回调
                            SetMarkerPoint(lng, lat);
                        },
                        MenuClickCallBack: function (lng, lat) {    //通过右键菜单添加标注点事件
                            SetMarkerPoint(lng, lat);
                        }
                    }
                },
                Points: LoadPoints(),
                Polyline: {
                    Path: [new BMap.Point(116.399, 39.910), new BMap.Point(116.405, 39.920), new BMap.Point(116.425, 39.900)],       // 轨迹点的集合
                    StrokeColor: 'red',      // 轨迹颜色
                    StrokeWeight: 6,   //折线的宽度，以像素为单位。
                    StrokeOpacity: 0.5, //折线的透明度，取值范围0 - 1。
                    StrokeStyle: 'solid', //折线的样式，solid或dashed。
                    PolylineRun: {
                        IsRun: true,      //是否运动显示轨迹 默认是true
                        //RunIcon: new BMap.Icon("/images/map1.gif", new BMap.Size(32, 70), { imageOffset: new BMap.Size(0, 0) }),   //轨迹上运动的图标
                        RunEvent: "click",
                        RunInterval: <%= MapPolylineInterval %>
                    }
                }
            };
        }

        function LoadPoints() {
            var points = [];
            $('.DataRow').each(function () {
                var info = new BMap.Point($(this).find('#Longitude').val(), $(this).find('#Latitude').val());
                info.Title = $(this).find('#CreatedDate').val();
                points.push(info);
            });
            return points.reverse();
        }

        // 第一次请求得到的数据 包括第一页和页码
        $(function () {
            $(window).load(function () {
                $('#MapContent').empty();
                var searchCity = $("[id$=txtCity]").val();
                var searchArea = $("#txtArea").val();
                option = InitMapOption();
                option.SearchCity = searchCity; //搜索的地区
                option.SearchArea = searchArea;
                baidumap.baidumapload(option);
                $('.MapDiv').show();
            });

            $('#chkTime').click(function () {
                var checked = $(this).prop('checked');
                var arrayOverlays = _baidumap.getOverlays();
                $(arrayOverlays).each(function () {
                    if (!this.Ua || this.Ua.className != "BMap_Marker") return;
                    var lable = this.getLabel();
                    if (!lable) return;
                    if (checked) lable.show();
                    else lable.hide();
                });
            });

            //网页点击搜索时执行
            $("#btnMapSearch").click(function () {
                $('#MapContent').empty();
                var searchCity = $("[id$=txtCity]").val();
                var searchArea = $("#txtArea").val();
                var option = InitMapOption();
                option.SearchCity = searchCity; //搜索的地区
                option.SearchArea = searchArea;

                baidumap.baidumapload(option);

                $('.MapDiv').show();
            });

        });
    </script>

</asp:Content>
