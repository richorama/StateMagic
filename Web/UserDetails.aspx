<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="StateMagic.Web.UserDetails" MasterPageFile="~/Master.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID=ContentPlaceHolderMenu>
<ul id="dropmenu">
    <li class="page_item page-item-2"><a href="SignOut.aspx" title="Sign Out">Sign Out</a></li>
</ul>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<h3>Hi, <asp:Label runat="server" ID="namelabel"></asp:Label></h3>
<div  style="width:65%; display:inline; float:left">
    
    Your account balance is <strong><asp:Label runat="server" ID="AccountBalanceLabel"></asp:Label></strong> transactions remaining.
    <br />
    You have <strong><asp:Label runat="server" ID="stateDiagramCountLabel"></asp:Label></strong> state diagrams.
    <br />
    <a href="Designer.aspx">&nbsp;<img src="images/application_go.png"/> Create a new diagram</a>
    <br />
    <br />

    <h3>Your diagrams</h3>
    <ul>
    <asp:Repeater runat="server" ID="AvailableModels">
    <ItemTemplate>
    <li class="leaf">
        <a href="Designer.aspx?ModelId=<%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).ModelDataID%>">
        <%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).ModelName%></a>
        <br />[<%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).DeserializedStateModel.States.Count%> states, <%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).DeserializedStateModel.Transitions.Count%> transitions]

    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
    <br />
    <br />
    

    </div>

    <div style="width:30%; display:inline">
        To purchase more transactions, press the 'Buy Now' button:<br />
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
    
    </div>

</asp:Content>