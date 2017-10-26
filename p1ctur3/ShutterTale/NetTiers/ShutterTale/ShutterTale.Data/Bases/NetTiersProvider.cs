#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using ShutterTale.Entities;

#endregion

namespace ShutterTale.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current MediaProviderBase instance.
		///</summary>
		public virtual MediaProviderBase MediaProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current MediaImageProviderBase instance.
		///</summary>
		public virtual MediaImageProviderBase MediaImageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current MediaVideoProviderBase instance.
		///</summary>
		public virtual MediaVideoProviderBase MediaVideoProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current MediaAudioProviderBase instance.
		///</summary>
		public virtual MediaAudioProviderBase MediaAudioProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current PreviewsProviderBase instance.
		///</summary>
		public virtual PreviewsProviderBase PreviewsProvider{get {throw new NotImplementedException();}}
		
		
	}
}
