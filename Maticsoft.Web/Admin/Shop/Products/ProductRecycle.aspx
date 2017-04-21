<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductRecycle.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductRecycle" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
     <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(function () {
            $("[id$='ddlSupplier']").select2({ placeholder: "请选择",width:'240px' });
            $(".select2-container").css("vertical-align", "middle");
            
            $("#ctl00_ContentPlaceHolder1_AspNetPager1").css("float", "right").css("padding-left", "20px");
            $("[id$='btnDeleteAll']").click(function () {
                DisableBtn();
                RunTask();
            });
            resizeImg('.group2', 120, 120);
        });
              function doProgressbar(count, i) {
                $("#probar").show();
                $.ajax({
                    url: ("ProductRecycle.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST',
                    dataType: 'json',
                    timeout: 10000,
                    data: {
                      Action: "DeleteProduct", 
                      Callback: "true",
                        TaskId: i
                    },
                    success: function(result) {
                        if (i <= count) {
                            $("#progressbar").progressbar({
                                value: i
                            });
                            $("#txtCount").text(i);
                            i++;
                            doProgressbar(count, i);
                        }
                        else {
                            alert("已全部清空");
                            EnableBtn();
                            RemoveTask();
                        }
                    }
                });
            }
            //执行任务
                  function RunTask() {
                $.ajax({
                    url: ("ProductRecycle.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST',
                    dataType: 'json',
                    timeout: 10000,
                    data: {
                        action: "DeleteAll",
                        Callback: "true"
                    },
                    success: function(result) {
                        if (result.STATUS == "SUCCESS" && result.DATA > 0) {
                            $("#progressbar").progressbar({
                                max: result.DATA,
                                value: 0
                            });
                            $("#txtTotalCount").text(result.DATA);
                            $("#txtCount").text(0);
                            $("#probar").show();
                            doProgressbar(result.DATA, 1);
                        }
                    }
                });
            }

              //清除任务
            function RemoveTask() {
                $.ajax({
                    url: ("ProductRecycle.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST',
                    dataType: 'json',
                    timeout: 10000,
                    data: {
                        action: "DeleteTask",
                          Callback: "true"
                    },
                    success: function() {
                        $("#probar").hide();
                          location.reload();
                    }
                });
            }
            function DisableBtn() {
                $("[id$='btnDeleteAll']").attr("disabled", "disabled");
                $("#ctl00_ContentPlaceHolder1_btnRevertAll").attr("disabled", "disabled");
                   $("#ctl00_ContentPlaceHolder1_btnDelete").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_btnRevert").attr("disabled", "disabled");
            }
            function EnableBtn() {
                $("[id$='btnDeleteAll']").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_btnRevertAll").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_btnDelete").removeAttr("disabled");
                   $("#ctl00_ContentPlaceHolder1_btnRevert").removeAttr("disabled");
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="商品回收站管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以还原、删除商品和清空回收站操作。
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />
                    <asp:Literal ID="Literal5" runat="server" Text="商家" />：
                    <span><asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                    </span>
                   商品名称：<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <br />

        <div class="newslist">
            <div class="newsicon">
                <ul class="list">
                    <li style="padding-left: 0px"  >
                        <input id="Checkbox1" type="checkbox" onclick='$(":checkbox").attr("checked", $(this).attr("checked")=="checked");' />
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,CheckAll %>" /></li>
  <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liRevert" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button2"  runat="server" OnClick="btnRevert_Click"
                             Text="批量还原" class="adminsubmit"  />
                    </li>
                      <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button1"  runat="server" OnClick="btnDelete_Click"
                             Text="<%$Resources:Site,btnDeleteListText %>" class="adminsubmit"  />
                    </li>
                     <li id="liRevertAll" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button3"  runat="server" OnClick="btnRevertAll_Click"
                             Text="还原所有" class="adminsubmit"  />
                    </li>
                     <li id="liDelAll" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="btnDeleteAll"   runat="server"  
                             Text="清空回收站" class="adminsubmit"  />
                    </li>
                
                    <li style="float: left; width: 680px; display:none" id="probar" >
                         <div id="progressbar" style="width: 560px; float:left;height: 17px;">
                    </div>
                       <div style=" float:left;padding-top: 2px;"> <span id="txtCount" ></span>/共<span id="txtTotalCount"></span></div>
                    </li>
                </ul>
            </div>
        </div>
        <table style="width: 100%; " cellpadding="5" cellspacing="5" class="border" runat="server" id="tableDataList">
            <tr>
                <td style=" width:960px">
                    <div id="Div1">
                        <asp:DataList ID="DataListProduct" RepeatColumns="5" RepeatDirection="Horizontal"
                            HorizontalAlign="Left" runat="server" OnItemCommand="DataList_RowCommand" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                            <a class="group2" href='<%#Maticsoft.Web.Components.FileHelper.GetImageUrl(Eval("ImageUrl").ToString())  %>' title='<%#Eval("ProductName") %>'>
                                                <img ref='<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(),"T350x350_") %>' style="width: 180px;" />
                                            </a>
                                            <br />
                                            <asp:CheckBox ID="ckProduct" runat="server" />
                                            <asp:HiddenField runat="server" ID="hfProduct" Value='<%#Eval("ProductID") %>' />
                                            <asp:Label ID="lblImageName" runat="server" Text='<%#Maticsoft.Common.StringPlus.SubString(Eval("ProductName"),"...",20,true) %>'></asp:Label><br />
                                         <span id="btnRevert" runat="server"> [<asp:LinkButton ID="linkRevert" runat="server" Style="color: #0063dc;" CommandName="Revert"
                                                CommandArgument='<%#Eval("ProductID") %>'>
                                                <asp:Literal ID="Literal3" runat="server" Text="还原" /></asp:LinkButton>]</span> <span id="btnDel" runat="server">[<asp:LinkButton
                                                    ID="lbtnDel" runat="server" Style="color: #0063dc;" CommandName="Delete" CommandArgument='<%#Eval("ProductID") %>'
                                                    OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                                                    <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton>]</span> </td></tr></table></ItemTemplate></asp:DataList></div></td></tr><tr>
                <td style=" width:960px">
                    <webdiyer:AspNetPager runat="server" ID="AspNetPager1" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPager1_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
