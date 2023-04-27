using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Avalonia.Threading;

using ReactiveUI;

namespace UserWall.Desktop.ViewModels;

public sealed class UserListViewModel : ReactiveObject
{
    public ObservableCollection<UserDto> Items { get; } = new();

    // [Reactive]
    public UserDto? SelectedItem { get; set; }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }

    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    public UserListViewModel()
    {
        this.WhenAnyValue(viewModel => viewModel.SelectedItem)
            .Subscribe((UserDto? userDto) => Debug.WriteLine(userDto));

        AddCommand = ReactiveCommand.CreateFromTask(Add);
        EditCommand = ReactiveCommand.CreateFromTask(Edit);
        RemoveCommand = ReactiveCommand.CreateFromTask(Remove);

        Task.Run(Load);
    }

    private Task Add()
    {
        return Task.CompletedTask;
    }

    private Task Edit()
    {
        return Task.CompletedTask;
    }

    private Task Remove()
    {
        return Task.CompletedTask;
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
