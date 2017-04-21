<%@ Page Title="Shop_Brands" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="AList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Brands.AList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/admin/js/jquery/brandslist.js" type="text/javascript"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <input type="hidden" id="hidDelbtn" runat="server"/>
    <input type="hidden" id="hidModifybtn" runat="server"/>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="商品品牌" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、
                        <asp:Literal ID="Literal3" runat="server" Text="删除商品品牌" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <label ID="Literal4">品牌名称</label>：
                    <input type="text" ID="txtBrandName"></input>&nbsp;&nbsp;
                    <label ID="Literal5">品牌拼音</label>：
                    <input type="text" ID="txtBrandSpell"></input>
                    <input type ="button" id="btnQuery" value="搜索" class="adminsubmit_short" />
                    
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="add.aspx">
                        添加</a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li id="lione" class="active" onclick="nTabs(this,0,-1);"><a href="javascript:;">全部</a></li>
                   <%-- <%=strLiList %>--%>
                </ul>
            </div>
        </div>

        <table cellspacing="0" cellpadding="0" rules="all" border="0px" cellpadding="4px"  cellspacing="1px" class="GridViewTyle" border="1" id="tbBrandsList"  style="border-color: #CCCCCC; border-width: 1px; border-style: solid; width: 100%; border-collapse: collapse;border-top:none;">
        <tr height="27px" style="background-color: #E3EFFF; height: 35px; background: #FFF;border-top:none;">
            <th style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;border-top:none;width:3%;display:none;"><a href="javascript:;" style="color: #003366;">选择</a> </th>
            <th style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;border-top:none;width:5%;"> <a href="javascript:;" style="color: #003366;">序号</a> </th>
            <th style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;border-top:none;width:8%;"> <a href="javascript:;" style="color: #003366;">品牌名称</a> </th>
            <th style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;border-top:none;width:10%;"> <a href="javascript:;" style="color: #003366;">品牌图片</a> </th>
            <th style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;border-top:none;"> <a href="javascript:;" style="color: #003366;">品牌描述</a> </th>
            <th style="border: 1px solid #dae2e8; border-left: 0px; border-right: 0px;border-top:none;width:240px"> <a href="javascript:;" style="color: #003366;">操作</a> </th>
        </tr>
        <tr height="27px"  style="display:none;">
            <td valign="middle" height="27px" style="width: 30px; padding-left: 5px; height: 27px;"> ssss </td>
            <td align="center" height="27px" style="padding-left: 5px; height: 27px;"> 动软卓越 </td>
            <td align="center" height="27px" style="padding-left: 5px; height: 27px;"> <img  /> </td>
            <td align="left" height="27px">   http://www.maticsoft.com  </td>
            <td align="center" height="27px" style="padding-left: 5px; height: 27px; "> <a href="Show.aspx?id=1" style="display: inline-block; width: 50px;">详细</a>  </td>
            <td align="center" height="27px" style="padding-left: 5px; height: 27px; "> <a href="Modify.aspx?id=1" style="display: inline-block; width: 50px;">编辑</a> </td>
        </tr>
    </table>
        <div id="pageBar" style="float: right;">
        </div>
        <%--<div id="div_load" style="text-align: center; width: 100%">
            <img src="/admin/Images/ajax-loader.gif" /></div>--%>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <input id="btnSelect" type="button" title="全 选" value="全 选" class="adminsubmit_short"  style="display:none;"/>
                    <input id="btnDelete" type="button" title="删 除" value="删 除" class="adminsubmit_short" style="display:none;"/>
                    <%--<asp:Button ID="btnDelete1" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />--%>
                </td>
            </tr>
        </table>
    </div>
      
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
