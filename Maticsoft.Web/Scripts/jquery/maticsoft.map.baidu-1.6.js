/*
* File:        maticsoft.map.baidu-1.6.js
* Author:      qihq@maticsoft.com, tuzh@maticsoft.com, yaoy@maticsoft.com
* Copyright © 2012 Maticsoft. All Rights Reserved.
*/
;
//旧方法 即将作废
function LoadMakers(data) {
    var Maker = "[";
    $(data).each(function() {
        var sContent = "<div  style='text-align: left;'><h4 style='margin:0 0 5px 0;padding:0.2em 0'>" + this.pointerTitle + "</h4>";
        if (this.pointImg) {
            sContent = "<div  style='text-align: left;'><h4 style='margin:0 0 5px 0;padding:0.2em 0; width:300px'>" + this.pointerTitle + "</h4>";
            sContent += "<img style='float:right;margin:4px' id='imgDemo' src='" + this.pointImg + "' width='139' height='104' title='" + this.pointerTitle + "'/>";
        }
        if (this.pointerContent) {
            sContent += "<p style='margin:0;line-height:1.5;font-size:13px;width:400px'>" + this.pointerContent + "</p>";
        }
        sContent += "</div>";
        Maker += "{Longitude:" + this.markersLongitude + ",Dimension:" + this.markersDimension + ",Window: { LoadEvent:'click',Content:\"" + sContent + "\"},enableMassClear:true},";
    });
    Maker += "]";
    return eval(Maker);
}
/*

Point.Longitude   lng   //经度
Point.Latitude    lat   //纬度

//TODO:Dimension 更名为 Latitude

Point.Title
Point.Img
Point.Content

1.地图封装类库, 新增 Point[] 属性, 同时用于 标记 和 轨迹 使用
2.页面拼装 Point[] 数据, 加载到地图中
3.联调测试

*/

