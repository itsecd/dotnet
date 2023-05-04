using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Avalonia.Threading;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace UserWall.Desktop.ViewModels;

public sealed class UserViewModel : ReactiveObject
{
    public ObservableCollection<UserDto> Items { get; } = new();

    public string Id { get; } = string.Empty;

    public ReactiveCommand<Unit, Unit> ApplyCommand { get; }

    public UserViewModel(UserDto? userDto)
    {
        if (userDto is not null)
        {
            Id = userDto.Id.ToString(CultureInfo.InvariantCulture);
        }

        ApplyCommand = ReactiveCommand.CreateFromTask(Apply);
    }

    private Task Apply()
    {
        // TODO
        return Task.CompletedTask;
    }
}
