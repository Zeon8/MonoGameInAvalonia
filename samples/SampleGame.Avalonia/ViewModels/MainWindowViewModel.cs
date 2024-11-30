using MonoGameInAvalonia;
using System.Threading;
using System.Threading.Tasks;

namespace SampleGame.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public EmbeddedGame Game { get; } = new Game1();

        public void RunGame()
        {
            new Thread(Game.Run)
            {
                IsBackground = true
            }
            .Start();
        }
    }
}
