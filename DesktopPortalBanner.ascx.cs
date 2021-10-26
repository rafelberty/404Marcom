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
using System.Text.RegularExpressions;


using ASPNetPortal;


namespace ASPNetPortal 
{
	/// <summary>
	///	The DesktopPortalBanner.ascx module is for rendering the remainder of the page header in almost all cases. The majority of the page header now is done by the standard @hp header javascripts combined with the page class which calculates the required values for the javascript variable either from say the Page Title or from values in the web.config file.
	/// <para></para>
	/// <para>The functions shown on the header are:</para>
	/// <list type="bullet">
	/// <item>
	/// <description>Printable Page</description>
	/// </item>
	/// <item>
	/// <description>Site Map</description>
	/// </item>
	/// <item>
	/// <description>Edit, Copy, Create, Delete Page Buttons if in edit mode</description>
	/// </item>
	/// <item>
	/// <description>File Manager button if in edit mode</description>
	/// </item>
	/// <item>
	/// <description>Breadcrumb navigation is selected</description>
	/// </item>
	/// </list>
	/// </summary>
	/// <seealso cref="DesktopDefault"/>
	/// <seealso cref="DesktopPortalTabs"/>
	/// <seealso cref="DesktopPortalFooter"/>
	public partial  class DesktopPortalBanner : System.Web.UI.UserControl 
	{

		public int tabID;
		public int tabIndex;
		public bool ShowTabs = true;
		protected string targetname;
		
		//BreadcrumbNavigation Elements
		protected string tabtitle;
		protected string tabordervalue;
		protected string tabidvalue;
		protected int navvisible;
		protected string BreadcrumbNavigationString;
		
		protected void Page_Load(object sender, System.EventArgs e) 
		{

			PrintablePage.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/functions_printable.gif";
			EditThisPageLayout.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/"+ConfigurationSettings.AppSettings["language_direction"]+"editthispagelayout.gif";
			CopyThisPage.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/"+ConfigurationSettings.AppSettings["language_direction"]+"copythispage.gif";
			CreateToplevelPage.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/"+ConfigurationSettings.AppSettings["language_direction"]+"createtoplevelpage.gif";
			CreateSiblingPage.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/"+ConfigurationSettings.AppSettings["language_direction"]+"createsiblingpage.gif";
			CreateChildPage.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/"+ConfigurationSettings.AppSettings["language_direction"]+"createchildpage.gif";
			DeleteThisPage.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/"+ConfigurationSettings.AppSettings["language_direction"]+"deletethispage.gif";
			FileManager.ImageUrl= ConfigurationSettings.AppSettings["apppath"]+"images/FileManager.gif";


			if (Page.IsPostBack)
			{
				EditThisPageLayout.Visible = false; 
				CopyThisPage.Visible = false;
				CreateToplevelPage.Visible = false;
				CreateSiblingPage.Visible = false;
				CreateChildPage.Visible = false;
				DeleteThisPage.Visible = false;
				FileManager.Visible = false;
			}



			
			if (this.Page.IsPostBack==false)
			{
				ModeSwitch.Visible=false;
			//	LogInLink.Text="";
			}





			string apppath=(ConfigurationSettings.AppSettings["apppath"]);

			// Ensure Page Edit Functionality visibility set to false on page load
			EditThisPageLayout.Visible=false;
			CopyThisPage.Visible=false;
			DeleteThisPage.Visible=false;
			CreateToplevelPage.Visible=false;
			CreateSiblingPage.Visible=false;
			CreateChildPage.Visible=false;
			FileManager.Visible = false;


			// Determine the page calling the banner
			string pageurl = Request.Path.ToString();

			if (pageurl.EndsWith("DesktopDefault.aspx") == false)
			{
				ModeSwitch.Visible=false;
			//	LogInLink.Text="";
			}

			// Render the banner correctly
			if (pageurl.EndsWith("Logon.aspx") == true )
			{
				PrintablePage.Visible=false;
				FileManager.Visible = false;
				
			}
			else if(pageurl.EndsWith("DesktopDefault.aspx") == true || pageurl.EndsWith("SiteMap.aspx") == true )
			{
				PrintablePage.Visible=true;
			}
			else
			{
				PrintablePage.Visible=false;
			}
			
			// Add javascript functionality to buttons (printable page and page delete)
			PrintablePage.Attributes.Add ("onclick", "javascript:window.open ('"+ Request.RawUrl+"&printable=true', 'PrintablePage');");
			DeleteThisPage.Attributes.Add("onClick","javascript: return confirm('DELETE PAGE - Are you sure?. Remember, all child pages will be orphaned and require assigning of new parents before they are again visible in the portal');");
			string FileManagerWindowDetails = "window.open('" + ConfigurationSettings.AppSettings["file_browser"]+"', 'ImageManager','toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=650,height=500,left=20,top=20'); return false;";
			FileManager.Attributes.Add("onclick", FileManagerWindowDetails);  	
			
			
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			
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

			//PageTitle.Text = " ";
			int tabID = portalSettings.ActiveTab.TabId;
			int tabIndex = portalSettings.ActiveTab.TabIndex;
			string tabIDstring = portalSettings.ActiveTab.TabId.ToString();			
			

			// Check if user is in editing mode, if applicable
			Components.UsersDB user = new ASPNetPortal.Components.UsersDB();
			int Editing=0;
            if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated && ASPNetPortal.Components.PortalSecurity.UserName != null && ASPNetPortal.Components.PortalSecurity.UserName != "")
			{
				try{Editing = user.GetUserEditingStatus(ASPNetPortal.Components.PortalSecurity.UserName);}catch{}
			}

			




