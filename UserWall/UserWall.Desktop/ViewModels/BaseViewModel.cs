using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using ReactiveUI;

namespace UserWall.Desktop.ViewModels;

public class BaseViewModel : ReactiveObject
{
    public Interaction<object?, Unit> CloseWindowInteraction { get; } = new();

    public Interaction<MessageViewModel, Unit> ShowMessageInteraction { get; } = new();

    protected void ShowMessage(MessageViewModel messageViewModel)
    {
        RxApp.MainThreadScheduler.Schedule(async () =>
        {
            try
            {
                await ShowMessageInteraction.Handle(messageViewModel);
            }
            catch
            {
                // Ignore
            }
        });
    }
}
