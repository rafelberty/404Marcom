<%@ Import Namespace="ASPNetPortal" %>
<%@ Control Language="c#" Inherits="ASPNetPortal.DesktopPortalFooter" CodeFile="DesktopPortalFooter.ascx.cs" %>
<%@ OutputCache Duration="1" VaryByParam="none"%>

<%--

   The DesktopPortalFooter User Control is responsible for displaying the standard Portal
   footer at the bottom of each .aspx page.

--%>

<TABLE height=1 cellSpacing=0 cellPadding=0 width="100%" border=0>
<tr>
	<td align=<%=(ConfigurationSettings.AppSettings["align_left"])%> Class="FooterUpdatedDate"><img src="images/spacer.gif" width=12><%=ConfigurationSettings.AppSettings["Updated"]%><asp:Label ID=FooterUpdatedDate CssClass="FooterUpdatedDate" Runat=server/></td>
</tr>
<tr>	
	<td align=<%=(ConfigurationSettings.AppSettings["align_left"])%> Class="FooterUpdatedDate"><img src="images/spacer.gif" width=12><%=ConfigurationSettings.AppSettings["Page_Views"]%><asp:Label ID="FooterPageViews" CssClass="FooterPageViews" Runat=server/><%=ConfigurationSettings.AppSettings["Times"]%></td>	
</tr>
</table>

<%-- BELOW IS THE STANDARD @hp Footer --%>

<TABLE height=1 cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TBODY>
  <TR>
    <TD height=1>
      <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
        <TBODY>
        <TR>
          <TD vAlign=top>&nbsp;</TD>
          <TD vAlign=top><IMG height=2 src="<%=(ConfigurationSettings.AppSettings["apppath"])%>images/spacer.gif" width="98%"> </TD>
          <TD vAlign=top align=right width=111>&nbsp;</TD>
        </TR>
        <TR>
          <TD vAlign=top width=5><FONT face="Verdana, Arial, Helvetica, sans-serif" size=2><B><IMG height=8 src="<%=(ConfigurationSettings.AppSettings["apppath"])%>images/spacer.gif" width=10></B></FONT></TD>
          <TD vAlign=top><SPAN style="FONT-SIZE: x-small; COLOR: #999999; FONT-FAMILY: Verdana,Arial,Helvetica"><A style="FONT-SIZE: x-small; COLOR: #003366; FONT-FAMILY: Verdana,Arial,Helvetica" href="mailto:<%=(ConfigurationSettings.AppSettings["portal_admin_mail_address"])%>?subject=<%=(ConfigurationSettings.AppSettings["portal_admin_mail_subject"])%>" target=feedback >feedback </A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: x-small; COLOR: #003366; FONT-FAMILY: Verdana,Arial,Helvetica" href="http://webservices.cv.hp.com/portal-feedback/" target=support >support </A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <A style="FONT-SIZE: x-small; COLOR: #003366; FONT-FAMILY: Verdana,Arial,Helvetica" href="http://info.portal.hp.com/news/privacy.htm" target=privacy >privacy statement </A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HP Restricted&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;version 3.9.1<BR>Use of this site indicates you accept the <A style="FONT-SIZE: x-small; COLOR: #003366; FONT-FAMILY: Verdana,Arial,Helvetica" href="http://info.portal.hp.com/news/privacy.htm" target=privacy >Terms of Use. </A>&nbsp;&nbsp;&nbsp;&nbsp;© 1994-<asp:label id=Year Font-Names="Verdana,Arial,Helvetica" ForeColor="#999999" Font-Size="x-small" runat="server"></asp:Label> Hewlett-Packard Company&nbsp;&nbsp;&nbsp;&nbsp; <br><asp:label id=Timestamp Font-Names="Verdana,Arial,Helvetica" ForeColor="#DDDDDD" Font-Size="XX-Small" runat="server"></asp:Label></SPAN></TD>
          <TD vAlign=top align=right width=111>
            <P><IMG height="100" alt="HP Logo" src="<%=(ConfigurationSettings.AppSettings["apppath"])%>images/hp_invent.gif" width="111"></P></TD>
		</TR>
		</TBODY>
		</TABLE>
		</TD>
		</TR>
		</TBODY>
		</TABLE>
