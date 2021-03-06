﻿
/*
	File generated by NetTiers templates [www.nettiers.com]
	Generated on : July 10, 2013
	Important: Do not modify this file. Edit the file MediaVideo.cs instead.
*/

#region using directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using ShutterTale.Entities.Validation;
#endregion

namespace ShutterTale.Entities
{
	///<summary>
	/// An object representation of the 'Media_Video' table. [No description found the database]	
	///</summary>
	[DataContract]
	[DataObject, CLSCompliant(true)]
	public abstract partial class MediaVideoBase : EntityBase, IMediaVideo, IEntityId<MediaVideoKey>, System.IComparable, System.ICloneable, ICloneableEx, IEditableObject, IComponent, INotifyPropertyChanged
	{		
		#region Variable Declarations
		
		/// <summary>
		///  Hold the inner data of the entity.
		/// </summary>
		[DataMember]
		private MediaVideoEntityData entityData;
		
		/// <summary>
		/// 	Hold the original data of the entity, as loaded from the repository.
		/// </summary>
		[DataMember]
		private MediaVideoEntityData _originalData;
		
		/// <summary>
		/// 	Hold a backup of the inner data of the entity.
		/// </summary>
		private MediaVideoEntityData backupData; 
		
		/// <summary>
		/// 	Key used if Tracking is Enabled for the <see cref="EntityLocator" />.
		/// </summary>
		private string entityTrackingKey;
		
		/// <summary>
		/// 	Hold the parent TList&lt;entity&gt; in which this entity maybe contained.
		/// </summary>
		/// <remark>Mostly used for databinding</remark>
		private TList<MediaVideo> parentCollection;
		
		private bool inTxn = false;
		
		/// <summary>
		/// Occurs when a value is being changed for the specified column.
		/// </summary>
		public event MediaVideoEventHandler ColumnChanging;		
		
		/// <summary>
		/// Occurs after a value has been changed for the specified column.
		/// </summary>
		public event MediaVideoEventHandler ColumnChanged;
		
		#endregion Variable Declarations
		
		#region Constructors
		///<summary>
		/// Creates a new <see cref="MediaVideoBase"/> instance.
		///</summary>
		public MediaVideoBase()
		{
			this.entityData = new MediaVideoEntityData();
			this.backupData = null;
		}		
		
		///<summary>
		/// Creates a new <see cref="MediaVideoBase"/> instance.
		///</summary>
		///<param name="_videoChannels"></param>
		///<param name="_videoCodec"></param>
		///<param name="_id"></param>
		public MediaVideoBase(System.Byte _videoChannels, System.String _videoCodec, 
			System.Guid _id)
		{
			this.entityData = new MediaVideoEntityData();
			this.backupData = null;

			this.VideoChannels = _videoChannels;
			this.VideoCodec = _videoCodec;
			this.Id = _id;
		}
		
		///<summary>
		/// A simple factory method to create a new <see cref="MediaVideo"/> instance.
		///</summary>
		///<param name="_videoChannels"></param>
		///<param name="_videoCodec"></param>
		///<param name="_id"></param>
        public static MediaVideo CreateMediaVideo(System.Byte _videoChannels, System.String _videoCodec, 
			System.Guid _id)
		{
			MediaVideo newMediaVideo = new MediaVideo();
			newMediaVideo.VideoChannels = _videoChannels;
			newMediaVideo.VideoCodec = _videoCodec;
			newMediaVideo.Id = _id;
			return newMediaVideo;
		}
				
		#endregion Constructors
			
		#region Properties	
		
		#region Data Properties		
		/// <summary>
		/// 	Gets or sets the VideoChannels property. 
		///		
		/// </summary>
		/// <value>This type is time.</value>
		/// <remarks>
		/// This property can be set to null. 
		/// If this column is null it is up to the developer to check using the HasValue property
		/// and perform business logic appropriately.
		/// </remarks>
		
		



