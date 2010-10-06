<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="StateMagic.Web.UserDetails" MasterPageFile="~/Master.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID=ContentPlaceHolderMenu>
<ul id="dropmenu">
    <li class="page_item page-item-2"><a href="SignOut.aspx" title="Sign Out">Sign Out</a></li>
</ul>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<h3>Hi, <asp:Label runat="server" ID="namelabel"></asp:Label></h3>
<br />
<div  style="width:50%; display:inline; float:left">
    <h3>Your Account</h3>
    Your account balance is <strong><asp:Label runat="server" ID="AccountBalanceLabel"></asp:Label></strong> transactions remaining.
    <br />
    You have <strong><asp:Label runat="server" ID="stateDiagramCountLabel"></asp:Label></strong> state diagrams.
    <br />
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
        <strong><%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).ModelDataID%></strong> - <%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).ModelName%></a>
        <br />[<%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).DeserializedStateModel.States.Count%> states, <%#(Container.DataItem as StateMagic.DatabaseTypes.ModelData).DeserializedStateModel.Transitions.Count%> transitions]

    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
    <br />
    <br />
    

    </div>
    <h3>Purchase Credit</h3>
    <div style="width:50%; display:inline; float:left">
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
    
    <br /><br />
    <h3>Development</h3>
    Your developer API Key is<br />
    <asp:TextBox runat="server" ID="APIKey1" ReadOnly="true" Width="250" /><br />
    <br />
    The WSDL for the web service: <a href="http://statemagic.com/WebServices.asmx?wsdl">http://statemagic.com/WebServices.asmx?wsdl</a>
    <br /><br />
    Example code calling the web service client:
    <br />
<div style="border:1px solid #67AAF4; padding:5px; -moz-border-radius:5px; border-radius:5px;">
<div>
<pre class="csharpcode">
<span class="kwrd">var</span> client = <span class="kwrd">new</span> StateMagic.WebServicesSoapClient();

<span class="kwrd">var</span> username = <span class="str">"<asp:Label runat="server" ID="usernameSnippet"/>"</span>;
<span class="kwrd">var</span> apiKey = <span class="kwrd">new</span> Guid(<span class="str">"<asp:Label runat="server" ID="apiKeySnippet"/>"</span>);
<span class="kwrd">var</span> diagramId = <asp:Label runat="server" ID="modelIdSnippet" />; <span class="rem">// the id of the diagram you wish to use</span>
<span class="kwrd">string</span> currentState = <span class="kwrd">null</span>;
            
<span class="kwrd">var</span> states = client.GetNextState(username, 
                                apiKey, 
                                diagramId, 
                                currentState);

<span class="rem">// the set of next possible states</span>
<span class="kwrd">foreach</span> (<span class="kwrd">foreach</span> state <span class="kwrd">in</span> states)
{
    Console.WriteLine(state);
}</pre>
</div>
    </div>



</asp:Content>