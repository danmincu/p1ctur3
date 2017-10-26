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
	/// This class is the base class for any <see cref="MediaImageProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class MediaImageProviderBaseCore : EntityProviderBase<ShutterTale.Entities.MediaImage, ShutterTale.Entities.MediaImageKey>
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
		public override bool Delete(TransactionManager transactionManager, ShutterTale.Entities.MediaImageKey key)
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
		public override ShutterTale.Entities.MediaImage Get(TransactionManager transactionManager, ShutterTale.Entities.MediaImageKey key, int start, int pageLength)
		{
			return GetById(transactionManager, key.Id, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_Media_Image index.
		/// </summary>
		/// <param name="_id"></param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaImage"/> class.</returns>
		public ShutterTale.Entities.MediaImage GetById(System.Guid _id)
		{
			int count = -1;
			return GetById(null,_id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Image index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaImage"/> class.</returns>
		public ShutterTale.Entities.MediaImage GetById(System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(null, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Image index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaImage"/> class.</returns>
		public ShutterTale.Entities.MediaImage GetById(TransactionManager transactionManager, System.Guid _id)
		{
			int count = -1;
			return GetById(transactionManager, _id, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Image index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaImage"/> class.</returns>
		public ShutterTale.Entities.MediaImage GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength)
		{
			int count = -1;
			return GetById(transactionManager, _id, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Image index.
		/// </summary>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaImage"/> class.</returns>
		public ShutterTale.Entities.MediaImage GetById(System.Guid _id, int start, int pageLength, out int count)
		{
			return GetById(null, _id, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Image index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaImage"/> class.</returns>
		public abstract ShutterTale.Entities.MediaImage GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;MediaImage&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;MediaImage&gt;"/></returns>
		public static TList<MediaImage> Fill(IDataReader reader, TList<MediaImage> rows, int start, int pageLength)
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
				
				ShutterTale.Entities.MediaImage c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("MediaImage")
					.Append("|").Append((System.Guid)reader[((int)MediaImageColumn.Id - 1)]).ToString();
					c = EntityManager.LocateOrCreate<MediaImage>(
					key.ToString(), // EntityTrackingKey
					"MediaImage",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new ShutterTale.Entities.MediaImage();
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
					c.Orientation = (reader.IsDBNull(((int)MediaImageColumn.Orientation - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.Orientation - 1)];
					c.YcbCrPositioning = (reader.IsDBNull(((int)MediaImageColumn.YcbCrPositioning - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.YcbCrPositioning - 1)];
					c.ExposureTime = (reader.IsDBNull(((int)MediaImageColumn.ExposureTime - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.ExposureTime - 1)];
					c.Fnumber = (reader.IsDBNull(((int)MediaImageColumn.Fnumber - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.Fnumber - 1)];
					c.ExposureProgram = (reader.IsDBNull(((int)MediaImageColumn.ExposureProgram - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.ExposureProgram - 1)];
					c.IsoSpeedRatings = (reader.IsDBNull(((int)MediaImageColumn.IsoSpeedRatings - 1)))?null:(System.Int16?)reader[((int)MediaImageColumn.IsoSpeedRatings - 1)];
					c.ShutterSpeedValue = (reader.IsDBNull(((int)MediaImageColumn.ShutterSpeedValue - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.ShutterSpeedValue - 1)];
					c.ApertureValue = (reader.IsDBNull(((int)MediaImageColumn.ApertureValue - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.ApertureValue - 1)];
					c.MeteringMode = (reader.IsDBNull(((int)MediaImageColumn.MeteringMode - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.MeteringMode - 1)];
					c.Flash = (reader.IsDBNull(((int)MediaImageColumn.Flash - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.Flash - 1)];
					c.FocalLength = (reader.IsDBNull(((int)MediaImageColumn.FocalLength - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.FocalLength - 1)];
					c.FlashpixVersion = (reader.IsDBNull(((int)MediaImageColumn.FlashpixVersion - 1)))?null:(System.String)reader[((int)MediaImageColumn.FlashpixVersion - 1)];
					c.ColorSpace = (reader.IsDBNull(((int)MediaImageColumn.ColorSpace - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.ColorSpace - 1)];
					c.SensingMethod = (reader.IsDBNull(((int)MediaImageColumn.SensingMethod - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.SensingMethod - 1)];
					c.ExposureMode = (reader.IsDBNull(((int)MediaImageColumn.ExposureMode - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.ExposureMode - 1)];
					c.WhiteBalance = (reader.IsDBNull(((int)MediaImageColumn.WhiteBalance - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.WhiteBalance - 1)];
					c.SceneCaptureType = (reader.IsDBNull(((int)MediaImageColumn.SceneCaptureType - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.SceneCaptureType - 1)];
					c.Sharpness = (reader.IsDBNull(((int)MediaImageColumn.Sharpness - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.Sharpness - 1)];
					c.Id = (System.Guid)reader[((int)MediaImageColumn.Id - 1)];
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
		/// Refreshes the <see cref="ShutterTale.Entities.MediaImage"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaImage"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, ShutterTale.Entities.MediaImage entity)
		{
			if (!reader.Read()) return;
			
			entity.Orientation = (reader.IsDBNull(((int)MediaImageColumn.Orientation - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.Orientation - 1)];
			entity.YcbCrPositioning = (reader.IsDBNull(((int)MediaImageColumn.YcbCrPositioning - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.YcbCrPositioning - 1)];
			entity.ExposureTime = (reader.IsDBNull(((int)MediaImageColumn.ExposureTime - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.ExposureTime - 1)];
			entity.Fnumber = (reader.IsDBNull(((int)MediaImageColumn.Fnumber - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.Fnumber - 1)];
			entity.ExposureProgram = (reader.IsDBNull(((int)MediaImageColumn.ExposureProgram - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.ExposureProgram - 1)];
			entity.IsoSpeedRatings = (reader.IsDBNull(((int)MediaImageColumn.IsoSpeedRatings - 1)))?null:(System.Int16?)reader[((int)MediaImageColumn.IsoSpeedRatings - 1)];
			entity.ShutterSpeedValue = (reader.IsDBNull(((int)MediaImageColumn.ShutterSpeedValue - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.ShutterSpeedValue - 1)];
			entity.ApertureValue = (reader.IsDBNull(((int)MediaImageColumn.ApertureValue - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.ApertureValue - 1)];
			entity.MeteringMode = (reader.IsDBNull(((int)MediaImageColumn.MeteringMode - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.MeteringMode - 1)];
			entity.Flash = (reader.IsDBNull(((int)MediaImageColumn.Flash - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.Flash - 1)];
			entity.FocalLength = (reader.IsDBNull(((int)MediaImageColumn.FocalLength - 1)))?null:(System.Double?)reader[((int)MediaImageColumn.FocalLength - 1)];
			entity.FlashpixVersion = (reader.IsDBNull(((int)MediaImageColumn.FlashpixVersion - 1)))?null:(System.String)reader[((int)MediaImageColumn.FlashpixVersion - 1)];
			entity.ColorSpace = (reader.IsDBNull(((int)MediaImageColumn.ColorSpace - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.ColorSpace - 1)];
			entity.SensingMethod = (reader.IsDBNull(((int)MediaImageColumn.SensingMethod - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.SensingMethod - 1)];
			entity.ExposureMode = (reader.IsDBNull(((int)MediaImageColumn.ExposureMode - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.ExposureMode - 1)];
			entity.WhiteBalance = (reader.IsDBNull(((int)MediaImageColumn.WhiteBalance - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.WhiteBalance - 1)];
			entity.SceneCaptureType = (reader.IsDBNull(((int)MediaImageColumn.SceneCaptureType - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.SceneCaptureType - 1)];
			entity.Sharpness = (reader.IsDBNull(((int)MediaImageColumn.Sharpness - 1)))?null:(System.Byte?)reader[((int)MediaImageColumn.Sharpness - 1)];
			entity.Id = (System.Guid)reader[((int)MediaImageColumn.Id - 1)];
			entity.OriginalId = (System.Guid)reader["Id"];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="ShutterTale.Entities.MediaImage"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaImage"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, ShutterTale.Entities.MediaImage entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Orientation = Convert.IsDBNull(dataRow["Orientation"]) ? null : (System.Byte?)dataRow["Orientation"];
			entity.YcbCrPositioning = Convert.IsDBNull(dataRow["YCbCrPositioning"]) ? null : (System.Byte?)dataRow["YCbCrPositioning"];
			entity.ExposureTime = Convert.IsDBNull(dataRow["ExposureTime"]) ? null : (System.Double?)dataRow["ExposureTime"];
			entity.Fnumber = Convert.IsDBNull(dataRow["FNumber"]) ? null : (System.Double?)dataRow["FNumber"];
			entity.ExposureProgram = Convert.IsDBNull(dataRow["ExposureProgram"]) ? null : (System.Byte?)dataRow["ExposureProgram"];
			entity.IsoSpeedRatings = Convert.IsDBNull(dataRow["ISOSpeedRatings"]) ? null : (System.Int16?)dataRow["ISOSpeedRatings"];
			entity.ShutterSpeedValue = Convert.IsDBNull(dataRow["ShutterSpeedValue"]) ? null : (System.Double?)dataRow["ShutterSpeedValue"];
			entity.ApertureValue = Convert.IsDBNull(dataRow["ApertureValue"]) ? null : (System.Double?)dataRow["ApertureValue"];
			entity.MeteringMode = Convert.IsDBNull(dataRow["MeteringMode"]) ? null : (System.Byte?)dataRow["MeteringMode"];
			entity.Flash = Convert.IsDBNull(dataRow["Flash"]) ? null : (System.Byte?)dataRow["Flash"];
			entity.FocalLength = Convert.IsDBNull(dataRow["FocalLength"]) ? null : (System.Double?)dataRow["FocalLength"];
			entity.FlashpixVersion = Convert.IsDBNull(dataRow["FlashpixVersion"]) ? null : (System.String)dataRow["FlashpixVersion"];
			entity.ColorSpace = Convert.IsDBNull(dataRow["ColorSpace"]) ? null : (System.Byte?)dataRow["ColorSpace"];
			entity.SensingMethod = Convert.IsDBNull(dataRow["SensingMethod"]) ? null : (System.Byte?)dataRow["SensingMethod"];
			entity.ExposureMode = Convert.IsDBNull(dataRow["ExposureMode"]) ? null : (System.Byte?)dataRow["ExposureMode"];
			entity.WhiteBalance = Convert.IsDBNull(dataRow["WhiteBalance"]) ? null : (System.Byte?)dataRow["WhiteBalance"];
			entity.SceneCaptureType = Convert.IsDBNull(dataRow["SceneCaptureType"]) ? null : (System.Byte?)dataRow["SceneCaptureType"];
			entity.Sharpness = Convert.IsDBNull(dataRow["Sharpness"]) ? null : (System.Byte?)dataRow["Sharpness"];
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
		/// <param name="entity">The <see cref="ShutterTale.Entities.MediaImage"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">ShutterTale.Entities.MediaImage Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, ShutterTale.Entities.MediaImage entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
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
		/// Deep Save the entire object graph of the ShutterTale.Entities.MediaImage object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">ShutterTale.Entities.MediaImage instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">ShutterTale.Entities.MediaImage Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, ShutterTale.Entities.MediaImage entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
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
	
	#region MediaImageChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>ShutterTale.Entities.MediaImage</c>
	///</summary>
	public enum MediaImageChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>Media</c> at IdSource
		///</summary>
		[ChildEntityType(typeof(Media))]
		Media,
	}
	
	#endregion MediaImageChildEntityTypes
	
	#region MediaImageFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;MediaImageColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaImage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaImageFilterBuilder : SqlFilterBuilder<MediaImageColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaImageFilterBuilder class.
		/// </summary>
		public MediaImageFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaImageFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaImageFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaImageFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaImageFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaImageFilterBuilder
	
	#region MediaImageParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;MediaImageColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaImage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaImageParameterBuilder : ParameterizedSqlFilterBuilder<MediaImageColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaImageParameterBuilder class.
		/// </summary>
		public MediaImageParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the MediaImageParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MediaImageParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MediaImageParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MediaImageParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MediaImageParameterBuilder
	
	#region MediaImageSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;MediaImageColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaImage"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class MediaImageSortBuilder : SqlSortBuilder<MediaImageColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaImageSqlSortBuilder class.
		/// </summary>
		public MediaImageSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion MediaImageSortBuilder
	
} // end namespace
