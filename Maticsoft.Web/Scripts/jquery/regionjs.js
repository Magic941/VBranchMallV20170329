/* File Created: 四月 11, 2012 */

function getCitys(ddlProvince) {
//    var gerIDArr = ddlProvince.id.split("_");
//    var gerID = gerIDArr[0].toString() + "_";
    var HFID = "[id$=HiddenField_SelectValue]";
    $(HFID).val("");
    $.ajax({
        url: "/RegisterValidate.aspx",
        type: "post",
        dataType: 'text',
        timeout: 10000,
        async: true, // 同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
        data: { action: "getCitys", ProvinceID: $(ddlProvince).find("option:selected").val() },
        success: function (jsonData) {
            var cityid = "[id$=ddlCity]";
            var areaId = "[id$=ddlArea]";
            $(cityid).empty();
            $(areaId).empty();
            var option0 = $("<option></option>");
            option0.text("请选择");
            option0.val("0");
            var option1 = $("<option></option>");
            option1.text("请选择");
            option1.val("0");
            $(cityid).append(option0);
            $(areaId).append(option1);
            if ($(ddlProvince).find("option:selected").val() != "0" && jsonData != "") {
                var objCitys = eval("(" + jsonData + ")");
                $.each(objCitys.ds, function (i, city) {
                    var option = $("<option></option>");
                    option.text(city.RegionName);
                    option.val(city.RegionId);
                    $(cityid).append(option);
                });
            }
        }
    });
}

function getAreas(ddlCity) {
//    var gerIDArr = ddlCity.id.split("_");
//    var gerID = gerIDArr[0].toString() + "_";
    var areaid = "[id$=ddlArea]";
    var HFID = "[id$=HiddenField_SelectValue]";
    var selectCity = $(ddlCity).find("option:selected").val();
    if (selectCity == "0") {
        $(HFID).val("");
    } else {
        $(HFID).val(selectCity);
    }
    $("[id$=HiddenField_OldValue]").val($(ddlCity).find("option:selected").val());
    //alert($(HFID).val());
    $.ajax({
        url: "/RegisterValidate.aspx",
        type: "post",
        dataType: 'text',
        timeout: 10000,
        async: true, // 同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
        data: { action: "getAreas", CityID: $(ddlCity).find("option:selected").val() },
        success: function (jsonData) {
            $(areaid).empty();
            var option0 = $("<option></option>");
            option0.text("请选择");
            option0.val("0");
            $(areaid).append(option0);
            if ($(ddlCity).find("option:selected").val() != "0" && jsonData != "") {
                var objCitys = eval("(" + jsonData + ")");
                $.each(objCitys.ds, function (i, city) {

                    var option = $("<option></option>");

                    option.text(city.RegionName);
                    option.val(city.RegionId);

                    $(areaid).append(option);
                });
            }
        }
    });
}

function getAreasID(ddlArea) {
//    var gerIDArr = ddlArea.id.split("_");
//    var gerID = gerIDArr[0].toString() + "_";
    var HFID = "[id$=HiddenField_SelectValue]";
    var selectArea = $(ddlArea).find("option:selected").val();
    if (selectArea == "0") {
        $(HFID).val($("[id$=HiddenField_OldValue]").val());
    } else {
        $(HFID).val(selectArea);
    }
}