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
	/// This class is the WebServiceClient Data Access Logic Component implementation for the <see cref="MediaAudio"/> entity.
	///</summary>
	[DataObject]
	[CLSCompliant(true)]
	public partial class WsMediaAudioProvider: WsMediaAudioProviderBase
	{		
		/// <summary>
		/// Creates a new <see cref="WsMediaAudioProvider"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsMediaAudioProvider(string url): base(url){}
	}
}