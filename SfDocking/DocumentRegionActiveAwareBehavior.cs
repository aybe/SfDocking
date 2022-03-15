using System;
using System.Windows;
using System.Windows.Controls;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Syncfusion.Windows.Tools.Controls;

namespace SfDocking;

public class DocumentRegionActiveAwareBehavior : RegionBehavior, IHostAwareRegionBehavior
{
    public const string BehaviorKey = "DocumentRegionActiveAwareBehavior";

    public DependencyObject HostControl { get; set; } = null!;

    protected override void OnAttach()
    {
        var manager = HostControl as DockingManager ?? throw new InvalidCastException();

        var container = manager.DocContainer as DocumentContainer ?? throw new InvalidCastException();

        container.AddTabDocumentAtLast = true;

        container.ActiveDocumentChanged += OnActiveDocumentChanged;
    }

    private void OnActiveDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue != null)
        {
            var item = e.OldValue;

            // are we dealing with a ContentPane directly

            if (Region.Views.Contains(item) && Region.ActiveViews.Contains(item))
            {
                Region.Deactivate(item);
            }
            else
            {
                // now check to see if we have any views that were injected

                if (item is ContentControl contentControl)
                {
                    var injectedView = contentControl.Content;

                    if (Region.Views.Contains(injectedView) && Region.ActiveViews.Contains(injectedView))
                    {
                        Region.Deactivate(injectedView);
                    }
                }
            }
        }

        if (e.NewValue != null)
        {
            var item = e.NewValue;

            // are we dealing with a ContentPane directly

            if (Region.Views.Contains(item) && !Region.ActiveViews.Contains(item))
            {
                Region.Activate(item);
            }
            else
            {
                // now check to see if we have any views that were injected

                if (item is ContentControl contentControl)
                {
                    var injectedView = contentControl.Content;

                    if (Region.Views.Contains(injectedView) && !Region.ActiveViews.Contains(injectedView))
                    {
                        Region.Activate(injectedView);
                    }
                }
            }
        }
    }
}