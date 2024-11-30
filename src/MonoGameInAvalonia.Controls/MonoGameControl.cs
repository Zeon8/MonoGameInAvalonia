using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;

namespace MonoGameInAvalonia.Controls;

public class MonoGameControl : UserControl
{
    /// <summary>
    /// Defines the <see cref="Game"/> property
    /// </summary>
    public static readonly DirectProperty<MonoGameControl, EmbeddedGame?> GameProperty =
        AvaloniaProperty.RegisterDirect<MonoGameControl, EmbeddedGame?>(nameof(Game),
            o => o.Game, (o, v) => o.Game = v);

    private EmbeddedGame? _game = default;

    /// <summary>
    /// Gets or sets the Game that will be embedded in control.
    /// </summary>
    public EmbeddedGame? Game
    {
        get => _game;
        set
        {
            if (value == _game)
                return;

            SetAndRaise(GameProperty, ref _game, value);

            Initialize();
        }
    }
    private void Initialize()
    {
        if (_game is not null)
            _game.WindowInitialized += Game_WindowInitialized;
        else
            Content = null;
    }

    private void Game_WindowInitialized(object? sender, System.EventArgs e)
    {
        Dispatcher.UIThread.Post(() => Content = new MonoGameNativeControl(_game!));
    }
}