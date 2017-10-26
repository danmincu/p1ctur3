﻿/*
	File Generated by NetTiers templates [www.nettiers.com]
	Generated on : July 10, 2013
	Important: Do not modify this file. Edit the file SqlMediaProvider.cs instead.
*/

#region using directives

using System;
using System.Data;
using System.Data.Common;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System.Collections;
using System.Collections.Specialized;

using System.Diagnostics;
using ShutterTale.Entities;
using ShutterTale.Data;
using ShutterTale.Data.Bases;

#endregion

namespace ShutterTale.Data.SqlClient
{
	///<summary>
	/// This class is the SqlClient Data Access Logic Component implementation for the <see cref="Media"/> entity.
	///</summary>
	public abstract partial class SqlMediaProviderBase : MediaProviderBase
	{
		#region Declarations
		
		string _connectionString;
	    bool _useStoredProcedure;
	    string _providerInvariantName;
			
		#endregion "Declarations"
			
		#region Constructors
		
		/// <summary>
		/// Creates a new <see cref="SqlMediaProviderBase"/> instance.
		/// </summary>
		public SqlMediaProviderBase()
		{
		}
	
	/// <summary>
	/// Creates a new <see cref="SqlMediaProviderBase"/> instance.
	/// Uses connection string to connect to datasource.
	/// </summary>
	/// <param name="connectionString">The connection string to the database.</param>
	/// <param name="useStoredProcedure">A boolean value that indicates if we should use stored procedures or embedded queries.</param>
	/// <param name="providerInvariantName">Name of the invariant provider use by the DbProviderFactory.</param>
	public SqlMediaProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
	{
		this._connectionString = connectionString;
		this._useStoredProcedure = useStoredProcedure;
		this._providerInvariantName = providerInvariantName;
	}
		
	#endregion "Constructors"
	
		#region Public properties
	/// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    /// <value>The connection string.</value>
    public string ConnectionString
	{
		get {return this._connectionString;}
		set {this._connectionString = value;}
	}
	
	/// <summary>
    /// Gets or sets a value indicating whether to use stored procedures.
    /// </summary>
    /// <value><c>true</c> if we choose to use use stored procedures; otherwise, <c>false</c>.</value>
	public bool UseStoredProcedure
	{
		get {return this._useStoredProcedure;}
		set {this._useStoredProcedure = value;}
	}
	
	/// <summary>
    /// Gets or sets the invariant provider name listed in the DbProviderFactories machine.config section.
    /// </summary>
    /// <value>The name of the provider invariant.</value>
    public string ProviderInvariantName
    {
        get { return this._providerInvariantName; }
        set { this._providerInvariantName = value; }
    }
	#endregion
	
		#region Get Many To Many Relationship Functions
		#endregion
	
		#region Delete Functions
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_id">. Primary Key.</param>	
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Delete(TransactionManager transactionManager, System.Guid _id)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_Delete", _useStoredProcedure);
			database.AddInParameter(commandWrapper, "@Id", DbType.Guid, _id);
			
			//Provider Data Requesting Command Event
			OnDataRequesting(new CommandEventArgs(commandWrapper, "Delete")); 

			int results = 0;
			
			if (transactionManager != null)
			{	
				results = Utility.ExecuteNonQuery(transactionManager, commandWrapper);
			}
			else
			{
				results = Utility.ExecuteNonQuery(database,commandWrapper);
			}
			
			//Stop Tracking Now that it has been updated and persisted.
			if (DataRepository.Provider.EnableEntityTracking)
			{
				string entityKey = EntityLocator.ConstructKeyFromPkItems(typeof(Media)
					,_id);
                EntityManager.StopTracking(entityKey);
                
			}
			
			//Provider Data Requested Command Event
			OnDataRequested(new CommandEventArgs(commandWrapper, "Delete")); 

			commandWrapper = null;
			
			return Convert.ToBoolean(results);
		}//end Delete
		#endregion

