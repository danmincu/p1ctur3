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
	/// Represents the DataRepository.MediaVideoProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(MediaVideoDataSourceDesigner))]
	public class MediaVideoDataSource : ProviderDataSource<MediaVideo, MediaVideoKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaVideoDataSource class.
		/// </summary>
		public MediaVideoDataSource() : base(DataRepository.MediaVideoProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the MediaVideoDataSourceView used by the MediaVideoDataSource.
		/// </summary>
		protected MediaVideoDataSourceView MediaVideoView
		{
			get { return ( View as MediaVideoDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the MediaVideoDataSource control invokes to retrieve data.
		/// </summary>
		public MediaVideoSelectMethod SelectMethod
		{
			get
			{
				MediaVideoSelectMethod selectMethod = MediaVideoSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (MediaVideoSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the MediaVideoDataSourceView class that is to be
		/// used by the MediaVideoDataSource.
		/// </summary>
		/// <returns>An instance of the MediaVideoDataSourceView class.</returns>
		protected override BaseDataSourceView<MediaVideo, MediaVideoKey> GetNewDataSourceView()
		{
			return new MediaVideoDataSourceView(this, DefaultViewName);
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
	/// Supports the MediaVideoDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class MediaVideoDataSourceView : ProviderDataSourceView<MediaVideo, MediaVideoKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaVideoDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the MediaVideoDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public MediaVideoDataSourceView(MediaVideoDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal MediaVideoDataSource MediaVideoOwner
		{
			get { return Owner as MediaVideoDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal MediaVideoSelectMethod SelectMethod
		{
			get { return MediaVideoOwner.SelectMethod; }
			set { MediaVideoOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal MediaVideoProviderBase MediaVideoProvider
		{
			get { return Provider as MediaVideoProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<MediaVideo> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<MediaVideo> results = null;
			MediaVideo item;
			count = 0;
			
			System.Guid _id;

			switch ( SelectMethod )
			{
				case MediaVideoSelectMethod.Get:
					MediaVideoKey entityKey  = new MediaVideoKey();
					entityKey.Load(values);
					item = MediaVideoProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<MediaVideo>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case MediaVideoSelectMethod.GetAll:
                    results = MediaVideoProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case MediaVideoSelectMethod.GetPaged:
					results = MediaVideoProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case MediaVideoSelectMethod.Find:
					if ( FilterParameters != null )
						results = MediaVideoProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = MediaVideoProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case MediaVideoSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Id"], typeof(System.Guid)) : Guid.Empty;
					item = MediaVideoProvider.GetById(GetTransactionManager(), _id);
					results = new TList<MediaVideo>();
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
			if ( SelectMethod == MediaVideoSelectMethod.Get || SelectMethod == MediaVideoSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(MediaVideo entity)
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
				MediaVideo entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					MediaVideoProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<MediaVideo> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			MediaVideoProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region MediaVideoDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the MediaVideoDataSource class.
	/// </summary>
	public class MediaVideoDataSourceDesigner : ProviderDataSourceDesigner<MediaVideo, MediaVideoKey>
	{
		/// <summary>
		/// Initializes a new instance of the MediaVideoDataSourceDesigner class.
		/// </summary>
		public MediaVideoDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaVideoSelectMethod SelectMethod
		{
			get { return ((MediaVideoDataSource) DataSource).SelectMethod; }
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
				actions.Add(new MediaVideoDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region MediaVideoDataSourceActionList

	/// <summary>
	/// Supports the MediaVideoDataSourceDesigner class.
	/// </summary>
	internal class MediaVideoDataSourceActionList : DesignerActionList
	{
		private MediaVideoDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the MediaVideoDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public MediaVideoDataSourceActionList(MediaVideoDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaVideoSelectMethod SelectMethod
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

	#endregion MediaVideoDataSourceActionList
	
	#endregion MediaVideoDataSourceDesigner
	
	#region MediaVideoSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the MediaVideoDataSource.SelectMethod property.
	/// </summary>
	public enum MediaVideoSelectMethod
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
	
	#endregion MediaVideoSelectMethod

	#region MediaVideoFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaVideo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaVideoFilter : SqlFilter<MediaVideoColumn>
	{
	}
	
	#endregion MediaVideoFilter

	#region MediaVideoExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaVideo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaVideoExpressionBuilder : SqlExpressionBuilder<MediaVideoColumn>
	{
	}
	
	#endregion MediaVideoExpressionBuilder	

	#region MediaVideoProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;MediaVideoChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="MediaVideo"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaVideoProperty : ChildEntityProperty<MediaVideoChildEntityTypes>
	{
	}
	
	#endregion MediaVideoProperty
}

