<%@ WebService Language="C#" Class="ShutterTaleServices" %>
<%@ Assembly Name="ShutterTale.Entities" %>
<%@ Assembly Name="ShutterTale.Data" %>

<%@ Assembly Name="ShutterTale.Data.SqlClient" %>

using System;
using System.Data;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using ShutterTale.Entities;
using ShutterTale.Data;
using ShutterTale.Data.SqlClient;


/// <summary>
///	Exposes CRUD webmethods for the ShutterTaleServices Database.
/// </summary>
[WebService(Namespace="http://localhost/ShutterTale.NetTiers/", Description="Exposes CRUD webmethods for the ShutterTaleServices Database.")]
public class ShutterTaleServices : WebService 
{
	
	/// <summary>
    /// used in exception handling for WebServices
    /// </summary>
    public enum FaultCode
    {
        /// <summary>
        /// Fault is on the client
        /// </summary>
        Client,
        /// <summary>
        /// Fault is on the server
        /// </summary>
        Server
    }


	
	#region Get from  Many To Many Relationship Functions
	#endregion	
	
	#region Delete Functions
		
	/// <summary>
	/// 	Deletes a row from the DataSource.
	/// </summary>
	/// <param name="Id">. Primary Key.</param>	
	/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
	/// <param name="connectionString">Connection string to datasource.</param>
	/// <remarks>Deletes based on primary key(s).</remarks>
	/// <returns>Returns true if operation suceeded.</returns>
	[WebMethod(Description="Delete a row from the table Media.")]
	public bool MediaProvider_Delete(System.Guid _id)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaProvider.Delete(_id);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_Delete", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion
	
	#region Find Methods
	
