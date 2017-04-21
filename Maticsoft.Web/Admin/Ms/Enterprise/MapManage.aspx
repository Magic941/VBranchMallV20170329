<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="MapManage.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Enterprise.MapManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://api.map.baidu.com/api?v=1.3" type="text/javascript"></script>
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
            $("[id$=MarkersLongitude]").val(lng);
            $("[id$=MarkersDimension]").val(lat);
        }

        function InitMapOption() {
            return {
                Container: "MapContent",        //地图的容器
                Longitude: undefined,           //经度
                Dimension: undefined,           //纬度
                Level: undefined,               //缩放级别
                SearchCity: undefined,          //搜索城市
                SearchArea: undefined,          //搜索地区
                EnableKeyboard: true,           // 是否开启键盘 上下键
                NavigationControl: true,        //是否有鱼骨工具（进行上下左右放大或小的图标）
                ScaleControl: true,             //是否显示比例尺
                MapTypeControl: true,           //是否有卫星地图等的图标
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
                            alert("添加标注成功, 请您填写地图下方的标注点信息.");
                        }
                    }
                }
            };
        }

        // 第一次请求得到的数据 包括第一页和页码
        $(function () {
            $(window).load(function () {
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
                    async: false,
                    timeout: 10000,
                    data: { Action: "GetDepartmentMapById", DepartmentId: $("[id$=hfEnID]").val() },
                    success: function (resultData) {
                        var option;
                        switch (resultData.STATUS) {
                            case "OK":
                                var Maker = LoadMakers(resultData.DATA);
                                $("[id$=MarkersLongitude]").val(Maker[0].Longitude);
                                $("[id$=MarkersDimension]").val(Maker[0].Dimension);
                                option = InitMapOption();
                                option.Longitude = Maker[0].Longitude;
                                option.Dimension = Maker[0].Dimension;
                                option.Markers = Maker;
                                baidumap.baidumapload(option);
                                $('.MapDiv').show();
                                break;
                            default:
                                $('#MapContent').empty();
                                var searchCity = $("[id$=txtCity]").val();
                                var searchArea = $("#txtArea").val();
                                option = InitMapOption();
                                option.SearchCity = searchCity; //搜索的地区
                                option.SearchArea = searchArea;
                                baidumap.baidumapload(option);
                                $('.MapDiv').show();
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
                $('#MapContent').html('');
                $.ajax({
                    url: "/MapHandle.aspx",
                    type: 'post',
                    dataType: 'json',
                    async: false,
                    timeout: 10000,
                    data: { Action: "GetDepartmentMapById", DepartmentId: $("[id$=hfEnID]").val() },
                    success: function (resultData) {

                        $('#MapContent').empty();
                        var searchCity = $("[id$=txtCity]").val();
                        var searchArea = $("#txtArea").val();
                        var option = InitMapOption();
                        option.SearchCity = searchCity; //搜索的地区
                        option.SearchArea = searchArea;

                        switch (resultData.STATUS) {
                            case "OK":
                                option.Markers = LoadMakers(resultData.DATA);
                                break;
                            default:
                                break;
                        }
                        baidumap.baidumapload(option);
                        $('.MapDiv').show();
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest.responseText);
                    }
                });

                //                var searchCity = $("[id$=txtCity]").val();
                //                var searchArea = $("#txtArea").val();
                //                var option = InitMapOption();
                //                option.SearchCity = searchCity; //搜索的地区
                //                option.SearchArea = searchArea;
                //                baidumap.baidumapload(option);
            });
            // 提交数据发送ajax 请求
            $("#btnSubmit").click(function () {
                $.ajax({
                    url: "/MapHandle.aspx",
                    type: 'post',
                    dataType: 'json',
                    async: false,
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
                                $('#MapContent').empty();
                                var option = InitMapOption();
                                option.Longitude = resultData.DATA.markersLongitude;
                                option.Dimension = resultData.DATA.markersDimension;
                                option.Markers = LoadMakers(resultData.DATA);
                                baidumap.baidumapload(option);
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--是否是编辑-->
    <asp:HiddenField ID="hfMapId" runat="server" />
    <asp:HiddenField ID="hfEnID" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="电子地图"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal7" runat="server" Text="您可以设置企业地图. （每个企业只有一个标注点）"></asp:Literal>
                        <br />
                        <span style="color: red">备注：您可在地图上右键选择[添加标注点]，设置企业的所在位置</span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="newsadd_title">
        <div class="member_info_show" style="padding: 0">
            <ul>
                <li style="margin: 0; text-align: left">城市：</li><li>
                    <input id="txtCity" runat="server" type="text" />（如北京）&nbsp;</li><li>具体位置：</li><li>
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
                        <asp:TextBox ID="txtPointerTitle" runat="server" Width="476px">
                        </asp:TextBox>&nbsp;&nbsp;&nbsp; </li>
                </ul>
                <ul>
                    <li>内容：</li><li>
                        <asp:TextBox ID="txtPointerContent" runat="server" TextMode="MultiLine" Width="476px">
                        </asp:TextBox><div id="progressbar1" class="progress">
                        </div>
                    </li>
                    <li>(字数限制为100个)</li>
                </ul>
                <ul>
                    <li>图片：</li>
                    <li><span>（可选, 用于在标注点提示中显示的图片.）</span></li>
                    <li style="padding-left: 70px; width: 650px; <%--height: 85px--%>">
                        <input id="file_upload" type="file" class="FileUpload" name="file_upload" />
                    </li>
                    <li style="padding-left: 70px;">
                        <img id="imgMap" width='139px' height='104px' /></li>
                </ul>
                <ul>
                    <li style="padding-left: 70px">
                        <input id="btnSubmit" type="button" value="保存" class="adminsubmit_short" />
                        <input id="btnReturn" type="button" value="返回" onclick="window.location='/admin/Ms/Enterprise/List.aspx';" class="adminsubmit_short" />
                    </li>
                </ul>
                <ul style="display: none">
                    <li style="margin: 0; width: 100%; text-align: left;">&nbsp;&nbsp;&nbsp; 经 度
                        <asp:TextBox ID="txtMarkersLongitude" runat="server">
                        </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 维度：<asp:TextBox ID="txtMarkersDimension" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hfUserID" runat="server" />
                        <asp:HiddenField ID="hfMapImgUrl" runat="server" />
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
