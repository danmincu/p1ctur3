using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Telekad.Services.MapTilesAdapter
{
    public class LayersCollection : ConfigurationElementCollection
    {
        private readonly static CompositionContainer CompositionContainer = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "debugMap";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Debug.DebugMapLayerConfigurationElement();
        }
        
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return (from p in CompositionContainer.GetExports<MapLayerConfiguration, IDictionary<string, object>>()
                           where elementName.Equals(p.Metadata["ConfigurationElementName"])
                           select p.Value).First();

        }

        protected override bool IsElementName(string elementName)
        {
            return true;
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return new ConfigurationPropertyCollection();
            }
        }

        public MapLayerConfiguration this[int index]
        {
            get
            {
                return base.BaseGet(index) as MapLayerConfiguration;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((MapLayerConfiguration)element).Name;
        }
    }
}