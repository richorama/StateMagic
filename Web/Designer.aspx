<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Designer.aspx.cs" Inherits="StateMagic.Web.Designer"
 MasterPageFile="~/Master.Master" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <style type="text/css">
    html, body {
	    
	    overflow: auto;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }
    </style>
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID=ContentPlaceHolderMenu>
<ul id="dropmenu">
    <li runat="server" id="SignOutLink" class="page_item page-item-2"><a href="SignOut.aspx" title="Sign Out">Sign Out</a></li>
    <li runat="server" id="UserDetailsLink" class="page_item page-item-2"><a href="UserDetails.aspx" title="Your Account">Your Account</a></li>
    <li runat="server" id="SignInLink" class="page_item page-item-2"><a href="SignIn.aspx" title="Sign In">Sign In</a></li>
</ul>
</asp:Content>
    <asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
    <div id="silverlightControlHost">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="600px">
		  <param name="source" value="ClientBin/StateDesigner.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="3.0.40818.0" />
		  <param name="autoUpgrade" value="true" />
          <param name="windowless" value="true" />
          <asp:PlaceHolder runat="server" id="initParams"></asp:PlaceHolder>
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
        </form>
    </asp:Content>
