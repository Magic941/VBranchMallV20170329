<%@ Page Title="会员信息管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Goodmanagement.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <style>
        .a {
            color: #f6780e;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("[id$='txtEndTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
            $("[id$='txtBeginTime']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });

            //审核按钮
            $(".ShipAction").each(function () {
                var userstatus = $(this).attr("userstatus");

                if (userstatus.indexOf('处理中') > 0) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });

        });
        //修改试用状态
        function changefun(obj) {
            var valuenumber = obj.value;
            var userId = $(obj).attr("RadProbation");

            $.ajax({
                url: "/ShopManage.aspx",
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "UpdateGoodUserProbation", Callback: "true", UserID: userId, Valuenumber: valuenumber },
                async: false,
                success: function (resultData) {

                    if (resultData.DATA == "Approve") {
                        alert("修改成功!");
                    }
                    else {
                        alert("修改失败!");
                        return false;
                    }
                }
            });
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">会员信息管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">您可以进行添加、删除用户标签信息操作
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
                    <asp:Literal ID="Literal2" runat="server" Text="昵称" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    手机：
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>

                    &nbsp;&nbsp;&nbsp;&nbsp;
                    邮箱：
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    健康卡：
                    <asp:TextBox ID="txtHaolinCard" runat="server"></asp:TextBox>

                    <br />
                    <div style="margin-top: 15px;">
                        <%--0 表示会员 1 表示好粉 2 表示好代 3 分销店，4服务店--%>
                    通过申请的类型:
                  <asp:DropDownList ID="DropUserAppType" runat="server">
                      <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                      <asp:ListItem Value="0" Text="会员"></asp:ListItem>
                      <asp:ListItem Value="1" Text="好粉"></asp:ListItem>
                      <asp:ListItem Value="2" Text="个人微店"></asp:ListItem>
                      <asp:ListItem Value="3" Text="分销店"></asp:ListItem>
                      <asp:ListItem Value="4" Text="服务店"></asp:ListItem>
                  </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    提交申请的类型:
                  <asp:DropDownList ID="DropUserOldType" runat="server" >
                      <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                      <asp:ListItem Value="0" Text="会员"></asp:ListItem>
                      <asp:ListItem Value="1" Text="好粉"></asp:ListItem>
                      <asp:ListItem Value="2" Text="个人微店"></asp:ListItem>
                      <asp:ListItem Value="3" Text="分销店"></asp:ListItem>
                      <asp:ListItem Value="4" Text="服务店"></asp:ListItem>
                  </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     试用状态:
                    <asp:DropDownList ID="DropProbation" runat="server" >
                        <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="正常使用"></asp:ListItem>
                        <asp:ListItem Value="1" Text="开始试用"></asp:ListItem>
                        <asp:ListItem Value="2" Text="暂停试用"></asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    审核状态:
                    <asp:DropDownList ID="DropUserStatus" runat="server" >
                        <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="处理中"></asp:ListItem>
                        <asp:ListItem Value="1" Text="通过审核"></asp:ListItem>
                        <asp:ListItem Value="2" Text="未通过审核"></asp:ListItem>
                    </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                            OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowCommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting"
            UnExportedColumnNames="Modify" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="UserID">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="编号" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="40" SortExpression="UserID" />
                <asp:TemplateField ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Width="80px" Height="80px" ImageAlign="Middle"
                            ImageUrl='<%#GetGravatar(Eval("UserID")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 100px">用 户 名：
                                    <%#Eval("UserName")%>
                                </td>
                                <td style="width: 240px">性&nbsp;&nbsp;&nbsp;&nbsp;别：
                                    <%#Eval("Sex").ToString().Trim() == "0" ? "女" : "男"%>
                                </td>
                            </tr>
                            <tr>
                                <td>昵&nbsp;&nbsp;&nbsp;&nbsp;称： <span style="color: Gray">
                                    <%#Eval("NickName")%></span>
                                </td>
                                <td>真实姓名：<span style="color: Gray">
                                    <%#Eval("TrueName")%></span>
                                </td>
                            </tr>
                            <tr>
                                <td>手机号码： <span style="color: Gray">
                                    <%#Eval("Phone")%></span>
                                </td>
                                <td>邮&nbsp;&nbsp;&nbsp;&nbsp;箱：<span style="color: Gray">
                                    <%#Eval("Email")%></span>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="推荐人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <span><%#GetRecommendUserName(Eval("RecommendUserID")) %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="User_dateCreate" HeaderText="申请时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100" SortExpression="User_dateCreate" />
                <asp:TemplateField HeaderText="申请状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <span><%#GetUserAppType(Eval("UserID")) %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="试用状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90">
                    <ItemTemplate>
                        <span style="width: 150px; height: 200px;" id="sp_<%#Eval("UserID")%>" class="RadProbation">
                            <input value='<%#Eval("Probation")%>' id="hidProbationstatus" class='hid_<%#Eval("UserID")%>' runat="server" style="display: none" />

                            <input type="radio" name="identity" id="RadProbation2" radprobation='<%#Eval("UserID")%>' runat="server" value="0" onchange='changefun(this)' />正常使用 
                            <input type="radio" name="identity" id="RadProbation3" radprobation='<%#Eval("UserID")%>' runat="server" value="1" onchange='changefun(this)' />开始试用
                            <input type="radio" name="identity" id="RadProbation4" radprobation='<%#Eval("UserID")%>' runat="server" value="2" onchange='changefun(this)' />暂停试用
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <span class="UserStatus"><%#GetUserStatus(Eval("UserID")) %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <a href='GoodMShow.aspx?id=<%#Eval("UserID") %>' class="ShipAction" userstatus="<%#GetUserStatus(Eval("UserID"))%>" style="display: none;">审核</a>&nbsp;
                        <a href='ExamineGoodM.aspx?id=<%#Eval("UserID") %>'>查看</a>&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
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
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 400px; height: 100%">
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "800", height: "400", overlayClose: false });
        });
    </script>
</asp:Content>
