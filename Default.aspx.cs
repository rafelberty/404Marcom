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

namespace ASPNetPortal 
{
	/// <summary>This class handles functionality associated with the Default.aspx page.
	/// <para>This page the first page to load from the root directory of the application. .</para>
	/// <para></para>
	/// <para>It does not render any content but is used solely to determine the browser type being used on the client and directs accordingly to either the desktop or mobile version of the portal.</para>
	/// <list type="bullet">
	/// <item>
	/// <description>The creation of new tabs relative to the current tab, i.e. top level of the site, sibling tab (i.e. same parent) or child tab.</description>
	/// </item>
	/// <item>
	/// <description>Ordering of sibling tabs relative to eachother</description>
	/// </item>
	/// <item>
	/// <description>Setting the parent - child relationship between tabs</description>
	/// </item>
	/// <item>
	/// <description>Jump To, editing and deletion of tabs</description>
	/// </item>
	/// </list>
	/// <note>Baseline mobile functionalty is included in the portal solution, but it has not been developed further with the ImageBuilder application. As a result there are only a few mobile modules, whereas all the ImageBuilder modules are designed for use
	/// in the desktop based view of the portal.</note>
	/// </summary>
	/// <seealso cref="MobileDefault"/>
	/// <seealso cref="DesktopDefault"/>  
    public partial class CDefault : System.Web.UI.Page 
	{

        public CDefault() {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e) {

            if (Request.Browser["IsMobileDevice"] == "true" ) {
        
                Response.Redirect("MobileDefault.aspx");
            }
            else
			{
				Response.Redirect("DesktopDefault.aspx");
			}
        }

        protected void Page_Init(object sender, EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
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
