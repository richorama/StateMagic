<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="StateMagic.Web.UserDetails" MasterPageFile="~/Master.Master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID=ContentPlaceHolderMenu>
<ul id="dropmenu">
    <li class="page_item page-item-2"><a href="SignOut.aspx" title="Sign Out">Sign Out</a></li>
</ul>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<form id="form1" runat="server">
                     
<h3>Hi, <%= this.Credentials.Username%></h3>
<br/>
<hr />
<br />
<div  style="width:50%; display:inline; float:left">
    <h3>Your Account</h3>
    Your account balance is <strong><%= this.Credentials.TransactionBalance.ToString() %></strong> transactions remaining.
    <br /> <br />
    
    To purchase more transactions, press the 'Buy Now' button:<br />
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
	<input type="image" src="https://www.paypal.com/en_US/GB/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online." style="border: 0px solid white">
	<img alt="" border="0" src="https://www.paypal.com/en_GB/i/scr/pixel.gif" width="1" height="1">
    </form>
    <br />
    <br />
    Your developer API Key is <input readonly="readonly" value="<%= this.Credentials.ApiKey.ToString() %>" size="38"/>
    
    </div>
    <div  style="width:50%; display:inline; float:left">
    <h3>Your diagrams</h3>
    You have <strong><%=this.Credentials.Models.Count.ToString() %></strong> state diagrams.
    <br />
    <div style="overflow:auto; height:200px;border:1px solid #67AAF4; padding:5px; -moz-border-radius:5px; border-radius:5px;">
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
    </div>
    <br />
    <h3><a href="Designer.aspx">&nbsp;<img src="images/application_go.png"/> Create a new diagram</a></h3>
    <br />
    </div>
    <div style="display:block">
    <br/>&nbsp;
<hr />
<br />
    <h3>Web Service Interface (SOAP)</h3>
    The WSDL for the web service: <a href="http://statemagic.com/WebServices.asmx?wsdl">http://statemagic.com/WebServices.asmx?wsdl</a>
    <br /><br />
    Example code calling the web service client:
    <br />
<div style="border:1px solid #67AAF4; padding:5px; -moz-border-radius:5px; border-radius:5px;">
<pre class="csharpcode">
<span class="kwrd">var</span> client = <span class="kwrd">new</span> StateMagic.WebServicesSoapClient();

<span class="kwrd">var</span> username = <span class="str">"<%= this.Credentials.Username%>"</span>;
<span class="kwrd">var</span> apiKey = <span class="kwrd">new</span> Guid(<span class="str">"<%= this.Credentials.ApiKey%>"</span>);
<span class="kwrd">var</span> diagramId = <%= this.LastModelId.ToString() %>; <span class="rem">// the id of the diagram you wish to use</span>
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
  
    <br /><br />
    <h3>RESTful Interface</h3>
    For the REST interface, use this URL:<br />
    <br />
    <a href="http://statemagic.com/Rest.aspx?username=<%= Server.UrlEncode(this.Credentials.Username)%>&apikey=<%=this.Credentials.ApiKey.ToString() %>&modelid=1">http://statemagic.com/Rest.aspx?username=<%= Server.UrlEncode(this.Credentials.Username)%>&apikey=<%=this.Credentials.ApiKey.ToString() %>&modelid=<%= this.LastModelId.ToString() %></a><br /><br />
    Example output:<br />
    <div style="border:1px solid #67AAF4; padding:5px; -moz-border-radius:5px; border-radius:5px;">
    <pre class="csharpcode">
<span class="kwrd">&lt;</span><span class="html">states</span><span class="kwrd">&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">state</span> <span class="attr">href</span><span class="kwrd">="http://statemagic.com/Rest.aspx?state=State 1&amp;username=<%= Server.UrlEncode(this.Credentials.Username) %>&amp;apikey=<%= this.Credentials.ApiKey.ToString() %>&amp;modelid=<%= this.LastModelId.ToString() %>"</span><span class="kwrd">&gt;</span>
    State 1<span class="kwrd">&lt;/</span><span class="html">state</span><span class="kwrd">&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">state</span> <span class="attr">href</span><span class="kwrd">="http://statemagic.com/Rest.aspx?state=State 2&amp;username=<%= Server.UrlEncode(this.Credentials.Username) %>&amp;apikey=<%= this.Credentials.ApiKey.ToString() %>&amp;modelid=<%= this.LastModelId.ToString() %>"</span><span class="kwrd">&gt;</span>
    State 2<span class="kwrd">&lt;/</span><span class="html">state</span><span class="kwrd">&gt;</span> 
  <span class="kwrd">&lt;</span><span class="html">state</span> <span class="attr">href</span><span class="kwrd">="http://statemagic.com/Rest.aspx?state=State 3&amp;username=<%= Server.UrlEncode(this.Credentials.Username) %>&amp;apikey=<%= this.Credentials.ApiKey.ToString() %>&amp;modelid=<%= this.LastModelId.ToString() %>"</span><span class="kwrd">&gt;</span>
    State 3<span class="kwrd">&lt;/</span><span class="html">state</span><span class="kwrd">&gt;</span> 
<span class="kwrd">&lt;/</span><span class="html">states</span><span class="kwrd">&gt;</span> </pre>
</div>
</div>
</form>
</asp:Content>