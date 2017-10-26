﻿using System;
using System.ComponentModel;

namespace ShutterTale.Entities
{
	/// <summary>
	///		The data structure representation of the 'Previews' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IPreviews 
	{
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Previews"</remarks>
		System.Guid Id { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.Guid OriginalId { get; set; }
			
		
		
		/// <summary>
		/// Level0 : 
		/// </summary>
		System.Byte[]  Level0  { get; set; }
		
		/// <summary>
		/// Level1 : 
		/// </summary>
		System.Byte[]  Level1  { get; set; }
		
		/// <summary>
		/// Level2 : 
		/// </summary>
		System.Byte[]  Level2  { get; set; }
		
		/// <summary>
		/// Level3 : 
		/// </summary>
		System.Byte[]  Level3  { get; set; }
		
		/// <summary>
		/// PreviewType : 
		/// </summary>
		System.String  PreviewType  { get; set; }
		
		/// <summary>
		/// Medium_Id : 
		/// </summary>
		System.Guid  MediumId  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}

