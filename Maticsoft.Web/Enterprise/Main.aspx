﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Enterprise/Basic.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Maticsoft.Web.Enterprise.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="admincenter">
<div class="admintitle"><div class="sj"><img src="/images/zao.gif" width="34" height="25" /></div>
<span><strong style="font-size:14px;"><%=CurrentUserName%> 您好，欢迎使用动软框架企业后台管理系统</strong> <a href="javascript:;"> 账号设置</a></span></div>
</div>
<div class="main_main"><div class="sj"><img src="/images/icon5.gif" width="22" height="20" /></div> 
<span class="main_time">您上次登录的时间：<asp:Literal ID="LitLastLoginTime" runat="server"></asp:Literal>
</span><br />&nbsp;&nbsp;&nbsp;&nbsp; 
<strong><asp:Literal ID="litMsg" runat="server"></asp:Literal></strong> </div>

<div class="main_line"></div>


<div class="main_iconmenu">
<span><img src="/images/icon_1.gif" width="36" height="32" /><br />网站设置</span>
<span><img src="/images/icon_2.gif" width="36" height="32" /><br />发布文章</span>
<span><img src="/images/icon_3.gif" width="36" height="32" /><br />数据统计</span>
<span><img src="/images/icon_4.gif" width="36" height="32" /><br />文件上传</span>
<span><img src="/images/icon_5.gif" width="36" height="32" /><br />目录管理</span>
<span><img src="/images/icon_6.gif" width="36" height="32" /><br />报表查询</span></div>


<div class="main_tj"><a href="javascript:;">添加新的快捷功能</a></div>

<div class="admintitle adminxia"><div class="sj" style=" margin-right:20px;"><img src="/images/icon6.gif" width="21" height="28" /></div>
<strong>系统使用指南</strong></div>

<div class="main_bottomzi">
<ul>
<li>您可以快速进行文章发布管理操作</li> 
<li><a href="javascript:;">发布或管理文章</a></li></ul>
<ul>
<li>您可以快速发布产品</li> 
<li><a href="javascript:;">发布或管理产品</a><li></ul>
<ul><li>您可以快速进行密码修改、账户设置等操作</li> 
<li><a href="javascript:;">账户管理</a><li></ul>
</div>
<div class="main_line_1"></div>
<div class="main_3"><div class="sj"><img src="/images/icon7.gif" width="15" height="15" /></div> 
<div class="mainzileft">我的个人资料管理</div></div>

<div class="main_xiaocaidan">
<ul>
<li><a href="javascript:;">登录信息</a></li>
<li><a href="javascript:;">修改密码</a></li>
<li><a href="javascript:;">个人资料</a></li>
<li><a href="javascript:;">我的账户</a></li>
<li><a href="javascript:;">信息中心</a></li>
<li><a href="javascript:;">定制菜单</a></li>

</ul>
</div>

</asp:Content>