        [DescriptionAttribute(@""), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(false, false, true)]
        public virtual System.Byte VideoChannels
		{
			get
			{
				return this.entityData.VideoChannels; 
			}
			
			set
			{
				if (this.entityData.VideoChannels == value)
					return;
				
                OnPropertyChanging("VideoChannels");                    
				OnColumnChanging(MediaVideoColumn.VideoChannels, this.entityData.VideoChannels);
				this.entityData.VideoChannels = value;
				if ( !this._deserializing && this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(MediaVideoColumn.VideoChannels, this.entityData.VideoChannels);
				OnPropertyChanged("VideoChannels");
			}
		}
		
		/// <summary>
		/// 	Gets or sets the VideoCodec property. 
		///		
		/// </summary>
		/// <value>This type is nvarchar.</value>
		/// <remarks>
		/// This property can not be set to null. 
		/// </remarks>
		/// <exception cref="ArgumentNullException">If you attempt to set to null.</exception>
		
		[Required(ErrorMessage = "VideoCodec is required")]




		[DescriptionAttribute(@""), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(false, false, false)]
		public virtual System.String VideoCodec
		{
			get
			{
				return this.entityData.VideoCodec; 
			}
			
			set
			{
				if (this.entityData.VideoCodec == value)
					return;
				
                OnPropertyChanging("VideoCodec");                    
				OnColumnChanging(MediaVideoColumn.VideoCodec, this.entityData.VideoCodec);
				this.entityData.VideoCodec = value;
				if ( !this._deserializing && this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(MediaVideoColumn.VideoCodec, this.entityData.VideoCodec);
				OnPropertyChanged("VideoCodec");
			}
		}
		
		/// <summary>
		/// 	Gets or sets the Id property. 
		///		
		/// </summary>
		/// <value>This type is uniqueidentifier.</value>
		/// <remarks>
		/// This property can not be set to null. 
		/// </remarks>
		
		[Required(ErrorMessage = "Id is required")]




		[DescriptionAttribute(@""), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(true, false, false)]
		public override System.Guid Id
		{
			get
			{
				return this.entityData.Id; 
			}
			
			set
			{
				if (this.entityData.Id == value)
					return;
				
                OnPropertyChanging("Id");                    
				OnColumnChanging(MediaVideoColumn.Id, this.entityData.Id);
				this.entityData.Id = value;
				this.EntityId.Id = value;
				if ( !this._deserializing && this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(MediaVideoColumn.Id, this.entityData.Id);
				OnPropertyChanged("Id");
			}
		}
		
		/// <summary>
		/// 	Get the original value of the Id property.
		///		
		/// </summary>
		/// <remarks>This is the original value of the Id property.</remarks>
		/// <value>This type is uniqueidentifier</value>
		[BrowsableAttribute(false)/*, XmlIgnoreAttribute()*/]
		public  virtual System.Guid OriginalId
		{
			get { return this.entityData.OriginalId; }
			set { this.entityData.OriginalId = value; }
		}
		
		#endregion Data Properties		

		#region Source Foreign Key Property
				
		/// <summary>
		/// Gets or sets the source <see cref="MediaAudio"/>.
		/// </summary>
		/// <value>The source MediaAudio for Id.</value>
        [XmlIgnore()]
		[Browsable(false), System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
		public virtual MediaAudio IdSource
      	{
            get { return entityData.IdSource; }
            set { entityData.IdSource = value; }
      	}
		#endregion
		
		#region Children Collections
		#endregion Children Collections
		
		#endregion
		#region Validation
		
		/// <summary>
		/// Assigns validation rules to this object based on model definition.
		/// </summary>
		/// <remarks>This method overrides the base class to add schema related validation.</remarks>
		protected override void AddValidationRules()
		{
			//Validation rules based on database schema.
			ValidationRules.AddRule( CommonRules.NotNull,
				new ValidationRuleArgs("VideoCodec", "Video Codec"));
		}
   		#endregion
		
		#region Table Meta Data
		/// <summary>
		///		The name of the underlying database table.
		/// </summary>
		[BrowsableAttribute(false), XmlIgnoreAttribute()]
		public override string TableName
		{
			get { return "Media_Video"; }
		}
		
		/// <summary>
		///		The name of the underlying database table's columns.
		/// </summary>
		[BrowsableAttribute(false), XmlIgnoreAttribute()]
		public override string[] TableColumns
		{
			get
			{
				return new string[] {"VideoChannels", "VideoCodec", "Id"};
			}
		}
		#endregion 
		
		#region IEditableObject
		
		#region  CancelAddNew Event
		/// <summary>
        /// The delegate for the CancelAddNew event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public delegate void CancelAddNewEventHandler(object sender, EventArgs e);
    
    	/// <summary>
		/// The CancelAddNew event.
		/// </summary>
		[field:NonSerialized]
		public event CancelAddNewEventHandler CancelAddNew ;

		/// <summary>
        /// Called when [cancel add new].
        /// </summary>
        public void OnCancelAddNew()
        {    
			if (!SuppressEntityEvents)
			{
	            CancelAddNewEventHandler handler = CancelAddNew ;
            	if (handler != null)
	            {    
    	            handler(this, EventArgs.Empty) ;
        	    }
	        }
        }
		#endregion 
		
		/// <summary>
		/// Begins an edit on an object.
		/// </summary>
		void IEditableObject.BeginEdit() 
	    {
	        //Console.WriteLine("Start BeginEdit");
	        if (!inTxn) 
	        {
	            this.backupData = this.entityData.Clone() as MediaVideoEntityData;
	            inTxn = true;
	            //Console.WriteLine("BeginEdit");
	        }
	        //Console.WriteLine("End BeginEdit");
	    }
	
		/// <summary>
		/// Discards changes since the last <c>BeginEdit</c> call.
		/// </summary>
	    void IEditableObject.CancelEdit() 
	    {
	        //Console.WriteLine("Start CancelEdit");
	        if (this.inTxn) 
	        {
	            this.entityData = this.backupData;
	            this.backupData = null;
				this.inTxn = false;

				if (this.bindingIsNew)
	        	//if (this.EntityState == EntityState.Added)
	        	{
					if (this.parentCollection != null)
						this.parentCollection.Remove( (MediaVideo) this ) ;
				}	            
	        }
	        //Console.WriteLine("End CancelEdit");
	    }
	
		/// <summary>
		/// Pushes changes since the last <c>BeginEdit</c> or <c>IBindingList.AddNew</c> call into the underlying object.
		/// </summary>
	    void IEditableObject.EndEdit() 
	    {
	        //Console.WriteLine("Start EndEdit" + this.custData.id + this.custData.lastName);
	        if (this.inTxn) 
	        {
	            this.backupData = null;
				if (this.IsDirty) 
				{
					if (this.bindingIsNew) {
						this.EntityState = EntityState.Added;
						this.bindingIsNew = false ;
					}
					else
						if (this.EntityState == EntityState.Unchanged) 
							this.EntityState = EntityState.Changed ;
				}

				this.bindingIsNew = false ;
	            this.inTxn = false;	            
	        }
	        //Console.WriteLine("End EndEdit");
	    }
	    
	    /// <summary>
        /// Gets or sets the parent collection of this current entity, if available.
        /// </summary>
        /// <value>The parent collection.</value>
	    [XmlIgnore]
		[Browsable(false)]
	    public override object ParentCollection
	    {
	        get 
	        {
	            return this.parentCollection;
	        }
	        set 
	        {
	            this.parentCollection = value as TList<MediaVideo>;
	        }
	    }
	    
	    /// <summary>
        /// Called when the entity is changed.
        /// </summary>
	    private void OnEntityChanged() 
	    {
	        if (!SuppressEntityEvents && !inTxn && this.parentCollection != null) 
	        {
	            this.parentCollection.EntityChanged(this as MediaVideo);
	        }
	    }


		#endregion
		
		#region ICloneable Members
		///<summary>
		///  Returns a Typed MediaVideo Entity 
		///</summary>
		protected virtual MediaVideo Copy(IDictionary existingCopies)
		{
			if (existingCopies == null)
			{
				// This is the root of the tree to be copied!
				existingCopies = new Hashtable();
			}

			//shallow copy entity
			MediaVideo copy = new MediaVideo();
			existingCopies.Add(this, copy);
			copy.SuppressEntityEvents = true;
				copy.VideoChannels = this.VideoChannels;
				copy.VideoCodec = this.VideoCodec;
				copy.Id = this.Id;
					copy.OriginalId = this.OriginalId;
			
			if (this.IdSource != null && existingCopies.Contains(this.IdSource))
				copy.IdSource = existingCopies[this.IdSource] as MediaAudio;
			else
				copy.IdSource = MakeCopyOf(this.IdSource, existingCopies) as MediaAudio;
		
			copy.EntityState = this.EntityState;
			copy.SuppressEntityEvents = false;
			return copy;
		}		
		
		
		
		///<summary>
		///  Returns a Typed MediaVideo Entity 
		///</summary>
		public virtual MediaVideo Copy()
		{
			return this.Copy(null);	
		}
		
		///<summary>
		/// ICloneable.Clone() Member, returns the Shallow Copy of this entity.
		///</summary>
		public object Clone()
		{
			return this.Copy(null);
		}
		
		///<summary>
		/// ICloneableEx.Clone() Member, returns the Shallow Copy of this entity.
		///</summary>
		public object Clone(IDictionary existingCopies)
		{
			return this.Copy(existingCopies);
		}
		
		///<summary>
		/// Returns a deep copy of the child collection object passed in.
		///</summary>
		public static object MakeCopyOf(object x)
		{
			if (x == null)
				return null;
				
			if (x is ICloneable)
			{
				// Return a deep copy of the object
				return ((ICloneable)x).Clone();
			}
			else
				throw new System.NotSupportedException("Object Does Not Implement the ICloneable Interface.");
		}
		
		///<summary>
		/// Returns a deep copy of the child collection object passed in.
		///</summary>
		public static object MakeCopyOf(object x, IDictionary existingCopies)
		{
			if (x == null)
				return null;
			
			if (x is ICloneableEx)
			{
				// Return a deep copy of the object
				return ((ICloneableEx)x).Clone(existingCopies);
			}
			else if (x is ICloneable)
			{
				// Return a deep copy of the object
				return ((ICloneable)x).Clone();
			}
			else
				throw new System.NotSupportedException("Object Does Not Implement the ICloneable or IClonableEx Interface.");
		}
		
		
		///<summary>
		///  Returns a Typed MediaVideo Entity which is a deep copy of the current entity.
		///</summary>
		public virtual MediaVideo DeepCopy()
		{
			return EntityHelper.Clone<MediaVideo>(this as MediaVideo);	
		}
		#endregion
		
		#region Methods	
			
		///<summary>
		/// Revert all changes and restore original values.
		///</summary>
		public override void CancelChanges()
		{
			IEditableObject obj = (IEditableObject) this;
			obj.CancelEdit();

			this.entityData = null;
			if (this._originalData != null)
			{
				this.entityData = this._originalData.Clone() as MediaVideoEntityData;
			}
			else
			{
				//Since this had no _originalData, then just reset the entityData with a new one.  entityData cannot be null.
				this.entityData = new MediaVideoEntityData();
			}
		}	
		
		/// <summary>
		/// Accepts the changes made to this object.
		/// </summary>
		/// <remarks>
		/// After calling this method, properties: IsDirty, IsNew are false. IsDeleted flag remains unchanged as it is handled by the parent List.
		/// </remarks>
		public override void AcceptChanges()
		{
			base.AcceptChanges();

			// we keep of the original version of the data
			this._originalData = null;
			this._originalData = this.entityData.Clone() as MediaVideoEntityData;
		}
		
		#region Comparision with original data
		
		/// <summary>
		/// Determines whether the property value has changed from the original data.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns>
		/// 	<c>true</c> if the property value has changed; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPropertyChanged(MediaVideoColumn column)
		{
			switch(column)
			{
					case MediaVideoColumn.VideoChannels:
					return entityData.VideoChannels != _originalData.VideoChannels;
					case MediaVideoColumn.VideoCodec:
					return entityData.VideoCodec != _originalData.VideoCodec;
					case MediaVideoColumn.Id:
					return entityData.Id != _originalData.Id;
			
				default:
					return false;
			}
		}
		
		/// <summary>
		/// Determines whether the property value has changed from the original data.
		/// </summary>
		/// <param name="columnName">The column name.</param>
		/// <returns>
		/// 	<c>true</c> if the property value has changed; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsPropertyChanged(string columnName)
		{
			return 	IsPropertyChanged(EntityHelper.GetEnumValue< MediaVideoColumn >(columnName));
		}
		
		/// <summary>
		/// Determines whether the data has changed from original.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if data has changed; otherwise, <c>false</c>.
		/// </returns>
		public bool HasDataChanged()
		{
			bool result = false;
			result = result || entityData.VideoChannels != _originalData.VideoChannels;
			result = result || entityData.VideoCodec != _originalData.VideoCodec;
			result = result || entityData.Id != _originalData.Id;
			return result;
		}	
		
		///<summary>
		///  Returns a MediaVideo Entity with the original data.
		///</summary>
		public MediaVideo GetOriginalEntity()
		{
			if (_originalData != null)
				return CreateMediaVideo(
				_originalData.VideoChannels,
				_originalData.VideoCodec,
				_originalData.Id
				);
				
			return (MediaVideo)this.Clone();
		}
		#endregion
	
	#region Value Semantics Instance Equality
        ///<summary>
        /// Returns a value indicating whether this instance is equal to a specified object using value semantics.
        ///</summary>
        ///<param name="Object1">An object to compare to this instance.</param>
        ///<returns>true if Object1 is a <see cref="MediaVideoBase"/> and has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object Object1)
        {
			// Cast exception if Object1 is null or DbNull
			if (Object1 != null && Object1 != DBNull.Value && Object1 is MediaVideoBase)
				return ValueEquals(this, (MediaVideoBase)Object1);
			else
				return false;
        }

        /// <summary>
		/// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// Provides a hash function that is appropriate for <see cref="MediaVideoBase"/> class 
        /// and that ensures a better distribution in the hash table
        /// </summary>
        /// <returns>number (hash code) that corresponds to the value of an object</returns>
        public override int GetHashCode()
        {
			return ((this.VideoChannels == null) ? string.Empty : this.VideoChannels.ToString()).GetHashCode() ^ 
					this.VideoCodec.GetHashCode() ^ 
					this.Id.GetHashCode();
        }
		
		///<summary>
		/// Returns a value indicating whether this instance is equal to a specified object using value semantics.
		///</summary>
		///<param name="toObject">An object to compare to this instance.</param>
		///<returns>true if toObject is a <see cref="MediaVideoBase"/> and has the same value as this instance; otherwise, false.</returns>
		public virtual bool Equals(MediaVideoBase toObject)
		{
			if (toObject == null)
				return false;
			return ValueEquals(this, toObject);
		}
		#endregion
		
		///<summary>
		/// Determines whether the specified <see cref="MediaVideoBase"/> instances are considered equal using value semantics.
		///</summary>
		///<param name="Object1">The first <see cref="MediaVideoBase"/> to compare.</param>
		///<param name="Object2">The second <see cref="MediaVideoBase"/> to compare. </param>
		///<returns>true if Object1 is the same instance as Object2 or if both are null references or if objA.Equals(objB) returns true; otherwise, false.</returns>
		public static bool ValueEquals(MediaVideoBase Object1, MediaVideoBase Object2)
		{
			// both are null
			if (Object1 == null && Object2 == null)
				return true;

			// one or the other is null, but not both
			if (Object1 == null ^ Object2 == null)
				return false;
				
			bool equal = true;
			if ( Object1.VideoChannels != null && Object2.VideoChannels != null )
			{
				if (Object1.VideoChannels != Object2.VideoChannels)
					equal = false;
			}
			else if (Object1.VideoChannels == null ^ Object2.VideoChannels == null )
			{
				equal = false;
			}
			if (Object1.VideoCodec != Object2.VideoCodec)
				equal = false;
			if (Object1.Id != Object2.Id)
				equal = false;
					
			return equal;
		}
		
		#endregion
		
		#region IComparable Members
		///<summary>
		/// Compares this instance to a specified object and returns an indication of their relative values.
		///<param name="obj">An object to compare to this instance, or a null reference (Nothing in Visual Basic).</param>
		///</summary>
		///<returns>A signed integer that indicates the relative order of this instance and obj.</returns>
		public virtual int CompareTo(object obj)
		{
			throw new NotImplementedException();
			//return this. GetPropertyName(SourceTable.PrimaryKey.MemberColumns[0]) .CompareTo(((MediaVideoBase)obj).GetPropertyName(SourceTable.PrimaryKey.MemberColumns[0]));
		}
		
		/*
		// static method to get a Comparer object
        public static MediaVideoComparer GetComparer()
        {
            return new MediaVideoComparer();
        }
        */

        // Comparer delegates back to MediaVideo
        // Employee uses the integer's default
        // CompareTo method
        /*
        public int CompareTo(Item rhs)
        {
            return this.Id.CompareTo(rhs.Id);
        }
        */

/*
        // Special implementation to be called by custom comparer
        public int CompareTo(MediaVideo rhs, MediaVideoColumn which)
        {
            switch (which)
            {
            	
            	
            	case MediaVideoColumn.VideoChannels:
            		return this.VideoChannels.Value.CompareTo(rhs.VideoChannels.Value);
            		
            		                 
            	
            	
            	case MediaVideoColumn.VideoCodec:
            		return this.VideoCodec.CompareTo(rhs.VideoCodec);
            		
            		                 
            	
            	
            	case MediaVideoColumn.Id:
            		return this.Id.CompareTo(rhs.Id);
            		
            		                 
            }
            return 0;
        }
        */
	
		#endregion
		
		#region IComponent Members
		
		private ISite _site = null;

		/// <summary>
		/// Gets or Sets the site where this data is located.
		/// </summary>
		[XmlIgnore]
		[SoapIgnore]
		[Browsable(false)]
		public ISite Site
		{
			get{ return this._site; }
			set{ this._site = value; }
		}

		#endregion

		#region IDisposable Members
		
		/// <summary>
		/// Notify those that care when we dispose.
		/// </summary>
		[field:NonSerialized]
		public event System.EventHandler Disposed;

		/// <summary>
		/// Clean up. Nothing here though.
		/// </summary>
		public virtual void Dispose()
		{
			this.parentCollection = null;
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		/// <summary>
		/// Clean up.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				EventHandler handler = Disposed;
				if (handler != null)
					handler(this, EventArgs.Empty);
			}
		}
		
		#endregion
				
		#region IEntityKey<MediaVideoKey> Members
		
		// member variable for the EntityId property
		private MediaVideoKey _entityId;

		/// <summary>
		/// Gets or sets the EntityId property.
		/// </summary>
		[XmlIgnore]
		public virtual MediaVideoKey EntityId
		{
			get
			{
				if ( _entityId == null )
				{
					_entityId = new MediaVideoKey(this);
				}

				return _entityId;
			}
			set
			{
				if ( value != null )
				{
					value.Entity = this;
				}
				
				_entityId = value;
			}
		}
		
		#endregion
		
		#region EntityState
		/// <summary>
		///		Indicates state of object
		/// </summary>
		/// <remarks>0=Unchanged, 1=Added, 2=Changed</remarks>
		[BrowsableAttribute(false) , XmlIgnoreAttribute()]
		public override EntityState EntityState 
		{ 
			get{ return entityData.EntityState;	 } 
			set{ entityData.EntityState = value; } 
		}
		#endregion 
		
		#region EntityTrackingKey
		///<summary>
		/// Provides the tracking key for the <see cref="EntityLocator"/>
		///</summary>
		[XmlIgnore]
		public override string EntityTrackingKey
		{
			get
			{
				if(entityTrackingKey == null)
					entityTrackingKey = new System.Text.StringBuilder("MediaVideo")
					.Append("|").Append( this.Id.ToString()).ToString();
				return entityTrackingKey;
			}
			set
		    {
		        if (value != null)
                    entityTrackingKey = value;
		    }
		}
		#endregion 
		
		#region ToString Method
		
		///<summary>
		/// Returns a String that represents the current object.
		///</summary>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
				"{4}{3}- VideoChannels: {0}{3}- VideoCodec: {1}{3}- Id: {2}{3}{5}", 
				(this.VideoChannels == null) ? string.Empty : this.VideoChannels.ToString(),
				this.VideoCodec,
				this.Id,
				System.Environment.NewLine, 
				this.GetType(),
				this.Error.Length == 0 ? string.Empty : string.Format("- Error: {0}\n",this.Error));
		}
		
		#endregion ToString Method
		
		#region Inner data class
		
	/// <summary>
	///		The data structure representation of the 'Media_Video' table.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DataContract]
	internal protected class MediaVideoEntityData : ICloneable, ICloneableEx
	{
		#region Variable Declarations
		private EntityState currentEntityState = EntityState.Added;
		
		#region Primary key(s)
		/// <summary>			
		/// Id : 
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Media_Video"</remarks>
		[DataMember]
		public System.Guid Id;
			
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		[DataMember]
		public System.Guid OriginalId;
		
		#endregion
		
		#region Non Primary key(s)
		
		/// <summary>
		/// VideoChannels : 
		/// </summary>
		[DataMember]
        public System.Byte VideoChannels = (byte)0;
		
		/// <summary>
		/// VideoCodec : 
		/// </summary>
		[DataMember]
		public System.String VideoCodec = string.Empty;
		#endregion
			
		#region Source Foreign Key Property
				
		private MediaAudio _idSource = null;
		
		/// <summary>
		/// Gets or sets the source <see cref="MediaAudio"/>.
		/// </summary>
		/// <value>The source MediaAudio for Id.</value>
		[DataMember]
		[Browsable(false)]
		public virtual MediaAudio IdSource
      	{
            get { return this._idSource; }
            set { this._idSource = value; }
      	}
		#endregion
        
		#endregion Variable Declarations

		#region Data Properties

		#endregion Data Properties
		#region Clone Method

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public Object Clone()
		{
			MediaVideoEntityData _tmp = new MediaVideoEntityData();
						
			_tmp.Id = this.Id;
			_tmp.OriginalId = this.OriginalId;
			
			_tmp.VideoChannels = this.VideoChannels;
			_tmp.VideoCodec = this.VideoCodec;
			
			#region Source Parent Composite Entities
			if (this.IdSource != null)
				_tmp.IdSource = MakeCopyOf(this.IdSource) as MediaAudio;
			#endregion
		
			#region Child Collections
			#endregion Child Collections
			
			//EntityState
			_tmp.EntityState = this.EntityState;
			
			return _tmp;
		}
		
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public object Clone(IDictionary existingCopies)
		{
			if (existingCopies == null)
				existingCopies = new Hashtable();
				
			MediaVideoEntityData _tmp = new MediaVideoEntityData();
						
			_tmp.Id = this.Id;
			_tmp.OriginalId = this.OriginalId;
			
			_tmp.VideoChannels = this.VideoChannels;
			_tmp.VideoCodec = this.VideoCodec;
			
			#region Source Parent Composite Entities
			if (this.IdSource != null && existingCopies.Contains(this.IdSource))
				_tmp.IdSource = existingCopies[this.IdSource] as MediaAudio;
			else
				_tmp.IdSource = MakeCopyOf(this.IdSource, existingCopies) as MediaAudio;
			#endregion
		
			#region Child Collections
			#endregion Child Collections
			
			//EntityState
			_tmp.EntityState = this.EntityState;
			
			return _tmp;
		}
		
		#endregion Clone Method
		
		/// <summary>
		///		Indicates state of object
		/// </summary>
		/// <remarks>0=Unchanged, 1=Added, 2=Changed</remarks>
		[BrowsableAttribute(false), XmlIgnoreAttribute()]
		public EntityState	EntityState
		{
			get { return currentEntityState;  }
			set { currentEntityState = value; }
		}
	
	}//End struct

		#endregion
		
				
		
		#region DataContract serialization
		
		bool _deserializing = false;
		
		/// <summary>
		/// Called before deserializing the type.
		/// </summary>
		[OnDeserializingAttribute]
		private void Initialize_BeforeDeserializing(StreamingContext context)
		{
			this._deserializing = true;
		
			this.entityData = new MediaVideoEntityData();
			this.backupData = null;
			
			AddValidationRules();
		}
		
		/// <summary>
		/// Called after deserializing the type.
		/// </summary>
		[OnDeserializedAttribute ]
		private void Initialize_Deserialized(StreamingContext context)
		{
			this._deserializing = false;
		}
				
		#endregion
		
		
		#region Events trigger
		/// <summary>
		/// Raises the <see cref="ColumnChanging" /> event.
		/// </summary>
		/// <param name="column">The <see cref="MediaVideoColumn"/> which has raised the event.</param>
		public virtual void OnColumnChanging(MediaVideoColumn column)
		{
			OnColumnChanging(column, null);
			return;
		}
		
		/// <summary>
		/// Raises the <see cref="ColumnChanged" /> event.
		/// </summary>
		/// <param name="column">The <see cref="MediaVideoColumn"/> which has raised the event.</param>
		public virtual void OnColumnChanged(MediaVideoColumn column)
		{
			OnColumnChanged(column, null);
			return;
		} 
		
		
		/// <summary>
		/// Raises the <see cref="ColumnChanging" /> event.
		/// </summary>
		/// <param name="column">The <see cref="MediaVideoColumn"/> which has raised the event.</param>
		/// <param name="value">The changed value.</param>
		public virtual void OnColumnChanging(MediaVideoColumn column, object value)
		{
			if(IsEntityTracked && EntityState != EntityState.Added && !EntityManager.TrackChangedEntities)
                EntityManager.StopTracking(entityTrackingKey);
                
			if (!SuppressEntityEvents)
			{
				MediaVideoEventHandler handler = ColumnChanging;
				if(handler != null)
				{
					handler(this, new MediaVideoEventArgs(column, value));
				}
			}
		}
		
		/// <summary>
		/// Raises the <see cref="ColumnChanged" /> event.
		/// </summary>
		/// <param name="column">The <see cref="MediaVideoColumn"/> which has raised the event.</param>
		/// <param name="value">The changed value.</param>
		public virtual void OnColumnChanged(MediaVideoColumn column, object value)
		{
			if (!SuppressEntityEvents)
			{
				MediaVideoEventHandler handler = ColumnChanged;
				if(handler != null)
				{
					handler(this, new MediaVideoEventArgs(column, value));
				}
			
				// warn the parent list that i have changed
				OnEntityChanged();
			}
		} 
		#endregion
			
	} // End Class
	
	
	#region MediaVideoEventArgs class
	/// <summary>
	/// Provides data for the ColumnChanging and ColumnChanged events.
	/// </summary>
	/// <remarks>
	/// The ColumnChanging and ColumnChanged events occur when a change is made to the value 
	/// of a property of a <see cref="MediaVideo"/> object.
	/// </remarks>
	public class MediaVideoEventArgs : System.EventArgs
	{
		private MediaVideoColumn column;
		private object value;
		
