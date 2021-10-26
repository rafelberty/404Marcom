<%@ Import Namespace="ASPNetPortal" %>
<%@ Control Language="c#" Inherits="ASPNetPortal.DesktopPortalTabs" CodeFile="DesktopPortalTabs.ascx.cs" %>
<%--

   The DesktopPortalTabs User Control is responsible for displaying the standard Portal
   tabs at the left side of each .aspx page.

   The DesktopPortalTabs uses the Portal Configuration System to obtain a list of the
   tab settings. It then render's this content into the page.

--%>
<!--CODE ADDED -->
<TABLE align=left class=leftNavSkin cellSpacing=0 cellPadding=0 width=147 border=0>
<TBODY>
<TR>
<TH class=leftNavHeader><IMG height=6 src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width=138></TH>
<TH class=leftNavHeader><IMG height=6 src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width=1></TH></TR>
<TR>
<TD colSpan=3><IMG height=10 src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width=138></TD></TR>
<TR>
<TD align=left width=138>
		<asp:label id="TabLinksHeading" CssClass="leftNavDown" runat="server"></asp:label>
		<asp:datalist id="TabLinks" DataKeyField="LinkID" runat="server" cssclass="leftNav" repeatdirection="vertical" ItemStyle-Cellpadding="0" ItemStyle-BorderStyle="none" ItemStyle-Height="0" SelectedItemStyle-CssClass="leftNav" ItemStyle-BorderWidth="0" EnableViewState="true">
			<ItemStyle Height="0px" BorderWidth="0px" BorderStyle="None" VerticalAlign="Top"></ItemStyle>
			<ItemTemplate>
				<div><img src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="11" height="8"</td><td>
				<asp:HyperLink ID="TabLinkText" cssclass="leftNav" Text='<%# DataBinder.Eval(Container.DataItem,"LinkTitle") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"LinkUrl") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem,"Description") %>' Target='<%# DataBinder.Eval(Container.DataItem,"Window") %>' runat="server" /></div>
			</ItemTemplate>
		</asp:datalist>
		<asp:datalist id="EditableTabLinks" DataKeyField="LinkID" runat="server" cssclass="leftNav" repeatdirection="vertical" ItemStyle-Cellpadding="0" ItemStyle-BorderStyle="none" ItemStyle-Height="0" SelectedItemStyle-CssClass="leftNav" ItemStyle-BorderWidth="0" EnableViewState="true">
			<ItemStyle Height="0px" BorderWidth="0px" BorderStyle="None" VerticalAlign="Top"></ItemStyle>
			<ItemTemplate>
				<div><asp:HyperLink ID=EditableTabLinkImage ImageUrl='images/edit.gif' BorderWidth='0' NavigateUrl='<%# ConfigurationSettings.AppSettings["apppath"]+ "DesktopModules/EditTabLinks.aspx?Delete=0&LinkID="+DataBinder.Eval(Container.DataItem,"LinkID")+"&TabID="+DataBinder.Eval(Container.DataItem,"TabID")%>' Runat='server'/>
				<asp:HyperLink ID="EditableTabLinkText" cssclass="leftNav" Text='<%# DataBinder.Eval(Container.DataItem,"LinkTitle") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"LinkUrl") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem,"Description") %>' Target='<%# DataBinder.Eval(Container.DataItem,"Window") %>' runat="server"/>
				<asp:HyperLink ID="DeleteTabLink" ImageUrl='images/delete.gif' BorderWidth='0' NavigateUrl='<%# ConfigurationSettings.AppSettings["apppath"]+ "DesktopModules/EditTabLinks.aspx?Delete=1&LinkID="+DataBinder.Eval(Container.DataItem,"LinkID")+"&TabID="+DataBinder.Eval(Container.DataItem,"TabID")%>' Runat='server'/></div>
			</ItemTemplate>
		</asp:datalist><asp:label id="TabLinksFooter" CssClass="leftNavDown" runat="server">&nbsp;</asp:label>

<asp:Image ID="HR1" BackColor="#ECE9D8" Width=136 Height="1" ImageUrl="images/spacer.gif" Runat=server/>	
	<asp:label id=TopLevelTabsHeading CssClass="leftNavDown" runat="server"></asp:label>
		<asp:datalist id=TopLevelTabs runat="server" cssclass="leftNav" repeatdirection="vertical" ItemStyle-Cellpadding="0" ItemStyle-BorderStyle="none" ItemStyle-Height="0" SelectedItemStyle-CssClass="leftNav" ItemStyle-BorderWidth="0" EnableViewState="true" BackColor="#ffffff" BorderColor="#ffffff">
		<SelectedItemStyle CssClass="leftNav"></SelectedItemStyle>
		<SelectedItemTemplate>
			<div><span class="leftNav"><img alt="This Page" src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="7" height="8"></td><td><img src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="10" height="1"><%# ((TabStripDetails) Container.DataItem).TabName %></span></div>
		</SelectedItemTemplate>
		<ItemStyle Height="0px" BorderWidth="0px" BorderStyle="None" VerticalAlign="Top"></ItemStyle>
		<ItemTemplate>
			<div></td><td style="PADDING-LEFT: 11px; MARGIN-LEFT: 11px"><a title="<%# ((TabStripDetails) Container.DataItem).TabName %>" href="DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>" class="leftNav"><%# ((TabStripDetails) Container.DataItem).TabName %></a></span></div>
		</ItemTemplate>
	</asp:datalist><asp:label id="TopLevelTabsFooter" CssClass="leftNavDown" runat="server"></asp:label>
	
