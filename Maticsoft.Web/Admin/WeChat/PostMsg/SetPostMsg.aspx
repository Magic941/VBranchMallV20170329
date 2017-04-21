<%@ Page Title="设置回复内容" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SetPostMsg.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.PostMsg.SetPostMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#contentMsg").load("PostMsgList.aspx", { id: <%=RuleId %> },
            function () {
            });
          $(".iframe").colorbox({ iframe: true, width: "480", height: "420", overlayClose: false });
        });
        function LoadData() {
             $("#cboxClose").click();
      $("#contentMsg").load("PostMsgList.aspx", { id: <%=RuleId %> },
            function () {
               
            });
        }
    </script>
    <input id="txtResetLoad" type="button" value="button" onclick="LoadData()" style="display: none" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="规则的关键字和回复内容管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以设置规则的关键字和回复内容" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Add end -->
        <br />
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td height="25" width="40%">
                  <asp:TextBox ID="txtValue" TabIndex="1" runat="server" Width="250px" MaxLength="30"></asp:TextBox> 
                    <asp:Button ID="btnAddValue" runat="server" Text="添加关键字"  class="adminsubmit" OnClick="btnAddValue_Click"/>
                </td>
                <td >
                       <asp:TextBox ID="txtPostMsg" TabIndex="1" runat="server" Width="250px" MaxLength="30"></asp:TextBox>    
                       <asp:Button ID="btnAddMsg" runat="server" Text="添加回复"  class="adminsubmit" OnClick="btnAddMsg_Click"/>
                </td>
            </tr>
            </table>
            <br/>
       
        <div id="contentMsg">
        </div>
    </div>
</asp:Content>