		///<summary>
		/// Initalizes a new Instance of the MediaVideoEventArgs class.
		///</summary>
		public MediaVideoEventArgs(MediaVideoColumn column)
		{
			this.column = column;
		}
		
		///<summary>
		/// Initalizes a new Instance of the MediaVideoEventArgs class.
		///</summary>
		public MediaVideoEventArgs(MediaVideoColumn column, object value)
		{
			this.column = column;
			this.value = value;
		}
		
		///<summary>
		/// The MediaVideoColumn that was modified, which has raised the event.
		///</summary>
		///<value cref="MediaVideoColumn" />
		public MediaVideoColumn Column { get { return this.column; } }
		
		/// <summary>
        /// Gets the current value of the column.
        /// </summary>
        /// <value>The current value of the column.</value>
		public object Value{ get { return this.value; } }

	}
	#endregion
	
	///<summary>
	/// Define a delegate for all MediaVideo related events.
	///</summary>
	public delegate void MediaVideoEventHandler(object sender, MediaVideoEventArgs e);
	
	#region MediaVideoComparer
		
	/// <summary>
	///	Strongly Typed IComparer
	/// </summary>
	public class MediaVideoComparer : System.Collections.Generic.IComparer<MediaVideo>
	{
		MediaVideoColumn whichComparison;
		
