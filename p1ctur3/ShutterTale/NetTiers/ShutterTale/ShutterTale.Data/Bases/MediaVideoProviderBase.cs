#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using ShutterTale.Entities;
using ShutterTale.Data;

#endregion

namespace ShutterTale.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="MediaVideoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MediaVideoProviderBase : MediaVideoProviderBaseCore
	{
	} // end class
} // end namespace
