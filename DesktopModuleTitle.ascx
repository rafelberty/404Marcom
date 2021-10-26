<%@ Control Language="c#" Inherits="ASPNetPortal.DesktopModuleTitle" CodeFile="DesktopModuleTitle.ascx.cs" %>
<%--

   The PortalModuleTitle User Control is responsible for displaying the title of each
   portal module within the portal -- as well as optionally the module's "Edit Page"
   (if such a page has been configured).

--%>
<table id="t1" runat="server" cellspacing="0" cellpadding="0" border=0 width="100%">
	<tr style="PADDING-TOP: 2px" width="100%">
		<td valign=middle id="td1" runat="server" ><div><asp:label id="ModuleTitle" cssclass="ModuleHeader" EnableViewState="false" runat="server" />&nbsp;<asp:ImageButton  id=ColorPickerBtn BorderWidth=0 ImageAlign=Top runat="server" AlternateText="Click Here to Edit Module Appearance"></asp:ImageButton><asp:hyperlink ForeColor="#9999ff" id="EditButton" cssclass="ModuleHeader"  EnableViewState="false" runat="server" ></asp:hyperlink><asp:label id="ModuleStatus" cssclass="ModuleStatus" EnableViewState="false" runat="server" /></td><td valign=middle align=right id="td2" runat="server" ><asp:Label ID="ExpandScript" Runat=server></asp:Label>&nbsp;</td>
	</tr>
	<tr>
		<td height=0 valign=top id="td3" runat="server" colspan="2"></td>
	</tr>
</table>

<div id="<%= ControlID%>">
<asp:Label ID="ToggleScript" Runat=server></asp:Label>