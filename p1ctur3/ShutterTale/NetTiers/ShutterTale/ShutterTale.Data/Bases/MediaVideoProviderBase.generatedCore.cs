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
	/// This class is the base class for any <see cref="MediaVideoProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MediaVideoProviderBaseCore : EntityProviderBase<ShutterTale.Entities.MediaVideo, ShutterTale.Entities.MediaVideoKey>
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
		public override bool Delete(TransactionManager transactionManager, ShutterTale.Entities.MediaVideoKey key)
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
		public override ShutterTale.Entities.MediaVideo Get(TransactionManager transactionManager, ShutterTale.Entities.MediaVideoKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Media_Video index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaVideo"/> class.</returns>
		public ShutterTale.Entities.MediaVideo GetById(System.Guid _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Video index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaVideo"/> class.</returns>
		public ShutterTale.Entities.MediaVideo GetById(System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Video index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaVideo"/> class.</returns>
		public ShutterTale.Entities.MediaVideo GetById(TransactionManager transactionManager, System.Guid _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Video index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaVideo"/> class.</returns>
		public ShutterTale.Entities.MediaVideo GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Video index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaVideo"/> class.</returns>
		public ShutterTale.Entities.MediaVideo GetById(System.Guid _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Video index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaVideo"/> class.</returns>
		public abstract ShutterTale.Entities.MediaVideo GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;MediaVideo&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;MediaVideo&gt;"/></returns>
		public static TList<MediaVideo> Fill(IDataReader reader, TList<MediaVideo> rows, int start, int pageLength)
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
				
				ShutterTale.Entities.MediaVideo c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("MediaVideo")
					.Append("|").Append((System.Guid)reader[((int)MediaVideoColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<MediaVideo>(
					key.ToString(), // EntityTrackingKey
					"MediaVideo",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ShutterTale.Entities.MediaVideo();
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
					c.VideoChannels = (System.Byte)reader[((int)MediaVideoColumn.VideoChannels - 1)];
					c.VideoCodec = (System.String)reader[((int)MediaVideoColumn.VideoCodec - 1)];
					c.Id = (System.Guid)reader[((int)MediaVideoColumn.Id - 1)];
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
		/// Refreshes the <see cref="ShutterTale.Entities.MediaVideo"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaVideo"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ShutterTale.Entities.MediaVideo entity)
		{
			if (!reader.Read()) return;
			
			entity.VideoChannels = (System.Byte)reader[((int)MediaVideoColumn.VideoChannels - 1)];
			entity.VideoCodec = (System.String)reader[((int)MediaVideoColumn.VideoCodec - 1)];
			entity.Id = (System.Guid)reader[((int)MediaVideoColumn.Id - 1)];
			entity.OriginalId = (System.Guid)reader["Id"];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.MediaVideo"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaVideo"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ShutterTale.Entities.MediaVideo entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.VideoChannels = (System.Byte)dataRow["VideoChannels"];
			entity.VideoCodec = (System.String)dataRow["VideoCodec"];
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
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaVideo"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ShutterTale.Entities.MediaVideo Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ShutterTale.Entities.MediaVideo entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region IdSource	
			if (CanDeepLoad(entity, "MediaAudio|IdSource", deepLoadType, innerList) 
				&& entity.IdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.Id;
				MediaAudio tmpEntity = EntityManager.LocateEntity<MediaAudio>(EntityLocator.ConstructKeyFromPkItems(typeof(MediaAudio), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.IdSource = tmpEntity;
				else
					entity.IdSource = DataRepository.MediaAudioProvider.GetById(transactionManager, entity.Id);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'IdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.IdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.MediaAudioProvider.DeepLoad(transactionManager, entity.IdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion IdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			
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
		/// Deep Save the entire object graph of the ShutterTale.Entities.MediaVideo object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ShutterTale.Entities.MediaVideo instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ShutterTale.Entities.MediaVideo Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ShutterTale.Entities.MediaVideo entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region IdSource
			if (CanDeepSave(entity, "MediaAudio|IdSource", deepSaveType, innerList) 
				&& entity.IdSource != null)
			{
				DataRepository.MediaAudioProvider.Save(transactionManager, entity.IdSource);
				entity.Id = entity.IdSource.Id;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
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
	
	#region MediaVideoChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ShutterTale.Entities.MediaVideo</c>
	///</summary>
	public enum MediaVideoChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>MediaAudio</c> at IdSource
		///</summary>
		[ChildEntityType(typeof(MediaAudio))]
		MediaAudio,
	}
	
	#endregion MediaVideoChildEntityTypes
	
	#region MediaVideoFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MediaVideoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaVideo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaVideoFilterBuilder : SqlFilterBuilder<MediaVideoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaVideoFilterBuilder class.
		/// </summary>
		public MediaVideoFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaVideoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaVideoFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaVideoFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaVideoFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaVideoFilterBuilder
	
	#region MediaVideoParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MediaVideoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaVideo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaVideoParameterBuilder : ParameterizedSqlFilterBuilder<MediaVideoColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaVideoParameterBuilder class.
		/// </summary>
		public MediaVideoParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaVideoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaVideoParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaVideoParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaVideoParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaVideoParameterBuilder
	
	#region MediaVideoSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MediaVideoColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaVideo"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MediaVideoSortBuilder : SqlSortBuilder<MediaVideoColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaVideoSqlSortBuilder class.
		/// </summary>
		public MediaVideoSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MediaVideoSortBuilder
	
} // end namespace
