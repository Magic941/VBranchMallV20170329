<%@ Page Title="" Language="C#" MasterPageFile="~/Enterprise/Basic.Master" AutoEventWireup="true" CodeBehind="MapManage.aspx.cs" Inherits="Maticsoft.Web.Enterprise.MapManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/baidu.map.api.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.map.baidu-1.6.js" type="text/javascript"></script>
    <link href="/Scripts/uploadify-v3.1/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/uploadify-v3.1/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.autosize-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.dynatextarea.js" type="text/javascript"></script>
    <style type="text/css">
        .loading { height: 24px; width: 400px; /*margin-left: auto;*/ /*margin-right: auto;*/ margin: 0 auto; visibility: hidden; }
        .progress { float: right; width: 1px; height: 14px; color: white; font-size: 12px; overflow: hidden; background-color: navy; padding-left: 5px; }
        .uploadify { margin: 0; /*float: left;*/ }
        .uploadify-queue { margin: 0; float: left; /*z-index: 9999; position: absolute; left: 235px; top: 200px;margin: 0;padding: 0 */ }
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
            //            alert("标注点标注成功");
            $("[id$=MarkersLongitude]").val(lng);
            $("[id$=MarkersDimension]").val(lat);
        }

        function LoadMakers(data) {
            var Maker = "[";
            $(data).each(function () {
                var sContent = "";
                if (this.pointImg) {
                    sContent =
                        "<div  style='text-align: left;'><h4 style='margin:0 0 5px 0;padding:0.2em 0; width:300px'>" + this.pointerTitle + "</h4>" +
                            "<img style='float:right;margin:4px' id='imgDemo' src='" + this.pointImg + "' width='139' height='104' title='" + this.pointerTitle + "'/>" +
                                "<p style='margin:0;line-height:1.5;font-size:13px;'>" + this.pointerContent + "</p></div>";
                } else {
                    sContent =
                        "<div  style='text-align: left;'><h4 style='margin:0 0 5px 0;padding:0.2em 0'>" + this.pointerTitle + "</h4>" +
                            "<p style='margin:0;line-height:1.5;font-size:13px;'>" + this.pointerContent + "</p></div>";
                }
                Maker += "{Longitude:" + this.markersLongitude + ",Dimension:" + this.markersDimension + ",Window: { LoadEvent:'click',Content:\"" + sContent + "\"},enableMassClear:true},";
            });
            Maker += "]";
            return eval(Maker);
        }

        function InitMapOption() {
            return {
                Container: "MapContent", //在那个id的容器里显示
                Longitude: undefined,
                Dimension: undefined,
                Level: undefined,
                SearchCity: undefined, //搜索的地区
                SearchArea: undefined,
                EnableOnlyMarker: true,
                EnableKeyboard: true, // 是否开启键盘 上下键
                EnableScrollWheelZoom: true, //是否启动鼠标的滚轮
                NavigationControl: true, //师傅需要鱼刺骨（进行上下左右放大或小的图标）
                ScaleControl: true,  //是否显示比例尺
                MapTypeControl: true, //是否有卫星地图等的图标
                Markers: undefined,
                MenuItem: {
                    MenuItemzoomIn: true,       // 是否产生放大地图按钮
                    MenuItemzoomOut: true,      // 是否产生缩小地图按钮
                    MenuItemsetZoomTop: true,   // 是否产生放大到最大级（最清晰）按钮
                    MenuItemsetZoomCanSeeCounty: true,
                    MenuItemsetPoint: {
                        EnableDragging: true,
                        SetAnimation: true,
                        MenuEvent: "dragend",
                        CallBack: function(lng, lat) {
                            SetMarkerPoint(lng, lat);
                        },
                        MenuClickCallBack: function(lng, lat) {
                            SetMarkerPoint(lng, lat);
                            alert("标注点标注成功");
                        }
                    }
                }
            };
        }

        // 第一次请求得到的数据 包括第一页和页码
        $(function () {
            $(document).ready(function () {
                $('textarea').autosize();
                $.dynatextarea($('[id$=txtPointerContent]'), 100, $('#progressbar1'));
                if ($("[id$=hfMapImgUrl]").val()) {
                    $('#imgMap').attr('src', $("[id$=hfMapImgUrl]").val());
                } else {
                    $('#imgMap').hide();
                }
                $.ajax({
                    url: "/MapHandle.aspx",
                    type: 'post',
                    dataType: 'json',
                    timeout: 10000,
                    data: { Action: "GetDepartmentMapById", DepartmentId: $("[id$=hfEnID]").val() },
                    success: function (resultData) {
                        switch (resultData.STATUS) {
                            case "OK":
                                $('.MapDiv').show();
                                var Maker = LoadMakers(resultData.DATA);
                                $("[id$=MarkersLongitude]").val(Maker[0].Longitude);
                                $("[id$=MarkersDimension]").val(Maker[0].Dimension);

                                var myoption = InitMapOption();
                                myoption.Longitude = Maker[0].Longitude;
                                myoption.Dimension = Maker[0].Dimension;
                                myoption.Markers = Maker;

                                baidumap.baidumapload(myoption);
                                break;
                            default:
                                $('.MapDiv').hide();
                                break;
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest.responseText);
                    }
                });
            });

            // 无刷新上传相关
            $('#file_upload').uploadify({
                swf: '/Scripts/uploadify-v3.1/uploadify.swf',
                uploader: '/MapMarkImgHandle.aspx',
                auto: true,
                multi: false,
                fileTypeExts: '*.jpg;*.jpeg;*.png;*.gif;*.bmp',
                fileTypeDesc: 'Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)',
                queueSizeLimit: 1,
                fileSizeLimit: '10MB',
                buttonImage: '/Scripts/uploadify-v3.1/uploadfile.png',
                //                buttonImage: '/Scripts/uploadify-v3.1/browse-btn.png',
                width: 92,
                height: 24,
                onUploadSuccess: function (file, data, response) {
                    $("[id$=hfMapImgUrl]").val(data.split('|')[1]);
                    $('#imgMap').attr('src', $("[id$=hfMapImgUrl]").val());
                    $('#imgMap').show();
                    clickautohide(4, "上传图片成功！", 3000);
                }
            });
            //网页点击搜索时执行
            $("#btnSearch").click(function () {
                $.ajax({
                    url: "/MapHandle.aspx",
                    type: 'post',
                    dataType: 'json',
                    timeout: 10000,
                    data: { Action: "GetDepartmentMapById", DepartmentId: $("[id$=hfEnID]").val() },
                    success: function (resultData) {

                        var searchCity = $("#txtCity").val();
                        var searchArea = $("#txtArea").val();
                        var myoption = InitMapOption();
                        myoption.SearchCity = searchCity; //搜索的地区
                        myoption.SearchArea = searchArea;

                        switch (resultData.STATUS) {
                            case "OK":
                                myoption.Markers = LoadMakers(resultData.DATA);
                                break;
                            default:
                                break;
                        }
                        baidumap.baidumapload(myoption);
                        $('.MapDiv').show();
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest.responseText);
                    }
                });

                //                var searchCity = $("#txtCity").val();
                //                var searchArea = $("#txtArea").val();
                //                var myoption = InitMapOption();
                //                myoption.SearchCity = searchCity; //搜索的地区
                //                myoption.SearchArea = searchArea;
                //                baidumap.baidumapload(myoption);
            });
            // 提交数据发送ajax 请求
            $("#btnSubmitMore").click(function () {
                $.ajax({
                    url: "/MapHandle.aspx",
                    type: 'post',
                    dataType: 'json',
                    timeout: 10000,
                    data: {
                        Action: "SetDepartmentMap",
                        //                        UserId: $("[id$=UserID]").val(),
                        DepartmentId: $("[id$=hfEnID]").val(),
                        MarkersLongitude: $("[id$=txtMarkersLongitude]").val(),
                        MarkersDimension: $("[id$=txtMarkersDimension]").val(),
                        PointerTitle: $("[id$=txtPointerTitle]").val(),
                        PointerContent: $("[id$=txtPointerContent]").val(),
                        PointImg: $("[id$=hfMapImgUrl]").val(),
                        MapId: $("[id$=hfMapId]").val()
                        //                        PointClass: $("[id$=txtPointClass").val()
                    },
                    success: function (resultData) {
                        switch (resultData.STATUS) {
                            case "OK":
                                var myoption = InitMapOption();
                                myoption.Longitude = resultData.DATA.markersLongitude;
                                myoption.Dimension = resultData.DATA.markersDimension;
                                myoption.Markers = LoadMakers(resultData.DATA);
                                baidumap.baidumapload(myoption);
                                break;
                            default:
                                break;
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest.responseText);
                    }
                });

            });
        });
    </script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <!--是否是编辑-->
    <asp:hiddenfield id="hfMapId" runat="server" />
    <asp:hiddenfield id="hfEnID" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:literal id="Literal1" runat="server" text="电子地图"></asp:literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:literal id="Literal7" runat="server" text="您可以设置企业地图. （首次设置地图请您先填写城市名称进行搜索）"></asp:literal>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="newsadd_title">
        <div class="member_info_show" style="padding: 0">
            <ul>
                <li style="margin: 0; text-align: left">城市：</li><li>
                    <input id="txtCity" type="text" />（如北京）&nbsp;</li><li>具体位置：</li><li>
                        <input id="txtArea" type="text" />（如颐和园）&nbsp; </li>
                <li>
                    <input id="btnSearch" type="button" class="adminsubmit" value="地图搜索" /></li>
            </ul>
        </div>
    </div>
    <!-- 地图信息start -->
    <div class="newsadd_title MapDiv">
        <ul>
            <li class="loading" style="margin: 0; width: 100%;">
                <p style="text-align: center">
                    <img src="/Images/data-loading.gif" alt="数据装载中" /></p>
            </li>
        </ul>
        <ul>
            <li>
                <div id="MapContent" style="width: 750px; height: 500px; margin: 0px;">
                </div>
            </li>
        </ul>
        <ul>
            <li class="loading" style="margin: 0; width: 100%;">
                <p style="text-align: center">
                    <img src="/Images/data-loading.gif" alt="数据装载中" /></p>
            </li>
        </ul>
    </div>
    <!-- 地图信息end -->
    <div class="newsadd_title">
        <div class="member_info_show" style="padding: 0">
            <div class="MapDiv">
                <ul>
                    <li>名称：</li><li>
                        <asp:textbox id="txtPointerTitle" runat="server" width="476px">
                        </asp:textbox>&nbsp;&nbsp;&nbsp; </li>
                </ul>
                <ul>
                    <li>内容：</li><li>
                        <asp:textbox id="txtPointerContent" runat="server" textmode="MultiLine" width="476px">
                        </asp:textbox><div id="progressbar1" class="progress">
                        </div>
                    </li>
                    <li>(字数限制为100个)</li>
                </ul>
                <ul>
                    <li>图片：</li>
                    <li><span>（可选, 用于在标注点提示中显示的图片.）</span></li>
                    <li style="padding-left: 70px; width: 650px; height: 85px">
                        <input id="file_upload" type="file" class="FileUpload" name="file_upload" />
                    </li>
                    <li style="padding-left: 70px;">
                        <img id="imgMap" width='139px' height='104px' /></li>
                </ul>
                <ul>
                    <li style="padding-left: 70px">
                        <input id="btnSubmitMore" type="button" value="保存" class="adminsubmit" />&nbsp;<span style="color: red">(备注：您可右键选择[添加标注点]，设置您的所在位置)</span></li>
                </ul>
                <ul style="display: none">
                    <li style="margin: 0; width: 100%; text-align: left;">&nbsp;&nbsp;&nbsp; 经 度
                        <asp:textbox id="txtMarkersLongitude" runat="server">
                        </asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 维度：<asp:textbox id="txtMarkersDimension" runat="server"></asp:textbox>
                        <asp:hiddenfield id="hfUserID" runat="server" />
                        <asp:hiddenfield id="hfMapImgUrl" runat="server" />
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:content>
