<%@ Register TagPrefix="portal" TagName="Footer" Src="DesktopPortalFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Tabs" Src="DesktopPortalTabs.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="DesktopPortalBanner.ascx" %>
<%@ Page language="c#"   ValidateRequest="false" Inherits="ASPNetPortal.IDesktopDefault" CodeFile="IDesktopDefault.aspx.cs"  %>


<%--

   The DesktopDefault.aspx page is used to load and populate each Portal View.  It accomplishes
   this by reading the layout configuration of the portal from the Portal Configuration
   system, and then using this information to dynamically instantiate portal modules
   (each implemented as an ASP.NET User Control), and then inject them into the page.

--%>
<HTML id="PageHTMLTag" runat="server">
	<HEAD>
		<title id="siteName" runat="server" />
		
		<META HTTP-EQUIV="expires" CONTENT="0" />
		<META http-equiv="Pragma" content="no-cache" />
		<META http-equiv="Cache-Control" content="no-cache" />

		<!-- Meta data standards can be found at http://eservices.athp.hp.com/documents/MetaStan.asp -->

		<meta name="TargetAudience" content=" " />
		<!-- All dates in ISO 8601 YYYY-MM-DD format -->
		<meta name="ATHP_Date_Created" id="ATHP_Date_Created" content="YYYY-MM-DD" runat="server" />
		<meta name="ATHP_Date_Modified" id="ATHP_Date_Modified" content="YYYY-MM-DD" runat="server" />


		<!-- Use Controlled Vocabulary for Languages for correct code -->
		<meta name="ATHP_Language" content=" "  id="ATHP_Language" runat="server" />
		<meta name="ATHP_Language" content="<%=(ConfigurationSettings.AppSettings["Meta_ATHP_Language"])%>">
				

		<!-- Use Controlled Vocabulary for Locations for Geography, Region, Country, and Site codes. -->
		<meta name="ATHP_Geography" content="<%=(ConfigurationSettings.AppSettings["Meta_ATHP_Geography"])%>">

		<!-- Region, Country and Site meta tags are required if geography code is not ALL. -->
		<meta name="ATHP_Region" content=" " />
		<meta name="ATHP_Country" content=" "  id="ATHP_Country" runat="server" />
		<meta id="description" runat="server" />
		<meta id="keywords" runat="server" />
		<meta id="Locale" runat="server" />	
		<meta name="ATHP_Site" content=" " />
		<meta name="ATHP_Creator" content=" " />
		<meta name="ATHP_Creator_Email" content=" " />
		<META NAME="ATHP_Publisher" CONTENT=" " />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link rel="stylesheet" type="text/css" href="https://home.hpe.com/lib/navigation/css/homepages-v5.css" />
		<link href="stylesheets/ImageBuilder.css" type="text/css" rel="stylesheet">
		
	</HEAD>
	<body  bgcolor="#FFFFFF" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0" marginwidth="0" marginheight="0">

		<!--Start: ToolBar V2.0-->
		<script language="javascript" type="text/javascript" src="https://home.hpe.com/lib/navigation/header_hpe.js"></script>
		
		<script language="javascript" type="text/javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/leftnav.js"></script>
		<!--End: ToolBar V2.0-->
		
		
		<script language="javascript" type="text/javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/expand_collapse.js"> </SCRIPT> <!--left navigation area expand and collapse script-->
		<script language="javascript" type="text/javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/toplevellinks.js"></script>
		<script language="javascript" type="text/javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/fieldcounter.js"></script>
		<SCRIPT language=javascript type="text/javascript">
		
		
		 
		<!--// Hide from old browsers
		var ToolBar_Supported = ToolBar_Supported;
		if (ToolBar_Supported != null && ToolBar_Supported == true)
		{
			<%= HeaderSetBanner%>;
			<%= HeaderDisplayLogin%>;
			<%= HeaderSetSiteSearch%>;
			<%= HeaderSetWarningMessage%>;
			<%= HeaderSetGreetingMessage%>;
			<%= HeaderDrawHeader%>;
		}
		//-->
		</SCRIPT>

		<form runat="server" id="DesktopDefault" name="DesktopDefault">
		<input id="Amit" type="hidden" runat="server"  value=""/>
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<td colspan="2"><portal:Banner id="Banner" SelectedTabIndex="0" runat="server"/></td>
				</tr>
				<tr>
					<td width="100%">
						<table align=left width="100%" cellspacing="0" cellpadding="0" border="0">
							<tr height="*" valign="top">
								<td align="left"><portal:Tabs id="Tabs" SelectedTabIndex="0" runat="server"/></td>
								<td align="left" width="100%">
									<table width="100%" cellspacing="0" cellpadding="0" border="0">
									<tr><td>
									<%--Main body area top table--%>
									<table border="0"  align="left" width="100%" >
									<tr>
										<td id="TopLeftPane" align="left" valign="top" Visible="false" Width="100%" runat="server">
										</td>
										<td class="ModuleWidthSeparator"></td>
										<td id="TopMiddlePane" align="middle" valign="top" Visible="false" Width="*" runat="server">
										</td>
										<td class="ModuleWidthSeparator"></td>
										<td  class="RightColumnSetup" id="TopRightPane" align="right" valign="top" Visible="false" Width="182" runat="server">
										</td>									
									</tr>
									</table>
									</td></tr>
									<tr><td>
									<%--Main body area centre table--%>
									<table border="0" width="100%" >
									<tr>
										<td id="CenterLeftPane" align="left" valign="top" Visible="false" Width="100%" runat="server">
										</td>
										<td class="ModuleWidthSeparator"></td>
										<td id="CenterMiddlePane" align="middle" valign="top" Visible="false" Width="*" runat="server">
										</td>
										<td class="ModuleWidthSeparator"></td>
										<td  class="RightColumnSetup" id="CenterRightPane" align="right"  valign="top" Visible="false" Width="182" runat="server">
										</td>									
									</tr>
									</table>
									</td></tr>
									<tr><td>
									<%--Main body area bottom table--%>
									<table border="0" width="100%" >
									<tr>
										<td id="BottomLeftPane" align="left"  valign="top" Visible="false" Width="100%" runat="server">
										</td>
										<td class="ModuleWidthSeparator"></td>
										<td id="BottomMiddlePane" align="middle"  valign="top" Visible="false" Width="*" runat="server">
										</td>
										<td class="ModuleWidthSeparator"></td>
										<td  class="RightColumnSetup" id="BottomRightPane" align="right"  valign="top" Visible="false"  Width="182" runat="server">
										</td>									
									</tr>
									</table>
								</td></tr>
								
								</table>
								
									
								</td>
								<td></td>
							</tr>
						</table>
					</td>
				</tr>
				
                

			</table>
			<table>
				<tr><td><font size=1>Updated:<asp:Label ID=FooterUpdatedDate CssClass="FooterUpdatedDate" Runat=server/></font></td></tr>
				<tr><td><font size=1>Viewed:<asp:Label ID="FooterPageViews" CssClass="FooterPageViews" Runat=server/>&nbsp;times.</font></td></tr>
			</table>
			
		</form>
		<script language="javascript" type="text/javascript">
		<!--// Hide from old browsers
			<%= FooterSetFooterFeedbackPage%>;
			<%= FooterSetLocalSupportPage%>;
			<%= FooterSetPageConfidential%>;
			<%= FooterSetPagePublic%>;
			<%= FooterSetVersionNumber%>;
			<%= FooterSetTargetFrame%>;
			<%= FooterDrawSeparator%>;
			<%= FooterDrawFooter%>;
		//-->
		</script>
	</body>
</HTML>
