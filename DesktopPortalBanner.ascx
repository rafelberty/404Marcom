<%@ Reference Page="~/data/filemanager.aspx" %>
<%@ Import Namespace="ASPNetPortal" %>
<%@ Control Language="c#" Inherits="ASPNetPortal.DesktopPortalBanner" CodeFile="DesktopPortalBanner.ascx.cs" %>



<%--

   The DesktopPortalBanner User Control is responsible for displaying the standard Portal
   banner at the top of each .aspx page.

   The DesktopPortalBanner uses the Portal Configuration System to obtain a list of the
   portal's sitename and tab settings. It then render's this content into the page.

--%>
<!--CODE ADDED -->
		<SCRIPT language="javascript" type="text/javascript">
			<!--// Hide from old browsers

			function getCurrentDate() {
			var day;
			var now;

				now = new Date();
			   day = now.getDay();
			   var dayname;
			   if (day == 0) dayname = "Sunday";
			   if (day == 1) dayname = "Monday";
			   if (day == 2) dayname = "Tuesday";
			   if (day == 3) dayname = "Wednesday";
			   if (day == 4) dayname = "Thursday";
			   if (day == 5) dayname = "Friday";
			   if (day == 6) dayname = "Saturday";

			   var month
			   month = now.getMonth();
				var monthname;
			   if (month == 0) monthname = "January";
			   if (month == 1) monthname = "February";
			   if (month == 2) monthname = "March";
			   if (month == 3) monthname = "April";
			   if (month == 4) monthname = "May";
			   if (month == 5) monthname = "June";
			   if (month == 6) monthname = "July";
			   if (month == 7) monthname = "August";
			   if (month == 8) monthname = "September";
			   if (month == 9) monthname = "October";
			   if (month == 10) monthname = "November";
			   if (month == 11) monthname = "December";

			   date = now.getDate();
			   year = now.getYear();
			   if (year < 1000)
					year += 1900;

				return (monthname + " " + date + ", " + year);
			}
			//-->
		</SCRIPT>

<table cellSpacing="0" cellPadding="0" border="0" width="100%">
	<tr>
		<td class="BreadcrumbNavigation" align=right colspan=5>
		<asp:Label ID="BreadcrumbNavigationLabel" Runat=server CssClass="BreadcrumbNavigation" ></asp:Label><img src="images/spacer.gif" width="18" height="1">
		</td>
		<td class="RightColumnSetup">
			<table border="0" cellpadding="0" width="182" cellspacing="0">
				<!-- Current Date -->
				<tr>
					<td class="DateHeader">
						<script language="javascript">
							document.write(getCurrentDate());
						</script>
					</td>
				</tr>
			</table>
		</td>		
	</tr>
	<tr>
		<td colspan=3>&nbsp;<asp:imagebutton id="ModeSwitch" runat="server" tabIndex=15></asp:imagebutton>&nbsp;<nobr><asp:imagebutton id="EditThisPageLayout" runat="server" AlternateText="Click to edit this page layout. To edit content, select the pencil icon or 'add/edit' link at top right of module" tabIndex=21></asp:imagebutton>&nbsp;<asp:imagebutton id="CopyThisPage" runat="server"  AlternateText="Click to copy this page." tabIndex=22></asp:imagebutton>&nbsp;<asp:imagebutton id="DeleteThisPage" runat="server"  AlternateText="Click to delete this page. WARNING: Although only this page is deleted - all its child pages are orphaned and no longer appear on the site until they are given new parent page(s)" tabIndex=23></asp:imagebutton>&nbsp;<asp:imagebutton id="CreateToplevelPage" runat="server"  AlternateText="Click to create a top level page." tabIndex=24></asp:imagebutton>&nbsp;<asp:imagebutton id="CreateSiblingPage" runat="server"  AlternateText="Click to create sibling page." tabIndex=25></asp:imagebutton>&nbsp;<asp:imagebutton id="CreateChildPage" runat="server"  AlternateText="Click to create child page." tabIndex=26></asp:imagebutton>&nbsp;<!--<img src="images/spacer.gif" width=20 height=1 border=0>&nbsp;<asp:ImageButton ID="FileManager" AlternateText="Click Here to Launch File Manager" Runat=server></asp:ImageButton>--></nobr></td>
		<td>&nbsp;</td>
		<td></td>
		<td align=right>&nbsp;<asp:imagebutton cssclass="OtherTabs" tabindex=17 id="PrintablePage" CausesValidation=False runat="server" width="14" height="14" border="0" AlternateText="Print printable version of this page" ></asp:imagebutton>&nbsp;<img src="<%=(ConfigurationSettings.AppSettings["apppath"])%>images/blue1x10.gif" border="0">&nbsp;<A tabindex=20 class="OtherTabs" href="<%=(ConfigurationSettings.AppSettings["apppath"])%>DesktopSiteMap.aspx?i=0"><img src="<%=(ConfigurationSettings.AppSettings["apppath"])%>images/SiteMap.gif" alt="SiteMap" border="0"></A>&nbsp;&nbsp;</td>
	</tr>
</table>
<!-- END OF CODE ADDED -->
<!-- Normal Welcome bar removed -->
