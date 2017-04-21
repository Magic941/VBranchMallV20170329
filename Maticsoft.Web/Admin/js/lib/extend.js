
//浮点型数据数据编辑
$.ligerDefaults.Grid.editors['numberbox'] = {
    create: function (container, editParm) {
        var column = editParm.column;
        var precision = column.editor.precision;
        var input = $("<input type='text' style='text-align:right' class='l-text' />");
        input.bind('keypress', function (e) {
            var keyCode = window.event ? e.keyCode : e.which;
            return keyCode >= 48 && keyCode <= 57 || keyCode == 46 || keyCode == 8;
        });
        input.bind('blur', function () {
            var value = input.val();
            input.val(parseFloat(value).toFixed(precision));
        });
        container.append(input);
        return input;
    },
    getValue: function (input, editParm) {
        return parseFloat(input.val());
    },
    setValue: function (input, value, editParm) {
        var column = editParm.column;
        var precision = column.editor.precision;
        input.val(value.toFixed(precision));
    },
    resize: function (input, width, height, editParm) {
        input.width(width).height(height);
    }
};

//扩展 numberbox 类型的格式化函数
$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

//数字类型数据编辑
function formatCurrency(num) {
    if (!num) return "0.00";
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0.00";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + '' + num + '.' + cents);
}