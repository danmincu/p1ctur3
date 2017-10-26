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
	/// This class is the base class for any <see cref="MediaImageProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MediaImageProviderBase : MediaImageProviderBaseCore
	{
	} // end class
} // end namespace
