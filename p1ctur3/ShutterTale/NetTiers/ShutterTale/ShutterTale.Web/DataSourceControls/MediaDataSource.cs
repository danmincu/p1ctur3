#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using ShutterTale.Entities;
using ShutterTale.Data;
using ShutterTale.Data.Bases;
#endregion

namespace ShutterTale.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.MediaProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(MediaDataSourceDesigner))]
	public class MediaDataSource : ProviderDataSource<Media, MediaKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaDataSource class.
		/// </summary>
		public MediaDataSource() : base(DataRepository.MediaProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the MediaDataSourceView used by the MediaDataSource.
		/// </summary>
		protected MediaDataSourceView MediaView
		{
			get { return ( View as MediaDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the MediaDataSource control invokes to retrieve data.
		/// </summary>
		public MediaSelectMethod SelectMethod
		{
			get
			{
				MediaSelectMethod selectMethod = MediaSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (MediaSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the MediaDataSourceView class that is to be
		/// used by the MediaDataSource.
		/// </summary>
		/// <returns>An instance of the MediaDataSourceView class.</returns>
		protected override BaseDataSourceView<Media, MediaKey> GetNewDataSourceView()
		{
			return new MediaDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the MediaDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class MediaDataSourceView : ProviderDataSourceView<Media, MediaKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the MediaDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public MediaDataSourceView(MediaDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal MediaDataSource MediaOwner
		{
			get { return Owner as MediaDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal MediaSelectMethod SelectMethod
		{
			get { return MediaOwner.SelectMethod; }
			set { MediaOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal MediaProviderBase MediaProvider
		{
			get { return Provider as MediaProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Media> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Media> results = null;
			Media item;
			count = 0;
			
			System.Guid _id;

			switch ( SelectMethod )
			{
				case MediaSelectMethod.Get:
					MediaKey entityKey  = new MediaKey();
					entityKey.Load(values);
					item = MediaProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Media>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case MediaSelectMethod.GetAll:
                    results = MediaProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case MediaSelectMethod.GetPaged:
					results = MediaProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case MediaSelectMethod.Find:
					if ( FilterParameters != null )
						results = MediaProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = MediaProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case MediaSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Id"], typeof(System.Guid)) : Guid.Empty;
					item = MediaProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Media>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				// M:M
				// Custom
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == MediaSelectMethod.Get || SelectMethod == MediaSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Media entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.Id == Guid.Empty )
				entity.Id = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				Media entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					MediaProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<Media> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			MediaProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region MediaDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the MediaDataSource class.
	/// </summary>
	public class MediaDataSourceDesigner : ProviderDataSourceDesigner<Media, MediaKey>
	{
		/// <summary>
		/// Initializes a new instance of the MediaDataSourceDesigner class.
		/// </summary>
		public MediaDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaSelectMethod SelectMethod
		{
			get { return ((MediaDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new MediaDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region MediaDataSourceActionList

	/// <summary>
	/// Supports the MediaDataSourceDesigner class.
	/// </summary>
	internal class MediaDataSourceActionList : DesignerActionList
	{
		private MediaDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the MediaDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public MediaDataSourceActionList(MediaDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion MediaDataSourceActionList
	
	#endregion MediaDataSourceDesigner
	
	#region MediaSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the MediaDataSource.SelectMethod property.
	/// </summary>
	public enum MediaSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion MediaSelectMethod

	#region MediaFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Media"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaFilter : SqlFilter<MediaColumn>
	{
	}
	
	#endregion MediaFilter

	#region MediaExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Media"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaExpressionBuilder : SqlExpressionBuilder<MediaColumn>
	{
	}
	
	#endregion MediaExpressionBuilder	

	#region MediaProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;MediaChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Media"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaProperty : ChildEntityProperty<MediaChildEntityTypes>
	{
	}
	
	#endregion MediaProperty
}

