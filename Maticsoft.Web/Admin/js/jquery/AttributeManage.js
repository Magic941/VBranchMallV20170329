/* File Created: 六月 19, 2012 */
function deleteAttributeValue(objValue, valueId) {
    $.ajax({
        url: "/Shopmanage.aspx",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        data: {
            action: "EditValue",
            ValueId: valueId
        },
        async: false,
        success: function (data) {
            if (data.STATUS == "SUCCESS") {
                $("#span" + valueId).hide();
            } else {
                alert("此属性值有商品在使用，删除失败");
            }
        }
    });
}