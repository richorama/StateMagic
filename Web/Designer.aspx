<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Designer.aspx.cs" Inherits="StateMagic.Web.Designer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <object width="300" height="300"
    data="data:application/x-silverlight-2," 
    type="application/x-silverlight-2" >
    <param name="source" value="WpfClient.xbap"/>
    
</object>
<!--
        <object id="ShowcaseNav" style="width: 1024px; height: 768px;" autoupdate="true" data="data:application/x-silverlight-2,"
            type="application/x-silverlight-2">
            <param name="MinRuntimeVersion" value="4.0.50401.0">
            <param name="Source" value="WpfClient.xbap">
            <param name="windowless" value="true">
            <a style="text-decoration: none;" href="http://go.microsoft.com/fwlink/?LinkId=149156">
                <img style="border-width: 0px" alt="Install Silverlight" src="http://i3.silverlight.net/resources/images/content/misc/Install-Silverlight-611x355-HomeShowcaseSize.png?cdn_id=06212010"></a>
                </object>-->
    </div>
    </form>
</body>
</html>
