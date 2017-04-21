<%@ Page Title="优惠券分类管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ClassList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Coupon.ClassList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "680", height: "360", overlayClose: false });

            $(".btnUpdate").click(function () {
                var _self = $(this);
                var ClassId = _self.attr("classid");
                var value = _self.prev().val();
                if (isNaN(parseInt(value)) || parseInt(value) <= 0) {
                    ShowFailTip('请填写正确的顺序值');
                    return;
                }
                $.ajax({
                    url: ("ClassList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "UpdateSeqNum", Callback: "true", ClassId: ClassId, UpdateValue: value },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            ShowSuccessTip('保存成功');
                            self.prev().val(value);
                        }
                        else {
                            ShowFailTip('服务器繁忙，请稍候再试！');
                        }
                    }
                });
            });


            $(".btnStatus").each(function () {
                var ruleId = $(this).attr("classId");
                var status = parseInt($(this).attr("status"));
                if (status == 0) {
                    $(this).css("color", "red");
                }
                else {
                    $(this).css("color", "green");
                }
            })
            $(".btnStatus").click(function () {
                var classId = $(this).attr("classId");
                var status = parseInt($(this).attr("status"));
                status = status == 0 ? 1 : 0;
                var self = $(this);
                $.ajax({
                    url: ("ClassList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "UpdateStatus", Callback: "true", ClassId: classId, Status: status },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            ShowSuccessTip("操作成功");
                            self.attr("status", status);
                            if (status == 0) {
                                self.text("未启用");
                                self.css("color", "red");
                            }
                            else {
                                self.text("启用");
                                self.css("color", "green");
                            }
                        }
                        else {
                            alert("系统忙请稍后再试！");
                        }
                    }
                });
            })

        });
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="优惠券分类管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行添加，编辑，删除优惠券分类操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="分类名称搜索" />：
              <asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox><asp:Button ID="Button1"
                                runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click"
                                class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li id="liAdd" runat="server" style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px;"><a href="AddClass.aspx"
                        class="iframe">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                    </li>
                </ul>
            </div>
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ClassId,Sequence" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                  <asp:TemplateField HeaderText="显示顺序" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSequence" runat="server" Text='<%#Eval("Sequence")%>' Width="50px"></asp:TextBox>
                        <a href="javascript:;" classid='<%#Eval("ClassId") %>' class="btnUpdate" style="display: none">保存</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="分类名称" SortExpression="Name" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="状态" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                           <a class="btnStatus" classId='<%# Eval("ClassId") %>'  status='<%# Eval("Status") %>'> <%# GetStatus(Eval("Status")) %> </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120">
                    <ItemTemplate>
                      <span id="btnModify" runat="server"><a href='UpdateClass.aspx?id=<%#Eval("ClassId") %>' class="iframe">编辑</a></span>  
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
       <asp:Button ID="btnSaveEq" runat="server" Text="保存顺序" OnClick="btnSaveEq_Click" CssClass="adminsubmit" />
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
