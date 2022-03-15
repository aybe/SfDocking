using JetBrains.Annotations;
using Prism.Mvvm;

namespace SfDocking.ViewModels;

[UsedImplicitly]
internal sealed class VideoMemoryViewModel : BindableBase
{
    public string Description => "Video Memory";
}