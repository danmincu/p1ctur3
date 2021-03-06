﻿/*
	File Generated by NetTiers templates [www.nettiers.com]
	Generated on : July 10, 2013
	Important: Do not modify this file. Edit the file ShutterTale.Entities.MediaAudio.cs instead.
*/

#region Using directives

using System;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Web.Services.Protocols;
using ShutterTale.Entities;
using ShutterTale.Data.Bases;

#endregion

namespace ShutterTale.Data.WebServiceClient
{

	///<summary>
	/// This class is the webservice client implementation that exposes CRUD methods for ShutterTale.Entities.MediaAudio objects.
	///</summary>
	public abstract partial class WsMediaAudioProviderBase : MediaAudioProviderBase
	{
		#region Declarations	
	
		/// <summary>
		/// the Url of the webservice.
		/// </summary>
		private string url;
			
		#endregion Declarations
		
		#region Constructors
	
		/// <summary>
		/// Creates a new <see cref="WsMediaAudioProviderBase"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		public WsMediaAudioProviderBase()
		{		
		}
		
		/// <summary>
		/// Creates a new <see cref="WsMediaAudioProviderBase"/> instance.
		/// Uses connection string to connect to datasource.
		/// </summary>
		/// <param name="url">The url to the nettiers webservice.</param>
		public WsMediaAudioProviderBase(string url)
		{
			this.Url = url;
		}
			
		#endregion Constructors
		
		#region Url
		///<summary>
		/// Current URL for webservice endpoint. 
		///</summary>
		public string Url
        {
        	get {return url;}
        	set {url = value;}
        }
		#endregion 
		
		#region Convertion utility
		
		/// <summary>
		/// Convert a collection from the ws proxy to a nettiers collection.
		/// </summary>
		public static ShutterTale.Entities.TList<MediaAudio> Convert(WsProxy.MediaAudio[] items)
		{
			ShutterTale.Entities.TList<MediaAudio> outItems = new ShutterTale.Entities.TList<MediaAudio>();
			foreach(WsProxy.MediaAudio item in items)
			{
				outItems.Add(Convert(item));
			}
			return outItems;
		}
		
		/// <summary>
		/// Convert a nettiers collection to the ws proxy collection.
		/// </summary>
		public static ShutterTale.Entities.MediaAudio Convert(WsProxy.MediaAudio item)
		{	
			ShutterTale.Entities.MediaAudio outItem = item == null ? null : new ShutterTale.Entities.MediaAudio();
			Convert(outItem, item);					
			return outItem;
		}
		
		/// <summary>
		/// Convert a nettiers collection to the ws proxy collection.
		/// </summary>
		public static ShutterTale.Entities.MediaAudio Convert(ShutterTale.Entities.MediaAudio outItem , WsProxy.MediaAudio item)
		{	
			if (item != null && outItem != null)
			{
				outItem.AudioChannels = item.AudioChannels;
				outItem.Duration = item.Duration;
				outItem.AudioCodec = item.AudioCodec;
				outItem.Id = item.Id;
				
				outItem.OriginalId = item.Id;
				outItem.AcceptChanges();			
			}
							
			return outItem;
		}
		
		/// <summary>
		/// Convert a nettiers entity to the ws proxy entity.
		/// </summary>
		public static WsProxy.MediaAudio Convert(ShutterTale.Entities.MediaAudio item)
		{			
			WsProxy.MediaAudio outItem = new WsProxy.MediaAudio();			
			outItem.AudioChannels = item.AudioChannels;
			outItem.Duration = item.Duration;
			outItem.AudioCodec = item.AudioCodec;
			outItem.Id = item.Id;

			outItem.OriginalId = item.OriginalId;
							
			return outItem;
		}
		
		/// <summary>
		/// Convert a collection from  to a nettiers collection to a the ws proxy collection.
		/// </summary>
		public static WsProxy.MediaAudio[] Convert(ShutterTale.Entities.TList<MediaAudio> items)
		{
			WsProxy.MediaAudio[] outItems = new WsProxy.MediaAudio[items.Count];
			int count = 0;
		
			foreach (ShutterTale.Entities.MediaAudio item in items)
			{
				outItems[count++] = Convert(item);
			}
			return outItems;
		}

		
		#endregion
		
		#region Get from  Many To Many Relationship Functions
		#endregion	
		
		
		#region Delete Methods
			
			/// <summary>
			/// 	Deletes a row from the DataSource.
			/// </summary>
			/// <param name="_id">. Primary Key.</param>	
            
			/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
			/// <remarks>Deletes based on primary key(s).</remarks>
			/// <returns>Returns true if operation suceeded.</returns>
			public override bool Delete(TransactionManager transactionManager, System.Guid _id)
			{
				try
				{
				// call the proxy
				WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
				proxy.Url = Url;
				
				bool result = proxy.MediaAudioProvider_Delete(_id);				
				return result;
				}
				catch(SoapException soex)
				{
					System.Diagnostics.Debug.WriteLine(soex);
					throw soex;
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex);
					throw ex;
				}
			}
			
