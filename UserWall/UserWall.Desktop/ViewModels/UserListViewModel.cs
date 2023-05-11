using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace UserWall.Desktop.ViewModels;

public sealed class UserListViewModel : BaseViewModel
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
        var userDto = await ItemInteraction.Handle(null);
        if (userDto is null)
            return;

        var client = Locator.Current.GetServiceOrThrow<UserWallClient>();

        // TODO: AutoMapper
        var id = await client.CreateUserAsync(new UserPostDto
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        });

        userDto = await client.GetUserAsync(id);

        Items.Add(userDto);
    }

    private async Task Edit()
    {
        if (SelectedItem is null)
            return;

        var userDto = await ItemInteraction.Handle(SelectedItem);
        if (userDto is null)
            return;

        var client = Locator.Current.GetServiceOrThrow<UserWallClient>();

        await client.UpdateUserAsync(userDto);

        Items.Replace(SelectedItem, userDto);
    }

    private async Task Remove()
    {
        if (SelectedItem is null)
            return;

        var client = Locator.Current.GetServiceOrThrow<UserWallClient>();

        await client.DeleteUserAsync(SelectedItem.Id);

        Items.Remove(SelectedItem);
    }

    private async Task Load()
    {
        var client = Locator.Current.GetServiceOrThrow<UserWallClient>();

        var users = (await client.GetUsersAsync()).ToList();

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            foreach (var user in users)
                Items.Add(user);
        });
    }
}
