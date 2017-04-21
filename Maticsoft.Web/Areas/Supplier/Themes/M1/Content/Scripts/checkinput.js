function preventEvent(event)
{
	var is_ie	= false;
	if ((navigator.userAgent.indexOf("MSIE") != -1) && (parseInt(navigator.appVersion) >= 4)) is_ie = true;
	if(is_ie)
	{
		event.returnValue	= false;
	}
	else
	{
		event.preventDefault();
	}
}
function is_ctrl(code)
{
	return code == 88 || code == 8 || code == 0 || code == 13 || code == 17;
}
function is_number(code)
{
	if(code < 48) return false;
	if(code > 57) return false;
	return true;
}
function is_lower(code)
{
	if(code < 97)	return false;
	if(code > 122)	return false;
	return true;
}
function is_alpha(code)
{
	if(code < 65) return false;
	if(code > 122) return false;
	if(code > 90 && code < 97) return false;
	return true;
}
function is_alphanumber(code)
{
	if(is_alpha(code))	return true;
	if(is_number(code))	return true;
	return false;
}
function check_input(event, callback, maxlen, except)
{
	var src_el	= window.event?event.srcElement:event.target;
	var	keycode	= window.event?event.keyCode : event.which;
	if(is_ctrl(keycode)) return keycode;
	var selected_string	= '';
	if(window.getSelection)
	{
		selected_string	= window.getSelection().toString();
	}
	else
	{
		if(document.getSelection)
		{
			selected_string	= document.getSelection();
		}
		else
		{
			selected_string	= document.selection.createRange().htmlText;
		}
	}
	if((selected_string == '' || selected_string == undefined))
	{
		if(src_el.selectionStart != undefined && src_el.selectionEnd != undefined)
		{
			var start = src_el.selectionStart;
			var end	= src_el.selectionEnd;
			selected_string	= src_el.value.substring(start, end);
		}
	}
	if(maxlen > 0 && (selected_string == '' || selected_string == undefined))
	{
		var src_val	= src_el.value;
		if(src_val.length >= maxlen) preventEvent(event);
	}
	if(callback(keycode)) return keycode;
	if(except)
	{
		for(i = 0; i < except.length; ++ i)
		{
			if(keycode == except.charCodeAt(i))	return keycode;
		}
	}
	preventEvent(event);
}
/**
* 将一个字符串强制转型成数字
*@return integer
*/
String.prototype.toNumber	= function()
{
	var num	= this.replace(/^0+/g, '');
	num	= parseInt(num);
	if(isNaN(num))
	{
		return 0;
	}
	return num;
}
/**
* 将一个字符串强制转换成浮点型数字
* @return float
*/
String.prototype.toFloat	=	function()
{
	var	num	= parseFloat(this);
	if(isNaN(num))
	{
		return 0;
	}
	return num;
}
/**
* 检查一个日期是否合法
* @param year integer 年
* @param month	integer 月 1 ~ 12
* @param	date integer 日 1 ~ 31
* @return boolean 当日期合法，返回true；日期不合法则返回false
*/
function checkDate(year, month, date)
{
	var tmp_date	= new Date(month + '/' + date + '/' + year);
	return tmp_date.getFullYear() == year && tmp_date.getMonth() == (month - 1) && tmp_date.getDate() == date;
}
/**
* 从一个身份证中解析出生日
* @param card string 身份证号
* @return object|false   当身份证日期不合法时，返回 false,否则，返回 object
*/
function parseDateFromId(card)
{
	var year	= 0;
	var month	= 0;
	var	date	= 0;
	if(card.length == 15)
	{
		year	= ('19' + card.substr(6, 2)).toNumber();
		month	= card.substr(8, 2).toNumber();
		date	= card.substr(10, 2).toNumber();
		if(!checkDate(year, month, date))	return false;
	}
	else
	{
		if(card.length == 18)
		{
			year	= card.substr(6, 4).toNumber();
			month	= card.substr(10,2).toNumber();
			date	= card.substr(12, 2).toNumber();
			if(!checkDate(year, month, date)) return false;
		}
	}
	if(year == 0)	return false;
	return {year_t:year, month_t:month, date_t:date};
}
/**
* 从比较宽松的格式中解析出日期，现在支持  1984-08-05、1987-8-2、19840805 格式
* @param date string字符串格式的日期
* @return object|false 当日期不合法时，返回false；否则返回object包含年月日信息
*/
function parseDateRlexFormat(date)
{
	var date_info	= [];
	if(/^[0-9]{4}-[0-9]?[0-9]-[0-9]?[0-9]$/.test(date))
	{
		date_info	= date.split('-', 3);
	}
	else
	{
		if(/^[0-9]{8}$/.test(date))
		{
			date_info[0]	= date.substr(0,4);
			date_info[1]	= date.substr(4,2);
			date_info[2]	= date.substr(6,2);
		}
	}
	if(date_info.length != 3)	return false;
	for(i = 0; i < 3; ++ i)
	{
		date_info[i]	= date_info[i].toNumber();
		if(date_info[i] == 0)	return false;
	}
	if(!checkDate(date_info[0], date_info[1], date_info[2]))	return false;
	return {year_t:date_info[0], month_t:date_info[1], date_t:date_info[2]};
}
/**
* 验证15位身份证
* @param card string 要验证的身份证号
* @param birth string optional 相应的日期
* @return integer 返回1代表合法；0代表身份证生日出错；-1代表生日和身份证生日不一致
*/
function checkIdCard15(card, birth)
{
	var card_date	= parseDateFromId(card);
	if(!card_date)	return 0;//身份证日期不合法
	if(birth)
	{
		var birth_date	= parseDateRlexFormat(birth);
		if(birth_date)
		{
			if(card_date.year_t != birth_date.year_t || card_date.month_t != birth_date.month_t || card_date.date_t != birth_date.date_t)	return -1;//生日和身份证不一致
		}
	}
	return 1;
}
/**
* 验证 18 位新身份证
* @param card string 要验证的身份证
* @param birth string 响应的日期
* @return integer 返回1代表合法；0代表身份证生日出错；-1代表生日和身份证生日不一致
*/
function checkIdCard18(card, birth)
{
	var card_date	= parseDateFromId(card);
	if(!card_date)	return 0;//身份证日期不合法
	if(birth)
	{
		var birth_date	= parseDateRlexFormat(birth);
		if(birth_date)
		{
			if(card_date.year_t != birth_date.year_t || card_date.month_t != birth_date.month_t || card_date.date_t != birth_date.date_t)	return -1;//生日和身份证不一致
		}
	}
	//额外的校验位校验
	var weight	= [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
	var crc_sum	= 0;
	for(i = 0; i < 17; ++ i)
	{
		crc_sum += card.charAt(i).toNumber() * weight[i];
	}
	crc_sum	= crc_sum % 11;
	switch(crc_sum)
	{
		case 0:
			key	= '1';
			break;
		case 1:
			key	= '0';
			break;
		case 2:
			key	= 'x';
			break;
		case 3:
			key	= '9';
			break;
		case 4:
			key	= '8';
			break;
		case 5:
			key = '7';
			break;
		case 6:
			key = '6';
			break;
		case 7:
			key = '5';
			break;
		case 8:
			key = '4';
			break;
		case 9:
			key = '3';
			break;
		case 10:
			key = '2';
			break;
		default:
			return 0;
	}
	if(key == card.charAt(17))	return 1;
	return 0;
}
/**
* 检查一个身份证号是否合法
* @param card string 要检查的身份证号
* @param birth string 生日，可选，当生日不合法时，会被忽略
* @return integer 返回 0 则表示身份证不合法, -1 表示身份证、生日不匹配， 1则表示验证通过
*/
function checkIdCard(card, birth)
{
	card	= card.toLowerCase();
	if(!/^[1-9]{1}[0-9]{14}$|^[1-9]{1}[0-9]{16}([0-9]{1}|x)$/i.test(card))	return 0;
	if(card.length == 15)	return checkIdCard15(card, birth);
	if(card.length == 18)	return checkIdCard18(card, birth);
	return 0;
}

/**
* 通过 element 的name属性查找一系列元素
* @param name string 要查找的元素名称
* @return object[] 元素数组
*/
function getInputsByName(name)
{
	var dt	= document.getElementsByName(name);
	if(dt.length > 0) return dt;
	dt	= [];
	var inputs	= document.getElementsByTagName('input');
	for(i = 0; i < inputs.length; ++i)
	{
		if(inputs[i].name == name)
		{
			dt[dt.length]= inputs[i];
		}
	}
	return dt;
}

/**
* 检查一系列的 checkBox 是否有一个已经被选中了
* 形如 <input type='checkbox' name='ch[]' value='a' /> <input type='checkbox' name='ch[]' value='b' />的检查
* @param name string 用于检查的checkbox名称
* @return boolean 为真则表示至少有一个已经被选中，否则没有任何一个被选中
*/
function hasChecked(name)
{
	var checkBoxes	= getInputsByName(name);
	for(i = 0; i < checkBoxes; ++ i)
	{
		if(checkBoxes[i].checked)	return true;
	}
	return false;
}

/**
* 本函数对应于 PHP中的 同名函数，只是日期格式固定些
* 支持 YYYY-MM-DD YYYYMMDD格式的时间
* @param string t  要转换的日期
* @return long  转换后的时间戳
*/
function strtotime(t)
{
	var parsed_date	= parseDateRlexFormat(t);
	var dt	= new Date(parsed_date.month_t + '/' + parsed_date.date_t + '/' + parsed_date.year_t);
	return dt.getTime() / 1000;
}