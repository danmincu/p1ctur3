﻿using System;
using System.ComponentModel;

namespace ShutterTale.Entities
{
	/// <summary>
	///		The data structure representation of the 'Media_Video' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IMediaVideo 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Media_Video"</remarks>
		System.Guid Id { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.Guid OriginalId { get; set; }
			
		
		
		/// <summary>
		/// VideoChannels : 
		/// </summary>
        System.Byte VideoChannels { get; set; }
		
		/// <summary>
		/// VideoCodec : 
		/// </summary>
		System.String  VideoCodec  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}

