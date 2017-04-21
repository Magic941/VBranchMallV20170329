<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.ToDoInfo.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <!--Select2 Start-->
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='dropToUserID']").select2({ placeholder: "请选择" });
            $("#selectPeop").hide();
            if ($("[id$='dropToType']").val() == "3") {
                $("#selectPeop").show();
            }
            $("[id$='dropToType']").change(function () {
                if ($("[id$='dropToType']").val() == "3") {
                    $("#selectPeop").show();
                }
                else {
                    $("#selectPeop").hide();
                }
            });
            $("[id$='dropToUserID']").change(function () {
                $("[id$='hdToUserID']").val($("[id$='dropToUserID']").val());
            });
        });          
        
    </script>
    <!--Select2 End-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="新增待办" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以进行新增待办操作" />
                    </td>
                </tr>
            </table>
    </div>  
    <br />
     <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="标题" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtTitle" TabIndex="2" runat="server" Width="400" MaxLength="20"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="内容" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtContent" TabIndex="3" runat="server" Width="395" Height="250" TextMode="MultiLine"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="发送类型" />：
                            </td>
                            <td class="25">
                                <asp:DropDownList ID="dropToType" runat="server" class="dropSelect" TabIndex="6" Width="180">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="selectPeop">
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="发送对象" />：
                            </td>
                            <td class="25">
                                <asp:DropDownList ID="dropToUserID" multiple="true" runat="server" 
                                    TabIndex="5">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdToUserID" runat="server" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="状态" />：
                            </td>
                            <td class="25">
                                <asp:DropDownList ID="dropStatus" runat="server" class="dropSelect" TabIndex="7" Width="180">
                                    <asp:ListItem Text="未办" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="已办" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="未通过" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="已通过" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">
                                附件：
                            </td>
                            <td class="25">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:FileUpload ID="FileUpload2" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload3" runat="server" />
                                <asp:FileUpload ID="FileUpload4" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload5" runat="server" />
                                <asp:FileUpload ID="FileUpload6" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload7" runat="server" />
                                <asp:FileUpload ID="FileUpload8" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload9" runat="server" />
                                <asp:FileUpload ID="FileUpload10" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short" TabIndex="8"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short" TabIndex="9"></asp:Button>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>   
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
