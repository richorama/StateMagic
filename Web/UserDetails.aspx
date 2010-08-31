<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="StateMagic.Web.UserDetails" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <ul>
    <asp:Repeater runat="server" ID="AvailableModels">
    <ItemTemplate><a href="Designer.aspx?ModelId=<%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).ModelDataID%>"><%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).ModelName%></a></ItemTemplate>
    </asp:Repeater>
    <li><a href="Designer.aspx">Create a new diagram</a>
    </li>
    </ul>
    <br />
    Account Balance: <asp:Label runat="server" ID="AccountBalanceLabel"></asp:Label> transactions remaining.
    <br />
    <br />
    <form action="https://www.paypal.com/cgi-bin/webscr" method="post">
	    <input type="hidden" name="cmd" value="_xclick">
	    <input type="hidden" name="business" value="richard.astbury@gmail.com">
	    <input type="hidden" name="lc" value="GB">
	    <input type="hidden" name="item_name" value="10,000 State Requests">
	    <input type="hidden" name="item_number" value="10000">
	    <input type="hidden" name="amount" value="10.00">
	    <input type="hidden" name="currency_code" value="USD">
	    <input type="hidden" name="button_subtype" value="services">
	    <input type="hidden" name="no_note" value="0">
	    <input type="hidden" name="bn" value="PP-BuyNowBF:btn_buynowCC_LG.gif:NonHostedGuest">
	    <input type="image" src="https://www.paypal.com/en_US/GB/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online.">
	    <img alt="" border="0" src="https://www.paypal.com/en_GB/i/scr/pixel.gif" width="1" height="1">
    </form>
</asp:Content>