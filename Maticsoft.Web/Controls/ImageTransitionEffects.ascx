<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageTransitionEffects.ascx.cs" Inherits="Maticsoft.Web.Controls.ImageTransitionEffects" %>
<%--<script src="/Scripts/MSClass.js" type="text/javascript"></script>--%>
<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script src="/Scripts/MSClass.js" type="text/javascript"></script>
<!--文字显示-->
<asp:Repeater ID="rp_TextShow" runat="server">
    <HeaderTemplate>
        <script src="/Scripts/jquery.marquee.js" type="text/javascript"></script>
        <link href="/Scripts/jquery.marquee.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            ul.marquee {display: block;line-height: 1;position: relative;overflow: hidden;width: <%=strWidth %>px;  height: <%=strHeight %>px; }  
            ul.marquee li {position: absolute; top: -999em;left: 0;display: block;  white-space: nowrap; padding: 3px 5px; text-indent:0.8em;}
        </style>
        <ul id="marquee" class="marquee">      
    </HeaderTemplate>
    <ItemTemplate>  
             <li><a href="<%#Eval("NavigateUrl") %>" target="_blank">  <%#Eval("AlternateText")%></a> </li>  
    </ItemTemplate>
    <FooterTemplate>
        </ul>
        <script type="text/javascript">
            $(function () {
                $("#marquee").marquee({ yScroll: "left", fxEasingShow: "swing",
                    fxEasingScroll: "linear"
                });
            });  
        </script>
    </FooterTemplate>
</asp:Repeater>
<!--图片显示-->
<asp:Repeater ID="rpTransitionEffects" runat="server">
    <HeaderTemplate>
        <style>
            a,body,select,td,b{font-size:12px;text-decoration:none;}
            body{background:#ffffff;}
            a,pre{color:#808080;}
            a:link,a:visited {color:#555;}
            a:hover,a:active { color:#ff4e00;}
            img{border:0}
            </style>
            <style type="text/css">
            #JINGDONGNumID{ position:absolute; bottom:5px; right:5px;}
            #JINGDONGNumID li{list-style:none;float:left;width:18px;height:16px;FILTER:alpha(opacity=80);opacity:0.8;border:1px solid #D00000;background-color:#FFFFFF;color:#D00000;text-align:center;cursor:pointer;margin-right:4px;padding-top:2px;overflow:hidden;}
            #JINGDONGNumID li:hover,#JINGDONGNumID li.active{border:1px solid #D00000;background-color:#FF0000;color:#FFFFFF;width:22px;height:18px;font-weight:bold;font-size:13px;}
    </style>
    <div style="overflow:hidden;width:<%=strWidth %>px;height:<%=strHeight %>px;position:relative;clear:both;border:1px solid #000000;background-color:#ffffff;border:none;" id="divGroup" Direction='<%=Direction %>'>
        <div id="JINGDONGBox">
            <ul id="JINGDONGContentID">
        </HeaderTemplate>
    <ItemTemplate>
        <li><a target="_blank" href="<%#Eval("NavigateUrl") %>">
           <img border="0" src='<%#Eval("FileUrl") %>' width='<%#Eval("Width") %>' height='<%#Eval("Height") %>' /></a></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </div>
        <ul id="JINGDONGNumID" >
            <li class="">1</li>
            <li class="">2</li>
            <li class="">3</li>
            <li class="">4</li>
            <li class="">5</li>
            <li class="">6</li>
            <li class="">7</li>
            <li class="">8</li>
        </ul>
        </div>

<script type="text/javascript">
    var height = $("#divGroup").height();
    var width = $("#divGroup").width();
    var direction = parseInt($("#divGroup").attr("Direction"));
    new Marquee(
{
    MSClassID: "JINGDONGBox",
    ContentID: "JINGDONGContentID",
    TabID: "JINGDONGNumID",
    Direction: direction,
    Step: 0.3,
    Width: width,
    Height: height,
    Timer: 30,
    DelayTime: 9000,
    WaitTime: 0,
    ScrollStep: height,
    SwitchType: 2,
    AutoStart: 1
})

</script>
    </FooterTemplate>
</asp:Repeater>
<!--Flash显示-->
<asp:Repeater ID="rp_FlashShow" runat="server">
    <HeaderTemplate>
        <style>
            a,body,select,td,b{font-size:12px;text-decoration:none;}
            body{background:#ffffff;}
            a,pre{color:#808080;}
            a:link,a:visited {color:#555;}
            a:hover,a:active { color:#ff4e00;}
            img{border:0}
            </style>
            <style type="text/css">
            #JINGDONGNumID{ position:absolute; bottom:5px; right:5px;}
            #JINGDONGNumID li{list-style:none;float:left;width:18px;height:16px;FILTER:alpha(opacity=80);opacity:0.8;border:1px solid #D00000;background-color:#FFFFFF;color:#D00000;text-align:center;cursor:pointer;margin-right:4px;padding-top:2px;overflow:hidden;}
            #JINGDONGNumID li:hover,#JINGDONGNumID li.active{border:1px solid #D00000;background-color:#FF0000;color:#FFFFFF;width:22px;height:18px;font-weight:bold;font-size:13px;}
    </style>
    <div style="overflow:hidden;width:<%=strWidth %>px;height:<%=strHeight %>px;position:relative;clear:both;border:1px solid #000000;background-color:#ffffff;border:none;" id="divGroup" Direction='<%=Direction %>'>
        <div id="JINGDONGBox">
            <ul id="JINGDONGContentID">
        </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a target="_blank" href="<%#Eval("NavigateUrl") %>">
               <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                    width="<%#Eval("Width") %>" height="<%#Eval("Height") %>">
                    <param name="wmode" value="opaque" />
                    <param name="movie" value="<%#Eval("FileUrl") %>" />
                    <param name="quality" value="high" />
                    <embed src="<%#Eval("FileUrl") %>" allowfullscreen="true" quality="high" width="<%#Eval("Width") %>" height="<%#Eval("Height") %>" align="middle" wmode="transparent" allowscriptaccess="always" type="application/x-shockwave-flash"></embed></object>
           </a></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </div>
        <ul id="JINGDONGNumID" style="z-index:100" >
            <li class="">1</li>
            <li class="">2</li>
            <li class="">3</li>
            <li class="">4</li>
            <li class="">5</li>
            <li class="">6</li>
            <li class="">7</li>
            <li class="">8</li>
        </ul>
        </div>

<script type="text/javascript">
    var height = $("#divGroup").height();
    var width = $("#divGroup").width();
    var direction = parseInt($("#divGroup").attr("Direction"));
    new Marquee(
{
    MSClassID: "JINGDONGBox",
    ContentID: "JINGDONGContentID",
    TabID: "JINGDONGNumID",
    Direction: direction,
    Step: 0.9,
    Width: width,
    Height: height,
    Timer: 90,
    DelayTime: 9000,
    WaitTime: 0,
    ScrollStep: height,
    SwitchType: 2,
    AutoStart: 1
})

</script>
    </FooterTemplate>
</asp:Repeater>

<!--自定义HTML代码显示-->
<asp:Repeater ID="rp_HtmlCode" runat="server">
    <HeaderTemplate>
        <div style="overflow: hidden; width: <%=strWidth %>px; height: <%=strHeight %>px;
            position: relative; clear: both; border: 1px solid #000000; background-color: #ffffff;
            border: none;" id="divGroup" direction='<%=Direction %>'>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <%#Eval("AdvHtml")%><a target="_blank" href="<%#Eval("NavigateUrl") %>"></a></li>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
