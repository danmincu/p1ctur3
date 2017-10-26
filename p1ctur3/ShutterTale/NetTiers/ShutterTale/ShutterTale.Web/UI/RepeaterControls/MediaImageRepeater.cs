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
    /// A designer class for a strongly typed repeater <c>MediaImageRepeater</c>
    /// </summary>
	public class MediaImageRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaImageRepeaterDesigner"/> class.
        /// </summary>
		public MediaImageRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is MediaImageRepeater))
			{ 
				throw new ArgumentException("Component is not a MediaImageRepeater."); 
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
			MediaImageRepeater z = (MediaImageRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="MediaImageRepeater"/> Type.
    /// </summary>
	[Designer(typeof(MediaImageRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:MediaImageRepeater runat=\"server\"></{0}:MediaImageRepeater>")]
	public class MediaImageRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaImageRepeater"/> class.
        /// </summary>
		public MediaImageRepeater()
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
		[TemplateContainer(typeof(MediaImageItem))]
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
		[TemplateContainer(typeof(MediaImageItem))]
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
        [TemplateContainer(typeof(MediaImageItem))]
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
		[TemplateContainer(typeof(MediaImageItem))]
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
		[TemplateContainer(typeof(MediaImageItem))]
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
						ShutterTale.Entities.MediaImage entity = o as ShutterTale.Entities.MediaImage;
						MediaImageItem container = new MediaImageItem(entity);
	
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
	public class MediaImageItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private ShutterTale.Entities.MediaImage _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaImageItem"/> class.
        /// </summary>
		public MediaImageItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MediaImageItem"/> class.
        /// </summary>
		public MediaImageItem(ShutterTale.Entities.MediaImage entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Orientation
        /// </summary>
        /// <value>The Orientation.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? Orientation
		{
			get { return _entity.Orientation; }
		}
        /// <summary>
        /// Gets the YcbCrPositioning
        /// </summary>
        /// <value>The YcbCrPositioning.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? YcbCrPositioning
		{
			get { return _entity.YcbCrPositioning; }
		}
        /// <summary>
        /// Gets the ExposureTime
        /// </summary>
        /// <value>The ExposureTime.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? ExposureTime
		{
			get { return _entity.ExposureTime; }
		}
        /// <summary>
        /// Gets the Fnumber
        /// </summary>
        /// <value>The Fnumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? Fnumber
		{
			get { return _entity.Fnumber; }
		}
        /// <summary>
        /// Gets the ExposureProgram
        /// </summary>
        /// <value>The ExposureProgram.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? ExposureProgram
		{
			get { return _entity.ExposureProgram; }
		}
        /// <summary>
        /// Gets the IsoSpeedRatings
        /// </summary>
        /// <value>The IsoSpeedRatings.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int16? IsoSpeedRatings
		{
			get { return _entity.IsoSpeedRatings; }
		}
        /// <summary>
        /// Gets the ShutterSpeedValue
        /// </summary>
        /// <value>The ShutterSpeedValue.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? ShutterSpeedValue
		{
			get { return _entity.ShutterSpeedValue; }
		}
        /// <summary>
        /// Gets the ApertureValue
        /// </summary>
        /// <value>The ApertureValue.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? ApertureValue
		{
			get { return _entity.ApertureValue; }
		}
        /// <summary>
        /// Gets the MeteringMode
        /// </summary>
        /// <value>The MeteringMode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? MeteringMode
		{
			get { return _entity.MeteringMode; }
		}
        /// <summary>
        /// Gets the Flash
        /// </summary>
        /// <value>The Flash.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? Flash
		{
			get { return _entity.Flash; }
		}
        /// <summary>
        /// Gets the FocalLength
        /// </summary>
        /// <value>The FocalLength.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? FocalLength
		{
			get { return _entity.FocalLength; }
		}
        /// <summary>
        /// Gets the FlashpixVersion
        /// </summary>
        /// <value>The FlashpixVersion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String FlashpixVersion
		{
			get { return _entity.FlashpixVersion; }
		}
        /// <summary>
        /// Gets the ColorSpace
        /// </summary>
        /// <value>The ColorSpace.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? ColorSpace
		{
			get { return _entity.ColorSpace; }
		}
        /// <summary>
        /// Gets the SensingMethod
        /// </summary>
        /// <value>The SensingMethod.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? SensingMethod
		{
			get { return _entity.SensingMethod; }
		}
        /// <summary>
        /// Gets the ExposureMode
        /// </summary>
        /// <value>The ExposureMode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? ExposureMode
		{
			get { return _entity.ExposureMode; }
		}
        /// <summary>
        /// Gets the WhiteBalance
        /// </summary>
        /// <value>The WhiteBalance.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? WhiteBalance
		{
			get { return _entity.WhiteBalance; }
		}
        /// <summary>
        /// Gets the SceneCaptureType
        /// </summary>
        /// <value>The SceneCaptureType.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? SceneCaptureType
		{
			get { return _entity.SceneCaptureType; }
		}
        /// <summary>
        /// Gets the Sharpness
        /// </summary>
        /// <value>The Sharpness.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Byte? Sharpness
		{
			get { return _entity.Sharpness; }
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
        /// Gets a <see cref="T:ShutterTale.Entities.MediaImage"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public ShutterTale.Entities.MediaImage Entity
        {
            get { return _entity; }
        }
	}
}
