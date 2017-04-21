<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductsInStock.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductsInStock" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <%--<script type="text/javascript" src="/Admin/js/Shop/poplayer/js/jquery.js"></script>--%>
    <script type="text/javascript" src="/Admin/js/Shop/poplayer/js/tipswindown.js"></script>
    <link href="/Admin/js/Shop/poplayer/js/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function changProductName(id) {
            $("#img_" + id).hide();
            $("#txtProductName_" + id).show().focus();
            $("#editsave_" + id).show();
            $("#p_" + id).hide();
        }

        function UpdateStock(id) {
            $("#imgStockNum_" + id).hide();
            $("#TextStockNum_" + id).show().focus();
            $("#aStockNum_" + id).show();
            $("#StockNum_" + id).hide();
        }

        function SaveStockNum(id) {
            var StockNum = $("#TextStockNum_" + id).val();
            if (StockNum == "") {
                alert('请输入库存数量！');
                return;
            }
            $.ajax({
                url: ("ProductsInStock.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "UpdateStockNum", Callback: "true", ProductId: id, UpdateValue: StockNum },
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $("#StockNum_" + id).text(StockNum);
                        $("#imgStockNum_" + id).show();
                        $("#TextStockNum_" + id).hide();
                        $("#aStockNum_" + id).hide();
                        $("#StockNum_" + id).show();
                    }
                    else {
                        alert("系统忙请稍后再试！");
                    }
                }
            });
        }

        function UpdateLowestSalePrice(id) {
            $("#imgLowestSalePrice_" + id).hide();
            $("#TextLowestSalePrice_" + id).show().focus();
            $("#aLowestSalePrice_" + id).show();
            $("#LowestSalePrice_" + id).hide();
        }

        function SaveLowestSalePrice(id) {
            var LowestSalePrice = $("#TextLowestSalePrice_" + id).val();
            if (LowestSalePrice == "") {
                alert('请输入商品销售价！');
                return;
            }
            $.ajax({
                url: ("ProductsInStock.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "UpdateLowestSalePrice", Callback: "true", ProductId: id, UpdateValue: LowestSalePrice },
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $("#LowestSalePrice_" + id).text("￥" + LowestSalePrice);
                        $("#imgLowestSalePrice_" + id).show();
                        $("#TextLowestSalePrice_" + id).hide();
                        $("#aLowestSalePrice_" + id).hide();
                        $("#LowestSalePrice_" + id).show();

                    }
                    else {
                        alert("系统忙请稍后再试！");
                    }
                }
            });
        }

        function UpdateMarketPrice(id) {
            $("#imgMarketPrice_" + id).hide();
            $("#TextMarketPrice_" + id).show().focus();
            $("#aMarketPrice_" + id).show();
            $("#MarketPrice_" + id).hide();
        }

        function SaveMarketPrice(id) {
            var MarketPrice = $("#TextMarketPrice_" + id).val();
            if (MarketPrice == "") {
                alert('请输入市场价！');
                return;
            }
            $.ajax({
                url: ("ProductsInStock.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "UpdateMarketPrice", Callback: "true", ProductId: id, UpdateValue: MarketPrice,colum },
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {

                        $("#MarketPrice_" + id).text("￥" + MarketPrice);
                        $("#imgMarketPrice_" + id).show();
                        $("#TextMarketPrice_" + id).hide();
                        $("#aMarketPrice_" + id).hide();
                        $("#MarketPrice_" + id).show();

                    }
                    else {
                        alert("系统忙请稍后再试！");
                    }
                }
            });
        }

        function saveChange(id) {
            var productName = $("#txtProductName_" + id).val();
            if (!productName) {
                alert('请输入商品名称！');
                return;
            }
            $.ajax({
                url: ("ProductsInStock.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "UpdateProductName", Callback: "true", ProductId: id, UpdateValue: productName },
                async: false,
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        $("#p_" + id).text(productName);
                        $("#img_" + id).show();
                        $("#editsave_" + id).hide();
                        $("#txtProductName_" + id).hide();
                        $("#p_" + id).show();
                    }
                    else {
                        alert("系统忙请稍后再试！");
                    }
                }
            });
        }

        $(document).ready(function () {

            $("[id$='ddlSupplier']").select2({ placeholder: "请选择" });
            $(".select2-container").css("vertical-align", "middle");
            resizeImg('.borderImage', 80, 80);
            $(".StockNum_Input").blur(function () {
                StockBulr(this);
            });

            $(".MarketPrice_Input").blur(function () {
                $(this).hide();
                var id = $(this).attr('i');
                $("#imgMarketPrice_" + id).show();
                $("#TextMarketPrice_" + id).hide();
                $("#aMarketPrice_" + id).hide();
                $("#MarketPrice_" + id).show();
            });

            $(".LowestSalePrice_Input").blur(function () {
                $(this).hide();
                var id = $(this).attr('i');
                $("#imgLowestSalePrice_" + id).show();
                $("#TextLowestSalePrice_" + id).hide();
                $("#aLowestSalePrice_" + id).hide();
                $("#LowestSalePrice_" + id).show();
            });

            $(".item-title-area").hover(function () {
                $(this).addClass('high-light');
            }, function () {
                $(this).removeClass("high-light");
            });
            $(".txtpname").blur(function () {
                textBulr(this);
            });
            $(".editsave").mouseenter(function () {
                var id = $(this).attr('i');
                $("#txtProductName_" + id).unbind('blur');
                $("#TextStockNum_" + id).unbind('blur');
                $("#TextLowestSalePrice_" + id).unbind('blur');
                $("#TextMarketPrice_" + id).unbind('blur');

            }).mouseleave(function () {
                var id = $(this).attr('i');
                $("#txtProductName_" + id).bind('blur', function () {
                    $("#txtProductName_" + id).hide();
                    $("#img_" + id).show();
                    $("#editsave_" + id).hide();
                    $("#p_" + id).show();
                });

            });


            $(".LowestSalePrice_save").mouseenter(function () {
                var id = $(this).attr('i');
                $("#TextLowestSalePrice_" + id).unbind('blur');

            }).mouseleave(function () {
                var id = $(this).attr('i');
                $("#TextLowestSalePrice_" + id).bind('blur', function () {
                    $("#imgLowestSalePrice_" + id).show();
                    $("#TextLowestSalePrice_" + id).hide();
                    $("#aLowestSalePrice_" + id).hide();
                    $("#LowestSalePrice_" + id).show();

                });
            });

            $(".MarketPrice_save").mouseenter(function () {
                var id = $(this).attr('i');
                $("#TextMarketPrice_" + id).unbind('blur');

            }).mouseleave(function () {
                var id = $(this).attr('i');
                $("#TextMarketPrice_" + id).bind('blur', function () {
                    $("#imgMarketPrice_" + id).show();
                    $("#TextMarketPrice_" + id).hide();
                    $("#aMarketPrice_" + id).hide();
                    $("#MarketPrice_" + id).show();
                });
            });

            // $(".iframe").colorbox({ iframe: true, width: "880", height: "720", overlayClose: false });


        });

        function textBulr(thisenent) {
            $(thisenent).hide();
            var id = $(thisenent).attr('i');
            $("#img_" + id).show();
            $("#editsave_" + id).hide();
            $("#p_" + id).show();
        }
        function StockBulr(thisenent) {
            $(thisenent).hide();
            var id = $(thisenent).attr('i');
            $("#imgStockNum_" + id).show();
            $("#TextStockNum_" + id).hide();
            $("#aStockNum_" + id).hide();
            $("#StockNum_" + id).show();
        }
    </script>
    <script type="text/javascript">
        /*
        *弹出本页指定ID的内容于窗口
        *id 指定的元素的id
        *title:	window弹出窗的标题
        *width:	窗口的宽,height:窗口的高
        */
        function showTipsWindown(title, id, width, height) {

            //tipsWindown(title,"id:"+id,width,height,"true","","true",id);
            tipsWindown(title, "iframe:" + id, width, height, "true", "", "true", id);
        }

      

        //弹出层调用
        function popTips(vid) {
            var width = Number(window.screen.availWidth) * 0.8;
            var hight = Number(window.screen.availHeight) * 0.6;
           
            showTipsWindown("商品修改", '/admin/shop/Products/ProductModify.aspx?pid=' + vid, width,hight);
          
        }

        $(document).ready(function () {
        
            $("#isread").click(popTips);
            $("#isread-text").click(popTips);
           

        });
        
        function getCheckBox()
        {
            var productid=0;
            var chklists=$(".GridViewTyle tr:gt(2) [type=checkbox] ");
            var clProduct=$(".clProduct");
            $.each(chklists, function(i,chk){   
                if(chk.checked==true)
                {
                    var pid=clProduct[i].value;
                    if(productid=='')
                    {
                        productid=pid;
                    }
                    else
                    {
                        productid=productid+","+pid;
                    }
                  
                }
            });  
            //if(productid=='')
            //{
            //    alert('请选择你需要修改的产品');
            //    return false;
            //}
            var width = Number(window.screen.availWidth) * 0.7;
            var hight = Number(window.screen.availHeight) * 0.5;
            var status=$(".statusClass").val();
          
           
            showTipsWindown("商品标签修改", '/admin/shop/Products/TagModify.aspx?pid=' + productid+"&SaleStatus="+status, width,hight);
          
        }
    </script>
    <style type="text/css">
        .autobrake {
            word-wrap: break-word;
            width: 280px;
        }

        .item-title-area {
            width: 300px;
        }

        .high-light {
            cursor: pointer;
        }

        .txtpname {
            width: 260px;
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="hidstatus" class="statusClass" name="hidstatus" value="-1" runat="server" />
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">商品管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <%=strTitle %>
                        （注：商品最低销售价、最低成本价蓝色标记的为多种规格的商品）
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal1" runat="server" Text="商品分类" />：
                    <asp:DropDownList CssClass="select2" ID="drpProductCategory" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;
                    <asp:Literal ID="Literal5" runat="server" Text="商家" />：
                    <span>
                        <asp:DropDownList CssClass="select2" ID="ddlSupplier" runat="server">
                        </asp:DropDownList>
                    </span>&nbsp;&nbsp;
                    <asp:Literal ID="Literal2" runat="server" Text="商品名称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Literal ID="Literal3" runat="server" Text="产品编号" />：
                    <asp:TextBox ID="txtProductNum" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    商品货号：<asp:TextBox ID="txtSKU" runat="server"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Literal ID="Literal4" runat="server" Text="警戒库存商品" />：
                    <asp:CheckBox ID="chkAlert" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button1" OnClientClick="return confirm('你确定要放入回收站吗？要还原商品请到回收站找回！')"
                            runat="server" Text="批量删除2" class="adminsubmit" OnClick="btnDelete_Click" />
                    </li>
                    <li style="margin-top: -6px; padding-left: 0px">
                        <asp:Button ID="btnInverseApprove2" runat="server" Text="批量下架" class="adminsubmit"
                            OnClick="btnInverseApprove_Click" />
                    </li>
                    <li style="margin-top: -6px; padding-left: 50px">
                        <%--<asp:Button ID="" runat="server" Text="批量更新标签" class="adminsubmit"  />--%>

                        <input type="button" id="btnSaveTag" name="btnSaveTag" onclick="return getCheckBox();" value="批量更新标签" class="adminsubmit" />
                    </li>
                    <li style="margin-top: -6px; padding-left: 50px">
                        <asp:Button ID="Button2" runat="server" Text="批量刷新" class="adminsubmit"
                            OnClick="btnUpdateDate_Click" />
                    </li>
                    <li style="margin-top: -6px; padding-left: 50px">
                         <asp:Button ID="Button3" runat="server" Text="进口商品" class="adminsubmit"
                            OnClick="btnUpdateImportPro_Click" />
                    </li>
                    <li style="margin-top: -6px; padding-left: 50px">
                         <asp:Button ID="Button4" runat="server" Text="取消进口商品" class="adminsubmit"
                            OnClick="btnDeleteImportPro_Click" />
                    </li>

                </ul>
            </div>
        </div>
        <!--Search end-->
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" unexportedcolumnnames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="1" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ProductId"
            ShowToolBar="false" AllowPagerTrue="false" ShowFootPageButton="false">
            <columns>
                <asp:TemplateField HeaderText="图片" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div class="borderImage">
                            <asp:TextBox ID="txtSaleStatus" runat="server" Text='<%#Eval("SaleStatus")%>' Visible="false"></asp:TextBox>
                            <input type="hidden" name="hidProductId" class="clProduct" value="<%#Eval("ProductId") %>" />
                            <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank">
                                <img src='<%# Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(),"T350x350_") %>' style="width: 80px; height: 60px" />
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ProductName" ItemStyle-HorizontalAlign="Left"
                    HeaderText="商品名称" ControlStyle-Width="300">
                    <ItemTemplate>
                        <div title="编辑宝贝标题" class="item-title-area">
                            <p id="p_<%# Eval("ProductId")%>" class="autobrake">
                                <%# Eval("ProductName")%>
                            </p>
                            <textarea id="txtProductName_<%# Eval("ProductId")%>" rows="2" class="txtpname" i="<%# Eval("ProductId")%>"><%# Eval("ProductName")%></textarea>
                            &nbsp;<img alt="编辑宝贝标题" id="img_<%# Eval("ProductId")%>" title="编辑宝贝标题" src="/admin/Images/up_xiaobi.png"
                                onclick='changProductName(<%# Eval("ProductId")%>);' />
                            <br />
                        </div>
                        <a id="editsave_<%# Eval("ProductId")%>" href="javascript:void(0)" onclick="saveChange(<%# Eval("ProductId")%>);"
                            class="editsave" i="<%# Eval("ProductId")%>" style="margin-left: 239px; display: none; border: none; width: 41px; height: 20px;">保存</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="市场价" ItemStyle-HorizontalAlign="Center" SortExpression="MarketPrice">
                    <ItemTemplate>
                        <span id="MarketPrice_<%# Eval("ProductId")%>">
                            <%#Eval("MarketPrice", "￥{0:N2}")%></span>
                        <br />
                        <input id="TextMarketPrice_<%# Eval("ProductId")%>" i="<%# Eval("ProductId")%>" type="text"
                            class="MarketPrice_Input" value="  <%#Maticsoft.Common.Globals.SafeDecimal(Eval("MarketPrice").ToString(),0).ToString("f")%>"
                            style="width: 80px; display: none;" />
                        &nbsp;<img alt="编辑商品市场价" id="imgMarketPrice_<%# Eval("ProductId")%>" src="/admin/Images/up_xiaobi.png"
                            onclick="UpdateMarketPrice(<%# Eval("ProductId")%>)" />
                        <br />
                        <a id="aMarketPrice_<%# Eval("ProductId")%>" href="javascript:void(0)" style="display: none;"
                            onclick="SaveMarketPrice(<%# Eval("ProductId")%>)" class="MarketPrice_save" i="<%# Eval("ProductId")%>">保存</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="最低销售价" ItemStyle-HorizontalAlign="Center" SortExpression="LowestSalePrice">
                    <ItemTemplate>
                         <%# Convert.ToBoolean(Eval("IsMany"))?"<span style='color:#1317fc;'>"+Eval("SalePrice")+"</span>":Eval("SalePrice") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                    HeaderText="最低成本价">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("IsMany"))?"<span style='color:#1317fc;'>"+Eval("CostPrice")+"</span>":Eval("CostPrice") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="所在分类">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litProductCate"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                    HeaderText="商家">
                    <ItemTemplate>
                        <%#GetSupplier(Eval("SupplierId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="进口商品">
                  <ItemTemplate>
                        <%#GetImportPro(Eval("ProductId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="VistiCounts" ItemStyle-HorizontalAlign="Center"
                    HeaderText="浏览" Visible="False">
                    <ItemTemplate>
                        <%#Eval("VistiCounts") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="库存">
                    <ItemTemplate>
                        <span id="StockNum_<%# Eval("ProductId")%>">
                            <%#StockNum(Eval("ProductId"))%></span>
                        <br />
                        <input id="TextStockNum_<%# Eval("ProductId")%>" i="<%# Eval("ProductId")%>" type="text"
                            class="StockNum_Input" value="  <%#StockNum(Eval("ProductId"))%>" style="width: 80px; display: none;" />
                        &nbsp;<img alt="编辑商品库存" id="imgStockNum_<%# Eval("ProductId")%>" src="/admin/Images/up_xiaobi.png"
                            onclick="UpdateStock(<%# Eval("ProductId")%>)" style="display: none" />
                        <br />
                        <a id="aStockNum_<%# Eval("ProductId")%>" href="javascript:void(0)" style="display: none;"
                            onclick="SaveStockNum(<%# Eval("ProductId")%>)">保存</a>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                    HeaderText="已售数量">
                    <ItemTemplate>
                        <%#Eval("SaleCounts")%>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField SortExpression="AddedDate" ItemStyle-HorizontalAlign="Center"
                    HeaderText="发布时间" ControlStyle-Width="120">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("AddedDate")).ToString("yyyy-MM-dd")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="二维码" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div class="code_hover" style="background: url(/admin/images/qr.png)  no-repeat; width: 37px; height: 48px; cursor: pointer;">
                            <div id="code_img" style="display: none; width: 120px; height: 122px; background-color: white; position: relative; border: 1px solid silver; padding-top: 5px; top: -50px; left: -42px;">
                                <img style="margin: 0 auto; display: block;" src="/Upload/Shop/QR/Product/<%# Eval("ProductId")%>.png" width="100px" height="100px" />
                                <span>扫描或右键另存</span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a target="mainFrame" style="white-space: nowrap;" onclick="popTips(<%#Eval("ProductId") %>)">编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a target="mainFrame" class="iframe" style="white-space: nowrap;" href="/admin/shop/ProductAccessories/List.aspx?pid=<%#Eval("ProductId") %>&acctype=1">[组合配件]</a><br />
                        <a target="mainFrame" class="iframe" style="white-space: nowrap;" href="/admin/shop/ProductAccessories/List.aspx?pid=<%#Eval("ProductId") %>&acctype=2">[组合优惠]</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
            <footerstyle height="25px" horizontalalign="Right" />
            <headerstyle height="35px" />
            <pagerstyle height="25px" horizontalalign="Right" />
            <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
            <rowstyle height="25px" />
            <sortdirectionstr>DESC</sortdirectionstr>
        </cc1:GridViewEx>
        <div style="text-align: center;">
            <webdiyer:AspNetPager ID="aspnetpager" runat="server"
                AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
                OnPageChanged="AspNetPager_PageChanged" PrevPageText="上一页"
                PageSize="10" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页显示%PageSize%条，共%RecordCount%条" ShowCustomInfoSection="Left" />
        </div>

        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
            <tr>
                <td style="width: 5px;"></td>
                <td align="left">
                    <asp:Button ID="btnDelete" OnClientClick="return confirm('你确定要放入回收站吗？要还原商品请到回收站找回！')"
                        runat="server" Text="批量删除" class="adminsubmit" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnInverseApprove" runat="server" Text="批量下架" class="adminsubmit"
                        OnClick="btnInverseApprove_Click" />
                    <asp:Button ID="btnCheck" runat="server" Text="批量审核" class="adminsubmit" OnClick="btnCheck_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".select2").select2({placeholder: "请选择",width:'240px'});
            $("#ddlSupplier").select2({placeholder:"请选择",width:"240px"});


            $('.code_hover').hover(
                function () {
                    $(this).find('#code_img').show();
                },
                function () {
                    $(this).find('#code_img').hide();
                });
        });
    </script>

</asp:Content>
