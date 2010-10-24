<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rest.aspx.cs" Inherits="StateMagic.Web.Rest" %>
<?xml version="1.0"?><asp:Repeater runat="server" ID="repeater">
<HeaderTemplate><states>
</HeaderTemplate>
<ItemTemplate>  <state href="http://statemagic.com/Rest.aspx?state=<%#(Container.DataItem as string)%>&username=<%= Server.UrlEncode(this.Credentials.Username) %>&apikey=<%= this.Credentials.ApiKey %>&modelid=<%= this.ModelId%>"><%#(Container.DataItem as string)%></state>
</ItemTemplate>
<FooterTemplate></states>
</FooterTemplate>
</asp:Repeater>
