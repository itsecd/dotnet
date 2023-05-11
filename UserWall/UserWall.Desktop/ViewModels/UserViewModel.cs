using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using ReactiveUI;

namespace UserWall.Desktop.ViewModels;

public sealed class UserViewModel : BaseViewModel
{
    public ObservableCollection<UserDto> Items { get; } = new();

    public int Id { get; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ReactiveCommand<Unit, Unit> ApplyCommand { get; }

    public UserViewModel(UserDto? userDto)
    {
        if (userDto is not null)
        {
            Id = userDto.Id;
            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
        }

        ApplyCommand = ReactiveCommand.CreateFromTask(Apply);
    }

    private async Task Apply()
    {
        var userDto = new UserDto
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName
        };

        // TODO: Validation

        await CloseWindowInteraction.Handle(userDto);
    }
}
