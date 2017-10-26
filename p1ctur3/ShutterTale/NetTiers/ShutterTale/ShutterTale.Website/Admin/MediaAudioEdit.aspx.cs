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

public partial class MediaAudioEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "MediaAudioEdit.aspx?{0}", MediaAudioDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "MediaAudioEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "MediaAudio.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


