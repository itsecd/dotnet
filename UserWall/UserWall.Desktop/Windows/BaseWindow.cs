using System.Reactive;
using System.Reactive.Disposables;

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
    }

    private void CloseWindow(InteractionContext<object?, Unit> ctx)
    {
        Close(ctx.Input);
        ctx.SetOutput(Unit.Default);
    }
}
