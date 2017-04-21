
function UpdateUI(data) {
    var html = "";
    if (typeof (data) == "undefined" || data == null) {
        return;
    }
    //1.如果用户是点翻页，排序等操作，只更新排序，商品和分页区域
    html += BuildFilterArea(data) + "\r\n";
    html += BuildSortUI(data.SortUI) + "\r\n";
    html += BuildStyleUI(data.StyleUI) + "\r\n";
    html += BuildPageUI(data.PageUI);
    ChangePageTitle(data.PageTitle);
    $("#mainDiv").find("[ajaxContent=1]").remove();
    var newContent = $(html);
    var pos = $("#ajaxContentPosition");
    pos.after(newContent);
}

function BuildPageUI(pageUI) {
    var html = "";
    if (pageUI.PageCount > 1) {
        html += "<div ajaxContent=\"1\" class=\"cf mt10 mb10\">"
			+ "<div class=\"paging-skip fr\">"
			+ "<div class=\"fl ml10\"><span class=\"paging-txt\">到第</span>"
			+ "<input type=\"text\" id=\"txtPage\" class=\"paging-text\" value=\"" + pageUI.JumpPageIndex + "\" />"
			+ "<span class=\"paging-txt\">页 </span><a pos=\"bottom\" overrideClick=\"1\" canAjax=\"1\" id=\"subPage\" class=\"paging-btn\" href=\"" + pageUI.JumpUrl + "\" anchor='Sort'><span>确定</span></a>"
        + "</div>"
		+ "</div>";

        html += "<div class=\"paging fr\">";
        if (pageUI.PrePageLink != null && pageUI.PrePageLink.IsShow) {
            html += "<a id=\"" + pageUI.PrePageLink.InnerName + "\" pos=\"bottom\" canAjax=\"1\" class=\"paging-prev\" anchor='Sort' href=\"" + pageUI.PrePageLink.Url + "\"><span>" + pageUI.PrePageLink.Text + "</span></a>";
        }

        for (var i = 0; i < pageUI.PageLinks.length; i++) {
            var page = pageUI.PageLinks[i];
            if (page.Url != null && page.Url != "") {
                html += "<a pos=\"bottom\" canAjax=\"1\" anchor='Sort' href=\"" + page.Url + "\"><span>" + page.Text + "</span></a>";
            }
            else {
                html += "<span class=\"" + (page.Text == "..." ? "paging-break" : "cur") + "\">" + page.Text + "</span>";
            }
        }
        if (pageUI.NextPageLink != null && pageUI.NextPageLink.IsShow) {
            html += "<a id=\"" + pageUI.NextPageLink.InnerName + "\" pos=\"bottom\" canAjax=\"1\" class=\"paging-next\" anchor='Sort' href=\"" + pageUI.NextPageLink.Url + "\"><span>" + pageUI.NextPageLink.Text + "</span></a>";
        }
        html += "</div>";
        html += "</div>";
    }
    return html;
}

