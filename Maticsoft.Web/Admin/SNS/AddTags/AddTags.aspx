<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTags.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.AddTags.AddTags" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/MasterPage1.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <link href="/admin/css/admin.css" type="text/css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript">
        $(function () {
            $("#Tags .SKUValue").die("click").live("click", function () {

                $(this).remove().appendTo("#TagSelect").find(".del").show();
                var tags = "";
                $("#TagSelect").find(".span1 a").each(function () { tags = tags + "," + $(this).text() })
                $("#hidValue").val(tags);
            });
            $(".del").die("click").live("click", function () {
                $(this).hide().parents(".SKUValue").remove().appendTo("#Tags");
                var tags = "";
                $("#TagSelect").find(".span1 a").each(function () { tags = tags + "," + $(this).text(); });
                $("#hidValue").val(tags);
            });
         
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <div class="newslistabout">
    <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       <asp:Literal ID="Literal1" runat="server" Text="标签选择" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="添加标签" />
                    </td>
                </tr>
            </table>
 
      <%--        <table  width="100%" cellpadding="2" cellspacing="1" class="border">
                  <tr>
                      <td class="tdbg">
                          <table cellspacing="0" width="100%" cellpadding="0" border="0">
                             
                          </table>
                      </td>
                  </tr>
              </table>--%>
                 <table  width="100%" cellpadding="2" cellspacing="1" class="border borderkuang" style=" margin-top:20px"> 
                  <tr>
                      <td class="tdbg">
                          <table cellspacing="0" width="100%" cellpadding="0" border="0"  style=" margin-top:20px;margin-bottom: 20px;">
                              
                               <tr>
                                  <td   style="width:60px">
                                      已选：
                                  </td>
                                  <td id="TagSelect" >
                                              <%=strSelectValue.ToString()%>
                                            
                                  </td>
                              </tr>

                          </table>
                      </td>
                  </tr>
              </table>
                <table  width="100%" cellpadding="2" cellspacing="1" class="border borderkuang" style=" margin-top:20px"> 
                  <tr>
                      <td class="tdbg">
                          <table cellspacing="0" width="100%" cellpadding="0" border="0"  style=" margin-top:20px;margin-bottom: 20px;">
                              <tr>
                                  <td class="td_class" height="30">
                                      未选 ：
                                  </td>
                                  <td id="Tags"  style="width:1200px">
                                    
                                          <%=strTagsValue.ToString()%>
                                  </td>
                              </tr>
                               
                          </table>
                      </td>
                  </tr>
              </table> 
              
               <table  width="100%" cellpadding="2" cellspacing="1" class="border borderkuang" style=" margin-top:20px"> 
                  <tr>
                      <td class="tdbg">
                          <table cellspacing="0" width="100%" cellpadding="0" border="0"  style=" margin-top:20px;margin-bottom: 20px;">
                              <tr>
                                  <td class="td_class" height="30">
                                      添加 ：
                                  </td>
                                  <td id="AddTags"  style="width:1200px">
                                      <asp:TextBox ID="InputTags" runat="server"></asp:TextBox>
                                     <asp:Button ID="btnAddTags" runat="server" Text="添加标签" class="adminsubmit_short" OnClick="btnSave_Click"
                                         />
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
                                      <asp:Button ID="btnSave" runat="server" Text="保存" class="adminsubmit_short" OnClick="btnSave_Click"
                                          OnClientClick="javascript:parent.$.colorbox.close();" />
                                      <asp:HiddenField ID="hidValue" runat="server" />
                                      &nbsp;&nbsp;<label visible="false" id="lblTip" style="color: #38BE2D" runat="server">保存成功</label>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
              </table>

   </div>

    <%--<div class="GridViewTyle" style="margin: 0 auto; width: 360px; ">
        <div>
 <%--           <span style="float: left; font-size: 15px; color: #666">选择标签:</span>
            <div id="TagSelect" style="width: 330px; height: auto; float: left">
                <%=strSelectValue.ToString()%></div>--%>
        </div>
  <%--      <div id="Tags" style="width: 360px; margin-top: 10px; float: left;">
            <%=strTagsValue.ToString()%></div>--%>
     <%--   <div style="width: 330px; margin-top: 40px; float: left">
         
        </div>
    </div>--%>
 
    </form>
</body>
</html>
