/*
# copyright c by zhangxinxu 2009-10-27
更多内容请访问http://www.zhangxinxu.com
*/

(function($){
	$.fn.extend({
		tabChange:function(type){
			return this.click(function(){			
				if($(this).hasClass("fri_num_on")){
					return;
				}else{
					option.selectType = type;
					$("a",$(this).parent()).removeClass("fri_num_on");	
					$(this).addClass("fri_num_on");
					friFilter();
					return false;
				}
			});	
		},
		friChoose:function(){
			return this.each(function(){
				$(this).click(function(){
					if($(this).hasClass("zxx_fri_on")){
						$(this).removeClass("zxx_fri_on");
						option.selectNum-=1;
						option.unSelectNum+=1;
						if(option.selectType === 2){
							$(this).parent().hide();	
						}
					}else{
						$(this).addClass("zxx_fri_on");	
						option.selectNum+=1;
						option.unSelectNum-=1;
						if(option.selectType === 3){
							$(this).parent().hide();
						}
					}
					option.operateObject.selectNum.text(option.selectNum);
					option.operateObject.unSelectNum.text(option.unSelectNum);
					return false;
				});						  
			});		
		}
	});
	var option = {
		selectNum:0,
		unSelectNum:15,
		selectType:1,
		operateObject:{}	
	};
	var bindEvent = function(){
		$("#zxxFriMain").find("a.zxx_fri_a").friChoose();
		$("#friSelectMenu").find("a#friSelectAll").tabChange(1);
		$("#friSelectMenu").find("a#friHaveSelected").tabChange(2);
		$("#friSelectMenu").find("a#friUnSelected").tabChange(3);
		option.operateObject.selectNum = $("#selectNum");
		option.operateObject.unSelectNum = $("#unSelectNum");
	}();
	var friFilter = function(){
		if(option.selectType === 1){
			$("#zxxFriMain").find("a.zxx_fri_a").parent().show();
		}else if(option.selectType === 2){
			$("#zxxFriMain").find("a.zxx_fri_on").parent().show();
			$("#zxxFriMain").find("a:not(.zxx_fri_on)").parent().hide();
		}else{
			$("#zxxFriMain").find("a.zxx_fri_on").parent().hide();
			$("#zxxFriMain").find("a:not(.zxx_fri_on)").parent().show();
		}
	};	
	function error(m){
		alert(m);
	}
})(jQuery);