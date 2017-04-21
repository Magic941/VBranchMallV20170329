<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyGuestBook.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Guestbook.ReplyGuestBook" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/MasterPage1.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/admin.css" type="text/css" rel="stylesheet" charset="utf-8" />
  
</head>
<body>
    <form id="form1" runat="server">
          <div class="newslistabout">
    <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       <asp:Literal ID="Literal1" runat="server" Text="留言回复" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="对留言进行邮件回复" />
                    </td>
                </tr>
            </table>
 

                 <table  width="100%" cellpadding="2" cellspacing="1" class="border borderkuang" style=" margin-top:20px"> 
                  <tr>
                      <td class="tdbg">
                          <table cellspacing="0" width="100%" cellpadding="0" border="0"  style=" margin-top:20px;margin-bottom: 20px;">
                              
                               <tr>
                                  <td   style="width:60px">
                                      回复内容：
                                  </td>
                                  <td >
                                      <asp:TextBox ID="TxtReply" runat="server" Height="116px" TextMode="MultiLine" 
                                          Width="379px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填字段"  ControlToValidate="TxtReply"></asp:RequiredFieldValidator>


                                            
                                  </td>
                              </tr>

                          </table>
                      </td>
                  </tr>
              </table>
                 
              
             


              <table  width="100%" cellpadding="2" cellspacing="1" class="border" style=" margin-top:20px"> 
                  <tr>
                      <td  class="tdbg">
                          <table cellspacing="0" width="100%" cellpadding="0" border="0" >
                              <tr >
                                  <td class="td_class" height="30">
                                  </td>
                                  <td height="25">
                                      <asp:Button ID="btnSend" runat="server" Text="回复" class="adminsubmit_short" OnClick="btnSend_Click"
                                           />
                                          
                                          <input type="button" class="adminsubmit_short" value="关闭" onclick="javascript:parent.$.colorbox.close();"/>
                                      <asp:HiddenField ID="hidValue" runat="server" />
                                      &nbsp;&nbsp;<label visible="false" id="lblTip" style="color: #38BE2D" runat="server">回复成功</label>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
              </table>

   </div>


 
    </form>
</body>
</html>
