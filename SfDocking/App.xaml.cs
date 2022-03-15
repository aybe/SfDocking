using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SfDocking.Modules;
using SfDocking.ViewModels;
using SfDocking.Views;
using Syncfusion.Windows.Tools.Controls;

namespace SfDocking;

public partial class App
{
    protected override Window CreateShell()
    {
        return Container.Resolve<Shell>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<VideoScreenView, VideoScreenViewModel>();
        containerRegistry.RegisterForNavigation<VideoMemoryView, VideoMemoryViewModel>();
    }

    protected override IModuleCatalog CreateModuleCatalog()
    {
        var catalog = base.CreateModuleCatalog();

        catalog.AddModule<VideoScreenModule>();
        catalog.AddModule<VideoMemoryModule>();

        return catalog;
    }

    protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
    {
        base.ConfigureRegionAdapterMappings(regionAdapterMappings);

        regionAdapterMappings.RegisterMapping(typeof(DockingManager), Container.Resolve<DockingManagerRegionAdapter>());
    }
}