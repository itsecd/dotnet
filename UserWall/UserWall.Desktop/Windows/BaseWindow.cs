using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;

using Avalonia.ReactiveUI;

using ReactiveUI;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Windows;

public class BaseWindow<TViewModel> : ReactiveWindow<TViewModel>
    where TViewModel : BaseViewModel
{
    public BaseWindow()
    {
        this.WhenActivated(cd =>
        {
            var viewModel = ViewModel;
            if (viewModel is null)
                return;

            OnActivated(viewModel, cd);
        });
    }

    protected virtual void OnActivated(TViewModel viewModel, CompositeDisposable cd)
    {
        cd.Add(viewModel.CloseWindowInteraction.RegisterHandler(CloseWindow));
        cd.Add(viewModel.ShowMessageInteraction.RegisterHandler(ShowMessage));
    }

    private void CloseWindow(InteractionContext<object?, Unit> ctx)
    {
        Close(ctx.Input);
        ctx.SetOutput(Unit.Default);
    }

    private async Task ShowMessage(InteractionContext<MessageViewModel, Unit> ctx)
    {
        var window = new MessageWindow
        {
            ViewModel = ctx.Input
        };

        await window.ShowDialog(this);

        ctx.SetOutput(Unit.Default);
    }
}
