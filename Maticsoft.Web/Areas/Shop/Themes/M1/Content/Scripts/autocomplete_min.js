(function (b) {
    function a(d, c) {
        this.el = b(d);
        this.el.attr("autocomplete", "off");
        this.suggestions = [];
        this.selectedIndex = -1;
        this.currentValue = this.el.val();
        this.intervalId = 0;
        this.cachedResponse = [];
        this.onChangeInterval = null;
        this.ignoreValueChange = false;
        this.serviceUrl = c.serviceUrl;
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
        this.setOptions(c)
    }
    b.fn.autocomplete = function (c) {
        return new a(this.get(0) || b("<input />"), c)
    };
    a.prototype = {
        killerFn: null,
        initialize: function () {
            var d, e, c;
            d = this;
            e = Math.floor(Math.random() * 1048576).toString(16);
            c = "Autocomplete_" + e;
            this.killerFn = function (f) {
                if (b(f.target).parents(".autocomplete").size() === 0) {
                    d.killSuggestions();
                    d.disableKillerFn()
                }
            };
            if (!this.options.width) {
                this.options.width = this.el.width()
            }
            this.mainContainerId = "AutocompleteContainter_" + e;
            b('<div id="' + this.mainContainerId + '" style="position:absolute;z-index:9999;"><ul name="__DROPSEARCH" class="autocomplete" id="' + c + '" style="display:none;"></ul></div>').appendTo("body");
            this.container = b("#" + c);
            this.fixPosition();
            if (window.opera) {
                this.el.keypress(function (f) {
                    d.onKeyPress(f)
                });
                this.el.focus(function () {
                    this.onChangeInterval = setInterval(function () {
                        if (d.currentValue !== d.el.val()) {
                            d.onValueChange()
                        }
                    },
                    d.options.deferRequestBy)
                });
                this.el.blur(function () {
                    clearInterval(this.onChangeInterval)
                })
            } else {
                this.el.keydown(function (f) {
                    d.onKeyPress(f)
                })
            }
            this.el.keyup(function (f) {
                d.onKeyUp(f)
            });
            this.el.blur(function () {
                d.enableKillerFn()
            });
            this.el.focus(function () {
                d.fixPosition()
            })
        },
        setOptions: function (c) {
            var d = this.options;
            b.extend(d, c);
            b("#" + this.mainContainerId).css({
                zIndex: d.zIndex
            });
            this.container.css({
                maxHeight: d.maxHeight + "px",
                width: d.width
            })
        },
        clearCache: function () {
            this.cachedResponse = [];
            window.sessionStorage.clear()
        },
        disable: function () {
            this.disabled = true
        },
        enable: function () {
            this.disabled = false
        },
        fixPosition: function () {
            var c = this.el.offset();
            b("#" + this.mainContainerId).css({
                top: (c.top + this.el.innerHeight()) + "px",
                left: c.left + "px"
            })
        },
        enableKillerFn: function () {
            var c = this;
            b(document).bind("click", c.killerFn)
        },
        disableKillerFn: function () {
            var c = this;
            b(document).unbind("click", c.killerFn)
        },
        killSuggestions: function () {
            var c = this;
            this.stopKillSuggestions();
            this.intervalId = window.setInterval(function () {
                c.hide();
                c.stopKillSuggestions()
            },
            300)
        },
        stopKillSuggestions: function () {
            window.clearInterval(this.intervalId)
        },
        onKeyPress: function (d) {
            if (this.disabled || !this.enabled) {
                return
            }
            var c;
            if (window.event) {
                c = event.keyCode
            } else {
                if (d.which) {
                    c = d.which
                }
            }
            switch (c) {
                case 27:
                    this.el.val(this.currentValue);
                    this.hide();
                    break;
                case 9:
                case 13:
                    if (this.selectedIndex === -1) {
                        this.hide();
                        return
                    }
                    this.select(this.selectedIndex, c);
                    if (c === 9) {
                        return
                    }
                    break;
                case 38:
                    this.moveUp();
                    break;
                case 40:
                    this.moveDown();
                    break;
                default:
                    return
            }
            d.stopImmediatePropagation();
            d.preventDefault()
        },
        onKeyUp: function (d) {
            if (this.disabled) {
                return
            }
            switch (d.keyCode) {
                case 38:
                case 40:
                    return
            }
            clearInterval(this.onChangeInterval);
            if (this.currentValue !== this.el.val()) {
                if (this.options.deferRequestBy > 0) {
                    var c = this;
                    this.onChangeInterval = setInterval(function () {
                        c.onValueChange()
                    },
                    this.options.deferRequestBy)
                } else {
                    this.onValueChange()
                }
            }
        },
        onValueChange: function () {
            clearInterval(this.onChangeInterval);
            this.currentValue = this.el.val();
            var c = this.getQuery(this.currentValue);
            this.selectedIndex = -1;
            if (this.ignoreValueChange) {
                this.ignoreValueChange = false;
                return
            }
            if (c === "" || c.length < this.options.minChars) {
                this.hide()
            } else {
                this.getSuggestions(c)
            }
        },
        getQuery: function (d) {
            var c, e;
            c = this.options.delimiter;
            if (!c) {
                return b.trim(d)
            }
            e = d.split(c);
            return b.trim(e[e.length - 1])
        },
        getSuggestions: function (d) {
            var c, e;
            if (window.sessionStorage && !(typeof (JSON) == "undefined")) {
                c = sessionStorage.getItem(d) ? JSON.parse(sessionStorage.getItem(d)) : ""
            } else {
                c = this.cachedResponse[d]
            }
            if (c && b.isArray(c.suggestions)) {
                this.suggestions = c.suggestions;
                this.suggest()
            } else {
                e = this;
                e.options.params.keyword = d;
                b.get(this.serviceUrl, e.options.params,
                function (f) {
                    e.processResponse(f)
                },
                "jsonp")
            }
        },
        hide: function () {
            this.enabled = false;
            this.selectedIndex = -1;
            this.container.hide()
        },
        suggest: function () {
            if (this.suggestions.length === 0) {
                this.hide();
                return
            }
            var j, h, l, g, k, d, m, e, c;
            j = this;
            h = this.suggestions.length;
            k = this.getQuery(this.currentValue);
            e = function (f) {
                return function () {
                    j.activate(f)
                }
            };
            c = function (f) {
                return function () {
                    j.select(f)
                }
            };
            this.container.hide().empty();
            for (d = 0; d < h; d++) {
                m = this.suggestions[d].keyword;
                t = this.suggestions[d].count;
                l = b((j.selectedIndex === d ? '<li class="selected"' : "<li") + ' title="' + m + '"><span class="sCount">约' + t + "条</span>" + m + "</li>");
                l.mouseover(e(d));
                l.click(c(d));
                this.container.append(l)
            }
            this.enabled = true;
            this.container.show()
        },
        processResponse: function (f) {
            var d, c;
            try {
                d = f
            } catch (e) {
                return
            }
            c = d.query;
            if (!this.options.noCache) {
                if (window.sessionStorage && !(typeof (JSON) == "undefined")) {
                    sessionStorage[c] = JSON.stringify(d)
                } else {
                    this.cachedResponse[c] = d
                }
            }
            if (d.query === this.getQuery(this.currentValue)) {
                this.suggestions = d.suggestions;
                this.suggest()
            }
        },
        activate: function (c) {
            var e, d;
            e = this.container.children();
            if (this.selectedIndex !== -1 && e.length > this.selectedIndex) {
                b(e.get(this.selectedIndex)).removeClass()
            }
            this.selectedIndex = c;
            if (this.selectedIndex !== -1 && e.length > this.selectedIndex) {
                d = e.get(this.selectedIndex);
                b(d).addClass("selected")
            }
            this.el.val(b(d).attr("title"));
            return d
        },
        deactivate: function (c, d) {
            c.className = "";
            if (this.selectedIndex === d) {
                this.selectedIndex = -1
            }
        },
        select: function (d, h) {
            var c, g;
            c = this.suggestions[d].keyword;
            if (c) {
                this.el.val(c);
                if (this.options.autoSubmit || h === 13) {
                    g = this.el.parents("form");
                    if (g.length > 0) {
                        g.get(0).submit()
                    }
                }
                this.ignoreValueChange = true;
                this.hide();
                this.onSelect(d)
            }
        },
        moveUp: function () {
            if (this.selectedIndex === -1) {
                return
            }
            if (this.selectedIndex === 0) {
                this.container.children().get(0).className = "";
                this.selectedIndex = -1;
                this.el.val(this.currentValue);
                return
            }
            this.activate(this.selectedIndex - 1)
        },
        moveDown: function () {
            if (this.selectedIndex === (this.suggestions.length - 1)) {
                return
            }
            this.activate(this.selectedIndex + 1)
        },
        onSelect: function (c) {
            var f, e, d;
            f = this;
            e = f.options.onSelect;
            d = f.suggestions[c].keyword;
            if (b.isFunction(e)) {
                e(d, f.el)
            }
        }
    }
} (jQuery));