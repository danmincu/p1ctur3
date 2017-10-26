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

public partial class PreviewsEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PreviewsEdit.aspx?{0}", PreviewsDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PreviewsEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Previews.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