		/// <summary>
        /// Initializes a new instance of the <see cref="T:MediaVideoComparer"/> class.
        /// </summary>
		public MediaVideoComparer()
        {            
        }               
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaVideoComparer"/> class.
        /// </summary>
        /// <param name="column">The column to sort on.</param>
        public MediaVideoComparer(MediaVideoColumn column)
        {
            this.whichComparison = column;
        }

		/// <summary>
        /// Determines whether the specified <see cref="MediaVideo"/> instances are considered equal.
        /// </summary>
        /// <param name="a">The first <see cref="MediaVideo"/> to compare.</param>
        /// <param name="b">The second <c>MediaVideo</c> to compare.</param>
        /// <returns>true if objA is the same instance as objB or if both are null references or if objA.Equals(objB) returns true; otherwise, false.</returns>
        public bool Equals(MediaVideo a, MediaVideo b)
        {
            return this.Compare(a, b) == 0;
        }

		/// <summary>
        /// Gets the hash code of the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int GetHashCode(MediaVideo entity)
        {
            return entity.GetHashCode();
        }

        /// <summary>
        /// Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns></returns>
        public int Compare(MediaVideo a, MediaVideo b)
        {
        	EntityPropertyComparer entityPropertyComparer = new EntityPropertyComparer(this.whichComparison.ToString());
        	return entityPropertyComparer.Compare(a, b);
        }

