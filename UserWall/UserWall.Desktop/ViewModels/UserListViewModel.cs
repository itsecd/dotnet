using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Avalonia.Threading;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace UserWall.Desktop.ViewModels;

public sealed class UserListViewModel : ReactiveObject
{
    public ObservableCollection<UserDto> Items { get; } = new();

    [Reactive] public UserDto? SelectedItem { get; set; }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    public Interaction<UserDto?, UserDto?> ItemInteraction { get; } = new();

    public UserListViewModel()
    {
        var hasSelectedItem = this
            .WhenAnyValue(viewModel => viewModel.SelectedItem)
            .Select(selectedItem => selectedItem is not null);

        AddCommand = ReactiveCommand.CreateFromTask(Add);
        EditCommand = ReactiveCommand.CreateFromTask(Edit, hasSelectedItem);
        RemoveCommand = ReactiveCommand.CreateFromTask(Remove, hasSelectedItem);

        Task.Run(Load);
    }

    private async Task Add()
    {
        await ItemInteraction.Handle(null);
    }

    private async Task Edit()
    {
        if (SelectedItem is null)
            return;

        await ItemInteraction.Handle(SelectedItem);
    }

    private async Task Remove()
    {
        if (SelectedItem is null)
            return;

        // await client.???(SelectedItem.Id);
        await Task.Delay(1000);
    }

    private async Task Load()
    {
        var client = new UserWallClient("http://localhost:5159", new HttpClient());
        var users = (await client.GetUsersAsync())
            .ToList();

        Dispatcher.UIThread.Post(() =>
        {
            foreach (var user in users)
                Items.Add(user);
        });
    }
}