function BuildStyleUI(styleUI) {
    var html = "";
    if (styleUI.SeeAllLink != null) {
        html += "<div ajaxContent=\"1\" class=\"warntxt\">抱歉，没有找到符合条件的商品！<a class=\"a2 underline\" canAjax=\"1\" anchor='Sort' href=\"" + styleUI.SeeAllLink.Url + "\">" + styleUI.SeeAllLink.Text + "</a></div>";
    }
    if (styleUI != null
		&& styleUI.Styles != null
		&& styleUI.Styles.length > 0) {
        html += "<div ajaxContent=\"1\" class=\"itemmain mb20 relative\" name=\"__FLLB002\">"
			+ "<ul class=\"list list-n4\" id=\"J_lazyload\">";
        var imgAttributeName = "src";
        for (var i = 0; i < styleUI.Styles.length; i++) {
            if (i >= 12) {
                imgAttributeName = "lazyload";
            }
            var style = styleUI.Styles[i];
            var aliasname = "";
            if (style.AliasName != undefined && style.AliasName != null && style.AliasName != "") {
                aliasname = style.AliasName;
            }
            html += "<li styleId=\"" + style.StyleId + "\" class=\"item item-sale\">";
            html += "<a target=\"_blank\" href=\"" + style.Url + "\" title=\"" + style.ChineseName + " " + aliasname + "\" class=\"item-desc ForSkuLink\">"
				+ "<img title=\"" + style.ChineseName + " " + aliasname + "\" " + imgAttributeName + "=\"" + style.ImageUrl + "\" alt=\"" + style.ChineseName + "\" width=\"175\" height=\"228\" />"
				+ "<span class=\"item-price\"><em>" + style.WebSalePrice + "</em>";
            if (style.GPPrice != null && style.GPPrice != "") {
                html += "<span class=\"item-points\">+" + style.GPPrice + "积分</span>";
            }

            html += "</span>";

            if (style.OriginalPrice != null && style.OriginalPrice != "") {
                html += "<del>" + style.OriginalPrice + "</del>";
            }
            html += "<span class=\"item-title\">" + style.ShortChineseName;

            if (style.AliasName != null && style.AliasName != "") {
                html += "<em>&ensp;" + style.AliasName + "</em>";
            }

            html += "</span>"
				+ "</a>";
            if (style.Skus != null
				&& style.Skus.length > 1) {
                html += "<div name=\"__ColorImg\">"
				+ "<ul class=\"item-color SkuColorSelect\">";
                for (var j = 0; j < style.Skus.length; j++) {
                    var sku = style.Skus[j];
                    html += "<li>"
								+ "<a rel=\"nofollow\" imgName=\"" + sku.StyleImageUrl + "\" skuId=\"" + sku.SKUDimentionId1 + "\" alt=\"" + sku.SKUDimentionName1 + "\" title=\"" + sku.SKUDimentionName1 + "\" href=\"javascript:void(0);\">"
								+ "<img alt=\"" + sku.SKUDimentionName1 + "\" " + imgAttributeName + "=\"" + sku.ImageUrl + "\" />"
								+ "</a>"
						+ "</li>";
                }
                html += "</ul>"
					+ "</div>";
            }
            if (style.IsShowComment) {
                html += "<div class=\"item-rate\">"
				+ "<div class=\"score-s\"><span class=\"score-s-star\" style=\"" + style.CommentInnerCss + "\"></span></div>"
				+ "<span class=\"item-rate-num\">(<a id=\"PLTS004\" href=\"" + style.Url + "#comment\" target=\"_blank\" class=\"CommSkuLink\">共" + style.CommentCount + "条</a>)</span>"
				+ "</div>";
            }
            html += "<p class=\"item-brand\">" + style.TradeMarkName + "</p>";
            if (style.SpecialPriceFlag) {
                html += "<p class=\"item-tag\">特价</p>";
            }
            html += "</li>";
        }
        html += "</ul>"
			+ "<div class=\"loadingmask\">"
   					+ "<div class=\"loadingmask-bg\"></div>"
    				+ "<div class=\"loadingmask-txt fixtop\"><img src=\"/Areas/Shop/Themes/M1/Content/images/loading-pink.gif\" class=\"mr10\" style=\"vertical-align:middle\"></div>"
				+ "</div>"
				+ "</div>";
    }
    return html;
}

