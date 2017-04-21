<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Tags.Add" Title="增加标签类型" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/admin/js/jquery/SelectTagsCategoryAuto.helper.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("a.iframe").colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" });
        $("#ctl00_ContentPlaceHolder1_txtCategoryText").focus(function () {
            $(this).colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" });
        })
    });
</script>
<style type="text/css">
    /*890px*/
    .results_pos { position: relative; overflow: hidden; background: red; float: left; width: 450px; background: #e5f0ff; border: 1px solid #c7deff; border-left: 0; height: 298px; }
    .results_ol { position: absolute; display: block; overflow: hidden; clear: both; left: 0px; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
         <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="增加标签" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以增加新的标签" />
                    </td>
                </tr>
            </table>
        </div>
         <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                   <table cellspacing="0" cellpadding="0" width="100%" border="0">

            <tr>
                <td height="25" width="30%" align="right">
                    选择上级分类 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtCategoryText" runat="server" Width="215" ReadOnly="true" ></asp:TextBox>
                    <a class='iframe' href="javascript:;">选择分类</a>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    标签名称:
                </td>
                <td height="25" width="*" align="left">                    
                    <asp:TextBox ID="txtTagName" runat="server" Width="215"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="Hidden_SelectName"/>
                </td>
            </tr>
            <tr>
                <td height="25" width="30%" align="right">
                    是否推荐:
                </td>
                <td height="25" width="*" align="left">
                    <asp:DropDownList ID="radlIsRecommand" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="true" Selected="True">推荐</asp:ListItem>
                        <asp:ListItem Value="false">不推荐</asp:ListItem>
                    </asp:DropDownList>
                </td>   
            </tr>
            <tr>
                <td class="td_class">
                    状态:
                </td>
                <td height="25" width="*" align="left">
                    <asp:DropDownList ID="radlStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">可用</asp:ListItem>
                        <asp:ListItem Value="0">不可用</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td height="20"></td>
            </tr>
            <tr>
                <td class="td_class">
                </td>
                
                <td height="25">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                        class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                </td>
            </tr>

                        
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <!--Add end -->
    </div>
    <div style='display: none;width: 700px;'>

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
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