		#region Find Functions

		#region Parsed Find Methods
		/// <summary>
		/// 	Returns rows meeting the whereClause condition from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out. The number of rows that match this query.</param>
		/// <remarks>Operators must be capitalized (OR, AND).</remarks>
		/// <returns>Returns a typed collection of ShutterTale.Entities.Media objects.</returns>
		public override TList<Media> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
		{
			count = -1;
			if (whereClause.IndexOf(";") > -1)
				return new TList<Media>();
	
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_Find", _useStoredProcedure);

		bool searchUsingOR = false;
		if (whereClause.IndexOf(" OR ") > 0) // did they want to do "a=b OR c=d OR..."?
			searchUsingOR = true;
		
		database.AddInParameter(commandWrapper, "@SearchUsingOR", DbType.Boolean, searchUsingOR);
		
		database.AddInParameter(commandWrapper, "@Id", DbType.Guid, DBNull.Value);
		database.AddInParameter(commandWrapper, "@CaptureDateTime", DbType.DateTime, DBNull.Value);
		database.AddInParameter(commandWrapper, "@FileDateTime", DbType.DateTime, DBNull.Value);
		database.AddInParameter(commandWrapper, "@ImportDateTime", DbType.DateTime, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Location", DbType.Object, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Pixelx", DbType.Int32, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Pixely", DbType.Int32, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Dpi", DbType.Double, DBNull.Value);
		database.AddInParameter(commandWrapper, "@ContentType", DbType.String, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Make", DbType.String, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Model", DbType.String, DBNull.Value);
		database.AddInParameter(commandWrapper, "@SoftwareVersion", DbType.String, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Origin", DbType.String, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Size", DbType.Int32, DBNull.Value);
		database.AddInParameter(commandWrapper, "@Quadkey18", DbType.String, DBNull.Value);
	
			// replace all instances of 'AND' and 'OR' because we already set searchUsingOR
			whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|") ; 
			string[] clauses = whereClause.ToLower().Split('|');
		
			// Here's what's going on below: Find a field, then to get the value we
			// drop the field name from the front, trim spaces, drop the '=' sign,
			// trim more spaces, and drop any outer single quotes.
			// Now handles the case when two fields start off the same way - like "Friendly='Yes' AND Friend='john'"
				
			char[] equalSign = {'='};
			char[] singleQuote = {'\''};
	   		foreach (string clause in clauses)
			{
				if (clause.Trim().StartsWith("id ") || clause.Trim().StartsWith("id="))
				{
					database.SetParameterValue(commandWrapper, "@Id", new Guid(
						clause.Trim().Remove(0,2).Trim().TrimStart(equalSign).Trim().Trim(singleQuote)));
					continue;
				}
				if (clause.Trim().StartsWith("capturedatetime ") || clause.Trim().StartsWith("capturedatetime="))
				{
					database.SetParameterValue(commandWrapper, "@CaptureDateTime", 
						clause.Trim().Remove(0,15).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("filedatetime ") || clause.Trim().StartsWith("filedatetime="))
				{
					database.SetParameterValue(commandWrapper, "@FileDateTime", 
						clause.Trim().Remove(0,12).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("importdatetime ") || clause.Trim().StartsWith("importdatetime="))
				{
					database.SetParameterValue(commandWrapper, "@ImportDateTime", 
						clause.Trim().Remove(0,14).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("location ") || clause.Trim().StartsWith("location="))
				{
					database.SetParameterValue(commandWrapper, "@Location", 
						clause.Trim().Remove(0,8).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("pixelx ") || clause.Trim().StartsWith("pixelx="))
				{
					database.SetParameterValue(commandWrapper, "@Pixelx", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("pixely ") || clause.Trim().StartsWith("pixely="))
				{
					database.SetParameterValue(commandWrapper, "@Pixely", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("dpi ") || clause.Trim().StartsWith("dpi="))
				{
					database.SetParameterValue(commandWrapper, "@Dpi", 
						clause.Trim().Remove(0,3).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("contenttype ") || clause.Trim().StartsWith("contenttype="))
				{
					database.SetParameterValue(commandWrapper, "@ContentType", 
						clause.Trim().Remove(0,11).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("make ") || clause.Trim().StartsWith("make="))
				{
					database.SetParameterValue(commandWrapper, "@Make", 
						clause.Trim().Remove(0,4).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("model ") || clause.Trim().StartsWith("model="))
				{
					database.SetParameterValue(commandWrapper, "@Model", 
						clause.Trim().Remove(0,5).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("softwareversion ") || clause.Trim().StartsWith("softwareversion="))
				{
					database.SetParameterValue(commandWrapper, "@SoftwareVersion", 
						clause.Trim().Remove(0,15).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("origin ") || clause.Trim().StartsWith("origin="))
				{
					database.SetParameterValue(commandWrapper, "@Origin", 
						clause.Trim().Remove(0,6).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("size ") || clause.Trim().StartsWith("size="))
				{
					database.SetParameterValue(commandWrapper, "@Size", 
						clause.Trim().Remove(0,4).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
				if (clause.Trim().StartsWith("quadkey18 ") || clause.Trim().StartsWith("quadkey18="))
				{
					database.SetParameterValue(commandWrapper, "@Quadkey18", 
						clause.Trim().Remove(0,9).Trim().TrimStart(equalSign).Trim().Trim(singleQuote));
					continue;
				}
	
				throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + clause);
			}
					
			IDataReader reader = null;
			//Create Collection
			TList<Media> rows = new TList<Media>();
	
				
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "Find", rows)); 

				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}		
				
				Fill(reader, rows, start, pageLength);
				
				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "Find", rows)); 
			}
			finally
			{
				if (reader != null) 
					reader.Close();	
					
				commandWrapper = null;
			}
			return rows;
		}

		#endregion Parsed Find Methods
		
		#region Parameterized Find Methods
		
		/// <summary>
		/// 	Returns rows from the DataSource that meet the parameter conditions.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="parameters">A collection of <see cref="SqlFilterParameter"/> objects.</param>
		/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out. The number of rows that match this query.</param>
		/// <returns>Returns a typed collection of ShutterTale.Entities.Media objects.</returns>
		public override TList<Media> Find(TransactionManager transactionManager, IFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
		{
			SqlFilterParameterCollection filter = null;
			
			if (parameters == null)
				filter = new SqlFilterParameterCollection();
			else 
				filter = parameters.GetParameters();
				
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_Find_Dynamic", typeof(MediaColumn), filter, orderBy, start, pageLength);
		
			SqlFilterParameter param;

			for ( int i = 0; i < filter.Count; i++ )
			{
				param = filter[i];
				database.AddInParameter(commandWrapper, param.Name, param.DbType, param.GetValue());
			}

			TList<Media> rows = new TList<Media>();
			IDataReader reader = null;
			
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "Find", rows)); 

				if ( transactionManager != null )
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}
				
				Fill(reader, rows, 0, int.MaxValue);
				count = rows.Count;
				
				if ( reader.NextResult() )
				{
					if ( reader.Read() )
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "Find", rows)); 
			}
			finally
			{
				if ( reader != null )
					reader.Close();
					
				commandWrapper = null;
			}
			
			return rows;
		}
		
		#endregion Parameterized Find Methods
		
		#endregion Find Functions
	
		#region GetAll Methods
				
		/// <summary>
		/// 	Gets All rows from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out. The number of rows that match this query.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ShutterTale.Entities.Media objects.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override TList<Media> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_Get_List", _useStoredProcedure);
			
			IDataReader reader = null;
		
			//Create Collection
			TList<Media> rows = new TList<Media>();
			
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "GetAll", rows)); 
					
				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}		
		
