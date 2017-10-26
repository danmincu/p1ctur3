using System.Collections.Generic;

namespace Evasearch
{
    public class Specification
    {
        public Filter Filter { set; get; }
        public List<FacetBase> Facets { set; get; }
    }
}