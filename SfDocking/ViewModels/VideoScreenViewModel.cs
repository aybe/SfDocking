using JetBrains.Annotations;
using Prism.Mvvm;

namespace SfDocking.ViewModels;

[UsedImplicitly]
internal sealed class VideoScreenViewModel : BindableBase
{
    public string Description => "Video Screen";
}