				Fill(reader, rows, start, pageLength);
				count = -1;
				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "GetAll", rows)); 
			}
			finally 
			{
				if (reader != null) 
					reader.Close();
					
				commandWrapper = null;	
			}
			return rows;
		}//end getall
		
		#endregion
				
		#region GetPaged Methods
				
		/// <summary>
		/// Gets a page of rows from the DataSource.
		/// </summary>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">Number of rows in the DataSource.</param>
		/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
		/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ShutterTale.Entities.Media objects.</returns>
		public override TList<Media> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_GetPaged", _useStoredProcedure);
		
			
            if (commandWrapper.CommandType == CommandType.Text
                && commandWrapper.CommandText != null)
            {
                commandWrapper.CommandText = commandWrapper.CommandText.Replace(SqlUtil.PAGE_INDEX, string.Concat(SqlUtil.PAGE_INDEX, Guid.NewGuid().ToString("N").Substring(0, 8)));
            }
			
			database.AddInParameter(commandWrapper, "@WhereClause", DbType.String, whereClause);
			database.AddInParameter(commandWrapper, "@OrderBy", DbType.String, orderBy);
			database.AddInParameter(commandWrapper, "@PageIndex", DbType.Int32, start);
			database.AddInParameter(commandWrapper, "@PageSize", DbType.Int32, pageLength);
		
			IDataReader reader = null;
			//Create Collection
			TList<Media> rows = new TList<Media>();
			
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "GetPaged", rows)); 

				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}
				
				Fill(reader, rows, 0, int.MaxValue);
				count = rows.Count;

				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "GetPaged", rows)); 

			}
			catch(Exception)
			{			
				throw;
			}
			finally
			{
				if (reader != null) 
					reader.Close();
				
				commandWrapper = null;
			}
			
			return rows;
		}
		
		#endregion	
		
		#region Get By Foreign Key Functions
	#endregion
	
		#region Get By Index Functions

		#region GetById
					
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_id"></param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query.</param>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.Media"/> class.</returns>
		/// <remarks></remarks>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override ShutterTale.Entities.Media GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_GetById", _useStoredProcedure);
			
				database.AddInParameter(commandWrapper, "@Id", DbType.Guid, _id);
			
			IDataReader reader = null;
			TList<Media> tmp = new TList<Media>();
			try
			{
				//Provider Data Requesting Command Event
				OnDataRequesting(new CommandEventArgs(commandWrapper, "GetById", tmp)); 

				if (transactionManager != null)
				{
					reader = Utility.ExecuteReader(transactionManager, commandWrapper);
				}
				else
				{
					reader = Utility.ExecuteReader(database, commandWrapper);
				}		
		
				//Create collection and fill
				Fill(reader, tmp, start, pageLength);
				count = -1;
				if(reader.NextResult())
				{
					if(reader.Read())
					{
						count = reader.GetInt32(0);
					}
				}
				
				//Provider Data Requested Command Event
				OnDataRequested(new CommandEventArgs(commandWrapper, "GetById", tmp));
			}
			finally 
			{
				if (reader != null) 
					reader.Close();
					
				commandWrapper = null;
			}
			
			if (tmp.Count == 1)
			{
				return tmp[0];
			}
			else if (tmp.Count == 0)
			{
				return null;
			}
			else
			{
				throw new DataException("Cannot find the unique instance of the class.");
			}
			
			//return rows;
		}
		
		#endregion

	#endregion Get By Index Functions

		#region Insert Methods
		/// <summary>
		/// Lets you efficiently bulk insert many entities to the database.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entities">The entities.</param>
		/// <remarks>
		///		After inserting into the datasource, the ShutterTale.Entities.Media object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>	
		public override void BulkInsert(TransactionManager transactionManager, TList<ShutterTale.Entities.Media> entities)
		{
			//System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(this._connectionString, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints); //, null);
			
			System.Data.SqlClient.SqlBulkCopy bulkCopy = null;
	
			if (transactionManager != null && transactionManager.IsOpen)
			{			
				System.Data.SqlClient.SqlConnection cnx = transactionManager.TransactionObject.Connection as System.Data.SqlClient.SqlConnection;
				System.Data.SqlClient.SqlTransaction transaction = transactionManager.TransactionObject as System.Data.SqlClient.SqlTransaction;
				bulkCopy = new System.Data.SqlClient.SqlBulkCopy(cnx, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints, transaction); //, null);
			}
			else
			{
				bulkCopy = new System.Data.SqlClient.SqlBulkCopy(this._connectionString, System.Data.SqlClient.SqlBulkCopyOptions.CheckConstraints); //, null);
			}
			
			bulkCopy.BulkCopyTimeout = 360;
			bulkCopy.DestinationTableName = "Media";
			
			DataTable dataTable = new DataTable();
			DataColumn col0 = dataTable.Columns.Add("Id", typeof(System.Guid));
			col0.AllowDBNull = false;		
			DataColumn col1 = dataTable.Columns.Add("CaptureDateTime", typeof(System.DateTime));
			col1.AllowDBNull = true;		
			DataColumn col2 = dataTable.Columns.Add("FileDateTime", typeof(System.DateTime));
			col2.AllowDBNull = false;		
			DataColumn col3 = dataTable.Columns.Add("ImportDateTime", typeof(System.DateTime));
			col3.AllowDBNull = false;		
			DataColumn col4 = dataTable.Columns.Add("Location", typeof(System.Object));
			col4.AllowDBNull = true;		
			DataColumn col5 = dataTable.Columns.Add("PixelX", typeof(System.Int32));
			col5.AllowDBNull = false;		
			DataColumn col6 = dataTable.Columns.Add("PixelY", typeof(System.Int32));
			col6.AllowDBNull = false;		
			DataColumn col7 = dataTable.Columns.Add("Dpi", typeof(System.Double));
			col7.AllowDBNull = false;		
			DataColumn col8 = dataTable.Columns.Add("ContentType", typeof(System.String));
			col8.AllowDBNull = false;		
			DataColumn col9 = dataTable.Columns.Add("Make", typeof(System.String));
			col9.AllowDBNull = true;		
			DataColumn col10 = dataTable.Columns.Add("Model", typeof(System.String));
			col10.AllowDBNull = true;		
			DataColumn col11 = dataTable.Columns.Add("SoftwareVersion", typeof(System.String));
			col11.AllowDBNull = true;		
			DataColumn col12 = dataTable.Columns.Add("Origin", typeof(System.String));
			col12.AllowDBNull = false;		
			DataColumn col13 = dataTable.Columns.Add("Size", typeof(System.Int32));
			col13.AllowDBNull = false;		
			DataColumn col14 = dataTable.Columns.Add("Quadkey18", typeof(System.String));
			col14.AllowDBNull = false;		
			
			bulkCopy.ColumnMappings.Add("Id", "Id");
			bulkCopy.ColumnMappings.Add("CaptureDateTime", "CaptureDateTime");
			bulkCopy.ColumnMappings.Add("FileDateTime", "FileDateTime");
			bulkCopy.ColumnMappings.Add("ImportDateTime", "ImportDateTime");
			bulkCopy.ColumnMappings.Add("Location", "Location");
			bulkCopy.ColumnMappings.Add("PixelX", "PixelX");
			bulkCopy.ColumnMappings.Add("PixelY", "PixelY");
			bulkCopy.ColumnMappings.Add("Dpi", "Dpi");
			bulkCopy.ColumnMappings.Add("ContentType", "ContentType");
			bulkCopy.ColumnMappings.Add("Make", "Make");
			bulkCopy.ColumnMappings.Add("Model", "Model");
			bulkCopy.ColumnMappings.Add("SoftwareVersion", "SoftwareVersion");
			bulkCopy.ColumnMappings.Add("Origin", "Origin");
			bulkCopy.ColumnMappings.Add("Size", "Size");
			bulkCopy.ColumnMappings.Add("Quadkey18", "Quadkey18");
			
			foreach(ShutterTale.Entities.Media entity in entities)
			{
				if (entity.EntityState != EntityState.Added)
					continue;
					
				DataRow row = dataTable.NewRow();
				
					row["Id"] = entity.Id;
							
				
					row["CaptureDateTime"] = entity.CaptureDateTime.HasValue ? (object) entity.CaptureDateTime  : System.DBNull.Value;
							
				
					row["FileDateTime"] = entity.FileDateTime;
							
				
					row["ImportDateTime"] = entity.ImportDateTime;
							
				
					row["Location"] = entity.Location;
							
				
					row["PixelX"] = entity.Pixelx;
							
				
					row["PixelY"] = entity.Pixely;
							
				
					row["Dpi"] = entity.Dpi;
							
				
					row["ContentType"] = entity.ContentType;
							
				
					row["Make"] = entity.Make;
							
				
					row["Model"] = entity.Model;
							
				
					row["SoftwareVersion"] = entity.SoftwareVersion;
							
				
					row["Origin"] = entity.Origin;
							
				
					row["Size"] = entity.Size;
							
				
					row["Quadkey18"] = entity.Quadkey18;
							
				
				dataTable.Rows.Add(row);
			}		
			
			// send the data to the server		
			bulkCopy.WriteToServer(dataTable);		
			
			// update back the state
			foreach(ShutterTale.Entities.Media entity in entities)
			{
				if (entity.EntityState != EntityState.Added)
					continue;
			
				entity.AcceptChanges();
			}
		}
				
		/// <summary>
		/// 	Inserts a ShutterTale.Entities.Media object into the datasource using a transaction.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">ShutterTale.Entities.Media object to insert.</param>
		/// <remarks>
		///		After inserting into the datasource, the ShutterTale.Entities.Media object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>	
		/// <returns>Returns true if operation is successful.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Insert(TransactionManager transactionManager, ShutterTale.Entities.Media entity)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_Insert", _useStoredProcedure);
			
            database.AddInParameter(commandWrapper, "@Id", DbType.Guid, entity.Id );
			database.AddInParameter(commandWrapper, "@CaptureDateTime", DbType.DateTime, (entity.CaptureDateTime.HasValue ? (object) entity.CaptureDateTime  : System.DBNull.Value));
            database.AddInParameter(commandWrapper, "@FileDateTime", DbType.DateTime, entity.FileDateTime );
            database.AddInParameter(commandWrapper, "@ImportDateTime", DbType.DateTime, entity.ImportDateTime );
            database.AddInParameter(commandWrapper, "@Location", DbType.Object, entity.Location );
            database.AddInParameter(commandWrapper, "@Pixelx", DbType.Int32, entity.Pixelx );
            database.AddInParameter(commandWrapper, "@Pixely", DbType.Int32, entity.Pixely );
            database.AddInParameter(commandWrapper, "@Dpi", DbType.Double, entity.Dpi );
            database.AddInParameter(commandWrapper, "@ContentType", DbType.String, entity.ContentType );
            database.AddInParameter(commandWrapper, "@Make", DbType.String, entity.Make );
            database.AddInParameter(commandWrapper, "@Model", DbType.String, entity.Model );
            database.AddInParameter(commandWrapper, "@SoftwareVersion", DbType.String, entity.SoftwareVersion );
            database.AddInParameter(commandWrapper, "@Origin", DbType.String, entity.Origin );
            database.AddInParameter(commandWrapper, "@Size", DbType.Int32, entity.Size );
            database.AddInParameter(commandWrapper, "@Quadkey18", DbType.String, entity.Quadkey18 );
			
			int results = 0;
			
			//Provider Data Requesting Command Event
			OnDataRequesting(new CommandEventArgs(commandWrapper, "Insert", entity));
				
			if (transactionManager != null)
			{
				results = Utility.ExecuteNonQuery(transactionManager, commandWrapper);
			}
			else
			{
				results = Utility.ExecuteNonQuery(database,commandWrapper);
			}
					
			
			entity.OriginalId = entity.Id;
			
			entity.AcceptChanges();
	
			//Provider Data Requested Command Event
			OnDataRequested(new CommandEventArgs(commandWrapper, "Insert", entity));

			return Convert.ToBoolean(results);
		}	
		#endregion

		#region Update Methods
				
		/// <summary>
		/// 	Update an existing row in the datasource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">ShutterTale.Entities.Media object to update.</param>
		/// <remarks>
		///		After updating the datasource, the ShutterTale.Entities.Media object will be updated
		/// 	to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>
		/// <returns>Returns true if operation is successful.</returns>
        /// <exception cref="System.Exception">The command could not be executed.</exception>
        /// <exception cref="System.Data.DataException">The <paramref name="transactionManager"/> is not open.</exception>
        /// <exception cref="System.Data.Common.DbException">The command could not be executed.</exception>
		public override bool Update(TransactionManager transactionManager, ShutterTale.Entities.Media entity)
		{
			SqlDatabase database = new SqlDatabase(this._connectionString);
			DbCommand commandWrapper = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Media_Update", _useStoredProcedure);
			
            database.AddInParameter(commandWrapper, "@Id", DbType.Guid, entity.Id );
			database.AddInParameter(commandWrapper, "@OriginalId", DbType.Guid, entity.OriginalId);
			database.AddInParameter(commandWrapper, "@CaptureDateTime", DbType.DateTime, (entity.CaptureDateTime.HasValue ? (object) entity.CaptureDateTime : System.DBNull.Value) );
            database.AddInParameter(commandWrapper, "@FileDateTime", DbType.DateTime, entity.FileDateTime );
            database.AddInParameter(commandWrapper, "@ImportDateTime", DbType.DateTime, entity.ImportDateTime );
            database.AddInParameter(commandWrapper, "@Location", DbType.Object, entity.Location );
            database.AddInParameter(commandWrapper, "@Pixelx", DbType.Int32, entity.Pixelx );
            database.AddInParameter(commandWrapper, "@Pixely", DbType.Int32, entity.Pixely );
            database.AddInParameter(commandWrapper, "@Dpi", DbType.Double, entity.Dpi );
            database.AddInParameter(commandWrapper, "@ContentType", DbType.String, entity.ContentType );
            database.AddInParameter(commandWrapper, "@Make", DbType.String, entity.Make );
            database.AddInParameter(commandWrapper, "@Model", DbType.String, entity.Model );
            database.AddInParameter(commandWrapper, "@SoftwareVersion", DbType.String, entity.SoftwareVersion );
            database.AddInParameter(commandWrapper, "@Origin", DbType.String, entity.Origin );
            database.AddInParameter(commandWrapper, "@Size", DbType.Int32, entity.Size );
            database.AddInParameter(commandWrapper, "@Quadkey18", DbType.String, entity.Quadkey18 );
			
			int results = 0;
			
			//Provider Data Requesting Command Event
			OnDataRequesting(new CommandEventArgs(commandWrapper, "Update", entity));

			if (transactionManager != null)
			{
				results = Utility.ExecuteNonQuery(transactionManager, commandWrapper);
			}
			else
			{
				results = Utility.ExecuteNonQuery(database,commandWrapper);
			}
			
			//Stop Tracking Now that it has been updated and persisted.
			if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(entity.EntityTrackingKey);				
            }
			
			entity.OriginalId = entity.Id;
			
			entity.AcceptChanges();
			
			//Provider Data Requested Command Event
			OnDataRequested(new CommandEventArgs(commandWrapper, "Update", entity));

			return Convert.ToBoolean(results);
		}
			
		#endregion
		
		#region Custom Methods
	
		#endregion
	}//end class
} // end namespace
