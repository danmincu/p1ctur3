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
	/// Represents the DataRepository.PreviewsProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(PreviewsDataSourceDesigner))]
	public class PreviewsDataSource : ProviderDataSource<Previews, PreviewsKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PreviewsDataSource class.
		/// </summary>
		public PreviewsDataSource() : base(DataRepository.PreviewsProvider)
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the PreviewsDataSourceView used by the PreviewsDataSource.
		/// </summary>
		protected PreviewsDataSourceView PreviewsView
		{
			get { return ( View as PreviewsDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the PreviewsDataSource control invokes to retrieve data.
		/// </summary>
		public PreviewsSelectMethod SelectMethod
		{
			get
			{
				PreviewsSelectMethod selectMethod = PreviewsSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (PreviewsSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the PreviewsDataSourceView class that is to be
		/// used by the PreviewsDataSource.
		/// </summary>
		/// <returns>An instance of the PreviewsDataSourceView class.</returns>
		protected override BaseDataSourceView<Previews, PreviewsKey> GetNewDataSourceView()
		{
			return new PreviewsDataSourceView(this, DefaultViewName);
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
	/// Supports the PreviewsDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class PreviewsDataSourceView : ProviderDataSourceView<Previews, PreviewsKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the PreviewsDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the PreviewsDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public PreviewsDataSourceView(PreviewsDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal PreviewsDataSource PreviewsOwner
		{
			get { return Owner as PreviewsDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal PreviewsSelectMethod SelectMethod
		{
			get { return PreviewsOwner.SelectMethod; }
			set { PreviewsOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal PreviewsProviderBase PreviewsProvider
		{
			get { return Provider as PreviewsProviderBase; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<Previews> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<Previews> results = null;
			Previews item;
			count = 0;
			
			System.Guid _mediumId;
			System.Guid _id;

			switch ( SelectMethod )
			{
				case PreviewsSelectMethod.Get:
					PreviewsKey entityKey  = new PreviewsKey();
					entityKey.Load(values);
					item = PreviewsProvider.Get(GetTransactionManager(), entityKey);
					results = new TList<Previews>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case PreviewsSelectMethod.GetAll:
                    results = PreviewsProvider.GetAll(GetTransactionManager(), StartIndex, PageSize, out count);
                    break;
				case PreviewsSelectMethod.GetPaged:
					results = PreviewsProvider.GetPaged(GetTransactionManager(), WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case PreviewsSelectMethod.Find:
					if ( FilterParameters != null )
						results = PreviewsProvider.Find(GetTransactionManager(), FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = PreviewsProvider.Find(GetTransactionManager(), WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case PreviewsSelectMethod.GetById:
					_id = ( values["Id"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Id"], typeof(System.Guid)) : Guid.Empty;
					item = PreviewsProvider.GetById(GetTransactionManager(), _id);
					results = new TList<Previews>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case PreviewsSelectMethod.GetByMediumId:
					_mediumId = ( values["MediumId"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["MediumId"], typeof(System.Guid)) : Guid.Empty;
					results = PreviewsProvider.GetByMediumId(GetTransactionManager(), _mediumId, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == PreviewsSelectMethod.Get || SelectMethod == PreviewsSelectMethod.GetById )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(Previews entity)
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
				Previews entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// execute deep load method
					PreviewsProvider.DeepLoad(GetTransactionManager(), GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<Previews> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// execute deep load method
			PreviewsProvider.DeepLoad(GetTransactionManager(), entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region PreviewsDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the PreviewsDataSource class.
	/// </summary>
	public class PreviewsDataSourceDesigner : ProviderDataSourceDesigner<Previews, PreviewsKey>
	{
		/// <summary>
		/// Initializes a new instance of the PreviewsDataSourceDesigner class.
		/// </summary>
		public PreviewsDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PreviewsSelectMethod SelectMethod
		{
			get { return ((PreviewsDataSource) DataSource).SelectMethod; }
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
				actions.Add(new PreviewsDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region PreviewsDataSourceActionList

	/// <summary>
	/// Supports the PreviewsDataSourceDesigner class.
	/// </summary>
	internal class PreviewsDataSourceActionList : DesignerActionList
	{
		private PreviewsDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the PreviewsDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public PreviewsDataSourceActionList(PreviewsDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public PreviewsSelectMethod SelectMethod
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

	#endregion PreviewsDataSourceActionList
	
	#endregion PreviewsDataSourceDesigner
	
	#region PreviewsSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the PreviewsDataSource.SelectMethod property.
	/// </summary>
	public enum PreviewsSelectMethod
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
		/// Represents the GetByMediumId method.
		/// </summary>
		GetByMediumId,
		/// <summary>
		/// Represents the GetById method.
		/// </summary>
		GetById
	}
	
	#endregion PreviewsSelectMethod

	#region PreviewsFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Previews"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PreviewsFilter : SqlFilter<PreviewsColumn>
	{
	}
	
	#endregion PreviewsFilter

	#region PreviewsExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Previews"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PreviewsExpressionBuilder : SqlExpressionBuilder<PreviewsColumn>
	{
	}
	
	#endregion PreviewsExpressionBuilder	

	#region PreviewsProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;PreviewsChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="Previews"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class PreviewsProperty : ChildEntityProperty<PreviewsChildEntityTypes>
	{
	}
	
	#endregion PreviewsProperty
}
