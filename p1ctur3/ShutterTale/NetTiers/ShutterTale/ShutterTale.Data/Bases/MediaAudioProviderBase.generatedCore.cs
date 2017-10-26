#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using ShutterTale.Entities;
using ShutterTale.Data;

#endregion

namespace ShutterTale.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="MediaAudioProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MediaAudioProviderBaseCore : EntityProviderBase<ShutterTale.Entities.MediaAudio, ShutterTale.Entities.MediaAudioKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, ShutterTale.Entities.MediaAudioKey key)
		{
			return Delete(transactionManager, key.Id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(System.Guid _id)
		{
			return Delete(null, _id);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, System.Guid _id);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override ShutterTale.Entities.MediaAudio Get(TransactionManager transactionManager, ShutterTale.Entities.MediaAudioKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Media_Audio index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public ShutterTale.Entities.MediaAudio GetById(System.Guid _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Audio index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public ShutterTale.Entities.MediaAudio GetById(System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Audio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public ShutterTale.Entities.MediaAudio GetById(TransactionManager transactionManager, System.Guid _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Audio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public ShutterTale.Entities.MediaAudio GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Audio index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public ShutterTale.Entities.MediaAudio GetById(System.Guid _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Audio index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public abstract ShutterTale.Entities.MediaAudio GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;MediaAudio&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;MediaAudio&gt;"/></returns>
		public static TList<MediaAudio> Fill(IDataReader reader, TList<MediaAudio> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				ShutterTale.Entities.MediaAudio c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("MediaAudio")
					.Append("|").Append((System.Guid)reader[((int)MediaAudioColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<MediaAudio>(
					key.ToString(), // EntityTrackingKey
					"MediaAudio",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ShutterTale.Entities.MediaAudio();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.AudioChannels = (System.Byte)reader[((int)MediaAudioColumn.AudioChannels - 1)];
					c.Duration = (System.String)reader[((int)MediaAudioColumn.Duration - 1)];
					c.AudioCodec = (System.String)reader[((int)MediaAudioColumn.AudioCodec - 1)];
					c.Id = (System.Guid)reader[((int)MediaAudioColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.MediaAudio"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaAudio"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ShutterTale.Entities.MediaAudio entity)
		{
			if (!reader.Read()) return;
			
			entity.AudioChannels = (System.Byte)reader[((int)MediaAudioColumn.AudioChannels - 1)];
			entity.Duration = (System.String)reader[((int)MediaAudioColumn.Duration - 1)];
			entity.AudioCodec = (System.String)reader[((int)MediaAudioColumn.AudioCodec - 1)];
			entity.Id = (System.Guid)reader[((int)MediaAudioColumn.Id - 1)];
			entity.OriginalId = (System.Guid)reader["Id"];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.MediaAudio"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaAudio"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ShutterTale.Entities.MediaAudio entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.AudioChannels = (System.Byte)dataRow["AudioChannels"];
			entity.Duration = (System.String)dataRow["Duration"];
			entity.AudioCodec = (System.String)dataRow["AudioCodec"];
			entity.Id = (System.Guid)dataRow["Id"];
			entity.OriginalId = (System.Guid)dataRow["Id"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaAudio"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ShutterTale.Entities.MediaAudio Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ShutterTale.Entities.MediaAudio entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region IdSource	
			if (CanDeepLoad(entity, "Media|IdSource", deepLoadType, innerList) 
				&& entity.IdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.Id;
				Media tmpEntity = EntityManager.LocateEntity<Media>(EntityLocator.ConstructKeyFromPkItems(typeof(Media), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.IdSource = tmpEntity;
				else
					entity.IdSource = DataRepository.MediaProvider.GetById(transactionManager, entity.Id);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'IdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.IdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.MediaProvider.DeepLoad(transactionManager, entity.IdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion IdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region MediaVideo
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "MediaVideo|MediaVideo", deepLoadType, innerList))
			{
				entity.MediaVideo = DataRepository.MediaVideoProvider.GetById(transactionManager, entity.Id);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MediaVideo' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.MediaVideo != null)
				{
					deepHandles.Add("MediaVideo",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< MediaVideo >) DataRepository.MediaVideoProvider.DeepLoad,
						new object[] { transactionManager, entity.MediaVideo, deep, deepLoadType, childTypes, innerList }
					));
				}
			}
			#endregion 
			
			
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the ShutterTale.Entities.MediaAudio object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ShutterTale.Entities.MediaAudio instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ShutterTale.Entities.MediaAudio Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ShutterTale.Entities.MediaAudio entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region IdSource
			if (CanDeepSave(entity, "Media|IdSource", deepSaveType, innerList) 
				&& entity.IdSource != null)
			{
				DataRepository.MediaProvider.Save(transactionManager, entity.IdSource);
				entity.Id = entity.IdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();

			#region MediaVideo
			if (CanDeepSave(entity.MediaVideo, "MediaVideo|MediaVideo", deepSaveType, innerList))
			{

				if (entity.MediaVideo != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.MediaVideo.Id = entity.Id;
					//DataRepository.MediaVideoProvider.Save(transactionManager, entity.MediaVideo);
					deepHandles.Add("MediaVideo",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< MediaVideo >) DataRepository.MediaVideoProvider.DeepSave,
						new object[] { transactionManager, entity.MediaVideo, deepSaveType, childTypes, innerList }
					));
				}
			} 
			#endregion 
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region MediaAudioChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ShutterTale.Entities.MediaAudio</c>
	///</summary>
	public enum MediaAudioChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Media</c> at IdSource
		///</summary>
		[ChildEntityType(typeof(Media))]
		Media,
		///<summary>
		/// Entity <c>MediaVideo</c> as OneToOne for MediaVideo
		///</summary>
		[ChildEntityType(typeof(MediaVideo))]
		MediaVideo,
	}
	
	#endregion MediaAudioChildEntityTypes
	
	#region MediaAudioFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MediaAudioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaAudio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaAudioFilterBuilder : SqlFilterBuilder<MediaAudioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaAudioFilterBuilder class.
		/// </summary>
		public MediaAudioFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaAudioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaAudioFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaAudioFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaAudioFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaAudioFilterBuilder
	
	#region MediaAudioParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MediaAudioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaAudio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaAudioParameterBuilder : ParameterizedSqlFilterBuilder<MediaAudioColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaAudioParameterBuilder class.
		/// </summary>
		public MediaAudioParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaAudioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaAudioParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaAudioParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaAudioParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaAudioParameterBuilder
	
	#region MediaAudioSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MediaAudioColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaAudio"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MediaAudioSortBuilder : SqlSortBuilder<MediaAudioColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaAudioSqlSortBuilder class.
		/// </summary>
		public MediaAudioSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MediaAudioSortBuilder
	
} // end namespace
