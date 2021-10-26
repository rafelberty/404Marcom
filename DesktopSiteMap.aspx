<%@ Page language="c#" Inherits="ASPNetPortal.DesktopSiteMap" CodeFile="DesktopSiteMap.aspx.cs" %>
<%@ Register TagPrefix="portal" TagName="Footer" Src="DesktopPortalFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="DesktopPortalBanner.ascx" %>

<HTML>
  <HEAD>
		<title id="siteName" runat="server" />
		<link href='<%= (ConfigurationSettings.AppSettings["apppath"]) + "stylesheets/portal.css" %>' type=text/css rel=stylesheet>
		<link href='<%= (ConfigurationSettings.AppSettings["apppath"]) + "stylesheets/portal_edit.css" %>' type=text/css rel=stylesheet>
		<link href='<%= (ConfigurationSettings.AppSettings["apppath"]) + "stylesheets/menuStyle.css" %>' type=text/css rel=stylesheet>
		<link rel="stylesheet" type="text/css" href="https://home.hpe.com/lib/navigation/css/homepages-v5.css" />
  </HEAD>
		<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0" marginwidth="0" marginheight="0">

		<!--Start: ToolBar V2.0-->
		<script language="javascript" type="text/javascript" src="https://home.hpe.com/lib/navigation/header_hpe.js"></script>
		<script language="javascript" type="text/javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/leftnav.js"></script>
		<!--End: ToolBar V2.0-->
		
		
		<script language="javascript" src="<%= (ConfigurationSettings.AppSettings["apppath"])%>javascripts/find.js"></script>
		<script language="javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/expand_collapse.js"> </script> <!--left navigation area expand and collapse script-->
		<script language="javascript" src="<%= TopLevelLinks_js%>"></script>
		<script language="javascript" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>javascripts/toplevellinks.js"></script>
		
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
		<form runat="server" ID="Form1">
			<table width="100%" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<td colspan="3">
						<portal:Banner id="Banner" SelectedTabIndex="0" runat="server" />
					</td>
				</tr>
				<tr>
					<TD width="20" bgColor="#ffffff"></TD>
					<td><h1>SITE MAP</h1>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
							<TR>
								<TD width="5" bgColor="#ffffff"></TD>
								<TD bgColor="#ffffff"><FONT class="SiteMapLink">
										<asp:DataList id="AllTabs" runat="server" RepeatColumns="1" CellSpacing="1" CellPadding="1" CssClass="SiteMapLink">
											<AlternatingItemStyle CssClass="SiteMapLink"></AlternatingItemStyle>
											<SeparatorStyle Font-Bold="True" ForeColor="Gray"></SeparatorStyle>
											<ItemStyle CssClass="SiteMapLink"></ItemStyle>
											<ItemTemplate>
												&nbsp;&nbsp;<a href="<%= (ConfigurationSettings.AppSettings["apppath"])%>DesktopDefault.aspx?tabindex=0&tabid=<%# DataBinder.Eval(Container.DataItem, "TabId") %>" class='SiteMapSublink<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"TabName")).StartsWith("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") %>'><%# DataBinder.Eval(Container.DataItem, "TabName") %></a>
											</ItemTemplate>
											<FooterStyle Font-Size="Small" Font-Names="Arial" Font-Bold="True" ForeColor="Blue" BackColor="Gainsboro"></FooterStyle>
											<HeaderStyle Font-Size="Medium" Font-Names="Arial" Font-Bold="True" ForeColor="Blue" BackColor="Gainsboro"></HeaderStyle>
										</asp:DataList></FONT></TD>
							</TR>
						</TABLE>
					</td>
				
					<TD valign='top'>
						<BR>
					</TD>
				</tr>
			</table>
		</form><!--end of ASP.NET Form-->

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