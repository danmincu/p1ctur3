using C1.WPF.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace P1ctur3.Viewer
{
    /// <summary>
    /// Defines a catch-all class of utility functions for use with the mapping components.
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Sets the center and target zoom of the given ComponentOne map. This addresses the following bugs:
        ///   - Bug 9234: The tiles were not being properly requested after a recenter
        /// </summary>
        /// <param name="c1Map">Map to be centered and zoomed.</param>
        /// <param name="center">New center point for the map.</param>
        /// <param name="targetZoom">New target zoom for the map.</param>
        public static void SetCenterAndTargetZoom(this C1Maps c1Map, Point center, double targetZoom)
        {
            // The ComponentOne workaround can only be applied if the tile source is not null, otherwise the map control
            // gets itself into a state where no tiles will ever be loaded.)
            if (c1Map.Source != null)
            {
                // Bug 9234: Workaround as provided by ComponentOne support. The saving and restoring of the tile source tricks the 
                // map into acting properly and requesting tiles for the panned region.

                // Check if the property was set using a local value (resource, binding, value), and not by other means (styles, default values).
                // If it's a local value, then save it.
                var enumerator = c1Map.GetLocalValueEnumerator();

                // This value could be a resource reference, a data binding, or the actual value.
                object originalLocalValue = null;

                while (enumerator.MoveNext() && originalLocalValue == null)
                {
                    var entry = enumerator.Current;
                    if (entry.Property.Equals(C1Maps.SourceProperty))
                        originalLocalValue = entry.Value;
                }

                c1Map.Source = null;

                c1Map.Center = center;
                c1Map.TargetZoom = targetZoom;

                // If there was no original value, then clear the property.
                if (originalLocalValue == null)
                {
                    c1Map.ClearValue(C1Maps.SourceProperty);
                }
                // Restore the old data binding.  We can't use the actual BindingExpression because
                // it cannot be assigned a second time (even to the same object).
                else if (originalLocalValue is BindingExpressionBase)
                {
                    c1Map.ClearValue(C1Maps.SourceProperty);
                    c1Map.SetBinding(C1Maps.SourceProperty, ((BindingExpressionBase)originalLocalValue).ParentBindingBase);
                }
                // Otherwise reset it to it's original value.
                else
                {
                    c1Map.SetValue(C1Maps.SourceProperty, originalLocalValue);
                }
            }
            else
            {
                c1Map.Center = center;
                c1Map.TargetZoom = targetZoom;
            }
        }
    }
}
