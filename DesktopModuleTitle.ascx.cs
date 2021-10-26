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
	///	The DesktopModuleTitle.ascx module is for rendering a normal header at the top of each module which includes it.
	/// <para></para>
	/// <para>Each module normally has a header which not only displays the title/name of the module but also provides:</para>
	/// <list type="bullet">
	/// <item>
	/// <description>The ability to surface the link to add addtional items to modules containing lists of data, e.g. news or documents</description>
	/// </item>
	/// <item>
	/// <description>The ability to render the Edit and <see cref="DesktopPages.EditModuleColor"/>/Edit Module properties features</description>
	/// </item>
	/// </list>
	/// <para>There is also the DeskTopModuleInvisibleTitle.ascx module where the functionality is required, but the header title should not be displayed. See <see cref="DeskTopModuleInvisibleTitle"/>.</para>
	/// <para>For other modules, a standard visible header is shown. See <see cref="DesktopModuleTitle"/>. </para>
	/// </summary>
	/// <seealso cref="DeskTopModuleInvisibleTitle"/>
	/// <seealso cref="EditModuleColor"/>
    public partial  class DesktopModuleTitle : System.Web.UI.UserControl 
	{
		
        public String EditText = null;
        public String EditUrl  = null;
		public String EditTarget = null;
		protected string ControlID;
		protected string ControlExpanded;
		
		
        protected void Page_Load(object sender, System.EventArgs e) {

			ControlID = this.ClientID;
			
			ColorPickerBtn.ImageUrl = ConfigurationSettings.AppSettings["apppath"]+ "Images/ColorPicker/coloricon.gif";

			
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];			

            // Obtain reference to parent portal module
            PortalModuleControl portalModule = (PortalModuleControl) this.Parent;

			string TitleColor = portalModule.ModuleConfiguration.TitleColor;
			string ModuleWidth = portalModule.ModuleConfiguration.Width.ToString() + portalModule.ModuleConfiguration.WidthUnit.ToString();
			

			//Set the appropriate Expanded Status
			int Expanded = portalModule.ModuleConfiguration.Expanded;
			
		

			//No Expansion Functionality Selected
			if (Expanded==-1)
			{
				ExpandScript.Text = "";
				ToggleScript.Text = "";
			}

			//No Selection and Expanded Functionality Selected
			if (Expanded==0)
			{
				ExpandScript.Text = "<a title=\"Expand/Collapse Module\" id=\""+ ControlID +"_link\" href=\"javascript: void(0);\" onclick=\"togglemodule(this, '"+ControlID+"');\"><img border=0 src=\"images/Collapse.gif\"></a>";
				ToggleScript.Text="<script language=\"javascript\" >;</script>";
			}

			//Collapsed Functionality Selected
			if (Expanded==1)
			{
				ExpandScript.Text = "<a title=\"Expand/Collapse Module\" id=\""+ ControlID +"_link\" href=\"javascript: void(0);\" onclick=\"togglemodule(this, '"+ControlID+"');\"><img border=0 src=\"images/Collapse.gif\"></a>";
				ToggleScript.Text="<script language=\"javascript\" >togglemodule(getObject('"+ControlID+"_link'), '"+ControlID+"');</script>";
			}



			// Determine Module Position
			if (portalModule.ModuleConfiguration.PaneName=="TopRightPane" || portalModule.ModuleConfiguration.PaneName=="CenterRightPane" || portalModule.ModuleConfiguration.PaneName=="BottomRightPane" )
			{
				td1.BgColor = "#cccccc";
				td2.BgColor = "#cccccc";
				td3.BgColor = "#cccccc";
				ModuleTitle.CssClass="RightModuleHeader";
				EditButton.CssClass="RightModuleHeader";
				ModuleStatus.CssClass="RightModuleHeader";
			}
			else
			{
				td1.BgColor = "#666666";
				td2.BgColor = "#666666";
				td3.BgColor = "#666666";				
				ModuleTitle.CssClass="ModuleHeader ";
				EditButton.CssClass="ModuleHeader ";
				ModuleStatus.CssClass="ModuleHeader ";
			}


            // Display Modular Title Text and Edit Buttons
			ModuleTitle.Text = portalModule.ModuleConfiguration.ModuleTitle;
			ModuleStatus.Text = portalModule.ModuleConfiguration.ModuleStatus;
			t1.Width = ModuleWidth;
			//td1.BgColor = TitleColor;
			//td2.BgColor = TitleColor;
			//td3.BgColor = TitleColor;

			//Set correct alignment
			td1.Align = ConfigurationSettings.AppSettings["align_left"];
			td2.Align = ConfigurationSettings.AppSettings["align_right"];


			// Set the EditButton and ColorPickerBtn to be invisible
			ColorPickerBtn.Visible=false;
			EditButton.Visible = false;
			

            // Display the Edit button if the parent portalmodule has configured the PortalModuleTitle User Control
            // to display it -- and the current client has edit access permissions

			// Check if user is in editing mode, if applicable
			Components.UsersDB user = new ASPNetPortal.Components.UsersDB();
			int Editing=0;
            if (ASPNetPortal.Components.PortalSecurity.IsAuthenticated && ASPNetPortal.Components.PortalSecurity.UserName != null && ASPNetPortal.Components.PortalSecurity.UserName != "")
			{
				try{Editing = user.GetUserEditingStatus(ASPNetPortal.Components.PortalSecurity.UserName);}
				catch{}
			}

			if (Editing==1)
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
			else if (Editing!=1) 
			{
				if ((portalSettings.AlwaysShowEditButton != true))
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


        public DesktopModuleTitle() {
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
			this.ColorPickerBtn.Click += new System.Web.UI.ImageClickEventHandler(this.ColorPickerBtn_Click);

		}
		#endregion
    }
}
