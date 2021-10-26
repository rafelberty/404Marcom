using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;


namespace ASPNetPortal
{

	/// <summary>
	///	The DesktopPortalFooter.ascx module is for rendering the remainder of the page footer in almost all cases. The majority of the page footer now is done by the standard @hp footer javascripts combined with the page class which calculates the required values for the javascript variables from values in the web.config file.
	/// <para></para>
	/// <para>The functions shown on the footer are:</para>
	/// <list type="bullet">
	/// <item>
	/// <description>Last Updated Date (of page)</description>
	/// </item>
	/// <item>
	/// <description>Page Views</description>
	/// </item>
	/// <item>
	/// <description>Edit, Copy, Create, Delete Page Buttons if in edit mode</description>
	/// </item>
	/// <item>
	/// <description>Year (current year shown in the footer)</description>
	/// </item>
	/// </list>
	/// </summary>
	/// <seealso cref="DesktopDefault"/>
	/// <seealso cref="DesktopPortalTabs"/>
	/// <seealso cref="DesktopPortalHeader"/>
	public partial  class DesktopPortalFooter : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{

			// Determine the querystring of the page calling the footer
			
			try
			{
				if (Request.Params["printable"]=="true" )
				{
					Timestamp.Visible=true;
				}
				else
				{
					Timestamp.Visible=false;
				}
			}
			catch
			{
				Timestamp.Visible=false;
			}


			Timestamp.Text = "Printed by: " + ASPNetPortal.Components.PortalSecurity.UserName+ " at " + DateTime.Now.ToLongTimeString() + ", " + DateTime.Now.ToLongDateString();

			if (!this.IsPostBack )
			{
				
				//Set the year in the footer
				Year.Text = DateTime.Now.Year.ToString();
			}

			//get the last updated date
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			FooterUpdatedDate.Text = portalSettings.ActiveTab.UpdatedDate.ToLongDateString() + " at " +portalSettings.ActiveTab.UpdatedDate.ToShortTimeString();
			FooterPageViews.Text = (portalSettings.ActiveTab.PageViews+1).ToString();
			



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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
