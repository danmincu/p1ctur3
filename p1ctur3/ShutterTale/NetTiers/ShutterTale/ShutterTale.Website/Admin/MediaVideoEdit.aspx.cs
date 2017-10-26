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

public partial class MediaVideoEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "MediaVideoEdit.aspx?{0}", MediaVideoDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "MediaVideoEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "MediaVideo.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