function BuildFilterArea(data) {
    var html = "";
    if ((data.ChoiceUI.Choices != null && data.ChoiceUI.Choices.length > 0) || (data.TradeMarkUI.TradeMarks != null && data.TradeMarkUI.TradeMarks.length > 0) || (data.PropertyUI.Properties != null && data.PropertyUI.Properties.length > 0)) {
        html += "<div ajaxContent=\"1\" id=\"" + hashInfo.target + "_Filter" + "\" class=\"proditembox mb20\">"
			+ "<div id='Filter'></div>"
            + "<div class=\"termlist bor1\">";
        if (data.ChoiceUI.Choices != null
		&& data.ChoiceUI.Choices.length > 0) {
            html += "<div class=\"choice-item cf\">"
				+ "<div class=\"cititle\">已选：</div>"
				+ "<div class=\"choice-item-list\">";
            for (var i = 0; i < data.ChoiceUI.Choices.length; i++) {
                var curChoice = data.ChoiceUI.Choices[i];
                html += "<a canAjax=\"1\" anchor='Filter' href=\"" + curChoice.Link.Url + "\">" + curChoice.Link.Text;
                if (curChoice.AliasName != null
				&& curChoice.AliasName != "" && curChoice.AliasName != curChoice.Link.Text) {
                    html += "<em>&nbsp;" + curChoice.AliasName + "</em>";
                }
                html += "</a>";
            }
            html += "</div>"
            if (data.ChoiceUI.CleanLink != null) {
                html += "<div class=\"delect-btn fr\"><a canAjax=\"1\" anchor='Filter' href=\"" + data.ChoiceUI.CleanLink.Url + "\">" + data.ChoiceUI.CleanLink.Text + "</a></div>";
            }
            html += "</div>";
        }

        if (data.TradeMarkUI.TradeMarks != null &&
		data.TradeMarkUI.TradeMarks.length > 0) {
            var tm = data.TradeMarkUI.TradeMarks[i];
            html += "<div class=\"prop-item branditem pr10\" name=\"pro_brand\">"
					+ "<dl class=\"cf\">"
					+ "<dt title=\"品牌\">品牌：</dt>"
					+ "<dd class=\"cf\">";
            if (data.TradeMarkUI.AllLink != null) {
                html += "<a canAjax=\"1\" anchor='Filter' href=\"" + data.TradeMarkUI.AllLink.Url + "\" title=\"" + data.TradeMarkUI.AllLink.Text + "\" class=\"allbtn" + (data.TradeMarkUI.AllLink.IsCurrent ? " cur" : "") + "\">" + data.TradeMarkUI.AllLink.Text + "</a>";
            }
            html += "<ul class=\"cf brandBox\">";
            for (var i = 0; i < data.TradeMarkUI.TradeMarks.length; i++) {
                var tm = data.TradeMarkUI.TradeMarks[i];
                html += "<li style=\"" + (tm.Link.IsShow ? "" : "display:none;") + "\">"
					+ "<a canAjax=\"1\" " + (tm.Link.IsCurrent ? " class=\"cur\"" : "") + " title=\"" + tm.TradeMarkName + "\" anchor='Filter' href=\"" + tm.Link.Url + "\">"
					+ "<span class=\"brandname\">" + tm.TradeMarkName + "</span><span class=\"brandimg\" style=\"background:url(http://img.maticsoft.com/web/pic/list/brand/" + tm.TradeMarkId + "_m.gif)\"></span>"
					+ "</a>"
					+ "</li>";
            }
            html += "</ul>"
					+ "</dd>"
					+ "</dl>"
					+ "</div>";
        }

        if (data.PropertyUI.Properties != null && data.PropertyUI.Properties.length > 0) {
            html += "<div id=\"propretyArea\" name=\"__pro_shuxing\">";
            for (var i = 0; i < data.PropertyUI.Properties.length; i++) {
                var pro = data.PropertyUI.Properties[i];
                html += "<div class=\"prop-item pr10 proTypeBox\" style=\"" + (pro.IsShow ? "" : "display:none;") + "\">"
				+ "<dl class=\"cf\">"
				+ "<dt title=\"" + pro.DisplayName + "\">" + pro.DisplayName + "：</dt>"
				+ "<dd>"
				+ "<a canAjax=\"1\" anchor='Filter' href=\"" + pro.AllValue.Url + "\" valueId=\"0\" title=\"" + pro.AllValue.Text + "\" class=\"allbtn" + (pro.AllValue.IsCurrent ? " cur" : "") + "\">" + pro.AllValue.Text + "</a>"
				+ "<ul class=\"cf\" typeId=\"" + pro.TypeId + "\">";
                if (pro.Values != null && pro.Values.length > 0) {
                    for (var j = 0; j < pro.Values.length; j++) {
                        var pv = pro.Values[j];
                        html += "<li style=\"" + (pv.IsShow ? "" : "display:none;") + "\"><a canAjax=\"1\" anchor='Filter' href=\"" + pv.Url + "\" valueId=\"\" title=\"" + pv.Text + "\" class=\"" + (pv.IsCurrent ? "cur" : "") + "\">" + pv.Text + "</a></li>";
                    }
                }
                html += "</ul>"
			+ "</dd>"
			+ "</dl>"
			+ "</div>";
            }
            html += "</div>";
        }
        html += "</div>"
		+ "</div>";
    }
    return html;
}