		/// <summary>
        /// Gets or sets the column that will be used for comparison.
        /// </summary>
        /// <value>The comparison column.</value>
        public MediaVideoColumn WhichComparison
        {
            get { return this.whichComparison; }
            set { this.whichComparison = value; }
        }
	}
	
	#endregion
	
	#region MediaVideoKey Class

	/// <summary>
	/// Wraps the unique identifier values for the <see cref="MediaVideo"/> object.
	/// </summary>
	[Serializable]
	[CLSCompliant(true)]
	public class MediaVideoKey : EntityKeyBase
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the MediaVideoKey class.
		/// </summary>
		public MediaVideoKey()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the MediaVideoKey class.
		/// </summary>
		public MediaVideoKey(MediaVideoBase entity)
		{
			this.Entity = entity;

			#region Init Properties

			if ( entity != null )
			{
				this.Id = entity.Id;
			}

			#endregion
		}
		
		/// <summary>
		/// Initializes a new instance of the MediaVideoKey class.
		/// </summary>
		public MediaVideoKey(System.Guid _id)
		{
			#region Init Properties

			this.Id = _id;

			#endregion
		}
		
		#endregion Constructors

		#region Properties
		
		// member variable for the Entity property
		private MediaVideoBase _entity;
		
		/// <summary>
		/// Gets or sets the Entity property.
		/// </summary>
		public MediaVideoBase Entity
		{
			get { return _entity; }
			set { _entity = value; }
		}
		
