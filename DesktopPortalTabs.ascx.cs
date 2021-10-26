using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace ASPNetPortal
{


	/// <summary>
	///	The DesktopPortalTabs.ascx module is for rendering the left navigation area consisting of both user defined links and automated links based on the position of the page relative to the others in the site.
	/// <para></para>
	/// <para>The features of the left navigation area include:</para>
	/// <list type="bullet">
	/// <item>
	/// <description>Display of user defined links, in either browse mode or edit mode</description>
	/// </item>
	/// <item>
	/// <description>Only those pages which have not been flagged as "Hidden From Navigation" will have their links displayed</description>
	/// </item>
	/// <item>
	/// <description>Automated build of 4 groups of links, top level, parent, sibling or child</description>
	/// </item>
	/// <item>
	/// <description>Depending on the group involved, the links group is either expanded or collapsed by default</description>
	/// </item>
	/// <item>
	/// <description>the separators are displayed only as required</description>
	/// </item>
	/// </list>
	/// </summary>
	/// <seealso cref="DesktopDefault"/>
	/// <seealso cref="DesktopPortalBanner"/>
	/// <seealso cref="DesktopPortalFooter"/>
	public partial  class DesktopPortalTabs : System.Web.UI.UserControl
		{
			

			public int tabId;
			public int tabIndex;
			public bool ShowTabs = true;
			protected System.Web.UI.WebControls.Label TabLinksSpacer;
			protected System.Web.UI.WebControls.HyperLink TabLinkImage;
			protected System.Web.UI.WebControls.HyperLink EditableTabLinkImage;
			protected System.Web.UI.WebControls.HyperLink EditableTabLinkText;
			protected System.Web.UI.WebControls.HyperLink DeleteTabLink;
			

			protected String LogoffLink = "";
			protected String linkImage = "";
			protected String EditLinkUrl = "";
			protected String TargetUrl="";
			protected int HR1Visible=0;
			protected int HR2Visible=0;
			protected int HR3Visible=0;
			protected int HR4Visible=0;
		

			protected void Page_Load(object sender, System.EventArgs e) 
			{

				// Obtain PortalSettings from Current Context
				PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

				// If user logged in, customize welcome message
                if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated) 
				{
        
					// if authentication mode is Cookie, provide a logoff link
					if (Context.User.Identity.AuthenticationType == "Forms") 
					{
						LogoffLink = "<" + "span class=\"Accent\">|</span>\n" + "<" + "a href=" + Request.ApplicationPath + "/Admin/Logoff.aspx class=SiteLink> Logoff" + "<" + "/a>";
					}

				}

				// Determine if the page is printable
				try
				{
					if (Request.Params["printable"]== "true") 
					{
						this.Visible=false;
					}
				}
				catch
				{this.Visible=true;}

				// Dynamically render portal tab strip
				if (ShowTabs == true) 
				{

					tabId = portalSettings.ActiveTab.TabId;
					tabIndex = portalSettings.ActiveTab.TabIndex;

					// Build list of tabs to be shown to user                                   

					
					#region Tab Links
					// Obtain tablinks information from the TabLinks table
					// and bind to the datalist control
					ASPNetPortal.Database.TabLinksDB Tablinks = new ASPNetPortal.Database.TabLinksDB();

					//if the request is authenticated, show the editable tabs, otherwise show the normal tabs

					// Check if user is in editing mode, if applicable
					Components.UsersDB user = new ASPNetPortal.Components.UsersDB();
					int Editing=0;


                    if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated && ASPNetPortal.Components.PortalSecurity.UserName != null && ASPNetPortal.Components.PortalSecurity.UserName != "")
					{
						try{Editing = user.GetUserEditingStatus(ASPNetPortal.Components.PortalSecurity.UserName);}
						catch{Editing=0;}
					}
					
					if (Page.IsPostBack==false  && Editing==1)
					{

                        if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated && Editing == 1) 
						{
						
							EditableTabLinks.Visible=true;
							TabLinks.Visible=false;
						
							EditableTabLinks.DataSource = Tablinks.GetCurrentTabLinks(tabId);
							EditableTabLinks.DataBind();

							if (EditableTabLinks.Items.Count==0)
							{
								TabLinksHeading.Visible=false;
								TabLinksFooter.Text="<table><tr><td><img src='images/spacer.gif' width=10 height=1 border='0'>&nbsp;<span><a class='leftNav' href='DesktopModules/EditTabLinks.aspx?LinkID=-1&TabID=" + tabId +"'><font class='leftNav'>...Add " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString()+ "</font></a></span><BR><BR></td></tr></table>";
							}
							else
							{
								TabLinksHeading.Text = "<div><a title=\"show/hide " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() +"\" id=\"KeyLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'KeyLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<font class='leftNavDown'>" + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() + "</font><BR><div id=\"KeyLinks\">";
								TabLinksFooter.Text="<table><tr><td><img src='images/spacer.gif' width=10 height=1 border='0'>&nbsp;<span><a class='leftNav' href='DesktopModules/EditTabLinks.aspx?LinkID=-1&TabID=" + tabId +"'><font class='leftNav'>...Add " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString()+ "</font></a></span><BR><BR></td></tr></table></div></div>";
								HR1Visible=1;
							}
							
							// to toggle the footer to start as collapsed, add this TO ALL CONDITIONS to the end of the TabLinksFooter.Text:   <script language=\"javascript\">toggle(getObject('KeyLinks_link'), 'KeyLinks');</script>
						
						}
                        else if (Editing != 1 || !ASPNetPortal.Components.PortalSecurity.IsAuthenticated) 
						{

							EditableTabLinks.Visible=false;
							TabLinks.Visible=true;
							TabLinks.DataSource = Tablinks.GetCurrentTabLinks(tabId);
							TabLinks.DataBind();
					
							if (TabLinks.Items.Count==0)
							{
								TabLinksHeading.Visible=false;
								TabLinksFooter.Visible=false;
							}
							else
							{
								TabLinksHeading.Text = "<div><a title=\"show/hide " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() +"\" id=\"KeyLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'KeyLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<font class='leftNavDown'>" + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() + "</font><BR><div id=\"KeyLinks\">";
								TabLinksFooter.Text="</div></div>";
								HR1Visible=1;
							}

							
						}
					}
					else
					{
						if (Editing==1) 
						{
						
							EditableTabLinks.Visible=true;
							TabLinks.Visible=false;
						
							EditableTabLinks.DataSource = Tablinks.GetCurrentTabLinks(tabId);
							EditableTabLinks.DataBind();

							if (EditableTabLinks.Items.Count==0)
							{
								TabLinksHeading.Visible=false;
								TabLinksFooter.Visible=false;
							}
							else
							{
								TabLinksHeading.Text = "<div><a title=\"show/hide " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() +"\" id=\"KeyLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'KeyLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<font class='leftNavDown'>" + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() + "</font><BR><div id=\"KeyLinks\">";
								TabLinksFooter.Text="<table><tr><td><img src='images/spacer.gif' width=10 height=1 border='0'>&nbsp;<span><a class='leftNav' href='DesktopModules/EditTabLinks.aspx?LinkID=-1&TabID=" + tabId +"'><font class='leftNav'>...Add " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString()+ "</font></a></span><BR><BR></td></tr></table></div></div>";
								HR1Visible=1;
							}
							
							
						
						}
						else
						{

							EditableTabLinks.Visible=false;
							TabLinks.Visible=true;
							TabLinks.DataSource = Tablinks.GetCurrentTabLinks(tabId);
							TabLinks.DataBind();
					
							if (TabLinks.Items.Count==0)
							{
								TabLinksHeading.Visible=false;
								TabLinksFooter.Visible=false;
							}
							else
							{
								TabLinksHeading.Text = "<div><a title=\"show/hide " + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() +"\" id=\"KeyLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'KeyLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<font class='leftNavDown'>" + (ConfigurationSettings.AppSettings["Key_Links_Label"]).ToString() + "</font><BR><div id=\"KeyLinks\">";
								TabLinksFooter.Text="</div></div>";
								HR1Visible=1;
							}
						}
					}
					
					#endregion

					#region Top Level Tabs
						
					if( portalSettings.ActiveTab.ParentTabId!=-1)
					{
					
					
						ArrayList TopLevelauthorisedTabs = new ArrayList();
						int TopLeveladdedTabs = 0;

						for (int i=0; i < portalSettings.TopLevelTabs.Count; i++) 
						{
            
							TabStripDetails tab = (TabStripDetails)portalSettings.TopLevelTabs[i];

							if (Components.PortalSecurity.IsInRoles(tab.AuthorizedRoles) && tab.NavVisible!=-1) 
							{ 
								TopLevelauthorisedTabs.Add(tab);
							}


							TopLeveladdedTabs++;
						}          

						// Populate Tab List at left of the Page with Parent tabs
						TopLevelTabs.DataSource = TopLevelauthorisedTabs;
						TopLevelTabs.DataBind();

						//Get the Top Level Tabs Heading
						TopLevelTabsHeading.Text = "<div><a title=\"show/hide top level site links\" id=\"TopLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'TopLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<a href='"+ConfigurationSettings.AppSettings["apppath"]+"'><font class='leftNavDown'>"+portalSettings.PortalName+"</font></a><BR><div id=\"TopLinks\">";
						TopLevelTabsFooter.Text="</div></div><script language=\"javascript\">toggle(getObject('TopLinks_link'), 'TopLinks');</script>";
						HR2Visible=1;
					}




				}
					#endregion

					#region Parent Level Tabs
					//If the tab is not top level tab, add the parent tabs
					if (portalSettings.ActiveTab.TabId!=-1)
					{
						ArrayList ParentauthorisedTabs = new ArrayList();
						int ParentLeveladdedTabs = 0;

						for (int i=0; i < portalSettings.ParentTabs.Count; i++) 
						{
	            
							TabStripDetails tab = (TabStripDetails)portalSettings.ParentTabs[i];
							
							if (tab.ParentTabId!=-1)
							{
								if (Components.PortalSecurity.IsInRoles(tab.AuthorizedRoles) && tab.NavVisible!=-1) 
								{ 
									ParentauthorisedTabs.Add(tab);
								}
							}
							ParentLeveladdedTabs++;
						}          

						// Populate Tab List at left of the Page with Parent tabs
						
						ParentTabs.DataSource = ParentauthorisedTabs;
						ParentTabs.DataBind();


						//Get the Parent Tabs Heading
						if (ParentauthorisedTabs.Count==0)
						{
							ParentTabsHeading.Visible=false;
							ParentTabsFooter.Visible=false;
							ParentTabs.Visible = false;
						}
						else
						{
							ParentTabsHeading.Text = "<div><a title=\"show/hide pages one level up\" id=\"ParentLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'ParentLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<a href='DesktopDefault.aspx?tabid="+portalSettings.ParentTabsHeadingTabID+"'><font class='leftNavDown'>"+portalSettings.ParentTabsHeading+"</font></a><BR><div id=\"ParentLinks\">";
							ParentTabsFooter.Text = "</div></div><script language=\"javascript\">toggle(getObject('ParentLinks_link'), 'ParentLinks');</script>";
							HR3Visible=1;
						}
					}
					else
					{
						ParentTabsHeading.Visible=false;
						ParentTabsFooter.Visible=false;
					}

					#endregion

					#region Sibling Level Tabs

					string SiblingHeading="";
					string SiblingTabHeadingLink="";
					if (portalSettings.ActiveTab.ParentTabId==-1)
					{
						SiblingHeading=portalSettings.PortalName.ToString();
						SiblingTabHeadingLink = ConfigurationSettings.AppSettings["apppath"];
					}
					else
					{
						SiblingHeading = portalSettings.SiblingTabsHeading;
						SiblingTabHeadingLink = "DesktopDefault.aspx?tabid="+portalSettings.SiblingTabsHeadingTabID.ToString();
					}



					ArrayList SiblingauthorisedTabs = new ArrayList();
					int SiblingLeveladdedTabs = 0;

					for (int i=0; i < portalSettings.SiblingTabs.Count; i++) 
					{
            
						TabStripDetails tab = (TabStripDetails)portalSettings.SiblingTabs[i];

						if (Components.PortalSecurity.IsInRoles(tab.AuthorizedRoles) && tab.NavVisible!=-1) 
						{ 
							SiblingauthorisedTabs.Add(tab);
						}

						if (tab.TabId == tabId) 
						{
							SiblingTabs.SelectedIndex = SiblingLeveladdedTabs;
						}

						SiblingLeveladdedTabs++;
					}          

					// Populate Tab List at left of the Page with Sibling tabs
					SiblingTabs.DataSource = SiblingauthorisedTabs;
					SiblingTabs.DataBind();

					if (SiblingauthorisedTabs.Count<=1) //i.e. if there is only the current tab in the list, then hide the headings as the current tab is not displayed, leaving an empty list.
					{
							SiblingTabsHeading.Visible=false;
							SiblingTabsFooter.Visible=false;
							SiblingTabs.Visible = false;
					}

					else
					{
						
						SiblingTabsHeading.Text = "<div><a title=\"show/hide pages at this level\" id=\"SiblingLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'SiblingLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<a href='"+SiblingTabHeadingLink+"'><font class='leftNavDown'>"+SiblingHeading+"</font></a><BR><div id=\"SiblingLinks\">";
						SiblingTabsFooter.Text = "</div></div>";
						HR4Visible=1;
					}
					

					// to toggle the footer to start as collapsed, add this to the end of the SiblingLinksFooter.Text:  <script language=\"javascript\">toggle(getObject('SiblingLinks_link'), 'SiblingLinks');</script>
					#endregion

					#region Child Level Tabs
					ArrayList ChildauthorisedTabs = new ArrayList();
					int ChildLeveladdedTabs = 0;

					for (int i=0; i < portalSettings.ChildTabs.Count; i++) 
					{
            
						TabStripDetails tab = (TabStripDetails)portalSettings.ChildTabs[i];

						if (Components.PortalSecurity.IsInRoles(tab.AuthorizedRoles) && tab.NavVisible!=-1) 
						{ 
							ChildauthorisedTabs.Add(tab);
						}

						ChildLeveladdedTabs++;
					}          

					// Populate Tab List at left of the Page with Child tabs
					ChildTabs.DataSource = ChildauthorisedTabs;
					ChildTabs.DataBind();

					if (ChildauthorisedTabs.Count==0)
					{
						ChildTabsHeading.Visible=false;
						ChildTabsFooter.Visible=false;
						ChildTabs.Visible = false;
					}
					else
					{
						ChildTabsHeading.Text = "<div><a title=\"show/hide pages one level down\" id=\"ChildLinks_link\" href=\"javascript: void(0);\" onclick=\"toggle(this, 'ChildLinks');\"><img border=0 src=\"images/leftnavbar_minus.gif\"></a>&nbsp;<a href='DesktopDefault.aspx?tabid="+portalSettings.ChildTabsHeadingTabID+"'><font class='leftNavDown'>"+portalSettings.ChildTabsHeading+"</font></a><BR><div id=\"ChildLinks\">";
						ChildTabsFooter.Text = "</div></div>";
					}

					// to toggle the footer to start as collapsed, add this to the end of the ChildLinksFooter.Text:     <script language=\"javascript\">toggle(getObject('ChildLinks_link'), 'ChildLinks');</script>

					#endregion






				#region Tab Group Separator Lines
				
				if (HR1Visible==1){HR1.Visible=true;}else{HR1.Visible=false;}
				if (HR2Visible==1){HR2.Visible=true;}else{HR2.Visible=false;}
				if (HR3Visible==1){HR3.Visible=true;}else{HR3.Visible=false;}
				if (HR4Visible==1){HR4.Visible=true;}else{HR4.Visible=false;}




				#endregion
				
			}
        
	
		public DesktopPortalTabs() 
		{
			this.Init += new System.EventHandler(Page_Init);
		}

		protected void Page_Init(object sender, EventArgs e) 
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 	Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