var _baidumap = null;
(function() {
    var CurrentOption;
    var map = null;

    function baidumapload(option) {
        CurrentOption = option;
        option = CurrentOption; //反向覆盖, 供外部使用
        if (!CurrentOption.SearchCity && !CurrentOption.Longitude) return;
        map = null;
        map = new BMap.Map(CurrentOption.Container); // 创建Map实例
        if (CurrentOption.SearchCity) {
            var local = new BMap.LocalSearch(CurrentOption.SearchCity, {
                renderOptions: {
                    map: map,
                    autoViewport: true,
                    selectFirstResult: false
                }
            });
            if (CurrentOption.SearchArea) {
                local.search(CurrentOption.SearchArea);
            } else {
                local.search(CurrentOption.SearchCity);
            }
        }
        if (CurrentOption.Longitude && CurrentOption.Dimension) {
            var point = new BMap.Point(CurrentOption.Longitude, CurrentOption.Dimension); // 创建点坐标
            if (CurrentOption.Level) {
                map.centerAndZoom(point, CurrentOption.Level);
            } else {
                map.centerAndZoom(point, 15);
            }
        }
        if (CurrentOption.Points && CurrentOption.Points.length > 0) {
            //根据坐标加载Marker
            if (CurrentOption.EnableMarkers) {
                var Maker = "[";
                $(CurrentOption.Points).each(function() {
                    if (this.Title) {
                        var sContent = "<div  style='text-align: left;'><h4 style='margin:0 0 5px 0;padding:0.2em 0'>" + this.Title + "</h4>";
                        if (this.Img) {
                            sContent = "<div  style='text-align: left;'><h4 style='margin:0 0 5px 0;padding:0.2em 0; width:300px'>" + this.Title + "</h4>";
                            sContent += "<img style='float:right;margin:4px' id='imgDemo' src='" + this.Img + "' width='139' height='104' title='" + this.Title + "'/>";
                        }
                        if (this.Content) {
                            sContent += "<p style='margin:0;line-height:1.5;font-size:13px;width:400px'>" + this.Content + "</p>";
                        }
                        sContent += "</div>";
                        Maker += "{Longitude:" + this.lng + ",Dimension:" + this.lat + ",Window: { LoadEvent:'click',Content:\"" + sContent + "\"},enableMassClear:true,Label:\"" + this.Title + "\"},";
                    } else {
                        Maker += "{Longitude:" + this.lng + ",Dimension:" + this.lat + ",enableMassClear:true},";
                    }
                });
                Maker += "]";
                CurrentOption.Markers = eval(Maker);
            }
            if (CurrentOption.EnablePolyline) {
                CurrentOption.Polyline.Path = CurrentOption.Points; //根据坐标加载路线
            }
            var viewport = map.getViewport(CurrentOption.Points); //Fix视野
            map.setCenter(viewport.center);
            map.setZoom(viewport.zoom);
        }
        if (CurrentOption.Markers) {
            if (CurrentOption.Markers.length > 0) {
                for (var i = 0; i < CurrentOption.Markers.length; i++) {
                    (function(index) {
                        if (!CurrentOption.Markers[index]) return;
                        var infoWindow; //弹出框
                        var marker;
                        if (CurrentOption.MarkerIcon) {
                            var myIcon = new BMap.Icon(CurrentOption.MarkerIcon, new BMap.Size(28, 37), {
                                
                                //                                  offset: new BMap.Size(10, 25),
                                 //                                  imageOffset: new BMap.Size(0 - i * 28, 0)
                            }); //                          var marker = new BMap.Marker(new BMap.Point(CurrentOption.Markers[index].Longitude, CurrentOption.Markers[index].Dimension), { icon: myIcon });  // 创建标注
                            marker = new BMap.Marker(new BMap.Point(CurrentOption.Markers[index].Longitude, CurrentOption.Markers[index].Dimension)); // 创建标注
                            marker.setIcon(myIcon);
                        } else {
                            marker = new BMap.Marker(new BMap.Point(CurrentOption.Markers[index].Longitude, CurrentOption.Markers[index].Dimension)); // 创建标注
                        }
                        if (CurrentOption.Markers[index].SetAnimation) {
                            marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
                        }
                        if (CurrentOption.Markers[index].EnableDragging) {
                            marker.enableDragging(); //开启标注拖拽功能
                        }
                        if (!CurrentOption.DisableMarkerLabel && CurrentOption.Markers[index].Label) {
                            var label = new BMap.Label(CurrentOption.Markers[index].Label, { offset: new BMap.Size(20, -10) }
                            );
                            marker.setLabel(label); //添加文本标注
                        }
                        infoWindow = undefined;
                        map.addOverlay(marker); // 将标注添加到地图中
                        //TODO: DisableMarkerWindow 应更正为 EnableMarkerWindow 目前是为了兼容历史版本
                        if (!CurrentOption.DisableMarkerWindow && CurrentOption.Markers[index].Window) {
                            if (CurrentOption.Markers[index].Window.Content) {
                                infoWindow = new BMap.InfoWindow(CurrentOption.Markers[index].Window.Content); // 创建信息窗口对象
                            }
                            map.addControl(new BMap.OverviewMapControl()); //添加缩略地图控件

                            if (CurrentOption.Markers[index].Window.LoadEvent)// || CurrentOption.Markers[index].Window.LoadEvent == "MapLoaded")
                            {
                                marker.addEventListener(CurrentOption.Markers[index].Window.LoadEvent,
                                    function() {
                                        var tmpInfo = infoWindow;
                                        this.openInfoWindow(tmpInfo); //图片加载完毕重绘infowindow
                                        tmpInfo.redraw();
                                    });
                            }
//                            else {
//                                marker.addEventListener(CurrentOption.Markers[index].Window.LoadEvent,
//                                    function() {
//                                        this.openInfoWindow(infoWindow); //图片加载完毕重绘infowindow
//                                        infoWindow.redraw();
//                                    });
//                            }
                        }
                    }(i));
                }
            }
        }
        if (CurrentOption.MenuItem) {
            (function() {
                var contextMenu = new BMap.ContextMenu();
                var txtMenuItem = [{
                        text: '放大',
                        callback: function() {
                            map.zoomIn();
                        }
                    },
                    {
                        text: '缩小',
                        callback: function() {
                            map.zoomOut();
                        }
                    },
                    //                    {
                //                        text: '放置到最大级',
                //                        callback: function() { map.setZoom(18); }
                //                    },
                //                    {
                //                        text: '查看全国',
                //                        callback: function() { map.setZoom(5); }
                //                    },
                    {
                        text: '在此添加标注',
                        callback: function(p) {
                            if (CurrentOption.EnableOnlyMarker) {
                                map.clearOverlays();
                            }
                            var marker = new BMap.Marker(p),
                                px = map.pointToPixel(p);
                            if (CurrentOption.MenuItem.MenuItemsetPoint.SetAnimation) {
                                marker.setAnimation(BMAP_ANIMATION_DROP); //跳动的动画
                            }
                            if (CurrentOption.MenuItem.MenuItemsetPoint.EnableDragging) {
                                marker.enableDragging(true); //开启标注拖拽功能
                            }
                            map.addOverlay(marker);
                            if (CurrentOption.MenuItem.MenuItemsetPoint.MenuEvent && CurrentOption.MenuItem.MenuItemsetPoint.CallBack) {
                                marker.addEventListener(CurrentOption.MenuItem.MenuItemsetPoint.MenuEvent,
                                    function(e) {
                                        CurrentOption.MenuItem.MenuItemsetPoint.CallBack(e.point.lng, e.point.lat);
                                    });
                                if (CurrentOption.MenuItem.MenuItemsetPoint.MenuClickCallBack) {
                                    CurrentOption.MenuItem.MenuItemsetPoint.MenuClickCallBack(p.lng, p.lat);
                                }
                            }
                        }
                    }];
                for (var i = 0; i < txtMenuItem.length; i++) {
                    contextMenu.addItem(new BMap.MenuItem(txtMenuItem[i].text, txtMenuItem[i].callback, 100));
                    if (i == 1 || i == 3) {
                        contextMenu.addSeparator();
                    }
                }
                map.addContextMenu(contextMenu);
            }());
        }
        if (CurrentOption.NavigationControl) {
            map.addControl(new BMap.NavigationControl()); // 启用鱼骨头。
        }
        if (CurrentOption.EnableScrollWheelZoom) { //BUG FOR IE DON'T ScrollWheelZoom  BEN ADD 20120926
            map.enableScrollWheelZoom(); // 启用滚轮放大缩小。
        }
        if (CurrentOption.EnableKeyboard) {
            map.enableKeyboard(); // 启用键盘操作。
        }
        if (CurrentOption.ScaleControl) {
            map.addControl(new BMap.ScaleControl()); // 启用比例尺。
        }
        if (CurrentOption.MapTypeControl) {
            map.addControl(new BMap.MapTypeControl()); // 是否启用卫星地图等等。
        }
        if (CurrentOption.Polyline) {
            var _self = CurrentOption.Polyline;
            if (_self.Path) {
                var polyline = new BMap.Polyline(_self.Path, {
                    strokeColor: "red",
                    strokeWeight: 6,
                    strokeOpacity: 0.5
                });
                if (_self.StrokeColor) {
                    polyline.setStrokeColor(_self.StrokeColor);
                }
                if (_self.StrokeWeight) {
                    polyline.setStrokeWeight(_self.StrokeWeight);
                }
                if (_self.StrokeOpacity) {
                    polyline.setStrokeOpacity(_self.StrokeOpacity);
                }
                if (_self.StrokeStyle) {
                    polyline.setStrokeStyle(_self.StrokeStyle);
                }
                map.addOverlay(polyline);
                if (_self.PolylineRun && _self.PolylineRun.IsRun) {
                    var runMk = null;
                    if (_self.PolylineRun.RunIcon) {
                        runMk = new BMap.Marker(_self.Path[0], {
                            icon: _self.PolylineRun.RunIcon
                        });
                    } else {
                        runMk = new BMap.Marker(_self.Path[0], {
                            //小车图片
                            icon: new BMap.Icon("/Content/themes/base/images/Marker/default.png", new BMap.Size(48, 70), {
                                //offset: new BMap.Size(0, -5),    //相当于CSS精灵
                                imageOffset: new BMap.Size(0, 0)    //图片的偏移量。为了是图片底部中心对准坐标点。
                            })
                        });
                    }
                    map.addOverlay(runMk);

                    _self.PolylineRun.RunInterval = _self.PolylineRun.RunInterval ? _self.PolylineRun.RunInterval : 500;
                    runMk.addEventListener(_self.PolylineRun.RunEvent,
                        function() {
                            map.setZoom(15);
                            resetMkPoint(0);
                        });
                    var paths = _self.Path.length; //获得有几个点
                    i = 0;

                    function resetMkPoint(i) {
                        runMk.setPosition(_self.Path[i]);
                        map.panTo(_self.Path[i]);
                        if (i < paths) {
                            setTimeout(function() {
                                i++;
                                resetMkPoint(i);
                            }, _self.PolylineRun.RunInterval);
                        }
                    }
                }
            }
        }
        //版权说明
        var copy = new BMap.CopyrightControl(BMAP_ANCHOR_BOTTOM_RIGHT, new BMap.Size(15, 15));
        copy.addCopyright(new BMap.Copyright(1, "@ 2012 动软卓越 (北京) 科技有限公司", map.getBounds()));
        map.addControl(copy); ////////--------
        if (CurrentOption.addEventListener) {
            if (CurrentOption.addEventListener.event && CurrentOption.addEventListener.callback) {
                map.addEventListener(CurrentOption.addEventListener.event,
                    function(e) {
                        CurrentOption.addEventListener.callback(e.point.lng, e.point.lat);
                    });
            }
        }
        map.addEventListener("click", function(e) {
            CurrentOption.MenuItem.MenuItemsetPoint.MenuClickCallBack(e.point.lng, e.point.lat);
        });
        map.setMinZoom(4); //设置地图允许的最小级别
        _baidumap = map;
    }

    window['baidumap'] = {};
    window['baidumap']['baidumapload'] = baidumapload;
} ()); 
//function SetMarkerPoint(lng, lat) {
//    $("[id$=MarkersLongitude]").val(lng);
//    $("[id$=MarkersDimension]").val(lat);
//}
//function InitMapOption() {
//    return {
//        Container: "MapContent",        //地图的容器
//        Longitude: undefined,           //经度
//        Dimension: undefined,           //纬度
//        Level: undefined,               //缩放级别
//        SearchCity: undefined,          //搜索城市
//        SearchArea: undefined,          //搜索地区
//        EnableOnlyMarker: true,
//        EnableKeyboard: true,           // 是否开启键盘 上下键
//        NavigationControl: true,        //是否有鱼骨工具（进行上下左右放大或小的图标）
//        ScaleControl: true,             //是否显示比例尺
//        MapTypeControl: true,           //是否有卫星地图等的图标
//        Markers: undefined,
//        MenuItem: {
//            MenuItemzoomIn: true,       // 是否产生放大地图按钮
//            MenuItemzoomOut: true,      // 是否产生缩小地图按钮
//            MenuItemsetZoomTop: true,   // 是否产生放大到最大级（最清晰）按钮
//            MenuItemsetZoomCanSeeCounty: true,
//            MenuItemsetPoint: {
//                EnableDragging: true,
//                SetAnimation: true,
//                MenuEvent: "dragend",
//                CallBack: function(lng, lat) {
//                    SetMarkerPoint(lng, lat);
//                },
//                MenuClickCallBack: function(lng, lat) {
//                    SetMarkerPoint(lng, lat);
//                    alert("添加标注成功, 请您填写地图下方的标注点信息.");
//                }
//            }
//        }
//        Polyline: {
//            Path: [new BMap.Point(116.399, 39.910), new BMap.Point(116.405, 39.920), new BMap.Point(116.425, 39.900)],       // 轨迹点的集合
//            StrokeColor: red,      // 轨迹颜色
//            StrokeWeight: 6,   //折线的宽度，以像素为单位。
//            StrokeOpacity: 0.5, //折线的透明度，取值范围0 - 1。
//            StrokeStyle: solid, //折线的样式，solid或dashed。
//            PolylineRun: {
//                IsRun: true,      //是否运动显示轨迹 默认是true
//                RunIcon: new BMap.Icon("/images/map1.gif", new BMap.Size(32, 70), {    imageOffset: new BMap.Size(0, 0)}),   //轨迹上运动的图标
//                RunEvent: "Click",
//                RunInterval: 1000
//            }
//        }
//    };
//}
