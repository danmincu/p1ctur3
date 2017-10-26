﻿#region Using Directives
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
	/// Represents the DataRepository.MediaImageProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(MediaImageDataSourceDesigner))]
	public class MediaImageDataSource : ProviderDataSource<MediaImage, MediaImageKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaImageDataSource class.
		/// </summary>
		public MediaImageDataSource() : base(DataRepository.MediaImageProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the MediaImageDataSourceView used by the MediaImageDataSource.
		/// </summary>
		protected MediaImageDataSourceView MediaImageView
		{
			get { return ( View as MediaImageDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the MediaImageDataSource control invokes to retrieve data.
		/// </summary>
		public MediaImageSelectMethod SelectMethod
		{
			get
			{
				MediaImageSelectMethod selectMethod = MediaImageSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (MediaImageSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the MediaImageDataSourceView class that is to be
		/// used by the MediaImageDataSource.
		/// </summary>
		/// <returns>An instance of the MediaImageDataSourceView class.</returns>
		protected override BaseDataSourceView<MediaImage, MediaImageKey> GetNewDataSourceView()
		{
			return new MediaImageDataSourceView(this, DefaultViewName);
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
	/// Supports the MediaImageDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class MediaImageDataSourceView : ProviderDataSourceView<MediaImage, MediaImageKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaImageDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the MediaImageDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public MediaImageDataSourceView(MediaImageDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal MediaImageDataSource MediaImageOwner
		{
			get { return Owner as MediaImageDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal MediaImageSelectMethod SelectMethod
		{
			get { return MediaImageOwner.SelectMethod; }
			set { MediaImageOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal MediaImageProviderBase MediaImageProvider
		{
			get { return Provider as MediaImageProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<MediaImage> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<MediaImage> results = null;
			MediaImage item;
			count = 0;
			
			System.Guid _id;

			switch ( SelectMethod )
			{
				case MediaImageSelectMethod.Get:
					MediaImageKey entityKey  = new MediaImageKey();
					entityKey.Load(values);
					item = MediaImageProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<MediaImage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case MediaImageSelectMethod.GetAll:
                    results = MediaImageProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case MediaImageSelectMethod.GetPaged:
					results = MediaImageProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case MediaImageSelectMethod.Find:
					if ( FilterParameters != null )
						results = MediaImageProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = MediaImageProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case MediaImageSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Id"], typeof(System.Guid)) : Guid.Empty;
					item = MediaImageProvider.GetById(GetTransactionManager(), _id);
					results = new TList<MediaImage>();
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
			if ( SelectMethod == MediaImageSelectMethod.Get || SelectMethod == MediaImageSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(MediaImage entity)
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
				MediaImage entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					MediaImageProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<MediaImage> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			MediaImageProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region MediaImageDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the MediaImageDataSource class.
	/// </summary>
	public class MediaImageDataSourceDesigner : ProviderDataSourceDesigner<MediaImage, MediaImageKey>
	{
		/// <summary>
		/// Initializes a new instance of the MediaImageDataSourceDesigner class.
		/// </summary>
		public MediaImageDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaImageSelectMethod SelectMethod
		{
			get { return ((MediaImageDataSource) DataSource).SelectMethod; }
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
				actions.Add(new MediaImageDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region MediaImageDataSourceActionList

	/// <summary>
	/// Supports the MediaImageDataSourceDesigner class.
	/// </summary>
	internal class MediaImageDataSourceActionList : DesignerActionList
	{
		private MediaImageDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the MediaImageDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public MediaImageDataSourceActionList(MediaImageDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaImageSelectMethod SelectMethod
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

	#endregion MediaImageDataSourceActionList
	
	#endregion MediaImageDataSourceDesigner
	
	#region MediaImageSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the MediaImageDataSource.SelectMethod property.
	/// </summary>
	public enum MediaImageSelectMethod
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
	
	#endregion MediaImageSelectMethod

	#region MediaImageFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaImage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaImageFilter : SqlFilter<MediaImageColumn>
	{
	}
	
	#endregion MediaImageFilter

	#region MediaImageExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaImage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaImageExpressionBuilder : SqlExpressionBuilder<MediaImageColumn>
	{
	}
	
	#endregion MediaImageExpressionBuilder	

	#region MediaImageProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;MediaImageChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="MediaImage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaImageProperty : ChildEntityProperty<MediaImageChildEntityTypes>
	{
	}
	
	#endregion MediaImageProperty
}
