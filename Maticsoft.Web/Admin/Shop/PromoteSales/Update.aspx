
<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
  CodeBehind="Update.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.PromoteSales.Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
      <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id$='ddlProduct']").select2();
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);

            $("#ctl00_ContentPlaceHolder1_txtEndDate").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd"
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="限时抢购活动管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行编辑限时抢购商品活动操作" />
                    </td>
                </tr>
            </table>
        </div>

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                      
                        <tr >
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="商品名称"  />：
                            </td>
                            <td height="25">
                             <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="结束时间" />：
                            </td>
                            <td height="25">
                                 <asp:TextBox ID="txtEndDate" runat="server" Width="100" MaxLength="30" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="抢购价格" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPrice" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="" />
                            </td>
                            <td height="25">
                                  <asp:CheckBox ID="chkStatus" Text="上架" runat="server" Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="活动说明" />
                            </td>
                            <td height="25">
                                     <asp:TextBox ID="txtDesc" runat="server" Width="420px"  TextMode="MultiLine" Rows="3"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="确定"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>