		// Check the authentication method
			// Windows Authentication
			if (Context.User.Identity.AuthenticationType != "Forms") 
			{
				//if authenticated
                if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated) 
				{
                    //WelcomeMessage.Text = "Welcome " + ASPNetPortal.Components.PortalSecurity.UserFullName + "! <" + "span class=Accent" + "><" + "/span" + ">";
					if (this.Page.IsPostBack==false  && pageurl.EndsWith("DesktopDefault.aspx") == true)
					{
						Components.UsersDB usercheck = new ASPNetPortal.Components.UsersDB();
						SqlDataReader dr = usercheck.GetSingleUser(ASPNetPortal.Components.PortalSecurity.UserName);
						
						if (dr.Read() && Components.PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedEditRoles) == true) // i.e. the user exists in the database and is authorised to edit the page.
						{
							ModeSwitch.Visible=true;
							ModeSwitch.AlternateText="Click to switch between Browse and Edit Modes if you are authorised";
						}
						dr.Close();

					}
					
					//if in edit mode
					if (Editing==1)
					{
					//	LogInLink.Text="";
						ModeSwitch.ImageUrl = ConfigurationSettings.AppSettings["apppath"]+"images/SwitchBrowse.gif";
						ModeSwitch.AlternateText="Click to switch to Browse Mode";
				

						if (Components.PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedEditRoles) == true  &&  pageurl.EndsWith("DesktopDefault.aspx") == true) 
						{
							EditThisPageLayout.Visible=true;
							CopyThisPage.Visible=true;
							DeleteThisPage.Visible=true;
							CreateToplevelPage.Visible=true;
							CreateSiblingPage.Visible=true;
							CreateChildPage.Visible=true;
							FileManager.Visible = true;
						}
						else
						{
							EditThisPageLayout.Visible=false;
							CopyThisPage.Visible=false;
							DeleteThisPage.Visible=false;
							CreateToplevelPage.Visible=false;
							CreateSiblingPage.Visible=false;
							CreateChildPage.Visible=false;
							FileManager.Visible = false;
						}
					
					}
					else
					{
					//	LogInLink.Text="";
						ModeSwitch.ImageUrl = ConfigurationSettings.AppSettings["apppath"]+"images/SwitchEdit.gif";
						ModeSwitch.AlternateText="Click to switch to Edit Mode if you are authorised";
						EditThisPageLayout.Visible=false;
						CopyThisPage.Visible=false;
						DeleteThisPage.Visible=false;
						CreateToplevelPage.Visible=false;
						CreateSiblingPage.Visible=false;
						CreateChildPage.Visible=false;
						FileManager.Visible = false;
					
					}
				
				}
				else
				{
                    //WelcomeMessage.Text = "Welcome " + ASPNetPortal.Components.PortalSecurity.UserFullName + "! <" + "span class=Accent" + "><" + "/span" + ">";
					ModeSwitch.Visible=false;
					EditThisPageLayout.Visible=false;
					CopyThisPage.Visible=false;
					DeleteThisPage.Visible=false;
					CreateToplevelPage.Visible=false;
					CreateSiblingPage.Visible=false;
					CreateChildPage.Visible=false;
					FileManager.Visible = false;
				}
					
			}

			// Forms Authentication
			else if (Context.User.Identity.AuthenticationType == "Forms")
			{

				//if authenticated (and in edit mode)
                if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated) 
				{
                    //WelcomeMessage.Text = "Welcome " + ASPNetPortal.Components.PortalSecurity.UserFullName + "! <" + "span class=Accent" + "><" + "/span" + ">";

					//LogInLink.Text = "<" + "span class=\"Accent\"></span>\n" + "<" + "a href='" + Request.ApplicationPath + "/Admin/Logoff.aspx?tabid="+tabID.ToString()+"&tabindex="+tabIndex.ToString()+"' class=Logoff><img src='images/arrow_log_off.gif' border=0 alt='log off'>" + "<" + "/a>";

					if (Components.PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedEditRoles) == true  &&  pageurl.EndsWith("DesktopDefault.aspx") == true) 
					{
						EditThisPageLayout.Visible=true;
						CopyThisPage.Visible=true;
						DeleteThisPage.Visible=true;
						CreateToplevelPage.Visible=true;
						CreateSiblingPage.Visible=true;
						CreateChildPage.Visible=true;
						FileManager.Visible = true;
					}
					else
					{
						EditThisPageLayout.Visible=false;
						CopyThisPage.Visible=false;
						DeleteThisPage.Visible=false;
						CreateToplevelPage.Visible=false;
						CreateSiblingPage.Visible=false;
						CreateChildPage.Visible=false;
						FileManager.Visible = false;
					}

				}
				else
				{
					//LogInLink.Text = "<" + "span class=\"Accent\"></span>\n" + "<" + "a href='" + Request.ApplicationPath + "/Admin/Logon.aspx?tabid="+tabID.ToString()+"&tabindex="+tabIndex.ToString()+"' class=Logoff><img src='images/arrow_log_on.gif' border=0 alt='log on'>" + "<" + "/a>";
					EditThisPageLayout.Visible=false;
					CopyThisPage.Visible=false;
					DeleteThisPage.Visible=false;
					CreateToplevelPage.Visible=false;
					CreateSiblingPage.Visible=false;
					CreateChildPage.Visible=false;
					FileManager.Visible = false;
				}


			}


			if (tabID!=0)
			{
				// Create Instance of Connection and Command Object
				SqlConnection myConnection = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);
				SqlCommand myCommand = new SqlCommand("GetTabTitle", myConnection);

				// Mark the Command as a SPROC
				myCommand.CommandType = CommandType.StoredProcedure;

				// Add Parameters to SPROC
				SqlParameter parameterTabID = new SqlParameter("@TabID", SqlDbType.Int, 4);
				parameterTabID.Value = tabID;
				myCommand.Parameters.Add(parameterTabID);

				// Execute the command, return the result and tidy up
				myConnection.Open();
				String result = (String)myCommand.ExecuteScalar();
				//PageTitle.Text = result;
				myConnection.Close();
		
			}
			else
			{
				//PageTitle.Text = ".";
				//PageTitle.ForeColor = Color.White;
				//PageTitle.Font.Size = 3;
			}
			

			//Breadcrumb Navigation
			if (this.Page.IsPostBack==false && ConfigurationSettings.AppSettings["Breadcrumb_Navigation"]=="on" && pageurl.EndsWith("DesktopDefault.aspx") == true)
			{
				ASPNetPortal.Database.TabsDB Breadcrumb = new ASPNetPortal.Database.TabsDB();
				SqlDataReader dr = Breadcrumb.GetParentTabDetails(tabID);
				while (dr.Read())
				{
					try{tabtitle = (String) dr["TabName"];}
					catch {tabtitle="";}
					try{tabordervalue = Convert.ToString((int) dr["taborder"]);}
					catch {tabordervalue="";}
					try{tabidvalue = Convert.ToString((int) dr["tabid"]);}
					catch {tabidvalue="";}
					try{navvisible = ((int) dr["NavVisible"]);}
					catch {navvisible=0;}
					if(navvisible!=-1)
					{
						BreadcrumbNavigationString = "<a href=\'DesktopDefault.aspx?tabid=" + tabidvalue + "&taborder=" + tabordervalue + "\'>"+tabtitle+"</A> " + BreadcrumbNavigationString;
					}
					if ((int) dr["taborder"]!=-1)
					{
						dr = Breadcrumb.GetParentTabDetails((int) dr["TabID"]);
						while (dr.Read())
						{
							try{tabtitle = (String) dr["TabName"];}
							catch {tabtitle="";}
							try{tabordervalue = Convert.ToString((int) dr["taborder"]);}
							catch {tabordervalue="";}
							try{tabidvalue = Convert.ToString((int) dr["tabid"]);}
							catch {tabidvalue="";}
							try{navvisible = ((int) dr["NavVisible"]);}
							catch {navvisible=0;}
							if(navvisible!=-1)
							{
								BreadcrumbNavigationString = "<a href=\'DesktopDefault.aspx?tabid=" + tabidvalue + "&taborder=" + tabordervalue + "\'>"+tabtitle+"</A> > " + BreadcrumbNavigationString;
							}
							if ((int) dr["ParentTabID"]!=-1)
							{
								dr = Breadcrumb.GetParentTabDetails((int) dr["TabID"]);
								while (dr.Read())
								{
									try{tabtitle = (String) dr["TabName"];}
									catch {tabtitle="";}
									try{tabordervalue = Convert.ToString((int) dr["taborder"]);}
									catch {tabordervalue="";}
									try{tabidvalue = Convert.ToString((int) dr["tabid"]);}
									catch {tabidvalue="";}
									try{navvisible = ((int) dr["NavVisible"]);}
									catch {navvisible=0;}
									if(navvisible!=-1)
									{
										BreadcrumbNavigationString = "<a href=\'DesktopDefault.aspx?tabid=" + tabidvalue + "&taborder=" + tabordervalue + "\'>"+tabtitle+"</A> > " + BreadcrumbNavigationString;
									}
									if ((int) dr["ParentTabID"]!=-1)
									{
										dr = Breadcrumb.GetParentTabDetails((int) dr["TabID"]);
										while (dr.Read())
										{
											try{tabtitle = (String) dr["TabName"];}
											catch {tabtitle="";}
											try{tabordervalue = Convert.ToString((int) dr["taborder"]);}
											catch {tabordervalue="";}
											try{tabidvalue = Convert.ToString((int) dr["tabid"]);}
											catch {tabidvalue="";}
											try{navvisible = ((int) dr["NavVisible"]);}
											catch {navvisible=0;}
											if(navvisible!=-1)
											{
												BreadcrumbNavigationString = "<a href=\'DesktopDefault.aspx?tabid=" + tabidvalue + "&taborder=" + tabordervalue + "\'>"+tabtitle+"</A> > " + BreadcrumbNavigationString;
											}
											if ((int) dr["ParentTabID"]!=-1)
											{
												dr = Breadcrumb.GetParentTabDetails((int) dr["TabID"]);
												while (dr.Read())
												{
													try{tabtitle = (String) dr["TabName"];}
													catch {tabtitle="";}
													try{tabordervalue = Convert.ToString((int) dr["taborder"]);}
													catch {tabordervalue="";}
													try{tabidvalue = Convert.ToString((int) dr["tabid"]);}
													catch {tabidvalue="";}
													BreadcrumbNavigationString = "<a href=\'DesktopDefault.aspx?tabid=" + tabidvalue + "&taborder=" + tabordervalue + "\'>"+tabtitle+"</A> > " + BreadcrumbNavigationString;
													if ((int) dr["ParentTabID"]!=-1)
													{
														BreadcrumbNavigationString = "... > " + BreadcrumbNavigationString;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					BreadcrumbNavigationLabel.Text = "<a href=\'"+ConfigurationSettings.AppSettings["relativeapppath"] + "\'>"+portalSettings.PortalName.ToString()+"</A> > " +BreadcrumbNavigationString;
					

				}
				dr.Close();
			}
			
		}

	
		// Disabled since this siteName is now handled within the @hp header javascript.		
		//protected void siteName_Click(Object sender, EventArgs e)
		//{
		//	// Redirect back to the portal home page
		//	Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"Default.aspx");
		//}

		private void EditThisPageLayout_Click(Object sender, ImageClickEventArgs e) 
		{
			if (Request.Params["tabid"]!=null) 
			{
				
				// Obtain PortalSettings from Current Context
				PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];
				int tabID = portalSettings.ActiveTab.TabId;
				int tabIndex = portalSettings.ActiveTab.TabIndex;
				
				Response.Redirect("~/Admin/TabLayout.aspx?tabid=" + tabID + "&patid=" + tabID + "&patindex=" + tabIndex);
			}
		}

		private void CopyThisPage_Click(Object sender, ImageClickEventArgs e) 
		{
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

			Database.AdminDB admin = new Database.AdminDB();
			admin.CopyTab(portalSettings.ActiveTab.TabId, ASPNetPortal.Components.PortalSecurity.UserName);


			SqlDataReader dr = admin.GetLatestTab(portalSettings.ActiveTab.TabId, "sibling", ASPNetPortal.Components.PortalSecurity.UserName);
			
			int newsiblingtabid;
			if (dr.Read()) // i.e. the new tab exists in the database.
			{
				newsiblingtabid = (int) dr["TabID"];
				// Redirect to this site to refresh
				Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"DesktopDefault.aspx?tabid="+ newsiblingtabid.ToString());

			}
			dr.Close();


			// Redirect to this site to refresh
			//Response.Redirect(Request.RawUrl);
			
		}
		private void DeleteThisPage_Click(Object sender, ImageClickEventArgs e) 
		{
			if (Request.Params["tabid"]!=null) 
			{
				
				// Obtain PortalSettings from Current Context
				PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

				int parenttabid = portalSettings.ActiveTab.ParentTabId;
				
				int tabID = Int32.Parse(Request.Params["tabid"]);
				
				// Delete page from database
				if (tabID!=1) //cannot delete the home page
				{
					Database.AdminDB admin = new Database.AdminDB();
					admin.DeleteTab(tabID);
				}
                        
				if (parenttabid!=-1)
				{
					// Redirect to the parent page
					Response.Redirect("~/DesktopDefault.aspx?tabindex=0&tabid=" + parenttabid.ToString());
				}
				else
				{
					// Redirect to this site to refresh
					Response.Redirect(Request.ApplicationPath);
				}
			}
		}

		private void CreateToplevelPage_Click(Object sender, ImageClickEventArgs e) 
		{
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

			string timestampedname = "NewPage-"+DateTime.Now.Day.ToString()+"/"+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString()+"-"+DateTime.Now.ToLongTimeString(); 


			// write new tab to database
			Database.AdminDB admin = new Database.AdminDB();
			//t.TabId = admin.AddTab(portalSettings.PortalId, "New Page");
			admin.AddTab(portalSettings.PortalId, timestampedname, ASPNetPortal.Components.PortalSecurity.UserName, ConfigurationSettings.AppSettings["DefaultCountryCode"],ConfigurationSettings.AppSettings["DefaultLanguageCode"],ConfigurationSettings.AppSettings["DefaultLocaleCode"],ConfigurationSettings.AppSettings["language_direction"],"","");

			SqlDataReader dr = admin.GetLatestTab(portalSettings.ActiveTab.TabId, "top", ASPNetPortal.Components.PortalSecurity.UserName);
			
			int newtopleveltabid;
			if (dr.Read()) // i.e. the new tab exists in the database.
			{
				newtopleveltabid = (int) dr["TabID"];
				// Redirect to this site to refresh
				Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"DesktopDefault.aspx?tabid="+ newtopleveltabid.ToString());

			}
			
			dr.Close();
			
		}
		private void CreateSiblingPage_Click(Object sender, ImageClickEventArgs e) 
		{
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

			// New tabs go to the end of the list
			Database.TabItem t = new Database.TabItem();
			t.TabId = portalSettings.ActiveTab.TabId;
			
			// write tab to database
			Database.AdminDB admin = new Database.AdminDB();
			admin.AddSiblingTab(t.TabId, ASPNetPortal.Components.PortalSecurity.UserName);

			SqlDataReader dr = admin.GetLatestTab(portalSettings.ActiveTab.TabId, "sibling", ASPNetPortal.Components.PortalSecurity.UserName);
			
			int newsiblingtabid;
			if (dr.Read()) // i.e. the new tab exists in the database.
			{
				newsiblingtabid = (int) dr["TabID"];
				// Redirect to this site to refresh
				Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"DesktopDefault.aspx?tabid="+ newsiblingtabid.ToString());

			}
			
			dr.Close();
			
		}
		private void CreateChildPage_Click(Object sender, ImageClickEventArgs e) 
		{
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

			// New tabs go to the end of the list
			Database.TabItem t = new Database.TabItem();
			t.TabId = portalSettings.ActiveTab.TabId;
			//portalTabs.Add(t);

			// write tab to database
			Database.AdminDB admin = new Database.AdminDB();
			//t.TabId = admin.AddChildTab(portalSettings.PortalId, t.TabId, t.TabName);
			admin.AddChildTab(t.TabId, ASPNetPortal.Components.PortalSecurity.UserName);

			SqlDataReader dr = admin.GetLatestTab(portalSettings.ActiveTab.TabId, "child", ASPNetPortal.Components.PortalSecurity.UserName);
			

			int newchildtabid;
			if (dr.Read()) // i.e. the new tab exists in the database.
			{
				newchildtabid = (int) dr["TabID"];
				// Redirect to this site to refresh
				Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"DesktopDefault.aspx?tabid="+ newchildtabid.ToString());

			}
			
			dr.Close();
			
		}
		
		private void ModeSwitch_Click(Object sender, ImageClickEventArgs e) 
		{
			
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

			// Determine the page calling the banner
			string pagepath = Request.Path.ToString();
			string pageurl = Request.Path.ToString()+ "?" + Request.QueryString.ToString();


			// Check if user is in editing mode, if applicable
			Components.UsersDB usermode = new ASPNetPortal.Components.UsersDB();
			int UserMode=0;
            if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated && ASPNetPortal.Components.PortalSecurity.UserName != null && ASPNetPortal.Components.PortalSecurity.UserName != "")
			{
				try{UserMode = usermode.GetUserEditingStatus(ASPNetPortal.Components.PortalSecurity.UserName);}
				catch{}
			}

			
			if (UserMode == 1)  
			{
				//Switch to Browsing
				//LogInLink.Text="";
				usermode.SetUserBrowsing(ASPNetPortal.Components.PortalSecurity.UserName);
				ModeSwitch.ImageUrl = ModeSwitch.ImageUrl = ConfigurationSettings.AppSettings["apppath"]+"images/SwitchEdit.gif";
				ModeSwitch.AlternateText="Click to switch to Edit Mode if you are authorised";
				EditThisPageLayout.Visible=false;
				CopyThisPage.Visible=false;
				DeleteThisPage.Visible=false;
				CreateToplevelPage.Visible=false;
				CreateSiblingPage.Visible=false;
				CreateChildPage.Visible=false;
				FileManager.Visible = false;
			}


			else if (UserMode != 1 && Components.PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedEditRoles) == true  &&  pagepath.EndsWith("DesktopDefault.aspx") == true)  
			{
				//Switch to Editing
				//LogInLink.Text="";
				usermode.SetUserEditing(ASPNetPortal.Components.PortalSecurity.UserName);
				ModeSwitch.ImageUrl = ModeSwitch.ImageUrl = ConfigurationSettings.AppSettings["apppath"]+"images/SwitchBrowse.gif";
				ModeSwitch.AlternateText="Click to switch to Browse Mode";
				EditThisPageLayout.Visible=true;
				CopyThisPage.Visible=true;
				DeleteThisPage.Visible=true;
				CreateToplevelPage.Visible=true;
				CreateSiblingPage.Visible=true;
				CreateChildPage.Visible=true;
				FileManager.Visible = false;
			
			}
			Response.Redirect(pageurl);
		}


		private void FileManager_Click(Object sender, ImageClickEventArgs e) 
		{
		}


        public DesktopPortalBanner() 
		{
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Init(object sender, EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

		#region Web Form Designer generated code
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.EditThisPageLayout.Click += new System.Web.UI.ImageClickEventHandler(this.EditThisPageLayout_Click);
			this.CopyThisPage.Click += new System.Web.UI.ImageClickEventHandler(this.CopyThisPage_Click);
			this.CreateToplevelPage.Click += new System.Web.UI.ImageClickEventHandler(this.CreateToplevelPage_Click);
			this.CreateSiblingPage.Click += new System.Web.UI.ImageClickEventHandler(this.CreateSiblingPage_Click);
			this.CreateChildPage.Click += new System.Web.UI.ImageClickEventHandler(this.CreateChildPage_Click);
			this.DeleteThisPage.Click += new System.Web.UI.ImageClickEventHandler(this.DeleteThisPage_Click);
			this.ModeSwitch.Click += new System.Web.UI.ImageClickEventHandler(this.ModeSwitch_Click);
			this.FileManager.Click += new System.Web.UI.ImageClickEventHandler(this.FileManager_Click);
		}
		#endregion
    }
}
