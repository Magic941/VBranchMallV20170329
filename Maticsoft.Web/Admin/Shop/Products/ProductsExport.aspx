<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductsExport.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductsExport" %>
<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<%@ Register TagPrefix="Maticsoft" TagName="CategoriesDropList" Src="~/Controls/CategoriesDropList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" type="text/css" rel="stylesheet" />
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <script src="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <link href="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
        <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
        <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
     <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <style> 
    .selecttr {border-spacing:5em}
    .tdalign td{
        
      text-align:left  
    }
    </style>
       <script type="text/javascript">
           $(document).ready(function () {
               $("input.openField").colorbox({ width: "auto", height: "auto", inline: true, href: "#divPackage" });
               resizeImg('.img', 80, 80);
           });
         
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HidSelectValue" runat="server" />
    <asp:HiddenField ID="hfRelatedProducts" runat="server" />
    <div class="newslistabout">
                <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                      导出商品
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                     根据条件选择要导出的商品和字段，导出Excel文件
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
              <tr class="selecttr">
                <td>
                    
                </td>
                   <td id="pc" style=" padding-bottom: 1em; ">
                       类别：  <Maticsoft:CategoriesDropList ID="CategoriesDropList1" runat="server" IsNull="true" />        
                         <script> $("#pc>div").css("display", "inline");</script>
                </td>

            </tr>
                <tr class="selecttr">
                <td>
                    
                </td>
                   <td style=" padding-bottom: 1em; ">
                       地区：        
                    <script src="/Scripts/jquery/jquery.guid.js" type="text/javascript"></script>
                     <asp:HiddenField ID="hfSelectedNode" runat="server"  />
                    <script src="/Scripts/jquery/maticsoft.selectregion.js" handle="/RegionHandle.aspx" isnull="true" type="text/javascript"></script>
                </td>

            </tr>
            <td>
                  <tr class="selecttr">
                <td>
                    
                </td >
                   <td style=" padding-bottom:1em; ">
                        <asp:Literal ID="Literal5" runat="server" Text="时间："></asp:Literal>
                    <asp:TextBox ID="txtBeginTime" runat="server" CssClass="PostDate" ></asp:TextBox>
                    <asp:Literal ID="Literal6" runat="server" Text="--"></asp:Literal>
                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="PostDate" ></asp:TextBox>
                </td>

            </tr>
            
                <tr class="selecttr">
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody" style=" padding-top: 10px; ">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                     品牌： <asp:dropdownlist ID="drpProductBrand" runat="server">
                                </asp:dropdownlist>
              <%--       类型： <asp:dropdownlist ID="drpProductCategory" runat="server">
                                </asp:dropdownlist>--%>
                     关键字：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                   
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        
            <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" unexportedcolumnnames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="1" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ProductId"
            ShowToolBar="True">
            <Columns>
                <asp:TemplateField HeaderText="" SortExpression="LOGO" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div class="img">
                       <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank">  <img ID="imgLOGO" runat="server"  ref='<%# Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(),"T150X150_") %>' /></a></div>
                        
                    <asp:HiddenField runat="server" ID="HidPid" Value='<%# Eval("ProductId") %>'>
                    </asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="ProductName" ItemStyle-HorizontalAlign="Left"
                    HeaderText="商品名称">
                    <ItemTemplate>
                        <span id="spProductName<%# Eval("ProductId")%>">
                            <%# Eval("ProductName")%></span></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  ItemStyle-HorizontalAlign="Left"
                    HeaderText="所在分类">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litProductCate"></asp:Literal>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品价格" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        市场价：<span style="color: red"><%#Eval("MarketPrice", "￥{0:N2}")%></span><br />销售价：<span style="color: green"><%#Eval("LowestSalePrice", "￥{0:N2}")%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="VistiCounts" ItemStyle-HorizontalAlign="Left"
                    HeaderText="库存数量">
                    <ItemTemplate>
                        <%#StockNum(Eval("ProductId"))%>
                     </ItemTemplate>
                </asp:TemplateField>
                       <asp:BoundField DataField="SaleCounts" HeaderText="销售数量" SortExpression="SaleCounts"
                    ItemStyle-HorizontalAlign="Left" /> 
                    <asp:BoundField DataField="AddedDate" HeaderText="添加时间" SortExpression="AddedDate"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
                <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        
           <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
               <input type="button"   class="adminsubmit openField" value="导出文件"/>
                 
                </td>
            </tr>
        </table>

        <div style='width: 700px;display:none '>
                    <div class="dataarea mainwidth td_top_ccc" style="background: white;" id='divPackage'>
                        <div class="advanceSearchArea clearfix">
                            <!--预留显示高级搜索项区域-->
                        </div>
                        <div class="toptitle">
                            <h1 class="title_height">
                               选择导出字段 </h1>
                        </div>
                        <%--<div class="search_results">
                            <div id="categoryEx" style="display: block;margin-bottom: 10px;">
                                <h2>
                                    <span>删除</span>已添加分类</h2>
                                <ul id="selectCategoryEx">
                                </ul>
                            </div>
                        </div>--%>
                        <div class="results">
                            <div>
                            <asp:CheckBoxList ID="chkTableField" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" TextAlign="Right" CssClass="tdalign" >
                             <asp:ListItem Value="CategoryId" Text="类别ID"></asp:ListItem>
                             <asp:ListItem Value="TypeId" Text="类型ID"></asp:ListItem>
                             <asp:ListItem Value="ProductId" Text="商品ID"></asp:ListItem>
                             <asp:ListItem Value="BrandId" Text="品牌Id"></asp:ListItem>
                             <asp:ListItem Value="ProductName" Text="名称"></asp:ListItem>
                             <asp:ListItem Value="ProductCode" Text="编码"></asp:ListItem>
                             <asp:ListItem Value="SupplierId" Text="供应商Id"></asp:ListItem>
                             <asp:ListItem Value="RegionId" Text="地区Id"></asp:ListItem>
                             <asp:ListItem Value="ShortDescription" Text="介绍"></asp:ListItem>
                             <asp:ListItem Value="Unit" Text="单位"></asp:ListItem>
                             <asp:ListItem Value="Description" Text="描述"></asp:ListItem>
                                 <asp:ListItem Value="Meta_Title" Text="SEO_Title"></asp:ListItem>
                             <asp:ListItem Value="Meta_Description" Text="SEO_Description"></asp:ListItem>
                             <asp:ListItem Value="Meta_Keywords" Text="SEO_KeyWord"></asp:ListItem>
                             <asp:ListItem Value="SaleStatus" Text="状态"></asp:ListItem>
                             <asp:ListItem Value="AddedDate" Text="添加日期"></asp:ListItem>
                             <asp:ListItem Value="VistiCounts" Text="访问次数"></asp:ListItem>
                             <asp:ListItem Value="SaleCounts" Text="售出总数"></asp:ListItem>
                             <asp:ListItem Value="Stock" Text="商品库存"></asp:ListItem>
                             <asp:ListItem Value="DisplaySequence" Text="显示顺序"></asp:ListItem>
                             <asp:ListItem Value="LineId" Text="生产线"></asp:ListItem>
                             <asp:ListItem Value="MarketPrice" Text="市场价"></asp:ListItem>
                             <asp:ListItem Value="LowestSalePrice" Text="最低价"></asp:ListItem>
                             <asp:ListItem Value="PenetrationStatus" Text="铺货状态"></asp:ListItem>
                             <asp:ListItem Value="MainCategoryPath" Text="分类路径"></asp:ListItem>
                             <asp:ListItem Value="ExtendCategoryPath" Text="扩展路径"></asp:ListItem>
                             <asp:ListItem Value="HasSKU" Text="是否有SKU"></asp:ListItem>
                             <asp:ListItem Value="Points" Text="积分"></asp:ListItem>
                             <asp:ListItem Value="ImageUrl" Text="图片路径"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl1" Text="图片路径1"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl2" Text="图片路径2"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl3" Text="图片路径3"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl4" Text="图片路径4"></asp:ListItem>
                                 <asp:ListItem Value="ThumbnailUrl5" Text="图片路径5"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl6" Text="图片路径6"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl7" Text="图片路径7"></asp:ListItem>
                             <asp:ListItem Value="ThumbnailUrl8" Text="图片路径8"></asp:ListItem>
                             <asp:ListItem Value="MaxQuantity" Text="最大购买量"></asp:ListItem>
                             <asp:ListItem Value="MinQuantity" Text="最小购买量"></asp:ListItem>
                             <asp:ListItem Value="Tags" Text="标签"></asp:ListItem>
                             <asp:ListItem Value="SeoUrl" Text="Url地址优化规则"></asp:ListItem>
                             <asp:ListItem Value="SeoImageAlt" Text="图片Alt信息"></asp:ListItem>
                             <asp:ListItem Value="SeoImageTitle" Text="图片Title信息"></asp:ListItem>
                                        </asp:CheckBoxList>
                       
                            </div>
                            <div id="packageList" style=" width: 500px; margin-top:15px">
                                

                            </div>
                        </div>
                        <div class="results_img">
                        </div>
                    
                            <asp:HiddenField runat="server" ID="selectPackageText" />
                        <div class="bntto">
                      <%--      <asp:Button ID="btnImport" runat="server" Text="导出" onclick="btnImport_Click" CssClass="adminsubmit_short" />--%>
                      <input id="fieldall" type="checkbox" value="0"/> <label id="fieldalltext" for="fieldall">全选</label>
                         <asp:Button ID="btnImport" runat="server" Text="导出"  CssClass="adminsubmit_short" onclick="btnImport_Click"  OnClientClick="javascript:$.colorbox.close();"
                                />
                            <input type="button" name="button2"  id="Button1" value="取消" class="adminsubmit_short" onclick="javascript:$.colorbox.close();"  />
                            <input type="hidden" value="true" id="Hidden_isCate" />
                    
                            
                        </div>
                    </div>
                </div>
             
     
    </div>
    <script type="text/javascript">
        $(function () {
            //去掉gridview自动生成的click事件
            $("#Checkbox2").removeAttr("onclick");
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
            var value = $("#ctl00_ContentPlaceHolder1_HidSelectValue").val();
//            SetCount(value);
            $('input[name$="ItemCheckBox"]').each(function () {
                var objectThis = $(this).parents("tr").find($('input[name$="HidPid"]'));
                if (value.indexOf(objectThis.val()) >= 0) {
                    $(this).prop('checked', true);
                }
            });
            //如果刚进入商品的选项框全部选中，则总的选项框选中
            CheckAllIsSelect();
            //给商品前面的选项框添加事件
            $('input[name$="ItemCheckBox"]').click(function () {
                checkEvent($(this));
                CheckAllIsSelect();
            });
            //总选项框点击触发的事件
            $("#Checkbox2").click(function () {
                if ($(this).is(':checked')) {
                    itemCheckEach(true);
                } else {
                    itemCheckEach(false);
                }
            });
            $("#fieldall").click(function () {
                if ($(this).is(':checked')) {
                    fieldCheckEach(true);
                } else {
                    fieldCheckEach(false);
                }
            });
        });

        //如果全选按钮选中，给商品前面的选择框的属性也全部选中，并且给相应的相应的隐藏域中赋相应的值
         function itemCheckEach(ischeck) {
            $('input[name$="ItemCheckBox"]').prop("checked", ischeck);
            $('input[name$="ItemCheckBox"]').each(function () {
                checkEvent($(this));
            });
        }
        function fieldCheckEach(ischeck) {
            $('input[name*="chkTableField"]').prop("checked", ischeck);

        }
         function checkEvent( object) {
            var tempval = $("#ctl00_ContentPlaceHolder1_HidSelectValue").val();
            var checkvalue = object.parents("tr").find($('input[name$="HidPid"]')).val();
            if (object.is(':checked')) {
                $("#ctl00_ContentPlaceHolder1_HidSelectValue").val(checkvalue + "," + tempval);
            } else {
                tempval = tempval.replace("" + checkvalue + ",", "");
                $("#ctl00_ContentPlaceHolder1_HidSelectValue").val(tempval);
            }
       //  SetCount(tempval);
         }

         function CheckAllIsSelect() {
             if ($('input[name$="ItemCheckBox"]:checked').length == $('input[name$="ItemCheckBox"]').length) {
                 $("#Checkbox2").prop("checked", true);
             } else {
                 $("#Checkbox2").prop("checked", false);
             }
         }
//         function SetCount(value) {
//             $("#selectcount").text(value.split(',').length);
//         }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>