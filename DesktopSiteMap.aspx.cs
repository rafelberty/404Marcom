using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using System.Configuration;

namespace ASPNetPortal
{
	/// <summary>
	///	The DesktopSiteMap.aspx page is for rendering an automated version of the site map, or in other words a hierarchical view of the site pages.
	/// <para></para>
	/// <para>The site map is not dynamically calculated but retrieved from a special hierarchical tabs table, which is updated only when their are changes to the page navigation structure through the addtion or deletion of pages, or through the reorganisation of pages. Only those pages which have not been flagged as "Hidden From Navigation" will have their links displayed</para>
	/// </summary>
	public partial class DesktopSiteMap : System.Web.UI.Page
	{
		public int tabIndex;
		public bool ShowTabs = true;

		protected string TopLevelLinks_js;

		
		protected string HeaderSetBanner;
		protected string HeaderDisplayLogin;
		protected string HeaderSetSiteSearch;
		protected string HeaderSetWarningMessage;
		protected string HeaderSetGreetingMessage;
		protected string HeaderDrawHeader;

		protected string FooterSetVersionNumber;
		protected string FooterSetLocalSupportPage;
		protected string FooterSetFooterFeedbackPage;
		protected string FooterSetPageConfidential;
		protected string FooterSetPagePublic;
		protected string FooterSetTargetFrame;
		protected string FooterDrawSeparator;
		protected string FooterDrawFooter;
			
	    
		public DesktopSiteMap() 
		{
			Page.Init += new System.EventHandler(Page_Init);
		}
		
		protected void Page_Init(object sender, EventArgs e) 
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();

			//*********************************************************************
			//
			// Page_Init Event Handler
			//
			// The Page_Init event handler executes at the very beginning of each page
			// request (immediately before Page_Load).
			//
			// The Page_Init event handler below determines the tab index of the currently
			// requested portal view, and then calls the PopulatePortalSection utility
			// method to dynamically populate the left, center and right hand sections
			// of the portal tab.
			//
			//*********************************************************************

			// Obtain PortalSettings from Current Context
			PortalSettings ps = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			// Dynamically Populate the Portal Site Name
			siteName.TagName = "title";
			siteName.InnerText = ps.PortalName + " - Site Map";

			// Dynamically Populate the Portal Top Level Links javascript
			TopLevelLinks_js = ps.GlobalLinksUrl.ToString();

		}



		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			ArrayList AuthorisedTabs = new ArrayList();
			
			portalSettings.GetVisibleHierarchicalTabsTable();
			for (int i=0; i < portalSettings.HierarchicalTabs.Count; i++) 
			{
				TabStripDetails tab = (TabStripDetails)portalSettings.HierarchicalTabs[i];
				if (Components.PortalSecurity.IsInRoles(tab.AuthorizedRoles) && tab.TabName.StartsWith("Orphan")!=true) 
				{ 
					Database.TabItem t = new Database.TabItem();
					t.TabName = tab.TabName.Replace("-","&nbsp;&nbsp;&nbsp;");
					t.TabId = tab.TabId;
					t.AuthorizedRoles = tab.AuthorizedRoles;
				
					AuthorisedTabs.Add(t);
				}
			} 
         
			// Populate the Sitemap Datalist
			AllTabs.DataSource = AuthorisedTabs;
			AllTabs.DataBind();		

			// Obtain PortalSettings from Current Context
			PortalSettings ps = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

			//Calculate the Header Draw Function Values
			HeaderSetBanner = "setBanner(\""+ps.PortalName.ToString() +"<br>Site Map\");";
			HeaderDisplayLogin=ConfigurationSettings.AppSettings["HeaderDisplayLogin"];
			HeaderSetSiteSearch=ConfigurationSettings.AppSettings["HeaderSetSiteSearch"];
			HeaderSetWarningMessage="setWarningMessage('"+ps.WarningMessage+"')";
			if(ConfigurationSettings.AppSettings["HeaderSetGreetingMessage"]=="")
			{
                HeaderSetGreetingMessage = "setGreetingMessage('Welcome &nbsp;" + ASPNetPortal.Components.PortalSecurity.UserFullName + "');";
			}
			else
			{
				HeaderSetGreetingMessage=ConfigurationSettings.AppSettings["HeaderSetGreetingMessage"];
			}
			// Determine the page calling the banner
			string pageurl = Request.Path.ToString();

			if (pageurl.EndsWith("DesktopDefault.aspx") == false)
			{
				HeaderDrawHeader="";
			}

			// Render the banner correctly
			if (pageurl.EndsWith("Logon.aspx") == true )
			{
				HeaderDrawHeader="drawHeader()";
			}
			else if(pageurl.EndsWith("DesktopDefault.aspx") == true || pageurl.EndsWith("SiteMap.aspx") == true )
			{
				HeaderDrawHeader="drawHeader()";
			}
			else
			{
				HeaderDrawHeader="";
			}

			// If page is printable, don't draw header
			try
			{
				if (Request.Params["printable"]!="true")
				{
					HeaderDrawHeader="drawHeader()";
				}
				else
				{
					HeaderDrawHeader="";
				}
			}
			catch{HeaderDrawHeader="drawHeader()";}

			//Calculate the Footer Draw Function Values
			FooterSetFooterFeedbackPage=ConfigurationSettings.AppSettings["FooterSetFooterFeedbackPage"];
			FooterSetLocalSupportPage=ConfigurationSettings.AppSettings["FooterSetLocalSupportPage"];
			FooterSetPageConfidential=ConfigurationSettings.AppSettings["FooterSetPageConfidential"];
			FooterSetPagePublic=ConfigurationSettings.AppSettings["FooterSetPagePublic"];
			FooterSetVersionNumber=ConfigurationSettings.AppSettings["FooterSetVersionNumber"];
			FooterSetTargetFrame=ConfigurationSettings.AppSettings["FooterSetTargetFrame"];
			FooterDrawSeparator=ConfigurationSettings.AppSettings["FooterDrawSeparator"];
			FooterDrawFooter="drawFooter()";
			


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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

	}
}
