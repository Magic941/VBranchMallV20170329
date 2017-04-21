<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SelectCategory.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ShopCategories.SelectCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*890px*/
        .results_pos { position: relative; overflow: hidden; background: red; float: left; width: 450px; background: #e5f0ff; border: 1px solid #c7deff; border-left: 0; height: 298px; }
        .results_ol { position: absolute; display: block; overflow: hidden; clear: both; left: 0px; }
    </style>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="dataarea mainwidth td_top_ccc" style="background: white;">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h1 class="title_height">
                选择分类</h1>
        </div>
        <div class="search_results">
        </div>
        <div class="results">
            <div class="results_main" style="overflow: hidden;">
                <div class="results_left">
                    <label>
                        <input type="button" name="button2" id="button2" value="" class="search_left" />
                    </label>
                </div>
                <div class="results_pos">
                    <ol class="results_ol">
                    </ol>
                </div>
                <div class="results_right">
                    <label>
                        <input type="button" name="button2" id="button2" value="" class="search_right" />
                    </label>
                </div>
            </div>
        </div>
        <div class="results_img">
        </div>
        <div class="results_bottom">
            <span class="spanE">您当前选择的是：</span> <span id="fullName"></span>
        </div>
        <div class="bntto">
            <%--<input type="submit" name="button2" id="btnNext" value="确定选择" class="adminsubmit" />--%>
            <input type="button" name="button2" id="btnNext" value="确定选择" class="adminsubmit" />
            <input type="hidden" value="true" id="Hidden_isCate" />
            <input type="hidden" value="true" id="Hidden_SelectValue" />
        </div>
    </div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceCheckright" runat="server">
    <script src="/admin/js/jquery/SelectCategoryAuto.helper.js" type="text/javascript"></script>
</asp:content>