	/// <summary>
	/// 	Returns rows meeting the whereclause condition from the DataSource.
	/// </summary>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query.</param>
	/// <remarks></remarks>
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media with additional query text.")]
	public ShutterTale.Entities.TList<Media> MediaProvider_Find(string whereClause, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaProvider.Find((TransactionManager) null, whereClause, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_Find", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion Find Methods
	
	
	#region GetAll Methods
		
	/// <summary>
	/// 	Gets All rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <returns>Returns a <s>DataSet</s>.</returns>
	[WebMethod(Description="Get all rows from the table Media.")]
	public ShutterTale.Entities.TList<Media> MediaProvider_GetAll(int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaProvider.GetAll(start, pageLength, out count);		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_GetAll", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}		
	}
	
	#endregion
	
	#region GetPaged Methods
	
	/// <summary>
	/// Gets a page of rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pageLength">Number of rows to return.</param>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
	/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <remarks></remarks>
	/// <returns>Returns a typed collection of ShutterTaleServices objects.</returns>
	[WebMethod(Description="Get all rows from the table Media.")]
	public ShutterTale.Entities.TList<Media> MediaProvider_GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaProvider.GetPaged(whereClause, orderBy, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_GetPaged", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion
		
	#region Get By Foreign Key Functions
#endregion
	
	#region Get By Index Functions
	
	/// <summary>
	/// 	Gets rows from the datasource based on the PK_Media index.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="Id"></param>
	/// <param name="count">out parameter to get total records for query</param>	
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media filtered by the column Id that is part of the PK_Media index.")]
	public ShutterTale.Entities.Media MediaProvider_GetById(System.Guid _id, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaProvider.GetById(_id, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_GetById", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	

	#endregion Get By Index Functions
	
	#region Insert Methods
		
	/// <summary>
	/// 	Inserts an object into the datasource.
	/// </summary>	
	/// <remarks>After inserting into the datasource, the object will be returned
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a row in the table Media.")]
	public ShutterTale.Entities.Media MediaProvider_Insert(ShutterTale.Entities.Media entity )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaProvider.Insert(entity);
		return entity;		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_Insert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	/// <summary>
	/// Inserts a ShutterTale.Entities.TList<Media> object into the datasource using a transaction.
	/// </summary>
	/// <param name="entity">ShutterTale.Entities.TList<Media> object to insert.</param>
	/// <remarks>After inserting into the datasource, the ShutterTale.Entities.Media object will be updated
	/// to refelect any changes made by the datasource. (ie: identity or computed columns)
	/// </remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a Bulk set of rows into the table Media.")]
	public void MediaProvider_BulkInsert(ShutterTale.Entities.TList<Media> entityList )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaProvider.BulkInsert(entityList);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_BulkInsert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	#endregion Insert Methods
			
	#region Update Methods
		
	/// <summary>
	/// 	Update an existing row in the datasource.
	/// </summary>
	/// <param name="entity"> object to update.</param>
	/// <remarks>After updating the datasource, the object will be updated
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Update a row in the table Media.")]
	public ShutterTale.Entities.Media MediaProvider_Update(ShutterTale.Entities.Media entity)
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaProvider.Update(entity);
		return entity;
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaProvider_Update", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion Update Methods

	#region Custom Methods
	
	
	#endregion


	
	#region Get from  Many To Many Relationship Functions
	#endregion	
	
	#region Delete Functions
		
	/// <summary>
	/// 	Deletes a row from the DataSource.
	/// </summary>
	/// <param name="Id">. Primary Key.</param>	
	/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
	/// <param name="connectionString">Connection string to datasource.</param>
	/// <remarks>Deletes based on primary key(s).</remarks>
	/// <returns>Returns true if operation suceeded.</returns>
	[WebMethod(Description="Delete a row from the table Media_Image.")]
	public bool MediaImageProvider_Delete(System.Guid _id)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaImageProvider.Delete(_id);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_Delete", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion
	
	#region Find Methods
	
	/// <summary>
	/// 	Returns rows meeting the whereclause condition from the DataSource.
	/// </summary>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query.</param>
	/// <remarks></remarks>
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media_Image with additional query text.")]
	public ShutterTale.Entities.TList<MediaImage> MediaImageProvider_Find(string whereClause, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaImageProvider.Find((TransactionManager) null, whereClause, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_Find", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion Find Methods
	
	
	#region GetAll Methods
		
	/// <summary>
	/// 	Gets All rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <returns>Returns a <s>DataSet</s>.</returns>
	[WebMethod(Description="Get all rows from the table Media_Image.")]
	public ShutterTale.Entities.TList<MediaImage> MediaImageProvider_GetAll(int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaImageProvider.GetAll(start, pageLength, out count);		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_GetAll", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}		
	}
	
	#endregion
	
	#region GetPaged Methods
	
	/// <summary>
	/// Gets a page of rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pageLength">Number of rows to return.</param>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
	/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <remarks></remarks>
	/// <returns>Returns a typed collection of ShutterTaleServices objects.</returns>
	[WebMethod(Description="Get all rows from the table Media_Image.")]
	public ShutterTale.Entities.TList<MediaImage> MediaImageProvider_GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaImageProvider.GetPaged(whereClause, orderBy, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_GetPaged", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion
		
	#region Get By Foreign Key Functions
#endregion
	
	#region Get By Index Functions
	
	/// <summary>
	/// 	Gets rows from the datasource based on the PK_Media_Image index.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="Id"></param>
	/// <param name="count">out parameter to get total records for query</param>	
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media_Image filtered by the column Id that is part of the PK_Media_Image index.")]
	public ShutterTale.Entities.MediaImage MediaImageProvider_GetById(System.Guid _id, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaImageProvider.GetById(_id, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_GetById", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	

	#endregion Get By Index Functions
	
	#region Insert Methods
		
	/// <summary>
	/// 	Inserts an object into the datasource.
	/// </summary>	
	/// <remarks>After inserting into the datasource, the object will be returned
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a row in the table Media_Image.")]
	public ShutterTale.Entities.MediaImage MediaImageProvider_Insert(ShutterTale.Entities.MediaImage entity )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaImageProvider.Insert(entity);
		return entity;		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_Insert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	/// <summary>
	/// Inserts a ShutterTale.Entities.TList<MediaImage> object into the datasource using a transaction.
	/// </summary>
	/// <param name="entity">ShutterTale.Entities.TList<MediaImage> object to insert.</param>
	/// <remarks>After inserting into the datasource, the ShutterTale.Entities.MediaImage object will be updated
	/// to refelect any changes made by the datasource. (ie: identity or computed columns)
	/// </remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a Bulk set of rows into the table Media_Image.")]
	public void MediaImageProvider_BulkInsert(ShutterTale.Entities.TList<MediaImage> entityList )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaImageProvider.BulkInsert(entityList);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_BulkInsert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	#endregion Insert Methods
			
	#region Update Methods
		
	/// <summary>
	/// 	Update an existing row in the datasource.
	/// </summary>
	/// <param name="entity"> object to update.</param>
	/// <remarks>After updating the datasource, the object will be updated
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Update a row in the table Media_Image.")]
	public ShutterTale.Entities.MediaImage MediaImageProvider_Update(ShutterTale.Entities.MediaImage entity)
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaImageProvider.Update(entity);
		return entity;
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaImageProvider_Update", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion Update Methods

	#region Custom Methods
	
	
	#endregion


	
	#region Get from  Many To Many Relationship Functions
	#endregion	
	
	#region Delete Functions
		
	/// <summary>
	/// 	Deletes a row from the DataSource.
	/// </summary>
	/// <param name="Id">. Primary Key.</param>	
	/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
	/// <param name="connectionString">Connection string to datasource.</param>
	/// <remarks>Deletes based on primary key(s).</remarks>
	/// <returns>Returns true if operation suceeded.</returns>
	[WebMethod(Description="Delete a row from the table Media_Video.")]
	public bool MediaVideoProvider_Delete(System.Guid _id)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaVideoProvider.Delete(_id);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_Delete", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion
	
	#region Find Methods
	
	/// <summary>
	/// 	Returns rows meeting the whereclause condition from the DataSource.
	/// </summary>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query.</param>
	/// <remarks></remarks>
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media_Video with additional query text.")]
	public ShutterTale.Entities.TList<MediaVideo> MediaVideoProvider_Find(string whereClause, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaVideoProvider.Find((TransactionManager) null, whereClause, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_Find", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion Find Methods
	
	
	#region GetAll Methods
		
	/// <summary>
	/// 	Gets All rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <returns>Returns a <s>DataSet</s>.</returns>
	[WebMethod(Description="Get all rows from the table Media_Video.")]
	public ShutterTale.Entities.TList<MediaVideo> MediaVideoProvider_GetAll(int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaVideoProvider.GetAll(start, pageLength, out count);		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_GetAll", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}		
	}
	
	#endregion
	
	#region GetPaged Methods
	
	/// <summary>
	/// Gets a page of rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pageLength">Number of rows to return.</param>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
	/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <remarks></remarks>
	/// <returns>Returns a typed collection of ShutterTaleServices objects.</returns>
	[WebMethod(Description="Get all rows from the table Media_Video.")]
	public ShutterTale.Entities.TList<MediaVideo> MediaVideoProvider_GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaVideoProvider.GetPaged(whereClause, orderBy, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_GetPaged", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion
		
	#region Get By Foreign Key Functions
#endregion
	
	#region Get By Index Functions
	
	/// <summary>
	/// 	Gets rows from the datasource based on the PK_Media_Video index.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="Id"></param>
	/// <param name="count">out parameter to get total records for query</param>	
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media_Video filtered by the column Id that is part of the PK_Media_Video index.")]
	public ShutterTale.Entities.MediaVideo MediaVideoProvider_GetById(System.Guid _id, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaVideoProvider.GetById(_id, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_GetById", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	

	#endregion Get By Index Functions
	
	#region Insert Methods
		
	/// <summary>
	/// 	Inserts an object into the datasource.
	/// </summary>	
	/// <remarks>After inserting into the datasource, the object will be returned
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a row in the table Media_Video.")]
	public ShutterTale.Entities.MediaVideo MediaVideoProvider_Insert(ShutterTale.Entities.MediaVideo entity )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaVideoProvider.Insert(entity);
		return entity;		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_Insert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	/// <summary>
	/// Inserts a ShutterTale.Entities.TList<MediaVideo> object into the datasource using a transaction.
	/// </summary>
	/// <param name="entity">ShutterTale.Entities.TList<MediaVideo> object to insert.</param>
	/// <remarks>After inserting into the datasource, the ShutterTale.Entities.MediaVideo object will be updated
	/// to refelect any changes made by the datasource. (ie: identity or computed columns)
	/// </remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a Bulk set of rows into the table Media_Video.")]
	public void MediaVideoProvider_BulkInsert(ShutterTale.Entities.TList<MediaVideo> entityList )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaVideoProvider.BulkInsert(entityList);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_BulkInsert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	#endregion Insert Methods
			
	#region Update Methods
		
	/// <summary>
	/// 	Update an existing row in the datasource.
	/// </summary>
	/// <param name="entity"> object to update.</param>
	/// <remarks>After updating the datasource, the object will be updated
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Update a row in the table Media_Video.")]
	public ShutterTale.Entities.MediaVideo MediaVideoProvider_Update(ShutterTale.Entities.MediaVideo entity)
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaVideoProvider.Update(entity);
		return entity;
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaVideoProvider_Update", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion Update Methods

	#region Custom Methods
	
	
	#endregion


	
	#region Get from  Many To Many Relationship Functions
	#endregion	
	
	#region Delete Functions
		
	/// <summary>
	/// 	Deletes a row from the DataSource.
	/// </summary>
	/// <param name="Id">. Primary Key.</param>	
	/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
	/// <param name="connectionString">Connection string to datasource.</param>
	/// <remarks>Deletes based on primary key(s).</remarks>
	/// <returns>Returns true if operation suceeded.</returns>
	[WebMethod(Description="Delete a row from the table Media_Audio.")]
	public bool MediaAudioProvider_Delete(System.Guid _id)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaAudioProvider.Delete(_id);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_Delete", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion
	
	#region Find Methods
	
	/// <summary>
	/// 	Returns rows meeting the whereclause condition from the DataSource.
	/// </summary>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query.</param>
	/// <remarks></remarks>
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media_Audio with additional query text.")]
	public ShutterTale.Entities.TList<MediaAudio> MediaAudioProvider_Find(string whereClause, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaAudioProvider.Find((TransactionManager) null, whereClause, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_Find", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion Find Methods
	
	
	#region GetAll Methods
		
	/// <summary>
	/// 	Gets All rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <returns>Returns a <s>DataSet</s>.</returns>
	[WebMethod(Description="Get all rows from the table Media_Audio.")]
	public ShutterTale.Entities.TList<MediaAudio> MediaAudioProvider_GetAll(int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaAudioProvider.GetAll(start, pageLength, out count);		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_GetAll", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}		
	}
	
	#endregion
	
	#region GetPaged Methods
	
	/// <summary>
	/// Gets a page of rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pageLength">Number of rows to return.</param>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
	/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <remarks></remarks>
	/// <returns>Returns a typed collection of ShutterTaleServices objects.</returns>
	[WebMethod(Description="Get all rows from the table Media_Audio.")]
	public ShutterTale.Entities.TList<MediaAudio> MediaAudioProvider_GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaAudioProvider.GetPaged(whereClause, orderBy, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_GetPaged", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion
		
	#region Get By Foreign Key Functions
#endregion
	
	#region Get By Index Functions
	
	/// <summary>
	/// 	Gets rows from the datasource based on the PK_Media_Audio index.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="Id"></param>
	/// <param name="count">out parameter to get total records for query</param>	
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Media_Audio filtered by the column Id that is part of the PK_Media_Audio index.")]
	public ShutterTale.Entities.MediaAudio MediaAudioProvider_GetById(System.Guid _id, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.MediaAudioProvider.GetById(_id, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_GetById", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	

	#endregion Get By Index Functions
	
	#region Insert Methods
		
	/// <summary>
	/// 	Inserts an object into the datasource.
	/// </summary>	
	/// <remarks>After inserting into the datasource, the object will be returned
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a row in the table Media_Audio.")]
	public ShutterTale.Entities.MediaAudio MediaAudioProvider_Insert(ShutterTale.Entities.MediaAudio entity )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaAudioProvider.Insert(entity);
		return entity;		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_Insert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	/// <summary>
	/// Inserts a ShutterTale.Entities.TList<MediaAudio> object into the datasource using a transaction.
	/// </summary>
	/// <param name="entity">ShutterTale.Entities.TList<MediaAudio> object to insert.</param>
	/// <remarks>After inserting into the datasource, the ShutterTale.Entities.MediaAudio object will be updated
	/// to refelect any changes made by the datasource. (ie: identity or computed columns)
	/// </remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a Bulk set of rows into the table Media_Audio.")]
	public void MediaAudioProvider_BulkInsert(ShutterTale.Entities.TList<MediaAudio> entityList )
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaAudioProvider.BulkInsert(entityList);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_BulkInsert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	#endregion Insert Methods
			
	#region Update Methods
		
	/// <summary>
	/// 	Update an existing row in the datasource.
	/// </summary>
	/// <param name="entity"> object to update.</param>
	/// <remarks>After updating the datasource, the object will be updated
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Update a row in the table Media_Audio.")]
	public ShutterTale.Entities.MediaAudio MediaAudioProvider_Update(ShutterTale.Entities.MediaAudio entity)
	{
		try
		{
		ShutterTale.Data.DataRepository.MediaAudioProvider.Update(entity);
		return entity;
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("MediaAudioProvider_Update", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion Update Methods

	#region Custom Methods
	
	
	#endregion


	
	#region Get from  Many To Many Relationship Functions
	#endregion	
	
	#region Delete Functions
		
	/// <summary>
	/// 	Deletes a row from the DataSource.
	/// </summary>
	/// <param name="Id">. Primary Key.</param>	
	/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
	/// <param name="connectionString">Connection string to datasource.</param>
	/// <remarks>Deletes based on primary key(s).</remarks>
	/// <returns>Returns true if operation suceeded.</returns>
	[WebMethod(Description="Delete a row from the table Previews.")]
	public bool PreviewsProvider_Delete(System.Guid _id)
	{
		try
		{
		return ShutterTale.Data.DataRepository.PreviewsProvider.Delete(_id);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_Delete", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion
	
	#region Find Methods
	
	/// <summary>
	/// 	Returns rows meeting the whereclause condition from the DataSource.
	/// </summary>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query.</param>
	/// <remarks></remarks>
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Previews with additional query text.")]
	public ShutterTale.Entities.TList<Previews> PreviewsProvider_Find(string whereClause, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.PreviewsProvider.Find((TransactionManager) null, whereClause, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_Find", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	#endregion Find Methods
	
	
	#region GetAll Methods
		
	/// <summary>
	/// 	Gets All rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <returns>Returns a <s>DataSet</s>.</returns>
	[WebMethod(Description="Get all rows from the table Previews.")]
	public ShutterTale.Entities.TList<Previews> PreviewsProvider_GetAll(int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.PreviewsProvider.GetAll(start, pageLength, out count);		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_GetAll", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}		
	}
	
	#endregion
	
	#region GetPaged Methods
	
	/// <summary>
	/// Gets a page of rows from the DataSource.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pageLength">Number of rows to return.</param>
	/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
	/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
	/// <param name="count">Number of rows in the DataSource.</param>
	/// <remarks></remarks>
	/// <returns>Returns a typed collection of ShutterTaleServices objects.</returns>
	[WebMethod(Description="Get all rows from the table Previews.")]
	public ShutterTale.Entities.TList<Previews> PreviewsProvider_GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.PreviewsProvider.GetPaged(whereClause, orderBy, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_GetPaged", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion
		
	#region Get By Foreign Key Functions
#endregion
	
	#region Get By Index Functions
	
	/// <summary>
	/// 	Gets rows from the datasource based on the IX_FK_MediaPreview index.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="Medium_Id"></param>
	/// <param name="count">out parameter to get total records for query</param>	
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Previews filtered by the column MediumId that is part of the IX_FK_MediaPreview index.")]
	public ShutterTale.Entities.TList<Previews> PreviewsProvider_GetByMediumId(System.Guid _mediumId, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.PreviewsProvider.GetByMediumId(_mediumId, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_GetByMediumId", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	

	
	/// <summary>
	/// 	Gets rows from the datasource based on the PK_Previews index.
	/// </summary>
	/// <param name="start">Row number at which to start reading.</param>
	/// <param name="pagelen">Number of rows to return.</param>
	/// <param name="Id"></param>
	/// <param name="count">out parameter to get total records for query</param>	
	/// <returns>Returns a DataSet.</returns>
	[WebMethod(Description="Get rows from the table Previews filtered by the column Id that is part of the PK_Previews index.")]
	public ShutterTale.Entities.Previews PreviewsProvider_GetById(System.Guid _id, int start, int pageLength, out int count)
	{
		try
		{
		return ShutterTale.Data.DataRepository.PreviewsProvider.GetById(_id, start, pageLength, out count);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_GetById", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	

	#endregion Get By Index Functions
	
	#region Insert Methods
		
	/// <summary>
	/// 	Inserts an object into the datasource.
	/// </summary>	
	/// <remarks>After inserting into the datasource, the object will be returned
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a row in the table Previews.")]
	public ShutterTale.Entities.Previews PreviewsProvider_Insert(ShutterTale.Entities.Previews entity )
	{
		try
		{
		ShutterTale.Data.DataRepository.PreviewsProvider.Insert(entity);
		return entity;		
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_Insert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	
	/// <summary>
	/// Inserts a ShutterTale.Entities.TList<Previews> object into the datasource using a transaction.
	/// </summary>
	/// <param name="entity">ShutterTale.Entities.TList<Previews> object to insert.</param>
	/// <remarks>After inserting into the datasource, the ShutterTale.Entities.Previews object will be updated
	/// to refelect any changes made by the datasource. (ie: identity or computed columns)
	/// </remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Inserts a Bulk set of rows into the table Previews.")]
	public void PreviewsProvider_BulkInsert(ShutterTale.Entities.TList<Previews> entityList )
	{
		try
		{
		ShutterTale.Data.DataRepository.PreviewsProvider.BulkInsert(entityList);
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_BulkInsert", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}
	#endregion Insert Methods
			
	#region Update Methods
		
	/// <summary>
	/// 	Update an existing row in the datasource.
	/// </summary>
	/// <param name="entity"> object to update.</param>
	/// <remarks>After updating the datasource, the object will be updated
	/// to refelect any changes made by the datasource. (ie: identity columns)</remarks>
	/// <returns>Returns true if operation is successful.</returns>
	[WebMethod(Description="Update a row in the table Previews.")]
	public ShutterTale.Entities.Previews PreviewsProvider_Update(ShutterTale.Entities.Previews entity)
	{
		try
		{
		ShutterTale.Data.DataRepository.PreviewsProvider.Update(entity);
		return entity;
		}
		catch(SoapException soex)
		{
			throw soex;
		}
		catch(Exception ex)
		{
			throw RaiseException("PreviewsProvider_Update", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		}
	}

	#endregion Update Methods

	#region Custom Methods
	
	
	#endregion

	
	
	/* --------------------------------------------------------
		SQL VIEWS
	----------------------------------------------------------- */

	#region General data access methods

		#region ExecuteNonQuery
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteNonQueryPs", Description="This method wrap the ExecuteNonQuery method provided by the Enterprise Library Data Access Application Block.")]
		public int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
		{
		        try
		        {
			    return ShutterTale.Data.DataRepository.Provider.ExecuteNonQuery(storedProcedureName, parameterValues);
                        }
		        catch(SoapException soex)
		        {
			    throw soex;
		        }
		        catch(Exception ex)
		        {
			    throw RaiseException("ExecuteNonQuery", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		        }
		}

		/*
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		[WebMethod(MessageName="ExecuteNonQueryCmd", Description="This method wrap the ExecuteNonQuery method provided by the Enterprise Library Data Access Application Block.")]
		public void ExecuteNonQuery(Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper commandWrapper)
		{
			ShutterTale.Data.DataRepository.Current.ExecuteNonQuery(commandWrapper);
		}
		*/

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteNonQueryQry", Description="This method wrap the ExecuteNonQuery method provided by the Enterprise Library Data Access Application Block.")]
		public int ExecuteNonQuery(CommandType commandType, string commandText)
		{
		        try
		        {
			    return ShutterTale.Data.DataRepository.Provider.ExecuteNonQuery(commandType, commandText);
                        }
		        catch(SoapException soex)
		        {
			    throw soex;
		        }
		        catch(Exception ex)
		        {
			    throw RaiseException("ExecuteNonQuery", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		        }
		}
		
		#endregion

		#region ExecuteDataSet
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteDataSetPs", Description="This method wrap the ExecuteDataSet method provided by the Enterprise Library Data Access Application Block.")]
		public DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
		{
		        try
		        {
			    return ShutterTale.Data.DataRepository.Provider.ExecuteDataSet(storedProcedureName, parameterValues);
                        }
		        catch(SoapException soex)
		        {
			    throw soex;
		        }
		        catch(Exception ex)
		        {
			    throw RaiseException("ExecuteDataSet", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		        }
		}
		
		/*
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteDataSetCmd", Description="This method wrap the ExecuteDataSet method provided by the Enterprise Library Data Access Application Block.")]
		public DataSet ExecuteDataSet(Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper commandWrapper)
		{
			return ShutterTale.Data.DataRepository.Current.ExecuteDataSet(commandWrapper);
		}
		*/

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteDataSetQry", Description="This method wrap the ExecuteDataSet method provided by the Enterprise Library Data Access Application Block.")]
		public DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
		        try
		        {
			    return ShutterTale.Data.DataRepository.Provider.ExecuteDataSet(commandType, commandText);
                        }
		        catch(SoapException soex)
		        {
			    throw soex;
		        }
		        catch(Exception ex)
		        {
			    throw RaiseException("ExecuteDataSet", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		        }
		}		
		#endregion

		#region ExecuteScalar
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteScalarPs", Description="This method wrap the ExecuteScalar method provided by the Enterprise Library Data Access Application Block.")]
		public object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
		{
		        try
		        {
			    return ShutterTale.Data.DataRepository.Provider.ExecuteScalar(storedProcedureName, parameterValues);
                        }
		        catch(SoapException soex)
		        {
			    throw soex;
		        }
		        catch(Exception ex)
		        {
			    throw RaiseException("ExecuteScalar", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		        }
		}	

		/*
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteScalarCmd", Description="This method wrap the ExecuteScalar method provided by the Enterprise Library Data Access Application Block.")]
		public object ExecuteScalar(Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper commandWrapper)
		{
			return ShutterTale.Data.DataRepository.Current.ExecuteScalar(commandWrapper);
		}
		*/

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		[WebMethod(MessageName="ExecuteScalarQry", Description="This method wrap the ExecuteScalar method provided by the Enterprise Library Data Access Application Block.")]
		public object ExecuteScalar(CommandType commandType, string commandText)
		{
		        try
		        {
			    return ShutterTale.Data.DataRepository.Provider.ExecuteScalar(commandType, commandText);	
                        }
		        catch(SoapException soex)
		        {
			    throw soex;
		        }
		        catch(Exception ex)
		        {
			    throw RaiseException("ExecuteScalar", "WSSoapException", ex.Message, "1000", ex.Source, FaultCode.Server);
		        }
		}
		
		#endregion

		#endregion
		
	
	#region Exception Handling
    /// <summary>
    /// Creates the SoapException object with all the error details
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="webServiceNamespace"></param>
    /// <param name="errorMessage"></param>
    /// <param name="errorNumber"></param>
    /// <param name="errorSource"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public SoapException RaiseException(string uri, string webServiceNamespace, string errorMessage, string errorNumber, string errorSource, FaultCode code)
    {
        XmlQualifiedName faultCodeLocation = null;

        //Identify the location of the FaultCode
        switch (code)
        {
            case FaultCode.Client:
                faultCodeLocation = SoapException.ClientFaultCode;
                break;
            case FaultCode.Server:
                faultCodeLocation = SoapException.ServerFaultCode;
                break;
        }

        XmlDocument xmlDoc = new XmlDocument();

        //Create the Detail node
        XmlNode rootNode = xmlDoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

        //Build specific details for the SoapException
        //Add first child of detail XML element.
        XmlNode errorNode = xmlDoc.CreateNode(XmlNodeType.Element, "Error", webServiceNamespace);

        //Create and set the value for the ErrorNumber node
        XmlNode errorNumberNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorNumber", webServiceNamespace);
        errorNumberNode.InnerText = errorNumber;

        //Create and set the value for the ErrorMessage node
        XmlNode errorMessageNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorMessage", webServiceNamespace);
        errorMessageNode.InnerText = errorMessage;

        //Create and set the value for the ErrorSource node
        XmlNode errorSourceNode = xmlDoc.CreateNode(XmlNodeType.Element, "ErrorSource", webServiceNamespace);
        errorSourceNode.InnerText = errorSource;

        //Append the Error child element nodes to the root detail node.
        errorNode.AppendChild(errorNumberNode);
        errorNode.AppendChild(errorMessageNode);
        errorNode.AppendChild(errorSourceNode);

        //Append the Detail node to the root node
        rootNode.AppendChild(errorNode);

        //Construct the exception
        SoapException soapEx = new SoapException(errorMessage, faultCodeLocation, uri, rootNode);

        //Raise the exception  back to the caller
        return soapEx;
    }
	#endregion
}
