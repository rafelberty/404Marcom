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
        String hostName; // keeps physical server dns name
        hostName = Dns.GetHostName();
        lblHostName.Text = hostName ;
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
        Response.Cache.SetCacheability(HttpCacheability.Public);
        Response.Cache.SetValidUntilExpires(true);

    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Host Name Test Page</title>
    
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
        </table>
    </div>
    </form>
</body>
</html>
