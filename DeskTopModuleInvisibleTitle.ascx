<%@ Control Language="c#" Inherits="ASPNetPortal.DeskTopModuleInvisibleTitle" CodeFile="DeskTopModuleInvisibleTitle.ascx.cs" %>
<%--

   The PortalModuleInvisibleTitle User Control is responsible for "displaying the invisible title of each
   portal module within the portal -- as well as optionally the module's "Edit Page"
   (if such a page has been configured).

--%>
<table width="100%" cellspacing="0" cellpadding="0">
	<tr>
		<td align="left">
			<asp:hyperlink id="EditButton" cssclass="CommandButton" EnableViewState="false" runat="server" />
<asp:ImageButton id=ColorPickerBtn runat="server" ImageUrl="Images/ColorPicker/coloricon.gif" AlternateText="Click Here to Edit Module Appearance"></asp:ImageButton>
<asp:label id=ModuleStatus runat="server" EnableViewState="false" cssclass="ModuleStatus"></asp:label>
		</td>
	</tr>
</table>