function BuildSortUI(sort) {
    var html = "";
    if (sort.SortLinks == null || sort.SortLinks.length == 0) {
        return html;
    }
    html += "<div ajaxcontent=\"1\" class=\"filterbox cf mb20\">";
    html += "<div id='Sort'></div>";
    html += "<div class=\"cf list-mode\" id=\"" + hashInfo.target + "_Sort" + "\">"
			+ "<div class=\"fl et\">"
			+ "<div class=\"filter-title\">排序：</div>"
			+ "<ul class=\"fl mr10\">";
    for (var i = 0; i < sort.SortLinks.length; i++) {
        var sortLink = sort.SortLinks[i];
        if (sort.SortLinks[i].IsShow) {
            var sortTitle = "";
            if (sortLink.InnerTitle != null && sortLink.InnerTitle != "") {
                sortTitle = "title=\"" + sortLink.InnerTitle + "\" ";
            }
            html += "<li class=\"" + sortLink.InnerCss + "\" ><a rel=\"nofollow\" canajax=\"1\" id=\"" + sortLink.InnerName + "\" anchor='Sort' href=\"" + sortLink.Url + "\" class=\"" + (sortLink.IsCurrent ? "cur" : "") + "\" " + sortTitle + "><span>" + sortLink.Text + "</span></a></li>";
        }
    }
    html += "</ul>"
			+ "<div class=\"fl mr5\">"
			+ "<input name=\"\" type=\"text\" class=\"mtext priceTxt\" id=\"txtPrice1\" maxlength=\"6\" value=\"" + sort.MinPrice + "\" />"
			+ "<span class=\"lower\">-</span>"
			+ "<input name=\"\" type=\"text\" class=\"mtext priceTxt\" id=\"txtPrice2\" maxlength=\"6\" value=\"" + sort.MaxPrice + "\" />"
			+ "</div>"
			+ "<a canajax=\"1\" overrideClick=\"1\" class=\"mbtn\" id=\"SubPrice\" id=\"JGQJ\" oriHref=\"" + sort.PriceComfirmLink.Url + "\" anchor='Sort' href=\"" + sort.PriceComfirmLink.Url + "\"><span>确定</span></a>"
			+ "</div>"
			+ "<div class=\"paging fr\">";
    if (sort.PageCount > 1) {
        html += "<span class=\"fl mr5 mt5 c9\">" + sort.PageIndex + "/" + sort.PageCount + "</span>";
    }
    if (sort.PrePageLink != null && sort.PrePageLink.IsShow) {
        html += "<a id=\"" + sort.PrePageLink.InnerName + "\" canajax=\"1\" class=\"paging-prev\" anchor='Sort' href=\"" + sort.PrePageLink.Url + "\"><span>" + sort.PrePageLink.Text + "</span></a>";
    }
    if (sort.NextPageLink != null && sort.NextPageLink.IsShow) {
        html += "<a id=\"" + sort.NextPageLink.InnerName + "\" canajax=\"1\" class=\"paging-next\" anchor='Sort' href=\"" + sort.NextPageLink.Url + "\"><span>" + sort.NextPageLink.Text + "</span></a>";
    }
    html += "</div>";
    html += "</div>";
    if ((sort.ItemTypeLinks != null && sort.ItemTypeLinks.length > 0) || (sort.ColorLinks != null && sort.ColorLinks.length > 0)) {
        html += "<div class=\"list-view mt10 pb5 cf\">";
        if (sort.ItemTypeLinks != null && sort.ItemTypeLinks.length > 0) {
            html += "<dl class=\"view-box\">";
            html += "<dt class=\"filter-title\">筛选：</dt>";
            for (var i = 0; i < sort.ItemTypeLinks.length; i++) {
                var typeLink = sort.ItemTypeLinks[i];
                if (typeLink.IsShow) {
                    html += "<dd class=\"view-check\"><a rel=\"nofollow\" canajax=\"1\" anchor=\"Sort\" href=\"" + typeLink.Url + "\" class=\"" + (typeLink.IsCurrent ? "cur" : "") + "\" id=\"" + typeLink.InnerName + "\">特价</a></dd>";
                }
            }
            html += "</dl>";
        }
        if (sort.ColorLinks != null && sort.ColorLinks.length > 0) {
            html += "<dl class=\"view-box view-box-r\">"
	        + "<dt class=\"filter-title\">显示：</dt>";
            for (var i = 0; i < sort.ColorLinks.length; i++) {
                var colorLink = sort.ColorLinks[i];
                html += "<dd><a rel=\"nofollow\" anchor=\"Sort\" href=\"" + colorLink.Url + "\" class=\"" + (colorLink.IsCurrent ? "cur" : "") + "\" id=\"" + colorLink.InnerName + "\">" + colorLink.Text + "</a></dd>";
            }
            html + "</dl>";
        }
        html += "</div>";
    }
    html += "</div>";
    return html;
}

