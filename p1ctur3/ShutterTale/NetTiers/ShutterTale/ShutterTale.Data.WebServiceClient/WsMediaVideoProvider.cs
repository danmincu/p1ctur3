﻿#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel;
using ShutterTale.Entities;
using ShutterTale.Data;

#endregion

namespace ShutterTale.Data.WebServiceClient
{
	///<summary>
	/// This class is the WebServiceClient Data Access Logic Component implementation for the <see cref="MediaVideo"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class WsMediaVideoProvider: WsMediaVideoProviderBase
	{		
		/// <summary>
		/// Creates a new <see cref="WsMediaVideoProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsMediaVideoProvider(string url): base(url){}
	}
}