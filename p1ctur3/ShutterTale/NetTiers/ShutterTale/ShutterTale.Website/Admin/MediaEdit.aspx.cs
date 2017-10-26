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

public partial class MediaEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "MediaEdit.aspx?{0}", MediaDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "MediaEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Media.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewPreviews1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("Id={0}", GridViewPreviews1.SelectedDataKey.Values[0]);
		Response.Redirect("PreviewsEdit.aspx?" + urlParams, true);		
	}	
}


