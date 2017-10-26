﻿#region Using directives

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
	/// This class is the base class for any <see cref="PreviewsProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class PreviewsProviderBaseCore : EntityProviderBase<ShutterTale.Entities.Previews, ShutterTale.Entities.PreviewsKey>
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
		public override bool Delete(TransactionManager transactionManager, ShutterTale.Entities.PreviewsKey key)
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
		public override ShutterTale.Entities.Previews Get(TransactionManager transactionManager, ShutterTale.Entities.PreviewsKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key IX_FK_MediaPreview index.
		/// </summary>
		/// <param name="_mediumId"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Previews&gt;"/> class.</returns>
		public TList<Previews> GetByMediumId(System.Guid _mediumId)
		{
			int count = -1;
			return GetByMediumId(null,_mediumId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_FK_MediaPreview index.
		/// </summary>
		/// <param name="_mediumId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Previews&gt;"/> class.</returns>
		public TList<Previews> GetByMediumId(System.Guid _mediumId, int start, int pageLength)
		{
			int count = -1;
			return GetByMediumId(null, _mediumId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_FK_MediaPreview index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mediumId"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Previews&gt;"/> class.</returns>
		public TList<Previews> GetByMediumId(TransactionManager transactionManager, System.Guid _mediumId)
		{
			int count = -1;
			return GetByMediumId(transactionManager, _mediumId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_FK_MediaPreview index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mediumId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Previews&gt;"/> class.</returns>
		public TList<Previews> GetByMediumId(TransactionManager transactionManager, System.Guid _mediumId, int start, int pageLength)
		{
			int count = -1;
			return GetByMediumId(transactionManager, _mediumId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_FK_MediaPreview index.
		/// </summary>
		/// <param name="_mediumId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;Previews&gt;"/> class.</returns>
		public TList<Previews> GetByMediumId(System.Guid _mediumId, int start, int pageLength, out int count)
		{
			return GetByMediumId(null, _mediumId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the IX_FK_MediaPreview index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_mediumId"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;Previews&gt;"/> class.</returns>
		public abstract TList<Previews> GetByMediumId(TransactionManager transactionManager, System.Guid _mediumId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Previews index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Previews"/> class.</returns>
		public ShutterTale.Entities.Previews GetById(System.Guid _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Previews index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Previews"/> class.</returns>
		public ShutterTale.Entities.Previews GetById(System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Previews index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Previews"/> class.</returns>
		public ShutterTale.Entities.Previews GetById(TransactionManager transactionManager, System.Guid _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Previews index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Previews"/> class.</returns>
		public ShutterTale.Entities.Previews GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Previews index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Previews"/> class.</returns>
		public ShutterTale.Entities.Previews GetById(System.Guid _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Previews index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Previews"/> class.</returns>
		public abstract ShutterTale.Entities.Previews GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Previews&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Previews&gt;"/></returns>
		public static TList<Previews> Fill(IDataReader reader, TList<Previews> rows, int start, int pageLength)
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
				
				ShutterTale.Entities.Previews c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Previews")
					.Append("|").Append((System.Guid)reader[((int)PreviewsColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Previews>(
					key.ToString(), // EntityTrackingKey
					"Previews",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ShutterTale.Entities.Previews();
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
					c.Id = (System.Guid)reader[((int)PreviewsColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.Level0 = (reader.IsDBNull(((int)PreviewsColumn.Level0 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level0 - 1)];
					c.Level1 = (reader.IsDBNull(((int)PreviewsColumn.Level1 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level1 - 1)];
					c.Level2 = (reader.IsDBNull(((int)PreviewsColumn.Level2 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level2 - 1)];
					c.Level3 = (reader.IsDBNull(((int)PreviewsColumn.Level3 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level3 - 1)];
					c.PreviewType = (System.String)reader[((int)PreviewsColumn.PreviewType - 1)];
					c.MediumId = (System.Guid)reader[((int)PreviewsColumn.MediumId - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.Previews"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.Previews"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ShutterTale.Entities.Previews entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Guid)reader[((int)PreviewsColumn.Id - 1)];
			entity.OriginalId = (System.Guid)reader["Id"];
			entity.Level0 = (reader.IsDBNull(((int)PreviewsColumn.Level0 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level0 - 1)];
			entity.Level1 = (reader.IsDBNull(((int)PreviewsColumn.Level1 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level1 - 1)];
			entity.Level2 = (reader.IsDBNull(((int)PreviewsColumn.Level2 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level2 - 1)];
			entity.Level3 = (reader.IsDBNull(((int)PreviewsColumn.Level3 - 1)))?null:(System.Byte[])reader[((int)PreviewsColumn.Level3 - 1)];
			entity.PreviewType = (System.String)reader[((int)PreviewsColumn.PreviewType - 1)];
			entity.MediumId = (System.Guid)reader[((int)PreviewsColumn.MediumId - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.Previews"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.Previews"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ShutterTale.Entities.Previews entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Guid)dataRow["Id"];
			entity.OriginalId = (System.Guid)dataRow["Id"];
			entity.Level0 = Convert.IsDBNull(dataRow["Level0"]) ? null : (System.Byte[])dataRow["Level0"];
			entity.Level1 = Convert.IsDBNull(dataRow["Level1"]) ? null : (System.Byte[])dataRow["Level1"];
			entity.Level2 = Convert.IsDBNull(dataRow["Level2"]) ? null : (System.Byte[])dataRow["Level2"];
			entity.Level3 = Convert.IsDBNull(dataRow["Level3"]) ? null : (System.Byte[])dataRow["Level3"];
			entity.PreviewType = (System.String)dataRow["PreviewType"];
			entity.MediumId = (System.Guid)dataRow["Medium_Id"];
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
		/// <param name="entity">The <see cref="ShutterTale.Entities.Previews"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ShutterTale.Entities.Previews Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ShutterTale.Entities.Previews entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region MediumIdSource	
			if (CanDeepLoad(entity, "Media|MediumIdSource", deepLoadType, innerList) 
				&& entity.MediumIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.MediumId;
				Media tmpEntity = EntityManager.LocateEntity<Media>(EntityLocator.ConstructKeyFromPkItems(typeof(Media), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.MediumIdSource = tmpEntity;
				else
					entity.MediumIdSource = DataRepository.MediaProvider.GetById(transactionManager, entity.MediumId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MediumIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.MediumIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.MediaProvider.DeepLoad(transactionManager, entity.MediumIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion MediumIdSource
			
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
		/// Deep Save the entire object graph of the ShutterTale.Entities.Previews object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ShutterTale.Entities.Previews instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ShutterTale.Entities.Previews Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ShutterTale.Entities.Previews entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region MediumIdSource
			if (CanDeepSave(entity, "Media|MediumIdSource", deepSaveType, innerList) 
				&& entity.MediumIdSource != null)
			{
				DataRepository.MediaProvider.Save(transactionManager, entity.MediumIdSource);
				entity.MediumId = entity.MediumIdSource.Id;
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
	
	#region PreviewsChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ShutterTale.Entities.Previews</c>
	///</summary>
	public enum PreviewsChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Media</c> at MediumIdSource
		///</summary>
		[ChildEntityType(typeof(Media))]
		Media,
	}
	
	#endregion PreviewsChildEntityTypes
	
	#region PreviewsFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;PreviewsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Previews"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PreviewsFilterBuilder : SqlFilterBuilder<PreviewsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PreviewsFilterBuilder class.
		/// </summary>
		public PreviewsFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PreviewsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PreviewsFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PreviewsFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PreviewsFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PreviewsFilterBuilder
	
	#region PreviewsParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;PreviewsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Previews"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PreviewsParameterBuilder : ParameterizedSqlFilterBuilder<PreviewsColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PreviewsParameterBuilder class.
		/// </summary>
		public PreviewsParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the PreviewsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public PreviewsParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the PreviewsParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public PreviewsParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion PreviewsParameterBuilder
	
	#region PreviewsSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;PreviewsColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Previews"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class PreviewsSortBuilder : SqlSortBuilder<PreviewsColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PreviewsSqlSortBuilder class.
		/// </summary>
		public PreviewsSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion PreviewsSortBuilder
	
} // end namespace
