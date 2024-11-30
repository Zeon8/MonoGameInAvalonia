using Microsoft.Xna.Framework;
using System;

namespace MonoGameInAvalonia
{
    public class EmbeddedGame : Game
    {
        public event EventHandler WindowInitialized;

        protected override void Initialize()
        {
            base.Initialize();
            WindowInitialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
