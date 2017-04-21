

function InputFocus(input_id) {
    input_id.select();
    input_id.style.border = "solid 2px #999";
}
function InputBlur(input_id) {
    input_id.style.borderWidth = '0px';
}


function OTable(v_id){
    this.id = v_id;
    var objtable = document.getElementById(v_id);    
    var datastartnum = objtable.rows.length;    
    this.hasSort = true;//第一列是否为排序列
    this.columnCount = 1; //表格总列数
    this.columnSumNum = 1;
    this.columnWidths = new Array("50");//每列中输入框的宽度
    this.sortColor = "#e4ecf7"; //序号的底色

    //初始化表格，v_xmldata为xml结构的数据；v_classname为行的样式名称。
    this.init = function (v_xmldata, v_classname) {
        var oDoc = GetXmlDoc(v_xmldata);
        var root = oDoc.documentElement;

        for (var i = 0; i < root.childNodes.length; i++) {
            var row = objtable.insertRow();
            row.className = v_classname;

            var colstart = 0;
            if (this.hasSort) {
                var cell0 = row.insertCell();
                cell0.innerHTML = objtable.rows.length - datastartnum;
                cell0.style.textAlign = "center";
                cell0.style.width = this.columnWidths[0];
                cell0.style.backgroundColor = this.sortColor;
                colstart = 1;
            }            
            for (var col = colstart; col < this.columnCount; col++) {
                var cell1 = row.insertCell();

                var w = this.columnWidths[col];
                var objinput = document.createElement("input");
                objinput.style.borderWidth = "0px";
                objinput.style.width = w;
                objinput.style.textAlign = "right";
                objinput.value = root.childNodes[i].childNodes[col - colstart].text;
                objinput.onfocus = function () { this.select(); this.style.border = "solid 2px #999"; };
                objinput.onblur = function () { this.style.borderWidth = '0px'; };
                objinput.onkeydown = this.goto;
                cell1.appendChild(objinput);
            }
        }
    }
	
	//初始化序号，一般不需要，当序号顺序出现问题的时候，可以使用
	this.initSort = function(){
		if(this.hasSort){
			for(var i=datastartnum;i<objtable.rows.length;i++){
				objtable.rows[i].cells[0].innerHTML = i + 1 - datastartnum;
			}
		}
	}

	this.goto = function () {
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
	    var curcol = this.parentNode.cellIndex;
	    var precol = curcol - 1;
	    var nextcol = curcol + 1;
	    var currow = this.parentNode.parentNode.rowIndex;
	    var uprow = currow - 1;
	    var downrow = currow + 1;

	    if (objtable.rows[currow].cells[nextcol] != null) {
	        var nextbox = objtable.rows[currow].cells[nextcol].firstChild;
	        if (event.keyCode == 13 || event.keyCode == 39) {
	            
	            if (nextbox != null) {
	                nextbox.focus();
	            }
	        }
	    }

	    if (objtable.rows[downrow] != null) {
	        var downbox = objtable.rows[downrow].cells[curcol].firstChild;
	        if (event.keyCode == 40) {
	            if (downbox != null) {
	                downbox.focus();
	            }
	        }
	    }

	    //当前一个单元格存在，并且不是序号列
	    if (objtable.rows[currow].cells[precol] != null && precol >= 1) {
	        var prebox = objtable.rows[currow].cells[precol].firstChild;
	        if (event.keyCode == 37) {
	            if (prebox != null) {
	                prebox.focus();
	            }
	        }
	        if (event.keyCode == 8) {
	            if (prebox != null) {
	                if (this.value == "") prebox.focus();
	            }
	        }
	    }

	    //当上一行存在，并且不是标题行
	    if (objtable.rows[uprow] != null && uprow >= datastartnum) {
	        var upbox = objtable.rows[uprow].cells[curcol].firstChild;
	        if (event.keyCode == 38) {
	            if (upbox != null) {
	                upbox.focus();
	            }
	        }
	    }
	}
    
    //在表格中增加一行，v_classname为行的样式名称。
    this.addRow = function(v_classname){
        var row = objtable.insertRow();
        row.className = v_classname;
        
        var colstart = 0;
        if(this.hasSort){
            var cell0 = row.insertCell();
            cell0.innerHTML = objtable.rows.length - datastartnum;
            cell0.style.textAlign = "center";
			cell0.style.width = this.columnWidths[0];
			cell0.style.backgroundColor = this.sortColor;
            colstart = 1;
        }
        
        for(var col=colstart;col<this.columnCount;col++){
            var cell1 = row.insertCell();
            var w = this.columnWidths[col];
            if(w == null) w = 100;
			
			var objinput = document.createElement("input");
			objinput.style.borderWidth = "0px";
			objinput.style.width = w;
			objinput.onfocus = function(){this.select();};
			objinput.onkeydown = this.goto;
			cell1.appendChild(objinput);
        }
    }
	
	//删除最后一行
	this.deleteRow = function(){
		if(objtable.rows.length <= datastartnum) return;
		
		objtable.deleteRow(objtable.rows.length - 1);
	}
	
	//删除指定索引的行
	this.deleteRowAt = function(v_rowindex){
		if(objtable.rows.length <= datastartnum) return;
		if(v_rowindex < 0) return;
		objtable.deleteRow(v_rowindex + datastartnum);
		
		//重新排序
		this.initSort();
	}
	
	//删除所有行
	this.deleteAllRow = function(){
	    for(var i=objtable.rows.length - 1;i>=datastartnum;i--){
	        objtable.deleteRow(i);
	    }
	}
    
    //获取表格中数据，为XML结构的数据。
	this.getData = function () {	    
	    var colstart = 0;
	    if (this.hasSort) colstart = 1;	    
	    var tmpstr = "<?xml version='1.0' encoding='gb2312'?><data>";
	    for (var i = datastartnum; i < objtable.rows.length; i++) {
	        var table = "<row>";
	        var nullrow = true; //假设空行
	        for (var col = colstart; col < this.columnCount; col++) {
	            var input = objtable.rows[i].cells[col].firstChild;
	            var v = input.value;
	            if (v.length > 0) nullrow = false; //如果这行有输入，则不是空行
	            table += "<col" + col.toString() + ">" + v + "</col" + col.toString() + ">";
	        }
	        table += "</row>";
	        //如果不是空行，则累加数据
	        if (nullrow == false) tmpstr += table;
	    }

	    tmpstr += "</data>";
	    return tmpstr;
	}
    	

}