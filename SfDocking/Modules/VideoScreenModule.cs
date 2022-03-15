using JetBrains.Annotations;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SfDocking.Views;

namespace SfDocking.Modules;

[UsedImplicitly]
internal sealed class VideoScreenModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var manager = containerProvider.Resolve<IRegionManager>();

        manager.RegisterViewWithRegion<VideoScreenView>(RegionNames.Main);
    }
}