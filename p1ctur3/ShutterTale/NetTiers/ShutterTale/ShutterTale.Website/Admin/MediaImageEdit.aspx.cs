#region Imports...
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ShutterTale.Web.UI;
#endregion

public partial class MediaImageEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "MediaImageEdit.aspx?{0}", MediaImageDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "MediaImageEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "MediaImage.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


