

function InputFocus(input_id) {
    input_id.select();
    input_id.style.border = "solid 2px #999";
}
function InputBlur(input_id) {
    input_id.style.borderWidth = '0px';
}

var objtable = document.getElementById("tableDatagrid");
var datastartnum = objtable.rows.length;

function goto(input_id) {
    //跳上面  ALT+CTRL
    if (event.altKey == true && event.shiftKey == true) {
        //document.getElementById("").focus();
        return;
    }
    //跳下面 ALT+SHIFT
    if (event.altKey == true && event.ctrlKey == true) {
        //document.getElementById("").focus();
        return;
    }

    var curcol = input_id.parentNode.cellIndex;
    var precol = curcol - 1;
    var nextcol = curcol + 1;
    var currow = input_id.parentNode.parentNode.rowIndex;
    var uprow = currow - 1;
    var downrow = currow + 1;

    if (objtable.rows[currow].cells[nextcol] != null) {
        var nextbox = objtable.rows[currow].cells[nextcol].firstChild;
        if (event.keyCode == 13 || event.keyCode == 39) {
            if (nextbox != null && nextbox.type == "text") {
                nextbox.focus();
            }
        }
    }

    if (objtable.rows[downrow] != null) {
        var downbox = objtable.rows[downrow].cells[curcol].firstChild;
        if (event.keyCode == 40) {
            if (downbox != null && downbox.type == "text") {
                downbox.focus();
            }
        }
    }

    //当前一个单元格存在，并且不是序号列
    if (objtable.rows[currow].cells[precol] != null && precol >= 1) {
        var prebox = objtable.rows[currow].cells[precol].firstChild;
        if (event.keyCode == 37) {
            if (prebox != null && prebox.type == "text") {
                prebox.focus();
            }
        }
        if (event.keyCode == 8) {
            if (prebox != null && prebox.type == "text") {
                if (this.value == "") prebox.focus();
            }
        }
    }

    //当上一行存在，并且不是标题行
    if (objtable.rows[uprow] != null) {
        var upbox = objtable.rows[uprow].cells[curcol].firstChild;
        if (event.keyCode == 38) {
            if (upbox != null && upbox.type == "text") {
                upbox.focus();
            }
        }
    }
}