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
	///	The DeskTopModuleInvisibleTitle.ascx module is for rendering an "invisible" header at the top of each module which includes it. Examples include: <see cref="PureImage"/>, <see cref="PureWYSIWYGModule"/>.
	/// <para></para>
	/// <para>The need for such a module is that although for the user, a header may not appear necessary, for a content editor the header is still required for two things:</para>
	/// <list type="bullet">
	/// <item>
	/// <description>The ability to surface the link to add addtional items to modules containing lists of data, e.g. news or documents</description>
	/// </item>
	/// <item>
	/// <description>The ability to render the Edit and <see cref="EditModuleColor"/>/Edit Module properties features</description>
	/// </item>
	/// </list>
	/// <para>Therefore this header functionality is still required. The result is that if the content editor is browsing in Edit mode then the feature are visible, otherwise nothing is visible in the header area. </para>
	/// <para>For other modules, a standard visible header is shown. See <see cref="DesktopModuleTitle"/>. </para>
	/// </summary>
	/// <seealso cref="DesktopModuleTitle"/>
	/// <seealso cref="EditModuleColor"/>
	public partial  class DeskTopModuleInvisibleTitle : System.Web.UI.UserControl 
	{
		protected System.Web.UI.WebControls.Label ModuleTitle;
		

		public String EditText = null;
		public String EditUrl  = null;
		public String EditTarget = null;

		protected void Page_Load(object sender, System.EventArgs e) 
		{

			ColorPickerBtn.ImageUrl = ConfigurationSettings.AppSettings["apppath"]+ "Images/ColorPicker/coloricon.gif";


			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

			// Obtain reference to parent portal module
			PortalModuleControl portalModule = (PortalModuleControl) this.Parent;

			ModuleStatus.Text = portalModule.ModuleConfiguration.ModuleStatus;
			

			// Set the EditButton and ColorPickerBtn to be invisible
			ColorPickerBtn.Visible=false;
			EditButton.Visible = false;


			// Display the Edit button if the parent portalmodule has configured the PortalModuleTitle User Control
			// to display it -- and the current client has edit access permissions

			// Check if user is in editing mode, if applicable
			Components.UsersDB user = new ASPNetPortal.Components.UsersDB();
			int Editing=0;
			if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated && ASPNetPortal.Components.PortalSecurity.UserName != null && ASPNetPortal.Components.PortalSecurity.UserName !="")
			{
				try{Editing = user.GetUserEditingStatus(ASPNetPortal.Components.PortalSecurity.UserName);}
				catch{}
			}

			if (Page.IsPostBack==false && Editing==1)
			{
			
				if ((portalSettings.AlwaysShowEditButton == true) || (Components.PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles)) && (EditText != null)) 
				{

					EditButton.Visible = true;
					EditButton.Text = EditText;
					EditButton.NavigateUrl = ConfigurationSettings.AppSettings["apppath"]+ EditUrl + "?mid=" + portalModule.ModuleId.ToString();
					EditButton.Target = EditTarget;
					ColorPickerBtn.Visible=true;
				}

			}
			else if (Page.IsPostBack==true && Editing!=1)
			{
				if ((portalSettings.AlwaysShowEditButton == true) || (Components.PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles)) && (EditText != null)) 
				{

					EditButton.Visible = true;
					EditButton.Text = EditText;
					EditButton.NavigateUrl = ConfigurationSettings.AppSettings["apppath"]+ EditUrl + "?mid=" + portalModule.ModuleId.ToString();
					EditButton.Target = EditTarget;
					ColorPickerBtn.Visible=true;
				}
			}
			else if (Page.IsPostBack==false && Editing!=1)
			{
				if ((portalSettings.AlwaysShowEditButton == true) || (Components.PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles)) && (EditText != null)) 
				{

					EditButton.Visible = false;
					EditButton.Text = EditText;
					EditButton.NavigateUrl = ConfigurationSettings.AppSettings["apppath"]+ EditUrl + "?mid=" + portalModule.ModuleId.ToString();
					EditButton.Target = EditTarget;
					ColorPickerBtn.Visible=false;
				}
			}
			else if (Page.IsPostBack==true && Editing==1)
			{
				if ((portalSettings.AlwaysShowEditButton == true) || (Components.PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles)) && (EditText != null)) 
				{

					EditButton.Visible = false;
					EditButton.Text = EditText;
					EditButton.NavigateUrl = ConfigurationSettings.AppSettings["apppath"]+ EditUrl + "?mid=" + portalModule.ModuleId.ToString();
					EditButton.Target = EditTarget;
					ColorPickerBtn.Visible=false;
				}
			}



		}
 

		private void ColorPickerBtn_Click(Object sender, ImageClickEventArgs e) 
		{
			// Obtain reference to parent portal module
			PortalModuleControl portalModule = (PortalModuleControl) this.Parent;

			
			// Redirect to the EditModuleColor.aspx page
			Response.Redirect(ConfigurationSettings.AppSettings["apppath"]+"DesktopModules/EditModuleColor.aspx?mid="+portalModule.ModuleId.ToString());
		}


		public DeskTopModuleInvisibleTitle() 
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
		//		Required method for Designer support - do not modify
		//		the contents of this method with the code editor.

		private void InitializeComponent() 
		{
			this.ColorPickerBtn.Click += new System.Web.UI.ImageClickEventHandler(this.ColorPickerBtn_Click);

		}
		#endregion
	}
}