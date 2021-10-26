using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using System.Configuration;

namespace ASPNetPortal 
{
	/// <summary>This class handles functionality associated with the DesktopDefault.aspx page.
	/// <para>This page renders the majority of page views of the portal.</para>
	/// <para></para>
	/// <para>It is composed as follows:</para>
	/// <list type="bullet">
	/// <item>
	/// <description>A header, which has been customised to utilise the @hp header javascripts which render the standard @hp header, with all its associated
	/// javascript variables to address such things as page title, warning message, inclusion of site specific search etc. Each of this javascript variables has corresponding .NET 
	/// variables included in the application code and as utilised in the Page_Load method of almost every portal page.</description>
	/// </item>
	/// <item>
	/// <description>A dynamic left navigation area, composed of user defined links in the top part and automatically generated links in the lower part, based on the 
	/// hierarchical page relationshp (parent page and child page).</description>
	/// </item>
	/// <item>
	/// <description>A main body area composed of a 3x3 table structure. The modules are then placed within one of the 3x3 tables within the page, and ordered relative to eachother. Management of this is via the <see cref="TabLayout"/> page</description>
	/// </item>
	/// <item>
	/// <description>A footer, which has been customised to utilise the @hp footer javascripts which render the standard @hp footer, with all its associated
	/// javascript variables to address such things as footer, support and confidentialty levels etc. Each of this javascript variables has corresponding .NET 
	/// variables included in the application code and as utilised in the Page_Load method of almost every portal page.</description>
	/// </item>
	/// </list>
	/// <para></para>
	/// </summary>
	/// <seealso cref="Tabs"/>
	/// <seealso cref="TabLayout"/>
	/// <seealso cref="DesktopPortalBanner"/>
	/// <seealso cref="DesktopPortalHeader"/>
	/// <seealso cref="DesktopPortalTabs"/>
	/// <seealso cref="DesktopPortalFooter"/>
    public partial class IDesktopDefault : System.Web.UI.Page 
	{

		
		protected string TopLevelLinks_js;
		protected string childpagekeywords;
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
        //protected System.Web.UI.HtmlControls.HtmlGenericControl _body;
			
	    
        public IDesktopDefault() {
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
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

			// Obtain PortalSettings from Current Context
			PortalSettings ps = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			// Dynamically Populate the Portal Site Name
			PageHTMLTag.Attributes.Add("dir", ps.ActiveTab.LanguageDirection);
			siteName.TagName = "title";
			siteName.InnerText = ps.PortalName;

			//Calculate the Header Draw Function Values
			HeaderSetBanner = "setBanner(\""+ps.PortalName.ToString() +"<br>"+ ps.ActiveTab.TabName.ToString()+"\");";
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



			//Dynamically populate the page meta data
			description.TagName = "meta";
			description.Attributes.Add("name","description");
			description.Attributes.Add("content",ps.PortalName + " - " + ps.ActiveTab.TabName.ToString() + ", " + ps.ActiveTab.Description);

			keywords.TagName = "meta";
			keywords.Attributes.Add("name","keywords");
			//get page titles as keywords
			for (int i=0; i < ps.SiblingTabs.Count; i++) 
			{
				TabStripDetails tab = (TabStripDetails)ps.SiblingTabs[i];
				childpagekeywords += tab.TabName.ToString()+", ";
			} 
			for (int i=0; i < ps.ChildTabs.Count; i++) 
			{
				TabStripDetails tab = (TabStripDetails)ps.ChildTabs[i];
				childpagekeywords += tab.TabName.ToString()+", ";
			} 
			keywords.Attributes.Add("content",ps.PortalName + ", " + childpagekeywords + ","  + ConfigurationSettings.AppSettings["Meta_Keywords"] + ", " + ps.ActiveTab.Keywords);
			
			try
			{
			ATHP_Date_Created.TagName = "meta";
			ATHP_Date_Created.Attributes.Add("name","ATHP_Date_Created");
			ATHP_Date_Created.Attributes.Add("content",ps.ActiveTab.CreatedDate.ToShortDateString());
			}
			catch{}

			try
			{
			ATHP_Date_Modified.TagName = "meta";
			ATHP_Date_Modified.Attributes.Add("name","ATHP_Date_Modified");
			ATHP_Date_Modified.Attributes.Add("content",ps.ActiveTab.UpdatedDate.ToShortDateString());
			}
			catch{}
			try
			{
			ATHP_Country.TagName = "meta";
			ATHP_Country.Attributes.Add("name","ATHP_Country");
			ATHP_Country.Attributes.Add("content",ps.ActiveTab.CountryCode.ToString());
			}
			catch{}
			try
			{
			ATHP_Language.TagName = "meta";
			ATHP_Language.Attributes.Add("name","ATHP_Language");
			ATHP_Language.Attributes.Add("content",ps.ActiveTab.LanguageCode.ToString());
			}
			catch{}
			try
			{
			Locale.TagName = "meta";
			Locale.Attributes.Add("name","Locale");
			Locale.Attributes.Add("content",ps.ActiveTab.LocaleCode.ToString());
			}
			catch{}
			

			// Dynamically Populate the Portal Top Level Links javascript
			TopLevelLinks_js = ps.GlobalLinksUrl.ToString();

				int tabID = portalSettings.ActiveTab.TabId;
				string tabIDstring = portalSettings.ActiveTab.TabId.ToString();			
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
					siteName.InnerText = siteName.InnerText + " - " + result;
					myConnection.Close();
	
				}


