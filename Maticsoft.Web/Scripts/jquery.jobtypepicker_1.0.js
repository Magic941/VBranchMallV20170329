/*
wusg
职级的三级选择
2015-1-26
*/
(function ($, cr) {
    var jobtypedata = [];
    var RegionPicker = function (el, options) {
        this.el = el;
        el.empty();
        this.Value = null;// 系统的值
        this.options = $.extend({
            remote: '',
            picked: '',
            visible: 10,
            animate: 0,
            companycode: '',
            level: 3,
            jobtypedata: [] //
        }, options || {});

        //alert(this.options.jobtypedata.length);
         //赋值
        jobtypedata = this.options.jobtypedata;

        if (this.options.companycode != '')
        {
            if (this.options.level == 3) {
                this.renderer = $('<select style="margin-right:5px;width:200px" size=5 class="jobtypeSele level1"  name="level1"></select><select class="jobtypeSele level2" size=5 style="margin-right:5px;width:200px" name="level2"></select><select class="jobtypeSele level3" size=5 style="width:258px" name="level3"></select>');
                this.renderer.appendTo(this.el);
                this.Level1 = $('.Level1', this.renderer).empty();
                this.Level2 = $('.Level2', this.renderer).empty();
                this.Level3 = $('.Level3', this.renderer).empty();
                this._render(".level1", 0);
            } else {
                this.renderer = $('<select style="margin-right:5px;width:200px" size=5 class="jobtypeSele level1"  name="level1"></select><select class="jobtypeSele level2" size=5 style="margin-right:5px;width:200px" name="level2"></select>');
                this.renderer.appendTo(this.el);
                this.Level1 = $('.Level1', this.renderer).empty();
                this.Level2 = $('.Level2', this.renderer).empty();
               
                this._render(".level1", 0);

            }
        }
        else
        {
            this.renderer = $('<input type="text" class="txtJobType" style="width:192px"/>');
            this.renderer.appendTo(this.el);
         }
    };

    RegionPicker.prototype = {

        constructor: RegionPicker,
        /**
        * _onItemClick, fired when a region item clicked
        * @param {Object} e, event object
        * @return undefined
        */
        _onItemClick: function (e) {
            var el = $(e.currentTarget), id = el.attr('data-id');
            e.preventDefault();
            this.el.trigger('loading.rp', this);
            this.picker.pick(id, $.proxy(function (regions, collections) {
                this.data.regions = regions;
                this.data.collections = collections;
                this.el.trigger('loaded.rp', [this.data, this]);

                var leafNode = (regions[regions.length - 1] && id === regions[regions.length - 1].i);
                if (leafNode) {
                    this.el.trigger('picked.rp', [regions, this]);
                    this._onCloserClick(e);
                } else {
                    this.renderer.trigger('reveal');
                }
            }, this));
        },

        /**
        * _render, fired when current plugin has shown
        * @return undefined
        */
        _render: function (level, pcode) {

            var container = $(level, this.el).empty();            
            var itemhtml = [];
          
            var dataitems = this._getdatas(pcode);
            $.each(dataitems, function (i, item) {
                itemhtml.push('<option class="level-list" level="'+ level +'" value="' + item["c"] + '">' + item["n"] + '</option>');
            });
            $(itemhtml.join('')).appendTo(container);
            var self = this;
            //$(".level-list", container).each(function (i, item) {
               
              
            //});
            $(container).on("click", function () {
               // alert(i);
                var item = $('option:selected', container);
                 var level = $(item).attr("level");
                // alert(level);alert($(item).val());
                 if (level == ".level1") {
                     $(".level2", self.el).empty();
                     $(".level3", self.el).empty();
                     self._render(".level2", $(item).val());
                 }
                 else if (level == ".level2") {            
                     if(self.options.level==3)
                         self._render(".level3", $(item).val());
                     else
                         self.Value = $(item).val();
                 } else if (level == ".level3") {
                     self.Value = $(item).val();
                 }
            });
        },       
        _getdatas: function (pcode)
        {
            var iccode = this.options.companycode;
            var a = [];
            //alert(jobtypedata.length);
            var container = $.each(jobtypedata, function (i, item) {
                
                   // alert(item["pC"]);
                   //alert(item["iCC"]);
              
                if (item["pC"] == pcode && iccode == item["iCC"])
                {
                    a.push(item);
                }
            });          
            a = a.sort(function (a, b) {  return a["id"] - b["id"]; });
           return a;
        }
        /*返回文本框值或选择值*/
        , getValue: function () {
            
            var self = this;
            if( this.options.companycode != '')
            {
                if (this.Value != null) {
                    var v = null;
                    $.each(jobtypedata, function (i, item) {
                        //alert(item["c"]);
                        //alert(self.Value);
                        if (self.Value == item["c"]) {
                            v= item;
                            return false;
                        }
                    });
                    if (v != null) {                         
                        return v["c"] + "-" + v["n"];
                    }
                    else { return '';}
                }
            }
            else {
                return $(this.renderer).val();
            }
        }
    };
    $.fn.regionPicker = function (options) {
        var vcache = $(this).data("keytype");
        if (vcache != null)
            return vcache;
        else {
            var v = new RegionPicker(this, options);
            $(this).data("keytype", v);
            return v;
        }
       
    };
})(jQuery);