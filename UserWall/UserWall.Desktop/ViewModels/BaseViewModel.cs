using System.Reactive;

using ReactiveUI;

namespace UserWall.Desktop.ViewModels;

public class BaseViewModel : ReactiveObject
{
    public Interaction<object?, Unit> CloseWindowInteraction { get; } = new();
}
