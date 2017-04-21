<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDroplistPermission.ascx.cs" Inherits="Maticsoft.Web.Controls.UCDroplistPermission" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
        <link media="screen" rel="stylesheet" href="/admin/js/colorbox/example1/colorbox.css" />
        <script src="/admin/js/colorbox/jquery.min.js"></script>
        <script src="/admin/js/colorbox/jquery.colorbox.js"></script>
       <script type="text/javascript">
            $(document).ready(function () {
                $(".example8").colorbox({ width: "500px", height: "280px", inline: true, href: "#inline_example1" });

                $("#cboxOverlay").appendTo('form');
                $("#colorbox").appendTo('form');
            });
        
            function save() {                
                var item = $("input[name$='radbtnlistPermission']:checked").val();                                
                //var item = $("table[id^=ctl] input[type=radio]:checked").val();
                //alert(item);
                $("[id$='HiddenFieldPermID']").val(item);
                $("#inline_example1").colorbox.close();
            }
            function closed() {
                $("#inline_example1").colorbox.close();
            }
        </script>
         
        <div style='display: none'>                        
            <div id='inline_example1' style='padding: 10px; background: #fff; text-align:left'>               
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <b>权限类别：</b><asp:DropDownList ID="droplistPermCategories" runat="server" AutoPostBack="true" >
                </asp:DropDownList>                               
                <br /><b>权限列表：</b><br />
                <asp:RadioButtonList ID="radbtnlistPermission" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" >
                </asp:RadioButtonList>
                </ContentTemplate>            
            </asp:UpdatePanel> 
            
            <br/>   
                <input type="submit" class="adminsubmit_short" value="确定" onclick="save()" />
                <input type="submit" class="adminsubmit_short" value="关闭" onclick="closed()" />
            </div>
        </div>
<asp:Label ID="lblPermName" runat="server" Text=""></asp:Label>&nbsp;<a class="example8" style=" color:Blue" href="#">[设置数据权限]</a>
                            <asp:HiddenField ID="HiddenFieldPermID" runat="server" />