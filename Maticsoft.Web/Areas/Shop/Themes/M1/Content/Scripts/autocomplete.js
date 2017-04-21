(function($) {
	function Autocomplete(el, options) {
		this.el = $(el);
		this.el.attr('autocomplete', 'off');
		this.suggestions = [];
		this.selectedIndex = -1;
		this.currentValue = this.el.val();
		this.intervalId = 0;
		this.cachedResponse = [];
		this.onChangeInterval = null;
		this.ignoreValueChange = false;
		this.serviceUrl = options.serviceUrl;
		this.options = {
			autoSubmit: false,
			minChars: 1,
			maxHeight: 300,
			deferRequestBy: 0,
			width: 0,
			highlight: true,
			params: {},
			delimiter: null,
			zIndex: 9999
		};
		this.initialize();
		this.setOptions(options);
	}
	
	$.fn.autocomplete = function(options) {
		return new Autocomplete(this.get(0)||$('<input />'), options);
	};


	Autocomplete.prototype = {

		killerFn: null,

		initialize: function() {
			var me, uid, autocompleteElId;
			me = this;
			uid = Math.floor(Math.random()*0x100000).toString(16);
			autocompleteElId = 'Autocomplete_' + uid;

			this.killerFn = function(e) {
				if ($(e.target).parents('.autocomplete').size() === 0) {
					me.killSuggestions();
					me.disableKillerFn();
				}
			};

			if (!this.options.width) { this.options.width = this.el.width(); }
			this.mainContainerId = 'AutocompleteContainter_' + uid;

			$('<div id="' + this.mainContainerId + '" style="position:absolute;z-index:9999;"><ul name="__DROPSEARCH" class="autocomplete" id="' + autocompleteElId + '" style="display:none;"></ul></div>').appendTo('body');

			this.container = $('#' + autocompleteElId);
			this.fixPosition();
			if (window.opera) {
				this.el.keypress(function(e) { me.onKeyPress(e); });
				//fix opera don't supprot keyup event
				this.el.focus(function() {
					this.onChangeInterval = setInterval(function() {if (me.currentValue !== me.el.val()){me.onValueChange();};}, me.options.deferRequestBy);
				});
				this.el.blur(function() { clearInterval(this.onChangeInterval);});
			} else {
				this.el.keydown(function(e) { me.onKeyPress(e); });
			}
			this.el.keyup(function(e) { me.onKeyUp(e); });
			this.el.blur(function() { me.enableKillerFn(); });
			this.el.focus(function() { me.fixPosition(); });
		},
		
		setOptions: function(options){
			var o = this.options;
			$.extend(o, options);
			$('#'+this.mainContainerId).css({ zIndex:o.zIndex });
			this.container.css({ maxHeight: o.maxHeight + 'px', width:o.width });
		},
		
		clearCache: function(){
			this.cachedResponse = [];
			window.sessionStorage.clear();
		},
		
		disable: function(){
			this.disabled = true;
		},
		
		enable: function(){
			this.disabled = false;
		},

		fixPosition: function() {
			var offset = this.el.offset();
			$('#' + this.mainContainerId).css({ top: (offset.top + this.el.innerHeight()) + 'px', left: offset.left + 'px' });
		},

		enableKillerFn: function() {
			var me = this;
			$(document).bind('click', me.killerFn);
		},

		disableKillerFn: function() {
			var me = this;
			$(document).unbind('click', me.killerFn);
		},

		killSuggestions: function() {
			var me = this;
			this.stopKillSuggestions();
			this.intervalId = window.setInterval(function() { me.hide(); me.stopKillSuggestions(); }, 300);
		},

		stopKillSuggestions: function() {
			window.clearInterval(this.intervalId);
		},

		onKeyPress: function(e) {
			if (this.disabled || !this.enabled) { return; }
			// return will exit the function
			// and event will not be prevented
			var keyCode;
			if(window.event){
					keyCode=event.keyCode;
				}
			else if(e.which){
				keyCode=e.which;
			} 
			switch (keyCode) {
				case 27: //KEY_ESC:
					this.el.val(this.currentValue);
					this.hide();
					break;
				case 9: //KEY_TAB:
				case 13: //KEY_RETURN:
					if (this.selectedIndex === -1) {
						this.hide();
						return;
					}
					this.select(this.selectedIndex,keyCode);
					if(keyCode === 9){ return; }
					break;
				case 38: //KEY_UP:
					this.moveUp();
					break;
				case 40: //KEY_DOWN:
					this.moveDown();
					break;
				default:
					return;
			}
			e.stopImmediatePropagation();
			e.preventDefault();
		},

		onKeyUp: function(e) {
			if(this.disabled){ return; }
			switch (e.keyCode) {
				case 38: //KEY_UP:
				case 40: //KEY_DOWN:
					return;
			}
			clearInterval(this.onChangeInterval);
			if (this.currentValue !== this.el.val()) {
				if (this.options.deferRequestBy > 0) {
					// Defer lookup in case when value changes very quickly:
					var me = this;
					this.onChangeInterval = setInterval(function() { me.onValueChange(); }, this.options.deferRequestBy);
				} else {
					this.onValueChange();
				}
			}
		},

		onValueChange: function() {
			clearInterval(this.onChangeInterval);
			this.currentValue = this.el.val();
			var q = this.getQuery(this.currentValue);
			this.selectedIndex = -1;
			if (this.ignoreValueChange) {
				this.ignoreValueChange = false;
				return;
			}
			if (q === '' || q.length < this.options.minChars) {
				this.hide();
			} else {
				this.getSuggestions(q);
			}
		},

		getQuery: function(val) {
			var d, arr;
			d = this.options.delimiter;
			if (!d) { return $.trim(val); }
			arr = val.split(d);
			return $.trim(arr[arr.length - 1]);
		},

		getSuggestions: function(q) {
			var cr, me;
			if(window.sessionStorage&&!(typeof(JSON)=="undefined")){
				cr=sessionStorage.getItem(q) ? JSON.parse(sessionStorage.getItem(q)) : '';
			}
			else{
				cr=this.cachedResponse[q];
			}
			if (cr && $.isArray(cr.suggestions)) {
				this.suggestions = cr.suggestions;
				this.suggest();
			} else{
				me = this;
				me.options.params.keyword = q;
				$.get(this.serviceUrl, me.options.params, function(txt) { me.processResponse(txt); }, 'jsonp');
				
			}
		},

		hide: function() {
			this.enabled = false;
			this.selectedIndex = -1;
			this.container.hide();
		},

		suggest: function() {
			if (this.suggestions.length === 0) {
				this.hide();
				return;
			}

			var me, len, item, f, v, i, s, mOver, mClick;
			me = this;
			len = this.suggestions.length;
			v = this.getQuery(this.currentValue);
			mOver = function(xi) { return function() { me.activate(xi); }; };
			mClick = function(xi) { return function() { me.select(xi); }; };
			this.container.hide().empty();
			for (i = 0; i < len; i++) {
				s = this.suggestions[i].keyword;
				t = this.suggestions[i].count;
				item = $((me.selectedIndex === i ? '<li class="selected"' : '<li') + ' title="' + s + '">' + '<span class="sCount">' + '约' + t + '条' + '</span>' + s + '</li>');
				item.mouseover(mOver(i));
				item.click(mClick(i));
				this.container.append(item);
			}
			this.enabled = true;
			this.container.show();
		},

		processResponse: function(data) {
			var response , passQuery;
			try {
				response = data;
				//response = eval('(' + data + ')');
			} catch (err) {return; }
			passQuery=response.query;
			//add html5 session storge
			if(!this.options.noCache){
				if(window.sessionStorage&&!(typeof(JSON)=="undefined")){
					sessionStorage[passQuery] = JSON.stringify(response);
				}
				else{
				this.cachedResponse[passQuery] = response;	
				}	
			}
			if (response.query === this.getQuery(this.currentValue)) {
				this.suggestions = response.suggestions;
				this.suggest(); 
			}
		},

		activate: function(index) {
			var items, activeItem;
			items = this.container.children();
			// Clear previous selection:
			if (this.selectedIndex !== -1 && items.length > this.selectedIndex) {
				$(items.get(this.selectedIndex)).removeClass();
			}
			this.selectedIndex = index;
			if (this.selectedIndex !== -1 && items.length > this.selectedIndex) {
				activeItem = items.get(this.selectedIndex);
				$(activeItem).addClass('selected');
			}
			return activeItem;
		},

		deactivate: function(li, index) {
			li.className = '';
			if (this.selectedIndex === index) { this.selectedIndex = -1; }
		},

		select: function(i,e) {
			var selectedValue, f;
			selectedValue = this.suggestions[i].keyword;
			if (selectedValue) {
				this.el.val(selectedValue);
				if (this.options.autoSubmit || e===13) {
					f = this.el.parents('form');
					if (f.length > 0) { f.get(0).submit(); }
				}
				this.ignoreValueChange = true;
				this.hide();
				this.onSelect(i);
			}
		},

		moveUp: function() {
			if (this.selectedIndex === -1) { return; }
			if (this.selectedIndex === 0) {
				this.container.children().get(0).className = '';
				this.selectedIndex = -1;
				this.el.val(this.currentValue);
				return;
			}
			this.activate(this.selectedIndex - 1);
		},

		moveDown: function() {
			if (this.selectedIndex === (this.suggestions.length - 1)) { return; }
			this.activate(this.selectedIndex + 1);
		},

		onSelect: function(i) {
			var me, fn, s;
			me = this;
			fn = me.options.onSelect;
			s = me.suggestions[i].keyword;
			if ($.isFunction(fn)) {fn(s, me.el); }
		}
	};

}(jQuery));