<asp:Image ID="HR2" BackColor="#ECE9D8" Width=136 Height="1" ImageUrl="images/spacer.gif" Runat=server/>	
	<asp:label id=ParentTabsHeading CssClass="leftNavDown" runat="server">ParentTabsHeading</asp:label>
	<asp:datalist id=ParentTabs runat="server" cssclass="leftNav" repeatdirection="vertical" ItemStyle-Cellpadding="0" ItemStyle-BorderStyle="none" ItemStyle-Height="0" SelectedItemStyle-CssClass="leftNav" ItemStyle-BorderWidth="0" EnableViewState="true" BackColor="#ffffff" BorderColor="#ffffff">
		<SelectedItemStyle CssClass="leftNav"></SelectedItemStyle>
		<SelectedItemTemplate>
			<div><span class="leftNav"><img alt="This Page" src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="7" height="8"></td><td><img src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="10" height="1"><%# ((TabStripDetails) Container.DataItem).TabName %></span></div>
		</SelectedItemTemplate>
		<ItemStyle Height="0px" BorderWidth="0px" BorderStyle="None" VerticalAlign="Top"></ItemStyle>
		<ItemTemplate>
			<div></td><td style="PADDING-LEFT: 11px; MARGIN-LEFT: 11px"><a title="<%# ((TabStripDetails) Container.DataItem).TabName %>" href="DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>" class="leftNav"><%# ((TabStripDetails) Container.DataItem).TabName %></a></span></div>
		</ItemTemplate>
	</asp:datalist><asp:label id="ParentTabsFooter" CssClass="leftNavDown" runat="server">&nbsp;</asp:label>

<asp:Image ID="HR3" BackColor="#ECE9D8" Width=136 Height="1" ImageUrl="images/spacer.gif" Runat=server/>		
	<asp:label id=SiblingTabsHeading CssClass="leftNavDown" runat="server">SiblingTabsHeading</asp:label>
	<asp:datalist id=SiblingTabs runat="server" cssclass="leftNav" repeatdirection="vertical" ItemStyle-Cellpadding="0" ItemStyle-BorderStyle="none" ItemStyle-Height="0" SelectedItemStyle-CssClass="leftNav" ItemStyle-BorderWidth="0" EnableViewState="true">
		<SelectedItemStyle CssClass="leftNav"></SelectedItemStyle>
		<SelectedItemTemplate>
			<div></td><td style="PADDING-LEFT: 11px; MARGIN-LEFT: 11px"><a title="<%# ((TabStripDetails) Container.DataItem).TabName %>" href="DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>" class="leftNav"><%# ((TabStripDetails) Container.DataItem).TabName %></a></span></div>
		</SelectedItemTemplate>
		<ItemStyle Height="0px" BorderWidth="0px" BorderStyle="None" VerticalAlign="Top"></ItemStyle>
		<ItemTemplate>
			<div></td><td style="PADDING-LEFT: 11px; MARGIN-LEFT: 11px"><a title="<%# ((TabStripDetails) Container.DataItem).TabName %>" href="DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>" class="leftNav"><%# ((TabStripDetails) Container.DataItem).TabName %></a></span></div>
		</ItemTemplate>
	</asp:datalist><asp:label id="SiblingTabsFooter" CssClass="leftNavDown" runat="server">&nbsp;</asp:label>

<asp:Image ID="HR4" BackColor="#ECE9D8" Width=136 Height="1" ImageUrl="images/spacer.gif" Runat=server/>		
		<asp:label id=ChildTabsHeading CssClass="leftNavDown" runat="server">ChildTabsHeading</asp:label>
		<asp:datalist id=ChildTabs runat="server" cssclass="leftNav" repeatdirection="vertical" ItemStyle-Cellpadding="0" ItemStyle-BorderStyle="none" ItemStyle-Height="0" SelectedItemStyle-CssClass="leftNav" ItemStyle-BorderWidth="0" EnableViewState="true">
			<SelectedItemStyle CssClass="leftNav"></SelectedItemStyle>
			<SelectedItemTemplate>
				<div><img alt="This Page" src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="7" height="8"></td><td><span class="leftNav"><img src="<%=ConfigurationSettings.AppSettings["apppath"]%>images/spacer.gif" width="10" height="1"><%# ((TabStripDetails) Container.DataItem).TabName %></span></div>
			</SelectedItemTemplate>
			<ItemStyle Height="0px" BorderWidth="0px" BorderStyle="None" VerticalAlign="Top"></ItemStyle>
			<ItemTemplate>
				<div></td><td style="PADDING-LEFT: 11px; MARGIN-LEFT: 11px"><a title="<%# ((TabStripDetails) Container.DataItem).TabName %>" href="DesktopDefault.aspx?tabindex=<%# Container.ItemIndex %>&tabid=<%# ((TabStripDetails) Container.DataItem).TabId %>" class="leftNav"><%# ((TabStripDetails) Container.DataItem).TabName %></a></span></div>
			</ItemTemplate>
		</asp:datalist><asp:label id="ChildTabsFooter" CssClass="leftNav" runat="server">&nbsp;</asp:label>
		
	</FONT>
</TD>
<TD width=1><IMG height=20 
src="https://portal.hp.com/lib/navigation/images/spacer.gif" width=1></TD></TR>
<TR>
<TD colSpan=3><IMG height=10 
src="https://portal.hp.com/lib/navigation/images/spacer.gif" 
width=138></TD></TR></TBODY>
</TABLE>