		// member variable for the Id property
		private System.Guid _id;
		
		/// <summary>
		/// Gets or sets the Id property.
		/// </summary>
		public System.Guid Id
		{
			get { return _id; }
			set
			{
				if ( this.Entity != null )
					this.Entity.Id = value;
				
				_id = value;
			}
		}
		
		#endregion

		#region Methods
		
		/// <summary>
		/// Reads values from the supplied <see cref="IDictionary"/> object into
		/// properties of the current object.
		/// </summary>
		/// <param name="values">An <see cref="IDictionary"/> instance that contains
		/// the key/value pairs to be used as property values.</param>
		public override void Load(IDictionary values)
		{
			#region Init Properties

			if ( values != null )
			{
				Id = ( values["Id"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Id"], typeof(System.Guid)) : Guid.Empty;
			}

			#endregion
		}

		/// <summary>
		/// Creates a new <see cref="IDictionary"/> object and populates it
		/// with the property values of the current object.
		/// </summary>
		/// <returns>A collection of name/value pairs.</returns>
		public override IDictionary ToDictionary()
		{
			IDictionary values = new Hashtable();

			#region Init Dictionary

			values.Add("Id", Id);

			#endregion Init Dictionary

			return values;
		}
		
		///<summary>
		/// Returns a String that represents the current object.
		///</summary>
		public override string ToString()
		{
			return String.Format("Id: {0}{1}",
								Id,
								System.Environment.NewLine);
		}

		#endregion Methods
	}
	
	#endregion	

	#region MediaVideoColumn Enum
	
	/// <summary>
	/// Enumerate the MediaVideo columns.
	/// </summary>
	[Serializable]
	public enum MediaVideoColumn : int
	{
		/// <summary>
		/// VideoChannels : 
		/// </summary>
		[EnumTextValue("Video Channels")]
		[ColumnEnum("VideoChannels", typeof(System.TimeSpan), System.Data.DbType.Time, false, false, true)]
		VideoChannels = 1,
		/// <summary>
		/// VideoCodec : 
		/// </summary>
		[EnumTextValue("Video Codec")]
		[ColumnEnum("VideoCodec", typeof(System.String), System.Data.DbType.String, false, false, false)]
		VideoCodec = 2,
		/// <summary>
		/// Id : 
		/// </summary>
		[EnumTextValue("Id")]
		[ColumnEnum("Id", typeof(System.Guid), System.Data.DbType.Guid, true, false, false)]
		Id = 3
	}//End enum

	#endregion MediaVideoColumn Enum

} // end namespace
