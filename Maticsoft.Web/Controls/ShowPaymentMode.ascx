<%@ Control Language="C#" EnableViewState="True" AutoEventWireup="true" CodeBehind="ShowPaymentMode.ascx.cs" Inherits="Maticsoft.Web.Controls.ShowPaymentMode" %>
<asp:repeater id="rptPaymentMode" runat="server">
    <headertemplate>
        <ul <%--style="padding-left: 100px;"--%> > 
    </headertemplate>
    <itemtemplate>
        <li>
            <input name="PaymentMode" type="radio" value='<%# Eval("modeId") %>'  style="vertical-align: middle; cursor:pointer"/>
            <img name="PaymentModeLogo" style=" text-align: center; vertical-align: middle;cursor:pointer" src="<%# Eval("logo") %>" alt="<%# Eval("name") %>" width="125" height="45" />
        </li>
    </itemtemplate>
    <footertemplate>
        <li <%= ShowBalanceMode ? "style='text-align:center;vertical-align: middle; width: 300px;'" : "style='display:none;text-align:center;vertical-align:middle; width: 300px;'" %>>
            <div style="float: left;">
            <input name="PaymentMode" type="radio" value='0' <%= IsEnableBalance?"":"disabled='disabled'" %>   style="vertical-align: middle; cursor:pointer"/>
            <img name="PaymentModeLogo" style=" text-align: center; vertical-align: middle;cursor:pointer" src="" alt="余额支付" width="125" height="45" />
            </div>
            <div style="float: left;margin-left: 5px; color: red; margin-top: 12px;">余额：<%= Balance %>元</div>
        </li>
        </ul>
    </footertemplate>
</asp:repeater>
<asp:HiddenField runat="server" ID="hfShowPaymentModeSelect"/>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        //默认选中第一个
        $('[name=PaymentMode]').first().attr('checked', 'checked');

        //设置图片关联选中
        $('[name=PaymentModeLogo]').bind('click', function () {
            $('[id$=hfShowPaymentModeSelect]').val($(this).prevAll('input[type=radio]').val());
            if ($(this).prevAll('input[type=radio]').attr('disabled') != "disabled") {
                $(this).prevAll('input[type=radio]').attr('checked', 'checked');
            }
        });

        //Set Value
        if ($('[id$=hfShowPaymentModeSelect]').val() != '') {
            $('[name=PaymentMode]').find('[value=' + $('[id$=hfShowPaymentModeSelect]').val() + ']').attr('checked', 'checked');
        }
        $('[name=PaymentMode]').bind('click', function () {
            $('[id$=hfShowPaymentModeSelect]').val($(this).val());
        });
    });
</script>