			#endregion Delete Methods
	
		
		#region Find Methods
		
		
		/// <summary>
		/// 	Returns rows meeting the whereclause condition from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pagelen">Number of rows to return.</param>
		/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
		/// <param name="count">Number of rows in the DataSource.</param>
		/// <remarks>Operators must be capitalized (OR, AND)</remarks>
		/// <returns>Returns a typed collection of ShutterTale.Entities.MediaAudio objects.</returns>
		public override ShutterTale.Entities.TList<MediaAudio> Find(TransactionManager transactionManager, string whereClause, int start, int pagelen, out int count)
		{
			try
			{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
			
			WsProxy.MediaAudio[] items = proxy.MediaAudioProvider_Find(whereClause, start, pagelen, out count);
			
			return Convert(items); 
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}
		
		#endregion Find Methods
		
		
		#region GetAll Methods
				
		/// <summary>
		/// 	Gets All rows from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>			
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ShutterTale.Entities.MediaAudio objects.</returns>
		public override ShutterTale.Entities.TList<MediaAudio> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
		{
			try
			{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
				
			WsProxy.MediaAudio[] items = proxy.MediaAudioProvider_GetAll(start, pageLength, out count);
			
			return Convert(items); 
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}
		
		#endregion GetAll Methods
		
		#region GetPaged Methods
						
		/// <summary>
		/// Gets a page of rows from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="whereClause">Specifies the condition for the rows returned by a query (Name='John Doe', Name='John Doe' AND Id='1', Name='John Doe' OR Id='1').</param>
		/// <param name="orderBy">Specifies the sort criteria for the rows in the DataSource (Name ASC; BirthDay DESC, Name ASC);</param>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">Number of rows in the DataSource.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of ShutterTale.Entities.MediaAudio objects.</returns>
		public override ShutterTale.Entities.TList<MediaAudio> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
		{
			try
			{
			whereClause = whereClause ?? string.Empty;
			orderBy = orderBy ?? string.Empty;
			
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
			
			WsProxy.MediaAudio[] items = proxy.MediaAudioProvider_GetPaged(whereClause, orderBy, start, pageLength, out count);
			
			// Create a collection and fill it with the dataset
			return Convert(items); 
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}
		
		#endregion GetPaged Methods
	
		
		#region Get By Foreign Key Functions
		#endregion
		
		
		#region Get By Index Functions
					
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_Media_Audio index.
		/// </summary>
		/// <param name="start">Row number at which to start reading.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_id"></param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="count">out parameter to get total records for query</param>	
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="ShutterTale.Entities.MediaAudio"/> class.</returns>
		public override ShutterTale.Entities.MediaAudio GetById(TransactionManager transactionManager, System.Guid _id, int start, int pageLength, out int count)
		{
			try
			{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
			WsProxy.MediaAudio items = proxy.MediaAudioProvider_GetById(_id, start, pageLength, out count);
			
			return Convert(items); 
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}
		
		#endregion Get By Index Functions
	
	
		#region Insert Methods
		/// <summary>
		/// 	Inserts a ShutterTale.Entities.MediaAudio object into the datasource using a transaction.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">ShutterTale.Entities.MediaAudio object to insert.</param>		
		/// <remarks></remarks>		
		/// <returns>Returns true if operation is successful.</returns>
		public override bool Insert(TransactionManager transactionManager, ShutterTale.Entities.MediaAudio entity)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
			
			try
			{
				WsProxy.MediaAudio result = proxy.MediaAudioProvider_Insert(Convert(entity));
				Convert(entity, result);
				return true;
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}
	
		/// <summary>
		/// Lets you efficiently bulk many entity to the database.
		/// </summary>
		/// <param name="transactionManager">NOTE: The transaction manager should be null for the web service client implementation.</param>
		/// <param name="entityList">The entities.</param>
		/// <remarks>
		/// After inserting into the datasource, the ShutterTale.Entities.MediaAudio object will be updated
		/// to refelect any changes made by the datasource. (ie: identity or computed columns)
		/// </remarks>
		public override void BulkInsert(TransactionManager transactionManager, ShutterTale.Entities.TList<MediaAudio> entityList)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
			try
			{
				proxy.MediaAudioProvider_BulkInsert(Convert(entityList));
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch (Exception ex)
			{	
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}

		#endregion Insert Methods
	
	
		#region Update Methods
						
		/// <summary>
		/// 	Update an existing row in the datasource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">ShutterTale.Entities.MediaAudio object to update.</param>		
		/// <remarks></remarks>
		/// <returns>Returns true if operation is successful.</returns>
		public override bool Update(TransactionManager transactionManager, ShutterTale.Entities.MediaAudio entity)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = Url;
			
			try
			{
				WsProxy.MediaAudio result = proxy.MediaAudioProvider_Update(Convert(entity));
				Convert(entity, result);
				entity.AcceptChanges();
				return true;
			}
			catch(SoapException soex)
			{
				System.Diagnostics.Debug.WriteLine(soex);
				throw soex;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw ex;
			}
		}
		
		#endregion Update Methods
			
		#region Custom Methods
		
		
		#endregion
					
	}//end class
} // end namespace
