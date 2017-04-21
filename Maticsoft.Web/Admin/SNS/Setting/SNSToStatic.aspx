
<%@ Page Title="SNS静态生成" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
  CodeBehind="SNSToStatic.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Setting.SNSToStatic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
  <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
       <link href="/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/SNSToStatic.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 32px;
        }
        .style2
        {
            width: 72px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("a.iframe").colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <asp:HiddenField ID="txtTaskCount" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtTaskCount_C" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtIsStatic" runat="server"></asp:HiddenField>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="社区静态化管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="设置社区是否启用静态化，并根据条件进行静态页面生成。" />
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="Table1">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style=" width:80px">
                    开启静态化：
                </td>
                <td >
                    <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="radlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="true">是</asp:ListItem>
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabIndex">
            <tr >
                <td height="18px;" style="width: 80px">
                </td>
           
                <td >
                   <asp:CheckBox ID="ckIndex" runat="server"  /> <span>首页</span>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                     <span style="display: none"> <asp:CheckBox ID="ckGroup" runat="server"  /><span>小组首页</span>
                        &nbsp;&nbsp;&nbsp;&nbsp;</span>
                     <asp:CheckBox ID="ckAlbum" runat="server"  /><span>专辑首页</span> 
                </td>
            </tr>
            <tr >
                <td height="18px;" style="width: 80px">
                </td>
                <td >
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button5" runat="server" Text="生成" OnClick="btnIndex_Click"
                    class="adminsubmit_short" />           
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabTask" >
            <tr>
                <td height="18px;">
                </td>
                <td colspan="2">
                    <h3>
                        SNS静态生成</h3>
                </td>
            </tr>
            <tr class="td_class">
                <td >
                </td>
                <td style="width: 80px"  class="td_class">
                    生成类型：
                </td>
                <td style="text-align: left; ">
                    <select id="txtType" style=" width:120px">
                        <option  value="4">产品内容页</option>
                             <option value="5">图片内容页</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td  style="width: 80px"  class="td_class">
                    选择时间：
                </td>
                <td style="text-align: left">
                   <asp:TextBox ID="txtFrom" runat="server"  name="from" style="width: 90px" ></asp:TextBox>
                    --
                     <asp:TextBox ID="txtTo" runat="server"  name="to" style="width: 90px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td>
                </td>
                <td style="text-align: left">
                    <%--  <asp:Button ID="btnToStatic" runat="server" Text="静态化"  OnClick="btnToStatic_Click"/>--%>
                    <input id="btnToStatic" type="button" value="生成" class="adminsubmit" />
                </td>
            </tr>
            <tr style="height: 60px">
                <td colspan="3" id="probar" style="display: none;">
                    <div id="progressbar" style="width: 560px;">
                    </div>
                    <div style="width: 560px; text-align: center">
                        <span id="txtCount"></span>/共<span id="txtTotalCount"></span></div>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%; display: none;"
            cellpadding="3" class="borderkuang" id="txtRemain">
            <tr>
                <td>
                产品详细静态化任务
                </td>
                <td colspan="2">
                    <div>
                        上次任务断点时间：<asp:Literal ID="txtTaskDate" runat="server"></asp:Literal>
                        &nbsp;&nbsp;任务ID：<asp:Literal ID="txtTaskId" runat="server"></asp:Literal>
                        &nbsp;&nbsp;剩余条数：<asp:Literal ID="txtTaskReCount" runat="server"></asp:Literal></div>
                    <div style="padding-left: 160px">
                        <input id="btnContinue" type="button" value="继续任务" class="adminsubmit" />
                        &nbsp;&nbsp;
                        <input id="btnRemove" type="button" value="清除任务" class="adminsubmit" /></div>
                </td>
            </tr>
        </table>
        
        <table border="0" cellspacing="1" style="width: 100%; height: 100%; display: none;"
            cellpadding="3" class="borderkuang" id="txtRemain_C">
            <tr>
                <td>
                图片详细静态化任务
                </td>
                <td colspan="2">
                    <div>
                        上次任务断点时间：<asp:Literal ID="txtTaskDate_C" runat="server"></asp:Literal>
                        &nbsp;&nbsp;任务ID：<asp:Literal ID="txtTaskId_C" runat="server"></asp:Literal>
                        &nbsp;&nbsp;剩余条数：<asp:Literal ID="txtTaskReCount_C" runat="server"></asp:Literal></div>
                    <div style="padding-left: 160px">
                        <input id="btnContinue_C" type="button" value="继续任务" class="adminsubmit" />
                        &nbsp;&nbsp;
                        <input id="btnRemove_C" type="button" value="清除任务" class="adminsubmit" /></div>
                </td>
            </tr>
        </table>
    </div>
    <div style='display: none; width: 700px;'>
        <div class="dataarea mainwidth td_top_ccc" style="background: white;" id='divModal'>
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
                <input type="button" name="button2" id="btnNext" value="确定选择" class="adminsubmit" />
                <input type="hidden" value="true" id="Hidden_isCate" />
                <asp:HiddenField ID="Hidden_SelectValue" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

