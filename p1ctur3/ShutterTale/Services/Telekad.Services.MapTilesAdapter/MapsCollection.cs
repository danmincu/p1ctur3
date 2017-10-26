using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Telekad.Services.MapTilesAdapter
{
    public class MapsCollection : ConfigurationElementCollection, IEnumerable<MapCollectionElement>
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get { return "map"; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return new ConfigurationPropertyCollection();
            }
        }

        public MapCollectionElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as MapCollectionElement;
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


        public new IEnumerator<MapCollectionElement> GetEnumerator()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as MapCollectionElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MapCollectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MapCollectionElement)element).Name;
        }
    }
}