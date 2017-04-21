<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.ToDoInfo.Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            if ($("#a_0").length > 0) {
                $("[id$='FileUpload1']").hide();
            } else {
                $("[id$='FileUpload1']").show();
            }
            if ($("#a_1").length > 0) {
                $("[id$='FileUpload2']").hide();
            } else {
                $("[id$='FileUpload2']").show();
            }
            if ($("#a_2").length > 0) {
                $("[id$='FileUpload3']").hide();
            } else {
                $("[id$='FileUpload3']").show();
            }
            if ($("#a_3").length > 0) {
                $("[id$='FileUpload4']").hide();
            } else {
                $("[id$='FileUpload4']").show();
            }
            if ($("#a_4").length > 0) {
                $("[id$='FileUpload5']").hide();
            } else {
                $("[id$='FileUpload5']").show();
            }
            if ($("#a_5").length > 0) {
                $("[id$='FileUpload6']").hide();
            } else {
                $("[id$='FileUpload6']").show();
            }
            if ($("#a_6").length > 0) {
                $("[id$='FileUpload7']").hide();
            } else {
                $("[id$='FileUpload7']").show();
            }
            if ($("#a_7").length > 0) {
                $("[id$='FileUpload8']").hide();
            } else {
                $("[id$='FileUpload8']").show();
            }
            if ($("#a_8").length > 0) {
                $("[id$='FileUpload9']").hide();
            } else {
                $("[id$='FileUpload9']").show();
            }
            if ($("#a_9").length > 0) {
                $("[id$='FileUpload10']").hide();
            } else {
                $("[id$='FileUpload10']").show();
            }

            $(".del_class").bind('click', function () {
                var index = $(this).attr('i');
                var fiName = $(this).attr('n');
                $(this).hide();
                $("#a_" + index).hide();
                $("[id$='FileUpload" + (parseInt(index) + 1) + "']").show();

                var filenames = $("[id$='HiddenField_Old']").val();


                $("[id$='HiddenField_Old']").val(filenames.replace( fiName, ''));

               // alert($("[id$='HiddenField_Old']").val(filenames.replace('|' + fiName, '')));
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="编辑待办" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以进行编辑待办操作" />
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
                                <asp:Literal ID="Literal9" runat="server" Text="待办编号" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlToDoID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="执行人" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlUserName" runat="server" Text="执行人" />
                            </td>
                        </tr>
                        <asp:HiddenField ID="hdToUserId" runat="server" />
                        <asp:HiddenField ID="hdCreatedID" runat="server" />
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="标题" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtTitle" TabIndex="2" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="内容" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtContent" TabIndex="3" runat="server" Width="196px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:HiddenField ID="hdToType" runat="server" />
                        <asp:HiddenField ID="hdCreatedDate" runat="server" />
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="状态" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="dropStatus" runat="server" class="dropSelect" TabIndex="7">
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
                            <td height="25" id="tdAtt">
                                
                                <asp:Literal ID="litFile" runat="server"></asp:Literal>
                                <asp:HiddenField ID="HiddenField_Old" runat="server" />
                                <asp:HiddenField ID="HiddenField_New" runat="server" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                <asp:HiddenField ID="HiddenField7" runat="server" />
                                <asp:HiddenField ID="HiddenField8" runat="server" />
                                <asp:HiddenField ID="HiddenField9" runat="server" />
                                <asp:HiddenField ID="HiddenField10" runat="server" />

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