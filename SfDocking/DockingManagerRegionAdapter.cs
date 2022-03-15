using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using JetBrains.Annotations;
using Prism.Regions;
using Syncfusion.Windows.Tools.Controls;

namespace SfDocking;

[UsedImplicitly]
public class DockingManagerRegionAdapter : RegionAdapterBase<DockingManager>
{
    public DockingManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, DockingManager regionTarget)
    {
        region.Views.CollectionChanged += (s, e) =>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if (e.NewItems != null)
                    {
                        foreach (var element in e.NewItems.Cast<FrameworkElement>())
                        {
                            if (regionTarget.Children.Contains(element))
                                continue;

                            regionTarget.BeginInit();
                            regionTarget.Children.Add(element);
                            regionTarget.EndInit();
                        }
                    }

                    break;
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new SingleActiveRegion();
    }

    protected override void AttachBehaviors(IRegion region, DockingManager regionTarget)
    {
        base.AttachBehaviors(region, regionTarget);

        if (region.Behaviors.ContainsKey(DocumentRegionActiveAwareBehavior.BehaviorKey))
            return;

        var behavior = new DocumentRegionActiveAwareBehavior
        {
            HostControl = regionTarget
        };

        region.Behaviors.Add(DocumentRegionActiveAwareBehavior.BehaviorKey, behavior);
    }
}