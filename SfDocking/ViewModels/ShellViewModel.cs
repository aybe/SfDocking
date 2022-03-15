using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SfDocking.Views;

namespace SfDocking.ViewModels;

[UsedImplicitly]
internal sealed class ShellViewModel : BindableBase
{
    public ShellViewModel(IRegionManager manager)
    {
        Manager = manager ?? throw new ArgumentNullException(nameof(manager));

        OpenVideoMemory = new DelegateCommand(() => { NavigateTo(RegionNames.Main, nameof(VideoMemoryView)); });

        OpenVideoScreen = new DelegateCommand(() => { NavigateTo(RegionNames.Main, nameof(VideoScreenView)); });
    }

    private IRegionManager Manager { get; }

    public DelegateCommand OpenVideoMemory { get; }

    public DelegateCommand OpenVideoScreen { get; }

    private void NavigateTo(string regionName, string target)
    {
        Debug.WriteLine("Navigating...");

        Manager.RequestNavigate(regionName, target, result =>
        {
            if (result.Error != null)
            {
                throw result.Error;
            }
        });
    }
}