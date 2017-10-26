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
	/// Represents the DataRepository.MediaAudioProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(MediaAudioDataSourceDesigner))]
	public class MediaAudioDataSource : ProviderDataSource<MediaAudio, MediaAudioKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaAudioDataSource class.
		/// </summary>
		public MediaAudioDataSource() : base(DataRepository.MediaAudioProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the MediaAudioDataSourceView used by the MediaAudioDataSource.
		/// </summary>
		protected MediaAudioDataSourceView MediaAudioView
		{
			get { return ( View as MediaAudioDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the MediaAudioDataSource control invokes to retrieve data.
		/// </summary>
		public MediaAudioSelectMethod SelectMethod
		{
			get
			{
				MediaAudioSelectMethod selectMethod = MediaAudioSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (MediaAudioSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the MediaAudioDataSourceView class that is to be
		/// used by the MediaAudioDataSource.
		/// </summary>
		/// <returns>An instance of the MediaAudioDataSourceView class.</returns>
		protected override BaseDataSourceView<MediaAudio, MediaAudioKey> GetNewDataSourceView()
		{
			return new MediaAudioDataSourceView(this, DefaultViewName);
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
	/// Supports the MediaAudioDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class MediaAudioDataSourceView : ProviderDataSourceView<MediaAudio, MediaAudioKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MediaAudioDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the MediaAudioDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public MediaAudioDataSourceView(MediaAudioDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal MediaAudioDataSource MediaAudioOwner
		{
			get { return Owner as MediaAudioDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal MediaAudioSelectMethod SelectMethod
		{
			get { return MediaAudioOwner.SelectMethod; }
			set { MediaAudioOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal MediaAudioProviderBase MediaAudioProvider
		{
			get { return Provider as MediaAudioProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<MediaAudio> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<MediaAudio> results = null;
			MediaAudio item;
			count = 0;
			
			System.Guid _id;

			switch ( SelectMethod )
			{
				case MediaAudioSelectMethod.Get:
					MediaAudioKey entityKey  = new MediaAudioKey();
					entityKey.Load(values);
					item = MediaAudioProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<MediaAudio>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case MediaAudioSelectMethod.GetAll:
                    results = MediaAudioProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case MediaAudioSelectMethod.GetPaged:
					results = MediaAudioProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case MediaAudioSelectMethod.Find:
					if ( FilterParameters != null )
						results = MediaAudioProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = MediaAudioProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case MediaAudioSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Id"], typeof(System.Guid)) : Guid.Empty;
					item = MediaAudioProvider.GetById(GetTransactionManager(), _id);
					results = new TList<MediaAudio>();
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
			if ( SelectMethod == MediaAudioSelectMethod.Get || SelectMethod == MediaAudioSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(MediaAudio entity)
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
				MediaAudio entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					MediaAudioProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<MediaAudio> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			MediaAudioProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region MediaAudioDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the MediaAudioDataSource class.
	/// </summary>
	public class MediaAudioDataSourceDesigner : ProviderDataSourceDesigner<MediaAudio, MediaAudioKey>
	{
		/// <summary>
		/// Initializes a new instance of the MediaAudioDataSourceDesigner class.
		/// </summary>
		public MediaAudioDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaAudioSelectMethod SelectMethod
		{
			get { return ((MediaAudioDataSource) DataSource).SelectMethod; }
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
				actions.Add(new MediaAudioDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region MediaAudioDataSourceActionList

	/// <summary>
	/// Supports the MediaAudioDataSourceDesigner class.
	/// </summary>
	internal class MediaAudioDataSourceActionList : DesignerActionList
	{
		private MediaAudioDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the MediaAudioDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public MediaAudioDataSourceActionList(MediaAudioDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public MediaAudioSelectMethod SelectMethod
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

	#endregion MediaAudioDataSourceActionList
	
	#endregion MediaAudioDataSourceDesigner
	
	#region MediaAudioSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the MediaAudioDataSource.SelectMethod property.
	/// </summary>
	public enum MediaAudioSelectMethod
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
	
	#endregion MediaAudioSelectMethod

	#region MediaAudioFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaAudio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaAudioFilter : SqlFilter<MediaAudioColumn>
	{
	}
	
	#endregion MediaAudioFilter

	#region MediaAudioExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MediaAudio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaAudioExpressionBuilder : SqlExpressionBuilder<MediaAudioColumn>
	{
	}
	
	#endregion MediaAudioExpressionBuilder	

	#region MediaAudioProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;MediaAudioChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="MediaAudio"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MediaAudioProperty : ChildEntityProperty<MediaAudioChildEntityTypes>
	{
	}
	
	#endregion MediaAudioProperty
}

