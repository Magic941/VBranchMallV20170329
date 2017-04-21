<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HaolinCardEdit.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UserCard.HaolinCardEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function closefrm() {
            alert('保存成功');
            parent.closeWindown();
        }
    </script>
    <style type="text/css">
        h5{margin:0px;padding:0px;}
        #CarNo7 {
            width: 319px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="600" border="0">
        <tr>
          <td width="40">&nbsp;</td>
          <td width="160" height="25" align="right"><h5>健康卡号：</h5></td>
          <td>
              <asp:TextBox ID="txtCardNo" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
          <td width="30">&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>用户编号：</h5></td>
          <td>
              <asp:TextBox ID="txtUsreCode" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>姓名：</h5></td>
          <td>
              <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>邮箱：</h5></td>
          <td>
              <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>手机：</h5></td>
          <td>
              <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>性别：</h5></td>
          <td>
              <asp:DropDownList ID="dlSex" runat="server" Width="60px">
                  <asp:ListItem Value="男">男</asp:ListItem>
                  <asp:ListItem Value="女">女</asp:ListItem>
              </asp:DropDownList>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>身份证号：</h5></td>
          <td>
              <asp:TextBox ID="txtCardId" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>

          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>第一投保人：</h5></td>
          <td>
              <asp:TextBox ID="txtInsureOneName" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>第一投保人身份证：</h5></td>
          <td>
              <asp:TextBox ID="txtInsureOneID" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>第一投保人关系：</h5></td>
          <td>
              <asp:TextBox ID="txtInsureOneRelationship" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>

          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>第二投保人：</h5></td>
          <td>
              <asp:TextBox ID="txtInsureTwoName" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>第一投保人身份证：</h5></td>
          <td>
              <asp:TextBox ID="txtInsureTwoID" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
          <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>第一投保人关系：</h5></td>
          <td>
              <asp:TextBox ID="txtInsureTwoRelationship" runat="server"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>

        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>激活时间：</h5></td>
          <td>
              <asp:TextBox ID="txtActiveDate" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right"><h5>地址：</h5></td>
          <td>
              <asp:TextBox ID="txtAddress" runat="server" Width="348px"></asp:TextBox>
            </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td height="25" align="right">&nbsp;</td>
          <td>
              <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
            </td>
          <td>&nbsp;</td>
        </tr>
      </table>
    
    </div>
    </form>
</body>
</html>
