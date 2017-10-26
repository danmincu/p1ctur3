using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace ShutterTale.Web.UI
{
    /// <summary>
    /// A designer class for a strongly typed repeater <c>MediaRepeater</c>
    /// </summary>
	public class MediaRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaRepeaterDesigner"/> class.
        /// </summary>
		public MediaRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is MediaRepeater))
			{ 
				throw new ArgumentException("Component is not a MediaRepeater."); 
			} 
			base.Initialize(component); 
			base.SetViewFlags(ViewFlags.TemplateEditing, true); 
		}


		/// <summary>
		/// Generate HTML for the designer
		/// </summary>
		/// <returns>a string of design time HTML</returns>
		public override string GetDesignTimeHtml()
		{

			// Get the instance this designer applies to
			//
			MediaRepeater z = (MediaRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();

			//return z.RenderAtDesignTime();

			//	ControlCollection c = z.Controls;
			//Totem z = (Totem) Component;
			//Totem z = (Totem) Component;
			//return ("<div style='border: 1px gray dotted; background-color: lightgray'><b>TagStat :</b> zae |  qsdds</div>");

		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="MediaRepeater"/> Type.
    /// </summary>
	[Designer(typeof(MediaRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:MediaRepeater runat=\"server\"></{0}:MediaRepeater>")]
	public class MediaRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaRepeater"/> class.
        /// </summary>
		public MediaRepeater()
		{
		}

		/// <summary>
        /// Gets a <see cref="T:System.Web.UI.ControlCollection"></see> object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        /// <value></value>
        /// <returns>The collection of child controls for the specified server control.</returns>
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		private ITemplate m_headerTemplate;
		/// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(MediaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate HeaderTemplate
		{
			get { return m_headerTemplate; }
			set { m_headerTemplate = value; }
		}

		private ITemplate m_itemTemplate;
		/// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(MediaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate ItemTemplate
		{
			get { return m_itemTemplate; }
			set { m_itemTemplate = value; }
		}

		private ITemplate m_seperatorTemplate;
        /// <summary>
        /// Gets or sets the Seperator Template
        /// </summary>
        [Browsable(false)]
        [TemplateContainer(typeof(MediaItem))]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ITemplate SeperatorTemplate
        {
            get { return m_seperatorTemplate; }
            set { m_seperatorTemplate = value; }
        }
			
		private ITemplate m_altenateItemTemplate;
        /// <summary>
        /// Gets or sets the alternating item template.
        /// </summary>
        /// <value>The alternating item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(MediaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate AlternatingItemTemplate
		{
			get { return m_altenateItemTemplate; }
			set { m_altenateItemTemplate = value; }
		}

		private ITemplate m_footerTemplate;
        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>The footer template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(MediaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//      {
//         if (ChildControlsCreated)
//         {
//            return;
//         }

//         Controls.Clear();

//         //Instantiate the Header template (if exists)
//         if (m_headerTemplate != null)
//         {
//            Control headerItem = new Control();
//            m_headerTemplate.InstantiateIn(headerItem);
//            Controls.Add(headerItem);
//         }

//         //Instantiate the Footer template (if exists)
//         if (m_footerTemplate != null)
//         {
//            Control footerItem = new Control();
//            m_footerTemplate.InstantiateIn(footerItem);
//            Controls.Add(footerItem);
//         }
//
//         ChildControlsCreated = true;
//      }
	
		/// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            
        }

        /// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
                
        }		
		
		/// <summary>
      	/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
      	/// </summary>
		protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
      	{
         int pos = 0;

         if (dataBinding)
         {
            //Instantiate the Header template (if exists)
            if (m_headerTemplate != null)
            {
                Control headerItem = new Control();
                m_headerTemplate.InstantiateIn(headerItem);
                Controls.Add(headerItem);
            }
			if (dataSource != null)
			{
				foreach (object o in dataSource)
				{
						ShutterTale.Entities.Media entity = o as ShutterTale.Entities.Media;
						MediaItem container = new MediaItem(entity);
	
						if (m_itemTemplate != null && (pos % 2) == 0)
						{
							m_itemTemplate.InstantiateIn(container);
							
							if (m_seperatorTemplate != null)
							{
								m_seperatorTemplate.InstantiateIn(container);
							}
						}
						else
						{
							if (m_altenateItemTemplate != null)
							{
								m_altenateItemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
								
							}
							else if (m_itemTemplate != null)
							{
								m_itemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
							}
							else
							{
								// no template !!!
							}
						}
						Controls.Add(container);
						
						container.DataBind();
						
						pos++;
				}
			}
            //Instantiate the Footer template (if exists)
            if (m_footerTemplate != null)
            {
                Control footerItem = new Control();
                m_footerTemplate.InstantiateIn(footerItem);
                Controls.Add(footerItem);
            }

		}
			
			return pos;
		}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#region Design time
        /// <summary>
        /// Renders at design time.
        /// </summary>
        /// <returns>a  string of the Designed HTML</returns>
		internal string RenderAtDesignTime()
		{			
			return "Designer currently not implemented"; 
		}

		#endregion
	}

    /// <summary>
    /// A wrapper type for the entity
    /// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class MediaItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private ShutterTale.Entities.Media _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaItem"/> class.
        /// </summary>
		public MediaItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaItem"/> class.
        /// </summary>
		public MediaItem(ShutterTale.Entities.Media entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Id
        /// </summary>
        /// <value>The Id.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid Id
		{
			get { return _entity.Id; }
		}
        /// <summary>
        /// Gets the CaptureDateTime
        /// </summary>
        /// <value>The CaptureDateTime.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? CaptureDateTime
		{
			get { return _entity.CaptureDateTime; }
		}
        /// <summary>
        /// Gets the FileDateTime
        /// </summary>
        /// <value>The FileDateTime.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime FileDateTime
		{
			get { return _entity.FileDateTime; }
		}
        /// <summary>
        /// Gets the ImportDateTime
        /// </summary>
        /// <value>The ImportDateTime.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime ImportDateTime
		{
			get { return _entity.ImportDateTime; }
		}
        /// <summary>
        /// Gets the Location
        /// </summary>
        /// <value>The Location.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Object Location
		{
			get { return _entity.Location; }
		}
        /// <summary>
        /// Gets the Pixelx
        /// </summary>
        /// <value>The Pixelx.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Pixelx
		{
			get { return _entity.Pixelx; }
		}
        /// <summary>
        /// Gets the Pixely
        /// </summary>
        /// <value>The Pixely.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Pixely
		{
			get { return _entity.Pixely; }
		}
        /// <summary>
        /// Gets the Dpi
        /// </summary>
        /// <value>The Dpi.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double Dpi
		{
			get { return _entity.Dpi; }
		}
        /// <summary>
        /// Gets the ContentType
        /// </summary>
        /// <value>The ContentType.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String ContentType
		{
			get { return _entity.ContentType; }
		}
        /// <summary>
        /// Gets the Make
        /// </summary>
        /// <value>The Make.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Make
		{
			get { return _entity.Make; }
		}
        /// <summary>
        /// Gets the Model
        /// </summary>
        /// <value>The Model.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Model
		{
			get { return _entity.Model; }
		}
        /// <summary>
        /// Gets the SoftwareVersion
        /// </summary>
        /// <value>The SoftwareVersion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String SoftwareVersion
		{
			get { return _entity.SoftwareVersion; }
		}
        /// <summary>
        /// Gets the Origin
        /// </summary>
        /// <value>The Origin.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Origin
		{
			get { return _entity.Origin; }
		}
        /// <summary>
        /// Gets the Size
        /// </summary>
        /// <value>The Size.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Size
		{
			get { return _entity.Size; }
		}
        /// <summary>
        /// Gets the Quadkey18
        /// </summary>
        /// <value>The Quadkey18.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Quadkey18
		{
			get { return _entity.Quadkey18; }
		}

        /// <summary>
        /// Gets a <see cref="T:ShutterTale.Entities.Media"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public ShutterTale.Entities.Media Entity
        {
            get { return _entity; }
        }
	}
}
