window.onerror = function(){
	// return true;
}

jQuery(function($){

var slider = function(options){

		/* 补丁 */
		$('.vjs-menu-item, .vjs-menu-title').remove();

		var container = $(options.container),
			dir = options.dir || 'left',
			type = options.type == 'special' ? 1 : 0,
			move = $(options.move),
			items = $(options.items),
			menus = $(options.menus),
			pointer = $(options.pointer),
			css = options.css || 'active',
			distance = options.distance || items.eq(0).outerWidth(),
			speed = options.speed || 'slow',
			time = options.time || 5000,
			autorun = options.autorun === false ? false : true,
			full = options.full || false,
			count = 0,
			callCount = 0,
			max = options.max || items.length - 1,
			effect = function(){
				if( dir == 'left' ){
					move.stop().animate({
						left: -	count * distance
					}, speed);
				}
				if( dir == 'top' ){
					
					if( type ){
						
						var __items = $(options.items);
						
						callCount = count;
						
						if( count == 1 ){
							__items.eq(0).before( __items.eq( __items.length - 1 ) );
							
							move.css({
								top: -distance
							});
							
							move.stop().animate({
								top: 0
							}, speed);
						}
						
						if( count == max ){
							
							move.stop().animate({
								top: -distance
							}, speed, function(){
								__items.eq(__items.length - 1).after( __items.eq(0) );
								move.css({
									top: 0
								});
							});
						}
						
						count = 0;
						
					}
				}
				
				if( pointer.length ){
					pointer.removeClass( css ).addClass('none').eq( count ).removeClass('none').addClass( css );
				}
				
				if( options.callback ) options.callback( callCount );
			},
			run = function(){
				count = count < max ? count + 1 : 0;
				effect();
			},
			interval;

		if( autorun ){
			interval = setInterval(run, time);
		}

		container
			.on('mouseover', function(){
				clearInterval(interval);
			})
			.on('mouseout', function(){
				if( autorun ){
					interval = setInterval(run, time);
				}
			});
		
		if( items.length > 1 ){
			if( pointer.length ){
				pointer.on('mouseover', function(){
					count = pointer.index( this );
					effect();
				});
			}
			
			menus.bind('click', function(){
				count = menus.index( this ) ? count + 1 : count - 1;
				if( count < 0 ){
					count = max;
				}
				if( count > max ){
					count = 0;
				}
				effect();
				
			});
			
			if( type ){
				items.on('click', function(){
					
					var index = $(options.items).index( this );
					var vhtml=$(".videos dd li").eq(index).html();
					$(".videos dd li").eq(index).html(vhtml);
					switch(index){
						case 0:
							count = 1;
							break;
						case 2:
							count = max;
							break;
						case 1:
							count = 0;
							break;
					}
					
					effect();
				});
			}
		}
		else{
			pointer.hide();
			menus.hide();
		}
		
		if( full ){
			var	win = $(window),
				doResize = !function(){
					win.resize(function(e){
						var winWidth = win.width();
						container.height( 690 * ( winWidth / 1600 ) );
						distance = winWidth;
						items.width( winWidth );
					}).trigger('resize');
				}();
		}
	},

	doClick = function(options){
		var menus = $(options.menus),
			targets = $(options.targets),
			clickHandle = function(i){
				targets.hide().eq( menus.index( this == window ? i : this ) ).show();
			}
		menus.on('click', clickHandle);
		clickHandle( menus.eq(0).get(0) );
	};

doClick({
	menus: '[triggerClick=true]',
	targets: '[clickContent=true]'
}),

slider({
	container: '.banner',
	move: '.banner-pic ul',
	items: '.banner-pic li',
	menus: '.banner-title a.prev, .banner-title a.next',
	pointer: '.banner-title li',
	css: 'active',
	speed: 'slow',
	time: 5000
//	full: true
}),
slider({
	container: '.piclist',
	move: '.piclist ul',
	items: '.piclist li',
	menus: '.piclist span.prev, .piclist span.next',
	/*
	distance: 820,
	max: Math.floor( $('.piclist li').length / 4 ),
	// pointer: '.piclist li',
	*/
	max: $('.piclist li').length - 4,
	css: 'active',
	speed: 'slow',
	time: 5000
}),

// tabs
	$.extend({
		tabs: function(options){
			if( !options ) return;
			options.event = options.event || 'click';
			options.css = options.css || 'active';
			options.menus.on(options.event, function(){
				(function(index){
				options.contents.hide().eq(index).show();
				options.menus.removeClass(options.css).eq(index).addClass(options.css);
			if( options.callback ) options.callback(index);
				})( options.menus.index(this) );
			}).eq(0).trigger(options.event);
		}
	})
	
	$.tabs({
		menus: $('.wallet .wallet_nav li'),
		contents: $('.wallet .write_box .writeList'),
		css: 'active'
	})
	
/* floater */
jQuery(function($){

	(function(ad){
		ad.css({
			position: 'fixed',
			left: '50%',
			top: 200,
			zIndex: 20,
			marginLeft: 512
		});
		$('body').children('.w').append( ad );
		if( ~navigator.userAgent.indexOf('MSIE 6') !== 0 ){
			$(window).scroll(function(){
				var winTop = $(window).scrollTop();
				ad.css({
					position: 'absolute',
					top: winTop + 300
				});
			}).trigger('scroll');
		}
	})(
		$('.floater')
	);

});

	/* backtop */
	var win = $(window), backtop = $('.backtop');
	backtop.css({
			position: 'fixed',
			left: '50%',
			top: 100,
			zIndex: 20,
			marginLeft: 465
		});
	backtop.on('click', function(){
		win.scrollTop(0);
	});
	win.scroll(function(){
		win.scrollTop() > 0 ? backtop.show() : backtop.hide();
	}).trigger('scroll');

	
	/* Textarea Date-Max */
	(function(text){
	
		var target = $(text), max = Number(target.attr('data-max'));
		target.on('keyup', function(e){
			var value = target.val();
			if( value.length > max ){
				target.val( value.substr(0, max) );
			}
		});
	
	})('textarea[data-max]');
	
	

	/* floater */
	function tabs(button, target, fn){
		button.click(function(){
			~function(index){
				target.hide().eq( index ).show();
				if(fn) fn( button, target, index );
			}( button.index(this) );
		}).eq(0).trigger('click');
	}
	tabs( $('.contable dt li'), $('.contable dd li'), function(button, target, index){
		button.removeAttr('class').eq(index).addClass('line');
	});

});

