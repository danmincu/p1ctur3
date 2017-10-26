using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telekad.Ux;

namespace P1ctur3.Standalone.Models
{
    public abstract class Selectable : NotifyObject, ICallBackNotify
    {
        public bool? IsSelected
        {
            set { Set(() => IsSelected, value); SelectionChanged(); }
            get { return Get(() => IsSelected); }
        }

        public void SilentSet(bool? value)
        {
            Set(() => IsSelected, value);

            if (value != null && this.SelectableChildren != null)
            {
                foreach (var item in this.SelectableChildren)
                {
                    item.SilentSet(value);
                }
            }
        }
        public void SelectionChanged()
        {
            if (this.IsSelected != null)
            {
                if (this.SelectableChildren != null)
                {
                    foreach (var item in this.SelectableChildren)
                    {
                        item.SilentSet(this.IsSelected);
                    }
                }
            }
            if (this.SelectableParent != null)
            {
                this.SelectableParent.EvaluateSelection();
                this.SelectableParent.Notify(this);
            }
            else
                this.Notify(this);

        }

        public abstract IEnumerable<Selectable> SelectableChildren { get; }
        public abstract Selectable SelectableParent { get; }
        public void EvaluateSelection()
        {
            if (this.SelectableChildren != null)
            {

                if (this.SelectableChildren.All(a => a.IsSelected == true))
                    this.SilentSet(true);
                else
                    if (this.SelectableChildren.Any(a => a.IsSelected != false))
                        this.SilentSet(null);
                    else this.SilentSet(false);
            }

            if (this.SelectableParent != null)
                this.SelectableParent.EvaluateSelection();
        }

        public virtual void Notify(object sender)
        {
            if (this.SelectableParent != null)
                this.SelectableParent.Notify(this);
        }

    }
}
