<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="System.IO"%>                             
<%@ Import Namespace="System.Net"%>
<%@ Import Namespace="System.Web"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Intialise();
    }



    public Boolean FileExists(string fileFullPath)
    {
        FileInfo file = new FileInfo(fileFullPath);
        return file.Exists;
    }
    public Boolean FolderExists(string folderPath)
    {
        DirectoryInfo directory = new DirectoryInfo(folderPath);
        return directory.Exists;
    }

    private void Intialise()
    {
        string sessHostName;// keep the host name in the session
        Boolean blnFileExists;//boolean returns true if particular file exists on the file system
        string physicalFilePath;//keep the physical  file path 
        String hostName; // keeps physical server dns name
   

        physicalFilePath= Request.ServerVariables["APPL_PHYSICAL_PATH"];
        sessHostName = Request.ServerVariables["SERVER_NAME"];
        blnFileExists = FileExists(physicalFilePath + sessHostName + ".txt");
        hostName = Dns.GetHostName();
        

        lblHostName.Text = "Session Host Name: " + sessHostName + " / Physical Server Name: " + hostName ;
        lblLockingFile.Text = "Looking for file -> " + physicalFilePath + sessHostName + ".txt" + " <- to return true";
        lblFileNameExist.Text = "Filename exists: " + blnFileExists;
        lblPhysicalPath.Text = "Physical Path: " + physicalFilePath;
        


	if (true) 
        { 
            lblResult.Text = "Success";
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetValidUntilExpires(true);
            
        } 
        else 
        { 
            lblResult.Text = "HTTP Response Status code is 404 and removes this server from GSLB rotation.";
    	    Response.Status = "404 Not Found";
            Response.StatusCode = 404;
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetValidUntilExpires(true);

        }
    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Health Check Monitoring Page</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <h3>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="lblHostName" runat="server"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="lblLockingFile" runat="server"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="lblFileNameExist" runat="server"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="lblPhysicalPath" runat="server"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="lblResult" runat="server"></asp:Label>
                    </h3>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
