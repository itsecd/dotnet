using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using ReactiveUI;

namespace UserWall.Desktop.ViewModels;

public class MessageViewModel : BaseViewModel
{
    public MessagePictogram Pictogram { get; }

    public string Title { get; } = "Сообщение";

    public string Message { get; }

    public ReactiveCommand<Unit, Unit> OkCommand { get; }

    public MessageViewModel(MessagePictogram pictogram, string message)
    {
        Pictogram = pictogram;
        Message = message;

        OkCommand = ReactiveCommand.CreateFromTask(Ok);
    }

    private async Task Ok()
    {
        await CloseWindowInteraction.Handle(null);
    }
}
