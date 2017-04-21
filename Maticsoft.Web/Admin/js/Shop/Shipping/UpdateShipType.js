$(".OnlyFloat").OnlyFloat();

//初始化地区Select option
//$('.selectregion:input').empty().html($('select[id$=drpRegion] option').clone());

//注册select2
var Select2App = angular.module('RegionApp', ['ui.select2']);

var BaseRegions = (function () {
    var data = [];
    $('select[id$=drpRegion] option').each(function () {
        data.push({ id: $(this).val(), text: $(this).text() });
    });
    return data;
})();

//$('.selectregion:input,select[id$=drpRegion]').live("change", function (e) {
//    if (!e.added && !e.removed) return;

//    if (e.added) {
//        $('.selectregion:input option,select[id$=drpRegion] option').filter('[value=' + e.added.id + ']').remove();
//    }
//    
//    if (e.removed) {
////        $('.selectregion:input,select[id$=drpRegion]').append();
//    }
//    
//    //log("change "+JSON.stringify({val:e.val, added:e.added, removed:e.removed}));
//});



//angular RegionCtrl 控制器
function RegionCtrl($scope) {
    //    $scope.select2Options = {
    //        initSelection: function (element, callback) {
    //            var data = [];
    //            $(element.val().split(",")).each(function () {
    //                var item = this;
    //                $scope.regions.BaseRegions.slice(0).each(function() {
    //                    if (this.id == item) {
    //                        data.push(this);
    //                    }
    //                });
    //            });
    //            callback(data);
    //        },
    //        query: function (options) {
    //            var data = { results: $scope.regions.BaseRegions.slice(0) };
    //            if ($scope.regions && $scope.regions.length > 0) {
    //                var removeKey = [];
    //                $(data.results).each(function (index) {
    //                    for (var i = 0; i < $scope.regions.length; i++) {
    //                        if ($.inArray(this.id, $scope.regions[i].ids) > -1) {
    //                            removeKey.push(index);
    //                        }
    //                    }
    //                });

    //                if (removeKey && removeKey.length > 0) {
    //                    for (var i = 0; i < removeKey.length; i++) {
    //                        data.results.remove(removeKey[i]);
    //                    }
    //                }
    //            }
    //            options.callback(data);
    //        },
    //        formatSelection: function (item) {
    //            return item.text;
    //        },
    //        formatResult: function (item) {
    //            return item.text;
    //        }
    //    };


    //Model 及默认值
    $scope.regions = $('[id$=hfRegionData]').val() ? jQuery.parseJSON($('[id$=hfRegionData]').val()) : [];

    $scope.regions.BaseRegions = BaseRegions.slice(0);

    //Add
    $scope.addRegion = function () {
        if (!$scope.ids || $scope.ids.length == 0) {
            alert('请选择地区'); return;
        }
        if (!$scope.price) {
            alert('请输入起步价'); return;
        }

        $scope.regions.push({ ids: $scope.ids, price: parseFloat($scope.price).toFixed(2),
            addprice: $scope.addprice?parseFloat($scope.addprice).toFixed(2):''
        });
        //        $('.selectregion:input:last').empty().html($('select[id$=drpRegion] option').clone());

        $scope.price = '';
        $scope.ids = '';
        $scope.addprice = '';
    };

    //remove
    $scope.removeRegion = function (item) {
        for (var i = 0; i < $scope.regions.length; i++) {
            if (item && item.$$hashKey == $scope.regions[i].$$hashKey) {
                $scope.regions.remove(i);
                return;
            }
        }
    };
}

