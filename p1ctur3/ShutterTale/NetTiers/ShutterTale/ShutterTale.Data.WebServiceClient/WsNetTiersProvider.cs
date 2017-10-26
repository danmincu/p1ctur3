
#region Using directives

using System;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using ShutterTale.Entities;
using ShutterTale.Data.Bases;

#endregion

namespace ShutterTale.Data.WebServiceClient
{
	/// <summary>
	/// The WebService client data provider.
	/// </summary>
	public sealed class WsNetTiersProvider : ShutterTale.Data.Bases.NetTiersProvider
	{
		private static object syncRoot = new Object();
		private string _applicationName;
		private string url;
        
		/// <summary>
		/// Initializes a new instance of the <see cref="WsNetTiersProvider"/> class.
		///</summary>
		public WsNetTiersProvider()
		{			
		}
		
		/// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
            {
                name = "SqlNetTiersProvider";
            }

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NetTiers Sql provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _applicationName
            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
            {
                _applicationName = "/";
            }
            config.Remove("applicationName");


            #region Initialize Url
            string url  = config["url"];
           	if (string.IsNullOrEmpty(url))
            {
                throw new ProviderException("Empty or missing url");
            }
            this.url = url;
            config.Remove("url");
            #endregion

        }
        
		/// <summary>
		/// Current Url for WebService EndPoint
		/// </summary>
        public string Url
        {
        	get {return this.url;}
        	set {this.url = value;}
        }
		
		/// <summary>
		/// Creates a new <see cref="TransactionManager"/> instance from the current datasource.
		/// </summary>
		/// <returns></returns>
		public override TransactionManager CreateTransaction()
		{
			throw new NotSupportedException("Transactions are not supported by the webservice client.");
		}
		
		///<summary>
		/// Indicates if the current <see cref="NetTiersProvider"/> implementation supports Transacton.
		///</summary>
		public override bool IsTransactionSupported
		{
			get
			{
				return false;
			}
		}

			
		private WsMediaProvider innerMediaProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Media"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override MediaProviderBase MediaProvider
		{
			get
			{
				if (innerMediaProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerMediaProvider == null)
						{
							this.innerMediaProvider = new WsMediaProvider(this.url);
						}
					}
				}
				return innerMediaProvider;
			}
		}
		
			
		private WsMediaImageProvider innerMediaImageProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="MediaImage"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override MediaImageProviderBase MediaImageProvider
		{
			get
			{
				if (innerMediaImageProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerMediaImageProvider == null)
						{
							this.innerMediaImageProvider = new WsMediaImageProvider(this.url);
						}
					}
				}
				return innerMediaImageProvider;
			}
		}
		
			
		private WsMediaVideoProvider innerMediaVideoProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="MediaVideo"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override MediaVideoProviderBase MediaVideoProvider
		{
			get
			{
				if (innerMediaVideoProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerMediaVideoProvider == null)
						{
							this.innerMediaVideoProvider = new WsMediaVideoProvider(this.url);
						}
					}
				}
				return innerMediaVideoProvider;
			}
		}
		
			
		private WsMediaAudioProvider innerMediaAudioProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="MediaAudio"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override MediaAudioProviderBase MediaAudioProvider
		{
			get
			{
				if (innerMediaAudioProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerMediaAudioProvider == null)
						{
							this.innerMediaAudioProvider = new WsMediaAudioProvider(this.url);
						}
					}
				}
				return innerMediaAudioProvider;
			}
		}
		
			
		private WsPreviewsProvider innerPreviewsProvider;

		///<summary>
		/// This class is the Data Access Logic Component for the <see cref="Previews"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		/// <value></value>
		public override PreviewsProviderBase PreviewsProvider
		{
			get
			{
				if (innerPreviewsProvider == null) 
				{
					lock (syncRoot)
					{
						if (innerPreviewsProvider == null)
						{
							this.innerPreviewsProvider = new WsPreviewsProvider(this.url);
						}
					}
				}
				return innerPreviewsProvider;
			}
		}
		
		
		
		#region "General data access methods"

		#region "ExecuteNonQuery"
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = this.url;
			return proxy.ExecuteNonQuery(storedProcedureName, parameterValues);
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		public override void ExecuteNonQuery(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}


		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = this.url;
			return proxy.ExecuteNonQuery((WsProxy.CommandType)Enum.Parse(typeof(WsProxy.CommandType), commandType.ToString(), false), commandText);
		}
		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override int ExecuteNonQuery(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}
		#endregion

		#region "ExecuteDataReader"
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(DbCommand commandWrapper)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}


		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}
		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("ExecuteReader methods are not supported by the WebService provider.");
		}
		#endregion

		#region "ExecuteDataSet"
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = this.url;
			return proxy.ExecuteDataSet(storedProcedureName, parameterValues);
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}


		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = this.url;
			return proxy.ExecuteDataSet((WsProxy.CommandType)Enum.Parse(typeof(WsProxy.CommandType), commandType.ToString(), false), commandText);
		}
		
		/// <summary>
		/// Executes the data set.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override DataSet ExecuteDataSet(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");			
		}
		#endregion

		#region "ExecuteScalar"
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = this.url;
			return proxy.ExecuteScalar(storedProcedureName, parameterValues);
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="storedProcedureName">Name of the stored procedure.</param>
		/// <param name="parameterValues">The parameter values.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, string storedProcedureName, params object[] parameterValues)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandWrapper">The command wrapper.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, DbCommand commandWrapper)
		{
			throw new NotSupportedException("DBCommandWrapper overloads are not supported by the WebService provider.");
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(CommandType commandType, string commandText)
		{
			WsProxy.ShutterTaleServices proxy = new WsProxy.ShutterTaleServices();
			proxy.Url = this.url;
			return proxy.ExecuteScalar((WsProxy.CommandType)Enum.Parse(typeof(WsProxy.CommandType), commandType.ToString(), false), commandText);	
		}
		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="commandText">The command text.</param>
		/// <returns></returns>
		public override object ExecuteScalar(TransactionManager transactionManager, CommandType commandType, string commandText)
		{
			throw new NotSupportedException("TransactionManager overloads are not supported by the WebService provider.");		
		}
		#endregion

		#endregion
	}
}