				// Ensure that the first time visit is to the home page
				if (portalSettings.ActiveTab.TabId <= 0) 
				{
					Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"DesktopDefault.aspx?tabid=1&tabindex=0");
				}
			
			        
				// Ensure that the visiting user has access to the current page
				if (Components.PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles) == false) 
				{
					Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"Admin/AccessDenied.aspx");
				}

				// Dynamically Populate the Left, Center and Right pane sections of the portal page
				if (portalSettings.ActiveTab.Modules.Count > 0) 
				{

					// Loop through each entry in the configuration system for this tab
					foreach (ModuleSettings _moduleSettings in portalSettings.ActiveTab.Modules) 
					{
                
						Control parent = Page.FindControl(_moduleSettings.PaneName);

						// If no caching is specified, create the user control instance and dynamically
						// inject it into the page.  Otherwise, create a cached module instance that
						// may or may not optionally inject the module into the tree

						if ((_moduleSettings.CacheTime) == 0) 
						{

							PortalModuleControl portalModule = (PortalModuleControl) Page.LoadControl(_moduleSettings.DesktopSrc);
                   
							portalModule.PortalId = portalSettings.PortalId;                                  
							portalModule.ModuleConfiguration = _moduleSettings;
                   
							parent.Controls.Add(portalModule);
						}
						else 
						{

							CachedPortalModuleControl portalModule = new CachedPortalModuleControl();
                   
							portalModule.PortalId = portalSettings.PortalId;                                 
							portalModule.ModuleConfiguration = _moduleSettings;
 
							parent.Controls.Add(portalModule);
						}

						// Dynamically inject separator break between portal modules
						parent.Controls.Add(new LiteralControl("<" + "br" + ">"));
						parent.Visible = true;
					}
				}            
			FooterUpdatedDate.Text = portalSettings.ActiveTab.UpdatedDate.ToShortTimeString()+", "+portalSettings.ActiveTab.UpdatedDate.ToLongDateString();
			FooterPageViews.Text = (portalSettings.ActiveTab.PageViews+1).ToString();
		
			// update the LastViewed Information.
			ASPNetPortal.Database.TabsDB LastViewed = new ASPNetPortal.Database.TabsDB();
			LastViewed.UpdateTabViewed (tabID, ASPNetPortal.Components.PortalSecurity.UserName.ToString());
			}


		
		#region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {    

		}
		#endregion

    }    
}

