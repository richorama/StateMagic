<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="StateMagic.Web.SignIn"
    MasterPageFile="~/Master.Master" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content runat="server" ContentPlaceHolderID="head">



</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID=ContentPlaceHolderMenu>
<ul id="dropmenu">
   
</ul>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <center>
        <div style="color: red">
            <asp:Label runat="server" ID="ErrorMessages"></asp:Label></div>
        <table cellspacing="25">
            <tr>
                <td>
                    <h3>Open a new account</h3>
                </td>
                <td>
                    <h3>Sign in</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="borderRadius" style="background-color: #840300; color: white; padding: 10px; text-align: left;">
                        <table cellspacing="5">
                            <tr>
                                <td align="right">
                                    Your Email Address:
                                </td>
                                <td>
                                    <asp:TextBox Width="180" runat="server" ID="EmailAddress"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Enter a Password:
                                </td>
                                <td>
                                    <asp:TextBox Width="180" runat="server" ID="Password1" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Retype your Password:
                                </td>
                                <td>
                                    <asp:TextBox Width="180" runat="server" ID="Password2" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <recaptcha:RecaptchaControl ID="recaptcha" runat="server" PublicKey="6LfkzLoSAAAAAItUwSvQMmU3ruNJgYSMG6kt_7Bn"
                                        PrivateKey="6LfkzLoSAAAAAM7-2c1nxX06xa8S8Q463p6kz4fJ" Theme="red" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button runat="server" ID="RegisterButton" Text="Open a New Account" OnClick="RegisterButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left">
                    <div class="borderRadius" style="background-color: #2688C4; color: white; padding: 10px; text-align: left;">
                        <table cellspacing="5">
                            <tr>
                                <td align="right">
                                    Email Address:
                                </td>
                                <td>
                                    <asp:TextBox Width="180" runat="server" ID="SignInEmailAddress"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Password:
                                </td>
                                <td>
                                    <asp:TextBox Width="180" runat="server" ID="SignInPassword" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button runat="server" ID="SignInButton" Text="Sign In" OnClick="SignInButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
