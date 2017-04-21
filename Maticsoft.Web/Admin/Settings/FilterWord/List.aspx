<%@ Page Title="SA_FilterWords" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.FilterWord.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .td_class {
            width: 75px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".btnEdit").click(function () {
                var itemId = $(this).attr("FilterId");
                var action = $(this).attr("Action");
                $(this).hide();
                $(this).next().show();

                $("#lblAction_" + itemId).hide();
                $("#lblRepalce_" + itemId).hide();
                $("#lblWord_" + itemId).hide();

                $("#txtWord_" + itemId).show();
                $("#ddlAction_" + itemId).show();
                if (action == 2) {
                    $("#txtRepalce_" + itemId).show();
                }
                $("#ddl_" + itemId).val(action);
            });
            $(".ddlAction").click(function () {
                var action = $(this).val();
                var itemId = $(this).attr("FilterId");
                $("#txtRepalce_" + itemId).hide();
                if (action == 2) {
                    $("#txtRepalce_" + itemId).show();
                }
            });
            $("#ctl00_ContentPlaceHolder1_ddSelect").click(function () {
                var action = $(this).val();
                $(this).parent().next().hide();
                if (action == 2) {
                  $(this).parent().next().show();
              }

          });
          $("#ctl00_ContentPlaceHolder1_ddlSelectType").click(function () {
              var action = $(this).val();
              $(this).next().hide();
              if (action == 2) {
                  $(this).next().show();
              }

          });
          
            

            $(".btnCancle").click(function () {
                $(this).parent().prev().show();
                $(this).parent().hide();
                var itemId = $(this).parent().attr("FilterId");


                $("#lblAction_" + itemId).show();
                $("#lblRepalce_" + itemId).show();
                $("#lblWord_" + itemId).show();

                $("#txtWord_" + itemId).hide();
                $("#ddlAction_" + itemId).hide();
                $("#txtRepalce_" + itemId).hide();
            });

            $(".btnUpdate").click(function () {
                var _self = $(this);
                var itemId = _self.parent().attr("FilterId");
                var word = $("#txtWord_" + itemId).val();
                var action = $("#ddl_" + itemId).val();

                var replace = $("#txtRepalce_" + itemId).val();
                if (word == "") {
                    alert("请输入关键词");
                    return;
                }
                if (action == 2 && replace == "") {
                    alert("请输入替换词");
                    return;
                }

                $.ajax({
                    url: ("List.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "Update", Callback: "true", FilterId: itemId, Word: word, ActionType: action, ReplaceWord: replace },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            _self.parent().prev().show();
                            _self.parent().hide();
                            var actioname = "禁用关键词";
                            if (action == 1) {
                                actioname = "审核关键词";
                            }
                            if (action == 2) {
                                actioname = "替换关键词";
                                $("#lblRepalce_" + itemId).show();
                            }
                            _self.parent().prev().attr("Action", action);
                            $("#lblAction_" + itemId).text(actioname);
                            $("#lblRepalce_" + itemId).text(replace);
                            $("#lblWord_" + itemId).text(word);

                            $("#lblAction_" + itemId).show();
                      
                            $("#lblWord_" + itemId).show();

                            $("#txtWord_" + itemId).hide();
                            $("#ddlAction_" + itemId).hide();
                            $("#txtRepalce_" + itemId).hide();
                        }
                        else {
                            alert("系统忙请稍后再试！");
                        }
                    }
                });

            });

        });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
         
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
                        <asp:Literal ID="Literal1" runat="server" Text="网站敏感词管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="网站敏感词" />，避免网站出现垃圾信息。
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <asp:HiddenField ID="txtLookupListId" runat="server" />
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="过滤词" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="过滤方式" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlSelectType" runat="server">
                                    <asp:ListItem Value="0">禁用关键词</asp:ListItem>
                                    <asp:ListItem Value="1">审核关键词</asp:ListItem>
                                    <asp:ListItem Value="2">替换关键词</asp:ListItem>
                                </asp:DropDownList>
                                  <span style="display: none"><asp:TextBox ID="txtAddReplace"  runat="server" Width="250px" MaxLength="20"></asp:TextBox></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br/>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat; width: 80px;" id="liAdd"
                        runat="server"><a href="add.aspx">批量添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 80px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM();">批量删除</a><b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCancelingEdit="gridView_RowCancelingEdit" OnRowEditing="gridView_RowEditing"
            OnRowUpdating="gridView_RowUpdating" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="FilterId">
            <Columns>
                    <asp:TemplateField ControlStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderText="过滤词">
                    <ItemTemplate>
                        <span id='lblWord_<%# (Eval("FilterId"))%>'>
                            <%#Eval("WordPattern")%></span>
                            <input id='txtWord_<%# (Eval("FilterId"))%>' style="display: none"  type="text" value='<%# (Eval("WordPattern"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" ItemStyle-HorizontalAlign="Center" HeaderText="过滤动作">
                    <ItemTemplate>
                        <span id='lblAction_<%# (Eval("FilterId"))%>'>
                            <%# GetActionText(Eval("ActionType"))%></span>
                             <span id='ddlAction_<%# (Eval("FilterId"))%>' style="display: none">
                                <select id="ddl_<%# (Eval("FilterId"))%>" class="ddlAction" FilterId='<%# (Eval("FilterId"))%>'>
                                    <option value="0">禁用关键词</option>
                                    <option value="1" >审核关键词</option>
                                    <option value="2">替换关键词</option>
                                </select>
                            </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderText="替换字符">
                    <ItemTemplate>
                        <span id='lblRepalce_<%# (Eval("FilterId"))%>'>
                            <%#  Maticsoft.Common.Globals.SafeInt(Eval("ActionType").ToString(),0)==2?Eval("RepalceWord").ToString():""%></span>
                            <input id='txtRepalce_<%# (Eval("FilterId"))%>' style="display: none"  type="text" value='<%# (Eval("RepalceWord"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Site, lblOperation %>" ShowHeader="False"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <span id="btnModify" runat="server" class="btnEdit"  FilterId='<%# (Eval("FilterId"))%>' Action='<%# (Eval("ActionType"))%>'><a href="javascript:void(0)">编辑</a></span> <span style="display: none"  FilterId='<%# (Eval("FilterId"))%>'>
                            <a href="javascript:void(0)" class="btnUpdate">更新</a> <a href="javascript:void(0)"
                                class="btnCancle">取消</a></span>
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText  %>"    OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td width="80">
                    <asp:DropDownList ID="ddSelect" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">设置禁止关键词</asp:ListItem>
                        <asp:ListItem Value="1">设置审核关键词</asp:ListItem>
                        <asp:ListItem Value="2">设置替换关键词</asp:ListItem>
                    </asp:DropDownList>
                </td>
                  <td width="80" style="display: none">
                <asp:TextBox ID="txtReplace" runat="server"></asp:TextBox>
                </td>
                
                <td width="80">
                    <asp:Button ID="btnSet" runat="server" Text="批量设置" class="adminsubmit" OnClick="btnSet_Click" />
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                        ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
