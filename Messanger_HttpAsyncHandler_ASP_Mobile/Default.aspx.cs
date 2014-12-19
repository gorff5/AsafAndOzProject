using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Button1.Attributes.Add("onclick", "javascript:buttonClick(this);return false;");
        Button2.Attributes.Add("onclick", "javascript:buttonClick(this);return false;");
        Button3.Attributes.Add("onclick", "javascript:buttonClick(this);return false;");
        Button4.Attributes.Add("onclick", "javascript:buttonClick(this);return false;");
    }

} 