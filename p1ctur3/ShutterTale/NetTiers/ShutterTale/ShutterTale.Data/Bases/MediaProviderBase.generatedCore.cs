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
	/// This class is the base class for any <see cref="MediaProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MediaProviderBaseCore : EntityProviderBase<ShutterTale.Entities.Media, ShutterTale.Entities.MediaKey>
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
		public override bool Delete(TransactionManager transactionManager, ShutterTale.Entities.MediaKey key)
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
		public override ShutterTale.Entities.Media Get(TransactionManager transactionManager, ShutterTale.Entities.MediaKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Media index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		public ShutterTale.Entities.Media GetById(System.Guid _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		public ShutterTale.Entities.Media GetById(System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		public ShutterTale.Entities.Media GetById(TransactionManager transactionManager, System.Guid _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		public ShutterTale.Entities.Media GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		public ShutterTale.Entities.Media GetById(System.Guid _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		public abstract ShutterTale.Entities.Media GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;Media&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;Media&gt;"/></returns>
		public static TList<Media> Fill(IDataReader reader, TList<Media> rows, int start, int pageLength)
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
				
				ShutterTale.Entities.Media c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("Media")
					.Append("|").Append((System.Guid)reader[((int)MediaColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<Media>(
					key.ToString(), // EntityTrackingKey
					"Media",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ShutterTale.Entities.Media();
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
					c.Id = (System.Guid)reader[((int)MediaColumn.Id - 1)];
					c.OriginalId = c.Id;
					c.CaptureDateTime = (reader.IsDBNull(((int)MediaColumn.CaptureDateTime - 1)))?null:(System.DateTime?)reader[((int)MediaColumn.CaptureDateTime - 1)];
					c.FileDateTime = (System.DateTime)reader[((int)MediaColumn.FileDateTime - 1)];
					c.ImportDateTime = (System.DateTime)reader[((int)MediaColumn.ImportDateTime - 1)];
					c.Location = (reader.IsDBNull(((int)MediaColumn.Location - 1)))?null:(System.Object)reader[((int)MediaColumn.Location - 1)];
					c.Pixelx = (System.Int32)reader[((int)MediaColumn.Pixelx - 1)];
					c.Pixely = (System.Int32)reader[((int)MediaColumn.Pixely - 1)];
					c.Dpi = (System.Double)reader[((int)MediaColumn.Dpi - 1)];
					c.ContentType = (System.String)reader[((int)MediaColumn.ContentType - 1)];
					c.Make = (reader.IsDBNull(((int)MediaColumn.Make - 1)))?null:(System.String)reader[((int)MediaColumn.Make - 1)];
					c.Model = (reader.IsDBNull(((int)MediaColumn.Model - 1)))?null:(System.String)reader[((int)MediaColumn.Model - 1)];
					c.SoftwareVersion = (reader.IsDBNull(((int)MediaColumn.SoftwareVersion - 1)))?null:(System.String)reader[((int)MediaColumn.SoftwareVersion - 1)];
					c.Origin = (System.String)reader[((int)MediaColumn.Origin - 1)];
					c.Size = (System.Int32)reader[((int)MediaColumn.Size - 1)];
					c.Quadkey18 = (System.String)reader[((int)MediaColumn.Quadkey18 - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.Media"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.Media"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ShutterTale.Entities.Media entity)
		{
			if (!reader.Read()) return;
			
			entity.Id = (System.Guid)reader[((int)MediaColumn.Id - 1)];
			entity.OriginalId = (System.Guid)reader["Id"];
			entity.CaptureDateTime = (reader.IsDBNull(((int)MediaColumn.CaptureDateTime - 1)))?null:(System.DateTime?)reader[((int)MediaColumn.CaptureDateTime - 1)];
			entity.FileDateTime = (System.DateTime)reader[((int)MediaColumn.FileDateTime - 1)];
			entity.ImportDateTime = (System.DateTime)reader[((int)MediaColumn.ImportDateTime - 1)];
			entity.Location = (reader.IsDBNull(((int)MediaColumn.Location - 1)))?null:(System.Object)reader[((int)MediaColumn.Location - 1)];
			entity.Pixelx = (System.Int32)reader[((int)MediaColumn.Pixelx - 1)];
			entity.Pixely = (System.Int32)reader[((int)MediaColumn.Pixely - 1)];
			entity.Dpi = (System.Double)reader[((int)MediaColumn.Dpi - 1)];
			entity.ContentType = (System.String)reader[((int)MediaColumn.ContentType - 1)];
			entity.Make = (reader.IsDBNull(((int)MediaColumn.Make - 1)))?null:(System.String)reader[((int)MediaColumn.Make - 1)];
			entity.Model = (reader.IsDBNull(((int)MediaColumn.Model - 1)))?null:(System.String)reader[((int)MediaColumn.Model - 1)];
			entity.SoftwareVersion = (reader.IsDBNull(((int)MediaColumn.SoftwareVersion - 1)))?null:(System.String)reader[((int)MediaColumn.SoftwareVersion - 1)];
			entity.Origin = (System.String)reader[((int)MediaColumn.Origin - 1)];
			entity.Size = (System.Int32)reader[((int)MediaColumn.Size - 1)];
			entity.Quadkey18 = (System.String)reader[((int)MediaColumn.Quadkey18 - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.Media"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.Media"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ShutterTale.Entities.Media entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Id = (System.Guid)dataRow["Id"];
			entity.OriginalId = (System.Guid)dataRow["Id"];
			entity.CaptureDateTime = Convert.IsDBNull(dataRow["CaptureDateTime"]) ? null : (System.DateTime?)dataRow["CaptureDateTime"];
			entity.FileDateTime = (System.DateTime)dataRow["FileDateTime"];
			entity.ImportDateTime = (System.DateTime)dataRow["ImportDateTime"];
			entity.Location = Convert.IsDBNull(dataRow["Location"]) ? null : (System.Object)dataRow["Location"];
			entity.Pixelx = (System.Int32)dataRow["PixelX"];
			entity.Pixely = (System.Int32)dataRow["PixelY"];
			entity.Dpi = (System.Double)dataRow["Dpi"];
			entity.ContentType = (System.String)dataRow["ContentType"];
			entity.Make = Convert.IsDBNull(dataRow["Make"]) ? null : (System.String)dataRow["Make"];
			entity.Model = Convert.IsDBNull(dataRow["Model"]) ? null : (System.String)dataRow["Model"];
			entity.SoftwareVersion = Convert.IsDBNull(dataRow["SoftwareVersion"]) ? null : (System.String)dataRow["SoftwareVersion"];
			entity.Origin = (System.String)dataRow["Origin"];
			entity.Size = (System.Int32)dataRow["Size"];
			entity.Quadkey18 = (System.String)dataRow["Quadkey18"];
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
		/// <param name="entity">The <see cref="ShutterTale.Entities.Media"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ShutterTale.Entities.Media Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ShutterTale.Entities.Media entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetById methods when available
			
			#region MediaImage
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "MediaImage|MediaImage", deepLoadType, innerList))
			{
				entity.MediaImage = DataRepository.MediaImageProvider.GetById(transactionManager, entity.Id);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MediaImage' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.MediaImage != null)
				{
					deepHandles.Add("MediaImage",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< MediaImage >) DataRepository.MediaImageProvider.DeepLoad,
						new object[] { transactionManager, entity.MediaImage, deep, deepLoadType, childTypes, innerList }
					));
				}
			}
			#endregion 
			
			
			
			#region MediaAudio
			// RelationshipType.OneToOne
			if (CanDeepLoad(entity, "MediaAudio|MediaAudio", deepLoadType, innerList))
			{
				entity.MediaAudio = DataRepository.MediaAudioProvider.GetById(transactionManager, entity.Id);
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'MediaAudio' loaded. key " + entity.EntityTrackingKey);
				#endif 

				if (deep && entity.MediaAudio != null)
				{
					deepHandles.Add("MediaAudio",
						new KeyValuePair<Delegate, object>((DeepLoadSingleHandle< MediaAudio >) DataRepository.MediaAudioProvider.DeepLoad,
						new object[] { transactionManager, entity.MediaAudio, deep, deepLoadType, childTypes, innerList }
					));
				}
			}
			#endregion 
			
			
			
			#region PreviewsCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<Previews>|PreviewsCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'PreviewsCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.PreviewsCollection = DataRepository.PreviewsProvider.GetByMediumId(transactionManager, entity.Id);

				if (deep && entity.PreviewsCollection.Count > 0)
				{
					deepHandles.Add("PreviewsCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<Previews>) DataRepository.PreviewsProvider.DeepLoad,
						new object[] { transactionManager, entity.PreviewsCollection, deep, deepLoadType, childTypes, innerList }
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
		/// Deep Save the entire object graph of the ShutterTale.Entities.Media object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ShutterTale.Entities.Media instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ShutterTale.Entities.Media Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ShutterTale.Entities.Media entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();

			#region MediaImage
			if (CanDeepSave(entity.MediaImage, "MediaImage|MediaImage", deepSaveType, innerList))
			{

				if (entity.MediaImage != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.MediaImage.Id = entity.Id;
					//DataRepository.MediaImageProvider.Save(transactionManager, entity.MediaImage);
					deepHandles.Add("MediaImage",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< MediaImage >) DataRepository.MediaImageProvider.DeepSave,
						new object[] { transactionManager, entity.MediaImage, deepSaveType, childTypes, innerList }
					));
				}
			} 
			#endregion 

			#region MediaAudio
			if (CanDeepSave(entity.MediaAudio, "MediaAudio|MediaAudio", deepSaveType, innerList))
			{

				if (entity.MediaAudio != null)
				{
					// update each child parent id with the real parent id (mostly used on insert)

					entity.MediaAudio.Id = entity.Id;
					//DataRepository.MediaAudioProvider.Save(transactionManager, entity.MediaAudio);
					deepHandles.Add("MediaAudio",
						new KeyValuePair<Delegate, object>((DeepSaveSingleHandle< MediaAudio >) DataRepository.MediaAudioProvider.DeepSave,
						new object[] { transactionManager, entity.MediaAudio, deepSaveType, childTypes, innerList }
					));
				}
			} 
			#endregion 
	
			#region List<Previews>
				if (CanDeepSave(entity.PreviewsCollection, "List<Previews>|PreviewsCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(Previews child in entity.PreviewsCollection)
					{
						if(child.MediumIdSource != null)
						{
							child.MediumId = child.MediumIdSource.Id;
						}
						else
						{
							child.MediumId = entity.Id;
						}

					}

					if (entity.PreviewsCollection.Count > 0 || entity.PreviewsCollection.DeletedItems.Count > 0)
					{
						//DataRepository.PreviewsProvider.Save(transactionManager, entity.PreviewsCollection);
						
						deepHandles.Add("PreviewsCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< Previews >) DataRepository.PreviewsProvider.DeepSave,
							new object[] { transactionManager, entity.PreviewsCollection, deepSaveType, childTypes, innerList }
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
	
	#region MediaChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ShutterTale.Entities.Media</c>
	///</summary>
	public enum MediaChildEntityTypes
	{
		///<summary>
		/// Entity <c>MediaImage</c> as OneToOne for MediaImage
		///</summary>
		[ChildEntityType(typeof(MediaImage))]
		MediaImage,
		///<summary>
		/// Entity <c>MediaAudio</c> as OneToOne for MediaAudio
		///</summary>
		[ChildEntityType(typeof(MediaAudio))]
		MediaAudio,
		///<summary>
		/// Collection of <c>Media</c> as OneToMany for PreviewsCollection
		///</summary>
		[ChildEntityType(typeof(TList<Previews>))]
		PreviewsCollection,
	}
	
	#endregion MediaChildEntityTypes
	
	#region MediaFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MediaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Media"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaFilterBuilder : SqlFilterBuilder<MediaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaFilterBuilder class.
		/// </summary>
		public MediaFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaFilterBuilder
	
	#region MediaParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MediaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Media"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaParameterBuilder : ParameterizedSqlFilterBuilder<MediaColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaParameterBuilder class.
		/// </summary>
		public MediaParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaParameterBuilder
	
	#region MediaSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MediaColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Media"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MediaSortBuilder : SqlSortBuilder<MediaColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaSqlSortBuilder class.
		/// </summary>
		public MediaSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MediaSortBuilder
	
} // end namespace
